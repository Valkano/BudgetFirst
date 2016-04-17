namespace BudgetFirst.MessageBus
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using SharedInterfaces.Messaging;
    using SharedInterfaces.DependencyInjection;

    public class MessageBus : IMessageBus
    {
        private IContainer container;

        public MessageBus(IContainer container)
        {
            this.container = container;
        }
        
        public void Publish<TDomainEvent>(TDomainEvent domainEvent) where TDomainEvent : IDomainEvent
        {
            var subscribers = this.container.ResolveAll<IDomainEventHandler<TDomainEvent>>();
            foreach(var subscriber in subscribers)
            {
                // TODO: error handling?
                subscriber.HandleEvent(domainEvent);
            }            
        }
    }
}
