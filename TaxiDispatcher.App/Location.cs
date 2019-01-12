using System;
using TaxiDispatcher.DAL;

namespace TaxiDispatcher.App
{
    public class Location
    {
        private int _coordinateX;

        public Location(int coordinateX)
        {
            _coordinateX = coordinateX;
        }

        public Location(DBLocation dbLocation)
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

        public DBLocation ToDBLocation()
        {
            return new DBLocation { CoordinateX = _coordinateX };
        }
    }
}