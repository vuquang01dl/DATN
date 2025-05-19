namespace TT1.Payloads.DataRequests
{
    public class Request_VNPayPayment
    {
        public int OrderId { get; set; }
        public string OrderType { get; set; }
     
        public string OrderDescription { get; set; }
        public string Name { get; set; }
    }
}
