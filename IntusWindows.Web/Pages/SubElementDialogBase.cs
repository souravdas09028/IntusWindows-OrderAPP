using IntusWindows.Common;
using IntusWindows.Common.Models;
using IntusWindows.Web.Models;
using Microsoft.AspNetCore.Components;
namespace IntusWindows.Web.Pages
{
    public class SubElementDialogBase : ComponentBase
    {
        [Parameter]
        public IEnumerable<ElementType> ElementTypes { get; set; }
        [Parameter]
        public DialogueModel<SubElementDTO> SubElementDialogueModel { get; set; }
        [Parameter]
        public EventCallback OnSave { get; set; }
        [Parameter]
        public EventCallback OnCancel { get; set; }

        protected override async Task OnInitializedAsync()
        {
            ElementTypes = (IEnumerable<ElementType>)Enum.GetValues(typeof(ElementType));
        }
    }
}
