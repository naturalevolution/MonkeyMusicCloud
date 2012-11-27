using System.Collections.ObjectModel;
using MonkeyMusicCloud.Domain.Model;

namespace MonkeyMusicCloud.Client.ViewModels.BodyViewModels
{
    public class ArtistDetailViewModel : ViewModelBase
    {
        private string artist;
        private ObservableCollection<string> albumList;
        private ObservableCollection<Song> songList;

        public string Artist
        {
            get { return artist; }
            set { 
                artist = value;
                AlbumList = Service.GetAlbumsByArtist(artist);
                SongList = Service.GetByArtist(artist);
                RaisePropertyChanged("Artist");
            }
        }
        
        public ObservableCollection<string> AlbumList
        {
            get { return albumList; }
            set {
                albumList = value;
                RaisePropertyChanged("AlbumList");
            }
        }

        public ObservableCollection<Song> SongList
        {
            get {
                return songList;
            }
            set {
                songList = value;
                RaisePropertyChanged("SongList");
            }
        }
    }
}