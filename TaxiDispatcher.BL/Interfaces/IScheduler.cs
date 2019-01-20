using TaxiDispatcher.BL.Rides;
using TaxiDispatcher.BL.Taxis;

namespace TaxiDispatcher.BL.Interfaces
{
    public interface IScheduler
    {
        Ride OrderRide(RideOrder rideOrder);
        Taxi AcceptRide(Ride ride);
    }
}
