

using Ordering.Domain.Enums;

namespace Ordering.Infrastructure.Data.Extensions;

public class InitialData
{
    public static IEnumerable<Customer> Customers => new List<Customer>
    {
        Customer.Create(CustomerId.Of(new Guid("938be781-df0f-4628-9463-653b95a5c503")), "keith", "keith@gmail.com"),
        Customer.Create(CustomerId.Of(new Guid("f0e2c7c2-1c39-4c4a-8f1c-2b4e3c5a8e16")), "muchita", "muchita@gmail.com")

    };

    public static IEnumerable<Product> Products => new List<Product>
    {
        Product.Create(
            ProductId.Of(new Guid("a1e9c2d8-5e0d-4d2a-9f2b-2b8d7e6a3f21")),
            "Wireless Mouse",
            29.99m
        ),
        Product.Create(
            ProductId.Of(new Guid("b7a3f4e1-2c0b-4f8d-9e3a-5d4c2b1a7e9f")),
            "Mechanical Keyboard",
            79.99m
        ),
        Product.Create(
            ProductId.Of(new Guid("c3d6e2f9-1b0a-4c3d-8f2e-6a7b5d4c2e1f")),
            "27\" 4K Monitor",
            299.99m
        )
    };

    public static IEnumerable<Order> OrdersWithItems
    {
        get
        {
            var address = Address.Of
            (
                "Keith",
                "Murphy",
                "keith.murphy@gmail.com",
                "123 Main Street, Ballitore",
                "Ireland",
                "Kildare",
                "R14 XYZ1"
            );
            var address2 = Address.Of(
                "Muchita",
                "O'Connor",
                "muchita.oconnor@gmail.com",
                "456 Green Lane, Naas",
                "Ireland",
                "Kildare",
                "W91 ABC2"
            );
            var payment = Payment.Of(
                "Muchita O'Connor",
                "4111 1111 1111 1111",
                "12/26",
                "123",
                1 // Example: 1 = Credit Card
            );
            var payment2 = Payment.Of(
                "Johnny O'Connor",
                "4111 1111 1111 1111",
                "12/26",
                "123",
                1 // Example: 1 = Credit Card
            );


            var order1 = Order.Create(
                OrderId.Of(new Guid("e1b4c5d6-7f8a-4b2c-9e0d-1a2b3c4d5e6f")),
                CustomerId.Of(new Guid("938be781-df0f-4628-9463-653b95a5c503")), // Keith
                OrderName.Of("ORD01"),
                shippingAddress: address,
                billingAddress: address,
                payment, OrderStatus.Pending);
            order1.Add(ProductId.Of(new Guid("a1e9c2d8-5e0d-4d2a-9f2b-2b8d7e6a3f21")), 1, 500);
            order1.Add(ProductId.Of(new Guid("b7a3f4e1-2c0b-4f8d-9e3a-5d4c2b1a7e9f")), 1, 300);

            return new List<Order> { order1 };
        }
    }
}
    
