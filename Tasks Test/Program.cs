using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Tasks_Test
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

            var longTask = Task.Run(() => LongTask(cancellationTokenSource.Token));
            var periodicToggler = Task.Run(() => PeriodicToggler(cancellationTokenSource.Token));
            var exitListener = Task.Run(() => ExitListener(cancellationTokenSource));

            while (true)
            {
            
                //Check to see if the cancellation has been requested
                if (cancellationTokenSource.Token.IsCancellationRequested)
                {
                    return;
                }
            }

        }

        static async Task ExitListener(CancellationTokenSource cancellationTokenSource)
        {
            ConsoleKeyInfo cki;
            do
            {
                cki = Console.ReadKey();
            }
            while (cki.Key != ConsoleKey.Escape);
            cancellationTokenSource.Cancel();

        }

        static async Task LongTask(CancellationToken cancellationToken)
        {
            long j = 0;
            for (long i = 0; i < 200E7; i++)
            {
                
                if (cancellationToken.IsCancellationRequested)
                {
                    return;
                }
               
                j++;
            }
            Console.WriteLine("Done Long task");
        }

        static async Task PeriodicToggler(CancellationToken cancellationToken)
        {
            bool b = false;
            var stopwatch = new Stopwatch();

            stopwatch.Start();

            while (true)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    return;
                }

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
