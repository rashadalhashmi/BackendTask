using Task.Entities;

namespace Task.Interfaces
{
    public interface ICustomerDemographicsDSL
    {
        IEnumerable<object> CalculateGenderDistribution(IEnumerable<Client> clients);
        IEnumerable<object> CalculateAgeDistribution(IEnumerable<Client> clients);
        IEnumerable<object> CalculateGeographicDistribution(IEnumerable<Client> clients);
        IEnumerable<object> CalculateServicePreferences(IEnumerable<BookingService> bookingServices);
        IEnumerable<object> CalculatePaymentMethods(IEnumerable<Transaction> transactions);

    }
}
