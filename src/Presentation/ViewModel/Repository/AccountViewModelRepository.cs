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
namespace BudgetFirst.ViewModel.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Threading.Tasks;
    using ReadSide.ReadModel;
    using ReadSide.Repositories;
    using SharedInterfaces.Commands;

    /// <summary>
    /// Account view model repository
    /// </summary>
    [ComVisible(false)]
    public class AccountViewModelRepository : ViewModelRepository<AccountReadModelRepository, Account, AccountViewModel>
    {
       /// <summary>
        /// Initialises a new instance of the <see cref="AccountViewModelRepository"/> class.
        /// </summary>
        /// <param name="readModelRepository">Read model repository (to get and wrap read models)</param>
        /// <param name="commandBus">Command bus</param>
        public AccountViewModelRepository(AccountReadModelRepository readModelRepository, ICommandBus commandBus) : base(readModelRepository, commandBus)
        {
        }
        
        /// <summary>
        /// Maps the account read model to the account view model
        /// </summary>
        /// <param name="readModel">Account read model</param>
        /// <returns>Account view model</returns>
        protected override AccountViewModel Map(Account readModel)
        {
            return new AccountViewModel(readModel, this.CommandBus); 
        }
    }
}
