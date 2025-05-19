using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using ThucTapW1.Entities;
using TT1.AppDbContexts;
using TT1.Entities;
using TT1.Payloads.Converters;
using TT1.Payloads.DataRequests;
using TT1.Payloads.DataResponses;
using TT1.Payloads.Responses;
using TT1.Services.Interfaces;

namespace TT1.Services.Implements
{
    public class PaymentService : IPaymentService
    {
        private readonly AppDbContext _context;
        private readonly ResponseObject<DataResponsePayment> _responseObject;
        private readonly PaymentConverter _converter;

        public PaymentService()
        {
            _context = new AppDbContext();
            _responseObject = new ResponseObject<DataResponsePayment>();
            _converter = new PaymentConverter();
        }

        public ResponseObject<DataResponsePayment> ProcessPayment(PaymentRequest paymentRequest)
        {
            try
            {
                var order = _context.Orders.SingleOrDefault(o => o.Id == paymentRequest.OrderId);

                if (order == null)
                {
                    return _responseObject.ResponseError(StatusCodes.Status404NotFound, "Không tìm thấy đơn đặt hàng", null);
                }

                var payment = new Payment
                {
                 
                    payment_method = paymentRequest.PaymentMethod,
                    status = paymentRequest.Status,
                    created_at = DateTime.Now,
                    updated_at = DateTime.Now
                };

                _context.Payments.Add(payment);
                _context.SaveChanges();

                // Nếu thanh toán thành công, cập nhật trạng thái đơn đặt hàng hoặc thêm các bước xử lý khác nếu cần thiết.
                order.paymentId = payment.Id;
                 _context.Orders.Update(order);
                 _context.SaveChanges();
                // tăng số lượt mua các sản phẩm có trong order 


                // Increase the purchase count for each product in the order
                var listorderdetail = _context.OrderDetails.FirstOrDefault(x => x.orderId == paymentRequest.OrderId);
                var product = _context.Products.FirstOrDefault(x => x.Id == listorderdetail.productId);
                product.NumberOfPurchases++;
                _context.Products.Update(product);
                _context.SaveChanges();
                return _responseObject.ResponseSucess("Thanh toán thành công", _converter.EntityToDTO(payment));
            }
            catch (Exception ex)
            {
                // Ghi log lỗi hoặc xử lý ngoại lệ phù hợp
                return _responseObject.ResponseError(StatusCodes.Status400BadRequest, "Lỗi thanh toán", null);
            }
        }
    }
}
