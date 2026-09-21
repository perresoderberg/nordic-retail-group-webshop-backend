using nordicretailgroup.webshop.Application.DTOs;
using nordicretailgroup.webshop.Domain;

namespace nordicretailgroup.webshop.Application;

public sealed class ProductService(
    IProductRepository productRepository)
    : IProductService
{
    public async Task<IReadOnlyList<ProductResponse>> GetAllAsync(
        GetProductsRequest request,
        CancellationToken cancellationToken = default)
    {
        var products = await productRepository.GetAllAsync(
            request,
            cancellationToken);

        return products
            .Select(MapToResponse)
            .ToList();
    }

    public async Task<ProductResponse?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        var product = await productRepository.GetByIdAsync(
            id,
            cancellationToken);

        return product is null
            ? null
            : MapToResponse(product);
    }

    public Task<bool> DeleteAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        return productRepository.DeleteAsync(
            id,
            cancellationToken);
    }

    public async Task<ProductResponse?> UpdateAsync(
        int id,
        UpdateProductRequest request,
        CancellationToken cancellationToken = default)
    {
        var product = await productRepository.UpdateAsync(
            id,
            request,
            cancellationToken);

        return product is null
            ? null
            : MapToResponse(product);
    }

    private static ProductResponse MapToResponse(Product product)
    {
        return new ProductResponse
        {
            Id = product.Id,
            Title = product.Title,
            Description = product.Description,
            Price = product.Price,
            DiscountPercentage = product.DiscountPercentage,
            Rating = product.Rating,
            Stock = product.Stock,
            Brand = product.Brand,
            Sku = product.Sku,
            Weight = product.Weight,
            WarrantyInformation = product.WarrantyInformation,
            ShippingInformation = product.ShippingInformation,
            AvailabilityStatus = product.AvailabilityStatus,
            ReturnPolicy = product.ReturnPolicy,
            MinimumOrderQuantity = product.MinimumOrderQuantity,
            Thumbnail = product.Thumbnail,
            CategoryId = product.CategoryId,

            Category = product.Category is null
                ? null
                : new CategoryResponse
                {
                    Id = product.Category.Id,
                    Name = product.Category.Name,
                    Slug = product.Category.Slug,
                    Image = product.Category.Image
                }
        };
    }
}