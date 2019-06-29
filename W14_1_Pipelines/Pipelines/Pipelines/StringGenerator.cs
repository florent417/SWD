using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pipelines
{
    class StringGenerator
    {
        public List<string> Stringlist { get; set; }
        private const string _chars = "ABC";
        public List<string> CompressedStrings { get; set; }
        
        public StringGenerator(int nbrStr, int nbrChars)
        {
            Stringlist = new List<string>();

            CompressedStrings = new List<string>();

            var random = new Random();

            for (int i = 0; i < nbrStr; i++)
            {
                var str = new char[nbrChars];
                for (int j = 0; j < nbrChars; j++)
                {
                    str[j] = _chars[random.Next((_chars.Length))];
                }

                Stringlist.Add(new string(str));
            }
        }

        public void CompressStrings()
        {
            StringBuilder builder = new StringBuilder();
            int occurences = 1;
            char compare = ' ';
            foreach (var str in Stringlist)
            {
                builder.Clear();
                for (int i = 0; i < str.Length; i++)
                {
                    if (str[i].Equals(compare))
                    {
                        ++occurences;
                        if (i == str.Length -1)
                        {
                            builder.Append(occurences.ToString());
                        }
                    }
                    else if (occurences > 1 && !str[i].Equals(compare))
                    {
                        builder.Append(occurences.ToString());
                        occurences = 1;
                        builder.Append(str[i]);
                    }
                    else
                    {
                        builder.Append(str[i]);
                    }
                    compare = str[i];

                }

                compare = ' ';
                CompressedStrings.Add(builder.ToString());
            }
        }

        public double CalculateCompressionsOfStrings()
        {
            double compression = 0.0;

            for (int i = 0; i < Stringlist.Count; i++)
            {
                compression = (double) Stringlist[i].Length / (double) CompressedStrings[i].Length;
            }
            
            return compression / (double) Stringlist.Count;
        }

        public void PrintList()
        {
            foreach (var item in Stringlist)
            {
                Console.WriteLine($"{item}");
            }
        }

        public void PrintCompressedList()
        {
            foreach (var item in CompressedStrings)
            {
                Console.WriteLine($"{item}");
            }
        }
    }
}
