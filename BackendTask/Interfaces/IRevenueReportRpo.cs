using Task.Entities;

namespace Task.Interfaces
{
    public interface IRevenueReportRpo
    {
        IEnumerable<object> CalculateRevenueByBranch();
        IEnumerable<object> CalculateRevenueByBranchCity();
        IEnumerable<object> CalculateRevenueByBranchCountry();
        IEnumerable<object> CalculateRevenueByPaymentMethod();
        IEnumerable<object> CalculateRevenueByService();
        decimal CalculateTotalRevenue();
    }
}