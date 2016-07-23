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
    using Budget.Domain.Interfaces.Events;
    using BudgetFirst.Budget.Domain.Commands.Account;
    using BudgetFirst.SharedInterfaces;
    using BudgetFirst.SharedInterfaces.ApplicationState;
    using BudgetFirst.SharedInterfaces.EventSourcing;
    using BudgetFirst.SharedInterfaces.ReadModel;
    using BudgetFirst.Wrappers;

    using ReadSide.Handlers;
    using ReadSide.Repositories;
    using SharedInterfaces.Commands;
    using SharedInterfaces.DependencyInjection;
    using SharedInterfaces.Messaging;

    /// <summary>
    /// Sets up dependency injection, message handler registration etc.
    /// </summary>
    internal class Bootstrap
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="Bootstrap"/> class.
        /// </summary>
        public Bootstrap()
        {
            this.Container = this.SetupDependencyInjection();
            this.RegisterGenerators(this.Container, this.MessageBus);
        }

        /// <summary>
        /// Gets the current device Id
        /// </summary>
        public IDeviceId DeviceId { get; private set; }
        
        /// <summary>
        /// Gets the current (master) vector clock
        /// </summary>
        public MasterVectorClock VectorClock { get; private set; }

        /// <summary>
        /// Gets the Application's MessageBus
        /// </summary>
        public IMessageBus MessageBus { get; private set; }

        /// <summary>
        /// Gets the Application's CommandBus
        /// </summary>
        public ICommandBus CommandBus { get; private set; }

        /// <summary>
        /// Gets the Application's Container
        /// </summary>
        public IContainer Container { get; private set; }

        /// <summary>
        /// Gets the application's event store
        /// </summary>
        public EventStore EventStore { get; private set; }

        /// <summary>
        /// Setup the dependency injection
        /// </summary>
        /// <returns>Initialised dependency injection container</returns>
        private IContainer SetupDependencyInjection()
        {
            var simpleInjector = new Container();

            // Event store only exists once (the state can be exchanged at runtime though)
            this.EventStore = new EventStore();
            simpleInjector.RegisterSingleton<IEventStore>(this.EventStore);
            simpleInjector.RegisterSingleton<IReadOnlyEventStore>(this.EventStore);

            // Device Id
            var deviceId = new DeviceId();
            this.DeviceId = deviceId;
            simpleInjector.RegisterSingleton<IDeviceId>(this.DeviceId);
            simpleInjector.RegisterSingleton<IReadOnlyDeviceId>(this.DeviceId);

            // Vector clock
            this.VectorClock = new MasterVectorClock(deviceId);
            simpleInjector.RegisterSingleton<IVectorClock>(this.VectorClock);

            // Core messaging infrastructure
            this.MessageBus = new MessageBus();
            simpleInjector.RegisterSingleton<IMessageBus>(this.MessageBus);

            // Initialise the command bus last because it depends on many other objects
            this.CommandBus = new CommandBus(
                simpleInjector,
                this.MessageBus,
                this.DeviceId,
                this.VectorClock,
                this.EventStore);
            simpleInjector.RegisterSingleton<ICommandBus>(this.CommandBus);
            
            // Command Handlers
            // Transient
            simpleInjector.Register<ICommandHandler<CreateAccountCommand>, AccountCommandHandler>();
            simpleInjector.Register<ICommandHandler<ChangeAccountNameCommand>, AccountCommandHandler>();

            // Read store (for read side repositories) 
            // same singleton for read and reset
            var readStore = new ReadStore();
            simpleInjector.RegisterSingleton<IReadStore>(readStore);
            simpleInjector.RegisterSingleton<IResetableReadStore>(readStore);

            // Read side repositories. 
            // While these could be stateless and transient, they are used by the singleton generators
            // -> Singleton
            simpleInjector.Register<AccountReadModelRepository, AccountReadModelRepository>(Wrappers.Container.Lifestyle.Singleton);
            simpleInjector.Register<AccountListItemReadModelRepository, AccountListItemReadModelRepository>(Wrappers.Container.Lifestyle.Singleton);
            simpleInjector.Register<AccountListReadModelRepository, AccountListReadModelRepository>(Wrappers.Container.Lifestyle.Singleton);

            // Generators
            // Only one instance per generator -> Singleton
            simpleInjector.Register<AccountGenerator, AccountGenerator>(Wrappers.Container.Lifestyle.Singleton);
            simpleInjector.Register<AccountListGenerator, AccountListGenerator>(Wrappers.Container.Lifestyle.Singleton);

            // Must also register container itself because infrastructure needs it
            simpleInjector.RegisterSingleton<IContainer>(simpleInjector);

            simpleInjector.Verify();
            return simpleInjector;
        }

        /// <summary>
        /// Register generators
        /// </summary>
        /// <param name="container">Dependency injection container</param>
        /// <param name="messageBus">Message bus</param>
        private void RegisterGenerators(IContainer container, IMessageBus messageBus)
        {
            {
                // Account
                var accountGenerator = container.Resolve<AccountGenerator>();
                messageBus.Subscribe<AccountCreated>(accountGenerator.Handle);
                messageBus.Subscribe<AccountNameChanged>(accountGenerator.Handle);
            }

            {
                // Account list
                var accountListGenerator = container.Resolve<AccountListGenerator>();
                messageBus.Subscribe<AccountCreated>(accountListGenerator.Handle);
                messageBus.Subscribe<AccountNameChanged>(accountListGenerator.Handle);
            }
        }
    }
}
