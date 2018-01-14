using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace B
{
    class Program
    {
        private static Dictionary<char, int> C; 
        static void Main(string[] args)
        {
            var io = new IO(args);
            int T = io.Read<int>();
            foreach (int c in T.Times())
            {
                io.In.ReadLine();
                var words = io.In.ReadLine().Split();
                C = new Dictionary<char, int>();

                if (!Shorten(words))
                {
                    io.Out.WriteLine(c.MakeOutput(0));
                    continue;
                }

                io.Out.WriteLine(c.MakeOutput());
            }

            
        }

        private static bool Shorten(string[] words)
        {
            var midc = new Dictionary<char, int>();
            for (int i = 0; i < words.Length; ++i)
            {
                var w = words[i];
                char pc = '\0';
                if (w.Length > 2)
                {
                    for (int j = 1; j < w.Length - 1; ++j)
                    {
                        if (pc != w[i])
                            if (midc.ContainsKey(w[i])) midc[w[i]]++; 
                            else midc[w[i]] = 1; 
                        pc = w[i];
                    }
                    words[i] = string.Concat(w.First(), w.Last());
                }
            }

            foreach (var w in words)
            {
                if (midc.ContainsKey(w.First()) || midc.ContainsKey(w.Last())) return false;
            }

            return (!midc.Any()) || midc.Values.Max() < 2;
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