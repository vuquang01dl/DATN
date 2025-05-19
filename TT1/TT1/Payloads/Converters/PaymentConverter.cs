using System.Linq;
using ThucTapW1.Entities;
using TT1.Entities;
using TT1.Payloads.DataResponses;

namespace TT1.Payloads.Converters
{
    public class PaymentConverter
    {
        public DataResponsePayment EntityToDTO(Payment payment)
        {
            if (payment == null)
            {
                return null;
            }

            return new DataResponsePayment
            {
                PaymentMethod = payment.payment_method,
                Status = payment.status,
                CreatedAt = payment.created_at,
                UpdatedAt = payment.updated_at,
                Orders = payment.orders?.Select(order => new DataResponseOrder
                {
                    // Map properties from Order to OrderDTO
                }).ToList()
            };
        }
    }
}
