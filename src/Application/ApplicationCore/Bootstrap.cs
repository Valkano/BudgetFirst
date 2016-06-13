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
    using BudgetFirst.Wrappers;
    using Messaging;
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
            Container = this.SetupDependencyInjection();
            this.EventStore = this.Container.Resolve<IEventStore>();
            this.MessageBus = this.Container.Resolve<IMessageBus>();
            this.CommandBus = this.Container.Resolve<ICommandBus>();
            this.RegisterEventHandlers(this.Container, this.MessageBus);

            // Init view model repositories
            this.AccountReadModelRepository = this.Container.Resolve<AccountReadModelRepository>();
            this.AccountListReadModelRepository = this.Container.Resolve<AccountListReadModelRepository>();
        }

        /// <summary>
        /// Gets the account view model repository
        /// </summary>
        public AccountReadModelRepository AccountReadModelRepository { get; private set; }

        /// <summary>
        /// Gets the account list repository
        /// </summary>
        public AccountListReadModelRepository AccountListReadModelRepository { get; private set; }

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
        /// Gets the Application's EventStore
        /// </summary>
        public IEventStore EventStore { get; private set; }
        
        /// <summary>
        /// Setup the dependency injection
        /// </summary>
        /// <returns>Initialised dependency injection container</returns>
        private IContainer SetupDependencyInjection()
        {
            var simpleInjector = new Container();

            // Core messaging infrastructure
            simpleInjector.Register<IEventStore, EventStore>(Wrappers.Container.Lifestyle.Singleton);
            simpleInjector.Register<ICommandBus, CommandBus>(Wrappers.Container.Lifestyle.Singleton);
            simpleInjector.Register<IMessageBus, MessageBus>(Wrappers.Container.Lifestyle.Singleton);

            // Command Handlers
            simpleInjector.Register<ICommandHandler<CreateAccountCommand>, AccountCommandHandler>();
            simpleInjector.Register<ICommandHandler<ChangeAccountNameCommand>, AccountCommandHandler>();

            // Read side repositories
            simpleInjector.Register<AccountReadModelRepository, AccountReadModelRepository>(Wrappers.Container.Lifestyle.Singleton);
            simpleInjector.Register<AccountListItemReadModelRepository, AccountListItemReadModelRepository>(Wrappers.Container.Lifestyle.Singleton);
            simpleInjector.Register<AccountListReadModelRepository, AccountListReadModelRepository>(Wrappers.Container.Lifestyle.Singleton);

            // Generators
            simpleInjector.Register<AccountGenerator, AccountGenerator>(Wrappers.Container.Lifestyle.Singleton);
            simpleInjector.Register<AccountListGenerator, AccountListGenerator>(Wrappers.Container.Lifestyle.Singleton);

            // Must also register container itself because infrastructure needs it
            simpleInjector.RegisterSingleton<IContainer>(simpleInjector);

            simpleInjector.Verify();
            return simpleInjector;
        }

        /// <summary>
        /// Register message bus event handlers (subscribers)
        /// </summary>
        /// <param name="container">Dependency injection container</param>
        /// <param name="messageBus">Message bus</param>
        private void RegisterEventHandlers(IContainer container, IMessageBus messageBus)
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
