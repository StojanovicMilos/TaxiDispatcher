﻿using System;
using TaxiDispatcher.BL.Interfaces;

namespace TaxiDispatcher.Tests.HelperClasses
{
    public class TestLogger : ILogger
    {
        public string AllMessages { get; private set; } = string.Empty;

        public void WriteLine(string message)
        {
            AllMessages += message + Environment.NewLine;
        }
    }
}
