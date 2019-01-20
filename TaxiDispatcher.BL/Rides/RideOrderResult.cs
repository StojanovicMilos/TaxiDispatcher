using System;

namespace TaxiDispatcher.BL.Rides
{
    public class RideOrderResult
    {
        public Ride Ride { get; }
        public string ErrorMessage { get; }

        public bool Success => ErrorMessage.Length == 0;

        public RideOrderResult(Ride ride)
        {
            Ride = ride ?? throw new ArgumentNullException(nameof(ride));
            ErrorMessage = string.Empty;
        }

        public RideOrderResult(string errorMessage)
        {
            Ride = null;
            ErrorMessage = errorMessage ?? throw new ArgumentNullException(nameof(errorMessage));
        }
    }
}
