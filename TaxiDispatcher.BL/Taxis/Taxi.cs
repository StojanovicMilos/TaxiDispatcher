using System;
using System.Collections.Generic;
using System.Linq;
using TaxiDispatcher.BL.Locations;
using TaxiDispatcher.BL.Rides;

namespace TaxiDispatcher.BL.Taxis
{
    public class Taxi
    {
        public int TaxiId { get; }
        public string DriverName { get; }
        public Location CurrentLocation { get; private set; }
        public List<Ride> Rides { get; }
        public TaxiCompany TaxiCompany { get; }

        public Taxi(int id, string taxiDriverName, Location currentLocation, List<Ride> rides, string taxiCompanyName)
        {
            TaxiId = id >= 0 ? id : throw new ArgumentException(nameof(id));
            DriverName = taxiDriverName ?? throw new ArgumentNullException(nameof(taxiDriverName));
            CurrentLocation = currentLocation ?? throw new ArgumentNullException(nameof(currentLocation));
            Rides = rides ?? throw new ArgumentNullException(nameof(rides));
            TaxiCompany = new TaxiCompany(taxiCompanyName);
        }

        public int CalculateInitialRidePrice(Location startLocation, Location destinationLocation)
        {
            if (startLocation == null) throw new ArgumentNullException(nameof(startLocation));
            if (destinationLocation == null) throw new ArgumentNullException(nameof(destinationLocation));
            return startLocation.DistanceTo(destinationLocation) * TaxiCompany.PricePerDistance;
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
            CurrentLocation = new Location(ride.DestinationLocation);
        }
    }
}
