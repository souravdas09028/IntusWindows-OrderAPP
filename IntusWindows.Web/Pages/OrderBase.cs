using IntusWindows.Common;
using IntusWindows.Common.Models;
using IntusWindows.Web.Extentions;
using IntusWindows.Web.Services.Interfaces;
using MatBlazor;
using Microsoft.AspNetCore.Components;

namespace IntusWindows.Web.Pages
{
    public class OrderBase : ComponentBase
    {
        [Inject]
        public IOrderService OrderService { get; set; }
        [Inject]
        public IMatToaster Toaster { get; set; }
        public IEnumerable<OrderDTO> Orders { get; set; } = null;
        public IEnumerable<WindowDTO> Windows { get; set; } = null;
        public IEnumerable<SubElementDTO> SubElements { get; set; } = null;
        public IEnumerable<ElementType> ElementTypes { get; set; } = null;
        public OrderDTO CurrentOrder { get; set; } = new OrderDTO();
        public WindowDTO CurrentWindow { get; set; } = new WindowDTO();     

        protected override async Task OnInitializedAsync()
        {
            await LoadForOrderGrid();

            ElementTypes = (IEnumerable<ElementType>)Enum.GetValues(typeof(ElementType));
        }

        protected async Task OnOrderChangeAsync()
        {
            await LoadForOrderGrid();
        }

        private async Task LoadForOrderGrid()
        {
            await LoadOrdersAsync();
        }

        private async Task LoadOrdersAsync()
        {
            Orders = await OrderService.GetOrders();
            TestHasItems(Orders);
        }

        public void TestHasItems<T>(IEnumerable<T> items)
        {
            if (items.Any())
            {
                return;
            }

            var dataModelTypeMap = new Dictionary<Type, string>
            {
                { typeof(OrderDTO), DataModelType.Order },
                { typeof(WindowDTO), DataModelType.Window },
                { typeof(SubElementDTO), DataModelType.SubElement }
            };

            var dataType = typeof(T);
            if (dataModelTypeMap.TryGetValue(dataType, out var dataModelType))
            {
                Toaster.DataNotFound(dataModelType);
            }
        }
    }
}
