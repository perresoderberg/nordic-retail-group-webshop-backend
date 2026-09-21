using nordicretailgroup.webshop.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace nordicretailgroup.webshop.Application;

public interface ICategoryService
{
    Task<IReadOnlyList<CategoryResponse>> GetAllAsync(
        CancellationToken cancellationToken = default);
}
