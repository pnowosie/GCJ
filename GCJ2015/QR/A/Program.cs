using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace A
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
                int invites = 0, stood = 0, level =0;
                var input = io.In.ReadLine().Split().ToArray();
                foreach (int standing in input[1].Select(_ => (int)_-0x30))
                {
                    Debug.Assert(0 <= standing && standing <= 9);
                    if (level > stood)
                        { ++invites; ++stood; }

                    stood += standing;
                    ++level;
                }

                io.Out.WriteLine(c.MakeOutput(invites));
            }
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
