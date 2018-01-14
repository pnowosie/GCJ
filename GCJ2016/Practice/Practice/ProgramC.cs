using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using static System.Linq.Enumerable;
using static System.Console;
using static LanguageExt.Prelude;
using static GCJ2016.Loop;

namespace GCJ2016
{
    using BI = System.Numerics.BigInteger;
    class ProgramC
    {
        private static void Main(string[] args)
        {
            Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;
            var io = new Io(args);

            var T = io.Read<int>();
            T.Times((C) =>
            {
                WriteLine($"Case #{C+1}:");
                var L = io.ReadArray<int>();
                int N = L[0], J = L[1];

                int ij = 0;
                while (J > 0)
                {
                    var jc = string.Concat('1', Convert.ToString(ij++, 2).PadLeft(N - 2, '0'), '1');
                    var bases = GetBaseValues(jc);
                    var divisors = GetDivisors(bases);
                    if (divisors.Length() == bases.Length())
                    {
                        WriteLine($"{jc} "+string.Join(" ", divisors));
                        --J;
                    }
                }
            });
        }

        static BI[] GetBaseValues(string jamcoin)
        {
            var coins = new BI[9];
            for (int i = 0; i < coins.Length; i++)
            {
                coins[i] = BI.Zero;
                for (int j = 0; j < jamcoin.Length; j++)
                {
                    if ('1' == jamcoin[jamcoin.Length - 1 - j])
                        coins[i] += BI.Pow(i+2, j);
                }
            }
            return coins;
        }

        static List<int> GetDivisors(BI[] bases)
        {
            var divisors = new List<int>();
            foreach (var bi in bases)
            {
                int d = 1;
                while (++d < 100)
                    if (bi%d == 0)
                    {
                        divisors.Add(d);
                        break;
                    }
            }
            return divisors;
        }
    }
}
