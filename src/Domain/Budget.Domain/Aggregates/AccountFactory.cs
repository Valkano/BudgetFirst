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
// along with Foobar.  If not, see<http://www.gnu.org/licenses/>.
// ===================================================================
namespace BudgetFirst.Budget.Domain.Aggregates
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using SharedInterfaces.Messaging;

    /// <summary>
    /// Factory for <see cref="Account"/> aggregates.
    /// </summary>
    public class AccountFactory
    {
        /// <summary>
        /// Create a new account
        /// </summary>
        /// <param name="id">Account Id</param>
        /// <param name="name">Account name</param>
        /// <returns>New account</returns>
        public static Account CreateAccount(Guid id, string name)
        {
            var account = new Account(id, name);
            return account;
        }

        /// <summary>
        /// Load an account from history
        /// </summary>
        /// <param name="id">Account to load</param>
        /// <param name="history">History to load from</param>
        /// <returns>Rehydrated aggregate</returns>
        public static Account ReconstituteFromEvents(Guid id, IEnumerable<IDomainEvent> history) // TODO: should be repository
        {
            var account = new Account(id, history); 
            return account;
        }
    }
}
