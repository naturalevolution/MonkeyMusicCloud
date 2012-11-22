using MonkeyMusicCloud.Utilities.Interface;

namespace MonkeyMusicCloud.Utilities
{
    public class ImageGoogleSearch : IImageSearch
    {
        public string GetImagePath(string search)
        {
            return "../../Themes/Images/MusiqueFolder.png";
        }
    }
}