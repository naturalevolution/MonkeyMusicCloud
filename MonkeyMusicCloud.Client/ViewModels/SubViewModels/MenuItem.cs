using MonkeyMusicCloud.Client.Views.BodyViews;

namespace MonkeyMusicCloud.Client.ViewModels.SubViewModels
{
    public class MenuItem : ViewModelBase
    {
        public IBodyView View{ get; set; }
        public string Label { get; set; }
        public string ImagePath { get; set; }
    }
}
