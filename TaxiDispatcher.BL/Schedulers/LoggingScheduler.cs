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

        public RideOrderResult OrderRide(RideOrder rideOrder)
        {
            if (rideOrder == null) throw new ArgumentNullException(nameof(rideOrder));
            _logger.WriteLine($"Ordering ride from {rideOrder.StartLocation} to {rideOrder.DestinationLocation}...");
            RideOrderResult rideOrderResult = _scheduler.OrderRide(rideOrder);
            if (rideOrderResult.Success)
            {
                _logger.WriteLine("Ride ordered, price: " + rideOrderResult.Ride.Price);
            }
            else
            {
                _logger.WriteLine(rideOrderResult.ErrorMessage);
            }
            return rideOrderResult;
        }

        public Taxi AcceptRide(Ride ride)
        {
            if (ride == null) throw new ArgumentNullException(nameof(ride));
            var taxi = _scheduler.AcceptRide(ride);
            _logger.WriteLine("Ride accepted, waiting for driver: " + taxi.DriverName + Environment.NewLine);
            return taxi;
        }
    }
}
