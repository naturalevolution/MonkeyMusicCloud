using System;
using System.Collections.ObjectModel;
using System.Linq;
using MonkeyMusicCloud.Domain.Model;

namespace MonkeyMusicCloud.Client.ViewModels.SubViewModels
{
    public class PlayList : ViewModelBase
    {
        public ObservableCollection<SongToPlay> OriginalSongs { get; set; }
        public ObservableCollection<SongToPlay> SongsToPlay { get; set; }

        public PlayList()
        {   
            OriginalSongs = new ObservableCollection<SongToPlay>();
            SongsToPlay = new ObservableCollection<SongToPlay>();
        }

        public virtual bool IsFinished
        {
            get { return SongsToPlay[SongsToPlay.Count() - 1].IsPlaying; }
        }

        public virtual bool IsEmpty
        {
            get { return SongsToPlay.Count == 0; }
        }

        public virtual SongToPlay ActualSong
        {
            get
            {
                if (SongsToPlay.Count(stp => stp.IsPlaying) == 1)
                {
                    return SongsToPlay.First(stp => stp.IsPlaying);
                }
                return null;
            }
        }

        public virtual bool PlayingTheFirst
        {
            get { return SongsToPlay[0].IsPlaying; }
        }

        public virtual void AddSongs(ObservableCollection<Song> songs)
        {
            foreach (Song song in songs)
            {
                if (SongsToPlay.All(stp => stp.Song != song))
                {
                    SongToPlay songToPlay = new SongToPlay() { Song = song };
                    OriginalSongs.Add(songToPlay);
                    SongsToPlay.Add(songToPlay);    
                }
            }
        }

        public virtual void Clear()
        {
            OriginalSongs.Clear();
            SongsToPlay.Clear();
        }

        public virtual SongToPlay GetNextSong()
        {
            int index = SongsToPlay.IndexOf(ActualSong);
            return SongsToPlay[index + 1];
        }

        public virtual SongToPlay GetPreviousSong()
        {
            int index = SongsToPlay.IndexOf(ActualSong);
            return SongsToPlay[index - 1];
        }

        public virtual void Mix()
        {
            Random rng = new Random();
            int n = SongsToPlay.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                SongToPlay value = SongsToPlay[k];
                SongsToPlay[k] = SongsToPlay[n];
                SongsToPlay[n] = value;
            }  
        }

        public virtual void Restaure()
        {
            SongsToPlay = new ObservableCollection<SongToPlay>(OriginalSongs);
        }

        public virtual void ResetAllSongs()
        {
            foreach (SongToPlay songToPlay in SongsToPlay)
            {
                songToPlay.Played = false;
                songToPlay.IsPlaying = false;
            }
        }

        public virtual SongToPlay GetFirst()
        {
            return SongsToPlay.First();
        }

        public virtual SongToPlay GetLast()
        {
            return SongsToPlay[SongsToPlay.Count() - 1 ];
        }
    }
}