using System;

namespace TaxiDispatcher.Client
{
    public class RideOrders
    {
        public int Start { get; set; }
        public int Destination { get; set; }
        public int RideType { get; set; }
        public DateTime RideDateTime { get; set; }
    }
}
