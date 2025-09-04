using System.Text.Json.Serialization;

namespace ProductCatalog.Application.Dtos.Orders
{
    public class CreateOrderDto
    {
        [JsonIgnore]
        public Guid UserId { get; set; }

        [JsonIgnore]
        public decimal TotalAmount { get; set; }

        [JsonIgnore]
        public DateTime OrderDate { get; set; }

        public ICollection<OrderItemDto>? OrderItems { get; set; }
    }
}
