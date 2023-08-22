using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using Task.Entities;
using Task.Interfaces;

namespace Task.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IAppointmentReportDSL _IAppointmentReportDSL;
        private readonly ICustomerDemographicsDSL _ICustomerDemographicsDSL;
        private readonly IRevenueReportDSl _IRevenueReportDSL;

        private IEnumerable<Booking> FilteredBookings;
        private IEnumerable<BookingService> FilteredBookingServices;
        private IEnumerable<Client> FilteredClients;
        private IEnumerable<Transaction> FilteredTransactions;
        private IEnumerable<Branch> FilteredBranches;

        public ReportController(IAppointmentReportDSL appointmentReportDSL, ICustomerDemographicsDSL customerDemographicsDSL, IRevenueReportDSl revenueReportDSl)
        {
            _IRevenueReportDSL = revenueReportDSl;
            _ICustomerDemographicsDSL = customerDemographicsDSL;
            _IAppointmentReportDSL = appointmentReportDSL;
            FilteredBookings = MockData.Bookings.ToList();
            FilteredBookingServices = MockData.BookingServices.ToList();
            FilteredClients = MockData.Clients.ToList();
            FilteredTransactions = MockData.Transactions.ToList();
            FilteredBranches = MockData.Branches.ToList();
        }

        [HttpGet]
        [Route("RevenueReport")]
        public IActionResult RevenueReport([FromQuery] ReportFilter reportFilters)
        {
            GetFilteredData(reportFilters);

            decimal totalRevenue = _IRevenueReportDSL.CalculateTotalRevenue(FilteredTransactions);
            var revenueByService = _IRevenueReportDSL.CalculateRevenueByService(FilteredTransactions, FilteredBookingServices);
            var revenueByPaymentMethod = _IRevenueReportDSL.CalculateRevenueByPaymentMethod(FilteredTransactions);
            var revenueByBranch = _IRevenueReportDSL.CalculateRevenueByBranch(FilteredTransactions, FilteredBookings, FilteredBranches);
            var revenueByBrarnchCity = _IRevenueReportDSL.CalculateRevenueByBranchCity(FilteredTransactions, FilteredBookings, FilteredBranches);
            var revenueByBranchCountry = _IRevenueReportDSL.CalculateRevenueByBranchCountry(FilteredTransactions, FilteredBookings, FilteredBranches);

            var Result = new
            {
                totalRevenue = totalRevenue,
                revenueByBranch = revenueByBranch,
                revenueByService = revenueByService,
                revenueByPaymentMethod = revenueByPaymentMethod,
                revenueByCity = revenueByBrarnchCity,
                revenueByCountry = revenueByBranchCountry,

            };

            return Ok(Result);
        }
        [HttpGet]
        [Route("AppointmentReport")]
        public IActionResult AppointmentReport([FromQuery] ReportFilter appointmentFilter)
        {
            GetFilteredData(appointmentFilter);

            var totalAppointment = _IAppointmentReportDSL.CalculateTotalAppointments(FilteredBookings);
            var appointmentByService = _IAppointmentReportDSL.CalculateAppointmentsByService(FilteredBookings, FilteredBookingServices);
            var appointmentByBranch = _IAppointmentReportDSL.CalculateAppointmentsByBranch(FilteredBookings);
            var appointmentByStatus = _IAppointmentReportDSL.CalculateAppointmentsByStatus(FilteredBookings);
            var appointmentByBranchCountry = _IAppointmentReportDSL.CalculateAppointmentsByBranchCountry(FilteredBookings, FilteredBranches);
            var appointmentByBranchCity = _IAppointmentReportDSL.CalculateAppointmentsByBranchCity(FilteredBookings, FilteredBranches);

            var result = new
            {
                totalAppointment = totalAppointment,
                AppointmentByService = appointmentByService,
                AppointmentByBranch = appointmentByBranch,
                AppointmentByStatus = appointmentByStatus,
                AppointmentByCountry = appointmentByBranchCountry,
                AppointmentByCity = appointmentByBranchCity,
            };

            return Ok(result);
        }
        [HttpGet]
        [Route("CustomerDemographics")]
        public IActionResult CustomerDemographics([FromQuery] ReportFilter reportFilters)
        {
            GetFilteredData(reportFilters);
            var genderDistribution = _ICustomerDemographicsDSL.CalculateGenderDistribution(FilteredClients);
            var ageDistribution = _ICustomerDemographicsDSL.CalculateAgeDistribution(FilteredClients);
            var geographicDistribution = _ICustomerDemographicsDSL.CalculateGeographicDistribution(FilteredClients);
            var servicePreferences = _ICustomerDemographicsDSL.CalculateServicePreferences(FilteredBookingServices);
            var paymentMethods = _ICustomerDemographicsDSL.CalculatePaymentMethods(FilteredTransactions);

            var result = new
            {
                genderDistribution = genderDistribution,
                paymentMethods = paymentMethods,
                servicePreferences = servicePreferences,
                ageDistribution = ageDistribution,
                geographicDistribution = geographicDistribution
            };

            return Ok(result);
        }
        private void GetFilteredData(ReportFilter filterData)
        {
            if (filterData.DateFrom.HasValue)
            {
                FilteredBookings = FilteredBookings.Where(b => new DateTime(b.BookingDate.Year, b.BookingDate.Month, b.BookingDate.Day) >= filterData.DateFrom.Value);
            }
            if (filterData.DateTo.HasValue)
            {
                FilteredBookings = FilteredBookings.Where(b => new DateTime(b.BookingDate.Year, b.BookingDate.Month, b.BookingDate.Day) <= filterData.DateTo.Value);
            }
            if (filterData.ServiceId.HasValue)
            {
                FilteredBookingServices = FilteredBookingServices.Where(bs => bs.ServiceId == filterData.ServiceId.Value);
            }
            if (filterData.BranchId.HasValue)
            {
                FilteredBookings = FilteredBookings.Where(b => b.BranchId == filterData.BranchId.Value);
            }

            if (!string.IsNullOrEmpty(filterData.ClientCity))
            {
                FilteredClients = FilteredClients.Where(c => c.City.Contains(filterData.ClientCity));
            }
            if (!string.IsNullOrEmpty(filterData.ClientCountry))
            {
                FilteredClients = FilteredClients.Where(c => c.Country.Contains(filterData.ClientCountry));
            }
            if (filterData.PaymentDateFrom.HasValue)
            {
                FilteredTransactions = FilteredTransactions.Where(t => t.PaymentDate >= filterData.PaymentDateFrom.Value);
            }
            if (filterData.PaymentDateTo.HasValue)
            {
                FilteredTransactions = FilteredTransactions.Where(t => t.PaymentDate <= filterData.PaymentDateTo.Value);
            }
            if (!string.IsNullOrEmpty(filterData.PaymentMethod))
            {
                FilteredTransactions = FilteredTransactions.Where(t => t.PaymentMethod == filterData.PaymentMethod);
            }
            if (filterData.BranchId.HasValue)
            {
                FilteredBranches = FilteredBranches.Where(b => b.BranchId == filterData.BranchId);
            }
            if (!string.IsNullOrEmpty(filterData.BranchCountry))
            {
                FilteredBranches = FilteredBranches.Where(b => b.Country.Contains(filterData.BranchCountry));
            }
            if (!string.IsNullOrEmpty(filterData.BranchCity))
            {
                FilteredBranches = FilteredBranches.Where(b => b.City.Contains(filterData.BranchCity));
            }
        }

    }

}
