using System;
using TaxiDispatcher.DAL.Entities;

namespace TaxiDispatcher.BL.Locations
{
    public class Location
    {
        private readonly int _coordinateX;

        public Location(int coordinateX)
        {
            _coordinateX = coordinateX;
        }

        public Location(DbLocation dbLocation)
        {
            _coordinateX = dbLocation.CoordinateX;
        }

        public int DistanceTo(Location otherLocation)
        {
            return Math.Abs(_coordinateX - otherLocation._coordinateX);
        }

        public override string ToString()
        {
            return _coordinateX.ToString();
        }

        public DbLocation ToDbLocation()
        {
            return new DbLocation { CoordinateX = _coordinateX };
        }
    }
}