using TaxiDispatcher.Abstractions.DbDTO;

namespace TaxiDispatcher.BL.Taxis
{
    public class NaxiTaxi : Taxi
    {
        public NaxiTaxi(DbTaxiDTO dbTaxi) : base(dbTaxi) { }

        protected override int PricePerDistance => 10;

        public override DbTaxiDTO ToDbTaxi()
        {
            DbTaxiDTO dbTaxi = ToDbTaxiBase();
            dbTaxi.TaxiCompany = "Naxi";
            return dbTaxi;
        }
    }
}
