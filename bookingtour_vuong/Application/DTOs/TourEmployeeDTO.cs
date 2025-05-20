using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class TourEmployeeDTO
    {
        public Guid TourID { get; set; }
        public Guid EmployeeID { get; set; }
        public bool IsLeader { get; set; }
    }
}
