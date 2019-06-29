using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace MapReduceDemo
{
    internal static class CountCardsSequentially
    {
        internal static void Count()
        {
            Console.WriteLine("Counting cards, sequentially");

            Stopwatch s1 = new Stopwatch();
            s1.Start();

            var files =
                Directory.EnumerateFiles(@"..\..\..\cards", "*.txt")
                    .AsParallel();

            List<string> allLines = new List<string>();
            foreach (var mf in files)
            {
                var lines = File.ReadAllLines(mf);
                allLines.AddRange(lines);
            }

            long spades = 0;
            long hearts = 0;
            long clubs = 0;
            long diamonds = 0;

            foreach (var line in allLines)
            {
                string[] words = line.Split(new char[] { ',' });
                string key = words[0];
                int value = int.Parse(words[1]);

                if (key.Equals("SPADE"))
                {
                    spades += value;
                }
                else if (key.Equals("HEART"))
                {
                    hearts += value;
                }
                else if (key.Equals("CLUB"))
                {
                    clubs += value;
                }
                else if (key.Equals("DIAMOND"))
                {
                    diamonds += value;
                }
            }

            Console.WriteLine("{0}: {1}", "SPADES", spades);
            Console.WriteLine("{0}: {1}", "HEARTS", hearts);
            Console.WriteLine("{0}: {1}", "CLUBS", clubs);
            Console.WriteLine("{0}: {1}", "DIAMONDS", diamonds);
            s1.Stop();
            Console.WriteLine("Time spent: {0} ms", s1.ElapsedMilliseconds);
        }
    }
}
