using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1_Isomorphs
{
    public class IsomorphGroups
    {
        public IsomorphGroups()
        {
        }

        private Dictionary<string, string> loose_patterns = new Dictionary<string, string>();
        private Dictionary<string, string> exact_patterns = new Dictionary<string, string>();

        public void AddWords(IEnumerable<string> words)
        {
            StringBuilder sb = new StringBuilder();
            Dictionary<char, int> iso_val = new Dictionary<char, int>();
            Dictionary<int, int> iso_count = new Dictionary<int, int>();
            foreach (var word in words)
            {
                if (loose_patterns.ContainsKey(word)) continue;
                sb.Clear();
                iso_val.Clear();
                iso_count.Clear();
                int nextVal = 0;
                foreach (var chr in word)
                {
                    if (!iso_val.ContainsKey(chr)) iso_val[chr] = nextVal++;
                    var val = iso_val[chr];
                    if (!iso_count.ContainsKey(val)) iso_count[val] = 1;
                    else iso_count[val]++;
                    sb.Append((char)('A' + val));
                }
                exact_patterns[word] = sb.ToString();
                loose_patterns[word] = string.Concat(iso_count.Values.OrderBy(v => v).Select(v => (char)('0' + v)));
            }
        }

        public void PrintStats(TextWriter cout)
        {
            var exact_isomorphs = (from kvp in exact_patterns
                                   group kvp.Key by kvp.Value into g
                                   select new { pattern = g.Key, words = g.ToArray() }).ToArray();
            var loose_isomorphs = (from kvp in loose_patterns
                                   group kvp.Key by kvp.Value into g
                                   select new { pattern = g.Key, words = g.ToArray() }).ToArray();

            var no_exact_isomorphs = exact_isomorphs.Where(tup => tup.words.Length == 1).Select(tup => tup.words[0]).ToArray();
            var non_isomorphs = loose_isomorphs.Where(tup => tup.words.Length == 1).Select(tup => tup.words[0]).Where(word => no_exact_isomorphs.Contains(word)).ToArray();

            Console.WriteLine("Exact isomorphs:");
            foreach (var exact in exact_isomorphs.Where(val => val.words.Length > 1))
            {
                Console.WriteLine($"{exact.pattern}: [{string.Join(", ", exact.words)}]");
            }
            Console.WriteLine();

            Console.WriteLine("Loose isomorphs:");
            foreach (var loose in loose_isomorphs.Where(val => val.words.Length > 1))
            {
                Console.WriteLine($"{loose.pattern}: [{string.Join(", ", loose.words)}]");
            }
            Console.WriteLine();

            Console.WriteLine("Non-isomorphs:");
            Console.WriteLine(string.Join(" ", non_isomorphs));
            Console.WriteLine();
        }
    }
}
