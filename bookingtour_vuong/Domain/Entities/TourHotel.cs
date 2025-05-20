using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Domain.Entities
{
    public class TourHotel
    {
        public Guid TourID { get; set; }
        public Guid HotelID { get; set; }

        // Navigation (tuỳ bạn dùng hay không)
        public Tour? Tour { get; set; }
        public Hotel? Hotel { get; set; }
    }
}
