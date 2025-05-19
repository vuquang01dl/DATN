﻿namespace Presentation.Areas.Admin.Models
{
    public class EmployeeViewModel
    {
        public Guid EmployeeID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Position { get; set; }    
        public string? Image { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool isActive { get; set; }
    }
}
