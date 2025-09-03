using ProductCatalog.Dormain;

namespace ProductCatalog.Application.Contracts.Repository
{
    public interface IProductRepository:IGenericRepository<Product>
    {
        Task<bool> IsProductExist(string name);
        Task<IEnumerable<Product>> GetProducts();
        Task<Product?> GetProduct(Guid productId);
    }
}
