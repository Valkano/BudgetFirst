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
        /// The Window Service.
        /// </summary>
        private IWindowService windowService;

        /// <summary>
        /// The application's command bus
        /// </summary>
        private ICommandBus commandBus;

        /// <summary>
        /// The Account List.
        /// </summary>
        private AccountList accountList;

        /// <summary>
        /// The account that is selected.
        /// </summary>
        private AccountListItem selectedAccount;

        /// <summary>
        /// Initialises a new instance of the <see cref="MainDesktopViewModel"/> class.
        /// </summary>
        /// <param name="windowService">The platform's window service.</param>
        public MainDesktopViewModel(IWindowService windowService)
        {
            this.windowService = windowService;
            this.AddAccountCommand = new RelayCommand(() => this.AddAccount());
            this.RenameAccountCommand = new RelayCommand(() => this.RenameAccount());
            this.BootstrapDomain();
            var accountListRepo = ApplicationCore.Core.Default.Repositories.AccountListReadModelRepository;
            this.AccountList = accountListRepo.Find();
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
            this.windowService.ShowMessage("test");

            base.Close();
        }

        /// <summary>
        /// Adds a new account.
        /// </summary>
        private void AddAccount()
        {
            this.commandBus.Submit(new CreateAccountCommand() { Id = Guid.NewGuid(), Name = "Account Name" });
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

            var accountRepo = ApplicationCore.Core.Default.Repositories.AccountReadModelRepository;
            var account = accountRepo.Find(this.SelectedAccount.Id);
            account.Name = "Renamed Account";
        }

        /// <summary>
        /// Initialises the Application Core.
        /// </summary>
        private void BootstrapDomain()
        {
            // Bootstrap domain and access the message bus and command bus
            this.commandBus = ApplicationCore.Core.Default.CommandBus;
        }
    }
}