using System;
using System.Configuration;
using System.IO;
using System.Timers;
using MonkeyMusicCloud.Utilities.Interface;
using IrrKlang;

namespace MonkeyMusicCloud.Utilities
{
    public class KlangPlayer : IMusicPlayer
    {
        ISoundEngine Engine { get; set; }
        ISound ActualSound { get; set; }
        Timer TmProgressionFlux { get; set; }

        public KlangPlayer()
        {
            try
            {
                Engine = new ISoundEngine();
            }
            catch (Exception)
            {
                Engine = null;
            }
            TmProgressionFlux = new Timer(500);

        }

        public void Play(Guid id, byte[] file)
        {
            string path = ConfigurationManager.AppSettings["MediaCache"] + id + ".mp3";
            File.WriteAllBytes(path, file);

            ActualSound = Engine.Play3D(path, 1, 1, 1);

            TmProgressionFlux = new Timer(1000) { Enabled = true };
            TmProgressionFlux.Elapsed += TmProgressionFluxTick;
            TimerResume();
        }

        //TODO BUG Actual song null :/
        private void TmProgressionFluxTick(object sender, EventArgs e)
        {
            if (ActualSound != null)
            {
                int total = (int)ActualSound.PlayLength / 1000;
                int current = (int)ActualSound.PlayPosition / 1000;
                PurcentagePlayed.Invoke(current, total);
                if (ActualSound.Finished)
                {
                    SongFinished.Invoke();
                }
            }
        }

        private void TimerPause()
        {
            TmProgressionFlux.Stop();
        }

        private void TimerResume()
        {
            TmProgressionFlux.Start();
        }

        public void Stop()
        {
            if (ActualSound != null)
            {
                ActualSound.Stop();

                ActualSound.PlayPosition = 0;
            }
            ActualSound = null;
            TimerPause();
        }

        public event PurcentagePlayedHandler PurcentagePlayed;
        public event SongFinishedHandler SongFinished;

        public void Pause()
        {
            ActualSound.Paused = true;
            TimerPause();
        }

        public void Resume()
        {
            ActualSound.Paused = false;
            TimerResume();
        }

        public void PlayAt(double purcentage)
        {
            Pause();
            int totalTime = (int)ActualSound.PlayLength;
            ActualSound.PlayPosition = (uint) (totalTime * purcentage / 100);
            Resume();
        }

        public void ChangeVolume(double volume)
        {
            if (ActualSound != null)
            {
                ActualSound.Volume = (float) volume;
            }
        }
    }
}