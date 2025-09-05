using ProductCatalog.Application.Models;
using ProductCatalog.Dormain;

namespace ProductCatalog.Application.Contracts.Repository
{
    public interface IOrderRepository:IGenericRepository<Order>
    {
        Task<OrderResponse> CreateOrder(Order order);
        Task<(List<OrderItem>, string)> OutOfStockProduct(Order order);
        Task<IEnumerable<Order>> GetAllOrder();
    }
}
