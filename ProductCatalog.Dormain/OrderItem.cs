using ProductCatalog.Dormain.Common;

namespace ProductCatalog.Dormain
{
    public class OrderItem:BaseDormainEntity
    {
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity {  get; set; }
        public decimal UnitPrice {  get; set; }
        public virtual Order? Order { get; set; }
        public virtual Product? Product { get; set; }

    }
}
