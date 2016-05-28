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
    /// An event transaction encapsulates a list of eventsToAdd
    /// </summary>
    public class EventTransaction : IEventTransaction
    {
        /// <summary>
        /// List of eventsToAdd contained in this transaction
        /// </summary>
        private List<IDomainEvent> events = new List<IDomainEvent>();

        /// <summary>
        /// Add an event to the transaction
        /// </summary>
        /// <typeparam name="TDomainEvent">Type of the event</typeparam>
        /// <param name="domainEvent">Event to add</param>
        public void Add<TDomainEvent>(TDomainEvent domainEvent) where TDomainEvent : IDomainEvent
        {
            this.events.Add(domainEvent);
        }

        /// <summary>
        /// Add a collection of events to the transaction
        /// </summary>
        /// <param name="eventsToAdd">The events to add</param>
        public void Add(IEnumerable<IDomainEvent> eventsToAdd)
        {
            this.events.AddRange(eventsToAdd);
        }

        /// <summary>
        /// Get all eventsToAdd contained in this transaction.
        /// Beware: the eventsToAdd are referenced directly, so do not manipulate them.
        /// </summary>
        /// <returns>References to all eventsToAdd in the transaction</returns>
        public IReadOnlyList<IDomainEvent> GetEvents()
        {
            return this.events;
        }
    }
}
