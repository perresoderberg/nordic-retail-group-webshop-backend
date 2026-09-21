using System;
using System.Collections.Generic;
using System.Text;

namespace nordicretailgroup.webshop.Application.DTOs;

public sealed class GetProductsRequest
{
    public string? Search { get; init; }
    public int? CategoryId { get; init; }
    public decimal? MinPrice { get; init; }
    public decimal? MaxPrice { get; init; }

    public string? SortBy { get; init; }

    public bool Ascending { get; init; } = true;
}