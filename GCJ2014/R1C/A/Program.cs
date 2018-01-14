using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace A
{
    class Program
    {

        static void Main(string[] args)
        {
            var io = new IO(args);
            int T = io.Read<int>();
            foreach (int c in T.Times())
            {
                var pqs = io.In.ReadLine().Split('/')
                            .Select(long.Parse).ToArray();
                long pp = pqs.First(), qq = pqs.Last();

                Fraction(ref pp, ref qq);

                int ans = Answer(pp, qq);
                io.Out.WriteLine(c.MakeOutput(ans > 0 ? ans.ToString() : "impossible"));
            }

        }

        private static int Answer(long p, long q)
        {
            if (p == 0) return -1;
            if (q%2 == 1) return -1;

            int lvl = 1, ans = 0;
            while (q > 1)
            {
                q /= 2;
                if (q > 1 && q%2 == 1) return -1;
                if (ans == 0 && (1f*p)/q >= 1f) ans = lvl;
                ++lvl;
            }
            return ans;
        }

        private static void Fraction(ref long pp, ref long qq)
        {
            long g = GCD(pp, qq);
            pp /= g;
            qq /= g;
        }

        private static long GCD(long a, long b)
        {
            if (0 == b) return a;
            else return GCD(b, a%b);
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

        public static string MakeOutput(this int C)
        {
            return string.Concat("Case #", C, ":");
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