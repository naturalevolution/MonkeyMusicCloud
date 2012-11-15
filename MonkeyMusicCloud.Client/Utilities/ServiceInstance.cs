using MonkeyMusicCloud.Client.Service.Proxy;

namespace MonkeyMusicCloud.Client.Utilities
{
    public class ServiceInstance
    {
        private static ServiceInstance instance;
        public IMusicService Service { get; set; }
        static readonly object InstanceLock = new object();

        public ServiceInstance(IMusicService musicService)
        {
            Service = musicService;
            instance = this;
        }

        public static ServiceInstance GetInstance()
        {
            lock (InstanceLock)
            {
                return instance ?? (instance = new ServiceInstance(new MusicServiceClient()));
            }
        }
    }
}
