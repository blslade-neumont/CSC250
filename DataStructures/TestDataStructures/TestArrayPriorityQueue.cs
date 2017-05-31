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

        [TestMethod]
        public void Test_SimpleAscending()
        {
            var queue = new ArrayPriorityQueue();

            const int test_num = 5;
            for (var q = 0; q < test_num; q++)
                queue.Enqueue(q, q);

            Assert.AreEqual(test_num, queue.Count);

            for (var q = test_num - 1; q >= 0; q--)
                expectNode(queue, q, q);
        }

        [TestMethod]
        public void Test_SimpleDescending()
        {
            var queue = new ArrayPriorityQueue();

            const int test_num = 5;
            for (var q = 0; q < test_num; q++)
                queue.Enqueue(q, test_num - 1 - q);

            Assert.AreEqual(test_num, queue.Count);

            for (var q = test_num - 1; q >= 0; q--)
                expectNode(queue, q, test_num - 1 - q);
        }


        [TestMethod]
        public void Test_Ascending()
        {
            var queue = new ArrayPriorityQueue();

            const int test_num = 500000;
            for (var q = 0; q < test_num; q++)
                queue.Enqueue(q, q);

            Assert.AreEqual(test_num, queue.Count);

            for (var q = test_num - 1; q >= 0; q--)
                expectPriority(queue, q);
        }

        [TestMethod]
        public void Test_Descending()
        {
            var queue = new ArrayPriorityQueue();

            const int test_num = 500000;
            for (var q = 0; q < test_num; q++)
                queue.Enqueue(q, test_num - 1 - q);

            Assert.AreEqual(test_num, queue.Count);

            for (var q = test_num - 1; q >= 0; q--)
                expectPriority(queue, q);
        }
        
        private void expectPriority(ArrayPriorityQueue queue, int priority)
        {
            var result = queue.Dequeue();
            Assert.AreEqual(priority, result.Priority);
        }
        private void expectValue(ArrayPriorityQueue queue, int value)
        {
            var result = queue.Dequeue();
            Assert.AreEqual(value, result.Value);
        }
        private void expectNode(ArrayPriorityQueue queue, int priority, int value)
        {
            var result = queue.Dequeue();
            Assert.AreEqual(priority, result.Priority);
            Assert.AreEqual(value, result.Value);
        }
    }
}
