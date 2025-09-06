
using System.Text.Json.Serialization;

namespace ProductCatalog.Application.Dtos.Orders
{
    public class OrderItemDto
    {
        [JsonIgnore]
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        [JsonIgnore]
        public decimal UnitPrice { get; set; }
    }
}
