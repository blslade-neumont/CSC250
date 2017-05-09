using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AlgoDataStructures;

namespace TestDataStructures
{
    [TestClass]
    public class TestBinarySearchTree
    {
        [TestMethod]
        public void Test_Empty()
        {
            var bst = new BinarySearchTree<int>();

            Assert.AreEqual(0, bst.Count);
            Assert.AreEqual(0, bst.Height());
            Assert.AreEqual("", bst.InOrder());
            Assert.AreEqual("", bst.PreOrder());
            Assert.AreEqual("", bst.PostOrder());
        }

        [TestMethod]
        public void Test_SimpleAdd()
        {
            var bst = new BinarySearchTree<int>();

            bst.Add(50);
            bst.Add(25);
            bst.Add(75);
            bst.Add(15);
            bst.Add(35);

            //     50
            //  25    75
            //15  35

            Assert.AreEqual("15, 25, 35, 50, 75", bst.InOrder());
            Assert.AreEqual("50, 25, 15, 35, 75", bst.PreOrder());
            Assert.AreEqual("15, 35, 25, 75, 50", bst.PostOrder());
        }

        [TestMethod]
        public void Test_SimpleRemove()
        {
            var bst = new BinarySearchTree<int>();

            bst.Add(50);
            bst.Add(25);
            bst.Add(75);
            bst.Add(15);
            bst.Add(35);
            bst.Remove(50);

            //     75
            //  25
            //15  35

            Assert.AreEqual("15, 25, 35, 75", bst.InOrder());
            Assert.AreEqual("75, 25, 15, 35", bst.PreOrder());
            Assert.AreEqual("15, 35, 25, 75", bst.PostOrder());
        }

        [TestMethod]
        public void Test_Count()
        {
            var bst = new BinarySearchTree<int>();
            Assert.AreEqual(0, bst.Count);

            bst.Add(50);
            bst.Add(25);
            bst.Add(75);
            Assert.AreEqual(3, bst.Count);

            bst.Add(75);
            Assert.AreEqual(4, bst.Count);

            bst.Add(15);
            bst.Add(35);
            Assert.AreEqual(6, bst.Count);

            bst.Remove(50);
            Assert.AreEqual(5, bst.Count);

            //     75
            //  25    75
            //15  35

            Assert.AreEqual("15, 25, 35, 75, 75", bst.InOrder());
            Assert.AreEqual("75, 25, 15, 35, 75", bst.PreOrder());
            Assert.AreEqual("15, 35, 25, 75, 75", bst.PostOrder());
        }

        [TestMethod]
        public void Test_Height()
        {
            var bst = new BinarySearchTree<int>();
            Assert.AreEqual(0, bst.Height());

            bst.Add(50);
            Assert.AreEqual(1, bst.Height());

            bst.Add(25);
            Assert.AreEqual(2, bst.Height());

            bst.Add(75);
            Assert.AreEqual(2, bst.Height());

            bst.Add(75);
            Assert.AreEqual(3, bst.Height());

            bst.Add(15);
            Assert.AreEqual(3, bst.Height());

            bst.Remove(25);
            Assert.AreEqual(3, bst.Height());

            //     50
            //  15    75
            //          75
        }
    }
}
