using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeSolver
{
    public class Program
    {
        public static void Main(string[] args)
        {
            const string DEFAULT_PATH = "test.txt";
            Console.Write($"Enter path to maze file: ({DEFAULT_PATH}) ");
            var path = Console.ReadLine();
            if (string.IsNullOrEmpty(path)) path = DEFAULT_PATH;
            try
            {
                MazeSolver.SolveAllFromFile(path);
            }
            catch (IOException)
            {
                Console.WriteLine($"Could not find file at path: {path}");
            }
            Console.ReadKey();
        }
    }
}
