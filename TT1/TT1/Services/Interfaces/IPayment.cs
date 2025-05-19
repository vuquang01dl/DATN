using TT1.Payloads.DataRequests;
using TT1.Payloads.DataResponses;

namespace TT1.Services.Interfaces
{
    public interface IPayment
    {
        string CreatePaymentUrl(Request_VNPayPayment model, HttpContext context);
        VnPayPaymentResponses PaymentExecute(IQueryCollection collections, Request_VNPayPayment request);
    }
}
