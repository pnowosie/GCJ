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
    class ProgramA
    {
        static void MainA(string[] args)
        {
            Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;
            var io = new Io(args);

            var T = io.Read<int>();
            T.Times((C) =>
            {
                var digits = New();
                int i = 0, n = io.Read<int>();
                while (i++ < 100)
                {
                    KeepTrack(digits, i*n);
                    if (AllSeen(digits)) break;
                }

                WriteLine($"Case #{C + 1}: "+ (n == 0 ? "INSOMNIA" : (i*n).ToString()));
            });
        }

        static Dictionary<char, int> New()
            => Range('0', '9').ToDictionary(identity, _ => 0);

        static bool AllSeen(Dictionary<char, int> digits) => digits.Values.All(i => i > 0);

        static void KeepTrack(Dictionary<char, int> digits, int n)
        {
            foreach (var d in n.ToString()) digits[d]++;
        }
    }
}
