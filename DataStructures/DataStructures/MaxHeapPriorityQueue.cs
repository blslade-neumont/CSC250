﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoDataStructures
{
    public class MaxHeapPriorityQueue
    {
        public void Enqueue(int priority, int value)
        {
            var index = append(new PQNode(priority, value));
            normalizeUp(index);
        }
        public PQNode Peek()
        {
            if (Count == 0) throw new InvalidOperationException("Empty queue");
            return _nodes[0];
        }
        public PQNode Dequeue()
        {
            if (Count == 0) throw new InvalidOperationException("Empty queue");
            var toReturn = _nodes[0];
            _nodes[0] = _nodes[--Count];
            _nodes[Count] = null;
            if (Count > 0) normalizeDown(0);
            return toReturn;
        }

        public int Count { get; private set; } = 0;

        public PQNode[] ToSortedArray()
        {
            var arr = new PQNode[Count];
            for (var q = 0; q < arr.Length; q++)
            {
                arr[q] = Dequeue();
            }
            return arr;
        }

        public override string ToString()
        {
            return string.Join(", ", _nodes.Take(Count).Select(node => $"{node.Priority}:{node.Value}"));
        }

        private PQNode[] _nodes = new PQNode[16];
        private int append(PQNode node)
        {
            if (Count == _nodes.Length)
            {
                var newNodes = new PQNode[_nodes.Length * 2];
                Array.Copy(_nodes, newNodes, _nodes.Length);
                _nodes = newNodes;
            }
            _nodes[Count++] = node;
            return Count - 1;
        }
        private void normalizeUp(int idx)
        {
            while (idx > 0)
            {
                var self = _nodes[idx];

                var parent = ((idx + 1) / 2) - 1;
                if (_nodes[parent].Priority >= self.Priority) return;

                _nodes[idx] = _nodes[parent];
                _nodes[parent] = self;
                idx = parent;
            }
        }
        private void normalizeDown(int idx)
        {
            var self = _nodes[idx];

            var leftChild = (idx + 1) * 2 - 1;
            var rightChild = leftChild + 1;

            if (leftChild >= Count) return;
            if (rightChild >= Count || _nodes[leftChild].Priority > _nodes[rightChild].Priority)
            {
                if (_nodes[leftChild].Priority > self.Priority)
                {
                    _nodes[idx] = _nodes[leftChild];
                    _nodes[leftChild] = self;
                    self = _nodes[idx];
                    normalizeDown(leftChild);
                }
            }
            else
            {
                if (_nodes[rightChild].Priority > self.Priority)
                {
                    _nodes[idx] = _nodes[rightChild];
                    _nodes[rightChild] = self;
                    self = _nodes[idx];
                    normalizeDown(rightChild);
                }
            }
        }
    }

    public class PQNode
    {
        public PQNode(int priority, int value)
        {
            this.Priority = priority;
            this.Value = value;
        }

        public int Priority { get; set; }
        public int Value { get; set; }
    }
}
