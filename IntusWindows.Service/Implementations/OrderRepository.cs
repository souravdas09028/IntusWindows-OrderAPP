using IntusWindows.Core.Data;
using IntusWindows.Core.Entities;
using IntusWindows.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IntusWindows.Service.Implementations
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IntusWindowsDbContext dbContext;

        public OrderRepository(IntusWindowsDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public void Add(Order order)
        {
            dbContext.Add(order);
        }

        public Task Update(Order model)
        {
            dbContext.Update(model);

            return Task.CompletedTask;
        }

        public async Task<IEnumerable<Order>> GetAll()
        {
            var orders = await dbContext.Orders.Include(order => order.Windows)
                .ThenInclude(window => window.SubElements)
                .AsQueryable().ToListAsync();

            return orders;
        }

        public async Task Delete(int id)
        {
            var item = await dbContext.Orders.FindAsync(id);

            if (item != null)
            {
                dbContext.Orders.Remove(item);
            }
        }
    }
}
