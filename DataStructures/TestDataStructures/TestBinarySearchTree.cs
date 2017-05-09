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
    }
}
