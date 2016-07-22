namespace BudgetFirst.SharedInterfaces
{
    using System;

    using BudgetFirst.SharedInterfaces.EventSourcing;
    using BudgetFirst.SharedInterfaces.Messaging;

    /// <summary>
    /// Represents the current application state
    /// </summary>
    public interface IApplicationState
    {
        /// <summary>
        /// Gets or sets the current event store
        /// </summary>
        IEventStore EventStore { get; set; }

        /// <summary>
        /// Gets or sets the current device Id
        /// </summary>
        Guid DeviceId { get; set; }

        /// <summary>
        /// Gets or sets the current vector clock
        /// </summary>
        VectorClock VectorClock { get; set;  }
    }
}