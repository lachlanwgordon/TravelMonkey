using System.Collections.Generic;
using System.Threading.Tasks;
using MvvmHelpers;
using MvvmHelpers.Commands;
using TravelMonkey.Services;

namespace TravelMonkey.ViewModels
{
    public class TranslateResultPageViewModel : BaseViewModel
    {
        private readonly TranslationService _translationService =
            new TranslationService();

        private string _inputText;
        private Dictionary<string, string> _translations;

        public string InputText
        {
            get => _inputText;
            set
            {
                if (_inputText == value)
                    return;

                SetProperty(ref _inputText, value);

                TranslateText(value);
            }
        }

        public Dictionary<string, string> Translations
        {
            get => _translations;
            set
            {
                SetProperty(ref _translations, value);
            }
        }

        public Command<string> TranslateTextCommand => new Command<string>((inputText) =>
        {
            InputText = inputText;
        });

        public string ErrorMessage { get; private set; }

        public async Task TranslateText(string text)
        {
            var result = await _translationService.TranslateText(text);

            if (!result.Succeeded)
            {
                //MessagingCenter.Send(this, Constants.TranslationFailedMessage);
                //TODO: what is this?
                ErrorMessage = "Failed to translate";
            }

            Translations = result.Translations;
        }
    }
}