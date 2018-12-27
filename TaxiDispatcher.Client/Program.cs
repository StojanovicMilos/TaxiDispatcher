using System;

namespace TaxiDispatcher.Client
{
    public class Program
    {
        static void Main(string[] args)
        {
            new TaxiDispatcherClient().Run();
            Console.ReadLine();
        }
    }
}
