using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Input;
using MicroMvvm;
using MonkeyMusicCloud.Client.Utilities;
using MonkeyMusicCloud.Client.ViewModels.SubViewModels;
using MonkeyMusicCloud.Domain.Model;
using MonkeyMusicCloud.Utilities.Interface;
using File = TagLib.File;

namespace MonkeyMusicCloud.Client.ViewModels.BodyViewModels
{
    public class UploadSongsViewModel : ViewModelBase
    {
        private string rootPath;
        private ObservableCollection<SongToAdd> songsToAdd;

        private IStreamHelper StreamHelper
        {
            get { return StreamInstance.GetInstance().StreamHelper; }
        }

        public ICommand AddSongCommand
        {
            get { return new RelayCommand(AddSongsExecute); }
        }

        public string RootPath
        {
            get { return rootPath; }
            set
            {
                SongsToAdd = GetAllSongsFromARootPath(value);
                rootPath = value;
            }
        }

        public ObservableCollection<SongToAdd> SongsToAdd
        {
            get { return songsToAdd; }
            set
            {
                songsToAdd = value;
                RaisePropertyChanged("SongsToAdd");
            }
        }

        private void AddSongsExecute()
        {
            if (songsToAdd != null)
            {
                foreach (SongToAdd songToAdd in SongsToAdd.Where(s => s.IsSelected))
                {
                    Service.AddASong(songToAdd.Song, songToAdd.MediaFile);
                }
            }
        }

        private ObservableCollection<SongToAdd> GetAllSongsFromARootPath(string root)
        {
            IList<SongToAdd> songs = new List<SongToAdd>();
            if (Directory.Exists(root))
            {
                foreach (string path in Directory.GetFiles(root))
                {
                    try
                    {
                        if (path.EndsWith(".mp3"))
                        {
                            File mp3 = File.Create(path);
                            MediaFile mediaFile = new MediaFile {Content = StreamHelper.ReadToEnd(path)};
                            Song song = new Song{
                                           Album = mp3.Tag.Album,
                                           Title = mp3.Tag.Title,
                                           Artist = mp3.Tag.FirstArtist
                                       };
                            songs.Add(new SongToAdd {IsSelected = true, Song = song, Path = path, MediaFile = mediaFile});
                        }
                    }
                    //TODO Logger le message
                    catch (Exception)
                    {
                        continue;
                    }
                }
                foreach (string directory in Directory.GetDirectories(root))
                {
                    foreach (SongToAdd song in GetAllSongsFromARootPath(directory))
                    {
                        songs.Add(song);
                    }
                }
            }
            return new ObservableCollection<SongToAdd>(songs);
        }
    }
}