using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using MicroMvvm;
using MonkeyMusicCloud.Client.Events;
using MonkeyMusicCloud.Domain.Model;

namespace MonkeyMusicCloud.Client.ViewModels
{
    public class SongListViewModel : ViewModelBase
    {
        private ObservableCollection<Song> songList;
        public ObservableCollection<Song> SongList
        {
            get { return songList; }
            set { 
                songList = value;
                RaisePropertyChanged("SongList");
            }
        }
        
        public ICommand RefreshSongListCommand { get { return new RelayCommand(RefreshSongListExecute); } }
        public ICommand AddSongToPlayListCommand { get { return new RelayCommand<IList>(AddSongToPlayListExecute); } }

        private void AddSongToPlayListExecute(IList songs)
        {
            ObservableCollection<Song> songCollection = new ObservableCollection<Song>(songs.Cast<Song>());
            EventsManager.InvokeAddToPlayList(songCollection);
        }

        private void RefreshSongListExecute()
        {
            SongList = Service.GetAllSongs();
        }
    }
}
