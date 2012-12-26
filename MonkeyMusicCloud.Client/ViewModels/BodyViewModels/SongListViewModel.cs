using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Documents;
using System.Windows.Input;
using MicroMvvm;
using MonkeyMusicCloud.Client.Observers;
using MonkeyMusicCloud.Client.ViewModels.SubViewModels;
using MonkeyMusicCloud.Client.Views.BodyViews;
using MonkeyMusicCloud.Domain.Model;
using MonkeyMusicCloud.Resource;

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
        
        public ICommand AddOneSongToPlayListCommand
        {
            get { return new RelayCommand<Song>(AddOneSongToPlayListExecute); }
        }

        public ICommand DownloadOneSongCommand
        {
            get { return new RelayCommand<Song>(DownloadOneSongExecute); }
        }
        public ICommand DownloadSongListCommand
        {
            get { return new RelayCommand<ObservableCollection<Song>>(DownloadSeveralSongExecute); }
        }

        private void DownloadSeveralSongExecute(ObservableCollection<Song> songListToDownload)
        {
            foreach (Song song in songListToDownload)
            {
                TaskObserver.NotifyAddTask(new DownloadTask(song));
            }
        }


        private void DownloadOneSongExecute(Song song)
        {
            TaskObserver.NotifyAddTask(new DownloadTask(song));
        }

        public ICommand OpenArtistCommand
        {
            get { return new RelayCommand<string>(OpenArtistExecute); }
        }

        private void OpenArtistExecute(string artist)
        {
            ContentBodyObserver.NotifyChangeContentView(new MenuItem { Label = string.Format(MusicResource.ArtistTab, artist), View = new ArtistDetailView(artist) });
        }

        public ICommand AddSongsToPlayListCommand
        {
            get { return new RelayCommand<IList>(AddSongsToPlayListExecute); }
        }

        private void AddSongsToPlayListExecute(IList songs)
        {
            if (songs != null)
            {
                PlayerObserver.NotifyAddToPlayList(new ObservableCollection<Song>(songs.Cast<Song>()));    
            }
        }

        public ICommand OpenAlbumCommand
        {
            get { return new RelayCommand<string>(OpenAlbumExecute); }
        }

        

        private void OpenAlbumExecute(string album)
        {
            ContentBodyObserver.NotifyChangeContentView(new MenuItem { Label = string.Format(MusicResource.AlbumTab, album), View = new AlbumDetailView(album) });
        }
        
        private void AddOneSongToPlayListExecute(Song song)
        {

            PlayerObserver.NotifyAddToPlayList(new ObservableCollection<Song> { song });
        }
    }
}