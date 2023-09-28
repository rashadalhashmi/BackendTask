using Task.Entities;

namespace Task.Interfaces
{
    public interface ICustomerDemographicsRpo
    {
        IEnumerable<object> CalculateAgeDistribution();
        IEnumerable<object> CalculateGenderDistribution();
        IEnumerable<object> CalculateGeographicDistribution();
        IEnumerable<object> CalculatePaymentMethods();
        IEnumerable<object> CalculateServicePreferences();
    }
}
}