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
    /// Read side account list item repository
    /// </summary>
    public class AccountListItemReadModelRepository : IReadModelRepository<AccountListItem>
    {
        /// <summary>
        /// Identity map (i.e. state)
        /// </summary>
        private Dictionary<Guid, AccountListItem> identityMap;

        /// <summary>
        /// Initialises a new instance of the <see cref="AccountListItemReadModelRepository"/> class.
        /// </summary>
        public AccountListItemReadModelRepository()
        {
            this.identityMap = new Dictionary<Guid, AccountListItem>();
        }

        /// <summary>
        /// Retrieve an account list item from the repository.
        /// </summary>
        /// <param name="id">Account Id</param>
        /// <returns>Reference to the account list item in the repository, if found. <c>null</c> otherwise.</returns>
        public AccountListItem Find(Guid id)
        {
            AccountListItem account;
            if (this.identityMap.TryGetValue(id, out account))
            {
                return account;
            }

            return null;
        }

        /// <summary>
        /// Save the account list item, or add it to the repository. 
        /// </summary>
        /// <param name="account">Account list item to save</param>
        public void Save(AccountListItem account)
        {
            this.identityMap[account.Id] = account;
        }
    }
}
