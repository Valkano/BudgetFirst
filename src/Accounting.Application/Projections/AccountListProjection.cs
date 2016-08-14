// BudgetFirst 
// ©2016 Thomas Mühlgrabner
//
// This source code is dual-licensed under:
//   * Mozilla Public License 2.0 (MPL 2.0) 
//   * GNU General Public License v3.0 (GPLv3)
//
// ==================== Mozilla Public License 2.0 ===================
// This Source Code Form is subject to the terms of the Mozilla Public 
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.
// ================= GNU General Public License v3.0 =================
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

namespace BudgetFirst.Accounting.Application.Projections
{
    using System;
    using System.Linq;

    using BudgetFirst.Accounting.Application.Projections.Models;
    using BudgetFirst.Accounting.Application.Projections.Repositories;
    using BudgetFirst.Accounting.Domain.Events;
    using BudgetFirst.Common.Infrastructure.Projections;

    /// <summary>
    /// Projection for account lists
    /// </summary>
    public class AccountListProjection : IProjectFrom<AccountCreated>, IProjectFrom<AccountNameChanged> // any new handler must be registered in bootstrap
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
        /// Initialises a new instance of the <see cref="AccountListProjection"/> class.
        /// </summary>
        /// <param name="accountListRepository">Account list repository to use</param>
        /// <param name="accountListItemRepository">Account list item repository to use</param>
        public AccountListProjection(AccountListReadModelRepository accountListRepository, AccountListItemReadModelRepository accountListItemRepository)
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
            if (accountList == null)
            {
                accountList = new AccountList();
                this.accountListRepository.Save(accountList);
            }

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
