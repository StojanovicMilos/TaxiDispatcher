using TaxiDispatcher.BL.Rides;
using TaxiDispatcher.BL.Taxis;

namespace TaxiDispatcher.BL.Interfaces
{
    public interface IScheduler
    {
        RideOrderResult OrderRide(RideOrder rideOrder);
        Taxi AcceptRide(Ride ride);
    }
}
