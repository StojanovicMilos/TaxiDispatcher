using System;
using TaxiDispatcher.Client;

namespace TaxiDispatcher.Tests.Logger
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
