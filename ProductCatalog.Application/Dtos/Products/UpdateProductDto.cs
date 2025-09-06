using ProductCatalog.Application.Dtos.Common;
using System.Text.Json.Serialization;


namespace ProductCatalog.Application.Dtos.Products
{
    public class UpdateProductDto : IProductDto
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public string? Name { get ; set ; }
        public string? Description { get ; set ; }
        public decimal Price { get ; set ; }
        public int StockQuantity { get ; set ; }
    }
}
