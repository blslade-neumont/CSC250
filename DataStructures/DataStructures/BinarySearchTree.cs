using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoDataStructures
{
    public class BinarySearchTree<T> : IEnumerable<T>
        where T: IComparable<T>
    {
        public void Add(T value)
        {
            if (root == null) root = new BinarySearchNode(value);
            else root.Add(value);
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
            return root == null ? 0 : root.Height(1);
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
            return InOrderEnumerable().GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private BinarySearchNode root;

        private class BinarySearchNode
        {
            public BinarySearchNode(T value)
            {
                this.value = value;
            }

            T value;
            BinarySearchNode left, right;

            public void Add(T value)
            {
                if (value.CompareTo(this.value) < 0)
                {
                    if (left == null) left = new BinarySearchNode(value);
                    else left.Add(value);
                }
                else
                {
                    if (right == null) right = new BinarySearchNode(value);
                    else right.Add(value);
                }
            }
            public BinarySearchNode Remove(T value, ref bool wasRemoved, bool canRemoveSelf = true)
            {
                var compare = value.CompareTo(this.value);
                if (compare == 0 && canRemoveSelf)
                {
                    wasRemoved = true;
                    if (right == null) return left;
                    BinarySearchNode parent = this, child = right;
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
                return this;
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
                var compare = this.value.CompareTo(value);
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
            public int Height(int heightSoFar)
            {
                var h = heightSoFar;
                if (left != null) h = left.Height(heightSoFar + 1);
                if (right != null) h = Math.Max(h, right.Height(heightSoFar + 1));
                return h;
            }
        }
    }
}
