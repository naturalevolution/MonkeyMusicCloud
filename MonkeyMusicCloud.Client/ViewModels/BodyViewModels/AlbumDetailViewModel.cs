using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using MicroMvvm;
using MonkeyMusicCloud.Client.Observers;
using MonkeyMusicCloud.Client.ViewModels.SubViewModels;
using MonkeyMusicCloud.Client.Views.BodyViews;
using MonkeyMusicCloud.Domain.Model;

namespace MonkeyMusicCloud.Client.ViewModels.BodyViewModels
{
    public class AlbumDetailViewModel : ViewModelBase
    {
        private ObservableCollection<Song> songList;
        private string album;
        public string Album
        {
            get { return album; }
            set { 
                album = value;
                SongList = Service.GetByAlbum(album);
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