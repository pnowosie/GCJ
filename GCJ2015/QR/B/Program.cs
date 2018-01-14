using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace B
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;
            var io = new IO(args);

            var T = io.Read<int>();
            foreach (var c in T.Times())
            {
                var inp = io.ReadList<double>();
                double C = inp[0], F = inp[1], X = inp[2];

                double ans = Answer(C, F, X);

                io.Out.WriteLine(c.MakeOutput("{0:f7}", ans));
            }
        }

        static double Answer(double C, double F, double X)
        {
            double TX = X/2.0, TAll = 0.0d;
            int k = 0;

            while (true)
            {
                double nb = Cost(X, F, k),
                    yb = Cost(C, F, k) + Cost(X, F, k + 1);
                if (nb < yb)
                {
                    TAll += nb;
                    break;
                }
                else
                {
                    TAll += Cost(C, F, k++);
                }

                if (TX < TAll) return TX;
            }

            return TAll;
        }

        static double Cost(double G, double F, int k)
        {
            return G/(2 + k*F);
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
