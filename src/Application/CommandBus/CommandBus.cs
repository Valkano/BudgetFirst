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

    public class CommandBus : ICommandBus
    {
        private IContainer dependencyInjectionContainer;
        private IMessageBus messageBus;
        private IEventStore eventStore;

        public CommandBus(IContainer dependencyInjector, IMessageBus messageBus, IEventStore eventStore)
        {
            this.dependencyInjectionContainer = dependencyInjector;
            this.messageBus = messageBus;
            this.eventStore = eventStore;
        }

        public void Submit<TCommand>(TCommand command) where TCommand : ICommand
        {
            try
            {
                var eventTransaction = new EventTransaction();
                InvokeHandler(command, eventTransaction);
                StoreEvents(eventTransaction, this.eventStore);
                PublishEvents(eventTransaction, this.messageBus);                
            }
            catch(Exception)
            {
                throw;
            }            
        }

        private void InvokeHandler<TCommand>(TCommand command, IEventTransaction eventTransaction) where TCommand : ICommand
        {
            var handler = dependencyInjectionContainer.Resolve<ICommandHandler<TCommand>>();
            handler.Handle(command, eventTransaction);
        }

        private void StoreEvents(IEventTransaction eventTransaction, IEventStore eventStore)
        {
            eventStore.Store(eventTransaction.GetEvents());
        }

        private void PublishEvents(IEventTransaction eventTransaction, IMessageBus messageBus)
        {
            var newEvents = eventTransaction.GetEvents();
            foreach (var newEvent in newEvents)
            {
                this.messageBus.Publish(newEvent);
            }
        }
    }
}
