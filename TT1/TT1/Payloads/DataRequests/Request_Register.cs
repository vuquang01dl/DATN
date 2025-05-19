using TT1.Entities;

namespace TT1.Payloads.DataRequests
{
    public class Request_Register
    {
        //account
        public string user_name { get; set; }
        public string avata { get; set; }
        public string password { get; set; }
        public int decentralizationId { get; set; }
        //user
        public string phone { get; set; }
        public string email { get; set; }
        public string address { get; set; }

    }
}
