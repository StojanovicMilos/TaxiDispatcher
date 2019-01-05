namespace TaxiDispatcher.DAL
{
    public class AlfaTaxi : Taxi
    {
        protected override int PricePerDistance => 15;
    }
}
