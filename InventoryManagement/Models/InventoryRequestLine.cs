using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.Models
{
    [PrimaryKey("Sku", "RequestId")]
    public class InventoryRequestLine
    {
        public string? Sku { get; set; }   

        public int Qty { get; set; }

        public Guid RequestId { get; set; }
    }
}