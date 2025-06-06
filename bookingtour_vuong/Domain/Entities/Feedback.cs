﻿using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Feedback
    {
        [Key]
        public Guid FeedbackID { get; set; }
        public string? Content { get; set; }
        public DateTime Date { get; set; }

        // Nếu có liên kết với Customer
        public Guid CustomerID { get; set; }
    }
}
