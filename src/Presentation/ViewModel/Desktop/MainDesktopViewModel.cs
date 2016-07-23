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
namespace BudgetFirst.ViewModel.Desktop
{
    using System;
    using BudgetFirst.Budget.Domain.Commands.Account;
    using BudgetFirst.ReadSide.ReadModel;
    using BudgetFirst.SharedInterfaces.Commands;
    using BudgetFirst.SharedInterfaces.Messaging;
    using BudgetFirst.ViewModel.Services;
    using BudgetFirst.ViewModel.Shared;
    using GalaSoft.MvvmLight.Command;

    /// <summary>
    /// Represents the main Application for Desktop Platforms.
    /// </summary>
    public class MainDesktopViewModel : ClosableViewModel
    {
        /// <summary>
        /// Current application core
        /// </summary>
        private ApplicationCore.Core applicationCore;

        /// <summary>
        /// The Window Service.
        /// </summary>
        private IWindowService windowService;

     /// <summary>
        /// The Account List.
        /// </summary>
        private AccountList accountList;

        /// <summary>
        /// The account that is selected.
        /// </summary>
        private AccountListItem selectedAccount;

        /// <summary>
        /// Counter to cause rename of account to be different each time
        /// </summary>
        private int renameCount = 0;

        /// <summary>
        /// Initialises a new instance of the <see cref="MainDesktopViewModel"/> class.
        /// </summary>
        /// <param name="windowService">The platform's window service.</param>
        public MainDesktopViewModel(IWindowService windowService)
        {
            this.windowService = windowService;
            this.applicationCore = ApplicationCore.CoreFactory.CreateNewBudget();
            this.InitialiseRelayCommands();
            this.accountList = this.applicationCore.Repositories.AccountListReadModelRepository.Find();

            // Account list can be null if we haven't loaded any data yet -> we won't be informed about property changes!
            // TODO: refactor this, because this means that we're accessing the read model repository directly for modifications, which we shouldn't
            if (this.accountList == null)
            {
                this.accountList = new AccountList();
                this.applicationCore.Repositories.AccountListReadModelRepository.Save(this.accountList);
            }
        }

        /// <summary>
        /// Gets the AccountList
        /// </summary>
        public AccountList AccountList
        {
            get
            {
                return this.accountList;
            }

            private set
            {
                this.accountList = value;
                this.RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Gets the command to add an Account.
        /// </summary>
        public RelayCommand AddAccountCommand { get; private set; }

        /// <summary>
        /// Gets the command to rename the selected Account.
        /// </summary>
        public RelayCommand RenameAccountCommand { get; private set; }

        /// <summary>
        /// Gets or sets the selected account.
        /// </summary>
        public AccountListItem SelectedAccount
        {
            get
            {
                return this.selectedAccount;
            }

            set
            {
                this.selectedAccount = value;
                this.RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Closes the application.
        /// </summary>
        protected override void Close()
        {
            // Run any closing logic then call to the base method to actually close the window.
            // this.windowService.ShowMessage("test");
            base.Close();
        }

        /// <summary>
        /// Initialise all relay command properties
        /// </summary>
        private void InitialiseRelayCommands()
        {
            this.AddAccountCommand = new RelayCommand(this.AddAccount);
            this.RenameAccountCommand = new RelayCommand(this.RenameAccount);
        }

        /// <summary>
        /// Adds a new account.
        /// </summary>
        private void AddAccount()
        {
            this.applicationCore.CommandBus.Submit(new CreateAccountCommand() { Id = Guid.NewGuid(), Name = "Account Name" });
        }

        /// <summary>
        /// Renames the selected account.
        /// </summary>
        private void RenameAccount()
        {
            if (this.SelectedAccount == null)
            {
                return;
            }

            var accountRepo = this.applicationCore.Repositories.AccountReadModelRepository;
            var account = accountRepo.Find(this.SelectedAccount.Id);
            account.Name = $"Renamed Account ({this.renameCount++})";
        }
    }
}