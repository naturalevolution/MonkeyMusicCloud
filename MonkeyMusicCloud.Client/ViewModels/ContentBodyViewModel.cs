using System.Collections.ObjectModel;
using MonkeyMusicCloud.Client.Observers;
using MonkeyMusicCloud.Client.ViewModels.SubViewModels;
using MonkeyMusicCloud.Client.Views.BodyViews;
using MonkeyMusicCloud.Resource;

namespace MonkeyMusicCloud.Client.ViewModels
{
    public class ContentBodyViewModel : ViewModelBase
    {
        

        public ContentBodyViewModel ()
        {
            Views = new ObservableCollection<MenuItem>()
                        {
                            new MenuItem {Label = MusicResource.MenuSongList, View = new SongListView()},
                            new MenuItem {Label = MusicResource.MenuAddMusics, View = new AddSongsView()}
                        };

            ContentBodyObserver.ChangeContentView += delegate(MenuItem item)
                                                         {
                                                             Views.Add(item);
                                                             SelectedItem = item;
                                                         };


        }

        public ObservableCollection<MenuItem> Views { get; set; }

        private MenuItem selectedItem;
        public MenuItem SelectedItem
        {
            get {
                return selectedItem;
            }
            set {
                selectedItem = value;
                RaisePropertyChanged("SelectedItem");
            }
        }
    }
}
