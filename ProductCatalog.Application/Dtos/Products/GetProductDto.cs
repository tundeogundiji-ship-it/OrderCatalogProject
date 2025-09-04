using ProductCatalog.Application.Dtos.Common;


namespace ProductCatalog.Application.Dtos.Products
{
    public class GetProductDto : BaseDto, IProductDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public DateTime DateCreated { get; set; }
        public string? CreatedBy {  get; set; }
    }
}
