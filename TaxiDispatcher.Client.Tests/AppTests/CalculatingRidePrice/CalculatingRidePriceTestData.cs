using System.Collections.Generic;
using TaxiDispatcher.Tests.AppTests.SharedTestData;

namespace TaxiDispatcher.Tests.AppTests.CalculatingRidePrice
{
    public class CalculatingRidePriceTestData
    {
        private static int ExpectedRidePrice(int price) => price;

        public static IEnumerable<object[]> CalculatingRidePricesForRideOrdersUsedInClientTestData =>
            new List<object[]>
            {
                new object[] {SharedRideOrderTestData.RideOrderUsedInClient1, ExpectedRidePrice(100)},
                new object[] {SharedRideOrderTestData.RideOrderUsedInClient2, ExpectedRidePrice(240)},
                new object[] {SharedRideOrderTestData.RideOrderUsedInClient3, ExpectedRidePrice(50)}
            };

        public static IEnumerable<object[]> CalculatingRidePricesForNaxiTaxiTestData =>
            new List<object[]>
            {
                new object[] {SharedRideOrderTestData.RideOrderCityDayNaxiTaxi, ExpectedRidePrice(10)},
                new object[] {SharedRideOrderTestData.RideOrderCityNightNaxiTaxi, ExpectedRidePrice(20)},
                new object[] {SharedRideOrderTestData.RideOrderInterCityDayNaxiTaxi, ExpectedRidePrice(20)},
                new object[] {SharedRideOrderTestData.RideOrderInterCityNightNaxiTaxi, ExpectedRidePrice(40)},
            };

        public static IEnumerable<object[]> CalculatingRidePricesForAlfaTaxiTestData =>
            new List<object[]>
            {
                new object[] {SharedRideOrderTestData.RideOrderCityDayAlfaTaxi, ExpectedRidePrice(15)},
                new object[] {SharedRideOrderTestData.RideOrderCityNightAlfaTaxi, ExpectedRidePrice(30)},
                new object[] {SharedRideOrderTestData.RideOrderInterCityDayAlfaTaxi, ExpectedRidePrice(30)},
                new object[] {SharedRideOrderTestData.RideOrderInterCityNightAlfaTaxi, ExpectedRidePrice(60)}
            };

        public static IEnumerable<object[]> CalculatingRidePricesForGoldTaxiTestData =>
            new List<object[]>
            {
                new object[] {SharedRideOrderTestData.RideOrderCityDayGoldTaxi, ExpectedRidePrice(13)},
                new object[] {SharedRideOrderTestData.RideOrderCityNightGoldTaxi, ExpectedRidePrice(26)},
                new object[] {SharedRideOrderTestData.RideOrderInterCityDayGoldTaxi, ExpectedRidePrice(26)},
                new object[] {SharedRideOrderTestData.RideOrderInterCityNightGoldTaxi, ExpectedRidePrice(52)}
            };
    }
}
