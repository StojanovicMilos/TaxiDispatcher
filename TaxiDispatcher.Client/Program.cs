using System;
using TaxiDispatcher.BL;
using TaxiDispatcher.BL.Taxis;
using TaxiDispatcher.Client.Logging;
using TaxiDispatcher.DAL;

namespace TaxiDispatcher.Client
{
    public class Program
    {
        static void Main()
        {
            var logger = new ConsoleLogger();
            var taxiContext = new TaxiContext(InMemoryDataBase.Instance);
            var scheduler = new Scheduler(InMemoryDataBase.Instance);

            new TaxiDispatcherClient(logger, scheduler, taxiContext).Run();
            Console.ReadLine();
        }
    }
}
