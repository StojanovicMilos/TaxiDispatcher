using System;
using TaxiDispatcher.App;

namespace TaxiDispatcher.Client
{
    public class Program
    {
        static void Main(string[] args)
        {
            RunTaxiDispatcher(new Logger());
            Console.ReadLine();
        }

        public static void RunTaxiDispatcher(ILogger logger)
        {
            Scheduler scheduler = new Scheduler();
            RideOrders[] rideOrders = GetRideOrders();
            OrderRides(logger, scheduler, rideOrders);
            const int TotalEarningsCalculationDriverId = 2;
            int totalEarnings = CalculateTotalEarnings(TotalEarningsCalculationDriverId, logger, scheduler);
            LogTotalEarnings(logger, totalEarnings);
        }

        private static RideOrders[] GetRideOrders()
        {
            return new RideOrders[]
            {
                new RideOrders {Start = 5, Destination = 0, RideType = Constants.City, RideDateTime =  new DateTime(2018, 1, 1, 23, 0, 0)},
                new RideOrders {Start = 0, Destination = 12, RideType = Constants.InterCity, RideDateTime =  new DateTime(2018, 1, 1, 9, 0, 0)},
                new RideOrders {Start = 5, Destination = 0, RideType = Constants.City, RideDateTime =  new DateTime(2018, 1, 1, 11, 0, 0)},
                new RideOrders {Start = 35, Destination = 12, RideType = Constants.City, RideDateTime =  new DateTime(2018, 1, 1, 11, 0, 0)}
            };
        }

        private static void OrderRides(ILogger logger, Scheduler scheduler, RideOrders[] rideOrders)
        {
            foreach (var rideOrder in rideOrders)
            {
                try
                {
                    OrderRide(logger, scheduler, rideOrder);
                }
                catch (Exception e)
                {
                    if (e.Message == "There are no available taxi vehicles!")
                    {
                        logger.WriteLine(e.Message);
                        logger.WriteLine("");
                    }
                    else
                        throw;
                }
            }
        }

        private static void OrderRide(ILogger logger, Scheduler scheduler, RideOrders rideOrder)
        {
            logger.WriteLine(string.Format("Ordering ride from {0} to {1}...", rideOrder.Start, rideOrder.Destination));
            Scheduler.Ride ride = scheduler.OrderRide(rideOrder.Start, rideOrder.Destination, rideOrder.RideType, rideOrder.RideDateTime);
            scheduler.AcceptRide(ride);
            logger.WriteLine("");
        }

        private static int CalculateTotalEarnings(int driverId, ILogger logger, Scheduler scheduler)
        {
            logger.WriteLine(string.Format("Driver with ID = {0} earned today:", driverId));
            int total = 0;
            foreach (Scheduler.Ride r in scheduler.GetRideList(driverId))
            {
                total += r.Price;
                logger.WriteLine("Price: " + r.Price);
            }
            return total;
        }

        private static void LogTotalEarnings(ILogger logger, int total)
        {
            logger.WriteLine("Total: " + total);
        }
    }
}
