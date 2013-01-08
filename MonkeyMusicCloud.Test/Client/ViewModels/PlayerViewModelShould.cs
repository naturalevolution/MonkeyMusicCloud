using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonkeyMusicCloud.Client.Exceptions;
using MonkeyMusicCloud.Client.Observers;
using MonkeyMusicCloud.Client.ViewModels;
using MonkeyMusicCloud.Domain.Model;
using MonkeyMusicCloud.Test.Helper;
using MonkeyMusicCloud.Test.Helper.EventCatchers;
using Moq;

namespace MonkeyMusicCloud.Test.Client.ViewModels
{
    [TestClass]
    public class PlayerViewModelShould : ViewModelsBaseTest
    {
        [TestMethod]
        public void CallMusicPlayerPlayWhenAPlaySongEventIsCatched()
        {
            MediaFile file = Create.MediaFile();
            Song songToPlay = Create.Song(file);
            PlayerViewModel viewModel = new PlayerViewModel();
            MockService.Setup(s => s.GetMediaFileById(songToPlay.MediaFileId)).Returns(file);
            
            PlayerObserver.NotifyPlayNewSong(songToPlay);

            MockMusicPlayer.Verify(mp => mp.Stop());
            MockService.Verify(s => s.GetMediaFileById(songToPlay.MediaFileId));
            MockMusicPlayer.Verify(mp => mp.Play(file.Id, file.Content));
            Assert.AreEqual(songToPlay, viewModel.CurrentSong);
        }

        [TestMethod]
        public void IfPlaySongReturnsAnExceptionCatchIt()
        {
            ExceptionEventCatcher catcher = new ExceptionEventCatcher();
            MediaFile file = Create.MediaFile();
            Song songToPlay = Create.Song(file);
            PlayerViewModel viewModel = new PlayerViewModel();
            MockService.Setup(s => s.GetMediaFileById(songToPlay.MediaFileId)).Returns(file);
            MockMusicPlayer.Setup(mp => mp.Play(file.Id, file.Content)).Throws<Exception>();
            
            PlayerObserver.NotifyPlayNewSong(songToPlay);

            Assert.IsTrue(catcher.ExceptionEventInvoked);
            Assert.IsTrue(catcher.Exception is PlayException);
        }

        [TestMethod]
        public void CallMusicPlayerResumeWhenAResumeSongEventIsCatched()
        {
            PlayerViewModel viewModel = new PlayerViewModel();

            PlayerObserver.NotifyResumeSong();

            MockMusicPlayer.Verify(mp => mp.Resume());
        }

        [TestMethod]
        public void CallMusicPlayerPauseWhenAPauseSongEventIsCatched()
        {
            PlayerViewModel viewModel = new PlayerViewModel();

            PlayerObserver.NotifyPauseSong();

            MockMusicPlayer.Verify(mp => mp.Pause());
        }
        
        [TestMethod]
        public void CallMusicPlayerStopWhenAStopSongEventIsCatched()
        {
            PlayerViewModel viewModel = new PlayerViewModel
                                            {
                                                CurrentSong = Create.Song(), 
                                                ElapsedTime = "0",
                                                PurcentagePlayed = 0,TotalTime = "0"
                                            };

            PlayerObserver.NotifyStopSong();

            MockMusicPlayer.Verify(mp => mp.Stop());
            Assert.IsNull(viewModel.ElapsedTime);
            Assert.IsNull(viewModel.TotalTime);
            Assert.IsNull(viewModel.CurrentSong);
            Assert.AreEqual(0, viewModel.PurcentagePlayed);
        }

        [TestMethod]
        public void RaiseNewSongFinishedEventWhenASongIsFinishedAndCleanAllValue()
        {
            PlayerEventCatcher catcher = new PlayerEventCatcher();
            PlayerViewModel viewModel = new PlayerViewModel
                                            {
                                                CurrentSong = Create.Song(),
                                                ElapsedTime = "0",
                                                PurcentagePlayed = 0,
                                                TotalTime = "0"
                                            };  

            MockMusicPlayer.Raise(mp => mp.SongFinished += null);

            Assert.IsTrue(catcher.SongFinishedInvoked);
            Assert.IsNull(viewModel.ElapsedTime);
            Assert.IsNull(viewModel.TotalTime);
            Assert.IsNull(viewModel.CurrentSong);
            Assert.AreEqual(0, viewModel.PurcentagePlayed);
        }

        [TestMethod]
        public void RefreshTimePlayedAndPurcentage()
        {
            const int elapsedTime = 3010;
            const int totalTime = 3690;
            PlayerViewModel viewModel = new PlayerViewModel {SliderIsOnDrag = false};

            MockMusicPlayer.Raise(mp => mp.PurcentagePlayed += null, elapsedTime, totalTime );

            Assert.AreEqual(elapsedTime * 100 / totalTime, viewModel.PurcentagePlayed);
            Assert.AreEqual("00:50:10", viewModel.ElapsedTime);
            Assert.AreEqual("01:01:30", viewModel.TotalTime);
        }

        [TestMethod]
        public void RefreshTimePlayedWithoutPurcentageIfSliderIsDragging()
        {
            const int elapsedTime = 3010;
            const int totalTime = 3690;
            PlayerViewModel viewModel = new PlayerViewModel { 
                SliderIsOnDrag = true,
                PurcentagePlayed = 5
            };

            MockMusicPlayer.Raise(mp => mp.PurcentagePlayed += null, elapsedTime, totalTime);

            Assert.AreEqual(5, viewModel.PurcentagePlayed);
            Assert.AreEqual("00:50:10", viewModel.ElapsedTime);
            Assert.AreEqual("01:01:30", viewModel.TotalTime);
        }

        [TestMethod]
        public void DoNotRefreshTimeIfTotalTimeIsEqualToZero()
        {
            const int elapsedTime = 3010;
            const int totalTime = 0;
            PlayerViewModel viewModel = new PlayerViewModel
            {
                SliderIsOnDrag = true,
                PurcentagePlayed = 5
            };

            MockMusicPlayer.Raise(mp => mp.PurcentagePlayed += null, elapsedTime, totalTime);

            Assert.AreEqual(5, viewModel.PurcentagePlayed);
            Assert.AreEqual("00:00:00", viewModel.ElapsedTime);
            Assert.AreEqual("00:00:00", viewModel.TotalTime);
        }

        [TestMethod]
        public void SetSliderIsOnDragOnStartDragCommand()
        {
            PlayerViewModel viewModel = new PlayerViewModel { SliderIsOnDrag = false };

            viewModel.StartDragCommand.Execute(null);

            Assert.IsTrue(viewModel.SliderIsOnDrag);
        }

        [TestMethod]
        public void SetSliderIsNotOnDragOnStopDragCommandAndMoveMusicToTheValue()
        {
            PlayerViewModel viewModel = new PlayerViewModel
                {
                    SliderIsOnDrag = true,
                    CurrentSong = Create.Song()
                };
            const double value = 50;
            
            viewModel.StopDragCommand.Execute(value);

            Assert.IsFalse(viewModel.SliderIsOnDrag);
            MockMusicPlayer.Verify(mp => mp.PlayAt(value));
        }

        [TestMethod]
        public void SetValueToZeroIfNoActualSongIsPlayed()
        {
            PlayerViewModel viewModel = new PlayerViewModel
                {
                    SliderIsOnDrag = false,
                    CurrentSong = null
                    
                };
            const double value = 50;

            viewModel.StopDragCommand.Execute(value);

            MockMusicPlayer.Verify(mp => mp.PlayAt(It.IsAny<double>()), Times.Never());
            Assert.AreEqual(0, viewModel.PurcentagePlayed);
        }
    }
}
