using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

using io = System.Console;
using System.Diagnostics;

using System.Numerics;

namespace C
{
    class Program
    {
        private static Dictionary<int, List<string>> pali = new Dictionary<int, List<string>> {
            { 0, new List<string> { "" }},
            { 1, new List<string> { "0", "1", "2", "3" }},
        };

        private static void Main(string[] args)
        {
            int MAX = 50;
            GenerateP(MAX);
            
            var numbers = pali.Skip(1)
                             .SelectMany(p => p.Value)
                             .Where(p => !p.StartsWith("0"))
                             .Select(BigInteger.Parse)
                             .OrderBy(p => p)
                             .ToList();

            var T = int.Parse(io.ReadLine());

            foreach (var c in T.Times())
            {
                var l = io.ReadLine().Split();

                BigInteger A = BigInteger.Parse(l.First()),
                           B = BigInteger.Parse(l.Last());

                var Cnt = numbers.Select(p => p*p)
                                 .SkipWhile(q => q < A)
                                 .TakeWhile(q => q <= B)
                                 .Count(IsP);
                
                io.WriteLine(c.MakeOutput(Cnt));
            }
        }

        static Func<IEnumerable<int>, int> dotProduct = 
                l1 => l1.Zip(l1, (i,j) => i*j).Sum(); 

        static void GenerateP(int len)
        {
            if (len <= 0 || pali.ContainsKey(len)) 
                return;

            GenerateP(len - 1);

            pali.Add(len,
                     pali[len - 2].SelectMany(p =>
                                              pali[1].Select(q => string.Concat(q, p, q))
                                 ).Where(p => dotProduct(p.Select(c => c-'0')) < 10)
                                  .ToList()
                );
        }

        static bool IsP(BigInteger n)
        {
            var s = n.ToString();
            return s.SequenceEqual(s.Reverse());
        }
    }

    public static class Ext
    {
        private static int _i;
        public static int RepCnt { get { return _i; } }

        public static IEnumerable<int> Times(this int N)
        {
            if (N <= 0)
                yield break;
            int i = 0;
            while (++i <= N)
                yield return i;
        }

        public delegate void JobDelegate();
        public static void Repeat(this int times, JobDelegate job)
        {
            _i = 0;
            while (++_i <= times)
                job();

            _i = 0;
        }

        public static string MakeOutput(this int C, params object[] args)
        {
            return string.Concat("Case #", C, ": ", string.Format("{0}", args));
        }
    }
}
