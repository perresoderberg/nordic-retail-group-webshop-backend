namespace nordicretailgroup.webshop.Domain;

public class Product
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public decimal DiscountPercentage { get; set; }
    public decimal Rating { get; set; }
    public int Stock { get; set; }
    public string? Brand { get; set; }
    public string Sku { get; set; } = string.Empty;
    public decimal Weight { get; set; }
    public string? WarrantyInformation { get; set; }
    public string? ShippingInformation { get; set; }
    public string? AvailabilityStatus { get; set; }
    public string? ReturnPolicy { get; set; }
    public int MinimumOrderQuantity { get; set; }
    public string? Thumbnail { get; set; }

    public int CategoryId { get; set; }
    public Category? Category { get; set; }
}