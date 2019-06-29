using System;
using System.Collections.Generic;
using System.Linq;

namespace MapReduceDemo
{
    internal static class MapReduceDemo
    {

        // See https://msdn.microsoft.com/en-us/library/bb397687.aspx - Lambdas with the standard query operators
        // The return value is always specified in the last type parameter. 
        // E.g. Func<int, string, bool> defines a delegate with two input parameters, int and string, and a return type of bool
        public static ParallelQuery<TResult> MapReduce<TSource, TMapped, TKey, TResult>(
            this ParallelQuery<TSource> source,
            Func<TSource, IEnumerable<TMapped>> map,
            Func<TMapped, TKey> keySelector,
            Func<IGrouping<TKey, TMapped>, IEnumerable<TResult>> reduce)
        {
            return source
                .SelectMany(map)
                .GroupBy(keySelector)
                .SelectMany(reduce);
        }

        //*********************************************************
        //*********************************************************
        // WORD-COUNT-BY-LENGTH EXAMPLE
        //*********************************************************
        //*********************************************************
        private static void Main(string[] args)
        {
            CountWordsByLength.Count();
            CountWordOccurences.Count();
            CountCards.Count();
            CountCardsWithTuples.Count();
            CountCardsSequentially.Count();
        }
    }
}
