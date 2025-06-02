using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class PaymentDTO
    {
        public Guid PaymentID { get; set; }
        public string Method { get; set; } = "";
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public Guid BookingID { get; set; }
        public Guid CustomerID { get; set; }   // ✅ bắt buộc phải có dòng này
    }

}
