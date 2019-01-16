using TaxiDispatcher.Abstractions.DbDTO;

namespace TaxiDispatcher.BL.Taxis
{
    public class AlfaTaxi : Taxi
    {
        public AlfaTaxi(DbTaxiDTO dbTaxi) : base(dbTaxi) { }

        protected override int PricePerDistance => 15;

        public override DbTaxiDTO ToDbTaxi()
        {
            DbTaxiDTO dbTaxi = ToDbTaxiBase();
            dbTaxi.TaxiCompany = "Alfa";
            return dbTaxi;
        }
    }
}
