using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Account
    {
        [Key]
        public Guid AccountID { get; set; }
        public string Email { get; set; } = "";
        public string PasswordHash { get; set; } = "";
        public string Role { get; set; } = "User"; // "User" | "Admin" (tùy)
        public bool IsActive { get; set; } = true;
    }
}
