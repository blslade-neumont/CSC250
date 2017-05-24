using System;
using System.Linq;
using System.Collections.Generic;

namespace NetworkArchitect
{
    public class Socket
    {
        private List<(Socket node, int weight)> connections = new List<(Socket node, int weight)>();
        public IEnumerable<(Socket node, int weight)> GetConnections()
        {
            return connections.AsEnumerable();
        }

        public void AddConnection(Socket other, int weight)
        {
            connections.Add((other, weight));
        }
        public bool HasConnection(Socket other)
        {
            return connections.Any(conn => conn.node == other);
        }

        public bool IsLeaf
        {
            get
            {
                return this.connections.Count <= 1;
            }
        }
    }
}
