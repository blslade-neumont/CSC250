using System;
using System.Collections.Generic;
using System.Linq;

namespace NetworkArchitect
{
    public class Network
    {
        private Network()
        {
        }

        public static Network FromLines(string[] lines)
        {
            if (lines.Length < 3)
            {
                Console.WriteLine("Failed to parse maze. Not enough lines.");
                return null;
            }

            var names = lines[0].ParseCSV();
            if (names.Length < 2)
            {
                Console.WriteLine("Failed to parse maze. Not enough nodes.");
                return null;
            }
            
            var maze = new Network();
            foreach (var tup in names)
                maze[tup.name] = new Socket();

            foreach (var line in lines.Skip(1))
            {
                var connections = line.ParseCSV();
                if (connections.Length == 0)
                {
                    Console.WriteLine($"Failed to parse maze. Could not understand line: '{line}'");
                    return null;
                }
                var fromSocket = maze[connections[0].name];
                foreach (var to in connections.Skip(1))
                {
                    fromSocket.AddConnection(maze[to.name], to.weight);
                }
            }

            return maze;
        }
        
        private Dictionary<string, Socket> sockets = new Dictionary<string, Socket>();
        public Socket this[string name]
        {
            get
            {
                return sockets[name];
            }
            private set
            {
                sockets[name] = value;
            }
        }
        string GetName(Socket node)
        {
            //This will throw an exception if this node is not in this dictionary
            if (node == null) return "(null)";
            return sockets.First(kvp => kvp.Value == node).Key;
        }

        public void Solve()
        {
            var uncheckedEdges = from kvp in sockets
                                 from (Socket otherSocket, int weight) connectionKvp in kvp.Value.GetConnections()
                                 let otherName = GetName(connectionKvp.otherSocket)
                                 where string.Compare(kvp.Key, otherName) < 0
                                 orderby connectionKvp.weight ascending
                                 select ((kvp.Key, kvp.Value), (otherName, connectionKvp.otherSocket), connectionKvp.weight);

            var addedSockets = new List<Socket>();
            var addedEdges = new List<((string name, Socket socket) from, (string name, Socket socket) to, int weight)>();

            int totalWeight = 0;

            foreach (((string name, Socket socket) from, (string name, Socket socket) to, int weight) edge in uncheckedEdges)
            {
                var canAdd = false;
                if (!addedSockets.Contains(edge.from.socket))
                {
                    canAdd = true;
                    addedSockets.Add(edge.from.socket);
                }
                if (!addedSockets.Contains(edge.to.socket))
                {
                    canAdd = true;
                    addedSockets.Add(edge.to.socket);
                }
                if (canAdd)
                {
                    addedEdges.Add(edge);
                    totalWeight += edge.weight;
                }
            }

            var subnetworks = new List<Network>();
            while (addedSockets.Count > 0)
            {
                var subnetwork = new Network();
                subnetworks.Add(subnetwork);
                var subnetworkWeight = 0;
                var toAdd = new List<Socket>() { addedSockets[0] };
                while (toAdd.Count > 0)
                {
                    var add = toAdd[0];
                    toAdd.Remove(add);
                    addedSockets.Remove(add);
                    var addingName = GetName(add);
                    if (!subnetwork.sockets.ContainsKey(addingName))
                    {
                        subnetwork[addingName] = new Socket();
                    }
                    var selfSocket = subnetwork[addingName];
                    foreach (var conn in addedEdges.Where(edge => edge.from.socket == add || edge.to.socket == add))
                    {
                        var other = conn.from.socket == add ? conn.to : conn.from;
                        var otherName = other.name;
                        if (!subnetwork.sockets.ContainsKey(otherName))
                        {
                            subnetwork[otherName] = new Socket();
                            toAdd.Add(other.socket);
                        }

                        Socket otherSocket = subnetwork[otherName];
                        if (!selfSocket.HasConnection(otherSocket))
                        {
                            selfSocket.AddConnection(otherSocket, conn.weight);
                            otherSocket.AddConnection(selfSocket, conn.weight);
                            subnetworkWeight += conn.weight;
                        }
                    }
                }

                subnetwork.findOptimalHubPlacement();

                Console.WriteLine();
                Console.WriteLine($"MST {subnetworks.Count}:");
                Console.WriteLine($"Socket set: {string.Join(", ", subnetwork.sockets.Keys)}");
                Console.WriteLine($"Cable needed: {subnetworkWeight}ft");
                Console.WriteLine($"Optimal hub placement: {subnetwork.GetName(subnetwork.OptimalHub)} ({subnetwork.OptimalHubMinDistance}, {subnetwork.OptimalHubMaxDistance})");
            }

            Console.WriteLine();
            Console.WriteLine($"Total weight: {totalWeight}ft");
        }

        public Socket OptimalHub { get; private set; }
        public int OptimalHubMinDistance { get; private set; } = int.MinValue;
        public int OptimalHubMaxDistance { get; private set; } = int.MaxValue;
        private void findOptimalHubPlacement()
        {
            var sockets = this.sockets.Values.ToArray();
            int difference = int.MaxValue;
            foreach (var testSocket in sockets.Where(socket => !socket.IsLeaf))
            {
                var fromName = GetName(testSocket);

                int minCost = int.MaxValue;
                int maxCost = int.MinValue;
                foreach (var testTo in sockets.Where(socket => socket != testSocket && socket.IsLeaf))
                {
                    var toName = GetName(testTo);

                    var (nodes, cost) = pathfind(testSocket, testTo);
                    if (nodes == null || nodes.Length < 1) throw new Exception("WTF? This should never happen.");
                    if (cost > maxCost) maxCost = (int)cost;
                    if (cost < minCost) minCost = (int)cost;
                }
                if (maxCost - minCost < difference)
                {
                    difference = maxCost - minCost;
                    OptimalHub = testSocket;
                    OptimalHubMinDistance = minCost;
                    OptimalHubMaxDistance = maxCost;
                }
            }
        }

        private (Socket[] nodes, float cost) pathfind(Socket fromSocket, Socket toSocket)
        {
            var checkedSockets = new List<Socket>();
            var toCheck = new List<Socket>();
            toCheck.Add(fromSocket);

            var cameFrom = new Dictionary<Socket, Socket>();

            var gScores = new Dictionary<Socket, float>();
            gScores[fromSocket] = 0;

            var fScores = new Dictionary<Socket, float>();
            fScores[fromSocket] = 0;// heuristicDistance(fromSocket, toSocket);
            
            while (toCheck.Count != 0)
            {
                float currentFScore = float.PositiveInfinity;
                Socket current = null;
                foreach (var node in toCheck)
                {
                    float fScore;
                    if (!fScores.TryGetValue(node, out fScore)) fScore = float.PositiveInfinity;
                    if (fScore < currentFScore)
                    {
                        currentFScore = fScore;
                        current = node;
                    }
                }

                if (current == toSocket) return (reconstructPath(cameFrom, current), gScores[current]);

                toCheck.Remove(current);
                checkedSockets.Add(current);
                foreach (var (conn, weight) in current.GetConnections())
                {
                    if (checkedSockets.Contains(conn)) continue;

                    var tentativeGScore = gScores[current] + weight;
                    if (!toCheck.Contains(conn)) toCheck.Add(conn);
                    else if (gScores.TryGetValue(conn, out var gScore) && tentativeGScore >= gScore) continue;

                    cameFrom[conn] = current;
                    gScores[conn] = tentativeGScore;
                    fScores[conn] = tentativeGScore;// + heuristicDistance(conn, toSocket);
                }
            }

            return (null, 0);
        }

        Socket[] reconstructPath(Dictionary<Socket, Socket> cameFrom, Socket current)
        {
            var completePath = new List<Socket>() { current };
            while (cameFrom.TryGetValue(current, out current) && current != null) completePath.Insert(0, current);
            return completePath.ToArray();
        }
    }
}
