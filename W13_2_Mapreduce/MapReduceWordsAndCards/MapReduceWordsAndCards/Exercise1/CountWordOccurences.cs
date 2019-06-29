using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MapReduceDemo
{
    internal static class CountWordOccurences
    {
        //*********************************************************
        //*********************************************************
        // Count number of occurences of each word
        //*********************************************************
        //*********************************************************
        internal static void Count()
        {
            Console.WriteLine("Counting number of occurences of each word.");
            var files =
                Directory.EnumerateFiles(@"C:..\..\..\Books", "*.txt")
                    .AsParallel();

            var wordCount = files.MapReduce(
                path => Source(path),
                map => Map(map),
                group => Reduce(group));

            var wc = wordCount.ToList();

            //find the top 20 used words
            wc.Sort(AscendingComparison);
            for (int i = 0; i < 20; i++)
            {
                Console.WriteLine("{0}: {1}", wc[i].Key, wc[i].Value);
            }
        }

        //Source() provides the source data on which the MapReduce query shall run.
        private static IEnumerable<string> Source(string path)
        {
            return File.ReadLines(path) // Read all lines in the path
                .SelectMany(line => line.ToLower().Split(new char[] { ' ', ',', '.', '-', '!', '?', ';' })); // Project the words into a single enumerable
        }


        //Map() returns the key which the word fits
        private static string Map(string word)
        {
            return word;
        }

        //Reduce() returns a list of Key/value pairs representing the results
        //An IGrouping<> represents a set of values that have the same key
        private static IEnumerable<KeyValuePair<string, int>> Reduce(IGrouping<string, string> group)
        {
            return new KeyValuePair<string, int>[]
            {
                new KeyValuePair<string, int>(group.Key, group.Count())
            };
        }


        private static int AscendingComparison(KeyValuePair<string, int> a, KeyValuePair<string, int> b)
        {
            return b.Value - a.Value;
        }
    }
}
