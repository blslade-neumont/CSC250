using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoDataStructures
{
    public class AVLTree<T> : IEnumerable<T>
        where T : IComparable<T>
    {
        public void Add(T value)
        {
            if (root == null) root = new Node(value);
            else
            {
                root.Add(value);
                root = root.Rebalance();
            }
            Count++;
        }
        public void Remove(T value)
        {
            if (root == null) return;
            bool wasRemoved = false;
            root = root.Remove(value, ref wasRemoved);
            if (wasRemoved) Count--;
        }
        public void Clear(bool traverse = false)
        {
            if (traverse && root != null) root.Clear();
            root = null;
            Count = 0;
        }

        public bool Contains(T value)
        {
            return root != null && root.Contains(value);
        }
        public int Count { get; private set; } = 0;
        public int Height()
        {
            return root == null ? 0 : root.Height;
        }
        public string InOrder()
        {
            return enumerableToString(InOrderEnumerable());
        }
        public string PreOrder()
        {
            return enumerableToString(PreOrderEnumerable());
        }
        public string PostOrder()
        {
            return enumerableToString(PostOrderEnumerable());
        }
        private string enumerableToString(IEnumerable<T> vals)
        {
            return string.Join(", ", vals);
        }
        public IEnumerable<T> InOrderEnumerable()
        {
            if (root != null) return root.ToEnumerable(0);
            else return Enumerable.Empty<T>();
        }
        public IEnumerable<T> PreOrderEnumerable()
        {
            if (root != null) return root.ToEnumerable(-1);
            else return Enumerable.Empty<T>();
        }
        public IEnumerable<T> PostOrderEnumerable()
        {
            if (root != null) return root.ToEnumerable(1);
            else return Enumerable.Empty<T>();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return PreOrderEnumerable().GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private Node root;

        private class Node
        {
            public Node(T value)
            {
                this.value = value;
            }

            T value;
            Node left, right;

            public void Add(T value)
            {
                if (value.CompareTo(this.value) < 0)
                {
                    if (left == null) left = new Node(value);
                    else
                    {
                        left.Add(value);
                        left = left.Rebalance();
                    }
                }
                else
                {
                    if (right == null) right = new Node(value);
                    else
                    {
                        right.Add(value);
                        right = right.Rebalance();
                    }
                }
            }
            public Node Remove(T value, ref bool wasRemoved, bool canRemoveSelf = true)
            {
                var compare = value.CompareTo(this.value);
                if (compare == 0 && canRemoveSelf)
                {
                    wasRemoved = true;
                    if (right == null) return left;
                    Node parent = this, child = right;
                    while (child.left != null)
                    {
                        parent = child;
                        child = child.left;
                    }
                    var replaceValue = child.value;
                    parent.Remove(child.value, ref wasRemoved, false);
                    this.value = replaceValue;
                }
                else if (compare < 0) left = left.Remove(value, ref wasRemoved);
                else right = right.Remove(value, ref wasRemoved);
                return Rebalance();
            }
            public Node Rebalance()
            {
                var bf = BalanceFactor;
                if (bf >= 2)
                {
                    if (right.BalanceFactor > 0) return rotateLeft();
                    else return rotateRightLeft();
                }
                else if (bf <= -2)
                {
                    if (left.BalanceFactor > 0) return rotateLeftRight();
                    else return rotateRight();
                }
                else return this;
            }
            private Node rotateLeft()
            {
                var oldRight = right;
                right = oldRight.left;
                oldRight.left = this;
                return oldRight;
            }
            private Node rotateRight()
            {
                var oldLeft = left;
                left = oldLeft.right;
                oldLeft.right = this;
                return oldLeft;
            }
            private Node rotateLeftRight()
            {
                left = left.rotateLeft();
                return rotateRight();
            }
            private Node rotateRightLeft()
            {
                right = right.rotateRight();
                return rotateLeft();
            }
            public void Clear()
            {
                if (left != null) left.Clear();
                if (right != null) right.Clear();
                left = null;
                right = null;
            }

            public bool Contains(T value)
            {
                var compare = value.CompareTo(this.value);
                if (compare == 0) return true;
                else if (compare < 0) return left != null && left.Contains(value);
                else return right != null && right.Contains(value);
            }
            public IEnumerable<T> ToEnumerable(int order)
            {
                if (order < 0) yield return value;
                if (left != null)
                {
                    foreach (T t in left.ToEnumerable(order))
                        yield return t;
                }
                if (order == 0) yield return value;
                if (right != null)
                {
                    foreach (T t in right.ToEnumerable(order))
                        yield return t;
                }
                if (order > 0) yield return value;
            }
            public int Height
            {
                get
                {
                    var leftHeight = (left == null ? 0 : left.Height);
                    var rightHeight = (right == null ? 0 : right.Height);
                    return Math.Max(leftHeight, rightHeight) + 1;
                }
            }
            public int BalanceFactor
            {
                get
                {
                    var leftHeight = (left == null ? 0 : left.Height);
                    var rightHeight = (right == null ? 0 : right.Height);
                    return rightHeight - leftHeight;
                }
            }
        }
    }
}
