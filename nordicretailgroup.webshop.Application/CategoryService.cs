using nordicretailgroup.webshop.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace nordicretailgroup.webshop.Application;

public sealed class CategoryService(
    ICategoryRepository categoryRepository)
    : ICategoryService
{
    public async Task<IReadOnlyList<CategoryResponse>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        var categories =
            await categoryRepository.GetAllAsync(cancellationToken);

        return categories
            .Select(x => new CategoryResponse
            {
                Id = x.Id,
                Name = x.Name,
                Slug = x.Slug,
                Image = x.Image
            })
            .ToList();
    }
}
