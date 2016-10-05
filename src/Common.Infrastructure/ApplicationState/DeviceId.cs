﻿// BudgetFirst 
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

namespace BudgetFirst.Common.Infrastructure.ApplicationState
{
    using System;

    /// <summary>
    /// Contains the current device Id
    /// </summary>
    public class DeviceId : IDeviceId
    {
        /// <summary>
        /// Current device Id
        /// </summary>
        private Guid deviceId;

        /// <summary>
        /// Set the current device Id
        /// </summary>
        /// <param name="id">Current device Id</param>
        public void SetDeviceId(Guid id)
        {
            this.deviceId = id;
        }

        /// <summary>
        /// Get the current device Id
        /// </summary>
        /// <returns>The current device Id</returns>
        public Guid GetDeviceId()
        {
            return this.deviceId;
        }

        /// <summary>
        /// To string
        /// </summary>
        /// <returns>String value of device id. Always formatted the same</returns>
        public override string ToString()
        {
            return this.deviceId.ToString("d");
        }
    }
}