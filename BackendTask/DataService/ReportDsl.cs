using Task;
using Task.Entities;
using Task.Interfaces;

namespace BackendTask.DataService
{
    public class ReportDsl : IReportDsl
    {
        private readonly IRevenueReportRpo _IRevenueReportRpo;
        private readonly ICustomerDemographicsRpo _ICustomerDemographicsRpo;
        private readonly IAppointmentReportRpo _IAppointmentReportRpo;


        private IEnumerable<Booking> FilteredBookings;
        private IEnumerable<BookingService> FilteredBookingServices;
        private IEnumerable<Client> FilteredClients;
        private IEnumerable<Transaction> FilteredTransactions;
        private IEnumerable<Branch> FilteredBranches;

        public ReportDsl()
        {
            FilteredBookings = MockData.Bookings.ToList();
            FilteredBookingServices = MockData.BookingServices.ToList();
            FilteredClients = MockData.Clients.ToList();
            FilteredTransactions = MockData.Transactions.ToList();
            FilteredBranches = MockData.Branches.ToList();
        }
        public object GetRevenueReport(ReportFilter reportFilter)
        {
            GetFilteredData(reportFilter);

            decimal totalRevenue = _IRevenueReportRpo.CalculateTotalRevenue();
            var revenueByService = _IRevenueReportRpo.CalculateRevenueByService();
            var revenueByPaymentMethod = _IRevenueReportRpo.CalculateRevenueByPaymentMethod();
            var revenueByBranch = _IRevenueReportRpo.CalculateRevenueByBranch();
            var revenueByBrarnchCity = _IRevenueReportRpo.CalculateRevenueByBranchCity();
            var revenueByBranchCountry = _IRevenueReportRpo.CalculateRevenueByBranchCountry();

            var Result = new
            {
                totalRevenue = totalRevenue,
                revenueByBranch = revenueByBranch,
                revenueByService = revenueByService,
                revenueByPaymentMethod = revenueByPaymentMethod,
                revenueByCity = revenueByBrarnchCity,
                revenueByCountry = revenueByBranchCountry,

            };
            return Result;
        }
        public object GetAppointmentReport(ReportFilter reportFilter)
        {
            GetFilteredData(reportFilter);

            var totalAppointment = _IAppointmentReportRpo.CalculateTotalAppointments();
            var appointmentByService = _IAppointmentReportRpo.CalculateAppointmentsByService();
            var appointmentByBranch = _IAppointmentReportRpo.CalculateAppointmentsByBranch();
            var appointmentByStatus = _IAppointmentReportRpo.CalculateAppointmentsByStatus();
            var appointmentByBranchCountry = _IAppointmentReportRpo.CalculateAppointmentsByBranchCountry();
            var appointmentByBranchCity = _IAppointmentReportRpo.CalculateAppointmentsByBranchCity();

            var result = new
            {
                totalAppointment = totalAppointment,
                AppointmentByService = appointmentByService,
                AppointmentByBranch = appointmentByBranch,
                AppointmentByStatus = appointmentByStatus,
                AppointmentByCountry = appointmentByBranchCountry,
                AppointmentByCity = appointmentByBranchCity,
            };
            return result;
        }
        public object GetCustomerDemographics(ReportFilter reportFilter)
        {
            GetFilteredData(reportFilter);
            var genderDistribution = _ICustomerDemographicsRpo.CalculateGenderDistribution();
            var ageDistribution = _ICustomerDemographicsRpo.CalculateAgeDistribution();
            var geographicDistribution = _ICustomerDemographicsRpo.CalculateGeographicDistribution();
            var servicePreferences = _ICustomerDemographicsRpo.CalculateServicePreferences();
            var paymentMethods = _ICustomerDemographicsRpo.CalculatePaymentMethods();
            var result = new
            {
                genderDistribution = genderDistribution,
                paymentMethods = paymentMethods,
                servicePreferences = servicePreferences,
                ageDistribution = ageDistribution,
                geographicDistribution = geographicDistribution
            };
            return result;
        }
        private void GetFilteredData(ReportFilter reportFilter)
        {
            if (reportFilter.DateFrom.HasValue)
            {
                FilteredBookings = FilteredBookings.Where(b => new DateTime(b.BookingDate.Year, b.BookingDate.Month, b.BookingDate.Day) >= reportFilter.DateFrom.Value);
            }
            if (reportFilter.DateTo.HasValue)
            {
                FilteredBookings = FilteredBookings.Where(b => new DateTime(b.BookingDate.Year, b.BookingDate.Month, b.BookingDate.Day) <= reportFilter.DateTo.Value);
            }
            if (reportFilter.ServiceId.HasValue)
            {
                FilteredBookingServices = FilteredBookingServices.Where(bs => bs.ServiceId == reportFilter.ServiceId.Value);
            }
            if (reportFilter.BranchId.HasValue)
            {
                FilteredBookings = FilteredBookings.Where(b => b.BranchId == reportFilter.BranchId.Value);
            }
            if (!string.IsNullOrEmpty(reportFilter.ClientCity))
            {
                FilteredClients = FilteredClients.Where(c => c.City.Contains(reportFilter.ClientCity));
            }
            if (!string.IsNullOrEmpty(reportFilter.ClientCountry))
            {
                FilteredClients = FilteredClients.Where(c => c.Country.Contains(reportFilter.ClientCountry));
            }
            if (reportFilter.PaymentDateFrom.HasValue)
            {
                FilteredTransactions = FilteredTransactions.Where(t => t.PaymentDate >= reportFilter.PaymentDateFrom.Value);
            }
            if (reportFilter.PaymentDateTo.HasValue)
            {
                FilteredTransactions = FilteredTransactions.Where(t => t.PaymentDate <= reportFilter.PaymentDateTo.Value);
            }
            if (!string.IsNullOrEmpty(reportFilter.PaymentMethod))
            {
                FilteredTransactions = FilteredTransactions.Where(t => t.PaymentMethod == reportFilter.PaymentMethod);
            }
            if (reportFilter.BranchId.HasValue)
            {
                FilteredBranches = FilteredBranches.Where(b => b.BranchId == reportFilter.BranchId);
            }
            if (!string.IsNullOrEmpty(reportFilter.BranchCountry))
            {
                FilteredBranches = FilteredBranches.Where(b => b.Country.Contains(reportFilter.BranchCountry));
            }
            if (!string.IsNullOrEmpty(reportFilter.BranchCity))
            {
                FilteredBranches = FilteredBranches.Where(b => b.City.Contains(reportFilter.BranchCity));
            }
        }

    }
}
