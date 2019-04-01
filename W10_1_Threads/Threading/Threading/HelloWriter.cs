using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Threading
{
    class HelloWriter
    {
        public string Name { get; set; }
        public int NumberOfIteratons { get; set; }
        public int SleepTime { get; set; }

        public HelloWriter()
        {
        }

        public HelloWriter(int nrOfIterations)
        {
            NumberOfIteratons = nrOfIterations;
        }

        public void SayHello(object parameter)
        {
            int numberOfIterations = (int) parameter;
            for (int i = 0; i < numberOfIterations; i++)
            {
                Console.WriteLine($"Hello from {Name} #{i}");      
                Thread.Sleep(SleepTime);
            }
        }
    }
}
