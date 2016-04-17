namespace BudgetFirst.SharedInterfaces.Messaging
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class EventTransaction : IEventTransaction
    {
        private List<IDomainEvent> events = new List<IDomainEvent>();

        public void Add<TDomainEvent>(TDomainEvent domainEvent) where TDomainEvent : IDomainEvent
        {
            this.events.Add(domainEvent);
        }

        public IReadOnlyList<IDomainEvent> GetEvents()
        {
            return this.events;
        }
    }
}
