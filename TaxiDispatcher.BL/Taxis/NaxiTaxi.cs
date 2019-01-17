using System.Collections.Generic;
using TaxiDispatcher.BL.Locations;
using TaxiDispatcher.BL.Rides;

namespace TaxiDispatcher.BL.Taxis
{
    public class NaxiTaxi : Taxi
    {
        public NaxiTaxi(int id, string name, Location current, List<Ride> rides) : base(id, name, current, rides) { }

        protected override int PricePerDistance => 10;
        public override string TaxiCompany { get; } = "Naxi";
    }
}
