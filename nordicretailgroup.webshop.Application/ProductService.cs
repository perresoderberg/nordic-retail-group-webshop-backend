using nordicretailgroup.webshop.Application.DTOs;
using nordicretailgroup.webshop.Domain;

namespace nordicretailgroup.webshop.Application;

public sealed class ProductService(
    IProductRepository productRepository)
    : IProductService
{
    public async Task<PagedResponse<ProductResponse>> GetAllAsync(GetProductsRequest request,CancellationToken cancellationToken = default)
    {
        if (request.Page < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(request.Page),"Page must be greater than 0.");
        }

        if (request.PageSize is < 1 or > 100)
        {
            throw new ArgumentOutOfRangeException(nameof(request.PageSize),"PageSize must be between 1 and 100.");
        }

        var products = await productRepository.GetAllAsync(request,cancellationToken);

        var items = products.Items.Select(MapToResponse).ToList();

        var totalPages = (int)Math.Ceiling(products.TotalCount / (double)request.PageSize);

        return new PagedResponse<ProductResponse>
        {
            Items = items,
            Page = request.Page,
            PageSize = request.PageSize,
            TotalCount = products.TotalCount,
            TotalPages = totalPages
        };
    }

    public async Task<ProductResponse?> GetByIdAsync(int id,CancellationToken cancellationToken = default)
    {
        var product = await productRepository.GetByIdAsync(id,cancellationToken);

        return product is null ? null : MapToResponse(product);
    }

    public Task<bool> DeleteAsync(int id,CancellationToken cancellationToken = default)
    {
        return productRepository.DeleteAsync(id, cancellationToken);
    }

    public async Task<ProductResponse?> UpdateAsync(int id, UpdateProductRequest request, CancellationToken cancellationToken = default)
    {
        var product = await productRepository.UpdateAsync(id,request,cancellationToken);

        return product is null ? null : MapToResponse(product);
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

            Category = product.Category is null ? null : new CategoryResponse
                {
                    Id = product.Category.Id,
                    Name = product.Category.Name,
                    Slug = product.Category.Slug,
                    Image = product.Category.Image
                }
        };
    }
}