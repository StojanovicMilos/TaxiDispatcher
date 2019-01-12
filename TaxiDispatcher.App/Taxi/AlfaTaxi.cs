using TaxiDispatcher.DAL;

namespace TaxiDispatcher.App
{
    public class AlfaTaxi : Taxi
    {
        public AlfaTaxi(DBTaxi dbTaxi) : base(dbTaxi) { }

        protected override int PricePerDistance => 15;

        public override DBTaxi ToDBTaxi()
        {
            DBTaxi dbTaxi = ToDBTaxiBase();
            dbTaxi.TaxiCompany = "Alfa";
            return dbTaxi;
        }
    }
}
