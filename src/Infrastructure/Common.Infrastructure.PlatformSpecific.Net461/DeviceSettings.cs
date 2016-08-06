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

namespace BudgetFirst.Common.Infrastructure.PlatformSpecific.Net461
{
    using System;

    using BudgetFirst.Infrastructure.Persistency;

    /// <summary>
    /// Device settings for .net 4.6.1 platforms (i.e. Windows or Linux)
    /// </summary>
    public class DeviceSettings : IDeviceSettings
    {
        /// <summary>
        /// Settings repository
        /// </summary>
        private SettingsRepository repository = new SettingsRepository();

        /// <summary>
        /// Get the device Id
        /// </summary>
        /// <returns>Device Id</returns>
        public Guid GetDeviceId()
        {
            // TODO: extract the behaviour, it should be the same for every platform:
            // Read persisted device Id - or generate one (and save it) if it doesn't exist yet
            var settings = this.repository.GetSettings();
            if (settings.DeviceId == Guid.Empty)
            {
                settings.DeviceId = Guid.NewGuid();
                settings.Save();
            }

            return settings.DeviceId;
        }
    }
}