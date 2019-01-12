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
        private readonly TaxiContext _taxiContext;
        private readonly Scheduler _scheduler;
        private readonly RideOrder[] _rideOrders = new RideOrder[]
            {
                new RideOrder { StartLocation = new Location(5), DestinationLocation = new Location(0), RideType = RideType.City, RideDateTime = new DateTime(2018, 1, 1, 23, 0, 0) },
                new RideOrder { StartLocation = new Location(0), DestinationLocation = new Location(12), RideType = RideType.InterCity, RideDateTime = new DateTime(2018, 1, 1, 9, 0, 0) },
                new RideOrder { StartLocation = new Location(5), DestinationLocation = new Location(0), RideType = RideType.City, RideDateTime = new DateTime(2018, 1, 1, 11, 0, 0) },
                new RideOrder { StartLocation = new Location(35), DestinationLocation = new Location(12), RideType = RideType.City, RideDateTime = new DateTime(2018, 1, 1, 11, 0, 0) }
            };

        public TaxiDispatcherClient()
        {
            _logger = new Logger();
            _taxiContext = new TaxiContext();
            _scheduler = new Scheduler();
        }

        public TaxiDispatcherClient(ILogger logger, IDatabase database)
        {
            _logger = logger;
            _taxiContext = new TaxiContext(database);
            _scheduler = new Scheduler(database);
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
            _logger.WriteLine("Ride ordered, price: " + ride.Price.ToString());
            _scheduler.AcceptRide(ride);
            Console.WriteLine("Ride accepted, waiting for driver: " + ride.RideTaxi.TaxiDriverName + Environment.NewLine);
        }

        private TaxiDTO GetTaxiWithEarningsById(int id) => _taxiContext.GetTaxiWithEarningsById(id);

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