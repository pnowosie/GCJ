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
                HashSet<int> set = null;
                int ans1, ans2;
                ans1 = io.Read<int>();
                for (int i = 1; i <= 4; i++)
                {
                    if (i == ans1)
                        set = new HashSet<int>(io.ReadMany<int>());
                    else io.In.ReadLine();
                }

                Debug.Assert(set != null);

                ans2 = io.Read<int>();
                for (int i = 1; i <= 4; i++)
                {
                    if (i == ans2)
                        set.IntersectWith(io.ReadMany<int>());
                    else io.In.ReadLine();
                }

                string answer;
                switch (set.Count)
                {
                    case 0: answer = "Volunteer cheated!"; break;
                    case 1: answer = set.Single().ToString(); break;
                    default: answer = "Bad magician!"; break;
                    
                }

                io.Out.WriteLine(c.MakeOutput(answer));
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
