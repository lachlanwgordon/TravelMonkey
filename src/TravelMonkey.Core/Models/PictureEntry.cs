
using System.IO;

namespace TravelMonkey.Models
{
    public class PictureEntry
    {
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public byte[] ImageBytes { get; set; }
        public Stream ImageStream { get; set; }
    }
}