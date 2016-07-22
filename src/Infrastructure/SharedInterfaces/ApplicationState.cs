namespace BudgetFirst.SharedInterfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using BudgetFirst.SharedInterfaces.EventSourcing;
    using BudgetFirst.SharedInterfaces.Messaging;

    /// <summary>
    /// Contains the current application state
    /// </summary>
    public class ApplicationState : IApplicationState
    {
        /// <summary>
        /// Gets or sets the current event store
        /// </summary>
        public IEventStore EventStore { get; set; }

        /// <summary>
        /// Gets or sets the current device Id
        /// </summary>
        public Guid DeviceId { get; set; }

        /// <summary>
        /// Gets or sets the current vector clock
        /// </summary>
        public VectorClock VectorClock { get; set; }
    }
}
