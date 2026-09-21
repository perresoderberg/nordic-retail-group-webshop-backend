using nordicretailgroup.webshop.Application.DTOs;
using nordicretailgroup.webshop.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace nordicretailgroup.webshop.Application;

public interface IProductService
{
    Task<IReadOnlyList<ProductResponse>> GetAllAsync(
        GetProductsRequest request,
        CancellationToken cancellationToken = default);

    Task<ProductResponse?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken = default);

    Task<bool> DeleteAsync(
        int id,
        CancellationToken cancellationToken = default);

    Task<ProductResponse?> UpdateAsync(
        int id,
        UpdateProductRequest request,
        CancellationToken cancellationToken = default);
}