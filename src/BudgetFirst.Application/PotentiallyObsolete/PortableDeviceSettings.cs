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

namespace BudgetFirst.Application.PotentiallyObsolete
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    /// <summary>
    /// Contains device settings used in portable mode
    /// </summary>
    [DataContract(Name = "PortableDeviceSettings", Namespace = "http://budgetfirst.github.io/schemas/2016/07/24/PortableDeviceSettings")]
    public class PortableDeviceSettings
    {
        /// <summary>
        /// Gets or sets the identifier used to automatically load a budget file on start. Can be <c>null</c>.
        /// </summary>
        [DataMember(Name = "AutoloadBudgetIdentifier")]
        public string AutoloadBudgetIdentifier { get; set; }

        /// <summary>
        /// Gets or sets the list of the recently opened budgets (display name + identifiers)
        /// </summary>
        [DataMember(Name = "RecentBudgets")]
        public List<PortableRecentBudget> RecentBudgets { get; set; }

        /// <summary>
        /// Contains a display name and the identifier of a recently opened budget
        /// </summary>
        [DataContract(Name = "RecentBudget", Namespace = "http://budgetfirst.github.io/schemas/2016/07/24/PortableDeviceSettings/RecentBudget")]
        public class PortableRecentBudget
        {
            /// <summary>
            /// Gets or sets the display name of the budget file
            /// </summary>
            [DataMember(Name = "DisplayName")]
            public string DisplayName { get; set; }

            /// <summary>
            /// Gets or sets the identifier of the budget (file)
            /// </summary>
            [DataMember(Name = "Identifier")]
            public string Identifier { get; set; }
        }
    }
}