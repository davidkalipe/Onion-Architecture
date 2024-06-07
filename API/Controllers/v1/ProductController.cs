using API.DTO;
using Application.Features.ProductFeatures.Commands;
using Application.Features.ProductFeatures.Queries;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.v1;

[ApiVersion("1.0")]
public class ProductController : BaseApiController
{
    /// <summary>
    /// Create a new product
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Create(CreateProductCommand command)
    {
        try
        {
            return Ok(await Mediator.Send(command));
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    
    /// <summary>
    /// Gets all Products
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var products = await Mediator.Send(new GetAllProductsQuery());
        var productsDto = Mapper.Map<List<GetAllProductDto>>(products);
        return Ok(productsDto);
    }

    ///<summary>
    /// Updates the Product Entity based on Id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPut("[action]")]
    public async Task<IActionResult> Update(Guid id, UpdateProductCommand command)
    {
        try
        {
            if (id != command.Id)
                return NotFound("Product not found");
            return Ok(await Mediator.Send(command));
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    
    ///<summary>
    /// Deletes product Entity based on Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        return Ok(await Mediator.Send(new DeleteProductCommand { Id = id }));
    }
}