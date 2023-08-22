using Task.Entities;

namespace Task
{
    public static class MockData
    {
        public static List<Transaction> Transactions;
        public static List<Branch> Branches;
        public static List<Service> Services;
        public static List<Client> Clients;
        public static List<Booking> Bookings;
        public static List<BookingService> BookingServices;

        static MockData()
        {
            Branches = GenerateMockBranches(100);
            Services = GenerateMockServices(100);
            Clients = GenerateMockClients(100);
            Bookings = GenerateMockBookings(100);
            BookingServices = GenerateMockBookingServices(100);
            Transactions = GenerateMockTransactions(100);       
        }


        private static List<Booking> GenerateMockBookings(int count)
        {
            List<Booking> bookings = new List<Booking>();
            Random random = new Random();

            for (int i = 1; i <= count; i++)
            {
                bookings.Add(new Booking
                {
                    BookingId = i,
                    ClientId = Clients[random.Next(1, Clients.Count)].ClientId,
                    BranchId = Branches[random.Next(1, Branches.Count)].BranchId,
                    BookingDate = new DateOnly(2023, random.Next(1, 13), random.Next(1, 29)),
                    BookingTime = new TimeOnly(random.Next(0, 24), random.Next(0, 60)),
                    Status = random.Next(2) == 0 ? "Confirmed" : "Pending"
                });
            }
            return bookings;
        }
        private static List<Client> GenerateMockClients(int count)
        {
            List<Client> clients = new List<Client>();
            Random random = new Random();

            for (int i = 1; i <= count; i++)
            {
                clients.Add(new Client
                {
                    ClientId = i,
                    FirstName = $"First{i}",
                    LastName = $"Last{i}",
                    Gender = random.Next(2) == 0 ? "Male" : "Female",
                    Email = $"client{i}@example.com",
                    Phone = $"123-456-{random.Next(1000, 9999)}",
                    Address = $"Address{i}",
                    City = $"City{i}",
                    Country = $"Country{i}",
                    Birthdate = new DateTime(random.Next(1980, 2005), random.Next(1, 13), random.Next(1, 29))
                });
            }

            return clients;
        }
        private static List<Branch> GenerateMockBranches(int count)
        {
            List<Branch> branches = new List<Branch>();
            Random random = new Random();

            for (int i = 1; i <= count; i++)
            {
                branches.Add(new Branch
                {
                    BranchId = i,
                    Name = $"Branch {i}",
                    Address = $"Address {i}",
                    City = $"City {random.Next(1, 6)}",
                    Country = $"Country {random.Next(1, 4)}",
                    Phone = $"Phone {random.Next(1000000, 9999999)}"
                });
            }

            return branches;
        }
        private static List<Transaction> GenerateMockTransactions(int count)
        {
            List<Transaction> transactions = new List<Transaction>();
            Random random = new Random();
            //(decimal)(random.Next(100, 1000) + random.NextDouble()),
            for (int i = 1; i <= count; i++)
            {
                transactions.Add(new Transaction
                {
                    TransactionId = i,
                    BookingId =BookingServices[random.Next(1, BookingServices.Count )].BookingId,
                    Amount = (decimal)(random.Next(100, 1000) + random.NextDouble()),
                    PaymentMethod = RandomPaymentMethod(random),
                    PaymentDate = new DateTime(2023, random.Next(1, 13), random.Next(1, 29))
                });
            }

            return transactions;
        }
        private static List<Service> GenerateMockServices(int count)
        {
            List<Service> services = new List<Service>();
            Random random = new Random();

            for (int i = 1; i <= count; i++)
            {
                services.Add(new Service
                {
                    ServiceId = i,
                    Name = $"Service {i}",
                    Description = $"Description for Service {i}",
                    Price = i * 10 ,//(decimal)(random.Next(10, 1000) + random.NextDouble()),
                    Duration = random.Next(30, 180)
                });
            }

            return services;
        }
        private static List<BookingService> GenerateMockBookingServices(int count)
        {
            List<BookingService> bookingServices = new List<BookingService>();
            Random random = new Random();

            for (int i = 1; i <= count; i++)
            {
                var serviceId = Services[random.Next(1, Services.Count)].ServiceId;
                var bookingId = Bookings[random.Next(1, Bookings.Count)].BookingId;
                if (bookingServices.Exists(x => x.ServiceId == serviceId && x.BookingId == bookingId))
                {
                    //i--;
                    continue;
                }
                bookingServices.Add(new BookingService
                {
                    BookingServiceId = i,
                    BookingId = bookingId,
                    ServiceId = serviceId,
                    Price = Services.FirstOrDefault(s => s.ServiceId == serviceId).Price
                });
            }

            return bookingServices;
        }
        private static string RandomPaymentMethod(Random random)
        {
            string[] paymentMethods = { "Credit Card", "PayPal", "Bank Transfer", "Cash", "Cryptocurrency" };
            return paymentMethods[random.Next(paymentMethods.Length)];
        }
        
    }

}
