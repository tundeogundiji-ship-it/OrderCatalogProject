using Microsoft.EntityFrameworkCore;
using ProductCatalog.Application.Contracts.Repository;
using ProductCatalog.Dormain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalog.Persistence.Repository
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        private readonly ProductCatalogDbContext _dbContext;
        public OrderRepository(ProductCatalogDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<(List<OrderItem>,string message)> CreateOrder(Order order)
        {
            using var transaction = await _dbContext.Database.BeginTransactionAsync();

            // first remove orderItem that is out of stock
            var orderItems = await FilterOrderItem(order);

            if (orderItems.itemB.Count > 0)
            {
                await transaction.RollbackAsync();

                return (orderItems.itemB,"Items out of stock");
            }

            //create order
            var orderEntity = await _dbContext.Orders.AddAsync(order);

            foreach (var item in orderItems.itemA)
            {
                //deduct product
                await _dbContext.Products.Where(x => x.Id == item.ProductId)
                    .ExecuteUpdateAsync(x => x.SetProperty(c => c.StockQuantity, c => c.StockQuantity - item.Quantity!));

                //create  order item
                item.OrderId = orderEntity.Entity.Id;
            }

            await _dbContext.OrderItems.AddRangeAsync(orderItems.itemA);

            await _dbContext.SaveChangesAsync();
            await transaction.CommitAsync();

            return (orderItems.itemA.ToList(),"successful processed");

        }

        public async Task<(List<OrderItem> itemA,List<OrderItem> itemB)> FilterOrderItem(Order order)
        {
            // first remove orderItem that is out of stock
            var orderItems = new List<OrderItem>();
            var outOfStockItems = new List<OrderItem>();

            foreach (var item in order.OrderItems!)
            {
                Product? product = await _dbContext.Products.FirstOrDefaultAsync(x => x.Id == item.Id);
                if (product!.StockQuantity >= item.Quantity)
                {
                    orderItems.Add(item);
                }

                outOfStockItems.Add(item);
            }

            return (orderItems, outOfStockItems);

        }

        public IEnumerable<Order> GetAllOrder()
        {
            var orders =  _dbContext.Orders.Include(x => x.User!)
                             .Include(y => y.OrderItems!)
                             .ThenInclude(q => q.Product)
                             .AsEnumerable();

            return orders!;
        }
    }
}
