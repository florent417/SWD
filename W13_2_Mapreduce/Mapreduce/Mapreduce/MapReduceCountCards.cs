//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Mapreduce
//{
//    internal static class MapReduceCountCards
//    {
//        public static ParallelQuery<TResult> MapReduce<TSource, TMapped, TKey, TResult>(
//            this ParallelQuery<TSource> source,
//            Func<TSource, IEnumerable<TMapped>> map,
//            Func<TMapped, TKey> keySelector,
//            Func<IGrouping<TKey, TMapped>, IEnumerable<TResult>> reduce)
//        {
//            // ==============================================================
//            // COMMENT IN TO GET INSTRUMENTED VERSION OF MapReduce()
//            // ==============================================================
//            //Console.WriteLine("TSource: {0}", typeof(TSource));
//            //Console.WriteLine("TMapped: {0}", typeof(TMapped));
//            //Console.WriteLine("TKey: {0}", typeof(TKey));
//            //Console.WriteLine("TResult: {0}", typeof(TResult));

//            //var selectManyFromSourceResult = source.SelectMany(map);
//            ////foreach (var mapped in selectManyFromSourceResult) Console.WriteLine("mapped: {0}", mapped);

//            //var grouped = selectManyFromSourceResult.GroupBy(keySelector);
//            ////foreach (var grouping in grouped)
//            ////{
//            ////    Console.Write("grouped: {0}: ", grouping.Key);
//            ////    foreach (var groupItem in grouping)
//            ////    {
//            ////        Console.Write("{0}, ", groupItem);
//            ////    }
//            ////    Console.WriteLine();
//            ////}

//            //var reduced = grouped.SelectMany(reduce);
//            ////foreach (var r in reduced) Console.WriteLine("reduced: {0}", r);
//            //return reduced;

//            return source
//                .SelectMany(map)
//                .GroupBy(keySelector)
//                .SelectMany(reduce);
//        }

//        //*********************************************************
//        //*********************************************************
//        // WORD-COUNT-BY-LENGTH EXAMPLE
//        //*********************************************************
//        //*********************************************************
//        private static void Main(string[] args)
//        {
//            var files =
//                Directory.EnumerateFiles(@"C:\Users\flole\Desktop\GIT_Skole\SWD\W13_2_Mapreduce\cards", "*.txt")
//                    .AsParallel();

//            var wordCount = files.MapReduce(
//                path => Source(path),
//                map => Map(map),
//                group => Reduce(group));

//            var wc = wordCount.ToList();
//            //wc.Sort(AscendingComparison);

//            foreach (var pair in wc)
//            {
//                Console.WriteLine("{0}: {1}", pair.Key, pair.Value);
//            }
//        }

//        // Source() provides the source data on which the MapReduce query shall run. 
//        private static IEnumerable<string> Source(string path)
//        {
//            //return File.ReadLines(path) // Read all lines in the path
//            //    .SelectMany(line => line.ToLower().Split(new char[] { ' ', ',', '.', '-', '!', '?', ';' }));
//            return File.ReadAllLines(path);
//            // Project the words into a single enumerable
//        }


//        // Map() returns the key which the word fits
//        private static string Map(string word)
//        {
//            var key = word.Split(new char[',']).First();
//            return key;
//        }


//        // Reduce() returns a list of Key/value pairs representing the results
//        // An IGrouping<> represents a set of values that have the same key
//        private static IEnumerable<KeyValuePair<string, int>> Reduce(IGrouping<string, string> group)
//        {
//            int sum = 0;
//            sum = group.Sum<string>(e =>
//            {
//                var el = e.Split(new char[',']).ElementAt(1);
//                return int.Parse(el);
//            });

//            return new KeyValuePair<string, int>[]
//            {

//                new KeyValuePair<string, int>(group.Key,sum)
//            };
//        }


//        private static int AscendingComparison(KeyValuePair<int, int> a, KeyValuePair<int, int> b)
//        {
//            return a.Key - b.Key;
//        }
//    }
//}
