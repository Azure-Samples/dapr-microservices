using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.Controllers
{
    public class ProductsOrderRequest
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public IDictionary<string, int> OrderDetails { get; set; } = new Dictionary<string, int>();
    }
}