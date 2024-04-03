using EatGoodNaija.Server.Data;
using EatGoodNaija.Server.Model;
using EatGoodNaija.Server.Model.DTO;
using EatGoodNaija.Server.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace EatGoodNaija.Server.Services.Implementation
{
    public class FoodOrderRepository : IFoodOrderRepository
    {
        private readonly DataContext _context;
        public FoodOrderRepository(DataContext context)
        {
            _context=context;
        }


        public async Task<List<OrderDto>> GetOrdersForCustomerAsync(string customerId, int page, int pageSize)
        {
            var orders = await _context.Orders
            .Where(o => o.CustomerId == customerId)
            .OrderByDescending(o => o.OrderDate)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

            return orders.Select(order => new OrderDto
            {
                Id = order.Id,
                OrderDate = order.OrderDate,
                Items = order.Items.Select(item => new CartItemDto
                {
                    Id = item.Id,
                    FoodItemId = item.FoodItemId,
                    Quantity = item.Quantity
                }).ToList()
            }).ToList();
        }

        public async Task<Order> ReorderAsync(string customerId, string orderId)
        {
            var originalOrder = await _context.Orders
                .Include(o => o.Items)
                .FirstOrDefaultAsync(o => o.Id == orderId);

            if (originalOrder == null)
                throw new ArgumentException("Order not found");

            var newOrder = new Order
            {
                OrderDate = DateTime.Now,
                CustomerId = customerId
            };

            foreach (var item in originalOrder.Items)
            {
                newOrder.Items.Add(new CartItem { FoodItemId = item.FoodItemId, Quantity = item.Quantity });
            }

            _context.Orders.Add(newOrder);
            await _context.SaveChangesAsync();

            return newOrder;
        }


    }
}
