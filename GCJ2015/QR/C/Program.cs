using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace C
{
    class Program
    {
        private static char[,] Board = new char[50, 50];

        static void Main(string[] args)
        {
            Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;
            var io = new IO(args);

            var T = io.Read<int>();
            foreach (var c in T.Times())
            {
                var input = io.ReadList<int>();
                int R = input[0], C = input[1], M = input[2];

                Init(R, C, M);

                //int mC = MinesCnt(R, C);
                //bool validate = Validate(R, C, M);
                //io.Out.WriteLine(c.MakeOutput(validate ? "Correct" : "Incorrect"));
                //io.Out.WriteLine("{0} {1} {2} => {3}", R, C, M, M == mC);
                //Print(R, C);
                //io.Out.WriteLine("--------------------------------------------------------");

                io.Out.WriteLine(c.MakeOutput());
                if (Validate(R, C, M)) Print(R, C);
                else io.Out.WriteLine("Impossible");
                //                       Impossible
            }
        }

        static void Init(int R, int C, int M)
        {
            for (int r = R - 1; r > 1; --r)
            {
                for (int c = C - 1; c > 1; --c)
                {
                    if (M-- > 0) Board[r, c] = '*';
                    else Board[r, c] = '.';
                }
            }

            int R1 = R > 1 ? 1 : R - 1;
            for (int c = C - 1; c > 2; --c)
            {
                for (int r = R1; r >= 0; --r)
                {
                    if (M > 0) Board[r, c] = '*';
                    else if (M == 0)
                    {
                        if (r == 0 && R > 2 && C > 2)
                        {
                            Board[2, 2] = '.';
                            Board[0, c] = '*';
                        }
                        else
                        {
                            Board[r, c] = '.';
                        }
                    }
                    else if (M < 0) Board[r, c] = '.';
                    --M;
                }
            }

            int C1 = C > 1 ? 1 : C - 1;
            for (int r = R - 1; r > 2; --r)
            {
                for (int c = C1; c >= 0; --c)
                {
                    if (M > 0) Board[r, c] = '*';
                    else if (M == 0)
                    {
                        if (c == 0 && R > 2 && C > 2)
                        {
                            Board[2, 2] = '.';
                            Board[r, 0] = '*';
                        }
                        else
                        {
                            Board[r, c] = '.';
                        }
                    }
                    else if (M < 0) Board[r, c] = '.';
                    --M;
                }
            }

            for (int r = 2; r >= 0; --r)
                for (int c = 2; c >= 0; --c)
                {
                    if (r + c == 0 || r + c == 4) continue;
                    if (r < R && c < C)
                        if (M-- > 0) Board[r, c] = '*';
                        else Board[r, c] = '.';
                }

            if (C > 2 && R > 1
             && Board[1, 1] == '*'
             && Board[0, 2] == '.')
            {
                Board[1, 1] = '.';
                Board[0, 2] = '*';
            }

            Board[0, 0] = 'c';
        }

        static bool Validate(int R, int C, int M)
        {
            int cc = R*C;
            if (M == 0) return true;
            if (cc == M +1) return true;
            if (R == 1 || C == 1) return true;
            if (R == 2 || C == 2) return (cc - M >= 3) && (M%2 == 0);

            int e = 0;
            for (int r = 0; r <= 2; ++r)
                for (int c = 0; c <= 2; ++c)
                    if (r < R && c < C && Board[r, c] == '.') ++e;

            if (e == 0 || e == 8) return true;
            if (e == 1 || e%2 == 0) return false;
            //if (e == 3)
            //{
            //    Board[1, 1] = '.';
            //    Board[0, 2] = '*';
            //}

            return true;
        }

        static int MinesCnt(int R, int C)
        {
            int mines = 0;
            for (int r =0; r < R; ++r)
                for (int c =0; c < C; ++c)
                    if (Board[r, c] == '*') ++mines;
            return mines;
        }

        static void Print(int R, int C)
        {
            for (int r =0; r < R; ++r)
            {
                for (int c =0; c < C; ++c) Console.Write(Board[r, c]);
                Console.WriteLine();
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
