using System;
using System.ComponentModel;
using Acr.UserDialogs;
using Plugin.Media;
using Plugin.Media.Abstractions;
using TravelMonkey.Core.Helpers;
using TravelMonkey.ViewModels;
using Xamarin.Forms;

namespace TravelMonkey.Views
{
    public partial class AddPicturePage : ContentPage
    {
        public AddPicturePageViewModel VM { get; set; } = new AddPicturePageViewModel();
        public AddPicturePage()
        {
            InitializeComponent();
            BindingContext = VM;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        private async void  TakePhotoClicked(object sender, EventArgs e)
        {
            var result = await UserDialogs.Instance.ActionSheetAsync("What do you want to do?",
                "Cancel", null, null, "Take photo", "Choose photo");

            MediaFile photo;
            if (result.Equals("Take photo"))
            {
                photo = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions { PhotoSize = PhotoSize.Small });
            }
            else if (result.Equals("Choose photo"))
            {
                photo = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions { PhotoSize = PhotoSize.Small });
            }
            else
            {
                return;
            }

            if (photo != null)
            {
                var pictureStream = photo.GetStreamWithImageRotatedForExternalStorage();

                VM.PhotoBytes = await pictureStream.ToByteArray();
                await VM.Post();
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            VM.PropertyChanged += VMPropertyChanged;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            VM.PropertyChanged -= VMPropertyChanged;
        }

        public async void VMPropertyChanged(object sender , PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(VM.ErrorMessage) && !string.IsNullOrWhiteSpace(VM.ErrorMessage))
            {
                await UserDialogs.Instance.AlertAsync(VM.ErrorMessage, "Error");

            }
        }
    }
}