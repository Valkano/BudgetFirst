namespace BudgetFirst.SharedInterfaces.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using BudgetFirst.SharedInterfaces.Messaging;

    public abstract class AggregateRoot
    {
        private IEventTransaction eventTransaction;

        protected AggregateRoot(IEventTransaction eventTransaction)
        {
            this.eventTransaction = eventTransaction;
        }

        public void RaiseEvent<TDomainEvent>(TDomainEvent domainEvent) where TDomainEvent : IDomainEvent
        {
            var self = this as IDomainEventHandler<TDomainEvent>;
            if(self != null)
            {
                self.HandleEvent(domainEvent);
            }
            this.eventTransaction.Add(domainEvent);
        }
    }
}
