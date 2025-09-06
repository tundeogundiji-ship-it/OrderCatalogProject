using ProductCatalog.Dormain;


namespace ProductCatalog.Application.Dtos.Orders
{
    public class GetOrderDto
    {
        public Guid UserId { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime OrderDate { get; set; }
        public string? CreatedBy { get; set; }
       

        public ICollection<GetOrderItemDto>? OrderItems { get; set; }
    }
}
