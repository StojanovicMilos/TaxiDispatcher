using System;
using TaxiDispatcher.BL.CustomExceptions;
using TaxiDispatcher.BL.Interfaces;
using TaxiDispatcher.BL.Locations;
using TaxiDispatcher.BL.Rides;
using TaxiDispatcher.BL.Schedulers;
using TaxiDispatcher.BL.Taxis;
using TaxiDispatcher.Client.Logging;
using TaxiDispatcher.DAL;

namespace TaxiDispatcher.Client
{
    public class Program
    {
        private static ILogger _logger;
        private static TaxiEarningsLogger _taxiEarningsLogger;
        private static IScheduler _scheduler;

        private static readonly RideOrder[] RideOrders =
        {
            new RideOrder(startLocation: new Location(5), destinationLocation: new Location(0), rideDateTime: new DateTime(2018, 1, 1, 23, 0, 0)),
            new RideOrder(startLocation: new Location(0), destinationLocation: new Location(12), rideDateTime: new DateTime(2018, 1, 1, 9, 0, 0)),
            new RideOrder(startLocation: new Location(5), destinationLocation: new Location(0), rideDateTime: new DateTime(2018, 1, 1, 11, 0, 0)),
            new RideOrder(startLocation: new Location(35), destinationLocation: new Location(12), rideDateTime: new DateTime(2018, 1, 1, 11, 0, 0))
        };

        static void Main()
        {
            ILogger logger = new ConsoleLogger();
            IDatabase database = InMemoryDataBase.Instance;
            IScheduler scheduler = new LoggingScheduler(logger, new Scheduler(database));
            TaxiEarningsLogger taxiEarningsLogger = new TaxiEarningsLogger(database, logger);
            ConfigureClient(logger, scheduler, taxiEarningsLogger);
            RunClient();
            Console.ReadLine();
        }

        public static void ConfigureClient(ILogger logger, IScheduler scheduler, TaxiEarningsLogger taxiEarningsLogger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _taxiEarningsLogger = taxiEarningsLogger ?? throw new ArgumentNullException(nameof(taxiEarningsLogger));
            _scheduler = scheduler ?? throw new ArgumentNullException(nameof(scheduler));
        }

        public static void RunClient()
        {
            PerformRides();
            LogTotalEarningsForTaxiWithId(2);
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
            var ride = _scheduler.OrderRide(rideOrder);
            _scheduler.AcceptRide(ride);
        }

        private static void LogTotalEarningsForTaxiWithId(int id) => _taxiEarningsLogger.LogTotalEarningsForTaxiWithId(id);
    }
}
