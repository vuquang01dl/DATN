using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class TourStatusLog
    {
        public Guid Id { get; set; }
        public Guid TourId { get; set; }
        public string Status { get; set; }   // VD: "Đang ở Bà Nà Hill", "Đang di chuyển", "Đã đến Hội An"
        public DateTime Time { get; set; }
        public string? Note { get; set; }
        public Guid? EmployeeId { get; set; } // ai cập nhật
        public virtual Tour Tour { get; set; }

    }

}
