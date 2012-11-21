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
        private ObservableCollection<Song> songs;
        private string album;
        public string Album
        {
            get { return album; }
            set { 
                album = value;
                Songs = Service.GetByAlbum(album);
            }
        }

        public ObservableCollection<Song> Songs
        {
            get {
                return songs;
            }
            set {
                songs = value;
                RaisePropertyChanged("Songs");
            }
        }
    }
}