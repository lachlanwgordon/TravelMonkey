using System;
using System.Linq;
using System.Threading.Tasks;
using BlazorInputFile;
using Microsoft.AspNetCore.Components;
using TravelMonkey.ViewModels;
using TravelMonkey.Core.Helpers;
using System.Diagnostics;

namespace TravelMonkey.Blazor.Pages
{
    public partial class AddPicturePage : ComponentBase
    {
        [Inject]
        public NavigationManager NavigationManager {get;set;}
        public AddPicturePageViewModel VM { get; set; } = new AddPicturePageViewModel();
        public ViewState ViewState { get; set; }
        public bool DialogIsOpen { get; set; }
        public bool CameraIsOpen { get; set; }
        public bool UploadIsOpen { get; set; }

        public string BackgroundGradient =>
            $"background: linear-gradient(45deg, rgba(255,255,255,1) 0%, {VM.PictureAccentColor} 100%)";


        public AddPicturePage()
        {
        }
        public string FileName { get; set; }
        async Task HandleFileSelected(IFileListEntry[] files)
        {
            // Do something with the files, e.g., read them
            var image = files.FirstOrDefault();
            FileName = image.Name;


            VM.PhotoBytes = await image.Data.ToByteArray();
            await VM.Post();
            CloseClick();
            StateHasChanged();
        }

        public void Back()
        {
            NavigationManager.NavigateTo("/");
        }
        
        public void CloseClick()
        {
            DialogIsOpen = false;
            CameraIsOpen = false;
            UploadIsOpen = false;
        }

        public void CameraClick()
        {
            DialogIsOpen = false;
            CameraIsOpen = true;
        }

        public void UploadClick()
        {
            DialogIsOpen = false;
            UploadIsOpen = true;
        }


        public void SavePicture()
        {
            VM.AddPicture();
        }

        public void AddPicture()
        {
            DialogIsOpen = true;
        }

        protected void OnDataReceived(byte[] data)
        {
            Debug.WriteLine($"Data Recieved of length: {data.Length}");
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
