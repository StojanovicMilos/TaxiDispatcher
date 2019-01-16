using System;
using TaxiDispatcher.Abstractions.Interfaces;
using TaxiDispatcher.Client.Logging;

namespace TaxiDispatcher.Tests.HelperClasses
{
    public class TestLogger : ILogger
    {
        public string AllMessages { get; set; }

        public void WriteLine(string message)
        {
            AllMessages += message + Environment.NewLine;
        }
    }
}
