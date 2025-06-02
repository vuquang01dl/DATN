using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entities;

namespace Domain.Entities
{
    public class TourDestination
    {
        [Key]
        public int Id { get; set; }

        public Guid TourId { get; set; }
        public Guid DestinationId { get; set; }

        [ForeignKey(nameof(TourId))]
        public Tour Tour { get; set; }

        [ForeignKey(nameof(DestinationId))]
        public Destination Destination { get; set; }
    }
}
