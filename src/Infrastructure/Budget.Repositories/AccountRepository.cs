namespace BudgetFirst.Budget.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using BudgetFirst.Budget.Domain.Aggregates;
    using BudgetFirst.SharedInterfaces.Messaging;

    /// <summary>
    /// Repository for <see cref="Account"/>
    /// </summary>
    public class AccountRepository
    {
        /// <summary>
        /// Event store
        /// </summary>
        private readonly IEventStore eventStore;

        /// <summary>
        /// Initialises a new instance of the <see cref="AccountRepository"/> class.
        /// </summary>
        /// <param name="eventStore">Event store</param>
        public AccountRepository(IEventStore eventStore)
        {
            this.eventStore = eventStore;
        }

        /// <summary>
        /// Find (rehydrate) an account aggregate
        /// </summary>
        /// <param name="id">Account Id</param>
        /// <returns>Rehydrated aggregate</returns>
        public Account Find(Guid id)
        {
            return new Account(id, this.eventStore.GetEvents());
        }
    }
}
