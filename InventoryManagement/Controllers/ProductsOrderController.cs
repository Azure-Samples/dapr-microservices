using Dapr;
using Dapr.Client;
using Google.Api;
using InventoryManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductsOrderController : ControllerBase
{
    private readonly ILogger<ProductsOrderController> _logger;

    private const string PubSub = "inventory";

    private readonly AppDbContext _dbContext;

    public ProductsOrderController(ILogger<ProductsOrderController> logger, DaprClient daprClient, AppDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    [Topic(pubsubName: PubSub, name: "request")]
    [HttpPost(Name = "RequestMaterials")]
    public async Task<ActionResult<InventoryRequest>> Post([FromBody] ProductsOrderRequest productsOrder)
    {
        var request = new InventoryRequest
        {
            Status = InventoryRequest.Statuses.Pending,
            // the same order (id) could be sent multiple times for retry purposes
            CorrelationId = productsOrder.Id
        };

        _dbContext.Add(request);
        await _dbContext.SaveChangesAsync();

        try
        {
            foreach (var line in productsOrder.OrderDetails)
            {
                // For each product in the order, check if there is enough inventory in the dbContext
                var product = await _dbContext.Products.FirstOrDefaultAsync(p => p.Sku == line.Key);

                if (product == null || product?.Qty < line.Value)
                {
                    throw new Exception("Not enough inventory");
                }
                else
                {
                    // we have enough inventory, so we can reduce the quantity
                    product.Qty -= line.Value;
                    _dbContext.Update(product);
                }

                var requestLine = new InventoryRequestLine
                {
                    Sku = line.Key,
                    Qty = line.Value,
                    RequestId = request.Id
                };

                _dbContext.Add(requestLine);
            }

            request.Status = InventoryRequest.Statuses.Fullfilled;
            _dbContext.Update(request);

            await _dbContext.SaveChangesAsync();
        }
        catch
        {
            _dbContext.ChangeTracker.Clear();

            request.Status = InventoryRequest.Statuses.Rejected;
            _dbContext.Update(request);
            await _dbContext.SaveChangesAsync();
        }
        
        return request;
    }

    [Topic(pubsubName: PubSub, name: "return")]
    [HttpPut(Name = "ReturnMaterials")]
    public async Task<ActionResult<InventoryRequest>> Put(Guid inventoryRequestId)
    {
        var request = _dbContext.Requests.Find(inventoryRequestId);

        if (request == null)
        {
            return NotFound();
        }

        if (request.Status != InventoryRequest.Statuses.Fullfilled)
        {
            return BadRequest();
        }

        request.Status = InventoryRequest.Statuses.Returned;

        var orderLines = _dbContext.OrderLines.Where(l => l.RequestId == request.Id);

        // return products to the inventory
        foreach (var line in orderLines)
        {
            var product = _dbContext.Products.FirstOrDefault(p => p.Sku == line.Sku);

            if (product != null)
            {
                product.Qty += line.Qty;
                _dbContext.Update(product);
            }
        }
        
        await _dbContext.SaveChangesAsync();

        return request;
    }
}
