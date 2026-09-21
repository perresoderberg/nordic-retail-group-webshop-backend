using System;
using System.Collections.Generic;
using System.Text;

namespace nordicretailgroup.webshop.Application.DTOs;

public sealed class CategoryResponse
{
    public int Id { get; init; }

    public string Name { get; init; } = string.Empty;

    public string Slug { get; init; } = string.Empty;

    public string? Image { get; init; }
}