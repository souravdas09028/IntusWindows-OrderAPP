using IntusWindows.Common.Models;

namespace IntusWindows.Web.Services.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDTO>> GetOrders();
        Task<bool> AddOrder(OrderDTO orderDTO);
        Task<bool> EditOrder(OrderDTO orderDTO);
        Task<bool> DeleteOrder(OrderDTO orderDTO);
    }
}
