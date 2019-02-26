using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L05_01_Template_And_Strategy_method
{
    class Program
    {
        static void Main(string[] args)
        {
            ArrayGenerator testArray = new ArrayGenerator(10,60,8);

            foreach (var element in testArray.RandomNumberArray)
            {
                Console.WriteLine("{0}",element);
            }
        }
    }
}
