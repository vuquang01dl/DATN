using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class TourEmployeeDTO
    {
        public Guid TourId { get; set; }
        public Guid EmployeeId { get; set; }
        public bool IsLeader { get; set; }
    }

}
