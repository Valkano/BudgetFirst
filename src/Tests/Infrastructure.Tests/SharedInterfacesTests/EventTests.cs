namespace BudgetFirst.Infrastructure.Tests.SharedInterfacesTests
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using NUnit.Framework;
    using System.Threading;
    using SharedInterfaces.Messaging;


    [TestFixture]
    public class EventTests
    {
        private TestEvent evt1;
        private TestEvent evt2;
        private TestEvent evt3;
        private TestEvent evt4;
        private TestEvent evt5;
        private TestEvent evt6;

        [SetUp]
        protected void SetUp()
        {
            evt6 = new TestEvent(); // Earliest timestamp, but should be last based on VectorClock
            Thread.Sleep(10);

            VectorClock clock = new VectorClock();
            evt1 = new TestEvent();
            evt1.VectorClock = clock.Increment("key1");

            evt2 = new TestEvent();
            evt2.VectorClock = evt1.VectorClock.Increment("key1");

            evt3 = new TestEvent();
            evt3.VectorClock = evt2.VectorClock.Increment("key1");
            Thread.Sleep(10);//Add sleep so the timestamp is different between evt3 and evt4
            evt4 = new TestEvent();
            evt4.VectorClock = evt2.VectorClock.Increment("key2");

            evt5 = new TestEvent();
            evt5.VectorClock = evt3.VectorClock.Merge(evt4.VectorClock).Increment("key3");

            evt6.VectorClock = evt5.VectorClock.Increment("key1");
        }

        [Test]
        public void CompareEvents()
        {
            Assert.That(evt1.CompareTo(evt2) == -1);
            Assert.That(evt2.CompareTo(evt1) == 1);

            Assert.That(evt3.CompareTo(evt4) == -1);
        }

        [Test]
        public void OrderEvents()
        {
            List<IDomainEvent> eventList = new List<IDomainEvent>();
            eventList.Add(evt5);
            eventList.Add(evt4);
            eventList.Add(evt3);
            eventList.Add(evt2);
            eventList.Add(evt1);

            eventList.Sort();
            Assert.That(eventList[0] == evt1);
            Assert.That(eventList[1] == evt2);
            Assert.That(eventList[2] == evt3);
            Assert.That(eventList[3] == evt4);
            Assert.That(eventList[4] == evt5);
        }

        [Test]
        public void VectorClockTakesPrecedence()
        {
            List<IDomainEvent> eventList = new List<IDomainEvent>();
            eventList.Add(evt6);//Earlier timestamp but later vectorclock
            eventList.Add(evt1);

            eventList.Sort();
            Assert.That(eventList[0] == evt1);
            Assert.That(eventList[1] == evt6);
        }

        [Test]
        public void SimultaneousEventsLastWins()
        {
            Assert.That(evt3.VectorClock.CompareTo(evt4.VectorClock) == VectorComparison.Simultaneous);
            List<IDomainEvent> eventList = new List<IDomainEvent>();
            eventList.Add(evt4);//Earlier timestamp
            eventList.Add(evt3);
            eventList.Sort();

            Assert.That(eventList[0] == evt3);
            Assert.That(eventList[1] == evt4);
        }
    }

    public class TestEvent : DomainEvent
    {

    }
}
