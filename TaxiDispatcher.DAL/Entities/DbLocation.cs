using System;
using TaxiDispatcher.BL.Locations;

namespace TaxiDispatcher.DAL.Entities
{
    public class DbLocation
    {
        private readonly int _coordinateX;

        public DbLocation(int coordinateX)
        {
            _coordinateX = coordinateX;
        }

        public DbLocation(Location location)
        {
            if (location == null) throw new ArgumentNullException(nameof(location));
            _coordinateX = location.CoordinateX;
        }

        public Location ToDomain() => new Location(_coordinateX);
    }
}
