using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonkeyMusicCloud.Client.ViewModels.SubViewModels;
using MonkeyMusicCloud.Domain.Model;
using MonkeyMusicCloud.Test.Helper;

namespace MonkeyMusicCloud.Test.Client.ViewModels.SubViewModels
{
    [TestClass]
    public class PlayListShould
    {
        [TestMethod]
        public void InstantiateCorrectly()
        {
            PlayList playList = new PlayList();
            Assert.AreEqual(0, playList.OriginalSongs.Count);
            Assert.AreEqual(0, playList.SongsToPlay.Count);
        }

        [TestMethod]
        public void AddSong()
        {
            PlayList playList = new PlayList();
            Song song = Create.Song();
            
            playList.AddSongs(new ObservableCollection<Song>{song});

            Assert.AreEqual(1, playList.OriginalSongs.Count);
            Assert.AreEqual(1, playList.SongsToPlay.Count);
            CollectionAssert.Contains(playList.OriginalSongs.Select(sts => sts.Song).ToList(), song );
            CollectionAssert.Contains(playList.SongsToPlay.Select(stt => stt.Song).ToList(), song );
        }

        [TestMethod]
        public void AddSongOnlyOnce()
        {
            PlayList playList = new PlayList();
            Song song = Create.Song();

            playList.AddSongs(new ObservableCollection<Song> { song });
            playList.AddSongs(new ObservableCollection<Song> { song });

            Assert.AreEqual(1, playList.OriginalSongs.Count);
            Assert.AreEqual(1, playList.SongsToPlay.Count);
            CollectionAssert.Contains(playList.OriginalSongs.Select(sts => sts.Song).ToList(), song);
            CollectionAssert.Contains(playList.SongsToPlay.Select(stt => stt.Song).ToList(), song);
        }

        [TestMethod]
        public void Clear()
        {
            PlayList playList = new PlayList();
            Song song = Create.Song();

            playList.AddSongs(new ObservableCollection<Song> { song });
            playList.Clear();

            Assert.AreEqual(0, playList.OriginalSongs.Count);
            Assert.AreEqual(0, playList.SongsToPlay.Count);
        }

        [TestMethod]
        public void GetActualSong()
        {
            SongToPlay song1 = new SongToPlay();
            SongToPlay song2 = new SongToPlay {IsPlaying = true};
            PlayList playList = new PlayList();
            playList.SongsToPlay.Add(song1);
            playList.SongsToPlay.Add(song2);

            Assert.AreEqual(song2, playList.ActualSong);
        }

        [TestMethod]
        public void ReturnNullIfTheyAreNoSongOnPlayingMode()
        {
            SongToPlay song1 = new SongToPlay();
            PlayList playList = new PlayList();
            playList.SongsToPlay.Add(song1);

            Assert.IsNull(playList.ActualSong);
        }

        [TestMethod]
        public void GetNextSong()
        {
            SongToPlay song1 = new SongToPlay();
            SongToPlay song2 = new SongToPlay { IsPlaying = true };
            SongToPlay song3 = new SongToPlay();
            PlayList playList = new PlayList();
            playList.SongsToPlay.Add(song1);
            playList.SongsToPlay.Add(song2);
            playList.SongsToPlay.Add(song3);

            Assert.AreEqual(song3, playList.GetNextSong());
        }

        [TestMethod]
        public void GetPreviousSong()
        {
            SongToPlay song1 = new SongToPlay();
            SongToPlay song2 = new SongToPlay { IsPlaying = true };
            SongToPlay song3 = new SongToPlay();
            PlayList playList = new PlayList();
            playList.SongsToPlay.Add(song1);
            playList.SongsToPlay.Add(song2);
            playList.SongsToPlay.Add(song3);

            Assert.AreEqual(song1, playList.GetPreviousSong());
        }

        [TestMethod]
        public void RestaureCollection()
        {
            Song song1 = Create.Song();
            Song song2 = Create.Song();
            Song song3 = Create.Song();
            PlayList playList = new PlayList();
            playList.AddSongs(new ObservableCollection<Song>{ song1, song2, song3});

            playList.Mix();
            playList.Restaure();

            Assert.AreEqual(playList.SongsToPlay[0], playList.OriginalSongs[0]);
            Assert.AreEqual(playList.SongsToPlay[1], playList.OriginalSongs[1]);
            Assert.AreEqual(playList.SongsToPlay[2], playList.OriginalSongs[2]);
        }

        [TestMethod]
        public void GetFirst()
        {
            SongToPlay song1 = new SongToPlay();
            SongToPlay song2 = new SongToPlay();
            SongToPlay song3 = new SongToPlay();
            PlayList playList = new PlayList();
            playList.SongsToPlay.Add(song1);
            playList.SongsToPlay.Add(song2);
            playList.OriginalSongs.Add(song3);

            Assert.AreEqual(song1, playList.GetFirst());
        }

        [TestMethod]
        public void ResetAllSongs()
        {
            SongToPlay song1 = new SongToPlay {Played = true};
            SongToPlay song2 = new SongToPlay ();
            SongToPlay song3 = new SongToPlay {Played = true, IsPlaying = true};
            PlayList playList = new PlayList();
            playList.SongsToPlay.Add(song1);
            playList.SongsToPlay.Add(song2);
            playList.SongsToPlay.Add(song3);

            playList.ResetAllSongs();

            Assert.IsFalse(song1.Played);
            Assert.IsFalse(song2.Played);
            Assert.IsFalse(song3.Played);
            Assert.IsFalse(song1.IsPlaying);
            Assert.IsFalse(song2.IsPlaying);
            Assert.IsFalse(song3.IsPlaying);
        }

        [TestMethod]
        public void GetTheLast()
        {
            SongToPlay song1 = new SongToPlay ();
            SongToPlay song2 = new SongToPlay();
            SongToPlay song3 = new SongToPlay();
            PlayList playList = new PlayList();
            playList.SongsToPlay.Add(song1);
            playList.SongsToPlay.Add(song2);
            playList.SongsToPlay.Add(song3);

            Assert.AreEqual(song3, playList.GetLast());
        }

        [TestMethod]
        public void SayIfItTheFirstThatIsPlaying()
        {
            SongToPlay song1 = new SongToPlay(){IsPlaying = true, Played = true};
            SongToPlay song2 = new SongToPlay();
            PlayList playList = new PlayList();
            playList.SongsToPlay.Add(song1);
            playList.SongsToPlay.Add(song2);

            Assert.IsTrue(playList.PlayingTheFirst);
        }

        [TestMethod]
        public void ReturnIsEmpty()
        {
            Song song1 = Create.Song();
            PlayList playList = new PlayList();

            Assert.IsTrue(playList.IsEmpty);

            playList.AddSongs(new ObservableCollection<Song> { song1});

            Assert.IsFalse(playList.IsEmpty);
        }

        [TestMethod]
        public void ReturnIsFinished()
        {
            SongToPlay song1 = new SongToPlay { Played = true };
            SongToPlay song2 = new SongToPlay();
            SongToPlay song3 = new SongToPlay { Played = true};
            PlayList playList = new PlayList();
            playList.SongsToPlay.Add(song1);
            playList.SongsToPlay.Add(song2);
            playList.SongsToPlay.Add(song3);

            Assert.IsFalse(playList.IsFinished);
            
            song3.IsPlaying = true;

            Assert.IsTrue(playList.IsFinished);
        }
    }
}
