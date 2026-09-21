using Microsoft.EntityFrameworkCore;
using nordicretailgroup.webshop.Application;
using nordicretailgroup.webshop.Application.DTOs;
using nordicretailgroup.webshop.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace nordicretailgroup.webshop.Infrastructure;

public sealed class ProductRepository(AppDbContext dbContext)
    : IProductRepository
{
    public async Task<IReadOnlyList<Product>> GetAllAsync(
        GetProductsRequest request,
        CancellationToken cancellationToken = default)
    {
        var query = dbContext.Products
            .AsNoTracking()
            .Include(x => x.Category)
            .AsQueryable();

        // Filtering
        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            var search = request.Search.Trim().ToLowerInvariant();

            query = query.Where(x =>
                x.Title.ToLower().Contains(search));
        }

        if (request.CategoryId.HasValue)
        {
            query = query.Where(x =>
                x.CategoryId == request.CategoryId.Value);
        }

        if (request.MinPrice.HasValue)
        {
            query = query.Where(x =>
                x.Price >= request.MinPrice.Value);
        }

        if (request.MaxPrice.HasValue)
        {
            query = query.Where(x =>
                x.Price <= request.MaxPrice.Value);
        }

        // Sorting
        query = request.SortBy?.ToLowerInvariant() switch
        {
            "title" => request.Ascending
                ? query.OrderBy(x => x.Title)
                : query.OrderByDescending(x => x.Title),

            "price" => request.Ascending
                ? query.OrderBy(x => x.Price)
                : query.OrderByDescending(x => x.Price),

            "rating" => request.Ascending
                ? query.OrderBy(x => x.Rating)
                : query.OrderByDescending(x => x.Rating),

            "stock" => request.Ascending
                ? query.OrderBy(x => x.Stock)
                : query.OrderByDescending(x => x.Stock),

            _ => query.OrderBy(x => x.Id)
        };

        return await query.ToListAsync(cancellationToken);
    }

    public async Task<Product?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        return await dbContext.Products
            .AsNoTracking()
            .Include(x => x.Category)
            .SingleOrDefaultAsync(
                x => x.Id == id,
                cancellationToken);
    }

    public async Task<bool> DeleteAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        var product = await dbContext.Products
            .SingleOrDefaultAsync(
                x => x.Id == id,
                cancellationToken);

        if (product is null)
        {
            return false;
        }

        dbContext.Products.Remove(product);

        await dbContext.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task<Product?> UpdateAsync(
        int id,
        UpdateProductRequest request,
        CancellationToken cancellationToken = default)
    {
        var product = await dbContext.Products
            .SingleOrDefaultAsync(
                x => x.Id == id,
                cancellationToken);

        if (product is null)
        {
            return null;
        }

        product.Title = request.Title;
        product.Description = request.Description;
        product.Price = request.Price;
        product.DiscountPercentage = request.DiscountPercentage;
        product.Stock = request.Stock;
        product.Brand = request.Brand;
        product.Weight = request.Weight;
        product.WarrantyInformation = request.WarrantyInformation;
        product.ShippingInformation = request.ShippingInformation;
        product.AvailabilityStatus = request.AvailabilityStatus;
        product.ReturnPolicy = request.ReturnPolicy;
        product.MinimumOrderQuantity = request.MinimumOrderQuantity;
        product.Thumbnail = request.Thumbnail;
        product.CategoryId = request.CategoryId;

        await dbContext.SaveChangesAsync(cancellationToken);

        return await dbContext.Products
            .AsNoTracking()
            .Include(x => x.Category)
            .SingleAsync(
                x => x.Id == id,
                cancellationToken);
    }
}