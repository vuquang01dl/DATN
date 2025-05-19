namespace TT1.Entities
{
    public class Account : Base
    {
        public string user_name { get; set; }
        public string avata { get; set; }
        public string password { get; set; }
        public int status { get; set; }
        public int decentralizationId { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public List<User> users { get; set; }
        public List<RefreshToken> refreshTokens { get; set; }
        public List<ConfirmEmail> confirmEmails { get; set; }
        public Decentralization decentralization { get; set; }
    }
}
