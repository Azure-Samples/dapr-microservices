using Dapr;
using Dapr.Client;
using InventoryManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductsController : ControllerBase
{
    private readonly ILogger<ProductsController> _logger;

    private readonly AppDbContext _dbContext;

    public ProductsController(ILogger<ProductsController> logger, DaprClient daprClient, AppDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    [HttpPost(Name = "CreateProduct")]
    public async Task<ActionResult<Product>> Post([FromBody] Product product)
    {
        _dbContext.Add(product);
        await _dbContext.SaveChangesAsync();

        return product;
    }

    [HttpPut("{sku}", Name = "UpdateProduct")]
    public async Task<ActionResult<Product>> Put(string sku, [FromBody] Product product)
    {
        if (sku != product.Sku)
        {
            return BadRequest();
        }

        _dbContext.Entry(product).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();

        return product;
    }

    [HttpDelete("{sku}", Name = "DeleteProduct")]
    public async Task<ActionResult<Product>> Delete(string sku)
    {
        var product = await _dbContext.Products.FirstOrDefaultAsync(p => p.Sku == sku);

        if (product == null)
        {
            return NotFound();
        }

        _dbContext.Remove(product);
        await _dbContext.SaveChangesAsync();

        return product;
    }
}
