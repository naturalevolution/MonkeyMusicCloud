using System.Windows.Input;
using MicroMvvm;
using MonkeyMusicCloud.Client.Observers;

namespace MonkeyMusicCloud.Client.ViewModels.BodyViewModels
{
    public class SearchViewModel : ViewModelBase
    {
        private bool searchOnTitle;
        private bool searchOnArtist;
        private bool searchOnAlbum;


        public SearchViewModel()
        {
            SearchOnTitle = true;
            SearchOnArtist = true;
            SearchOnAlbum = true;
        }

        public ICommand SearchSongsListCommand
        {
            get { return new RelayCommand<string>(SearchSongsExecute); }
        }

        public bool SearchOnTitle
        {
            get { return searchOnTitle; }
            set
            {
                searchOnTitle = value;
                RaisePropertyChanged("SearchOnTitle");
            }
        }

        public bool SearchOnArtist
        {
            get { return searchOnArtist; }
            set
            {
                searchOnArtist = value;
                RaisePropertyChanged("SearchOnArtist");
            }
        }

        public bool SearchOnAlbum
        {
            get { return searchOnAlbum; }
            set
            {
                searchOnAlbum = value;
                RaisePropertyChanged("SearchOnAlbum");
            }
        }

        private void SearchSongsExecute(string filter)
        {
            ContentBodyObserver.NotifyNewSeach(Service.SearchSongs(filter, SearchOnTitle, SearchOnArtist, SearchOnAlbum), filter);
        }
    }
}