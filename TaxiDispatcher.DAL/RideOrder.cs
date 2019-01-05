using System;

namespace TaxiDispatcher.DAL
{
    public class RideOrder
    {
        public int Start { get; set; }
        public int Destination { get; set; }
        public int RideType { get; set; }
        public DateTime RideDateTime { get; set; }

        public int Distance() => Math.Abs(Start - Destination);
    }
}
