using Task.Entities;

namespace Task.Interfaces
{
    public interface IReportDsl
    {
        object GetAppointmentReport(ReportFilter appointmentFilter);
        object GetCustomerDemographics(ReportFilter reportFilters);
        object GetRevenueReport(ReportFilter reportFilters);
    }
}