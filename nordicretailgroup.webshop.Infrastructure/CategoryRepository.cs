using Microsoft.EntityFrameworkCore;
using nordicretailgroup.webshop.Application;
using nordicretailgroup.webshop.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace nordicretailgroup.webshop.Infrastructure;

public sealed class CategoryRepository(AppDbContext dbContext)
    : ICategoryRepository
{
    public async Task<IReadOnlyList<Category>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        return await dbContext.Categories
            .AsNoTracking()
            .OrderBy(x => x.Name)
            .ToListAsync(cancellationToken);
    }
}
