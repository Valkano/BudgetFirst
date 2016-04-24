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

            // View side repositories
            simpleInjector.Register<AccountViewModelRepository, AccountViewModelRepository>(Lifestyle.Singleton);

            // Generators
            simpleInjector.Register<AccountGenerator, AccountGenerator>(Lifestyle.Singleton);

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
            // All generators are treated as singletons
            var accountGenerator = container.Resolve<AccountGenerator>();
            messageBus.Subscribe<AccountCreated>(accountGenerator.Handle);
            messageBus.Subscribe<AccountNameChanged>(accountGenerator.Handle);
        }
    }
}
