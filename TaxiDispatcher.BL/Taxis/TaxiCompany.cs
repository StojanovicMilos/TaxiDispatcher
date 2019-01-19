using TaxiDispatcher.BL.CustomExceptions;

namespace TaxiDispatcher.BL.Taxis
{
    public class TaxiCompany
    {
        public string Name { get; }
        public int PricePerDistance { get; }

        public TaxiCompany(string taxiCompanyName)
        {
            Name = taxiCompanyName;

            switch (taxiCompanyName)
            {
                case "Alfa":
                    PricePerDistance = 15;
                    break;
                case "Gold":
                    PricePerDistance = 13;
                    break;
                case "Naxi":
                    PricePerDistance = 10;
                    break;
                default:
                    throw new InvalidTaxiCompanyException(taxiCompanyName);
            }
        }
    }
}
