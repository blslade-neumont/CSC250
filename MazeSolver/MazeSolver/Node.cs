using System;
using System.Linq;
using System.Collections.Generic;

namespace MazeSolver
{
    public class Node
    {
        private List<(Node node, int weight)> connections = new List<(Node node, int weight)>();
        public IEnumerable<(Node node, int weight)> GetConnections()
        {
            return connections.AsEnumerable();
        }

        public void AddConnection(Node other, int weight = 1)
        {
            connections.Add((other, weight));
        }
    }
}
