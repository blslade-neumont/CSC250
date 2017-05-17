using System;
using System.Collections.Generic;
using System.Linq;

namespace MazeSolver
{
    public class Maze
    {
        private Maze()
        {
        }

        public static Maze FromLines(string[] lines)
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

            var solverOptions = lines[1].ParseCSV();
            if (solverOptions.Length != 2)
            {
                Console.WriteLine("Failed to parse maze. Second line should contain 'startName, endName'");
                return null;
            }
            var (startName, endName) = solverOptions;

            var maze = new Maze();
            foreach (var name in names)
                maze[name] = new Node();
            maze.StartNode = maze[startName];
            maze.EndNode = maze[endName];

            foreach (var line in lines.Skip(2))
            {
                var connectionNames = line.ParseCSV();
                if (connectionNames.Length == 0)
                {
                    Console.WriteLine($"Failed to parse maze. Could not understand line: '{line}'");
                    return null;
                }
                var fromNode = maze[connectionNames[0]];
                foreach (var toName in connectionNames.Skip(1))
                {
                    fromNode.AddConnection(maze[toName]);
                }
            }

            return maze;
        }

        public Node StartNode { get; private set; }
        public Node EndNode { get; private set; }

        private Dictionary<string, Node> nodes = new Dictionary<string, Node>();
        public Node this[string name]
        {
            get
            {
                return nodes[name];
            }
            private set
            {
                nodes[name] = value;
            }
        }
        string GetName(Node node)
        {
            //This will throw an exception if this node is not in this dictionary
            return nodes.First(kvp => kvp.Value == node).Key;
        }

        public void Solve()
        {
            var (nodes, cost) = pathfind(StartNode, EndNode);
            if (nodes == null || nodes.Length == 0) Console.WriteLine("Maze cannot be solved.");
            else Console.WriteLine($"Solution: {string.Join("", nodes.Select(node => GetName(node)))}");
        }
        
        private (Node[] nodes, float cost) pathfind(Node fromNode, Node toNode)
        {
            var checkedNodes = new List<Node>();
            var toCheck = new List<Node>();
            toCheck.Add(fromNode);

            var cameFrom = new Dictionary<Node, Node>();

            var gScores = new Dictionary<Node, float>();
            gScores[fromNode] = 0;

            var fScores = new Dictionary<Node, float>();
            fScores[fromNode] = 0;// heuristicDistance(fromNode, toNode);
            
            while (toCheck.Count != 0)
            {
                float currentFScore = float.PositiveInfinity;
                Node current = null;
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

                if (current == toNode) return (reconstructPath(cameFrom, current), gScores[current]);

                toCheck.Remove(current);
                checkedNodes.Add(current);
                foreach (var (conn, weight) in current.GetConnections())
                {
                    if (checkedNodes.Contains(conn)) continue;

                    var tentativeGScore = gScores[current] + weight;
                    if (!toCheck.Contains(conn)) toCheck.Add(conn);
                    else if (gScores.TryGetValue(conn, out var gScore) && tentativeGScore >= gScore) continue;

                    cameFrom[conn] = current;
                    gScores[conn] = tentativeGScore;
                    fScores[conn] = tentativeGScore;// + heuristicDistance(conn, toNode);
                }
            }

            return (null, 0);
        }

        Node[] reconstructPath(Dictionary<Node, Node> cameFrom, Node current)
        {
            var completePath = new List<Node>() { current };
            while (cameFrom.TryGetValue(current, out current) && current != null) completePath.Insert(0, current);
            return completePath.ToArray();
        }
    }
}
