using TaxiDispatcher.DAL;

namespace TaxiDispatcher.App.Taxis
{
    public class NaxiTaxi : Taxi
    {
        public NaxiTaxi(DbTaxi dbTaxi) : base(dbTaxi) { }

        protected override int PricePerDistance => 10;

        public override DbTaxi ToDbTaxi()
        {
            DbTaxi dbTaxi = ToDbTaxiBase();
            dbTaxi.TaxiCompany = "Naxi";
            return dbTaxi;
        }
    }
}
