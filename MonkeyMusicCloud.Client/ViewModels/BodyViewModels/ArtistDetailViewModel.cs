using System.Collections.ObjectModel;
using System.Windows.Input;
using MicroMvvm;
using MonkeyMusicCloud.Client.Observers;
using MonkeyMusicCloud.Client.ViewModels.SubViewModels;
using MonkeyMusicCloud.Client.Views.BodyViews;
using MonkeyMusicCloud.Domain.Model;

namespace MonkeyMusicCloud.Client.ViewModels.BodyViewModels
{
    public class ArtistDetailViewModel : ViewModelBase
    {
        private ObservableCollection<string> albumList;
        private string artist;
        private ObservableCollection<Song> songList;

        public string Artist
        {
            get { return artist; }
            set
            {
                artist = value;
                AlbumList = Service.GetAlbumsByArtist(artist);
                SongList = Service.GetByArtist(artist);
                RaisePropertyChanged("Artist");
            }
        }

        public ObservableCollection<string> AlbumList
        {
            get { return albumList; }
            set
            {
                albumList = value;
                RaisePropertyChanged("AlbumList");
            }
        }

        public ObservableCollection<Song> SongList
        {
            get { return songList; }
            set
            {
                songList = value;
                RaisePropertyChanged("SongList");
            }
        }

        public ICommand OpenAlbumCommand
        {
            get { return new RelayCommand<string>(OpenAlbumExecute); }
        }

        private void OpenAlbumExecute(string album)
        {
            ContentBodyObserver.NotifyChangeContentView(new MenuItem {Label = album, View = new AlbumDetailView(album)});
        }
    }
}