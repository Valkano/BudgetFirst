namespace BudgetFirst.Infrastructure.Tests.SharedInterfacesTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using NUnit.Framework;
    using SharedInterfaces.Messaging;


    [TestFixture]
    public class VectorClockTests
    {
        [Test]
        public void StartAtOne()
        {
            VectorClock clock = new VectorClock();
            Assert.That(!clock.Vector.ContainsKey("key1"));

            clock = clock.Increment("key1");
            Assert.That(clock.Vector["key1"] == 1);
        }

        [Test]
        public void IncrementClock()
        {
            Dictionary<string, int> vector = new Dictionary<string, int>();
            vector["key1"] = 2;
            vector["key2"] = 3;
            VectorClock clock = new VectorClock(vector);

            clock = clock.Increment("key1");
            clock = clock.Increment("key2");

            Assert.That(clock.Vector["key1"] == 3 && clock.Vector["key2"] == 4);
        }

        [Test]
        public void MergeClock()
        {
            VectorClock clock1 = new VectorClock();
            clock1 = clock1.Increment("key1");
            clock1 = clock1.Increment("key1");
            clock1 = clock1.Increment("key1");

            VectorClock clock2 = new VectorClock();
            clock2 = clock2.Increment("key2");
            clock2 = clock2.Increment("key2");

            VectorClock clock3 = new VectorClock();
            clock3 = clock3.Increment("key3");

            VectorClock MergedClock = new VectorClock();
            MergedClock = MergedClock.Merge(clock1);
            MergedClock = MergedClock.Increment("key1");
            MergedClock = MergedClock.Merge(clock2);
            MergedClock = MergedClock.Merge(clock3);

            Assert.That(MergedClock.Vector["key1"] == 4 && MergedClock.Vector["key2"] == 2 && MergedClock.Vector["key3"] == 1);

            //Merge should not affect the original VectorClock
            Assert.That(clock1.Vector["key1"] == 3);
        }

        [Test]
        public void CopyClock()
        {
            VectorClock clock1 = new VectorClock();
            clock1 = clock1.Increment("key1");
            clock1 = clock1.Increment("key1");
            clock1 = clock1.Increment("key1");

            VectorClock clock2 = clock1.Copy();
            clock2 = clock2.Increment("key1");

            Assert.That(clock1 != clock2);
            Assert.That(clock1.Vector != clock2.Vector);
            Assert.That(clock1.Vector["key1"] == 3);
            Assert.That(clock2.Vector["key1"] == 4);
        }

        [Test]
        public void CompareVectors()
        {
            VectorClock clock1 = new VectorClock();
            clock1 = clock1.Increment("key1");

            VectorClock clock2 = clock1.Copy();
            clock2 = clock2.Increment("key2");

            VectorClock clock3 = new VectorClock();
            clock3 = clock3.Increment("key1");
            clock3 = clock3.Increment("key2");

            Assert.That(clock1.CompareVectors(clock2) == VectorComparison.Smaller);
            Assert.That(clock2.CompareVectors(clock1) == VectorComparison.Greater);
            Assert.That(clock3.CompareVectors(clock2) == VectorComparison.Equal);
        }

        [Test]
        public void DetectSimultaneousOperations()
        {
            VectorClock baseClock = new VectorClock();
            baseClock = baseClock.Increment("key1");
            baseClock = baseClock.Increment("key1");

            VectorClock clock1 = baseClock.Copy();
            VectorClock clock2 = baseClock.Copy();
            VectorClock clock3 = baseClock.Copy();
            clock1 = clock1.Increment("key1");
            clock2 = clock2.Increment("key2");
            clock3 = clock3.Increment("key3");

            Assert.That(clock1.CompareVectors(clock2) == VectorComparison.Simultaneous);
            Assert.That(clock2.CompareVectors(clock1) == VectorComparison.Simultaneous);

            Assert.That(clock1.CompareVectors(clock3) == VectorComparison.Simultaneous);
            Assert.That(clock3.CompareVectors(clock1) == VectorComparison.Simultaneous);

            Assert.That(clock3.CompareVectors(clock2) == VectorComparison.Simultaneous);
            Assert.That(clock2.CompareVectors(clock3) == VectorComparison.Simultaneous);

            VectorClock clock4 = clock1.Merge(clock2);
            clock4 = clock4.Merge(clock3);

            Assert.That(clock4.CompareVectors(clock1) == VectorComparison.Greater);
            Assert.That(clock4.CompareVectors(clock2) == VectorComparison.Greater);
            Assert.That(clock4.CompareVectors(clock3) == VectorComparison.Greater);
            Assert.That(clock1.CompareVectors(clock4) == VectorComparison.Smaller);
            Assert.That(clock2.CompareVectors(clock4) == VectorComparison.Smaller);
            Assert.That(clock3.CompareVectors(clock4) == VectorComparison.Smaller);

            //All previous clocks  descend from baseClock which has the vector ["key1" : 2]
            VectorClock otherClock = new VectorClock();
            otherClock = otherClock.Increment("key4");

            //otherClock should be simultaneous with all of them since it doesnt recognize ["key1" : 2]
            Assert.That(otherClock.CompareVectors(baseClock) == VectorComparison.Simultaneous);
            Assert.That(otherClock.CompareVectors(clock1) == VectorComparison.Simultaneous);
            Assert.That(otherClock.CompareVectors(clock2) == VectorComparison.Simultaneous);
            Assert.That(otherClock.CompareVectors(clock3) == VectorComparison.Simultaneous);
            Assert.That(otherClock.CompareVectors(clock4) == VectorComparison.Simultaneous);
        }
    }
}
