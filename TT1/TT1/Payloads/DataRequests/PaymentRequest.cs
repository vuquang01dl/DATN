namespace TT1.Payloads.DataRequests
{
    public class PaymentRequest
    {
        public int OrderId {  get; set; }
        public string PaymentMethod { get; set; }
        public int Status { get; set; }

    }
}
