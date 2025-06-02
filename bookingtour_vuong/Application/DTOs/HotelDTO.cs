using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    namespace Application.DTOs
    {
        public class HotelDTO
        {
            public Guid HotelID { get; set; }
            public string Name { get; set; } = "";
            public int StarRating { get; set; }
            public string Description { get; set; } = "";
            public string? Image { get; set; }
            public string Address { get; set; } = "";
            public string City { get; set; } = "";
            public string District { get; set; } = "";
            public string Ward { get; set; } = "";
            public string Phone { get; set; } = "";
        }
    }

}
