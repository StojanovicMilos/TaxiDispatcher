using System;

namespace TaxiDispatcher.Client
{
    public class Program
    {
        static void Main()
        {
            new TaxiDispatcherClient().Run();
            Console.ReadLine();
        }
    }
}
