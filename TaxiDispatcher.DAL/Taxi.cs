using System;

namespace TaxiDispatcher.DAL
{
    public class Taxi
    {
        public int Taxi_driver_id { get; set; }
        public string Taxi_driver_name { get; set; }
        public string Taxi_company { get; set; }
        public int Location { get; set; }

        public int DistanceTo(int start)
        {
            return Math.Abs(start - Location);
        }

        public int CalculateInitialRidePrice(RideOrder rideOrder)
        {
            switch (Taxi_company)
            {
                case "Naxi":
                    {
                        return 10 * Math.Abs(rideOrder.Start - rideOrder.Destination);
                    }
                case "Alfa":
                    {
                        return 15 * Math.Abs(rideOrder.Start - rideOrder.Destination);
                    }
                case "Gold":
                    {
                        return 13 * Math.Abs(rideOrder.Start - rideOrder.Destination);
                    }
                default:
                    {
                        throw new Exception("Ilegal company");
                    }
            }
        }
    }
}
