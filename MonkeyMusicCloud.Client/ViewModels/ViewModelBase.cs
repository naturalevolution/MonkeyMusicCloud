using MicroMvvm;
using MonkeyMusicCloud.Client.Service.Proxy;
using MonkeyMusicCloud.Client.Utilities;

namespace MonkeyMusicCloud.Client.ViewModels
{
    public class ViewModelBase : ObservableObject
    {
        protected IMusicService Service { get { return ServiceInstance.GetInstance().Service; } }
    }
}
