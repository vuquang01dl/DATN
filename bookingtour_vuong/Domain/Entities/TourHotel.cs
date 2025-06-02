using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class TourHotel
    {
        [Key, Column(Order = 0)]
        public Guid TourId { get; set; }

        [Key, Column(Order = 1)]
        public Guid HotelId { get; set; }

        [ForeignKey(nameof(TourId))]
        public virtual Tour Tour { get; set; }

        [ForeignKey(nameof(HotelId))]
        public virtual Hotel Hotel { get; set; }
    }
}
