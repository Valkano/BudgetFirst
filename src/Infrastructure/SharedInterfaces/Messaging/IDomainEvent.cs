// BudgetFirst 
// ©2016 Thomas Mühlgrabner
//
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
// along with Budget First.  If not, see<http://www.gnu.org/licenses/>.
// ===================================================================
namespace BudgetFirst.SharedInterfaces.Messaging
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Represents a domain event, which is raised by an aggregate
    /// </summary>
    public interface IDomainEvent : IComparable
    {
        /// <summary>
        /// Gets the Id of the event
        /// </summary>
        Guid EventId { get; }

        /// <summary>
        /// Gets the Id of the device that the event happened on
        /// </summary>
        Guid DeviceId { get; }

        /// <summary>
        /// Gets the Id of the aggregate that raised this event
        /// </summary>
        Guid AggregateId { get; }

        /// <summary>
        /// Gets the UTC timestamp of when the event occurred
        /// </summary>
        DateTime Timestamp { get; }

        /// <summary>
        /// Gets the VectorClock for the event
        /// </summary>
        VectorClock VectorClock { get; }

        /// <summary>
        /// Is this event valid or are there missing fields?
        /// </summary>
        /// <returns><c>true</c> if all required fields are set</returns>
        bool IsValid();
    }
}
