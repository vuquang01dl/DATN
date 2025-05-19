using Microsoft.AspNetCore.Mvc;
using TT1.Payloads.DataRequests;
using TT1.Payloads.Responses;
using TT1.Services.Interfaces;

namespace YourNamespace.Controllers
{
    [ApiController]
    [Route("api/payments")]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost("process")]
        public IActionResult ProcessPayment([FromBody] PaymentRequest paymentRequest)
        {
                return Ok(_paymentService.ProcessPayment(paymentRequest));

        }
    }
}
