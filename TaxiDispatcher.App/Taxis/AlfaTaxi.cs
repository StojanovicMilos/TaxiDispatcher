using TaxiDispatcher.DAL;

namespace TaxiDispatcher.App.Taxis
{
    public class AlfaTaxi : Taxi
    {
        public AlfaTaxi(DbTaxi dbTaxi) : base(dbTaxi) { }

        protected override int PricePerDistance => 15;

        public override DbTaxi ToDbTaxi()
        {
            DbTaxi dbTaxi = ToDbTaxiBase();
            dbTaxi.TaxiCompany = "Alfa";
            return dbTaxi;
        }
    }
}
