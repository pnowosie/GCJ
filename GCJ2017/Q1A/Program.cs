namespace Q1A
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.IO;

    using static System.Diagnostics.Debug;
    using static System.Console;

    class Program
    {
        private const string noAns = "IMPOSSIBLE";
        static void Main(string[] args)
        {
            string _fin = "0.in", _fout = "0.out";
            if (args.Length > 0)
            {
                _fin = args[0];
                _fout = args[0] + ".out";
            }

            using (var fin = new StreamReader(_fin, Encoding.UTF8))
            using (var fout = new StreamWriter(_fout))
            {
                int N = int.Parse(fin.ReadLine());
                for (int c = 1; c <= N; c++)
                {
                    var ins = fin.ReadLine().Split();
                    bool[] row = ins[0].Select(x => x == '+').ToArray();
                    int flipCnt = int.Parse(ins[1]);

                    var solve = countFlips(row, flipCnt);
                    fout.WriteLine($"Case #{c}: {(solve.HasValue ? solve.Value.ToString() : noAns)}");
                }
            }
        }

        static int? countFlips(bool[] row, int flipCnt)
        {
            int count = 0, start = 0;
            while (true)
            {
                var unhappy = firstUnhappy(row, start);

                if (!unhappy.HasValue) return count;
                if (row.Length - unhappy < flipCnt) return null;


                flip(row, unhappy.Value, flipCnt);
                count++;
                start = unhappy.Value + 1;
            }

            Assert(false);
        }

        static void flip(bool[] row, int start, int flipCnt)
        {
            Assert(row.Length > start);
            for (int i=start; i<start+flipCnt; i++)
                row[i] = !row[i];
        }

        static int? firstUnhappy(bool[] row, int start)
        {
            if (start >= row.Length) return null;
            for (int i = start; i<row.Length; i++)
                if (!row[i]) return i;
            return null;
        }
    }
}
