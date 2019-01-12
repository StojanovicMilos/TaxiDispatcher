using System;

namespace TaxiDispatcher.Client.Logging
{
    public class Logger : ILogger
    {
        public void WriteLine(string message)
        {
            Console.WriteLine(message);
        }
    }
}