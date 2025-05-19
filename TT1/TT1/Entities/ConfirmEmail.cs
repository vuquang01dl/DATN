using System.Diagnostics.Eventing.Reader;

namespace TT1.Entities
{
    public class ConfirmEmail : Base
    {
        public int accountId { get; set; }
        public Account account { get; set; }
        public DateTime ExpiredTime { get; set; }
        public string CodeActive { get; set; }
        public bool IsConfirm { get; set; } = false;
    }
}
