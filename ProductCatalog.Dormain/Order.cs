using ProductCatalog.Dormain.Common;

namespace ProductCatalog.Dormain
{
    public class Order: BaseDormainEntity
    {
        public Guid UserId { get; set; }
        public decimal TotalAmount {  get; set; }
        public DateTime OrderDate { get; set; }
        public User? User { get; set; }
    }
}
