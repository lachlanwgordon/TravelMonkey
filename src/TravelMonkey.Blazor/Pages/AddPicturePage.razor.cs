using System;
using System.Linq;
using System.Threading.Tasks;
using BlazorInputFile;
using Microsoft.AspNetCore.Components;
using TravelMonkey.ViewModels;

namespace TravelMonkey.Blazor.Pages
{
    public partial class AddPicturePage : ComponentBase
    {
        [Inject]
        public NavigationManager NavigationManager {get;set;}
        public AddPicturePageViewModel VM { get; set; } = new AddPicturePageViewModel();
        public ViewState ViewState { get; set; }
        public bool dialogIsOpen { get; set; }

        public AddPicturePage()
        {
        }
        public string FileName { get; set; }
        async Task HandleFileSelected(IFileListEntry[] files)
        {
            // Do something with the files, e.g., read them
            var image = files.FirstOrDefault();
            FileName = image.Name;

            VM.PhotoStream = image.Data;
            await VM.Post();
            StateHasChanged();
        }

        public void Back()
        {
            NavigationManager.NavigateTo("/");
        }
        
        public void OkClick()
        {
            dialogIsOpen = false;
        }

        public void AddPicture()
        {
            dialogIsOpen = true;
        }
    }
    public enum ViewState
    {
        none = 0,
        alert = 1,
        camera = 2,
        picker = 3

    }
}
