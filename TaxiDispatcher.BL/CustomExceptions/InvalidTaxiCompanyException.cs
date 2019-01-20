using System;

namespace TaxiDispatcher.BL.CustomExceptions
{
    public class InvalidTaxiCompanyException : Exception
    {
        public InvalidTaxiCompanyException(string taxiCompanyName)
            : base($"Taxi company {taxiCompanyName} doesn't exist.")
        {
        }
    }
}
