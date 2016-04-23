namespace Budget.Repositories
{
    using BudgetFirst.Budget.Domain.Aggregates;
    using BudgetFirst.SharedInterfaces.Messaging;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class AccountRepository
    {
        private IEventStore eventStore;

        public AccountRepository(IEventStore eventStore)
        {
            this.eventStore = eventStore;
        }

        // TODO: should perhaps be moved to domain, even if not as clean. would allow for use of internal constructor
    }
}
