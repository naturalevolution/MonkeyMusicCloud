using System.Collections.ObjectModel;
using MicroMvvm;
using MonkeyMusicCloud.Client.Views.BodyViews;

namespace MonkeyMusicCloud.Client.ViewModels
{
    public class MenuViewModel : ObservableObject
    {
        public MenuViewModel()
        {
            BodyViews = new ObservableCollection<IBodyView>()
                            {
                                new AddSongsView(),
                                new SongListView()
                            };
        }

        public ObservableCollection<IBodyView> BodyViews { get; set; }
    }
}
