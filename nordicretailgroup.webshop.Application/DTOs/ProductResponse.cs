using System;
using System.Collections.Generic;
using System.Text;

namespace nordicretailgroup.webshop.Application.DTOs;

public sealed class ProductResponse
{
    public int Id { get; init; }

    public string Title { get; init; } = string.Empty;

    public string Description { get; init; } = string.Empty;

    public decimal Price { get; init; }

    public decimal DiscountPercentage { get; init; }

    public decimal Rating { get; init; }

    public int Stock { get; init; }

    public string? Brand { get; init; }

    public string Sku { get; init; } = string.Empty;

    public decimal Weight { get; init; }

    public string? WarrantyInformation { get; init; }

    public string? ShippingInformation { get; init; }

    public string? AvailabilityStatus { get; init; }

    public string? ReturnPolicy { get; init; }

    public int MinimumOrderQuantity { get; init; }

    public string? Thumbnail { get; init; }

    public int CategoryId { get; init; }

    public CategoryResponse? Category { get; init; }
}
