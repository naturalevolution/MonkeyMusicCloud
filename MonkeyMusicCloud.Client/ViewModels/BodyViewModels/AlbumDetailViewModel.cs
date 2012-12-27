using System.Collections.ObjectModel;
using MonkeyMusicCloud.Client.Utilities;
using MonkeyMusicCloud.Domain.Model;
using MonkeyMusicCloud.Utilities.Interface;

namespace MonkeyMusicCloud.Client.ViewModels.BodyViewModels
{
    public class AlbumDetailViewModel : ViewModelBase
    {
        private string album;
        private string albumImagePath;
        private ObservableCollection<Song> songList;

        protected IImageSearch ImageSearch
        {
            get { return ImageSearchInstance.GetInstance().ImageSearch; }
        }

        public string Album
        {
            get { return album; }
            set
            {
                album = value;
                SongList = Service.GetByAlbum(album);
                AlbumImagePath = ImageSearch.GetImagePath(album);
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

        public string AlbumImagePath
        {
            get { return albumImagePath; }
            set
            {
                albumImagePath = value;
                RaisePropertyChanged("AlbumImagePath");
            }
        }
    }
}