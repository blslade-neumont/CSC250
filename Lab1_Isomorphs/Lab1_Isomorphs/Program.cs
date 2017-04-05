using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1_Isomorphs
{
    public class Program
    {
        static string input_1 = @"aaa fear mates gag egg add foo sap yay tot look meet took seer seep ate bar eat fit";
        static string input_2 = @"aaa fear mates gag egg add foo sap yay tot look meet took seer seep ate bar eat fit
aaabbbzzz bbbaaazzz abzzbabaz bazzababz warrior aedor eiruw aa bb cacccdaabc cdcccaddbc dcdddbccad bdbbbaddcb bdbcadbbdc abaadcbbda babcdabbac cacdbaccad dcddabccad cacccbaadb bbcdcbcbdd bcbadcbbca";

        public static void Main(string[] args)
        {
            var groups = new IsomorphGroups();
            groups.AddWords(getWords());
            groups.PrintStats(Console.Out);
            Console.ReadKey();
        }

        private static IEnumerable<string> getWords()
        {
            //TODO: allow user to specify a path to a file
            //return input_1.Split();
            return input_2.Split(new char[] { ' ', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
