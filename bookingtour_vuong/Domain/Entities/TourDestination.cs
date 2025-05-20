
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class TourDestination
    {
        public Guid TourID { get; set; }
        public Guid DestinationID { get; set; }

        public Tour? Tour { get; set; }
        public Destination? Destination { get; set; }
    }
}
