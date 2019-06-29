using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace MapReduceDemo
{
    internal static class CountCards
    {
        internal static void Count()
        {
            Console.WriteLine("Counting cards");
            Stopwatch s1 = new Stopwatch();
            s1.Start();

            var files =
                Directory.EnumerateFiles(@"..\..\..\cards", "*.txt")
                    .AsParallel();

            var wordCount = files.MapReduce(
                path => Source(path),
                map => Map(map),
                group => Reduce(group));


            var wc = wordCount.ToList();
            wc.Sort(AscendingComparison);

            foreach (var element in wc)
            {
                Console.WriteLine("{0}: {1}", element.Key, element.Value);
            }
            s1.Stop();
            Console.WriteLine("Time spent: {0} ms", s1.ElapsedMilliseconds);
        }

        // Source() provides the source data on which the MapReduce query shall run. 
        private static IEnumerable<string> Source(string path)
        {
            return File.ReadAllLines(path);
        }


        // Map() returns the key which the word fits
        private static string Map(string word)
        {
            string theKey = word.Split(new char[] { ',' }).First();
            return theKey;
        }

        // Reduce() returns a list of Key/value pairs representing the results
        // An IGrouping<> represents a set of values that have the same key
        private static IEnumerable<KeyValuePair<string, int>> Reduce(IGrouping<string, string> group)
        {
            int sum = 0;

            sum = group.Sum<string>(e =>
            {
                int val = int.Parse(e.Split(new char[] { ',' }).ElementAt(1));
                return val;
            });

            return new KeyValuePair<string, int>[]
            {
              new KeyValuePair<string, int>(group.Key, sum)
            };
        }

        private static int AscendingComparison(KeyValuePair<string, int> a, KeyValuePair<string, int> b)
        {
            return b.Value - a.Value;
        }
    }
}
