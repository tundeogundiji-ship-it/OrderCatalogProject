using Microsoft.EntityFrameworkCore;
using ProductCatalog.Application.Constants;
using ProductCatalog.Application.Contracts.Repository;
using ProductCatalog.Application.Models;
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

        public async Task<OrderResponse> CreateOrder(Order order)
        {
            OrderResponse response = new();
            StringBuilder error = new StringBuilder();
            using var transaction = await _dbContext.Database.BeginTransactionAsync();

            // first remove orderItem that quantity is more than what we have in stock 
            var orderResult = await OutOfStockProduct(order);

            if (orderResult.Item1.Count > 0)
            {
                await transaction.RollbackAsync();

                response.message = orderResult.Item2.ToString();

                return response;
            }

            //create order
            var orderEntity = await _dbContext.Orders.AddAsync(order);

            foreach (var item in order.OrderItems!)
            {
                //deduct product
                await _dbContext.Products.Where(x => x.Id == item.ProductId)
                    .ExecuteUpdateAsync(x => x.SetProperty(c => c.StockQuantity, c => c.StockQuantity - item.Quantity!));

                //create  order item
                item.OrderId = orderEntity.Entity.Id;
            }

            await _dbContext.OrderItems.AddRangeAsync(order.OrderItems);

            await _dbContext.SaveChangesAsync();
            await transaction.CommitAsync();

            response.message = ResponseMessageConstant.SuccessfulOrderMessage;
            response.orderId = orderEntity.Entity.Id;

            return response;

        }

        public async Task<(List<OrderItem>,string)> OutOfStockProduct(Order order)
        {
            StringBuilder error = new StringBuilder();
            var outOfStockItems = new List<OrderItem>();

            foreach (var item in order.OrderItems!)
            {
                Product? product = await _dbContext.Products.FirstOrDefaultAsync(x => x.Id == item.Id);
                if (item.Quantity>product!.StockQuantity)
                {
                    error.Append($"Product Id {item.Id} has  quantity greater than what we have in the stock");
                    outOfStockItems.Add(item);
                }
            }

            return (outOfStockItems,error.ToString());

        }

        public async Task<IEnumerable<Order>> GetAllOrder()
        {
            var orders = await _dbContext.Orders.Include(x => x.User!)
                             .Include(y => y.OrderItems!)
                             .ThenInclude(q => q.Product)
                             .ToListAsync();

            return orders!;
        }
    }
}
