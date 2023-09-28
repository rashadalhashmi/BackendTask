using Task.Entities;
using Task.Interfaces;

namespace Task.Rpo
{
    public class AppointmentReportRpo : IAppointmentReportRpo, IAppointmentReportRpo
    {

        private IEnumerable<Booking> Bookings;
        private IEnumerable<BookingService> BookingServices;
        private IEnumerable<Client> Clients;
        private IEnumerable<Transaction> Transactions;
        private IEnumerable<Branch> Branches;

        public AppointmentReportRpo()
        {
            Bookings = MockData.Bookings.ToList();
            BookingServices = MockData.BookingServices.ToList();
            Clients = MockData.Clients.ToList();
            Transactions = MockData.Transactions.ToList();
            Branches = MockData.Branches.ToList();
        }
        public int CalculateTotalAppointments()
        {
            return Bookings.Count();
        }

        public IEnumerable<object> CalculateAppointmentsByService()
        {
            var appointmentByService = from b in Bookings
                                       join bs in BookingServices on b.BookingId equals bs.BookingId
                                       group bs by bs.ServiceId into serviceGroup
                                       select new
                                       {
                                           ServiceId = serviceGroup.Key,
                                           Appointment = serviceGroup.Count()
                                       };
            return appointmentByService;
        }

        public IEnumerable<object> CalculateAppointmentsByBranch()
        {
            var appointmentByBranch = from b in Bookings
                                      group b by b.BranchId into grouped
                                      select new
                                      {
                                          BranchId = grouped.Key,
                                          Appointment = grouped.Count()
                                      };
            return appointmentByBranch;
        }

        public IEnumerable<object> CalculateAppointmentsByStatus()
        {
            var appointmentByStatus = from b in Bookings
                                      group b by b.Status into grouped
                                      select new
                                      {
                                          Status = grouped.Key,
                                          Appointment = grouped.Count()
                                      };
            return appointmentByStatus;
        }

        public IEnumerable<object> CalculateAppointmentsByBranchCountry()
        {
            var appointmentByBranchCountry = from b in Bookings
                                             join br in Branches on b.BranchId equals br.BranchId
                                             group br by br.Country into grouped
                                             select new
                                             {
                                                 Country = grouped.Key,
                                                 Appointment = grouped.Count()
                                             };
            return appointmentByBranchCountry;
        }

        public IEnumerable<object> CalculateAppointmentsByBranchCity()
        {
            var appointmentByBranchCity = from b in Bookings
                                          join br in Branches on b.BranchId equals br.BranchId
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
