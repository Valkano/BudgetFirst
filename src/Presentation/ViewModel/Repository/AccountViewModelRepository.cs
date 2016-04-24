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
namespace BudgetFirst.ViewModel.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using ReadSide.ReadModel;
    using ReadSide.Repositories;
    using SharedInterfaces.Commands;

    /// <summary>
    /// Account view model repository
    /// </summary>
    public class AccountViewModelRepository
    {
        /// <summary>
        /// Account read model repository
        /// </summary>
        private readonly AccountReadModelRepository readModelRepository;

        /// <summary>
        /// Command bus
        /// </summary>
        private readonly ICommandBus commandBus;

        /// <summary>
        /// Initialises a new instance of the <see cref="AccountViewModelRepository"/> class.
        /// </summary>
        /// <param name="readModelRepository">Read model repository (to get and wrap read models)</param>
        /// <param name="commandBus">Command bus</param>
        public AccountViewModelRepository(AccountReadModelRepository readModelRepository, ICommandBus commandBus)
        {
            this.readModelRepository = readModelRepository;
            this.commandBus = commandBus;
        }

        /// <summary>
        /// Find an account with a specific Id
        /// </summary>
        /// <param name="id">Account Id</param>
        /// <returns>Account view model, or <c>null</c> if not found.</returns>
        public AccountViewModel Find(Guid id)
        {
            var readModel = this.readModelRepository.Find(id);
            if (readModel == null)
            {
                return null;
            }

            return new AccountViewModel(readModel, this.commandBus);
        }
    }
}
