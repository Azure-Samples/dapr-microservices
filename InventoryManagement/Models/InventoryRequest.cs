using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.Models
{
    public class InventoryRequest
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid CorrelationId { get; set; }

        public string Status { get; set; } = Statuses.Pending;

        public IEnumerable<InventoryRequestLine> OrderLines { get; set; } = new List<InventoryRequestLine>();

        public class Statuses
        {
            public const string Pending = "Pending";

            public const string Fullfilled = "Fullfilled";

            public const string Rejected = "Rejected";

            public const string Cancelled = "Cancelled";

            public const string Returned = "Returned";
        }
    }
}