using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkArchitect
{
    public class Program
    {
        public static void Main(string[] args)
        {
            const string DEFAULT_PATH = "test.txt";
            Console.Write($"Enter path to network file: ({DEFAULT_PATH}) ");
            var path = Console.ReadLine();
            if (string.IsNullOrEmpty(path)) path = DEFAULT_PATH;
            try
            {
                NetworkSolver.SolveAllFromFile(path);
            }
            catch (IOException)
            {
                Console.WriteLine($"Could not find file at path: {path}");
            }
            Console.ReadKey();
        }
    }
}
