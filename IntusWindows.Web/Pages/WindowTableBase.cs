using IntusWindows.Common.Models;
using IntusWindows.Web.Extentions;
using IntusWindows.Web.Models;
using IntusWindows.Web.Services.Interfaces;
using MatBlazor;
using Microsoft.AspNetCore.Components;

namespace IntusWindows.Web.Pages
{
    public class WindowTableBase : ComponentBase
    {
        #region Injects dependencies
        [Inject]
        public IWindowService WindowService { get; set; }
        [Inject]
        protected IMatDialogService MatDialogService { get; set; }
        [Inject]
        protected IMatToaster Toaster { get; set; }
        #endregion

        #region Declear parameters
        [Parameter]
        public OrderDTO Order { get; set; }
        [Parameter]
        public IEnumerable<WindowDTO> Windows { get; set; }
        [Parameter]
        public EventCallback<WindowDTO> OnWindowSelected { get; set; }
        [Parameter]
        public EventCallback<OrderDTO> OnChange { get; set; }
        #endregion

        protected IEnumerable<WindowDTO> DisplayedWindows = new List<WindowDTO>();
        private IEnumerable<WindowDTO> oldWindows { get; set; }
        public DialogueModel<WindowDTO> WindowDialogueModel { get; set; } = new DialogueModel<WindowDTO>(new WindowDTO());

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            oldWindows = Windows;
            DisplayedWindows = Windows;
            WindowSearchText = string.Empty;
        }
        protected override async Task OnParametersSetAsync()
        {
            await base.OnParametersSetAsync();

            if (!oldWindows.Equals(Windows))
            {
                oldWindows = Windows;
                DisplayedWindows = Windows;
                WindowSearchText = string.Empty;
                SortData(previousSortChangedEvent);
            }
        }

        private string _windowSearchText;
        protected string WindowSearchText
        {
            get => _windowSearchText;
            set
            {
                _windowSearchText = value;

                WindowSearchTextOnUpdate(value);
            }
        }
        private void WindowSearchTextOnUpdate(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                DisplayedWindows = Windows;
            }
            else
            {
                DisplayedWindows = Windows.Where(order =>
                    order.Name.Contains(text, StringComparison.OrdinalIgnoreCase) ||
                    order.Quantity.ToString().Contains(text, StringComparison.OrdinalIgnoreCase));
            }
        }

        private MatSortChangedEvent previousSortChangedEvent = new MatSortChangedEvent();

        protected void SortData(MatSortChangedEvent sort)
        {
            previousSortChangedEvent = sort;

            if (!(sort == null || sort.Direction == MatSortDirection.None || string.IsNullOrEmpty(sort.SortId)))
            {
                if (sort.SortId == "name" && sort.Direction == MatSortDirection.Asc)
                {
                    DisplayedWindows = Windows.OrderBy(order => order.Name);
                }
                else if (sort.SortId == "name" && sort.Direction == MatSortDirection.Desc)
                {
                    DisplayedWindows = Windows.OrderByDescending(order => order.Name);
                }
                else if (sort.SortId == "quantity-of-windows" && sort.Direction == MatSortDirection.Asc)
                {
                    DisplayedWindows = Windows.OrderBy(order => order.Quantity);
                }
                else if (sort.SortId == "quantity-of-windows" && sort.Direction == MatSortDirection.Desc)
                {
                    DisplayedWindows = Windows.OrderByDescending(order => order.Quantity);
                }
            }
        }

        public void OpenAddWindowDialogue()
        {
            WindowDialogueModel
                .Clear()
                .Set("OrderId", Order.ID)
                .AddDialogue()
                .Open();
        }
        public void OpenEditWindowDialogue(WindowDTO window)
        {
            WindowDialogueModel
                .Set(window)
                .EditDialogue()
                .Open();
        }
        public async Task OpenDeleteWindowPopupAsync(WindowDTO window)
        {
            var deleteThis = await MatDialogService.ConfirmAsync("Delete this window?");
            if (deleteThis)
            {
                var isDeleted = await WindowService.DeleteWindow(window);

                await OnChange.InvokeAsync();

                Action<string> toastAction = isDeleted ? Toaster.DeleteSuccessful
                                                        : Toaster.DeleteFailed;
                toastAction(DataModelType.Window);
            }
        }
        protected async Task OnSaveAsync()
        {
            if (!WindowDialogueModel.IsOpen)
            {
                return;
            }

            var isSuccess = true;

            if (WindowDialogueModel.IsAdd())
            {
                Order.Windows.Add(WindowDialogueModel.ModelDTO);
            }
            else
            {
                Order.Windows.RemoveAll(x => x.ID == WindowDialogueModel.ModelDTO.ID);
                Order.Windows.Add(WindowDialogueModel.ModelDTO);
            }

            if (isSuccess)
            {
                await OnChange.InvokeAsync();
                WindowDialogueModel.Close();
            }

            Action<string> toastAction = isSuccess
               ? (WindowDialogueModel.IsAdd() ? Toaster.AddedSuccessful : Toaster.UpdateSuccessful)
               : (WindowDialogueModel.IsAdd() ? Toaster.AddFailed : Toaster.UpdateFailed);

            toastAction(DataModelType.Window);

            await OnWindowSelected.InvokeAsync(WindowDialogueModel.ModelDTO);
        }
        protected void OnCancel()
        {
            WindowDialogueModel.Close();
        }
    }
}
