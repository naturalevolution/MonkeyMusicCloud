using System.Collections.ObjectModel;
using MicroMvvm;
using MonkeyMusicCloud.Client.ViewModels.SubViewModels;
using MonkeyMusicCloud.Client.Views.BodyViews;
using MonkeyMusicCloud.Resource;

namespace MonkeyMusicCloud.Client.ViewModels
{
    public class ContentBodyViewModel : ObservableObject
    {
        public ContentBodyViewModel ()
        {
            Views = new ObservableCollection<MenuItem>()
                        {
                            new MenuItem { Label = MusicResource.MenuSongList, View = new SongListView()},
                            new MenuItem { Label = MusicResource.MenuAddMusics, View = new AddSongsView()}
                        };
        }

        public ObservableCollection<MenuItem> Views { get; set; }
    }
}
