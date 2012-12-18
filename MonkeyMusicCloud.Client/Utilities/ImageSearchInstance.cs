using MonkeyMusicCloud.Utilities;
using MonkeyMusicCloud.Utilities.Interface;

namespace MonkeyMusicCloud.Client.Utilities
{
    public class ImageSearchInstance
    {
        private static ImageSearchInstance instance;
        public IImageSearch ImageSearch { get; set; }
        static readonly object InstanceLock = new object();

        public ImageSearchInstance(IImageSearch musicService)
        {
            ImageSearch = musicService;
            instance = this;
        }

        public static ImageSearchInstance GetInstance()
        {
            lock (InstanceLock)
            {
                return instance ?? (instance = new ImageSearchInstance(new ImageGoogleSearch()));
            }
        }
    }
}
