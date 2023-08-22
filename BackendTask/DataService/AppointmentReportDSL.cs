using Task.Entities;
using Task.Interfaces;

namespace Task.DataService
{
    public class AppointmentReportDSL: IAppointmentReportDSL
    {

        public int CalculateTotalAppointments(IEnumerable<Booking> bookings)
        {
            return bookings.Count();
        }

        public IEnumerable<object> CalculateAppointmentsByService(IEnumerable<Booking> bookings, IEnumerable<BookingService> bookingServices)
        {
            var appointmentByService = from b in bookings
                                       join bs in bookingServices on b.BookingId equals bs.BookingId
                                       group bs by bs.ServiceId into serviceGroup
                                       select new
                                       {
                                           ServiceId = serviceGroup.Key,
                                           Appointment = serviceGroup.Count()
                                       };
            return appointmentByService;
        }

        public IEnumerable<object> CalculateAppointmentsByBranch(IEnumerable<Booking> bookings)
        {
            var appointmentByBranch = from b in bookings
                                      group b by b.BranchId into grouped
                                      select new
                                      {
                                          BranchId = grouped.Key,
                                          Appointment = grouped.Count()
                                      };
            return appointmentByBranch;
        }

        public IEnumerable<object> CalculateAppointmentsByStatus(IEnumerable<Booking> bookings)
        {
            var appointmentByStatus = from b in bookings
                                      group b by b.Status into grouped
                                      select new
                                      {
                                          Status = grouped.Key,
                                          Appointment = grouped.Count()
                                      };
            return appointmentByStatus;
        }

        public IEnumerable<object> CalculateAppointmentsByBranchCountry(IEnumerable<Booking> bookings, IEnumerable<Branch> branches)
        {
            var appointmentByBranchCountry = from b in bookings
                                             join br in branches on b.BranchId equals br.BranchId
                                             group br by br.Country into grouped
                                             select new
                                             {
                                                 Country = grouped.Key,
                                                 Appointment = grouped.Count()
                                             };
            return appointmentByBranchCountry;
        }

        public IEnumerable<object> CalculateAppointmentsByBranchCity(IEnumerable<Booking> bookings, IEnumerable<Branch> branches)
        {
            var appointmentByBranchCity = from b in bookings
                                          join br in branches on b.BranchId equals br.BranchId
                                          group br by br.City into grouped
                                          select new
                                          {
                                              City = grouped.Key,
                                              Appointment = grouped.Count()
                                          };
            return appointmentByBranchCity;
        }

    }
}
