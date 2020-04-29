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
        public AddPicturePageViewModel VM { get; set; } = new AddPicturePageViewModel();
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
        }
    }
}
