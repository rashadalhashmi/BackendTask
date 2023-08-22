using Task.Entities;

namespace Task.Interfaces
{
    public interface IRevenueReportDSl
    {
        decimal CalculateTotalRevenue(IEnumerable<Transaction> transactions);
        IEnumerable<object> CalculateRevenueByService(IEnumerable<Transaction> transactions, IEnumerable<BookingService> bookingServices);
        IEnumerable<object> CalculateRevenueByPaymentMethod(IEnumerable<Transaction> transactions);
        IEnumerable<object> CalculateRevenueByBranch(IEnumerable<Transaction> transactions, IEnumerable<Booking> bookings, IEnumerable<Branch> branches);
        IEnumerable<object> CalculateRevenueByBranchCity(IEnumerable<Transaction> transactions, IEnumerable<Booking> bookings, IEnumerable<Branch> branches);
        IEnumerable<object> CalculateRevenueByBranchCountry(IEnumerable<Transaction> transactions, IEnumerable<Booking> bookings, IEnumerable<Branch> branches);

    }
}
