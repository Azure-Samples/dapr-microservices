using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.Models
{
    public class Product
    {
        [Key]
        public string? Sku { get; set; }

        public string? Name { get; set; }

        public int? Qty { get; set; }
    }
}
