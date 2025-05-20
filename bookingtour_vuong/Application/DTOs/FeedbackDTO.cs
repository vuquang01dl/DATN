using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class FeedbackDTO
    {
        public Guid FeedbackID { get; set; }
        public string? Content { get; set; }
        public DateTime Date { get; set; }
        public Guid CustomerID { get; set; }
    }
}
