using TaxiDispatcher.DAL;

namespace TaxiDispatcher.App
{
    public class NaxiTaxi : Taxi
    {
        public NaxiTaxi(DBTaxi dbTaxi) : base(dbTaxi) { }

        protected override int PricePerDistance => 10;

        public override DBTaxi ToDBTaxi()
        {
            DBTaxi dbTaxi = ToDBTaxiBase();
            dbTaxi.TaxiCompany = "Naxi";
            return dbTaxi;
        }
    }
}
