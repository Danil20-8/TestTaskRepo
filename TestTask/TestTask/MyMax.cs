using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTask
{
    static class MyMax
    {
        public static IEnumerable<Tsourse> WithMax<Tsourse, Tcomparable>(this IEnumerable<Tsourse> enumerable, Func<Tsourse, Tcomparable> selector) where Tcomparable : IComparable
        {
            using (var i = enumerable.GetEnumerator())
            {
                if (!i.MoveNext())
                    return new Tsourse[0];

                var curr = i.Current;

                List<Tsourse> maxItems = new List<Tsourse>();
                maxItems.Add(curr);
                Tcomparable maxValue = selector(curr);

                while(i.MoveNext())
                {
                    curr = i.Current;
                    var value = selector(curr);
                    var compareResult = value.CompareTo(maxValue);
                    if (compareResult > 0)
                    {
                        maxItems.Clear();
                        maxItems.Add(curr);
                        maxValue = value;
                    }
                    else if (compareResult == 0)
                    {
                        maxItems.Add(curr);
                    }
                }
                return maxItems;
            }
        }
    }
}
