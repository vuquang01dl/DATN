using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class DestinationDTO
    {
        public Guid DestinationID { get; set; }
        public string Name { get; set; } = "";
        public string? Description { get; set; }
        public string? Location { get; set; }
    }
}
