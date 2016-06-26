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

namespace BudgetFirst.Budget.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using BudgetFirst.Budget.Domain.Aggregates;
    using BudgetFirst.SharedInterfaces.Messaging;

    /// <summary>
    /// Repository for <see cref="Account"/> aggregates
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
        /// Find (rehydrate) an account aggregate.
        /// </summary>
        /// <param name="id">Account Id</param>
        /// <param name="unitOfWork">Unit of work</param>
        /// <returns>Rehydrated aggregate</returns>
        /// <remarks>TODO: Could it happen that there are new events for the aggregate that are not on the aggregate in our unit of work?</remarks>
        public Account Find(Guid id, IAggregateUnitOfWork unitOfWork)
        {
            // This assumes that the aggregate in the unit of work is up-to-date
            var fromUnitOfWork = unitOfWork.Get<Account>(id);
            if (fromUnitOfWork != null)
            {
                return fromUnitOfWork;
            }

            var newAccount = new Account(id, this.eventStore.GetEventsFor(id));
            unitOfWork.Register(newAccount);
            return newAccount;
        }

        /// <summary>
        /// Save the (new) account (in the unit of work)
        /// </summary>
        /// <param name="account">Account to save</param>
        /// <param name="unitOfWork">Unit of work to use</param>
        public void Save(Account account, IAggregateUnitOfWork unitOfWork)
        {
            unitOfWork.Register(account);
        }
    }
}
