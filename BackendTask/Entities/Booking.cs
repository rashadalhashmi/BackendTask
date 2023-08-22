namespace Task.Entities
{
    public class Booking
    {
        public int BookingId { get; set; }
        public int ClientId { get; set; }
        public int BranchId { get; set; }
        public DateOnly BookingDate { get; set; }
        public TimeOnly BookingTime { get; set; }
        public string Status { get; set; }
    }
}

