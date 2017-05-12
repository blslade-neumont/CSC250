using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AlgoDataStructures;

namespace TestDataStructures
{
    [TestClass]
    public class TestAVLTree
    {
        [TestMethod]
        public void Test_Empty()
        {
            var avl = new AVLTree<int>();

            Assert.AreEqual(0, avl.Count);
            Assert.AreEqual(0, avl.Height());
            Assert.AreEqual("", avl.InOrder());
            Assert.AreEqual("", avl.PreOrder());
            Assert.AreEqual("", avl.PostOrder());
        }

        [TestMethod]
        public void Test_EnumerablePreOrder()
        {
            var avl = new AVLTree<int>();

            avl.Add(25);
            avl.Add(15);
            avl.Add(50);

            //  25
            //15  50

            Assert.AreEqual("25, 15, 50", string.Join(", ", avl.ToArray()));
        }

        [TestMethod]
        public void Test_RotateRight()
        {
            var avl = new AVLTree<int>();

            avl.Add(50);
            avl.Add(25);
            avl.Add(15);

            //  25
            //15  50
            
            Assert.AreEqual("25, 15, 50", avl.PreOrder());
        }

        [TestMethod]
        public void Test_RotateLeft()
        {
            var avl = new AVLTree<int>();

            avl.Add(15);
            avl.Add(25);
            avl.Add(50);

            //  25
            //15  50
            
            Assert.AreEqual("25, 15, 50", avl.PreOrder());
        }

        [TestMethod]
        public void Test_RotateLeftRight()
        {
            var avl = new AVLTree<int>();

            avl.Add(50);
            avl.Add(15);
            avl.Add(25);

            //  25
            //15  50

            Assert.AreEqual("25, 15, 50", avl.PreOrder());
        }

        [TestMethod]
        public void Test_RotateRightLeft()
        {
            var avl = new AVLTree<int>();

            avl.Add(15);
            avl.Add(50);
            avl.Add(25);

            //  25
            //15  50

            Assert.AreEqual("25, 15, 50", avl.PreOrder());
        }

        [TestMethod]
        public void Test_ComplexBalance()
        {
            var avl = new AVLTree<int>();

            avl.Add(5);
            avl.Add(10);
            avl.Add(7);
            avl.Add(2);
            avl.Add(4);
            avl.Add(11);
            avl.Add(12);
            avl.Add(15);
            avl.Add(13);
            avl.Add(14);

            //        7
            //  4          13
            //2   5    11      15
            //       10  12  14

            Assert.AreEqual("7, 4, 2, 5, 13, 11, 10, 12, 15, 14", avl.PreOrder());
        }

        [TestMethod]
        public void Test_RemoveBalance()
        {
            var avl = new AVLTree<int>();

            avl.Add(5);
            avl.Add(10);
            avl.Add(7);
            avl.Add(2);
            avl.Add(4);
            avl.Add(11);
            avl.Add(12);
            avl.Add(15);
            avl.Add(13);
            avl.Add(14);

            //        7
            //  4          13
            //2   5    11      15
            //       10  12  14

            avl.Remove(13);
            avl.Remove(14);

            //       7
            //  4        11
            //2   5   10    15
            //            12

            Assert.AreEqual("7, 4, 2, 5, 11, 10, 15, 12", avl.PreOrder());
        }

        [TestMethod]
        public void Test_SimpleAdd()
        {
            var avl = new AVLTree<int>();

            avl.Add(50);
            avl.Add(25);
            avl.Add(75);
            avl.Add(15);
            avl.Add(35);

            //     50
            //  25    75
            //15  35

            Assert.AreEqual("15, 25, 35, 50, 75", avl.InOrder());
            Assert.AreEqual("50, 25, 15, 35, 75", avl.PreOrder());
            Assert.AreEqual("15, 35, 25, 75, 50", avl.PostOrder());
        }

        [TestMethod]
        public void Test_SimpleRemove()
        {
            var avl = new AVLTree<int>();

            avl.Add(50);
            avl.Add(25);
            avl.Add(75);
            avl.Add(15);
            avl.Add(35);
            avl.Remove(50);

            //     75
            //  25
            //15  35

            Assert.AreEqual("15, 25, 35, 75", avl.InOrder());
            Assert.AreEqual("75, 25, 15, 35", avl.PreOrder());
            Assert.AreEqual("15, 35, 25, 75", avl.PostOrder());
        }

        [TestMethod]
        public void Test_Count()
        {
            var avl = new AVLTree<int>();
            Assert.AreEqual(0, avl.Count);

            avl.Add(50);
            avl.Add(25);
            avl.Add(75);
            Assert.AreEqual(3, avl.Count);

            avl.Add(75);
            Assert.AreEqual(4, avl.Count);

            avl.Add(15);
            avl.Add(35);
            Assert.AreEqual(6, avl.Count);

            avl.Remove(50);
            Assert.AreEqual(5, avl.Count);

            //     75
            //  25    75
            //15  35

            Assert.AreEqual("15, 25, 35, 75, 75", avl.InOrder());
            Assert.AreEqual("75, 25, 15, 35, 75", avl.PreOrder());
            Assert.AreEqual("15, 35, 25, 75, 75", avl.PostOrder());
        }

        [TestMethod]
        public void Test_Height()
        {
            var avl = new AVLTree<int>();
            Assert.AreEqual(0, avl.Height());

            avl.Add(50);
            Assert.AreEqual(1, avl.Height());

            avl.Add(25);
            Assert.AreEqual(2, avl.Height());

            avl.Add(75);
            Assert.AreEqual(2, avl.Height());

            avl.Add(75);
            Assert.AreEqual(3, avl.Height());

            avl.Add(15);
            Assert.AreEqual(3, avl.Height());

            avl.Remove(25);
            Assert.AreEqual(3, avl.Height());

            //     50
            //  15    75
            //          75
        }

        [TestMethod]
        public void Test_Clear()
        {
            var avl = new AVLTree<int>();

            avl.Add(50);
            avl.Add(25);
            avl.Add(75);
            avl.Add(15);
            avl.Add(35);
            Assert.AreEqual(5, avl.Count);
            Assert.AreEqual(3, avl.Height());

            avl.Clear();
            Assert.AreEqual(0, avl.Count);
            Assert.AreEqual(0, avl.Height());

            Assert.AreEqual("", avl.InOrder());
            Assert.AreEqual("", avl.PreOrder());
            Assert.AreEqual("", avl.PostOrder());
        }

        [TestMethod]
        public void Test_Contains()
        {
            var avl = new AVLTree<int>();

            Assert.IsFalse(avl.Contains(50));

            avl.Add(50);
            Assert.IsTrue(avl.Contains(50));

            avl.Add(50);
            Assert.IsTrue(avl.Contains(50));

            avl.Remove(50);
            Assert.IsTrue(avl.Contains(50));

            avl.Remove(50);
            Assert.IsFalse(avl.Contains(50));

            Assert.AreEqual("", avl.InOrder());
            Assert.AreEqual("", avl.PreOrder());
            Assert.AreEqual("", avl.PostOrder());
        }

        [TestMethod]
        public void ContainsMaxMin()
        {
            var avl = new AVLTree<int>();

            avl.Add(24);
            avl.Add(10);
            avl.Add(1337);
            avl.Add(8);
            avl.Add(12);
            avl.Add(100);
            avl.Add(1400);
            avl.Add(7);
            avl.Add(9);
            avl.Add(11);
            avl.Add(13);
            avl.Add(90);
            avl.Add(110);
            avl.Add(1350);
            avl.Add(1500);

            //           24
            //     10          1337
            //   8   12    100      1400
            //  7 9 11 13 90 110 1350  1500

            Assert.IsTrue(avl.Contains(7));
            Assert.IsTrue(avl.Contains(1500));
        }
    }
}
