using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AlgoDataStructures;

namespace TestDataStructures
{
    [TestClass]
    public class TestBinarySearchTree
    {
        [TestMethod]
        public void Test_SimpleAdd()
        {
            var bst = new BinarySearchTree<int>();

            bst.Add(50);
            bst.Add(25);
            bst.Add(75);
            bst.Add(15);
            bst.Add(35);

            Assert.AreEqual("15, 25, 35, 50, 75", bst.InOrder());
        }
    }
}
