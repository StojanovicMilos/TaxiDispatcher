using System;
using TaxiDispatcher.Abstractions.DbDTO;

namespace TaxiDispatcher.BL.Locations
{
    public enum City
    {
        City1 = 0,
        City2 = 1
    };

    public class Location
    {
        private readonly int _coordinateX;

        public Location(int coordinateX)
        {
            _coordinateX = coordinateX;
        }

        public Location(DbLocationDTO dbLocation)
        {
            if (dbLocation == null)
                throw new ArgumentNullException(nameof(dbLocation));
            _coordinateX = dbLocation.CoordinateX;
        }

        public int DistanceTo(Location otherLocation)
        {
            if (otherLocation == null)
                throw new ArgumentNullException(nameof(otherLocation));
            return Math.Abs(_coordinateX - otherLocation._coordinateX);
        }

        public override string ToString()
        {
            return _coordinateX.ToString();
        }

        public DbLocationDTO ToDbLocation()
        {
            return new DbLocationDTO {CoordinateX = _coordinateX};
        }

        public City City => _coordinateX < 11 ? City.City1 : City.City2;

    }
}