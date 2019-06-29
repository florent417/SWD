using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapReduce
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
            var files =
                Directory.EnumerateFiles(@"C:\Users\flole\Desktop\GIT_Skole\SWD\W13_2_Mapreduce\cards", "*.txt")
                    .AsParallel();

            var wordCount = files.MapReduce(
                path => Source(path),
                map => Map(map),
                group => Reduce(group));

            var wc = wordCount.ToList();
            //wc.Sort(AscendingComparison);

            foreach (var pair in wc)
            {
                Console.WriteLine("{0}: {1}", pair.Key, pair.Value);
            }
        }

        // Source() provides the source data on which the MapReduce query shall run. 
        private static IEnumerable<string> Source(string path)
        {
            return File.ReadLines(path) // Read all lines in the path
                .SelectMany(line => line.ToLower().Split(new char[] {' ', ',', '.', '-', '!', '?', ';'}));
            // Project the words into a single enumerable
        }

        private static string Map(string word)
        {
            var key = word.Split(new char[',']).First();
            return key;
        }


        // Reduce() returns a list of Key/value pairs representing the results
        // An IGrouping<> represents a set of values that have the same key
        private static IEnumerable<KeyValuePair<string, int>> Reduce(IGrouping<string, string> group)
        {
            int sum = 0;
            sum = group.Sum<string>(e =>
            {
                int val = int.Parse(e.Split(new char[]{ ',' }).ElementAt(1));
                return val;
            });

            return new KeyValuePair<string, int>[]
            {

                new KeyValuePair<string, int>(group.Key, sum)
            };
        }
    }
}


//        // Map() returns the key which the word fits
//        private static int Map(string word)
//        {
//            return word.Length;
//        }


//        // Reduce() returns a list of Key/value pairs representing the results
//        // An IGrouping<> represents a set of values that have the same key
//        private static IEnumerable<KeyValuePair<int, int>> Reduce(IGrouping<int, string> group)
//        {
//            return new KeyValuePair<int, int>[]
//            {
//                new KeyValuePair<int, int>(group.Key,group.Count())
//            };
//        }




//        private static int AscendingComparison(KeyValuePair<int, int> a, KeyValuePair<int, int> b)
//        {
//            return a.Key - b.Key;
//        }
//    }
//}
