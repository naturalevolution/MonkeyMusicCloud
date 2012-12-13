using System.Collections.ObjectModel;
using System.Configuration;
using System.Windows.Input;
using MicroMvvm;
using MonkeyMusicCloud.Client.Observers;
using MonkeyMusicCloud.Client.ViewModels.SubViewModels;
using MonkeyMusicCloud.Client.Views.BodyViews;
using MonkeyMusicCloud.Domain.Model;
using MonkeyMusicCloud.Resource;

namespace MonkeyMusicCloud.Client.ViewModels
{
    public class ContentBodyViewModel : ViewModelBase
    {
        public ContentBodyViewModel ()
        {
            Items = new ObservableCollection<MenuItem>();
                      
            ContentBodyObserver.ChangeContentView += delegate(MenuItem item)
                                                         {
                                                             Items.Add(item);
                                                             SelectedItem = item;
                                                         };
            ContentBodyObserver.NewSearch += OnContentBodyObserverOnNewSearch;
        }

        private void OnContentBodyObserverOnNewSearch(ObservableCollection<Song> songs, string search)
        {
            SongListView songListView = new SongListView {SongList = songs};
            MenuItem item = new MenuItem
                {
                    Label = string.Format(MusicResource.SearchTab, search), 
                    View = songListView, 
                    ImagePath = string.Format("{0}search.png", ConfigurationManager.AppSettings["ImageFolder"])
                };
            Items.Add(item);
            SelectedItem = item;
        }

        public ObservableCollection<MenuItem> Items { get; set; }

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
        
        public ICommand CloseCommand
        {
            get { return new RelayCommand<MenuItem>(CloseExecute); }
        }

        private void CloseExecute(MenuItem item)
        {
            Items.Remove(item);
        }
    }
}
