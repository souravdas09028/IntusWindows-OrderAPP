using IntusWindows.Common;
using IntusWindows.Common.Models;
using IntusWindows.Web.Extentions;
using IntusWindows.Web.Models;
using IntusWindows.Web.Services.Implementations;
using IntusWindows.Web.Services.Interfaces;
using MatBlazor;
using Microsoft.AspNetCore.Components;

namespace IntusWindows.Web.Pages
{
    public class WindowDialogBase : ComponentBase
    {
        public IEnumerable<ElementType> ElementTypes { get; set; } = null;
        public WindowDTO CurrentWindow { get; set; } = new WindowDTO();
        public IEnumerable<SubElementDTO> SubElements { get; set; } = null;

        [Inject]
        public IMatToaster Toaster { get; set; }

        [Inject]
        public ISubElementService SubElementService { get; set; }
        [Parameter]
        public DialogueModel<WindowDTO> WindowDialogueModel { get; set; }
        [Parameter]
        public EventCallback OnSave { get; set; }
        [Parameter]
        public EventCallback OnCancel { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            await base.OnParametersSetAsync();
            ElementTypes = (IEnumerable<ElementType>)Enum.GetValues(typeof(ElementType));
            await LoadSubElementsAsync(WindowDialogueModel.ModelDTO);
        }
        protected async Task OnSubElementChangeAsync()
        {
            await LoadForSubElementGrid();
        }

        private async Task LoadForSubElementGrid()
        {
            await LoadSubElementsAsync(CurrentWindow);
        }

        private async Task LoadSubElementsAsync(WindowDTO window)
        {
            CurrentWindow = window;

            SubElements = window.SubElements;

            TestHasItems(SubElements);

            await Task.FromResult(0);
        }

        public void TestHasItems<T>(IEnumerable<T> items)
        {
            if (CurrentWindow == null || CurrentWindow.ID == 0 || items.Any())
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
