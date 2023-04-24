using IntusWindows.Core.Entities;

namespace IntusWindows.Service.Interfaces
{
    public interface IWindowRepository
    {
        Task<IEnumerable<Window>> GetByOrderID(int id);
        Task Delete(int id);
    }
}
