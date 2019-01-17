using System;
using System.Collections.Generic;
using System.Linq;
using TaxiDispatcher.BL.Locations;
using TaxiDispatcher.BL.Rides;

namespace TaxiDispatcher.BL.Taxis
{
    public abstract class Taxi
    {
        public Location CurrentLocation { get; private set; }
        
        protected abstract int PricePerDistance { get; }
        public abstract string TaxiCompany { get; }

        public int TaxiDriverId { get; }
        public string TaxiDriverName { get; }
        public List<Ride> Rides { get; }

        protected Taxi(int id, string name, Location current, List<Ride> rides)
        {
            TaxiDriverId = id;
            TaxiDriverName = name;
            CurrentLocation = current;
            Rides = rides;
        }

        public int CalculateInitialRidePrice(Location startLocation, Location destinationLocation)
        {
            if (startLocation == null) throw new ArgumentNullException(nameof(startLocation));
            if (destinationLocation == null) throw new ArgumentNullException(nameof(destinationLocation));
            return startLocation.DistanceTo(destinationLocation) * PricePerDistance;
        }

        public int DistanceTo(Location startLocation)
        {
            if (startLocation == null) throw new ArgumentNullException(nameof(startLocation));
            return startLocation.DistanceTo(CurrentLocation);
        }

        public int CalculateTotalEarnings() => Rides.Sum(r => r.Price);

        public void AcceptRide(Ride ride)
        {
            if (ride == null)
                throw new ArgumentNullException(nameof(ride));
            Rides.Add(ride);
            CurrentLocation = ride.DestinationLocation;
        }
    }
}
