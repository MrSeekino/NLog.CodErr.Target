using System.Collections.Generic;
using System.Threading;

namespace NLog.CodErr.Target.Examples.Runner
{
    class Program
    {
        static Logger Log => LogManager.GetCurrentClassLogger();

        static void Main(string[] args)
        {
            Log.Info("Starting Example Runner");

            List<ExampleWorker> workerList = new List<ExampleWorker>();
            for (int i = 0; i < 2; i++)
            {
                ExampleWorker exampleWorker = new ExampleWorker(i);
                exampleWorker.Start();

                workerList.Add(exampleWorker);
            }

            foreach (var worker in workerList)
                worker.Stop();

            Log.Info("Example Runner has finished");
            Thread.Sleep(2000);
        }

        private void ThreadWork(int threadNumber)
        {
            for (int i = 0; i < 10; i++)
            {
                Log.Debug($"{threadNumber} Operation {i + 1}");
                Thread.Sleep(100);
            }
        }
    }
}
