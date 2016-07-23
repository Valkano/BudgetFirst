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

    using BudgetFirst.SharedInterfaces.ApplicationState;
    using BudgetFirst.SharedInterfaces.EventSourcing;

    using Domain;

    /// <summary>
    /// Unit of work for aggregates.
    /// Should only be used on a single aggregate per unit of work.
    /// Combine multiple unit of works for multiple aggregates in a saga.
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// Event store for readonly access
        /// </summary>
        private IEventStore eventStore;

        /// <summary>
        /// Original vector clock
        /// </summary>
        private IVectorClock masterVectorClock;

        /// <summary>
        /// Message bus
        /// </summary>
        private IMessageBus messageBus;

        /// <summary>
        /// Initialises a new instance of the <see cref="UnitOfWork"/> class.
        /// </summary>
        /// <param name="readOnlyDeviceId">Device Id</param>
        /// <param name="vectorClock">Vector clock</param>
        /// <param name="eventStore">Event store</param>
        /// <param name="messageBus">Message bus</param>
        public UnitOfWork(IReadOnlyDeviceId readOnlyDeviceId, IVectorClock vectorClock, IEventStore eventStore, IMessageBus messageBus)
        {
            this.ReadOnlyDeviceId = readOnlyDeviceId;
            this.VectorClock = vectorClock.Clone(); // vector clock in unit of work must be isolated
            this.masterVectorClock = vectorClock;
            this.NewEvents = new List<DomainEvent>();
            this.eventStore = eventStore;
            this.messageBus = messageBus;
        }

        /// <summary>
        /// Gets the current device id
        /// </summary>
        public IReadOnlyDeviceId ReadOnlyDeviceId { get; }

        /// <summary>
        /// Gets the current vector clock
        /// </summary>
        public IVectorClock VectorClock { get; private set; }

        /// <summary>
        /// Gets the current list of events (only new events in this unit of work)
        /// </summary>
        public IList<DomainEvent> NewEvents { get; }

        /// <summary>
        /// Get ALL events for the aggregate - includes new events from the unit of work and events from the event store.
        /// </summary>
        /// <param name="aggregateId">Aggregate id</param>
        /// <returns>All events (from store and unit of work) for the aggregate</returns>
        public IReadOnlyList<DomainEvent> GetEventsForAggregate(Guid aggregateId)
        {
            var result = new List<DomainEvent>();
            var existingEvents = this.eventStore.GetEventsFor(aggregateId);
            if (existingEvents?.Any() ?? false)
            {
                result.AddRange(existingEvents);
            }

            result.AddRange(this.NewEvents.Where((x) => x.AggregateId == aggregateId));
            return result;
        }

        /// <summary>
        /// Commit the unit of work
        /// </summary>
        public void Commit()
        {
            this.StoreEvents();
            this.ApplyNewVectorClock();
            this.PublishEvents();
            this.ClearState();
        }

        /// <summary>
        /// Add the events from the transaction to the event store
        /// </summary>
        private void StoreEvents()
        {
            this.eventStore.Add(this.NewEvents);
        }

        /// <summary>
        /// Publish the events from the transaction
        /// </summary>
        private void PublishEvents()
        {
            foreach (var newEvent in this.NewEvents)
            {
                this.messageBus.Publish(newEvent);
            }
        }

        /// <summary>
        /// Apply the new vector clock
        /// </summary>
        private void ApplyNewVectorClock()
        {
            this.masterVectorClock.Update(this.VectorClock);
        }

        /// <summary>
        /// Clear the state (after commit)
        /// </summary>
        private void ClearState()
        {
            this.VectorClock = this.masterVectorClock.Clone();
            this.NewEvents.Clear();
        }
    }
}
