using System;
using TaxiDispatcher.BL.Interfaces;

namespace TaxiDispatcher.BL.Taxis
{
    public class TaxiEarningsLogger
    {
        private readonly ILogger _logger;
        private readonly IDatabase _database;

        public TaxiEarningsLogger(ILogger logger, IDatabase database)
        {
            _database = database ?? throw new ArgumentNullException(nameof(database));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public void LogTotalEarningsForTaxiWithId(int taxiId)
        {
            if (taxiId < 0)
                throw new ArgumentException(nameof(taxiId));

            Taxi taxi = _database.GetTaxi(taxiId);

            _logger.WriteLine($"Driver with ID = {taxi.TaxiId} earned today:");
            foreach (var ride in taxi.Rides)
            {
                _logger.WriteLine("Price: " + ride.Price);
            }

            _logger.WriteLine("Total: " + taxi.CalculateTotalEarnings());
        }
    }
}
