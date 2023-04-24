using IntusWindows.Common.Models;
using IntusWindows.Web.Services.Interfaces;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace IntusWindows.Web.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly HttpClient httpClient;
        public OrderService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<bool> AddOrder(OrderDTO orderDTO)
        {
            try
            {
                var response = await httpClient.PostAsJsonAsync("api/Order", orderDTO);

                if (response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadAsStringAsync();
                    JsonConvert.DeserializeObject<OrderDTO>(responseBody);
                    return true;
                }
                else
                {
                    Console.WriteLine("Failed to create order");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception thrown while {nameof(AddOrder)}.", ex);
            }

            return false;
        }

        public async Task<bool> DeleteOrder(OrderDTO order)
        {
            try
            {
                var response = await httpClient.DeleteAsync($"api/Order/{order.ID}");

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    Console.WriteLine("Failed to delete order");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception thrown while {nameof(DeleteOrder)}.", ex);
            }

            return false;
        }

        public async Task<bool> EditOrder(OrderDTO orderDTO)
        {
            try
            {
                var response = await httpClient.PutAsJsonAsync("api/Order", orderDTO);

                if (response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadAsStringAsync();
                    JsonConvert.DeserializeObject<OrderDTO>(responseBody);
                    return true;
                }
                else
                {
                    Console.WriteLine("Failed to edit order");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception thrown while {nameof(EditOrder)}.", ex);
            }

            return false;
        }

        public async Task<IEnumerable<OrderDTO>> GetOrders()
        {
            try
            {
                var orders = await httpClient.GetFromJsonAsync<IEnumerable<OrderDTO>>("api/order");
                return orders;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception thrown while {nameof(GetOrders)}.", ex);
            }

            return new List<OrderDTO>();
        }
    }
}
