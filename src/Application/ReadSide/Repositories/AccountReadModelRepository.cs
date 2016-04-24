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
namespace BudgetFirst.ReadSide.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml.Linq;
    using ReadModel;

    /// <summary>
    /// Read side account repository
    /// </summary>
    public class AccountReadModelRepository
    {
        /// <summary>
        /// Identity map (i.e. state)
        /// </summary>
        private Dictionary<Guid, Account> identityMap;

        /// <summary>
        /// Initialises a new instance of the <see cref="AccountReadModelRepository"/> class.
        /// </summary>
        public AccountReadModelRepository()
        {
            this.identityMap = new Dictionary<Guid, Account>();
        }

        /// <summary>
        /// Retrieve an account from the repository.
        /// </summary>
        /// <param name="id">Account Id</param>
        /// <returns>Reference to the account in the repository, if found. <c>null</c> otherwise.</returns>
        public Account Find(Guid id)
        {
            Account account;
            if (this.identityMap.TryGetValue(id, out account))
            {
                return account;
            }

            return null;
        }

        /// <summary>
        /// Save the account, or add it to the repository. 
        /// </summary>
        /// <param name="account">Account to save</param>
        public void Save(Account account)
        {
            this.identityMap[account.Id] = account;
        }
    }
}
