// This source code is dual-licensed under:
//   * Mozilla Public License 2.0 (MPL 2.0) 
//   * GNU General Public License v3.0 (GPLv3)
//
// ==================== Mozilla Public License 2.0 ===================
// This Source Code Form is subject to the terms of the Mozilla Public 
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.
// ================= GNU General Public License v3.0 =================
// This file is part of BudgetFirst.
//
// BudgetFirst is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// BudgetFirst is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with Foobar.  If not, see<http://www.gnu.org/licenses/>.
// ===================================================================
namespace BudgetFirst.SharedInterfaces.Messaging
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;


    public enum VectorComparison
    {
        Equal, Greater, Smaller, Simultaneous
    }

    /// <summary>
    /// A Vector Clock that can tell the relative order of events on distributed systems.
    /// </summary>
    public class VectorClock : IComparable
    {
        public IReadOnlyDictionary<string, int> Vector { get; private set; }
        public DateTime Timestamp { get; private set; }

        public VectorClock()
        {
            this.Vector = new Dictionary<string, int>();
            Timestamp = DateTime.UtcNow;
        }

        public VectorClock(Dictionary<string, int> vector)
        {
            this.Vector = vector;
            Timestamp = DateTime.UtcNow;
        }

        /// <summary>
        /// Create a copy of the current VectorClock and Increment the Vector for the given Device ID by 1 on the new VectorClock
        /// </summary>
        /// <param name="deviceId">The deviceId to increment.</param>
        /// <returns>The new incremented VectorClock</returns>
        public VectorClock Increment(string deviceId)
        {
            Dictionary<string, int> newVector = this.CopyVector();
            if (newVector.ContainsKey(deviceId))
            {
                newVector[deviceId]++;
            }
            else
            {
                newVector[deviceId] = 1;
            }

            return new VectorClock(newVector);
        }

        /// <summary>
        /// Merges the VectorClock with a second VectorClock by returning a new VectorClock
        /// that has maximum vector value for each device.
        /// </summary>
        /// <param name="clock2">The VectorClock to be merged into the current VectorClock.</param>
        /// <returns>A new VectorClock with the maximum value for each device.</returns>
        public VectorClock Merge(VectorClock clock2)
        {
            Dictionary<string, int> mergedVector = this.CopyVector();

            foreach (string deviceId in this.Vector.Keys)
            {
                if (clock2.Vector.ContainsKey(deviceId))
                {
                    mergedVector[deviceId] = Math.Max(this.Vector[deviceId], clock2.Vector[deviceId]);
                }
                else
                {
                    mergedVector[deviceId] = this.Vector[deviceId];
                }
            }

            foreach (string deviceId in clock2.Vector.Keys)
            {
                if (!this.Vector.ContainsKey(deviceId))
                {
                    mergedVector[deviceId] = clock2.Vector[deviceId];
                }
            }

            return new VectorClock(mergedVector);
        }

        /// <summary>
        /// A function to compare the current VectorClock to another and 
        /// determine which came first, or if the events happend simultaneously.
        /// </summary>
        /// <param name="clock2">The VectorClock to compare.</param>
        /// <returns>A VectorComparison enum with the result of the comparison.</returns>
        public VectorComparison CompareVectors(VectorClock clock2)
        {
            /* We check every deviceId that is a key in this vector clock against every deviceId in clock2.
             * If all deviceId values in both clocks are equal they are the same clock(equal).  This result should never happen in BudgetFirst since we always increment the clock for each event.
             * If every deviceId value in this clock is less than the deviceId in clock2 then this VectorClock came before clock2. Some of the deviceIds can be equal but at least one must be less for this case.
             * If every deviceId value in this clock is greater than the deviceId in clock2 then this VectorClock came after clock2.   Some of the deviceIds can be equal but at least one must be greater for this case.
             * If at least one deviceId is greater, and at least one other deviceId is less between the two clocks, the events happened simultaneously.
             */

            bool equal = true;
            bool greater = true;
            bool smaller = true;

            foreach (string deviceId in this.Vector.Keys)
            {
                if (clock2.Vector.ContainsKey(deviceId))
                {
                    if (this.Vector[deviceId] < clock2.Vector[deviceId])
                    {
                        equal = false;
                        greater = false;
                    }
                    if (this.Vector[deviceId] > clock2.Vector[deviceId])
                    {
                        equal = false;
                        smaller = false;
                    }
                }
                else if (this.Vector[deviceId] != 0)
                {
                    equal = false;
                    smaller = false;
                }
            }

            //Check if clock2 has any deviceIds that are not present in this VectorClock
            foreach (string deviceId in clock2.Vector.Keys)
            {
                if (!this.Vector.ContainsKey(deviceId) && clock2.Vector[deviceId] != 0)
                {
                    equal = false;
                    greater = false;
                }
            }


            if (equal)
            {
                //The vectors are the same
                return VectorComparison.Equal;
            }
            else if (greater && !smaller)
            {
                return VectorComparison.Greater;
            }
            else if (smaller && !greater)
            {
                return VectorComparison.Smaller;
            }
            else
            {
                //The events were simultaneuous
                return VectorComparison.Simultaneous;
            }
        }

        /// <summary>
        /// The default comparer for VectorClock.  Determines the order of events 
        /// based on the vector and the timestamp.
        /// </summary>
        /// <param name="obj">The VectorClock to compare to.</param>
        /// <returns>1 if this VectorClock happened after, 
        /// -1 if this VectorClock happened before,
        /// 0 if the order can not be determined(Simultaneous or Equal Vectors and the same timestamp)</returns>
        public int CompareTo(object obj)
        {
            VectorClock clock2 = obj as VectorClock;
            VectorComparison compare = this.CompareVectors(clock2);
            if (compare == VectorComparison.Greater)
                return 1;
            else if (compare == VectorComparison.Smaller)
                return -1;
            else //Either simlutaneuous or equal falls back to timestamp comparison
            {
                return DateTime.Compare(this.Timestamp, clock2.Timestamp);
            }
        }

        /// <summary>
        /// Create an exact copy of this current VectorClock.
        /// </summary>
        /// <returns>A copy of the current VectorClock</returns>
        public VectorClock Copy()
        {
            return new VectorClock(this.CopyVector());
        }

        /// <summary>
        /// Creates a Dictionary Copy of the IReadOnlyDictionary Vector.
        /// </summary>
        private Dictionary<string, int> CopyVector()
        {
            return this.Vector.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        }


    }
}
