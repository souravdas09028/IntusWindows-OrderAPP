using IntusWindows.Common;
using IntusWindows.Common.Models;
using IntusWindows.Web.Extentions;
using IntusWindows.Web.Models;
using IntusWindows.Web.Services.Implementations;
using IntusWindows.Web.Services.Interfaces;
using MatBlazor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace IntusWindows.Web.Pages
{
    public class OrderDialogBase : ComponentBase
    {
        [Inject]
        public IMatToaster Toaster { get; set; }
        [Parameter]
        public DialogueModel<OrderDTO> OrderDialogueModel { get; set; }
        [Parameter]
        public EventCallback OnSave { get; set; }
        [Parameter]
        public EventCallback OnCancel { get; set; }

        [Parameter]
        public IEnumerable<USState> States { get; set; }

        public OrderDTO CurrentOrder { get; set; } = new OrderDTO();
        public IEnumerable<WindowDTO> Windows { get; set; } = null;     


        protected override async Task OnInitializedAsync()
        {
            await LoadWindowsAsync(CurrentOrder);

            States = (IEnumerable<USState>)Enum.GetValues(typeof(USState));
        }

        protected override async Task OnParametersSetAsync()
        {
            await base.OnParametersSetAsync();
            await LoadWindowsAsync(OrderDialogueModel.ModelDTO);
        }

        protected async Task OnSubElementChangeAsync()
        {
            await LoadWindowsAsync(CurrentOrder);
        }

        public void TestHasItems<T>(IEnumerable<T> items)
        {
            if (CurrentOrder == null || CurrentOrder.ID == 0 || items.Any())
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

        protected async Task OnWindowChangeAsync()
        {
            await LoadForWindowGrid();
        }

        private async Task LoadForWindowGrid()
        {
            await LoadWindowsAsync(CurrentOrder);
        }

        private async Task LoadWindowsAsync(OrderDTO order)
        {
            CurrentOrder = order;
            Windows = order.Windows;
            TestHasItems(Windows);

            await Task.FromResult(0);
        }
    }
}
