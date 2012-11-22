using System.Collections.ObjectModel;
using MonkeyMusicCloud.Client.Utilities;
using MonkeyMusicCloud.Domain.Model;
using MonkeyMusicCloud.Utilities.Interface;

namespace MonkeyMusicCloud.Client.ViewModels.BodyViewModels
{
    public class AlbumDetailViewModel : ViewModelBase
    {
        protected IImageSearch ImageSearch { get { return ImageSearchInstance.GetInstance().ImageSearch; } }
        private ObservableCollection<Song> songList;
        private string album;
        private string albumImagePath;

        public string Album
        {
            get { return album; }
            set { 
                album = value;
                SongList = Service.GetByAlbum(album);
                AlbumImagePath = ImageSearch.GetImagePath(album);
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

        public string AlbumImagePath
        {
            get {
                return albumImagePath;
            }
            set {
                albumImagePath = value;
                RaisePropertyChanged("AlbumImagePath");
            }
        }
    }
}