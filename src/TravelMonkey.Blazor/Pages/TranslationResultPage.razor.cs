using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using TravelMonkey.ViewModels;

namespace TravelMonkey.Blazor.Pages
{
    public partial class TranslationResultPage : ComponentBase
    {
        public TranslateResultPageViewModel VM = new TranslateResultPageViewModel();
        public TranslationResultPage()
        {
        }
        [Parameter]
        public string Text { get; set; } = "You didn't say anything";


        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            //VM.InputText = Text;

            await VM.TranslateText(Text);
            StateHasChanged();

        }
    }
}
