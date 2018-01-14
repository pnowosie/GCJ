#region Usings
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Numerics;
using System.Diagnostics;
#endregion
using io = System.Console;
using E = System.Linq.Enumerable;

namespace GCJ
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;
            //Debugger.Launch();


            int T = int.Parse(io.ReadLine());
            foreach (var C in E.Range(1, T))
            {
                int curM = int.Parse(io.ReadLine().Split().First());
                var motes = io.ReadLine().Split()
                              .Select(int.Parse).OrderBy(m => m).ToList();

                List<int> bestSoFar = new List<int> { motes.Count };
                Debug.Assert(bestSoFar.Count == 1);

                int ind = 0, cost = 0;
                while (ind < motes.Count && curM > 1)
                {
                    while (ind < motes.Count && curM > motes[ind]) curM += motes[ind++];

                    bestSoFar.Add(cost + motes.Count - ind);
                    if (ind >= motes.Count) break;

                    while (curM <= motes[ind])
                    {
                        curM = 2 * curM - 1;
                        cost++;
                    }
                }

                io.WriteLine(C.Answer(bestSoFar.Min()));
            }
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
