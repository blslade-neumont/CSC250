using System;
using System.Linq;

namespace MazeSolver
{
    public static class ArrayExt
    {
        public static string[] ParseCSV(this string str)
        {
            return str
                .Split(',')
                .Select(val => val.Trim())
                .Where(val => !string.IsNullOrEmpty(val))
                .ToArray();
        }

        public static void Deconstruct<T>(this T[] ts, out T val0)
        {
            if (ts == null) throw new ArgumentNullException(nameof(ts));
            if (ts.Length < 1) throw new ArgumentException("This array is too short to be destructured", nameof(ts));
            val0 = ts[0];
        }
        public static void Deconstruct<T>(this T[] ts, out T val0, out T val1)
        {
            if (ts == null) throw new ArgumentNullException(nameof(ts));
            if (ts.Length < 2) throw new ArgumentException("This array is too short to be destructured", nameof(ts));
            val0 = ts[0];
            val1 = ts[1];
        }
    }
}
