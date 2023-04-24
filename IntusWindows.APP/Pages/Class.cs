using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;

namespace IntusWindows.APP.Pages
{
    public class Class :ComponentBase
    {
        [Inject]
        public IToastService toastService { get; set; }
        protected override async Task OnInitializedAsync()
        {
            toastService.ShowSuccess("Hello, world!");
        }
    }
}
