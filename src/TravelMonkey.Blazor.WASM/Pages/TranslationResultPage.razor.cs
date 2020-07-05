using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using TravelMonkey.ViewModels;

namespace TravelMonkey.Blazor.WASM.Pages
{
    public partial class TranslationResultPage : ComponentBase
    {
        [Inject]
        NavigationManager NavigationManager { get; set; }
        public TranslateResultPageViewModel VM = new TranslateResultPageViewModel();
        public TranslationResultPage()
        {
        }
        [Parameter]
        public string Text { get; set; } = "You didn't say anything";


        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            VM.InputText = Text;
            await VM.TranslateText(Text);
            StateHasChanged();

        }

        public async Task Translate()
        {
            await VM.TranslateText(VM.InputText);
            StateHasChanged();
        }

        public void Back()
        {
            NavigationManager.NavigateTo("/");
        }

    }
}
