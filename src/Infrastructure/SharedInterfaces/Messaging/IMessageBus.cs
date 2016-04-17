using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetFirst.SharedInterfaces.Messaging
{
    /// <summary>
    /// Represents a message bus/queue, publish-subscribe pattern.
    /// Subscribers are to be automatically resolved
    /// </summary>
    public interface IMessageBus
    {
        /// <summary>
        /// Publish an event to all subscribers of that event
        /// </summary>
        /// <typeparam name="TDomainEvent">Event type</typeparam>
        /// <param name="domainEvent">Event to publish</param>
        void Publish<TDomainEvent>(TDomainEvent domainEvent) where TDomainEvent : IDomainEvent;
    }
}
