using System.Collections.ObjectModel;
using MonkeyMusicCloud.Client.Observers;
using MonkeyMusicCloud.Client.ViewModels.SubViewModels;
using MonkeyMusicCloud.Domain.Model;

namespace MonkeyMusicCloud.Test.Helper.EventCatchers
{
    public class ContentBodyEventCatcher
    {
        public ContentBodyEventCatcher()
        {
            
            ContentBodyObserver.ChangeContentView += delegate(MenuItem view)
            {
                ChangeContentViewInvoked = true;
                MenuItem = view;
            };

            ContentBodyObserver.NewSearch += delegate(ObservableCollection<Song> songs, string search)
            {
                NewSearchInvoked = true;
                SearchSongList = songs;
                SearchFilter = search;
            };
        }

        public MenuItem MenuItem { get; set; }
        public ObservableCollection<Song> SearchSongList { get; set; }

        public string SearchFilter;
        public bool ChangeContentViewInvoked = false;
        public bool NewSearchInvoked = false;
    }
}
