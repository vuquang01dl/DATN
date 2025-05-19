using ThucTapW1.Entities;

namespace TT1.Entities
{
    public class User : Base
    {
        public string user_name { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string address { get; set; }
        public int accountId { get; set; }
        public DateTime create_at { get; set; }
        public DateTime update_at { get; set; }

        public Account account { get; set; }
    }
}
