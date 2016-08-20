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

namespace BudgetFirst.Budgeting.Application.Projections
{
    using System;
    using System.Linq;

    using BudgetFirst.Budgeting.Application.Projections.Models;
    using BudgetFirst.Budgeting.Application.Projections.Repositories;
    using BudgetFirst.Budgeting.Domain.Events;
    using BudgetFirst.Common.Infrastructure.Commands;
    using BudgetFirst.Common.Infrastructure.Projections;

    /// <summary>
    /// Projection for budget lists
    /// </summary>
    public class BudgetListProjection : IProjectFrom<AddedBudget> // any new handler must be registered in bootstrap
    {
        /// <summary>
        /// Budget list repository
        /// </summary>
        private BudgetListRepository budgetListRepository;

        /// <summary>
        /// Budget list item repository
        /// </summary>
        private BudgetListItemlRepository budgetListItemRepository;

        /// <summary>
        /// Command bus
        /// </summary>
        private ICommandBus commandBus;

        /// <summary>
        /// Initialises a new instance of the <see cref="BudgetListProjection"/> class.
        /// </summary>
        /// <param name="budgetListRepository">Budget list repository to use</param>
        /// <param name="budgetListItemRepository">Budget list item repository to use</param>
        /// <param name="commandBus">Command bus</param>
        public BudgetListProjection(
            BudgetListRepository budgetListRepository,
            BudgetListItemlRepository budgetListItemRepository,
            ICommandBus commandBus)
        {
            this.budgetListRepository = budgetListRepository;
            this.budgetListItemRepository = budgetListItemRepository;
            this.commandBus = commandBus;
        }

        /// <summary>
        /// Budget created/added event handler
        /// </summary>
        /// <param name="e">Budget created event</param>
        public void Handle(AddedBudget e)
        {
            var budgetList = this.budgetListRepository.Find();
            if (budgetList == null)
            {
                budgetList = new BudgetList();
                this.budgetListRepository.Save(budgetList);
            }
            
            var account = this.budgetListItemRepository.Find(e.BudgetId);
            if (account == null)
            {
                account = new BudgetListItem(e.BudgetId, e.Name, this.commandBus);
                this.budgetListItemRepository.Save(account);
            }

            if (!budgetList.Any(x => e.BudgetId.Equals(x.Id)))
            {
                budgetList.Add(account);
                this.budgetListRepository.Save(budgetList);
            }
        }
    }
}
