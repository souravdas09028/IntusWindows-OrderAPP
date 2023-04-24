using IntusWindows.Common.Models;

namespace IntusWindows.Web.Services.Interfaces
{
    public interface IWindowService
    {
        Task<IEnumerable<WindowDTO>> GetWindows(int orderId);
        Task<bool> DeleteWindow(WindowDTO window);
    }
}
