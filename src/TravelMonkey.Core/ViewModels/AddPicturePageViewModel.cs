using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using MvvmHelpers;
using MvvmHelpers.Commands;
using TravelMonkey.Core.Helpers;
using TravelMonkey.Data;
using TravelMonkey.Models;
using TravelMonkey.Services;

namespace TravelMonkey.ViewModels
{
    public class AddPicturePageViewModel : BaseViewModel
    {
        private readonly ComputerVisionService _computerVisionService = new ComputerVisionService();

        byte[] photoBytes;
        public byte[] PhotoBytes
        {
            get => photoBytes;
            set => SetProperty(ref photoBytes, value);
        }

        private bool _isPosting;
        public bool IsPosting
        {
            get => _isPosting;
            set => SetProperty(ref _isPosting, value);
        }
        private string _pictureAccentColor = "#2222bb";//   .ToString();//TODO check if generates hex
        public string PictureAccentColor
        {
            get => _pictureAccentColor;
            set => SetProperty(ref _pictureAccentColor, value);
        }



        private string _pictureDescription;
        public string PictureDescription
        {
            get => _pictureDescription;
            set => SetProperty(ref _pictureDescription, value);
        }

        public Command TakePhotoCommand { get; }
        public Command AddPictureCommand { get; }

        public AddPicturePageViewModel()
        {
            AddPictureCommand = new Command(() =>
            {
                AddPicture();
            });
        }

        public void AddPicture()
        {
            MockDataStore.Pictures.Add(new PictureEntry { Description = _pictureDescription, PhotoBytes = PhotoBytes });
        }

        string errorMessage;
        public string ErrorMessage
        {
            get => errorMessage;
            set => SetProperty(ref errorMessage, value);
        }

        public async Task Post()
        {
            if (PhotoBytes == null || PhotoBytes.Length == 0)
            {
                ErrorMessage = "Please select an image";
                return;
            }

            IsPosting = true;

            try
            {
                var stream = await photoBytes.ToStream();

                var result = await _computerVisionService.AddPicture(stream);

                if (!result.Succeeded)
                {
                    ErrorMessage = "Can you hand me my glasses? Something went wrong while analyzing this image";
                    return;
                }

                PictureAccentColor = result.AccentColor;

                PictureDescription = result.Description;

                Debug.WriteLine($" {result.LandmarkDescription}"  );
                if (!string.IsNullOrWhiteSpace(result.LandmarkDescription))
                {

                    PictureDescription += $". {result.LandmarkDescription}";

                }
            }
            finally
            {
                IsPosting = false;
            }
        }
    }
}