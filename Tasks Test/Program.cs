using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Tasks_Test
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            Task.Run(LongTask);
            Task.Run(PeriodicToggler);

            while (true)
            {

            }

        }

        static async Task LongTask()
        {
            long j = 0;
            for (long i = 0; i < 200E7; i++)
            {
                j++;
            }
            Console.WriteLine("Done Long task");
        }

        static async Task PeriodicToggler()
        {
            bool b = false;
            var stopwatch = new Stopwatch();

            stopwatch.Start();

            while (true)
            {
                if (stopwatch.ElapsedMilliseconds > 1000)
                {
                    stopwatch.Restart();
                    Console.WriteLine($"{b}");
                    b = !b;
                }


            }
        }


    }
}
