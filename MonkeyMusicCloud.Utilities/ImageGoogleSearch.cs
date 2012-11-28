using MonkeyMusicCloud.Utilities.Amazon;
using MonkeyMusicCloud.Utilities.Interface;

namespace MonkeyMusicCloud.Utilities
{
    public class ImageGoogleSearch : IImageSearch
    {
        public string GetImagePath(string album)
        {
            return "../../Themes/Images/MusiqueFolder.png";
        }
    }
}