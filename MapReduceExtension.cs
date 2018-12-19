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

        public static IEnumerable<T> Filter<T>(this IEnumerable<T> table, Func<T,bool> condition ){
            List<T> result = new List<T>();

            for (int i = 0; i < table.Count(); i++)
            {
                if(condition(table.ElementAt(i))){

                result.Add(table.ElementAt(i));
                }
            }


            return result;
        }

        public static U Reduce<T,U>(this IEnumerable<T> table, U init,  Func<U,T,U> f ){

            U acc = init;
            for (int i = 0; i < table.Count(); i++)
            {
                acc = f(acc,table.ElementAt(i));
            }



            return acc;
        }
    }
}
