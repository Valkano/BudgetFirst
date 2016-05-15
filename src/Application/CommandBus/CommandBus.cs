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
namespace BudgetFirst.CommandBus
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using SharedInterfaces;
    using SharedInterfaces.Commands;
    using SharedInterfaces.DependencyInjection;
    using SharedInterfaces.Messaging;

    /// <summary>
    /// The command bus accepts commands and forwards them to the corresponding command handler.
    /// </summary>
    public class CommandBus : ICommandBus
    {
        /// <summary>
        /// Dependency injection container, needed to resolve command handler
        /// </summary>
        private IContainer dependencyInjectionContainer;

        /// <summary>
        /// Message bus, needed to publish events to
        /// </summary>
        /// <remarks>TODO: event publishing should be job of event store?</remarks>
        private IMessageBus messageBus;

        /// <summary>
        /// Event store for event sourcing
        /// </summary>
        private IEventStore eventStore;

        /// <summary>
        /// Initialises a new instance of the <see cref="CommandBus"/> class. 
        /// </summary>
        /// <param name="dependencyInjector">Dependency injection/resolving container</param>
        /// <param name="messageBus">Message bus to publish events to</param>
        /// <param name="eventStore">Event store to save events to</param>
        public CommandBus(IContainer dependencyInjector, IMessageBus messageBus, IEventStore eventStore)
        {
            this.dependencyInjectionContainer = dependencyInjector;
            this.messageBus = messageBus;
            this.eventStore = eventStore;
        }

        /// <summary>
        /// Submit a command to the corresponding handler
        /// </summary>
        /// <typeparam name="TCommand">Command type</typeparam>
        /// <param name="command">Command to be executed</param>
        public void Submit<TCommand>(TCommand command) where TCommand : ICommand
        {
            var eventTransaction = new EventTransaction();
            this.InvokeHandler(command, eventTransaction);
            this.StoreEvents(eventTransaction);
            this.PublishEvents(eventTransaction);
        }

        /// <summary>
        /// Resolve and invoke the corresponding command handler
        /// </summary>
        /// <typeparam name="TCommand">Command type</typeparam>
        /// <param name="command">Command to execute</param>
        /// <param name="eventTransaction">Event transaction to track unpublished events</param>
        private void InvokeHandler<TCommand>(TCommand command, IEventTransaction eventTransaction) where TCommand : ICommand
        {
            var handler = this.dependencyInjectionContainer.Resolve<ICommandHandler<TCommand>>();
            handler.Handle(command, eventTransaction);
        }

        /// <summary>
        /// Add the events from the transaction to the event store
        /// </summary>
        /// <param name="eventTransaction">Event transaction</param>
        private void StoreEvents(IEventTransaction eventTransaction)
        {
            this.eventStore.Add(eventTransaction.GetEvents());
        }

        /// <summary>
        /// Publish the events from the transaction
        /// </summary>
        /// <param name="eventTransaction">Event transaction containing the new events</param>
        private void PublishEvents(IEventTransaction eventTransaction)
        {
            var newEvents = eventTransaction.GetEvents();
            foreach (var newEvent in newEvents)
            {
                this.messageBus.Publish(newEvent);
            }
        }
    }
}
