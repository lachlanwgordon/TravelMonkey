﻿using System;
using System.Threading.Tasks;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using TravelMonkey.Data;
using TravelMonkey.Services;
using TravelMonkey.ViewModels;
using Microsoft.JSInterop;

namespace TravelMonkey.Blazor.WASM.Pages
{
    public partial class Index
    {
        [Inject] NavigationManager NavigationManager { get; set; }
        [Inject] IJSRuntime JSRuntime { get; set; }

        public readonly MainPageViewModel VM = new MainPageViewModel();

        private readonly BingSearchService _bingSearchService = new BingSearchService();
        protected override async Task OnInitializedAsync()
        {
            VM.PropertyChanged += VM_PropertyChanged;
            await base.OnInitializedAsync();
            MockDataStore.Destinations = await _bingSearchService.GetDestinations();

            VM.StartSlideShow();
            StateHasChanged();
        }

        private void VM_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "CurrentDestination")
            {
                InvokeAsync(() =>
                {
                    JSRuntime.InvokeVoidAsync("setDestination",VM.CurrentDestination.ImageUrl, VM.CurrentDestination.Title);
                });
            }
        }

        public void Translate()
        {
            if (string.IsNullOrEmpty(VM.TranslateText))
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
