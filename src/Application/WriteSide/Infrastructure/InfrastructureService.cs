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

namespace BudgetFirst.WriteSide.Infrastructure
{
    using BudgetFirst.Common.Infrastructure.Commands;
    using BudgetFirst.Common.Infrastructure.Messaging;
    using BudgetFirst.Common.Infrastructure.Persistency;

    using GalaSoft.MvvmLight.Messaging;

    /// <summary>
    /// Infrastructure command service
    /// </summary>
    public class InfrastructureService : ICommandHandler<SaveApplicationState>
    {
        /// <summary>
        /// Application state repository
        /// </summary>
        private IPersistedApplicationStateRepository repository;

        /// <summary>
        /// Factory for current application state
        /// </summary>
        private ICurrentApplicationStateFactory factory;

        /// <summary>
        /// Initialises a new instance of the <see cref="InfrastructureService"/> class.
        /// </summary>
        /// <param name="repository">Application state repository</param>
        /// <param name="factory">Current application state factory</param>
        public InfrastructureService(IPersistedApplicationStateRepository repository, ICurrentApplicationStateFactory factory)
        {
            this.repository = repository;
            this.factory = factory;
        }

        /// <summary>
        /// Handle the save application state command
        /// </summary>
        /// <param name="command">Save application state command</param>
        /// <param name="unitOfWork">Unit of work (is not used in this case)</param>
        public void Handle(SaveApplicationState command, IUnitOfWork unitOfWork)
        {
            this.repository.Save(this.factory.GetCurrentApplicationState(), command.Location);
        }
    }
}