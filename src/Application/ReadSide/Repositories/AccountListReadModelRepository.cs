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
    /// Read side account list repository
    /// </summary>
    public class AccountListReadModelRepository 
    {
        /// <summary>
        /// Single account list contained in this repository
        /// </summary>
        private AccountList accountList;

        /// <summary>
        /// Initialises a new instance of the <see cref="AccountListReadModelRepository"/> class.
        /// </summary>
        public AccountListReadModelRepository()
        {
            this.accountList = new AccountList();
        }

        /// <summary>
        /// Retrieve an account list from the repository.
        /// </summary>
        /// <returns>Reference to the account list in the repository, if found. <c>null</c> otherwise.</returns>
        public AccountList Find()
        {
            return this.accountList;
        }

        /// <summary>
        /// Save the account list, or add it to the repository. Beware: replaces existing account list in repository.
        /// </summary>
        /// <param name="accountList">Account list to save</param>
        public void Save(AccountList accountList)
        {
            this.accountList = accountList;
        }
    }
}
