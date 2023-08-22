using Task.Entities;
using Task.Interfaces;

namespace Task.DataService
{
    public class RevenueReportDSl : IRevenueReportDSl
    {
     
        public RevenueReportDSl()
        {

        }


        public decimal CalculateTotalRevenue(IEnumerable<Transaction> transactions)
        {
            return transactions.Sum(transaction => transaction.Amount);
        }

        public IEnumerable<object> CalculateRevenueByService(IEnumerable<Transaction> transactions, IEnumerable<BookingService> bookingServices)
        {
            var revenueByService = from transaction in transactions
                                   join bookingService in bookingServices
                                   on transaction.BookingId equals bookingService.BookingId
                                   group bookingService by bookingService.ServiceId into grouped
                                   select new
                                   {
                                       ServiceId = grouped.Key,
                                       Revenue = grouped.Sum(item => item.Price)
                                   };
            return revenueByService;
        }

        public IEnumerable<object> CalculateRevenueByPaymentMethod(IEnumerable<Transaction> transactions)
        {
            var revenueByPaymentMethod = transactions.GroupBy(transaction => transaction.PaymentMethod)
                                                     .Select(group => new
                                                     {
                                                         PaymentMethod = group.Key,
                                                         Revenue = group.Sum(item => item.Amount)
                                                     });
            return revenueByPaymentMethod;
        }

        public IEnumerable<object> CalculateRevenueByBranch(IEnumerable<Transaction> transactions, IEnumerable<Booking> bookings, IEnumerable<Branch> branches)
        {
            var revenueByBranch = from transaction in transactions
                                  join booking in bookings
                                  on transaction.BookingId equals booking.BookingId
                                  join branch in branches
                                  on booking.BranchId equals branch.BranchId
                                  group transaction by branch.BranchId into grouped
                                  select new
                                  {
                                      BranchId = grouped.Key,
                                      Revenue = grouped.Sum(item => item.Amount)
                                  };
            return revenueByBranch;
        }

        public IEnumerable<object> CalculateRevenueByBranchCity(IEnumerable<Transaction> transactions, IEnumerable<Booking> bookings, IEnumerable<Branch> branches)
        {
            var revenueByBranchCity = from t in transactions
                                      join b in bookings on t.BookingId equals b.BookingId
                                      join br in branches on b.BranchId equals br.BranchId
                                      group t by br.City into cityGroup
                                      select new
                                      {
                                          BranchCity = cityGroup.Key,
                                          Revenue = cityGroup.Sum(t => t.Amount)
                                      };
            return revenueByBranchCity;
        }

        public IEnumerable<object> CalculateRevenueByBranchCountry(IEnumerable<Transaction> transactions, IEnumerable<Booking> bookings, IEnumerable<Branch> branches)
        {
            var revenueByBranchCountry = from t in transactions
                                         join b in bookings on t.BookingId equals b.BookingId
                                         join br in branches on b.BranchId equals br.BranchId
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
