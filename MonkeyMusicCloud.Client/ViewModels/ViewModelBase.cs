using MicroMvvm;
using MonkeyMusicCloud.Client.Service.Proxy;
using MonkeyMusicCloud.Client.Utilities;
using MonkeyMusicCloud.Utilities.Interface;

namespace MonkeyMusicCloud.Client.ViewModels
{
    public class ViewModelBase : ObservableObject
    {
        protected IMusicService Service { get { return ServiceInstance.GetInstance().Service; } }
        protected IMusicPlayer MusicPlayer { get { return MusicPlayerInstance.GetInstance().Player; } }
    }
}
