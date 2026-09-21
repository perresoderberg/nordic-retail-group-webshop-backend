using Microsoft.AspNetCore.Mvc;
using nordicretailgroup.webshop.Application;
using nordicretailgroup.webshop.Application.DTOs;

namespace nordicretailgroup.webshop.UI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController(
    ICategoryService categoryService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(
        typeof(IReadOnlyList<CategoryResponse>),
        StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<CategoryResponse>>> GetAll(
        CancellationToken cancellationToken)
    {
        var categories =
            await categoryService.GetAllAsync(cancellationToken);

        return Ok(categories);
    }
}