using System;
using System.Collections.Generic;
using System.Linq;

namespace Her
{


    public static class MapReduceExtension
    {
        public static IEnumerable<U> Map<T,U>(this IEnumerable<T> table, Func<T,U> f ){

            U[] result = new U[table.Count()];
            for (int i = 0; i < table.Count(); i++)
            {
                result[i] = f(table.ElementAt(i));
            }
            return result;
        }
    }
}
