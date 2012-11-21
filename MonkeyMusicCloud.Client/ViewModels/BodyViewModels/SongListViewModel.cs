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
    public class SongListViewModel : ViewModelBase
    {
        private ObservableCollection<Song> songList;

        public ObservableCollection<Song> SongList
        {
            get { return songList; }
            set
            {
                songList = value;
                RaisePropertyChanged("SongList");
            }
        }

        public ICommand RefreshSongListCommand
        {
            get { return new RelayCommand(RefreshSongListExecute); }
        }

        public ICommand AddSongToPlayListCommand
        {
            get { return new RelayCommand<Song>(AddSongToPlayListExecute); }
        }

        public ICommand SearchSongsListCommand
        {
            get { return new RelayCommand<string>(SearchSongsExecute); }
        }

        public ICommand OpenAlbumCommand
        {
            get { return new RelayCommand<string>(OpenAlbumExecute); }
        }

        private void OpenAlbumExecute(string album)
        {
            ContentBodyObserver.NotifyChangeContentView(new MenuItem {Label = album, View = new AlbumDetailView(album)});
        }

        private void SearchSongsExecute(string filter)
        {
            SongList = Service.SearchSongs(filter);
        }

        private void AddSongToPlayListExecute(Song song)
        {
            
            PlayerObserver.NotifyAddToPlayList(song);
        }

        private void RefreshSongListExecute()
        {
            SongList = Service.GetAllSongs();
        }
    }
}