
using Task.Entities;
using Task.Interfaces;

namespace Task.Rpo
{
    public class RevenueReportRpo : IRevenueReportRpo
    {
        private IEnumerable<Booking> Bookings;
        private IEnumerable<BookingService> BookingServices;
        private IEnumerable<Client> Clients;
        private IEnumerable<Transaction> Transactions;
        private IEnumerable<Branch> Branches;

        public RevenueReportRpo()
        {
            Bookings = MockData.Bookings.ToList();
            BookingServices = MockData.BookingServices.ToList();
            Clients = MockData.Clients.ToList();
            Transactions = MockData.Transactions.ToList();
            Branches = MockData.Branches.ToList();
        }
        public decimal CalculateTotalRevenue()
        {
            return Transactions.Sum(transaction => transaction.Amount);
        }

        public IEnumerable<object> CalculateRevenueByService()
        {
            var revenueByService = from transaction in Transactions
                                   join bookingService in BookingServices
                                   on transaction.BookingId equals bookingService.BookingId
                                   group bookingService by bookingService.ServiceId into grouped
                                   select new
                                   {
                                       ServiceId = grouped.Key,
                                       Revenue = grouped.Sum(item => item.Price)
                                   };
            return revenueByService;
        }

        public IEnumerable<object> CalculateRevenueByPaymentMethod()
        {
            var revenueByPaymentMethod = Transactions.GroupBy(transaction => transaction.PaymentMethod)
                                                     .Select(group => new
                                                     {
                                                         PaymentMethod = group.Key,
                                                         Revenue = group.Sum(item => item.Amount)
                                                     });
            return revenueByPaymentMethod;
        }

        public IEnumerable<object> CalculateRevenueByBranch()
        {
            var revenueByBranch = from transaction in Transactions
                                  join booking in Bookings
                                  on transaction.BookingId equals booking.BookingId
                                  join branch in Branches
                                  on booking.BranchId equals branch.BranchId
                                  group transaction by branch.BranchId into grouped
                                  select new
                                  {
                                      BranchId = grouped.Key,
                                      Revenue = grouped.Sum(item => item.Amount)
                                  };
            return revenueByBranch;
        }

        public IEnumerable<object> CalculateRevenueByBranchCity()
        {
            var revenueByBranchCity = from t in Transactions
                                      join b in Bookings on t.BookingId equals b.BookingId
                                      join br in Branches on b.BranchId equals br.BranchId
                                      group t by br.City into cityGroup
                                      select new
                                      {
                                          BranchCity = cityGroup.Key,
                                          Revenue = cityGroup.Sum(t => t.Amount)
                                      };
            return revenueByBranchCity;
        }

        public IEnumerable<object> CalculateRevenueByBranchCountry()
        {
            var revenueByBranchCountry = from t in Transactions
                                         join b in Bookings on t.BookingId equals b.BookingId
                                         join br in Branches on b.BranchId equals br.BranchId
                                         group t by br.Country into countryGroup
                                         select new
                                         {
                                             BranchCountry = countryGroup.Key,
                                             Revenue = countryGroup.Sum(t => t.Amount)
                                         };
            return revenueByBranchCountry;
        }

    }
}
