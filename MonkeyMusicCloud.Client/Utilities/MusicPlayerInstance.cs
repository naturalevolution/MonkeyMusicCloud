using MonkeyMusicCloud.Utilities;
using MonkeyMusicCloud.Utilities.Interface;

namespace MonkeyMusicCloud.Client.Utilities
{
    public class MusicPlayerInstance
    {
        private static MusicPlayerInstance instance;
        public IMusicPlayer Player { get; set; }
        static readonly object InstanceLock = new object();

        public MusicPlayerInstance(IMusicPlayer musicService)
        {
            Player = musicService;
            instance = this;
        }

        public static MusicPlayerInstance GetInstance()
        {
            lock (InstanceLock)
            {
                return instance ?? (instance = new MusicPlayerInstance(new KlangPlayer()));
            }
        }
    }
}
