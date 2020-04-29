using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using TravelMonkey.Data;
using TravelMonkey.Services;
using TravelMonkey.ViewModels;

namespace TravelMonkey.Blazor.Pages
{
    public partial class Index : ComponentBase
    {
        [Inject] NavigationManager NavigationManager { get; set; }

        public readonly MainPageViewModel VM = new MainPageViewModel();
        public Index()
        {
        }

        private readonly BingSearchService _bingSearchService = new BingSearchService();
        protected override async Task OnInitializedAsync()
        {
            VM.PropertyChanged += VM_PropertyChanged;
            await base.OnInitializedAsync();
            //VM.IsBusy = true;
            MockDataStore.Destinations = await _bingSearchService.GetDestinations();

            //VM.IsBusy = false;

            VM.StartSlideShow();
            StateHasChanged();
        }

        public int currentCount;

        private void VM_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            currentCount++;

            if(e.PropertyName != "IsBusy")
            {
                InvokeAsync(() => {
                    StateHasChanged();
                });
            }

            //if(e.PropertyName == "CurrentDestination")
            //{
            //    Debug.WriteLine($"{e.PropertyName} {VM.CurrentDestination.Title }");
            //    StateHasChanged();

            //}
            //else
            //    Debug.WriteLine($"{e.PropertyName} ");

        }

        public void Translate()
        {
            if(string.IsNullOrEmpty(VM.TranslateText))
            {
                VM.ErrorMessage = "You didn't enter any text!";
                return;
            }
            NavigationManager.NavigateTo($"/translation/{VM.TranslateText}");
        }

        public void AddPicture()
        {
            
            NavigationManager.NavigateTo($"/addpicture");
        }


        public void Update()
        {
            StateHasChanged();
        }
    }
}
