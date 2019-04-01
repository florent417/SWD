using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Threading
{
    class Program
    {
        static void Main(string[] args)
        {
            HelloWriter writer1 = new HelloWriter()
            {
                Name="Flow",
                SleepTime = 200
                //NumberOfIteratons = 50
            };
            HelloWriter writer2 = new HelloWriter()
            {
                Name = "Test",
                SleepTime = 500
                //NumberOfIteratons = 10
            };

            Thread writer1_t = new Thread(() => writer1.SayHello(20));
            Thread writer2_t = new Thread(() => writer2.SayHello(20));
            Thread neverending_t = new Thread(NeverEndingStoryThread);

            writer1_t.Start();
            writer2_t.Start();
            neverending_t.Start();

            writer1_t.Join();
            writer2_t.Join();
            ShallStop = true;
            Console.WriteLine("Hello from main");
        }

        static void NeverEndingStoryThread()
        {
            while (!ShallStop)
            {
                Console.WriteLine("Never ending story");
                Thread.Sleep(5000);
            }
        }

        static bool ShallStop;
    }
}
