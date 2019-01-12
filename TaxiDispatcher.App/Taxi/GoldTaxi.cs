using TaxiDispatcher.DAL;

namespace TaxiDispatcher.App
{
    public class GoldTaxi : Taxi
    {
        public GoldTaxi(DBTaxi dbTaxi) : base(dbTaxi) { }

        protected override int PricePerDistance => 13;

        public override DBTaxi ToDBTaxi()
        {
            DBTaxi dbTaxi = ToDBTaxiBase();
            dbTaxi.TaxiCompany = "Gold";
            return dbTaxi;
        }
    }
}
