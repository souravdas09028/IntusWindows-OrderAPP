using IntusWindows.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntusWindows.Service.Interfaces
{
    public interface ISubElementRepository
    {
        Task<IEnumerable<SubElement>> GetByWindowID(int id);
        Task<SubElement> Update(SubElement model);
        Task Delete(int id);
    }
}
