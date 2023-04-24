using IntusWindows.Common.Models;
using IntusWindows.Web.Services.Interfaces;
using System.Net.Http.Json;

namespace IntusWindows.Web.Services.Implementations
{
    public class WindowService : IWindowService
    {
        private readonly HttpClient httpClient;
        public WindowService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<bool> DeleteWindow(WindowDTO window)
        {
            try
            {
                var response = await httpClient.DeleteAsync($"api/Windows/{window.ID}");

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    Console.WriteLine("Failed to delete window");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception thrown while {nameof(DeleteWindow)}.", ex);
            }

            return false;
        }

        public async Task<IEnumerable<WindowDTO>> GetWindows(int orderId)
        {
            try
            {
                var windows = await httpClient.GetFromJsonAsync<IEnumerable<WindowDTO>>($"api/Windows/for/{orderId}");
                return windows;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception thrown while {nameof(GetWindows)}.", ex);
            }

            return new List<WindowDTO>();
        }
    }
}
