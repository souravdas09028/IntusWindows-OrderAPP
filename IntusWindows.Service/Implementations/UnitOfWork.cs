using IntusWindows.Core.Data;
using IntusWindows.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntusWindows.Service.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IntusWindowsDbContext dbContext;

        public UnitOfWork(IntusWindowsDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IOrderRepository ordrRepository => new OrderRepository(dbContext);
        public IWindowRepository windowRepository => new WindowRepository(dbContext);
        public ISubElementRepository subElementRepository => new SubElementRepository(dbContext);

        public async Task<bool> SaveAsync()
        {
            return await dbContext.SaveChangesAsync() > 0;
        }
    }
}
