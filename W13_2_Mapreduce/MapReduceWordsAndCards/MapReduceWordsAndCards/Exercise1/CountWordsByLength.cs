using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MapReduceDemo
{
    internal static class CountWordsByLength
    {
        //*********************************************************
        //*********************************************************
        // WORD-COUNT-BY-LENGTH EXAMPLE
        //*********************************************************
        //*********************************************************
        internal static void Count()
        {
            Console.WriteLine("Counting words by length.");
            var files =
                Directory.EnumerateFiles(@"..\..\..\Books", "*.txt")
                    .AsParallel();

            var wordCount = files.MapReduce(
                path => Source(path),
                map => Map(map),
                group => Reduce(group));

            var wc = wordCount.ToList();
            wc.Sort(AscendingComparison);

            foreach (var pair in wc)
            {
                Console.WriteLine("{0}: {1}", pair.Key, pair.Value);
            }
        }

        // Source() provides the source data on which the MapReduce query shall run. 
        private static IEnumerable<string> Source(string path)
        {
            return File.ReadLines(path) // Read all lines in the path
                .SelectMany(line => line.ToLower().Split(new char[] { ' ', ',', '.', '-', '!', '?', ';' }));
            // Project the words into a single enumerable
        }


        // Map() returns the key which the word fits
        private static int Map(string word)
        {
            return word.Length;
        }


        // Reduce() returns a list of Key/value pairs representing the results
        // An IGrouping<> represents a set of values that have the same key
        private static IEnumerable<KeyValuePair<int, int>> Reduce(IGrouping<int, string> group)
        {
            return new KeyValuePair<int, int>[]
            {
                        new KeyValuePair<int, int>(group.Key, group.Count())
            };
        }


        private static int AscendingComparison(KeyValuePair<int, int> a, KeyValuePair<int, int> b)
        {
            return a.Key - b.Key;
        }
    }
}
