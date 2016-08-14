// BudgetFirst 
// ©2016 Thomas Mühlgrabner
// This source code is dual-licensed under:
//   * Mozilla Public License 2.0 (MPL 2.0) 
//   * GNU General Public License v3.0 (GPLv3)
// ==================== Mozilla Public License 2.0 ===================
// This Source Code Form is subject to the terms of the Mozilla Public 
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.
// ================= GNU General Public License v3.0 =================
// This file is part of BudgetFirst.
// BudgetFirst is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// BudgetFirst is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
// GNU General Public License for more details.
// You should have received a copy of the GNU General Public License
// along with Budget First.  If not, see<http://www.gnu.org/licenses/>.
// ===================================================================
namespace BudgetFirst.Application
{
    using System;
    using System.Collections.Generic;

    using BudgetFirst.Accounting.Application.Commands;
    using BudgetFirst.Accounting.Application.Projections;
    using BudgetFirst.Accounting.Application.Projections.Repositories;
    using BudgetFirst.Accounting.Application.Services;
    using BudgetFirst.Accounting.Domain.Events.Events;
    using BudgetFirst.Application.Commands.Infrastructure;
    using BudgetFirst.Application.Services;
    using BudgetFirst.Common.Infrastructure.ApplicationState;
    using BudgetFirst.Common.Infrastructure.Commands;
    using BudgetFirst.Common.Infrastructure.DependencyInjection;
    using BudgetFirst.Common.Infrastructure.EventSourcing;
    using BudgetFirst.Common.Infrastructure.Messaging;
    using BudgetFirst.Common.Infrastructure.Persistency;
    using BudgetFirst.Common.Infrastructure.Projections.Models;
    using BudgetFirst.Common.Infrastructure.Serialisation;
    using BudgetFirst.Common.Infrastructure.Wrappers;
    using BudgetFirst.Currencies.Application.Projections.Repositories;

    /// <summary>
    /// Sets up dependency injection, message handler registration etc.
    /// </summary>
    internal class Bootstrap
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="Bootstrap"/> class.
        /// </summary>
        /// <param name="persistedApplicationStateRepository">Application state repository for access to the persisted application state</param>
        /// <param name="applicationStateFactory">Factory for the current application state</param>
        public Bootstrap(
            IPersistedApplicationStateRepository persistedApplicationStateRepository, 
            ICurrentApplicationStateFactory applicationStateFactory)
        {
            this.Container = this.SetupDependencyInjection(persistedApplicationStateRepository, applicationStateFactory);
            this.RegisterProjections(this.Container, this.MessageBus);
            RegisterKnownTypesForSerialisation();
        }

        /// <summary>
        /// Gets the Application's CommandBus
        /// </summary>
        public ICommandBus CommandBus { get; private set; }

        /// <summary>
        /// Gets the Application's Container
        /// </summary>
        public IContainer Container { get; private set; }

        /// <summary>
        /// Gets the current device Id
        /// </summary>
        public IDeviceId DeviceId { get; private set; }

        /// <summary>
        /// Gets the application's event store
        /// </summary>
        public EventStore EventStore { get; private set; }

        /// <summary>
        /// Gets the Application's MessageBus
        /// </summary>
        public IMessageBus MessageBus { get; private set; }

        /// <summary>
        /// Gets the current (master) vector clock
        /// </summary>
        public MasterVectorClock VectorClock { get; private set; }

        /// <summary>
        /// Register known types for serialisation
        /// </summary>
        private static void RegisterKnownTypesForSerialisation()
        {
            // Serialisation needs to know about the available types. However, we cannot use reflection here
            var allKnownTypes = new List<Type>();
            allKnownTypes.AddRange(BudgetFirst.Accounting.Domain.Events.KnownTypesRegistry.EventTypes);
            Serialiser.KnownTypes = allKnownTypes.ToArray();
        }

        /// <summary>
        /// Register projections
        /// </summary>
        /// <param name="container">Dependency injection container</param>
        /// <param name="messageBus">Message bus</param>
        private void RegisterProjections(IContainer container, IMessageBus messageBus)
        {
            // we'd love to solve this via reflection or some automatic means but we don't have that available in PCL projects
            {
                // Account
                var accountProjection = container.Resolve<AccountProjection>();
                messageBus.Subscribe<AccountCreated>(accountProjection.Handle);
                messageBus.Subscribe<AccountNameChanged>(accountProjection.Handle);
            }

            {
                // Account list
                var accountListProjection = container.Resolve<AccountListProjection>();
                messageBus.Subscribe<AccountCreated>(accountListProjection.Handle);
                messageBus.Subscribe<AccountNameChanged>(accountListProjection.Handle);
            }
        }

        /// <summary>
        /// Setup the dependency injection
        /// </summary>
        /// <returns>Initialised dependency injection container</returns>
        /// <param name="persistedApplicationStateRepository">Application state repository for access to the persisted application state</param>
        /// <param name="applicationStateFactory">Factory for the current application state</param>
        private IContainer SetupDependencyInjection(
            IPersistedApplicationStateRepository persistedApplicationStateRepository, 
            ICurrentApplicationStateFactory applicationStateFactory)
        {
            var simpleInjector = new Container();

            // Application state repository is required when we want to save current the application state
            simpleInjector.RegisterSingleton<IPersistedApplicationStateRepository>(persistedApplicationStateRepository);
            simpleInjector.RegisterSingleton<ICurrentApplicationStateFactory>(applicationStateFactory);

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
            simpleInjector.Register<IHandleCommand<CreateAccountCommand>, AccountService>();
            simpleInjector.Register<IHandleCommand<ChangeAccountNameCommand>, AccountService>();
            simpleInjector.Register<IHandleCommand<SaveApplicationState>, ApplicationStateService>();
            simpleInjector.Register<IHandleCommand<LoadApplicationState>, ApplicationStateService>();

            // Read store (for read side repositories) 
            // same singleton for read and reset
            var readStore = new ReadStore();
            simpleInjector.RegisterSingleton<IReadStore>(readStore);
            simpleInjector.RegisterSingleton<IResetableReadStore>(readStore);

            // Read side repositories. 
            // While these could be stateless and transient, they are used by the singleton generators
            // -> Singleton
            simpleInjector.Register<AccountReadModelRepository>(Common.Infrastructure.Wrappers.Container.Lifestyle.Singleton);
            simpleInjector.Register<AccountListItemReadModelRepository>(Common.Infrastructure.Wrappers.Container.Lifestyle.Singleton);
            simpleInjector.Register<AccountListReadModelRepository>(Common.Infrastructure.Wrappers.Container.Lifestyle.Singleton);
            simpleInjector.Register<CurrencyRepository>(Common.Infrastructure.Wrappers.Container.Lifestyle.Singleton);

            // Generators
            // Only one instance per generator -> Singleton
            simpleInjector.Register<AccountProjection, AccountProjection>(Common.Infrastructure.Wrappers.Container.Lifestyle.Singleton);
            simpleInjector.Register<AccountListProjection, AccountListProjection>(Common.Infrastructure.Wrappers.Container.Lifestyle.Singleton);

            // Must also register container itself because infrastructure needs it
            simpleInjector.RegisterSingleton<IContainer>(simpleInjector);

            simpleInjector.Verify();
            return simpleInjector;
        }
    }
}