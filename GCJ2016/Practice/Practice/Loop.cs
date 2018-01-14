using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using static LanguageExt.Prelude;

namespace GCJ2016
{


    public static class Loop
    {
        /// <summary>
        /// Range [ i .. cnt ) by step
        /// </summary>
        public static IEnumerable<int> For(int i, int cnt, int step = 1)
            => Range(i, cnt, step).AsEnumerable();

        /// <summary>
        /// Range [ 0 .. n-1 ]
        /// </summary>
        public static IEnumerable<int> For(int n) => For(0, n);

        /// <summary>
        /// Loop i := 0 .. n-1
        /// </summary>
        public static void Times(this int n, Action<int> action)
            => For(0, n).Do(action);

        /// <summary>
        /// Loop reversed i := n-1 .. 0
        /// </summary>
        public static void Repeat(this int n, Action<int> action)
            => Range(n-1, n, -1).AsEnumerable().Do(action);


        public static void Do(this IEnumerable<int> range, Action<int> action)
        {
            foreach (int i in range) action(i);
        }

    }
}