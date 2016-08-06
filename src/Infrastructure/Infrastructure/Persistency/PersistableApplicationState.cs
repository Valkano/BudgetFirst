namespace BudgetFirst.Infrastructure.Persistency
{
    using System.Runtime.Serialization;

    using BudgetFirst.Infrastructure.EventSourcing;
    using BudgetFirst.Infrastructure.Messaging;

    /// <summary>
    /// Persistable application state - used to save and load the current application state (i.e. budget)
    /// </summary>
    [DataContract(Name = "PersistableApplicationState", Namespace = "http://budgetfirst.github.io/schemas/2016/07/23/PersistableApplicationState")]
    public class PersistableApplicationState
    {
        /// <summary>
        /// Gets or sets the current state for the event store
        /// </summary>
        [DataMember(Name = "EventStoreState")]
        public EventStoreState EventStoreState { get; set; }

        /// <summary>
        /// Gets or sets the current vector clock
        /// </summary>
        [DataMember(Name = "VectorClock")]
        public VectorClock VectorClock { get; set; }
    }
}