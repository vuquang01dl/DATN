using System.Collections.Generic;
using ThucTapW1.Entities;
using TT1.Payloads.DataRequests;
using TT1.Payloads.DataResponses;
using TT1.Payloads.Responses;

namespace TT1.Services.Interfaces
{
    public interface IPaymentService
    {
        ResponseObject<DataResponsePayment> ProcessPayment(PaymentRequest paymentRequest);
    }
}
