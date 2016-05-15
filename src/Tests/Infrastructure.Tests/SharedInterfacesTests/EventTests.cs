namespace BudgetFirst.Infrastructure.Tests.SharedInterfacesTests
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading;
    using NUnit.Framework;
    using SharedInterfaces.Messaging;

    /// <summary>
    /// Test fixture for events and vector clocks
    /// </summary>
    [TestFixture]
    public class EventTests
    {
        /// <summary>
        /// Device 1, first event
        /// </summary>
        private TestEvent evt1;

        /// <summary>
        /// Device 1, second event
        /// </summary>
        private TestEvent evt2;

        /// <summary>
        /// Device 1, third event
        /// </summary>
        private TestEvent evt3;

        /// <summary>
        /// Device 2, first event (fourth total)
        /// </summary>
        private TestEvent evt4;

        /// <summary>
        /// Device 3, vector clock merged from fourth event, first event on device 3
        /// </summary>
        private TestEvent evt5;

        /// <summary>
        /// Device 1, last event.
        /// Event with a weird timestamp, but last according to vector clock
        /// </summary>
        private TestEvent evt6;

        /// <summary>
        /// Setup for each test
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            this.evt6 = new TestEvent(); // Earliest timestamp, but should be last based on VectorClock
            Thread.Sleep(10);

            VectorClock clock = new VectorClock();
            this.evt1 = new TestEvent();
            this.evt1.VectorClock = clock.Increment("key1");

            this.evt2 = new TestEvent();
            this.evt2.VectorClock = this.evt1.VectorClock.Increment("key1");

            this.evt3 = new TestEvent();
            this.evt3.VectorClock = this.evt2.VectorClock.Increment("key1");
            Thread.Sleep(10); // Add sleep so the timestamp is different between evt3 and evt4
            this.evt4 = new TestEvent();
            this.evt4.VectorClock = this.evt2.VectorClock.Increment("key2");

            this.evt5 = new TestEvent();
            this.evt5.VectorClock = this.evt3.VectorClock.Merge(this.evt4.VectorClock).Increment("key3");

            this.evt6.VectorClock = this.evt5.VectorClock.Increment("key1");
        }

        /// <summary>
        /// Tests that the comparison of two events is correct
        /// </summary>
        [Test]
        public void CompareEvents()
        {
            Assert.That(this.evt1.CompareTo(this.evt2) == -1);
            Assert.That(this.evt2.CompareTo(this.evt1) == 1);

            Assert.That(this.evt3.CompareTo(this.evt4) == -1);
        }

        /// <summary>
        /// Tests sorting of a list of events
        /// </summary>
        [Test]
        public void OrderEvents()
        {
            List<IDomainEvent> eventList = new List<IDomainEvent>();
            eventList.Add(this.evt5);
            eventList.Add(this.evt4);
            eventList.Add(this.evt3);
            eventList.Add(this.evt2);
            eventList.Add(this.evt1);

            eventList.Sort();
            Assert.That(eventList[0] == this.evt1);
            Assert.That(eventList[1] == this.evt2);
            Assert.That(eventList[2] == this.evt3);
            Assert.That(eventList[3] == this.evt4);
            Assert.That(eventList[4] == this.evt5);
        }

        /// <summary>
        /// Tests that vector clock is used when possible
        /// </summary>
        [Test]
        public void VectorClockTakesPrecedence()
        {
            List<IDomainEvent> eventList = new List<IDomainEvent>();
            eventList.Add(this.evt6); // Earlier timestamp but later vectorclock
            eventList.Add(this.evt1);

            eventList.Sort();
            Assert.That(eventList[0] == this.evt1);
            Assert.That(eventList[1] == this.evt6);
        }

        /// <summary>
        /// Tests that the timestamp is used as a fallback
        /// </summary>
        [Test]
        public void SimultaneousEventsLastWins()
        {
            Assert.That(this.evt3.VectorClock.CompareVectors(this.evt4.VectorClock) == VectorClock.ComparisonResult.Simultaneous);
            List<IDomainEvent> eventList = new List<IDomainEvent>();
            eventList.Add(this.evt4); // Earlier timestamp
            eventList.Add(this.evt3);
            eventList.Sort();

            Assert.That(eventList[0] == this.evt3);
            Assert.That(eventList[1] == this.evt4);
        }

        /// <summary>
        /// Test event
        /// </summary>
        private class TestEvent : DomainEvent
        {
        }
    }
}
