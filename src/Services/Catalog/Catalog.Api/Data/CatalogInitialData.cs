using Marten.Schema;

namespace Catalog.Api.Data;

public class CatalogInitialData : IInitialData
{
    public async Task Populate(IDocumentStore store, CancellationToken cancellation)
    {
        using var session = store.LightweightSession();

        if (await session.Query<Product>().AnyAsync())
            return;
            // Marten UPSERT will cater for existing records
            session.Store<Product>(GetPreconfiguredProducts());


        await session.SaveChangesAsync();
    }

    private static IEnumerable<Product> GetPreconfiguredProducts() => new List<Product>()
    {
        new Product()
        {
            Id = new Guid("b1f0a1e1-1b2c-4c4d-9f1a-1d2e3f4a5b6c"),
            Name = "Galaxy S25 Ultra",
            Description = "Flagship smartphone with advanced camera and display.",
            ImageFile = "galaxy_s25_ultra.jpg",
            Price = 1199.99m,
            Category = new List<string>{"Smart Phone"}
        },
        new Product()
        {
            Id = new Guid("c2e1b3f4-2c3d-4d6e-8f2b-3e4f5a6b7c8d"),
            Name = "iPhone 16 Pro",
            Description = "Latest Apple iPhone with A18 chip and improved battery.",
            ImageFile = "iphone_16_pro.jpg",
            Price = 1299.00m,
            Category = new List<string>{"Smart Phone"}
        },
        new Product()
        {
            Id = new Guid("d3f2c4e5-3d4e-4e7f-9a3c-4f5a6b7c8d9e"),
            Name = "Surface Pro X",
            Description = "Versatile 2-in-1 laptop/tablet with detachable keyboard.",
            ImageFile = "surface_pro_x.jpg",
            Price = 999.99m,
            Category = new List<string>{"Tablet", "Laptop"}
        },
        new Product()
        {
            Id = new Guid("e4a5d6f7-4e5f-4f8a-0a4d-5a6a7c8d9e0f"),
            Name = "MacBook Air M4",
            Description = "Ultra-light laptop with Apple Silicon M4 processor.",
            ImageFile = "macbook_air_m4.jpg",
            Price = 1399.00m,
            Category = new List<string>{"Laptop"}
        },
        new Product()
        {
            Id = new Guid("f5b6e7a8-5f6a-8a9a-1a5e-6a7a8d9e0f1a"),
            Name = "Pixel Tab 2",
            Description = "Google's latest Android tablet with high-res display.",
            ImageFile = "pixel_tab_2.jpg",
            Price = 699.00m,
            Category = new List<string>{"Tablet"}
        },
        new Product()
        {
            Id = new Guid("a6c7f8a9-6a7a-9a0a-2a6f-7a8a9e0f1a2b"),
            Name = "Bose QuietComfort 55",
            Description = "Wireless noise-cancelling headphones with long battery life.",
            ImageFile = "bose_qc_55.jpg",
            Price = 349.99m,
            Category = new List<string>{"Headphones"}
        },
        new Product()
        {
            Id = new Guid("b7d8a9a0-7a8a-0a1a-3a7a-8a9a0f1a2b3c"),
            Name = "Apple Watch Series 10",
            Description = "Latest smartwatch with health and fitness tracking.",
            ImageFile = "apple_watch_10.jpg",
            Price = 499.00m,
            Category = new List<string>{"Smart Watch"}
        },
        new Product()
        {
            Id = new Guid("c8e9a0a1-8a9a-1a2a-4a8a-9a0a1f2a3b4d"),
            Name = "Dell XPS 15",
            Description = "High-performance laptop with 4K display and NVIDIA graphics.",
            ImageFile = "dell_xps_15.jpg",
            Price = 1799.00m,
            Category = new List<string>{"Laptop"}
        },
        new Product()
        {
            Id = new Guid("d9f0a1a2-9a0a-2a3a-5a9a-0a1a2f3a4b5e"),
            Name = "Samsung Galaxy Buds 3",
            Description = "True wireless earbuds with active noise cancellation.",
            ImageFile = "galaxy_buds_3.jpg",
            Price = 199.99m,
            Category = new List<string>{"Headphones"}
        },
        new Product()
        {
            Id = new Guid("e0a1a2a3-0a1a-3a4a-6a0a-1a2a3f4a5b6f"),
            Name = "Fitbit Charge 7",
            Description = "Advanced fitness tracker with heart rate and sleep monitoring.",
            ImageFile = "fitbit_charge_7.jpg",
            Price = 179.00m,
            Category = new List<string>{"Fitness Tracker"}
        }
    };
}
