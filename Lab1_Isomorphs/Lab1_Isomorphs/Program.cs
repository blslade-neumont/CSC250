using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1_Isomorphs
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var groups = new IsomorphGroups();
            var words = getWords();
            if (words != null)
            {
                groups.AddWords(words);
                Console.WriteLine();

                StringWriter sw = new StringWriter();
                groups.PrintStats(sw);
                var str = sw.GetStringBuilder().ToString();
                Console.WriteLine(str);
                saveOutput(str);
            }

            Console.WriteLine();
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }

        const string defaultInputFile = "sample2.txt";

        private static IEnumerable<string> getWords()
        {
            string input = null;
            while (input == null)
            {
                Console.Write($"Enter the path to a seed file: ({defaultInputFile}) ");
                var pathToFile = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(pathToFile)) pathToFile = defaultInputFile;

                try
                {
                    using (var fs = File.OpenRead(pathToFile))
                    using (var reader = new StreamReader(fs))
                        input = reader.ReadToEnd();
                }
                catch (Exception)
                {
                    Console.WriteLine("ERROR: Invalid path. Try again.");
                }
            }

            return input.Split(new char[] { ' ', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
        }

        private static void saveOutput(string output, string outPath = "output.txt")
        {
            Console.Write($"Saving output to \"{outPath}\"... ");

            try
            {
                using (var fs = File.OpenWrite(outPath))
                using (var writer = new StreamWriter(fs))
                    writer.Write(output);
                Console.WriteLine("Done.");
            }
            catch (Exception e)
            {
                Console.WriteLine();
                Console.WriteLine($"Failed to save output. Error: {e.Message}");
            }
        }
    }
}
