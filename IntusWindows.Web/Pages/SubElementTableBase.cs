using IntusWindows.Common.Models;
using Microsoft.AspNetCore.Components;
using IntusWindows.Web.Services.Interfaces;
using MatBlazor;
using IntusWindows.Web.Models;
using IntusWindows.Common;
using IntusWindows.Web.Extentions;
using System.Security.Cryptography.X509Certificates;

namespace IntusWindows.Web.Pages
{
    public class SubElementTableBase : ComponentBase
    {
        [Inject]
        public ISubElementService SubElementService { get; set; }
        [Inject]
        protected IMatToaster Toaster { get; set; }
        [Inject]
        protected IMatDialogService MatDialogService { get; set; }
        [Parameter]
        public WindowDTO Window { get; set; }
        [Parameter]
        public IEnumerable<SubElementDTO> SubElements { get; set; }
        [Parameter]
        public IEnumerable<ElementType> ElementTypes { get; set; }
        [Parameter]
        public EventCallback<OrderDTO> OnChange { get; set; }

        protected IEnumerable<SubElementDTO> DisplayedSubElements { get; set; }
        public IEnumerable<SubElementDTO> OldSubElements { get; set; }
        public DialogueModel<SubElementDTO> SubElementDialogueModel { get; set; } = new DialogueModel<SubElementDTO>(new SubElementDTO());

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            OldSubElements = SubElements;
            DisplayedSubElements = SubElements;
            SubElementSearchText = string.Empty;
        }

        private bool shouldUpdate()
        {
            if (OldSubElements.Count() != SubElements.Count())
            {
                return true;
            }

            var bothEquals = OldSubElements.OrderBy(old => old.ID)
                .SequenceEqual(SubElements.OrderBy(current => current.ID));

            return !bothEquals;
        }

        protected override async Task OnParametersSetAsync()
        {
            await base.OnParametersSetAsync();

            if (shouldUpdate())
            {
                OldSubElements = SubElements;
                DisplayedSubElements = SubElements;
                SubElementSearchText = string.Empty;
                SortData(previousSortChangedEvent);
            }
        }

        private string _subElementSearchText;
        protected string SubElementSearchText
        {
            get => _subElementSearchText;
            set
            {
                _subElementSearchText = value;

                SubElementSearchTextOnUpdate(value);
            }
        }

        private void SubElementSearchTextOnUpdate(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                DisplayedSubElements = SubElements;
            }
            else
            {
                DisplayedSubElements = SubElements.Where(subElement =>
                    subElement.Element.ToString().Contains(text, StringComparison.OrdinalIgnoreCase) ||
                    subElement.Width.ToString().Contains(text, StringComparison.OrdinalIgnoreCase) ||
                    subElement.Height.ToString().Contains(text, StringComparison.OrdinalIgnoreCase) ||
                    subElement.ElementType.ToString().Contains(text, StringComparison.OrdinalIgnoreCase));
            }
        }

        private MatSortChangedEvent previousSortChangedEvent = new MatSortChangedEvent();

        protected void SortData(MatSortChangedEvent sort)
        {
            previousSortChangedEvent = sort;

            if (!(sort == null || sort.Direction == MatSortDirection.None || string.IsNullOrEmpty(sort.SortId)))
            {

                if (sort.SortId == "element" && sort.Direction == MatSortDirection.Asc)
                {
                    DisplayedSubElements = SubElements.OrderBy(order => order.Element);
                }
                else if (sort.SortId == "element" && sort.Direction == MatSortDirection.Desc)
                {
                    DisplayedSubElements = SubElements.OrderByDescending(order => order.Element);
                }
                else if (sort.SortId == "width" && sort.Direction == MatSortDirection.Asc)
                {
                    DisplayedSubElements = SubElements.OrderBy(order => order.Width);
                }
                else if (sort.SortId == "width" && sort.Direction == MatSortDirection.Desc)
                {
                    DisplayedSubElements = SubElements.OrderByDescending(order => order.Width);
                }
                else if (sort.SortId == "height" && sort.Direction == MatSortDirection.Asc)
                {
                    DisplayedSubElements = SubElements.OrderBy(order => order.Height);
                }
                else if (sort.SortId == "height" && sort.Direction == MatSortDirection.Desc)
                {
                    DisplayedSubElements = SubElements.OrderByDescending(order => order.Height);
                }
                else if (sort.SortId == "element-type" && sort.Direction == MatSortDirection.Asc)
                {
                    DisplayedSubElements = SubElements.OrderBy(order => order.ElementType);
                }
                else if (sort.SortId == "element-type" && sort.Direction == MatSortDirection.Desc)
                {
                    DisplayedSubElements = SubElements.OrderByDescending(order => order.ElementType);
                }
            }
        }

        public void OpenAddSubElementDialogue()
        {
            SubElementDialogueModel
            .Clear()
            .Set("WindowId", Window.ID)
            .Set("ElementType", ElementTypes.FirstOrDefault())
            .AddDialogue()
            .Open();
        }

        protected async Task OnSaveAsync()
        {
            if (!SubElementDialogueModel.IsOpen)
            {
                return;
            }

            if (SubElementDialogueModel.ModelDTO.Element <= 0)
                SubElementDialogueModel.ModelDTO.Element = Window.SubElements.Count() + 1;

            var isSuccess = true;
            if (SubElementDialogueModel.IsAdd())
            {
                Window.SubElements.Add((SubElementDialogueModel.ModelDTO));
            }
            else
            {
                Window.SubElements.RemoveAll(x => x.ID == SubElementDialogueModel.ModelDTO.ID);
                Window.SubElements.Add(SubElementDialogueModel.ModelDTO);
            }

            if (isSuccess)
            {
                await OnChange.InvokeAsync();
                SubElementDialogueModel.Close();
            }

            Action<string> toastAction = isSuccess
                ? (SubElementDialogueModel.IsAdd() ? Toaster.AddedSuccessful : Toaster.UpdateSuccessful)
                : (SubElementDialogueModel.IsAdd() ? Toaster.AddFailed : Toaster.UpdateFailed);

            toastAction(DataModelType.SubElement);
        }

        public void OpenEditSubElementDialogue(SubElementDTO subElement)
        {
            SubElementDialogueModel
                .Set(subElement)
                .EditDialogue()
                .Open();
        }
        public async Task OpenDeleteSubElementPopupAsync(SubElementDTO subElementDTO)
        {
            var deleteThis = await MatDialogService.ConfirmAsync("Delete this sub element?");
            if (deleteThis)
            {
                var isDeleted = await SubElementService.DeleteSubElement(subElementDTO);

                Window.SubElements.Remove(subElementDTO);

                await OnChange.InvokeAsync();

                Action<string> toastAction = isDeleted ? Toaster.DeleteSuccessful
                                                        : Toaster.DeleteFailed;
                toastAction(DataModelType.SubElement);
            }
        }

        protected void OnCancel()
        {
            SubElementDialogueModel.Close();
        }
    }
}
