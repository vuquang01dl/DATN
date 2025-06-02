using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class TourEmployee
    {
        [Key, Column(Order = 0)]
        public Guid TourId { get; set; }

        [Key, Column(Order = 1)]
        public Guid EmployeeId { get; set; }

        [ForeignKey(nameof(TourId))]
        public virtual Tour Tour { get; set; }

        [ForeignKey(nameof(EmployeeId))]
        public virtual Employee Employee { get; set; }

        public bool IsLeader { get; set; }
    }
}
