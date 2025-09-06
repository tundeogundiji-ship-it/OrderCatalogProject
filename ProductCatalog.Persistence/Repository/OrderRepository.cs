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

            if (!string.IsNullOrWhiteSpace(orderResult.Item2))
            {
                await transaction.RollbackAsync();

                response.message = orderResult.Item2.ToString();

                return response;
            }

            List<Product> products = new();
            //create order
            order.TotalAmount = orderResult.Item1;
            var orderEntity = await _dbContext.Orders.AddAsync(order);

            foreach (var item in order.OrderItems!)
            {
                //deduct product
                var product = await _dbContext.Products.FirstOrDefaultAsync(x => x.Id == item.ProductId);
                product!.StockQuantity -= item.Quantity!; 

                products.Add(product);
                
                //create  order item
                item.OrderId = orderEntity.Entity.Id;
                item.UnitPrice = product.Price;
            }

            _dbContext.Products.UpdateRange(products);
            await _dbContext.OrderItems.AddRangeAsync(order.OrderItems);

            await _dbContext.SaveChangesAsync();
            await transaction.CommitAsync();

            response.message = ResponseMessageConstant.SuccessfulOrderMessage;
            response.orderId = orderEntity.Entity.Id;

            return response;

        }

        public async Task<(decimal,string)> OutOfStockProduct(Order order)
        {
            StringBuilder error = new StringBuilder();
            var outOfStockItems = new List<OrderItem>();
            decimal TotalAmount = 0M;
            foreach (var item in order.OrderItems!)
            {
                Product? product = await _dbContext.Products.FirstOrDefaultAsync(x => x.Id == item.ProductId);
                if (item.Quantity>product!.StockQuantity)
                {
                    error.Append($"Product Id {item.ProductId} has  quantity greater than what we have in the stock");
                    outOfStockItems.Add(item);
                }
                else
                {
                    TotalAmount += (product.Price* item.Quantity);
                }
            }

            return (TotalAmount,error.ToString());

        }

       

        public async Task<IEnumerable<Order>> GetAllOrder(Guid userId)
        {
            var orders = await _dbContext.Orders
                             .Include(y => y.OrderItems!)
                             .Where(x=>x.UserId==userId)
                             .ToListAsync();

            return orders!;
        }
    }
}
