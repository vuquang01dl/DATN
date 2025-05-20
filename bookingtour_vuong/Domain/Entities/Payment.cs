
namespace Domain.Entities
{
    public class Payment
    {
        public Guid PaymentID { get; set; }
        public string Method { get; set; } = "";
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public Guid BookingID { get; set; } // khóa ngoại đến Booking
    }
}
