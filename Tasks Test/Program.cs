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

           //Run the tasks, note as they are not called async the Task.Run method returns immediately so they all will start 
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

                //Put this thread to sleep as there is no need to constanly process
                Thread.Sleep(50);
            }
            while (cki.Key != ConsoleKey.Escape);
            
            //Escape key pressed, send the cancellation request to any tasks that might be cancellable
            cancellationTokenSource.Cancel();

        }

        static async Task LongTask(CancellationToken cancellationToken)
        {
            //Not doing anything here other than running up the CPU
            for (long i = 0; i < 200E7; i++)
            {
                //Check to see if the cancellation has been requested
                if (cancellationToken.IsCancellationRequested)
                {
                    return;
                }
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
                //Check to see if the cancellation has been requested
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
                
                //Put this thread to sleep as there is no need to constanly process
                Thread.Sleep(50);
            
            }
        }





    }
}
