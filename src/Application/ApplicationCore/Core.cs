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
namespace BudgetFirst.ApplicationCore
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using BudgetFirst.SharedInterfaces.Commands;
    using BudgetFirst.SharedInterfaces.EventSourcing;
    using BudgetFirst.SharedInterfaces.Messaging;
    using BudgetFirst.SharedInterfaces.ReadModel;

    /// <summary>
    /// Represents the Application's core functionality as a Singleton.
    /// </summary>
    public class Core
    {
        /// <summary>
        /// The Core's bootstrap.
        /// </summary>
        private readonly Bootstrap bootstrap;

        /// <summary>
        /// Initialises a new instance of the <see cref="Core"/> class.
        /// Prevents a default instance of the <see cref="Core"/> class from being created.
        /// </summary>
        internal Core()
        {
            this.bootstrap = new Bootstrap();

            // TODO: initialisation of state; restore read models from event store
            // TODO: event store state
            // this.bootstrap.EventStore.SetState();
            this.bootstrap.VectorClock.SetState(new VectorClock());
            this.bootstrap.DeviceId.SetDeviceId(new Guid("A621850A-5B4B-479F-9071-1F3588C144E6"));

            this.Repositories = new Repositories(this.bootstrap);
            this.CommandBus = this.bootstrap.CommandBus;
        }
        
        /// <summary>
        /// Gets the Application's Repositories.
        /// </summary>
        public Repositories Repositories { get; private set; }

        /// <summary>
        /// Gets the  Application's CommandBus
        /// </summary>
        public ICommandBus CommandBus { get; private set; }

        /// <summary>
        /// Reset the current state and replay all events
        /// </summary>
        internal void ResetState()
        {
            // TODO: broadcast reset after it is done
            this.bootstrap.Container.Resolve<IResetableReadStore>().Clear();
            foreach (var @event in this.bootstrap.EventStore.GetEvents())
            {
                this.bootstrap.MessageBus.Publish(@event);
            }
        }
    }
}
