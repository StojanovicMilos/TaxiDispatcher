using TaxiDispatcher.Abstractions.DbDTO;

namespace TaxiDispatcher.BL.Taxis
{
    public class GoldTaxi : Taxi
    {
        public GoldTaxi(DbTaxiDTO dbTaxi) : base(dbTaxi) { }

        protected override int PricePerDistance => 13;

        public override DbTaxiDTO ToDbTaxi()
        {
            DbTaxiDTO dbTaxi = ToDbTaxiBase();
            dbTaxi.TaxiCompany = "Gold";
            return dbTaxi;
        }
    }
}
