using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using MvvmHelpers;
using MvvmHelpers.Commands;
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
        public Stream PhotoStream { get; set; }
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
            TakePhotoCommand = new Command(async () => await TakePhoto());
            AddPictureCommand = new Command(() =>
            {
                AddPicture();
            });
        }

        public void AddPicture()
        {
            MockDataStore.Pictures.Add(new PictureEntry { Description = _pictureDescription, ImageUrl = "todo" });
            //MessagingCenter.Send(this, Constants.PictureAddedMessage);TODO what was this for
        }

        private async Task TakePhoto()
        {
            //TODO move to view
            //var result = await UserDialogs.Instance.ActionSheetAsync("What do you want to do?",
            //    "Cancel", null, null, "Take photo", "Choose photo");

            //if (result.Equals("Take photo"))
            //{
            //    _photo = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions { PhotoSize = PhotoSize.Small });

            //    PhotoSource = (StreamImageSource)ImageSource.FromStream(() => _photo.GetStream());
            //}
            //else if (result.Equals("Choose photo"))
            //{
            //    _photo = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions { PhotoSize = PhotoSize.Small });

            //    PhotoSource = (StreamImageSource)ImageSource.FromStream(() => _photo.GetStream());
            //}
            //else
            //{
            //    return;
            //}

            //if (_photo != null)
            //    await Post();
        }
        public string ErrorMessage { get; set; }
        public async Task Post()
        {
            if (PhotoStream == null)
            {
                ErrorMessage = "Please select an image";
                //await UserDialogs.Instance.AlertAsync("Please select an image first", "No image selected");
                return;
            }

            IsPosting = true;

            try
            {
                //var pictureStream = _photo.GetStreamWithImageRotatedForExternalStorage();
                var result = await _computerVisionService.AddPicture(PhotoStream);

                if (!result.Succeeded)
                {
                    //MessagingCenter.Send(this, Constants.PictureFailedMessage);//TODO what does this do
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