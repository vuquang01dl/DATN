namespace TT1.Entities
{
    public class RefreshToken : Base
    {
        public string Token { get; set; }
        public DateTime ExpiredTime { get; set; }
        public int accountId { get; set; }
        public Account account { get; set; }
    }
}
