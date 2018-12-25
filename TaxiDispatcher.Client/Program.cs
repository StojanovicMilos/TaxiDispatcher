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

            try
            {
                logger.WriteLine("Ordering ride from 5 to 0...");
                Scheduler.Ride ride = scheduler.OrderRide(5, 0, Constants.City, new DateTime(2018, 1, 1, 23, 0, 0));
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

            try
            {
                logger.WriteLine("Ordering ride from 0 to 12...");
                Scheduler.Ride ride = scheduler.OrderRide(0, 12, Constants.InterCity, new DateTime(2018, 1, 1, 9, 0, 0));
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

            try
            {
                logger.WriteLine("Ordering ride from 5 to 0...");
                Scheduler.Ride ride = scheduler.OrderRide(5, 0, Constants.City, new DateTime(2018, 1, 1, 11, 0, 0));
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

            try
            {
                logger.WriteLine("Ordering ride from 35 to 12...");
                Scheduler.Ride ride = scheduler.OrderRide(35, 12, Constants.City, new DateTime(2018, 1, 1, 11, 0, 0));
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
