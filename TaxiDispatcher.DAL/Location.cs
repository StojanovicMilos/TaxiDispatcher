using System;

namespace TaxiDispatcher.DAL
{
    public class Location
    {
        public int CoordinateX { get; set; }

        public int DistanceTo(Location otherLocation)
        {
            return Math.Abs(CoordinateX - otherLocation.CoordinateX);
        }

        public override string ToString()
        {
            return CoordinateX.ToString();
        }
    }
}