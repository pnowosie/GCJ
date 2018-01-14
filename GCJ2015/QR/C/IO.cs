using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C
{
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

        public IEnumerable<T> ReadMany<T>(Func<string, object> conv = null)
        {
            var chunks = In.ReadLine().Split();

            if (null == conv) conv = s => Convert.ChangeType(s, typeof(T));
            return chunks.Select(conv)
                         .Cast<T>();
        }

        public List<T> ReadList<T>(Func<string, object> conv = null)
        {
            return ReadMany<T>(conv).ToList();
        }

        public T[] ReadArray<T>(Func<string, object> conv = null)
        {
            return ReadMany<T>(conv).ToArray();
        }

        public T Read<T>(Func<string, object> conv = null)
        {
            string elt = In.ReadLine().Split().Single();
            if (null == conv) conv = s => Convert.ChangeType(s, typeof(T));
            return (T)conv(elt);
        }
    }


}
