using System;

namespace TaxiDispatcher.BL.CustomExceptions
{
    public class InvalidTaxiCompanyException : Exception
    {
        public InvalidTaxiCompanyException(string taxiCompanyName)
            : base(string.Format("Taxi company {0} doesn't exist.", taxiCompanyName))
        {
        }
    }
}
