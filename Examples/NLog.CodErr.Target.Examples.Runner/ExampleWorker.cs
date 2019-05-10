using System.Threading;

namespace NLog.CodErr.Target.Examples.Runner
{
    public class ExampleWorker
    {
        Logger Log => LogManager.GetCurrentClassLogger();

        int WorkerId { get; set; }
        Thread Thread { get; set; }

        public ExampleWorker(int workerId)
        {
            WorkerId = workerId;
        }

        public void Start()
        {
            Thread = new Thread(WorkThread);
            Thread.Start();
        }

        public void Stop()
        {
            Thread.Join();
            Thread.Abort();
        }

        private void WorkThread()
        {
            for (int i = 0; i < 10; i++)
            {
                Log.Info($"Thread {WorkerId} Operation {i + 1}");
                Thread.Sleep(100);
            }
        }
    }
}
