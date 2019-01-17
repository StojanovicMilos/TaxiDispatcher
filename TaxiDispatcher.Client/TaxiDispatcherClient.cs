using System;
using TaxiDispatcher.BL;
using TaxiDispatcher.BL.CustomExceptions;
using TaxiDispatcher.BL.Locations;
using TaxiDispatcher.BL.Rides;
using TaxiDispatcher.BL.Taxis;
using TaxiDispatcher.Client.UIDTO;
using ILogger = TaxiDispatcher.Client.Logging.ILogger;

namespace TaxiDispatcher.Client
{
    public class TaxiDispatcherClient
    {
        private readonly ILogger _logger;
        private readonly TaxiContext _taxiContext;
        private readonly Scheduler _scheduler;
        private readonly RideOrder[] _rideOrders =
        {
            new RideOrder(startLocation: new Location(5), destinationLocation: new Location(0), rideDateTime: new DateTime(2018, 1, 1, 23, 0, 0)),
            new RideOrder(startLocation: new Location(0), destinationLocation: new Location(12), rideDateTime: new DateTime(2018, 1, 1, 9, 0, 0)),
            new RideOrder(startLocation: new Location(5), destinationLocation: new Location(0), rideDateTime: new DateTime(2018, 1, 1, 11, 0, 0)),
            new RideOrder(startLocation: new Location(35), destinationLocation: new Location(12), rideDateTime: new DateTime(2018, 1, 1, 11, 0, 0))
        };

        public TaxiDispatcherClient(ILogger logger, Scheduler scheduler, TaxiContext taxiContext)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _taxiContext = taxiContext ?? throw new ArgumentNullException(nameof(taxiContext));
            _scheduler = scheduler ?? throw new ArgumentNullException(nameof(scheduler));
        }

        public void Run()
        {
            PerformRides();
            UITaxiDTO taxiWithEarnings = new UITaxiDTO(GetTaxiWithEarningsById(2));
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
            if (rideOrder == null) throw new ArgumentNullException(nameof(rideOrder));
            _logger.WriteLine($"Ordering ride from {rideOrder.StartLocation} to {rideOrder.DestinationLocation}...");
            var ride = _scheduler.OrderRide(rideOrder);
            _logger.WriteLine("Ride ordered, price: " + ride.Price);
            var taxi = _scheduler.AcceptRide(ride);
            _logger.WriteLine("Ride accepted, waiting for driver: " + taxi.TaxiDriverName + Environment.NewLine);
        }

        private Taxi GetTaxiWithEarningsById(int id) => _taxiContext.GetTaxiWithEarningsById(id);

        private void LogTaxiWithEarnings(UITaxiDTO taxiWithEarnings)
        {
            if (taxiWithEarnings == null) throw new ArgumentNullException(nameof(taxiWithEarnings));
            _logger.WriteLine($"Driver with ID = {taxiWithEarnings.TaxiDriverId} earned today:");
            foreach (var ride in taxiWithEarnings.Rides)
            {
                _logger.WriteLine("Price: " + ride.Price);
            }
            _logger.WriteLine("Total: " + taxiWithEarnings.TotalEarnings);
        }
    }
}