using System;
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
            try
            {
                ILogger logger = new ConsoleLogger();
                IDatabase database = InMemoryDataBase.Instance;
                IScheduler scheduler = new LoggingScheduler(logger, new Scheduler(database));
                TaxiEarningsLogger taxiEarningsLogger = new TaxiEarningsLogger(logger, database);
                ConfigureClient(scheduler, taxiEarningsLogger);
                RunClient();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                Console.ReadLine();
            }
        }

        public static void ConfigureClient(IScheduler scheduler, TaxiEarningsLogger taxiEarningsLogger)
        {
            _taxiEarningsLogger = taxiEarningsLogger ?? throw new ArgumentNullException(nameof(taxiEarningsLogger));
            _scheduler = scheduler ?? throw new ArgumentNullException(nameof(scheduler));
        }

        public static void RunClient()
        {
            CheckIfClientIsConfigured();
            PerformRides();
            LogTotalEarningsForTaxiWithId(2);
        }

        private static void CheckIfClientIsConfigured()
        {
            if (_scheduler == null || _taxiEarningsLogger == null)
                throw new Exception("Client is not configured!");
        }

        private static void PerformRides()
        {
            foreach (var rideOrder in RideOrders)
            {
                PerformRide(rideOrder);
            }
        }

        private static void PerformRide(RideOrder rideOrder)
        {
            if (rideOrder == null) throw new ArgumentNullException(nameof(rideOrder));
            RideOrderResult rideOrderResult = _scheduler.OrderRide(rideOrder);
            if (rideOrderResult.Success)
                _scheduler.AcceptRide(rideOrderResult.Ride);
        }

        private static void LogTotalEarningsForTaxiWithId(int id) => _taxiEarningsLogger.LogTotalEarningsForTaxiWithId(id);
    }
}
