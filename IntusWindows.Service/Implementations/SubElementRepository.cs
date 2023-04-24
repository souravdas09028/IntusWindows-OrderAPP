using IntusWindows.Common;
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
    public class SubElementRepository : ISubElementRepository
    {
        private readonly IntusWindowsDbContext dbContext;

        public SubElementRepository(IntusWindowsDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task Delete(int id)
        {
            var element = await dbContext.SubElements.FindAsync(id);

            if (element != null)
            {
                dbContext.Remove(element);
            }
        } 

        public async Task<IEnumerable<SubElement>> GetByWindowID(int windowID)
        {
            var subElements = await dbContext.SubElements.Where(element => element.WindowId == windowID)
               .AsQueryable().ToListAsync();

            return subElements;
        }

        public async Task<SubElement> Update(SubElement subElement)
        {  
            if (subElement == null)
            {
                throw new ArgumentNullException("Sub element can not be null");
            }

            dbContext.Update(subElement);

            await dbContext.SaveChangesAsync();

            return subElement;
        }
    }
}
