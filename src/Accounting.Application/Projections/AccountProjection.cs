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

    using BudgetFirst.Accounting.Application.Projections.Models;
    using BudgetFirst.Accounting.Application.Projections.Repositories;
    using BudgetFirst.Accounting.Domain.Events;
    using BudgetFirst.Common.Infrastructure.Commands;
    using BudgetFirst.Common.Infrastructure.Projections;

    /// <summary>
    /// Projection for accounts
    /// </summary>
    public class AccountProjection : IProjectFrom<AccountCreated>, IProjectFrom<AccountNameChanged> // any new handler must be registered in bootstrap
    {
        /// <summary>
        /// The application's Command Bus.
        /// </summary>
        private readonly ICommandBus commandBus;

        /// <summary>
        /// Account repository
        /// </summary>
        private AccountReadModelRepository repository;

        /// <summary>
        /// Initialises a new instance of the <see cref="AccountProjection"/> class.
        /// </summary>
        /// <param name="repository">Account repository to use</param>
        /// <param name="commandBus">The application's command bus</param>
        public AccountProjection(AccountReadModelRepository repository, ICommandBus commandBus)
        {
            this.repository = repository;
            this.commandBus = commandBus;
        }

        /// <summary>
        /// Account created event handler
        /// </summary>
        /// <param name="event">Account created event</param>
        public void Handle(AccountCreated @event)
        {
            var account = this.repository.Find(@event.AggregateId);
            if (account != null)
            {
                throw new InvalidOperationException("Account with id " + @event.AggregateId.ToString() + " is already created in repository.");
            }

            account = new Account(this.commandBus) { Id = @event.AggregateId };
            account.UpdateName(@event.Name);
            this.repository.Save(account);
        }

        /// <summary>
        /// Account name changed event handler
        /// </summary>
        /// <param name="event">Account renamed event</param>
        public void Handle(AccountNameChanged @event)
        {
            var account = this.repository.Find(@event.AggregateId);
            if (account == null)
            {
                throw new InvalidOperationException("Account with id " + @event.AggregateId.ToString() + " was not found in repository.");
            }

            account.UpdateName(@event.Name);
            this.repository.Save(account);
        }
    }
}
