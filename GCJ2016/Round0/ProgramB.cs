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
    class ProgramB
    {
        static void MainB(string[] args)
        {
            Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;
            var io = new Io(args);

            var T = io.Read<int>();
            T.Times((C) =>
            {
                var stack = io.In.ReadLine()
                    .Select(c => int.Parse(c + "1")).ToArray();

                int i = 0, flipCnt = 0, startSide = stack.FirstOrDefault();
                while (startSide != 0 && i < stack.Length)
                {
                    if (stack[i] != startSide)
                    {
                        ++flipCnt;
                        startSide *= -1;
                    }
                    ++i;
                }

                if (startSide < 0) ++flipCnt;

                WriteLine($"Case #{C+1}: {flipCnt}");
            });
        }
    }
}
