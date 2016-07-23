namespace BudgetFirst.SharedInterfaces.EventSourcing
{
    using System;

    /// <summary>
    /// A domain event is no valid to be persisted because some fields are missing.
    /// </summary>
    public class DomainEventIncompleteException : Exception
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="DomainEventIncompleteException"/> class.
        /// </summary>
        public DomainEventIncompleteException()
        {
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="DomainEventIncompleteException"/> class.
        /// </summary>
        /// <param name="message">Exception message text</param>
        public DomainEventIncompleteException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="DomainEventIncompleteException"/> class.
        /// </summary>
        /// <param name="message">Exception message text</param>
        /// <param name="innerException">Inner exception</param>
        public DomainEventIncompleteException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}