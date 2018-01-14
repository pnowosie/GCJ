using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Numerics;
using System.Diagnostics;
using io = System.Console;
using E = System.Linq.Enumerable;

namespace GCJ
{
    class Program
    {
        private static List<int> motes;
        static void Main(string[] args)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;
            //Debugger.Launch();


            int T = int.Parse(io.ReadLine());
            foreach (var C in E.Range(1, T))
            {
                int m = int.Parse(io.ReadLine().Split().First());
                motes = io.ReadLine().Split().Select(int.Parse).ToList();

                motes.Sort();

                var ans = Absorb(m, 0);

                io.WriteLine(C.Answer(ans));
                motes = null;
            }
        }

        static int Absorb(int curM, int ind)
        {
            // try absorb as many as possible
            while (ind < motes.Count && curM > motes[ind])
            {
                curM += motes[ind++];
            }

            if (ind >= motes.Count) return 0;

            int removed = Absorb(curM, ind + 1);
            int added = curM > 1 ? Absorb(2*curM - 1, ind) : int.MaxValue;

            return 1 + Math.Min(removed, added);
        }
    }

    #region Common Tools
    internal static class Ext
    {

        public static string Answer(this int C, params object[] args)
        {
            if (null == args || null == args.FirstOrDefault())
            {
                throw new ArgumentNullException("args", "First argument cannot be ommited or null");
            }

            string answer = args.First().ToString();

            string fmt = args.First() as string;
            if (fmt != null)
            {
                answer = string.Format(fmt, args.Skip(1).ToArray());
            }


            return string.Concat("Case #", C, ": ", answer);
        }

        public static void Each<T>(this IEnumerable<T> @this, Action<T, int> job)
        {
            int _i = 0;
            foreach (var elt in @this)
            {
                job(elt, _i++);
            }
        }
    }
    #endregion
}
