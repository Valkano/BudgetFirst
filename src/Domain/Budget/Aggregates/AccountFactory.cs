namespace BudgetFirst.Budget.Domain.Aggregates
{
    using System;
    using System.Collections.Generic;

    using BudgetFirst.Infrastructure.Messaging;
    
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
        /// <param name="unitOfWork">Current unit of work</param>
        /// <returns>A new account</returns>
        public static Account Create(Guid id, string name, IUnitOfWork unitOfWork)
        {
            return new Account(id, name, unitOfWork);
        }

        /// <summary>
        /// Load account from (event) history
        /// </summary>
        /// <param name="id">Account id</param>
        /// <param name="unitOfWork">Current unit of work</param>
        /// <returns>An existing account, loaded from the event history</returns>
        public static Account Load(Guid id, IUnitOfWork unitOfWork)
        {
            return new Account(id, unitOfWork);
        }
    }
}