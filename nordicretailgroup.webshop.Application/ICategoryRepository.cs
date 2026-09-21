using nordicretailgroup.webshop.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace nordicretailgroup.webshop.Application;

public interface ICategoryRepository
{
    Task<IReadOnlyList<Category>> GetAllAsync(
        CancellationToken cancellationToken = default);
}
