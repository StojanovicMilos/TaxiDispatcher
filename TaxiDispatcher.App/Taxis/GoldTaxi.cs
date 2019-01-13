using TaxiDispatcher.DAL;
using TaxiDispatcher.DAL.Entities;

namespace TaxiDispatcher.BL.Taxis
{
    public class GoldTaxi : Taxi
    {
        public GoldTaxi(DbTaxi dbTaxi) : base(dbTaxi) { }

        protected override int PricePerDistance => 13;

        public override DbTaxi ToDbTaxi()
        {
            DbTaxi dbTaxi = ToDbTaxiBase();
            dbTaxi.TaxiCompany = "Gold";
            return dbTaxi;
        }
    }
}
