using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTask
{
    static class MyToString
    {
        public static string ToString<T>(this IEnumerable<T> items, string separator)
        {
            return string.Join(separator, items);
        }
        public static string ToString<T>(this IEnumerable<T> items, string convertFormat, string separator)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var s in items)
            {
                sb.Append(String.Format(convertFormat, s));
                sb.Append(separator);
            }
            sb.Remove(sb.Length - separator.Length, separator.Length);
            return sb.ToString();
        }
    }
}
