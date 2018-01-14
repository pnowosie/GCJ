using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCJ2016
{
    class Io
    {
        private readonly TextReader _in;

        public Io(string[] args)
        {
            if (args.Any() && File.Exists(args.First()))
            {
                _in = new StreamReader(args.First());
            }
            else
            {
                _in = Console.In;
            }
        }

        public TextReader In => _in;

        public IEnumerable<T> ReadMany<T>(Func<string, object> conv = null)
        {
            var chunks = In.ReadLine().Split();

            if (null == conv) conv = s => Convert.ChangeType(s, typeof(T));
            return chunks.Select(conv)
                         .Cast<T>();
        }

        public List<T> ReadList<T>(Func<string, object> conv = null)
         => ReadMany<T>(conv).ToList();

        public T[] ReadArray<T>(Func<string, object> conv = null)
            => ReadMany<T>(conv).ToArray();

        public T Read<T>(Func<string, object> conv = null)
        {
            string elt = In.ReadLine().Split().Single();
            if (null == conv) conv = s => Convert.ChangeType(s, typeof(T));
            return (T)conv(elt);
        }
    }


}
