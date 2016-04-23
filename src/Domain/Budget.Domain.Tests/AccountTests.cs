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
// along with Foobar.  If not, see<http://www.gnu.org/licenses/>.
// ===================================================================
namespace BudgetFirst.Budget.Domain.Tests
{
    using System;
    using BudgetFirst.Budget.Domain.Aggregates;
    using BudgetFirst.Budget.Domain.Events;
    using BudgetFirst.SharedInterfaces.Messaging;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Contains tests for the account aggregate
    /// </summary>
    /// <remarks>TODO: Switching to a different unit testing framework might be a good idea (multiplatform-support!)</remarks>
    [TestClass]
    public class AccountTests
    {
        /// <summary>
        /// A new account must have a correct name
        /// </summary>
        [TestMethod]
        public void NewAccountHasName()
        {
            var account = AccountFactory.CreateAccount(new Guid("DB1C3C3E-C8C4-47A0-AD43-F154FDDB0577"), "Test1");
            Assert.AreEqual("Test1", account.Name);
        }

        /// <summary>
        /// An account, that is loaded from history, must have the correct name
        /// </summary>
        [TestMethod]
        public void ReconstitutedNewAccountHasCorrectName()
        {
            var accountId = new Guid("A34C7724-F9FE-4A14-89A2-C8F1D662EE2A");
            var eventStore = new EventStore();
            var prevouslyCreatedAccount = AccountFactory.CreateAccount(accountId, "Test2");
            eventStore.Add(prevouslyCreatedAccount.Events);

            var account = AccountFactory.ReconstituteFromEvents(accountId, eventStore.GetEvents());

            Assert.AreEqual("Test2", account.Name);
        }
    }
}
