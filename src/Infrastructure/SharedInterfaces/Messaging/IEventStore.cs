namespace BudgetFirst.SharedInterfaces.Messaging
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;


    public interface IEventStore
    {
        void Store(IDomainEvent domainEvent);
        void Store(IReadOnlyList<IDomainEvent> domainEvents);
        IReadOnlyList<IDomainEvent> GetEvents();
    }
}
