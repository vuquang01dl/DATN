namespace TT1.Entities
{
    public class Decentralization : Base
    {
        public string authority_name { get; set; }
        public string Code { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }

        public List<Account> accounts { get; set; }
    }
}
