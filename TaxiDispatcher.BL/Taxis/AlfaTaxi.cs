using System.Collections.Generic;
using TaxiDispatcher.BL.Locations;
using TaxiDispatcher.BL.Rides;

namespace TaxiDispatcher.BL.Taxis
{
    public class AlfaTaxi : Taxi
    {
        public AlfaTaxi(int id, string name, Location current, List<Ride> rides) : base(id, name, current, rides) { }
        
        protected override int PricePerDistance => 15;
        public override string TaxiCompany { get; } = "Alfa";
    }
}
