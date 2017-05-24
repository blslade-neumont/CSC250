using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NetworkArchitect
{
    public static class NetworkSolver
    {
        public static void SolveAllFromFile(string path)
        {
            using (var file = File.OpenRead(path))
                SolveAllFromStream(file);
        }
        public static void SolveAllFromStream(Stream stream)
        {
            var networks = new List<string[]>();
            using (var reader = new StreamReader(stream))
            {
                var lines = (reader.ReadToEnd() + '\n')
                    .Split('\n')
                    .Where(line => !line.StartsWith("//"))
                    .Select(line => line.Trim(' ', '\t', '\r', '\n'));
                var networkLines = new List<string>();
                foreach (var line in lines)
                {
                    if (string.IsNullOrEmpty(line))
                    {
                        if (networkLines.Count > 0)
                        {
                            networks.Add(networkLines.ToArray());
                            networkLines.Clear();
                        }
                    }
                    else networkLines.Add(line);
                }
            }
            foreach (var network in networks)
            {
                Network.FromLines(network)?.Solve();
            }
        }
    }
}
