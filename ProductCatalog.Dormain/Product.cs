using ProductCatalog.Dormain.Common;

namespace ProductCatalog.Dormain
{
    public class Product:BaseDormainEntity
    {
        public string? Name {  get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity {  get; set; }
    }
}
