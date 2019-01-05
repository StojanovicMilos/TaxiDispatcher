using System;

namespace TaxiDispatcher.App.CustomExceptions
{
    public class NoAvailableTaxiVehiclesException : Exception
    {
        public NoAvailableTaxiVehiclesException()
            : base("There are no available taxi vehicles!")
        {
        }
    }
}
