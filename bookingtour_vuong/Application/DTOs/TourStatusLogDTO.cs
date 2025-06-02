using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class TourStatusLogDTO
    {
        public Guid Id { get; set; }
        public Guid TourId { get; set; }
        public string Status { get; set; }
        public DateTime Time { get; set; }
        public string? Note { get; set; }
        public Guid? EmployeeId { get; set; }
    }

}
