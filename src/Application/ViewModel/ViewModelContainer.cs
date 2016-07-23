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

namespace BudgetFirst.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using BudgetFirst.ApplicationCore.PlatformSpecific;
    using BudgetFirst.Wrappers;

    using Desktop;

    /// <summary>
    /// A Singleton Container for ViewModels.
    /// </summary>
    public class ViewModelContainer
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="ViewModelContainer"/> class.
        /// </summary>
        /// <param name="deviceSettings">Platform-specific device settings</param>
        /// <param name="persistableApplicationStateRepository">Platform-specific repository for application state</param>
        public ViewModelContainer(IDeviceSettings deviceSettings, IPersistableApplicationStateRepository persistableApplicationStateRepository)
        {
            this.Container = new BudgetFirst.Wrappers.Container();

            this.Container.Register<MainDesktopViewModel>(Container.Lifestyle.Singleton);
            this.Container.RegisterSingleton<IDeviceSettings>(deviceSettings);
            this.Container.RegisterSingleton<IPersistableApplicationStateRepository>(persistableApplicationStateRepository);
        }

        /// <summary>
        /// Gets the SimpleInjector Container.
        /// </summary>
        public Container Container { get; private set; }

        /// <summary>
        /// Returns an instance of <see cref="TInstance"/> from the container.
        /// </summary>
        /// <typeparam name="TInstance">The type to return.</typeparam>
        /// <returns>An instantiated <see cref="TInstance"/>.</returns>
        public TInstance Resolve<TInstance>() where TInstance : class
        {
            return this.Container.Resolve<TInstance>();
        }
    }
}