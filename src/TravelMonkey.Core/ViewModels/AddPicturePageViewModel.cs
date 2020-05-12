using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using MvvmHelpers;
using MvvmHelpers.Commands;
using TravelMonkey.Core.Helpers;
//using Acr.UserDialogs;
//using Plugin.Media;
//using Plugin.Media.Abstractions;
using TravelMonkey.Data;
using TravelMonkey.Models;
using TravelMonkey.Services;
//using Xamarin.Forms;

namespace TravelMonkey.ViewModels
{
    public class AddPicturePageViewModel : BaseViewModel
    {
        private readonly ComputerVisionService _computerVisionService = new ComputerVisionService();

        //TODO Move carea stuff to View
        //public bool ShowImagePlaceholder => !ShowPhoto;
        //public bool ShowPhoto => _photoSource != null;

        //MediaFile _photo;
        //StreamImageSource _photoSource;
        //Stream photoStream;
        //public Stream PhotoStream
        //{
        //    get => photoStream;
        //    set => SetProperty(ref photoStream, value);
        //}

        byte[] photoBytes;
        public byte[] PhotoBytes
        {
            get => photoBytes;
            set => SetProperty(ref photoBytes, value);
        }
        //public StreamImageSource PhotoSource
        //{
        //    get => _photoSource;
        //    set
        //    {
        //        if (Set(ref _photoSource, value))
        //        {
        //            RaisePropertyChanged(nameof(ShowPhoto));
        //            RaisePropertyChanged(nameof(ShowImagePlaceholder));
        //        }
        //    }
        //}

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
            //MessagingCenter.Send(this, Constants.PictureAddedMessage);TODO what was this for
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

                if (!string.IsNullOrWhiteSpace(result.LandmarkDescription))
                    PictureDescription += $". {result.LandmarkDescription}";
            }
            finally
            {
                IsPosting = false;
            }
        }
    }
}