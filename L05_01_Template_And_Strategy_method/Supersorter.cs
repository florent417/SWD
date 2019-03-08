using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace L05_01_Template_And_Strategy_method
{
    class Supersorter
    {
        private int[] _unsortedArray;
        private ISort _sortingStrategy;
        private Stopwatch stopWatch;
        Supersorter(int[] unsortedArray)
        {
            _unsortedArray = unsortedArray;
        }

        public void SetAlgrotihmStrategy(ISort sortingStrategy)
        {
            _sortingStrategy = sortingStrategy;
        }

        public int[] Sort(ISort sortingAlgorithm, out long milliSeconds)
        {
            var data = (int[])_unsortedArray.Clone();
            stopWatch = new Stopwatch();
            stopWatch.Start();
            var sorted = _sortingStrategy.SortArray(data);
            stopWatch.Stop();
            milliSeconds=stopWatch.ElapsedMilliseconds;

            return sorted;
        }

    }
}
