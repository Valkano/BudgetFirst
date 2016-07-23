namespace BudgetFirst.Budget.Domain.Aggregates
{
    using System;
    using System.Collections.Generic;

    using BudgetFirst.SharedInterfaces;
    using BudgetFirst.SharedInterfaces.Messaging;

    /// <summary>
    /// Factory for <see cref="Account">accounts</see>
    /// </summary>
    public static class AccountFactory
    {
        /// <summary>
        /// Creates a new account
        /// </summary>
        /// <param name="id">Account id</param>
        /// <param name="name">Account name</param>
        /// <param name="applicationState">Current application state</param>
        /// <returns>A new account</returns>
        public static Account Create(Guid id, string name, IApplicationState applicationState)
        {
            return new Account(id, name);
        }

        /// <summary>
        /// Load account from (event) history
        /// </summary>
        /// <param name="id">Account id</param>
        /// <param name="applicationState">Current application state</param>
        /// <returns>An existing account, loaded from the event history</returns>
        public static Account Load(Guid id, IApplicationState applicationState)
        {
            return new Account(id, applicationState.EventStore.GetEventsFor(id));
        }
    }
}