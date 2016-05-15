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
