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

        Carousel TheCarousel;


        public readonly MainPageViewModel VM = new MainPageViewModel();
        public Index()
        {
        }

        private readonly BingSearchService _bingSearchService = new BingSearchService();
        protected override async Task OnInitializedAsync()
        {
            VM.PropertyChanged += VM_PropertyChanged;
            await base.OnInitializedAsync();
            MockDataStore.Destinations = await _bingSearchService.GetDestinations();

            VM.StartSlideShow();
            StateHasChanged();
        }

        public int currentCount;
        public string URL { get; set; }

        private void VM_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            currentCount++;

            if(e.PropertyName == "CurrentDestination")
            {
                //StateHasChanged();//This doesn't nothing because I'm on background thread
                InvokeAsync(() =>
                {
                    //URL = VM.CurrentDestination.ImageUrl;
                    Debug.WriteLine($"{URL}");
                    
                    StateHasChanged();//Updated image but input loses focus
                });
                
            }


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
