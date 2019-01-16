﻿using System;
using TaxiDispatcher.Abstractions.Interfaces;

namespace TaxiDispatcher.Client.Logging
{
    public class ConsoleLogger : ILogger
    {
        public void WriteLine(string message)
        {
            Console.WriteLine(message);
        }
    }
}