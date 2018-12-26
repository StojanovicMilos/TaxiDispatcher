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
            RideOrders[] rideOrders = new RideOrders[]
            {
                new RideOrders {Start = 5, Destination = 0, RideType = Constants.City, RideDateTime =  new DateTime(2018, 1, 1, 23, 0, 0)},
                new RideOrders {Start = 0, Destination = 12, RideType = Constants.InterCity, RideDateTime =  new DateTime(2018, 1, 1, 9, 0, 0)},
                new RideOrders {Start = 5, Destination = 0, RideType = Constants.City, RideDateTime =  new DateTime(2018, 1, 1, 11, 0, 0)},
                new RideOrders {Start = 35, Destination = 12, RideType = Constants.City, RideDateTime =  new DateTime(2018, 1, 1, 11, 0, 0)}
            };

            foreach (var rideOrder in rideOrders)
            {
                try
                {
                    logger.WriteLine(string.Format("Ordering ride from {0} to {1}...", rideOrder.Start, rideOrder.Destination));
                    Scheduler.Ride ride = scheduler.OrderRide(rideOrder.Start, rideOrder.Destination, rideOrder.RideType, rideOrder.RideDateTime);
                    scheduler.AcceptRide(ride);
                    logger.WriteLine("");
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

            logger.WriteLine("Driver with ID = 2 earned today:");
            int total = 0;
            foreach (Scheduler.Ride r in scheduler.GetRideList(2))
            {
                total += r.Price;
                logger.WriteLine("Price: " + r.Price);
            }
            logger.WriteLine("Total: " + total);
        }
    }
}
