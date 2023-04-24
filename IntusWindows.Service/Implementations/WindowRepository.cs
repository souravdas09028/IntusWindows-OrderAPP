using IntusWindows.Core.Data;
using IntusWindows.Core.Entities;
using IntusWindows.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntusWindows.Service.Implementations
{
    public class WindowRepository : IWindowRepository
    {
        private readonly IntusWindowsDbContext dbContext;

        public WindowRepository(IntusWindowsDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task Delete(int id)
        {
            var item = await dbContext.Windows.FindAsync(id);

            if (item != null)
            {
                dbContext.Windows.Remove(item);
            }
        }

        public async Task<IEnumerable<Window>> GetByOrderID(int orderID)
        {
            var windows = await dbContext.Windows.Where(window => window.OrderId == orderID)
                .Include(window => window.SubElements)
               .AsQueryable().ToListAsync();

            return windows;
        }
    }
}
