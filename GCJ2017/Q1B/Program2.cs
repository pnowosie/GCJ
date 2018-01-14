namespace Q1B
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Threading.Tasks;

    class Program
    {
        static void Main(string[] args)
        {
            string _fin = "0.in", _fout = "0.out";
            if (args.Length > 0)
            {
                _fin = args[0];
                _fout = args[0] + ".out";
            }

            using (var fin = new StreamReader(_fin, Encoding.UTF8))
            using (var fout = new StreamWriter(_fout))
            {
                var T = int.Parse(fin.ReadLine());
                for (int c = 1; c <= T; c++)
                {
                    var N = fin.ReadLine();
                    fout.WriteLine($"Case #{c}: {fix(N)}");
                }
            }
        }

        static string fix(string n)
        {
            int i = 0, j = 0;
            while (i < n.Length - 1 && n[i] <= n[i + 1])
            {
                ++i;
                if (n[j] < n[j + 1]) ++j;
            }
            if (i == n.Length - 1) return n;

            return (n.Substring(0, j)
                + ((char)(n[j] - 1))
                + new string('9', n.Length - j - 1)
               ).TrimStart("0".ToCharArray());
        }
    }
}
