namespace ShopSphere.Domain.Entities;

public class ProductItem
{
    public int Id { get; set; }

    public int ProductId { get; set; }
    public Product? Product { get; set; }

    public string SKU { get; set; } = string.Empty;
    public string Color { get; set; } = string.Empty;
    public string Size { get; set; } = string.Empty;

    public decimal Price { get; set; }
    public int Stock { get; set; }
}