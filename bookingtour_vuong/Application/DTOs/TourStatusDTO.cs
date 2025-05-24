using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class TourStatusDTO
    {
        public string TourName { get; set; } = string.Empty;
        public string CustomerName { get; set; } = string.Empty;
        public string BookingDate { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
    }
}

