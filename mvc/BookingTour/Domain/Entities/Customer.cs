﻿namespace Domain.Entities
{
    public class Customer
    {
        public Guid CustomerID { get; set; }
        public string? Image { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }

        public Guid AccountID { get; set; }

        public virtual ICollection<Booking> Bookings { get; set; }
        public virtual Account Account {  get; set; }
        public virtual ICollection<Feedback> Feedbacks { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
    }
}
