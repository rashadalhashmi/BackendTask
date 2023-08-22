namespace Task.Entities
{
    public class ReportFilter
    {
        public string? PaymentMethod { get; set; }
        public DateTime? PaymentDateFrom { get; set; }
        public DateTime? PaymentDateTo { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public int? ServiceId { get; set; }
        public string? BranchCountry { get; set; }
        public string? BranchCity { get; set; }
        public string? ClientCountry { get; set; }
        public string? ClientCity { get; set; }
        public int? BranchId { get; set; }
    }
}
