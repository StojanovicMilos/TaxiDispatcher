using System;

namespace TaxiDispatcher.BL.Locations
{
    public enum City
    {
        City1 = 0,
        City2 = 1
    };

    public class Location
    {
        public int CoordinateX { get; }

        public Location(int coordinateX)
        {
            CoordinateX = coordinateX;
        }

        public Location(Location otherLocation)
        {
            if (otherLocation == null) throw new ArgumentNullException(nameof(otherLocation));
            CoordinateX = otherLocation.CoordinateX;
        }

        public int DistanceTo(Location otherLocation)
        {
            if (otherLocation == null) throw new ArgumentNullException(nameof(otherLocation));
            return Math.Abs(CoordinateX - otherLocation.CoordinateX);
        }

        public override string ToString()
        {
            return CoordinateX.ToString();
        }

        //Assumption: one city is from -infinity to 10, the other city is from 11 to +infinity
        public City City => CoordinateX <= 10 ? City.City1 : City.City2;
    }
}