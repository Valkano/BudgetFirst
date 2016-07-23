namespace BudgetFirst.SharedInterfaces.Persistence
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    using BudgetFirst.SharedInterfaces.EventSourcing;
    using BudgetFirst.SharedInterfaces.Messaging;

    /// <summary>
    /// State for the <see cref="EventStore"/>
    /// </summary>
    [DataContract(Name = "EventStoreState", Namespace = "http://budgetfirst.github.io/schemas/2016/07/23/EventStoreState")]
    public sealed class EventStoreState
    {
        /// <summary>
        /// Contains the list of events in this store
        /// </summary>
        [DataMember(Name = "Events")]
        private List<DomainEvent> events;

        /// <summary>
        /// Gets or sets the events in this store.
        /// Is guaranteed to be not <c>null</c>.
        /// </summary>
        public List<DomainEvent> Events
        {
            get
            {
                return this.events ?? (this.events = new List<DomainEvent>());
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException();
                }

                this.events = value;
            }
        }
    }
}