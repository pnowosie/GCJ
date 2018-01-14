using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace GCJ
{
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
