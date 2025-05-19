namespace TT1.Payloads.DataResponses
{
    public class DataResponsePayment
    {
        public string PaymentMethod { get; set; }
        public int Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
      
    }
}
