using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public enum TourStatus
    {
        ChoMoBan,
        DangMoBan,
        DaKhoaSo,
        DangDienRa,
        KetThuc,
        Huy
    }

    public class Tour
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;
        public TourStatus Status { get; set; } = TourStatus.ChoMoBan;
        public string Description { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public string Location { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public ICollection<TourHotel> TourHotels { get; set; } = new List<TourHotel>();
        public ICollection<TourDestination> TourDestinations { get; set; } = new List<TourDestination>();
        public ICollection<TourEmployee> TourEmployees { get; set; } = new List<TourEmployee>();
    }

}
