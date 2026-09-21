using nordicretailgroup.webshop.Application.DTOs;
using nordicretailgroup.webshop.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace nordicretailgroup.webshop.Application;

public interface IProductRepository
{
    Task<IReadOnlyList<Product>> GetAllAsync(
        GetProductsRequest request,
        CancellationToken cancellationToken = default);

    Task<Product?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken = default);

    Task<bool> DeleteAsync(
        int id,
        CancellationToken cancellationToken = default);

    Task<Product?> UpdateAsync(
        int id,
        UpdateProductRequest request,
        CancellationToken cancellationToken = default);
}