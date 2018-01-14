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
    class ProgramD
    {
        static void MainD(string[] args)
        {
            Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;
            var io = new Io(args);

            var T = io.Read<int>();
            T.Times((X) =>
            {
                var L = io.ReadArray<int>();
                int K = L[0], C = L[1], S = L[2];

                if (C*S < K)
                {
                    WriteLine($"Case #{X + 1}: IMPOSSIBLE");
                    return;
                }

                var pos = new List<long>();
                int i = 0;
                while (i < K)
                {
                    int pi = i;
                    i = Math.Min(i + C - 1, K - 1);

                    long p = 0;
                    while (pi <= i) p = p*K + (pi++);

                    pos.Add(p+1);
                    ++i;
                }

                WriteLine($"Case #{X+1}: "+ string.Join(" ", pos));
            });
        }
    }
}
