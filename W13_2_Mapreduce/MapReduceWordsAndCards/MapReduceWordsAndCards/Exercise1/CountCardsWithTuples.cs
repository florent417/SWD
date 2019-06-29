using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace MapReduceDemo
{
    public static class CountCardsWithTuples
    {
        internal static void Count()
        {
            Console.WriteLine("Counting cards, this time with tuples.");
            var cards = Directory.EnumerateFiles(@"..\..\..\cards", "*.txt").AsParallel();
            var s1 = new Stopwatch();
            s1.Start();

            var cardCount = cards.MapReduce(
                path => ReadCardsFromFiles(path),
                map => MapCardToSuit(map),
                group => ReduceCardGroupings(group));

            foreach (var pair in cardCount)
            {
                Console.WriteLine("{0}: {1}", pair.Key, pair.Value);
            }
            s1.Stop();

            Console.WriteLine("Done! That took {0} ms", s1.ElapsedMilliseconds);

        }

        //*********************************************************
        // ReadCardsFromFiles() reads playing card definitions from files 
        static IEnumerable<Tuple<string, int>> ReadCardsFromFiles(string path)
        {
            var lines = File.ReadLines(path); // Read all lines in the path
            foreach (var line in lines)
            {
                var items = line.Split(',');
                yield return new Tuple<string, int>(items[0], int.Parse(items[1]));
            }
        }


        // MapCardToSuit() returns the suit of a given card
        static string MapCardToSuit(Tuple<string, int> card)
        {
            return card.Item1;
        }


        // ReduceCardGroupings() returns a list of Key/value pairs representing the results
        // An IGrouping<> represents a set of values that have the same key
        static IEnumerable<KeyValuePair<string, int>> ReduceCardGroupings(IGrouping<string, Tuple<string, int>> cardGrouping)
        {
            int sum = cardGrouping.Sum(tuple => tuple.Item2);

            var retVal = new KeyValuePair<string, int>[]
            {
                new KeyValuePair<string,int>(cardGrouping.Key, sum)
            };

            return retVal;
        }




    }
}
