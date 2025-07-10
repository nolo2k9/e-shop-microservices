namespace Ordering.Application.Extensions
{
    public static class OrderExtensions
    {
        public static IEnumerable<OrderDto> ToOrderDtoList(this IEnumerable<Order> orders)
        {
            return orders.Select(order =>
            {
                // Top-level null checks
                if (order == null) throw new ArgumentNullException(nameof(order), "Order is null");
                if (order.Id == null) throw new Exception("Order.Id is null");
                if (order.CustomerId == null) throw new Exception("Order.CustomerId is null");
                if (order.OrderName == null) throw new Exception("Order.OrderName is null");
                if (order.ShippingAddress == null) throw new Exception("Order.ShippingAddress is null");
                if (order.Payment == null) throw new Exception("Order.Payment is null");
                if (order.OrderItems == null) throw new Exception("Order.OrderItems is null");

                // Shipping address checks
                var shipping = order.ShippingAddress;
                if (shipping.FirstName == null) throw new Exception("ShippingAddress.FirstName is null");
                if (shipping.LastName == null) throw new Exception("ShippingAddress.LastName is null");
                if (shipping.EmailAddress == null) throw new Exception("ShippingAddress.EmailAddress is null");
                if (shipping.AddressLine == null) throw new Exception("ShippingAddress.AddressLine is null");
                if (shipping.Country == null) throw new Exception("ShippingAddress.Country is null");
                if (shipping.State == null) throw new Exception("ShippingAddress.State is null");
                if (shipping.ZipCode == null) throw new Exception("ShippingAddress.ZipCode is null");

                // Billing address checks - MAKE OPTIONAL
                AddressDto? billingAddressDto = null;
                var billing = order.BillingAddress;
                if (billing != null)
                {
                    billingAddressDto = new AddressDto(
                        billing.FirstName,
                        billing.LastName,
                        billing.EmailAddress!,
                        billing.AddressLine,
                        billing.Country,
                        billing.State,
                        billing.ZipCode
                    );
                }

                // Payment checks
                var payment = order.Payment;
                if (payment.CardName == null) throw new Exception("Payment.CardName is null");
                if (payment.CardNumber == null) throw new Exception("Payment.CardNumber is null");
                if (payment.Expiration == null) throw new Exception("Payment.Expiration is null");
                if (payment.CVV == null) throw new Exception("Payment.CVV is null");
                if (payment.PaymentMethod == null) throw new Exception("Payment.PaymentMethod is null");

                // OrderItems checks
                foreach (var oi in order.OrderItems)
                {
                    if (oi.OrderId == null) throw new Exception("OrderItem.OrderId is null");
                    if (oi.ProductId == null) throw new Exception("OrderItem.ProductId is null");
                    // Quantity and Price assumed non-nullable (value types)
                }

                return new OrderDto(
                    Id: order.Id.Value,
                    CustomerId: order.CustomerId.Value,
                    OrderName: order.OrderName.Value,
                    ShippingAddress: new AddressDto(
                        shipping.FirstName,
                        shipping.LastName,
                        shipping.EmailAddress!,
                        shipping.AddressLine,
                        shipping.Country,
                        shipping.State,
                        shipping.ZipCode),
                    BillingAddress: billingAddressDto, // Pass null if not present
                    Payment: new PaymentDto(
                        payment.CardName!,
                        payment.CardNumber,
                        payment.Expiration,
                        payment.CVV,
                        payment.PaymentMethod),
                    Status: order.Status,
                    OrderItems: order.OrderItems.Select(oi => new OrderItemDto(oi.OrderId.Value, oi.ProductId.Value, oi.Quantity, oi.Price)).ToList()
                );
            });
        }
    }
}
