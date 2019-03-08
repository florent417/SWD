using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L05_01_Template_And_Strategy_method
{
    class RandomArrayGenerator : IArrayGenerator
    {
        private int _max = 0;
        private int _size = 0;
        private int _seed = 0;
        public int[] RandomNumberArray { get; private set; }
        private Random rnd;

        public int[] ArrayGenerator()
        {
            RandomNumberArray = new int[_size];
            rnd = new Random(_seed);

            for (int i = 0; i < _size; i++)
            {
                RandomNumberArray[i] = rnd.Next(_max);
            }
            return RandomNumberArray;
        }
        RandomArrayGenerator(int max, int size, int seed)
        {
            _max = max;
            _size = size;
            _seed = seed;
        }
    }
}
