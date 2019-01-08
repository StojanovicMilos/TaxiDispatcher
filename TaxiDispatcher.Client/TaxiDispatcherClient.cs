using System;
using TaxiDispatcher.App;
using TaxiDispatcher.App.CustomExceptions;
using TaxiDispatcher.DAL;

namespace TaxiDispatcher.Client
{
    public class TaxiDispatcherClient
    {
        private readonly ILogger _logger;
        private readonly Scheduler _scheduler = new Scheduler();
        private readonly RideOrder[] _rideOrders = new RideOrder[]
            {
                new RideOrder {StartLocation = new Location { CoordinateX= 5 }, DestinationLocation = new Location { CoordinateX = 0 }, RideType = RideType.City, RideDateTime =  new DateTime(2018, 1, 1, 23, 0, 0)},
                new RideOrder {StartLocation = new Location { CoordinateX= 0 }, DestinationLocation = new Location { CoordinateX = 12 }, RideType = RideType.InterCity, RideDateTime =  new DateTime(2018, 1, 1, 9, 0, 0)},
                new RideOrder {StartLocation = new Location { CoordinateX= 5 }, DestinationLocation = new Location { CoordinateX = 0 }, RideType = RideType.City, RideDateTime =  new DateTime(2018, 1, 1, 11, 0, 0)},
                new RideOrder {StartLocation = new Location { CoordinateX= 35 }, DestinationLocation = new Location { CoordinateX = 12 }, RideType = RideType.City, RideDateTime =  new DateTime(2018, 1, 1, 11, 0, 0)}
            };

        public TaxiDispatcherClient() : this(new Logger()) { }

        public TaxiDispatcherClient(ILogger logger)
        {
            _logger = logger;
        }

        public void Run()
        {
            OrderRides();
            const int TotalEarningsCalculationDriverId = 2;
            int totalEarnings = CalculateTotalEarningsForDriver(TotalEarningsCalculationDriverId);
            LogTotalEarnings(totalEarnings);
        }

        private void OrderRides()
        {
            foreach (var rideOrder in _rideOrders)
            {
                try
                {
                    OrderRide(rideOrder);
                }
                catch (NoAvailableTaxiVehiclesException e)
                {
                    _logger.WriteLine(e.Message + Environment.NewLine);
                }
            }
        }

        private void OrderRide(RideOrder rideOrder)
        {
            _logger.WriteLine(string.Format("Ordering ride from {0} to {1}...", rideOrder.StartLocation, rideOrder.DestinationLocation));
            var ride = _scheduler.OrderRide(rideOrder);
            _scheduler.AcceptRide(ride);
            _logger.WriteLine("");
        }

        private int CalculateTotalEarningsForDriver(int driverId)
        {
            _logger.WriteLine(string.Format("Driver with ID = {0} earned today:", driverId));
            int total = 0;
            foreach (Ride r in _scheduler.GetRideList(driverId))
            {
                total += r.Price;
                _logger.WriteLine("Price: " + r.Price);
            }
            return total;
        }

        private void LogTotalEarnings(int total)
        {
            _logger.WriteLine("Total: " + total);
        }
    }
}