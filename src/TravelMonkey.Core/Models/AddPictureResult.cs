
namespace TravelMonkey.Models
{
    public class AddPictureResult
    {
        public string Description { get; }
        public string LandmarkDescription { get; }
        public string AccentColor { get; }
        public bool Succeeded => !string.IsNullOrEmpty(Description) && !string.IsNullOrEmpty(AccentColor) ;

        public AddPictureResult() { }

        public AddPictureResult(string description, string accentColor, string landmarkDescription = "")
        {
            Description = $"I see {description}";
            AccentColor = accentColor;
            LandmarkDescription = string.IsNullOrWhiteSpace(landmarkDescription) ? "" : $"And I think I recognize {landmarkDescription}";
        }
    }
}