using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L05_01_Template_And_Strategy_method
{
    class BubbleSort : ISort
    {
        public int[] SortArray(int[] unsortedArray)
        {
            foreach (var element in unsortedArray)
            {
                for (var i = 0; i < unsortedArray.Length-2; i++)
                {
                    if(unsortedArray[i+1] < unsortedArray[i])
                    {
                        var higherNumber = unsortedArray[i];

                        unsortedArray[i] = unsortedArray[i + 1];
                        unsortedArray[i + 1] = higherNumber;
                    }
                }
            }

            return unsortedArray;
        }
    }
}
