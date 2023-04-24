using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntusWindows.Service.Interfaces
{
    public interface IUnitOfWork
    {
        IOrderRepository ordrRepository { get; }
        IWindowRepository windowRepository { get; }
        ISubElementRepository subElementRepository { get; }
        Task<bool> SaveAsync();
    }
}
