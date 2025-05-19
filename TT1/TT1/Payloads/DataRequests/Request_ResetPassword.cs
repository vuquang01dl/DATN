namespace TT1.Payloads.DataRequests
{
    public class Request_ResetPassword
    {
        public string email { get; set; }
        public string newPassword { get; set; }
        public string resetToken { get; set; }
    }
}
