using TT1.Entities;

namespace ThucTapW1.Entities
{
    public class Payment : Base
    {
        public string payment_method { get; set; }
        public int status { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }

 
    }
}
