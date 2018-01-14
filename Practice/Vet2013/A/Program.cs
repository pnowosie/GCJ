using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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

            var T = int.Parse(io.In.ReadLine());
            foreach (var c in Enumerable.Range(1, T))
            {
                io.In.ReadLine();
                //Debugger.Launch();
                var input = io.ReadList<float>();
                for (int i = 1; i < input.Count - 1; ++i)
                {
                    var avg = (input[i - 1] + input[i + 1])/2;
                    if (avg < input[i]) input[i] = avg;
                }
                io.Out.WriteLine("Case #{0}: {1:f6}", c, input[input.Count - 2]);
                //Console.WriteLine("Case #{0}: {1:f6}", c, input[input.Length-2]);
            }
        }

        class IO
        {
            private TextReader _in;
            private TextWriter _out;

            public IO(string[] args)
            {
                if (args.Any() && File.Exists(args.First()))
                {
                    _in = new StreamReader(args.First());
                }
                else
                {
                    _in = Console.In;
                }
                _out = Console.Out;
            }

            public TextReader In { get { return this._in; } }
            public TextWriter Out { get { return this._out; } }

            public List<T> ReadList<T>(Func<string, object> conv = null)
            {
                var chunks = In.ReadLine().Split();

                if (null == conv) conv = s => Convert.ChangeType(s, typeof(T));
                var result = chunks.Select(conv)
                                   .Cast<T>()
                                   .ToList();

                return result;
            }

            public T Read<T>(Func<string, object> conv = null)
            {
                string elt = In.ReadLine().Split().Single();
                if (null == conv) conv = s => Convert.ChangeType(s, typeof(T));
                return (T)conv(In.ReadLine());
            }
        }

        static string io()
        {
            var r = Console.In;
            return r.ReadLine();
        }
    }
}
