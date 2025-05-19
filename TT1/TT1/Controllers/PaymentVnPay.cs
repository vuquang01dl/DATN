using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TT1.AppDbContexts;
using TT1.Payloads.DataRequests;
using TT1.Services.Interfaces;

namespace TT1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentVnPay : ControllerBase
    {
        private readonly IPayment paymentServices;
        private readonly AppDbContext _context;
        public Request_VNPayPayment request;

        public PaymentVnPay(IPayment _paymentServices)
        {
            paymentServices = _paymentServices;
            _context = new AppDbContext();
        }
        [HttpPost]
        public IActionResult CreatePaymentUrl(Request_VNPayPayment model)
        {
            var url = paymentServices.CreatePaymentUrl(model, HttpContext);

            return Ok(url);
        }
        [HttpGet]
        public IActionResult PaymentCallback()
        {
            var response = paymentServices.PaymentExecute(Request.Query, request);     
            return Ok(response);
        }
    }
}
