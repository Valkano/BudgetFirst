// This file is part of BudgetFirst.
//
// BudgetFirst is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// BudgetFirst is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with Budget First.  If not, see<http://www.gnu.org/licenses/>.
// ===================================================================

namespace BudgetFirst.ReadSide.Handlers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Budget.Domain.Interfaces.Events;
    using BudgetFirst.SharedInterfaces.Commands;
    using ReadModel;
    using Repositories;
    using SharedInterfaces.Messaging;

    /// <summary>
    /// Generator for account lists
    /// </summary>
    public class AccountListGenerator : IDomainEventHandler<AccountCreated>, IDomainEventHandler<AccountNameChanged>
    {
        /// <summary>
        /// Account list repository
        /// </summary>
        private AccountListReadModelRepository accountListRepository;

        /// <summary>
        /// Account list item repository
        /// </summary>
        private AccountListItemReadModelRepository accountListItemRepository;

        /// <summary>
        /// Initialises a new instance of the <see cref="AccountListGenerator"/> class.
        /// </summary>
        /// <param name="accountListRepository">Account list repository to use</param>
        /// <param name="accountListItemRepository">Account list item repository to use</param>
        public AccountListGenerator(AccountListReadModelRepository accountListRepository, AccountListItemReadModelRepository accountListItemRepository)
        {
            this.accountListRepository = accountListRepository;
            this.accountListItemRepository = accountListItemRepository;
        }

        /// <summary>
        /// Account created event handler
        /// </summary>
        /// <param name="event">Account created event</param>
        public void Handle(AccountCreated @event)
        {
            var accountList = this.accountListRepository.Find();
            var account = this.accountListItemRepository.Find(@event.AggregateId);
            if (account == null)
            {
                account = new AccountListItem() { Id = @event.AggregateId, Name = @event.Name };
                this.accountListItemRepository.Save(account);
            }

            if (!accountList.Any(x => x.Id == @event.AggregateId))
            {
                accountList.Add(account);
                this.accountListRepository.Save(accountList);
            }
        }

        /// <summary>
        /// Account name changed event handler
        /// </summary>
        /// <param name="event">Account renamed event</param>
        public void Handle(AccountNameChanged @event)
        {
            var account = this.accountListItemRepository.Find(@event.AggregateId);
            if (account == null)
            {
                throw new InvalidOperationException("Account list item with id " + @event.AggregateId.ToString() + " was not found in repository.");
            }

            account.Name = @event.Name;
            this.accountListItemRepository.Save(account);
        }
    }
}
