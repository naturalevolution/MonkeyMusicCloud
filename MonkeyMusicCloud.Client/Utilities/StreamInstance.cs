using MonkeyMusicCloud.Utilities;
using MonkeyMusicCloud.Utilities.Interface;

namespace MonkeyMusicCloud.Client.Utilities
{
    public class StreamInstance
    {
        private static StreamInstance instance;
        public IStreamHelper StreamHelper { get; set; }
        static readonly object InstanceLock = new object();

        public StreamInstance(IStreamHelper streamHelper)
        {
            StreamHelper = streamHelper;
            instance = this;
        }

        public static StreamInstance GetInstance()
        {
            lock (InstanceLock)
            {
                return instance ?? (instance = new StreamInstance(new StreamHelper()));
            }
        }
    }
}
