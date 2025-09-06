using ProductCatalog.Dormain;


namespace ProductCatalog.Application.Dtos.Orders
{
    public class GetOrderItemDto
    {
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public virtual Product? Product { get; set; }
    }
}
