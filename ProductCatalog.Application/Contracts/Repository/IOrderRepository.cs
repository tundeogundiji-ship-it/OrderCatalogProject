using ProductCatalog.Dormain;

namespace ProductCatalog.Application.Contracts.Repository
{
    public interface IOrderRepository:IGenericRepository<Order>
    {
        Task<(List<OrderItem>, string message)> CreateOrder(Order order);
        Task<(List<OrderItem> itemA, List<OrderItem> itemB)> FilterOrderItem(Order order);
        IEnumerable<Order> GetAllOrder();
    }
}
