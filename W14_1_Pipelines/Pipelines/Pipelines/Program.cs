using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pipelines
{
    class Program
    {
        static void Main(string[] args)
        {
            
            SequentialStringCompression s1 = new SequentialStringCompression("ABC",1000,250);
            var watch = System.Diagnostics.Stopwatch.StartNew();
            s1.Run();
            watch.Stop();
            var elapsedTime = watch.ElapsedMilliseconds;
            Console.WriteLine("Elapsed time in milliseconds (sequential): {0}",elapsedTime);


        }
    }
}
