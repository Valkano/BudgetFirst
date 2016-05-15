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
    using ReadModel;
    using Repositories;
    using SharedInterfaces.Messaging;

    /// <summary>
    /// Generator for accounts
    /// </summary>
    public class AccountGenerator : IDomainEventHandler<AccountCreated>, IDomainEventHandler<AccountNameChanged>
    {
        /// <summary>
        /// Account repository
        /// </summary>
        private AccountReadModelRepository repository;

        /// <summary>
        /// Initialises a new instance of the <see cref="AccountGenerator"/> class.
        /// </summary>
        /// <param name="repository">Account repository to use</param>
        public AccountGenerator(AccountReadModelRepository repository)
        {
            this.repository = repository;
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

            account = new Account() { Id = @event.AggregateId, Name = @event.Name };
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

            account.Name = @event.Name;
            this.repository.Save(account);
        }
    }
}
