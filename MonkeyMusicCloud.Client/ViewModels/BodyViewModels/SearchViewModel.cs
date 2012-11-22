using System.Collections.ObjectModel;
using System.Windows.Input;
using MicroMvvm;
using MonkeyMusicCloud.Domain.Model;

namespace MonkeyMusicCloud.Client.ViewModels.BodyViewModels
{
    public class SearchViewModel : ViewModelBase
    {
        private ObservableCollection<Song> songList;

        public ObservableCollection<Song> SongList
        {
            get
            {
                return songList;
            }
            set
            {
                songList = value;
                RaisePropertyChanged("SongList");
            }
        }
        
        
        public ICommand SearchSongsListCommand
        {
            get { return new RelayCommand<string>(SearchSongsExecute); }
        }

        private void SearchSongsExecute(string filter)
        {
            SongList = Service.SearchSongs(filter);
        }
    }
}