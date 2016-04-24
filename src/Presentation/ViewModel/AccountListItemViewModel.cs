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
namespace BudgetFirst.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Budget.Domain.Commands.Account;
    using ReadSide.ReadModel;
    using SharedInterfaces.Commands;
    using SharedInterfaces.DependencyInjection;

    /// <summary>
    /// Account view model
    /// </summary>
    public class AccountListItemViewModel : ViewModel<AccountListItem>, IAccountListItem
    {
        /// <summary>
        /// Command bus
        /// </summary>
        private readonly ICommandBus commandBus;

        /// <summary>
        /// Initialises a new instance of the <see cref="AccountListItemViewModel"/> class.
        /// </summary>
        /// <param name="readModel">Account read model to base the view model on.</param>
        /// <param name="commandBus">Command bus</param>
        public AccountListItemViewModel(AccountListItem readModel, ICommandBus commandBus) : base(readModel)
        {
            this.commandBus = commandBus;
        }

        /// <summary>
        /// Gets the Account Id
        /// </summary>
        public Guid Id
        {
            get
            {
                return this.ReadModel.Id;
            }
        }

        /// <summary>
        /// Gets the account name
        /// </summary>
        public string Name
        {
            get
            {
                return this.ReadModel.Name;
            }
        }

        /// <summary>
        /// Add a new account
        /// </summary>
        /// <param name="name">Account name</param>
        public void AddAccount(string name)
        {
            // TODO: error handling? Guid?
            this.commandBus.Submit(new CreateAccountCommand() { Id = Guid.NewGuid(), Name = name });
        }
    }
}
