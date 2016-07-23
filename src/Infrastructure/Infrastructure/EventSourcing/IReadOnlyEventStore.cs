namespace BudgetFirst.Infrastructure.EventSourcing
{
    using System;
    using System.Collections.Generic;

    using BudgetFirst.Infrastructure.Messaging;

    /// <summary>
    /// Read-only access to the event store
    /// </summary>
    public interface IReadOnlyEventStore
    {
        /// <summary>
        /// Get all events in the store
        /// </summary>
        /// <returns>All events in the store</returns>
        IReadOnlyList<DomainEvent> GetEvents();

        /// <summary>
        /// Get all saved events for a specific aggregate.
        /// Beware: events are referenced directly, do not manipulate them.
        /// </summary>
        /// <param name="aggregateId">Aggregate Id</param>
        /// <returns>Reference to all events for the given aggregate</returns>
        IReadOnlyList<DomainEvent> GetEventsFor(Guid aggregateId);
    }
}