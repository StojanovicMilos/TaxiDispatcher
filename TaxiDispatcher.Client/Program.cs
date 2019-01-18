using System;
using TaxiDispatcher.BL;
using TaxiDispatcher.BL.CustomExceptions;
using TaxiDispatcher.BL.Locations;
using TaxiDispatcher.BL.Rides;
using TaxiDispatcher.BL.Taxis;
using TaxiDispatcher.Client.Logging;
using TaxiDispatcher.Client.UIDTO;
using TaxiDispatcher.DAL;

namespace TaxiDispatcher.Client
{
    public class Program
    {
        private static ILogger _logger;
        private static TaxiContext _taxiContext;
        private static Scheduler _scheduler;

        private static readonly RideOrder[] RideOrders =
        {
            new RideOrder(startLocation: new Location(5), destinationLocation: new Location(0), rideDateTime: new DateTime(2018, 1, 1, 23, 0, 0)),
            new RideOrder(startLocation: new Location(0), destinationLocation: new Location(12), rideDateTime: new DateTime(2018, 1, 1, 9, 0, 0)),
            new RideOrder(startLocation: new Location(5), destinationLocation: new Location(0), rideDateTime: new DateTime(2018, 1, 1, 11, 0, 0)),
            new RideOrder(startLocation: new Location(35), destinationLocation: new Location(12), rideDateTime: new DateTime(2018, 1, 1, 11, 0, 0))
        };

        static void Main()
        {
            ConfigureClient(new ConsoleLogger(), new Scheduler(InMemoryDataBase.Instance), new TaxiContext(InMemoryDataBase.Instance));
            RunClient();
            Console.ReadLine();
        }

        public static void ConfigureClient(ILogger logger, Scheduler scheduler, TaxiContext taxiContext)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _taxiContext = taxiContext ?? throw new ArgumentNullException(nameof(taxiContext));
            _scheduler = scheduler ?? throw new ArgumentNullException(nameof(scheduler));
        }

        public static void RunClient()
        {
            PerformRides();
            UITaxiDTO taxiWithEarnings = new UITaxiDTO(GetTaxiWithEarningsById(2));
            LogTaxiWithEarnings(taxiWithEarnings);
        }

        private static void PerformRides()
        {
            foreach (var rideOrder in RideOrders)
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

        private static void PerformRide(RideOrder rideOrder)
        {
            if (rideOrder == null) throw new ArgumentNullException(nameof(rideOrder));
            _logger.WriteLine($"Ordering ride from {rideOrder.StartLocation} to {rideOrder.DestinationLocation}...");
            var ride = _scheduler.OrderRide(rideOrder);
            _logger.WriteLine("Ride ordered, price: " + ride.Price);
            var taxi = _scheduler.AcceptRide(ride);
            _logger.WriteLine("Ride accepted, waiting for driver: " + taxi.TaxiDriverName + Environment.NewLine);
        }

        private static Taxi GetTaxiWithEarningsById(int id) => _taxiContext.GetTaxiWithEarningsById(id);

        private static void LogTaxiWithEarnings(UITaxiDTO taxiWithEarnings)
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
