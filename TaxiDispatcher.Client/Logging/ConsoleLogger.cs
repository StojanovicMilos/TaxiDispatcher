using System;
using TaxiDispatcher.BL.Interfaces;

namespace TaxiDispatcher.Client.Logging
{
    public class ConsoleLogger : ILogger
    {
        public void WriteLine(string message)
        {
            if (message == null) throw new ArgumentNullException(nameof(message));
            Console.WriteLine(message);
        }
    }
}