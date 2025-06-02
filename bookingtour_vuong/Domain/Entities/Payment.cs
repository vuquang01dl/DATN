
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Payment
    {
        [Key]
        public Guid PaymentID { get; set; }

        public string Method { get; set; } = "";

        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        public DateTime Date { get; set; }
        public Guid BookingID { get; set; } // FK tới Booking
        public Guid? CustomerID { get; set; }     // ✅ phải có

    }

}
