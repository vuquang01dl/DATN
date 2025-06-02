using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class BookingDTO
    {
        public Guid BookingId { get; set; }
        public Guid TourId { get; set; }
        public Guid CustomerId { get; set; }
        public Guid? PaymentId { get; set; }
        public int Adult { get; set; }
        public int Child { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime? ModifyAt { get; set; }
        public string? Note { get; set; }
    }


}
