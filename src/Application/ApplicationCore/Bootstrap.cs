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
    using CommandBus;
    using MessageBus;
    using ReadSide.Handlers;
    using ReadSide.Repositories;
    using SharedInterfaces.Commands;
    using SharedInterfaces.DependencyInjection;
    using SharedInterfaces.Messaging;
    using SimpleInjector;
    using ViewModel.Repository;

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
            var container = this.SetupDependencyInjection();
            var messageBus = container.Resolve<IMessageBus>();
            this.RegisterEventHandlers(container, messageBus);

            // Init view model repositories
            this.AccountViewModelRepository = container.Resolve<AccountViewModelRepository>();
        }

        /// <summary>
        /// Gets the account view model repository
        /// </summary>
        public AccountViewModelRepository AccountViewModelRepository { get; private set; }
        
        /// <summary>
        /// Setup the dependency injection
        /// </summary>
        /// <returns>Initialised dependency injection container</returns>
        private IContainer SetupDependencyInjection()
        {
            var simpleInjector = new SimpleInjector.Container();

            // Core messaging infrastructure
            simpleInjector.Register<ICommandBus, CommandBus>(Lifestyle.Singleton);
            simpleInjector.Register<IMessageBus, MessageBus>(Lifestyle.Singleton);

            // Read side repositories
            simpleInjector.Register<AccountReadModelRepository, AccountReadModelRepository>(Lifestyle.Singleton);
            simpleInjector.Register<AccountListItemReadModelRepository, AccountListItemReadModelRepository>(Lifestyle.Singleton);
            simpleInjector.Register<AccountListReadModelRepository, AccountListReadModelRepository>(Lifestyle.Singleton);

            // View side repositories
            simpleInjector.Register<AccountViewModelRepository, AccountViewModelRepository>(Lifestyle.Singleton);

            // TODO: account list repository
            simpleInjector.Register<AccountListItemViewModelRepository, AccountListItemViewModelRepository>(Lifestyle.Singleton);

            // Generators
            simpleInjector.Register<AccountGenerator, AccountGenerator>(Lifestyle.Singleton);
            simpleInjector.Register<AccountListGenerator, AccountListGenerator>(Lifestyle.Singleton);

            var container = new SimpleInjectorWrapper(simpleInjector);

            // Must also register container itself because infrastructure needs it
            simpleInjector.RegisterSingleton<IContainer>(container);

            simpleInjector.Verify();
            return container;
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
