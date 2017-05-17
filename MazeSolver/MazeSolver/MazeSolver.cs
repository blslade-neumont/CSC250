using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MazeSolver
{
    public static class MazeSolver
    {
        public static void SolveAllFromFile(string path)
        {
            using (var file = File.OpenRead(path))
                SolveAllFromStream(file);
        }
        public static void SolveAllFromStream(Stream stream)
        {
            var mazes = new List<string[]>();
            using (var reader = new StreamReader(stream))
            {
                var lines = (reader.ReadToEnd() + '\n')
                    .Split('\n')
                    .Select(line => line.Trim(' ', '\t', '\r', '\n'));
                var mazeLines = new List<string>();
                foreach (var line in lines)
                {
                    if (string.IsNullOrEmpty(line))
                    {
                        if (mazeLines.Count > 0)
                        {
                            mazes.Add(mazeLines.ToArray());
                            mazeLines.Clear();
                        }
                    }
                    else mazeLines.Add(line);
                }
            }
            foreach (var maze in mazes)
            {
                Maze.FromLines(maze)?.Solve();
            }
        }
    }
}
