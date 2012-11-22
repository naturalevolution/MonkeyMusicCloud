using System.Collections.ObjectModel;
using MonkeyMusicCloud.Client.ViewModels.SubViewModels;
using MonkeyMusicCloud.Domain.Model;

namespace MonkeyMusicCloud.Client.Observers
{
    public class ContentBodyObserver
    {

        public static event NewSeachHandler NewSearch;
        public static void NotifyNewSeach(ObservableCollection<Song> songs, string search)
        {
            NewSeachHandler handler = NewSearch;
            if (handler != null) handler(songs, search);
        }

        public static event ChangeContentViewHandler ChangeContentView;
        public static void NotifyChangeContentView(MenuItem item)
        {
            ChangeContentViewHandler handler = ChangeContentView;
            if (handler != null) handler(item);
        }
    }

    public delegate void ChangeContentViewHandler(MenuItem item);
    public delegate void NewSeachHandler(ObservableCollection<Song> songs, string search);
}
