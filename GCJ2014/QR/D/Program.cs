using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace D
{
    class Program
    {
        struct Block
        {
            public bool IsMy { get; set; }
            public float Weight { get; set; }
        }

        static void Main(string[] args)
        {
            Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;
            var io = new IO(args);

            var T = io.Read<int>();
            foreach (var c in T.Times())
            {
                int All = io.Read<int>();
                var Nb = io.ReadMany<float>();
                var Kb = io.ReadMany<float>();

                var blocks = Nb.Select(w => new Block {Weight = w, IsMy = true})
                    .Concat( Kb.Select(w => new Block {Weight = w, IsMy = false}))
                    .ToArray();
                Array.Sort(blocks, (b1, b2) => b1.Weight.CompareTo(b2.Weight));


                int Win = ShouldWin(blocks),
                    Los = MustLoose(blocks);

                io.Out.WriteLine(c.MakeOutput("{0} {1}", All-Los, Win));
            }
        }

        static int ShouldWin(Block[] blocks)
        {
            int Len = blocks.Length,
                M = 0, Y = 0;
            for (int i = Len-1; i >=0; i--)
            {
                if (blocks[i].IsMy)
                {
                    if (Y == 0) ++M;
                    else --Y;
                }
                else
                {
                    ++Y;
                }
            }
            return M;
        }

        static int MustLoose(Block[] blocks)
        {
            int Len = blocks.Length,
                M = 0, Y = 0;
            for (int i = 0; i < Len; i++)
            {
                if (blocks[i].IsMy)
                {
                    if (Y == 0) ++M;
                    else --Y;
                }
                else
                {
                    ++Y;
                }
            }
            return M;
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

        public static string MakeOutput(this int C, object arg)
        {
            return string.Concat("Case #", C, ": ", string.Format("{0}", arg));
        }

        public static string MakeOutput(this int C, string fmt, params object[] args)
        {
            return string.Concat("Case #", C, ": ", string.Format(fmt, args));
        }
    }
}
