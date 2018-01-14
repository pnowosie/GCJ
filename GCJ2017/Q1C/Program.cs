
namespace Q1C
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.IO;
    using System.Threading.Tasks;

    using static System.Diagnostics.Debug;
    

    class Program
    {
        private static string _fin = "0.in", _fout = "0.out";

        private static TextWriter fout;

        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                _fin = args[0];
                _fout = args[0] + ".out";
            }

            using (var fin = new StreamReader(_fin, Encoding.UTF8))
            using (fout = new StreamWriter(_fout))
            {
                var T = int.Parse(fin.ReadLine());
                for (int c = 1; c <= T; c++)
                {
                    var NK = fin.ReadLine().Split().Select(i => int.Parse(i));
                    int N = NK.First(), K = NK.Last();

                    //fout.WriteLine($"N = {N}, K = {K}");

                    //if (K > N / 2 + 1)
                    //{
                    //    fout.WriteLine($"Case #{c}: 0 0");
                    //    continue;
                    //}

                    bool[] r = build(N);
                    var best = Tuple.Create(0, 0);
                    for (int k = 1; k <= K; ++k) {
                        best = add2(r, k, false);
                    }
                    fout.WriteLine($"Case #{c}: {best.Item1} {best.Item2}");
                }
            }
        }

        private static bool[] build(int n)
        {
            var R = new bool[n + 2];
            R[0] = R[n + 1] = true;
            return R;
        }

        private static void print(bool[] r)
        {
            fout.WriteLine(
                string.Join("", r.Select(o => o ? "o" : "."))
                );
        }

        private static Tuple<int, int> mostLeftLogestEmpty(bool[] r)
        {
            int p = 0, k = 0, l = 0;
            int cp = 0;
            bool inside = false;
            for (int i = 0; i < r.Length; i++)
            {
                if (!inside && !r[i])
                {
                    inside = true;
                    cp = i;
                }
                if (inside && r[i])
                {
                    inside = false;
                    if (i - cp > l) {
                        p = cp; k = i; l = i - cp;
                    }
                }
            }
            return Tuple.Create(p, k);
        }

        private static Tuple<int, int> add2(bool[] r, int pe, bool pr = false)
        {
            //fout.WriteLine($"Adding {pe}-person");
            if (pr) print(r);

            var t = mostLeftLogestEmpty(r);
            int p = t.Item1, k = t.Item2, l = k - p;
            int mid = (p+k-1) / 2;
            r[mid] = true;

            int L = mid - p;
            int P = k - mid - 1; 

            if (pr) {
                print(r);
                fout.WriteLine($"Choice: pos {mid}, ({L}, {P})");
            }

            return Tuple.Create(Math.Max(L, P), Math.Min(L, P));
        }

        private static Choice add(bool[] r, int p, bool pr = false)
        {
            //fout.WriteLine($"Adding {p}-person");
            if (pr) print(r);

            Choice choice = new Choice(r.Length, -1, -1);
            var start = findFree(r, 0);
            bool hasChoice = start.HasValue;
            while (hasChoice)
            {
                Choice curr;
                if ((curr = new Choice(
                            start.Value, 
                            countL(r, start.Value),
                            countR(r, start.Value)))
                        .BetterThan(choice))
                    choice = curr;

                //fout.WriteLine($"Eval pos: {curr.Field}, best so far: {choice}");

                start = findFree(r, start.Value);
                hasChoice = start.HasValue;
            }
            r[choice.Field] = true;

            if (pr) {
                print(r);
                fout.WriteLine($"Choice: {choice}");
            }

            return choice;
        }

        private static int? findFree(bool[] r, int start)
        {
            Assert(r.Length > start);
            for (int i = start + 1; i < r.Length; ++i) if (!r[i]) return i;
            return null;
        }

        private static int countL(bool[] r, int pos)
        {
            int c = 0;
            while (!r[--pos]) ++c;
            return c;
        }
        private static int countR(bool[] r, int pos)
        {
            int c = 0;
            while (!r[++pos]) ++c;
            return c;
        }

        struct Choice
        {
            public Choice(int field, int left, int right)
            {
                this.Field = field;
                this.Left = left;
                this.Right = right;
            }

            public int Field { get; }
            public int Left { get; }
            public int Right { get; }

            public int m => Math.Min(Left, Right);
            public int M => Math.Max(Left, Right);

            public bool BetterThan(Choice that)
            {
                return this.m > that.m 
                    || (this.m == that.m && this.M > that.M)
                    || (this.m == that.m && this.M == that.M && this.Field < that.Field);
            }

            public override string ToString()
            {
                return $"Pos: {this.Field} ({this.Left}, {this.Right})";
            }
        }
    }
}
