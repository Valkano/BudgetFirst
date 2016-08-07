// BudgetFirst 
// ©2016 Thomas Mühlgrabner
// This source code is dual-licensed under:
//   * Mozilla Public License 2.0 (MPL 2.0) 
//   * GNU General Public License v3.0 (GPLv3)
// ==================== Mozilla Public License 2.0 ===================
// This Source Code Form is subject to the terms of the Mozilla Public 
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.
// ================= GNU General Public License v3.0 =================
// This file is part of BudgetFirst.
// BudgetFirst is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// BudgetFirst is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
// GNU General Public License for more details.
// You should have received a copy of the GNU General Public License
// along with Budget First.  If not, see<http://www.gnu.org/licenses/>.
// ===================================================================
namespace BudgetFirst.ViewModel.Desktop
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    using BudgetFirst.Infrastructure.Persistency;

    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;
    using GalaSoft.MvvmLight.Views;

    using Microsoft.Practices.ServiceLocation;

    /// <summary>
    /// Initial page view model. Contains steps to open or create a budget.
    /// </summary>
    public class WelcomeViewModel : ViewModelBase
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="WelcomeViewModel"/> class.
        /// </summary>
        /// <param name="deviceSettings">Device settings</param>
        public WelcomeViewModel(IDeviceSettings deviceSettings)
        {
            var recentBudgets = deviceSettings.GetRecentBudgets() ?? new List<Infrastructure.Persistency.RecentBudget>();

            // Map to viewmodel-specific model
            var viewModelRecentBudgets =
                recentBudgets.Select(
                    x => new RecentBudget()
                             {
                                 Name = x.DisplayName, 
                                 OpenCommand = new RelayCommand(
                                     () =>
                                         {
                                             var navigationService =
                                                 ServiceLocator.Current.GetInstance<INavigationService>();
                                             navigationService.NavigateTo(ViewModelContainer.OpenBudgetPageKey, x.Identifier);
                                         }), 
                             });

            this.RecentBudgets = new ObservableCollection<RecentBudget>(viewModelRecentBudgets);

            // Initialise relay commands
            this.CreateNewBudgetCommand = new RelayCommand(
                () =>
                    {
                        var navigationService =
                                                 ServiceLocator.Current.GetInstance<INavigationService>();
                        navigationService.NavigateTo(ViewModelContainer.CreateNewBudgetPageKey);
                    });
        }

        /// <summary>
        /// Gets the create new budget command
        /// </summary>
        public RelayCommand CreateNewBudgetCommand { get; }

        /// <summary>
        /// Gets the recent budgets
        /// </summary>
        public ObservableCollection<RecentBudget> RecentBudgets { get; private set; }

        /// <summary>
        /// View model specific recent budget representation
        /// </summary>
        public class RecentBudget
        {
            /// <summary>
            /// Gets or sets the name of the recent budget
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// Gets or sets the command to invoke when opening the budget
            /// </summary>
            public RelayCommand OpenCommand { get; set; }
        }
    }
}