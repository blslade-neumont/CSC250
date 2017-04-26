using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4_NQueens
{
    public class Program
    {
        static Program()
        {
            computeFactorial(12);
        }

        public static void Main(string[] args)
        {
            var n = solicitInt("Enter a number between 1 and 12: ", num => num >= 1 && num <= 12);
            var nfact = fact[n];
            int[] positions = new int[n];
            int[] checkPositions = new int[n];
            int solutionCount = 0;
            for (int q = 0; q < nfact; q++)
            {
                var perm = permutation(positions, q);
                int i = 0;
                foreach (var val in perm)
                {
                    checkPositions[i] = val;
                    for (int w = 0; w < i; w++)
                    {
                        var diff = checkPositions[w] - val;
                        var diffIdx = w - i;
                        if (diff == diffIdx || diff == -diffIdx) goto nextPerm;
                    }
                    i++;
                }
                Console.WriteLine($"Solution found, permutation {q}:");
                printBoard(checkPositions);
                solutionCount++;
            nextPerm:;
            }

            Console.WriteLine($"Done! {nfact} permutations checked, {solutionCount} solutions found.");
            Console.ReadKey();
        }

        private static int solicitInt(string prompt = "Enter a number: ", Func<int, bool> predicate = null)
        {
            if (predicate == null) predicate = num => true;
            while (true)
            {
                Console.Write(prompt);
                var input = Console.ReadLine();
                if (int.TryParse(input, out int num) && predicate(num)) return num;
                Console.WriteLine("Invalid input.");
            }
        }

        private static int[] fact = new[] { 1, 1, 2 };
        private static int computeFactorial(int n)
        {
            var computed = fact.Length;
            if (computed < n + 1)
            {
                var arr = new int[n + 1];
                Array.Copy(fact, arr, fact.Length);
                fact = arr;
            }

            var val = fact[computed - 1];
            for (int q = computed; q < n + 1; q++)
                fact[q] = (val *= q);
            return val;
        }
        
        private static IEnumerable<int> permutation(int[] positions, int nthPerm)
        {
            for (int q = 0; q < positions.Length; q++)
                positions[q] = q;
            return perm(positions.Length, nthPerm);

            IEnumerable<int> perm(int n, int k)
            {
                while (n != 0)
                {
                    var f = fact[n - 1];
                    var i = (int)Math.Floor(k / (double)f);
                    yield return positions[i];
                    swap(ref positions[i], ref positions[--n]);
                    k %= f;
                }
            }
            void swap<T>(ref T x, ref T y)
            {
                var temp = x;
                x = y;
                y = temp;
            }
        }

        private static void printBoard(int[] board)
        {
            for (int q = 0; q < board.Length; q++)
            {
                for (int w = 0; w < board.Length; w++)
                {
                    Console.Write(board[w] == q ? 'Q' : '-');
                }
                Console.WriteLine();
            }
        }
    }
}
