using Task.Entities;

namespace Task.Interfaces
{
    public interface IAppointmentReportDSL
    {
        int CalculateTotalAppointments(IEnumerable<Booking> bookings);
        IEnumerable<object> CalculateAppointmentsByService(IEnumerable<Booking> bookings, IEnumerable<BookingService> bookingServices);
        IEnumerable<object> CalculateAppointmentsByBranch(IEnumerable<Booking> bookings);
        IEnumerable<object> CalculateAppointmentsByStatus(IEnumerable<Booking> bookings);
        IEnumerable<object> CalculateAppointmentsByBranchCountry(IEnumerable<Booking> bookings, IEnumerable<Branch> branches);
        IEnumerable<object> CalculateAppointmentsByBranchCity(IEnumerable<Booking> bookings, IEnumerable<Branch> branches);

    }
}
