namespace BudgetFirst.SharedInterfaces.Messaging
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class EventStore : IEventStore
    {
        private List<IDomainEvent> store = new List<IDomainEvent>();

        public IReadOnlyList<IDomainEvent> GetEvents()
        {
            return this.store;
        }

        public void Store(IReadOnlyList<IDomainEvent> domainEvents)
        {
            this.store.AddRange(domainEvents);
        }

        public void Store(IDomainEvent domainEvent)
        {
            this.store.Add(domainEvent);
        }
    }
}
