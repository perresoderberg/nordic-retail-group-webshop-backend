using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using nordicretailgroup.webshop.Application;
using nordicretailgroup.webshop.Application.DTOs;

namespace nordicretailgroup.webshop.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController(IProductService productService) : ControllerBase
{
    
    [Authorize]
    [HttpGet("who-am-i")]
    public IActionResult WhoAmI()
    {
        return Ok(new
        {
            userId = User.FindFirst("sub")?.Value,
            email = User.FindFirst("email")?.Value
        });
    }

    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<ProductResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<ProductResponse>>> GetAll([FromQuery] GetProductsRequest request,CancellationToken cancellationToken)
    {
        var products = await productService.GetAllAsync(request,cancellationToken);

        return Ok(products);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(
        typeof(ProductResponse),
        StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProductResponse>> GetById(int id,CancellationToken cancellationToken)
    {
        var product = await productService.GetByIdAsync(id,cancellationToken);

        if (product is null)
        {
            return NotFound();
        }

        return Ok(product);
    }

    [Authorize]
    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(ProductResponse),StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProductResponse>> Update(int id,[FromBody] UpdateProductRequest request,CancellationToken cancellationToken)
    {
        var product = await productService.UpdateAsync(id,request,cancellationToken);

        if (product is null)
        {
            return NotFound();
        }

        return Ok(product);
    }

    [Authorize]
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id,CancellationToken cancellationToken){
        var deleted = await productService.DeleteAsync(id,cancellationToken);

        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}
