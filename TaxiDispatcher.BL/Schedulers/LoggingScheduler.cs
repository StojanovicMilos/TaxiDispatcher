using System;
using TaxiDispatcher.BL.Interfaces;
using TaxiDispatcher.BL.Rides;
using TaxiDispatcher.BL.Taxis;

namespace TaxiDispatcher.BL.Schedulers
{
    public class LoggingScheduler : IScheduler
    {
        private readonly ILogger _logger;
        private readonly IScheduler _scheduler;

        public LoggingScheduler(ILogger logger, IScheduler scheduler)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _scheduler = scheduler ?? throw new ArgumentNullException(nameof(scheduler));
        }

        public Ride OrderRide(RideOrder rideOrder)
        {
            _logger.WriteLine($"Ordering ride from {rideOrder.StartLocation} to {rideOrder.DestinationLocation}...");
            var ride = _scheduler.OrderRide(rideOrder);
            _logger.WriteLine("Ride ordered, price: " + ride.Price);
            return ride;
        }

        public Taxi AcceptRide(Ride ride)
        {
            var taxi = _scheduler.AcceptRide(ride);
            _logger.WriteLine("Ride accepted, waiting for driver: " + taxi.DriverName + Environment.NewLine);
            return taxi;
        }
    }
}
