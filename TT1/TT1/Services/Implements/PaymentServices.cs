using Azure;
using Azure.Core;
using Newtonsoft.Json;
using System;
using TT1.AppDbContexts;
using TT1.Libraries;
using TT1.Payloads.DataRequests;
using TT1.Payloads.DataResponses;
using TT1.Services.Implements;
using TT1.Services.Interfaces;
using Microsoft.AspNetCore.Http;

namespace TT1.Services.Implements
{
    public class PaymentServices : BaseService, IPayment
    {
        private readonly IConfiguration _configuration;
        private readonly AppDbContext _context;

        public PaymentServices(IConfiguration configuration)
        {
            _configuration = configuration;
            _context = new AppDbContext();
        }

        public string CreatePaymentUrl(Request_VNPayPayment model, HttpContext context)
        {
            var order = _context.Orders.FirstOrDefault(x => x.Id == model.OrderId);

            var timeZoneById = TimeZoneInfo.FindSystemTimeZoneById(_configuration["TimeZoneId"]);
            var timeNow = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZoneById);
            //var tick = DateTime.Now.Ticks.ToString();
            var pay = new VnPayLibrary();
            var urlCallBack = _configuration["PaymentCallBack:ReturnUrl"];

            pay.AddRequestData("vnp_Version", _configuration["Vnpay:Version"]);
            pay.AddRequestData("vnp_Command", _configuration["Vnpay:Command"]);
            pay.AddRequestData("vnp_TmnCode", _configuration["Vnpay:TmnCode"]);
            pay.AddRequestData("vnp_Amount", ((int)order.actual_price * 100).ToString());
            pay.AddRequestData("vnp_CreateDate", timeNow.ToString("yyyyMMddHHmmss"));
            pay.AddRequestData("vnp_CurrCode", _configuration["Vnpay:CurrCode"]);
            pay.AddRequestData("vnp_IpAddr", pay.GetIpAddress(context));
            pay.AddRequestData("vnp_Locale", _configuration["Vnpay:Locale"]);
            pay.AddRequestData("vnp_OrderInfo", $"{model.Name} {model.OrderDescription} {order.actual_price}");
            pay.AddRequestData("vnp_OrderType", model.OrderType);
            pay.AddRequestData("vnp_ReturnUrl", urlCallBack);
            pay.AddRequestData("vnp_TxnRef", model.OrderId.ToString());

            var paymentUrl =
                pay.CreateRequestUrl(_configuration["Vnpay:BaseUrl"], _configuration["Vnpay:HashSecret"]);

            return paymentUrl;
        }


        public VnPayPaymentResponses PaymentExecute(IQueryCollection collections, Request_VNPayPayment request)
        {
            var pay = new VnPayLibrary();
            var response = pay.GetFullResponseData(collections, _configuration["Vnpay:HashSecret"], request);

            return response;
        }
    }
}
