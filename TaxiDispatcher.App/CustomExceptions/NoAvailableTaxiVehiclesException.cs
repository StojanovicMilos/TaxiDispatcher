using System;

namespace TaxiDispatcher.BL.CustomExceptions
{
    public class NoAvailableTaxiVehiclesException : Exception
    {
        public NoAvailableTaxiVehiclesException()
            : base("There are no available taxi vehicles!")
        {
        }
    }
}
