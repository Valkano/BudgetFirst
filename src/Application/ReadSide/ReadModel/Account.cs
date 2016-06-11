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

namespace BudgetFirst.ReadSide.ReadModel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using BudgetFirst.Budget.Domain.Commands.Account;
    using BudgetFirst.Budget.Domain.Interfaces.Events;
    using BudgetFirst.SharedInterfaces.Commands;
    using SharedInterfaces.ReadModel;

    /// <summary>
    /// Account read model
    /// </summary>
    public class Account : ReadModel, IAccount
    {
        /// <summary>
        /// The application's CommandBus
        /// </summary>
        private ICommandBus commandBus;

        /// <summary>
        /// Account name
        /// </summary>
        private string name;

        /// <summary>
        /// Initialises a new instance of the <see cref="Account"/> class.
        /// </summary>
        /// <param name="commandBus">The application's command bus</param>
        public Account(ICommandBus commandBus)
        {
            this.commandBus = commandBus;
        }

        /// <summary>
        /// Gets or sets the account name
        /// </summary>
        public string Name
        {
            get { return this.name; }
            set { this.commandBus.Submit(new ChangeAccountNameCommand() { Id = this.Id, Name = value }); }
        }

        /// <summary>
        /// Updates the Account's name backing variable directly without using the command bus, should be used by Generators ONLY.
        /// </summary>
        /// <param name="newName">The new name.</param>
        public void UpdateName(string newName)
        {
            this.SetProperty(ref this.name, newName, "Name");
        }
    }
}
