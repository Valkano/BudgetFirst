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

namespace BudgetFirst.Presentation.Wpf.PlatformSpecific
{
    using System;

    using BudgetFirst.Common.Infrastructure.PlatformSpecific.Net461;
    using BudgetFirst.Infrastructure.Persistency;

    /// <summary>
    /// Contains the device settings for the Windows platform
    /// </summary>
    public class WindowsDeviceSettings : IDeviceSettings
    {
        /// <summary>
        /// Platform-specific device settings - this one is for .net 4.6.1
        /// </summary>
        private DeviceSettings settings = new DeviceSettings(); 

        /// <summary>
        /// Get the current device Id
        /// </summary>
        /// <returns>Current device Id</returns>
        public Guid GetDeviceId()
        {
            return this.settings.GetDeviceId();
        }
    }
}