using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AlgoDataStructures;

namespace TestDataStructures
{
    [TestClass]
    public class TestArrayPriorityQueue
    {
        [TestMethod]
        public void Test_Empty()
        {
            var queue = new ArrayPriorityQueue();

            Assert.AreEqual(0, queue.Count);
            Assert.AreEqual("", queue.ToString());
            Assert.ThrowsException<InvalidOperationException>(() => queue.Peek());
            Assert.ThrowsException<InvalidOperationException>(() => queue.Dequeue());
        }
    }
}
