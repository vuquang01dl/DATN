using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public TourStatus Status { get; set; } = TourStatus.ChoMoBan;

        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Location { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
