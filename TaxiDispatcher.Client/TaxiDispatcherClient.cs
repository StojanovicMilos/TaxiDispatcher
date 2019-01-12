using System;
using TaxiDispatcher.App;
using TaxiDispatcher.App.CustomExceptions;
using TaxiDispatcher.DAL;
using TaxiDispatcher.DTO;

namespace TaxiDispatcher.Client
{
    public class TaxiDispatcherClient
    {
        private readonly ILogger _logger;
        private readonly Scheduler _scheduler = new Scheduler();
        private readonly RideOrder[] _rideOrders = new RideOrder[]
            {
                new RideOrder { StartLocation = new Location { CoordinateX = 5 }, DestinationLocation = new Location { CoordinateX = 0 }, RideType = RideType.City, RideDateTime = new DateTime(2018, 1, 1, 23, 0, 0) },
                new RideOrder { StartLocation = new Location { CoordinateX = 0 }, DestinationLocation = new Location { CoordinateX = 12 }, RideType = RideType.InterCity, RideDateTime = new DateTime(2018, 1, 1, 9, 0, 0) },
                new RideOrder { StartLocation = new Location { CoordinateX = 5 }, DestinationLocation = new Location { CoordinateX = 0 }, RideType = RideType.City, RideDateTime = new DateTime(2018, 1, 1, 11, 0, 0) },
                new RideOrder { StartLocation = new Location { CoordinateX = 35 }, DestinationLocation = new Location { CoordinateX = 12 }, RideType = RideType.City, RideDateTime = new DateTime(2018, 1, 1, 11, 0, 0) }
            };

        public TaxiDispatcherClient() : this(new Logger()) { }

        public TaxiDispatcherClient(ILogger logger)
        {
            _logger = logger;
        }

        public void Run()
        {
            PerformRides();
            TaxiDTO taxiWithEarnings = GetTaxiWithEarningsById(2);
            LogTaxiWithEarnings(taxiWithEarnings);
        }

        private void PerformRides()
        {
            foreach (var rideOrder in _rideOrders)
            {
                try
                {
                    PerformRide(rideOrder);
                }
                catch (NoAvailableTaxiVehiclesException e)
                {
                    _logger.WriteLine(e.Message + Environment.NewLine);
                }
            }
        }

        private void PerformRide(RideOrder rideOrder)
        {
            _logger.WriteLine(string.Format("Ordering ride from {0} to {1}...", rideOrder.StartLocation, rideOrder.DestinationLocation));
            var ride = _scheduler.OrderRide(rideOrder);
            _scheduler.AcceptRide(ride);
            _logger.WriteLine("");
        }

        private TaxiDTO GetTaxiWithEarningsById(int id) => new TaxiContext().GetTaxiWithEarningsById(id);

        private void LogTaxiWithEarnings(TaxiDTO taxiWithEarnings)
        {
            _logger.WriteLine(string.Format("Driver with ID = {0} earned today:", taxiWithEarnings.TaxiDriverId));
            foreach (var ride in taxiWithEarnings.Rides)
            {
                _logger.WriteLine("Price: " + ride.Price);
            }
            _logger.WriteLine("Total: " + taxiWithEarnings.TotalEarnings);
        }
    }
}