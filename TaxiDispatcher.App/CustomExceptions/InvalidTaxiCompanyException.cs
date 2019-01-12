using System;

namespace TaxiDispatcher.App.CustomExceptions
{
    public class InvalidTaxiCompanyException : Exception
    {
        public InvalidTaxiCompanyException(string taxiCompanyName)
            : base(string.Format("Taxi company {0} doesn't exist.", taxiCompanyName))
        {
        }
    }
}
