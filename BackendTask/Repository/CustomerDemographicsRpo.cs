
using Task.Entities;
using Task.Interfaces;

namespace Task.Rpo
{
    public class CustomerDemographicsRpo : ICustomerDemographicsRpo
    {
        private IEnumerable<Booking> Bookings;
        private IEnumerable<BookingService> BookingServices;
        private IEnumerable<Client> Clients;
        private IEnumerable<Transaction> Transactions;
        private IEnumerable<Branch> Branches;

        public CustomerDemographicsRpo()
        {
            Bookings = MockData.Bookings.ToList();
            BookingServices = MockData.BookingServices.ToList();
            Clients = MockData.Clients.ToList();
            Transactions = MockData.Transactions.ToList();
            Branches = MockData.Branches.ToList();
        }
        public IEnumerable<object> CalculateGenderDistribution()
        {
            var genderDistribution = Clients
                .GroupBy(c => c.Gender)
                .Select(group => new
                {
                    Gender = group.Key,
                    Count = group.Count(),
                    Percentage = Math.Round(group.Count() * 100.0 / Clients.Count(), 2)
                });
            return genderDistribution;
        }

        public IEnumerable<object> CalculateAgeDistribution()
        {
            var ageDistribution = Clients
                .GroupBy(c => CalculateAgeRange(c.Birthdate))
                .Select(group => new
                {
                    AgeRange = group.Key,
                    Count = group.Count(),
                    Percentage = Math.Round(group.Count() * 100.0 / Clients.Count(), 2)
                })
                .OrderBy(group => group.AgeRange.MinAge);
            return ageDistribution;
        }

        public IEnumerable<object> CalculateGeographicDistribution()
        {
            var geographicDistribution = Clients
                .GroupBy(c => new { c.City, c.Country })
                .Select(group => new
                {
                    City = group.Key.City,
                    Country = group.Key.Country,
                    Count = group.Count(),
                    Percentage = Math.Round(group.Count() * 100.0 / Clients.Count(), 2)
                })
                .OrderByDescending(group => group.Count);
            return geographicDistribution;
        }

        public IEnumerable<object> CalculateServicePreferences()
        {
            var servicePreferences = BookingServices
                .GroupBy(bs => bs.ServiceId)
                .Select(group => new
                {
                    ServiceId = group.Key,
                    Count = group.Count(),
                    Percentage = Math.Round(group.Count() * 100.0 / BookingServices.Count(), 2)
                })
                .OrderByDescending(group => group.Count);
            return servicePreferences;
        }

        public IEnumerable<object> CalculatePaymentMethods()
        {
            var paymentMethods = transactions
                .GroupBy(t => t.PaymentMethod)
                .Select(group => new
                {
                    PaymentMethod = group.Key,
                    Count = group.Count(),
                    Percentage = Math.Round(group.Count() * 100.0 / Transactions.Count(), 2)
                });
            return paymentMethods;
        }
        private AgeRange CalculateAgeRange(DateTime birthdate)
        {
            int age = DateTime.Today.Year - birthdate.Year;

            AgeRange ageRange;

            if (age < 18)
            {
                ageRange = new AgeRange { Range = "Under 18", MinAge = 0, MaxAge = 17 };
            }
            else if (age < 25)
            {
                ageRange = new AgeRange { Range = "18-24", MinAge = 18, MaxAge = 24 };
            }
            else if (age < 35)
            {
                ageRange = new AgeRange { Range = "25-34", MinAge = 25, MaxAge = 34 };
            }
            else if (age < 45)
            {
                ageRange = new AgeRange { Range = "35-44", MinAge = 35, MaxAge = 44 };
            }
            else if (age < 55)
            {
                ageRange = new AgeRange { Range = "45-54", MinAge = 45, MaxAge = 54 };
            }
            else
            {
                ageRange = new AgeRange { Range = "55+", MinAge = 55, MaxAge = int.MaxValue };
            }

            return ageRange;
        }


        public void ApplyReportFilter(ReportFilter reportFilter)
        {

        }
    }
}
