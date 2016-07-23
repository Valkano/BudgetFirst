namespace BudgetFirst.Infrastructure.Tests.SharedInterfacesTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using BudgetFirst.SharedInterfaces.EventSourcing;
    using BudgetFirst.SharedInterfaces.Messaging;

    using NUnit.Framework;

    /// <summary>
    /// Contains tests for the event store
    /// </summary>
    [TestFixture]
    public class EventStoreTests
    {
        /// <summary>
        /// Added events are in the store
        /// </summary>
        [Test]
        public void AddedEventsAreInStore()
        {
            var eventStore = new EventStore();
            var @event = new TestEvent();
            @event.AggregateId = new Guid("FFF291EF-1911-4C7C-BB30-89C29F0D6D3E");
            @event.DeviceId = new Guid("6BAAB117-6D9B-404A-AB6A-4AE7A8DA856C");
            @event.VectorClock = new VectorClock();

            eventStore.Add(@event);

            Assert.Contains(@event, eventStore.GetEvents().ToList());
        }

        /// <summary>
        /// Only the added events are in the store, no other events
        /// </summary>
        [Test]
        public void OnlyAddedEventsAreInStore()
        {
            var eventStore = new EventStore();
            var @event = new TestEvent();
            @event.AggregateId = new Guid("FFF291EF-1911-4C7C-BB30-89C29F0D6D3E");
            @event.DeviceId = new Guid("6BAAB117-6D9B-404A-AB6A-4AE7A8DA856C");
            @event.VectorClock = new VectorClock();

            eventStore.Add(@event);

            var events = eventStore.GetEvents().ToList();
            Assert.That(() => events.All(x => ReferenceEquals(x, @event) && events.Count == 1));
        }

        /// <summary>
        /// Adding an invalid (incomplete) event throws an exception
        /// </summary>
        [Test]
        public void AddingInvalidEventThrowsException()
        {
            var eventStore = new EventStore();
            var @event = new TestEvent();
            /* Do not set aggregate id etc. */

            Assert.Throws<DomainEventIncompleteException>(() => eventStore.Add(@event));
        }

        /// <summary>
        /// A new event store is empty
        /// </summary>
        [Test]
        public void NewEventStoreIsEmpty()
        {
            var eventStore = new EventStore();

            Assert.IsEmpty(eventStore.GetEvents());
        }

        /// <summary>
        /// Simple, empty event for tests
        /// </summary>
        private class TestEvent : DomainEvent
        {
        }
    }
}
