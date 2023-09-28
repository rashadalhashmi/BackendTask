using Task.Entities;

namespace Task.Interfaces
{
    public interface IAppointmentReportRpo
    {
        IEnumerable<object> CalculateAppointmentsByBranch();
        IEnumerable<object> CalculateAppointmentsByBranchCity();
        IEnumerable<object> CalculateAppointmentsByBranchCountry();
        IEnumerable<object> CalculateAppointmentsByService();
        IEnumerable<object> CalculateAppointmentsByStatus();
        int CalculateTotalAppointments();
    }
}