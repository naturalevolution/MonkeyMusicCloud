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
        ISoundEngine engine = new ISoundEngine();
        ISound ActualSound { get; set; }
        private Timer tmProgressionFlux = new Timer(500);

        public void Play(Guid id, byte[] file)
        {

            //pb d'accès concurrenciel
            string path = ConfigurationManager.AppSettings["MediaCache"] + id + ".mp3";
            File.WriteAllBytes(path, file);

            ActualSound = engine.Play3D(path, 1, 1, 1);

            tmProgressionFlux = new Timer(1000) { Enabled = true };
            tmProgressionFlux.Elapsed += TmProgressionFluxTick;
            TimerResume();
        }

        private void TmProgressionFluxTick(object sender, EventArgs e)
        {
            int total = (int)ActualSound.PlayLength / 1000;
            int current = (int)ActualSound.PlayPosition / 1000;
            PurcentagePlayed.Invoke(current, total);
            if (ActualSound.PlayPosition == ActualSound.PlayLength)
            {
                SongFinished.Invoke();
            }
        }

        private void TimerPause()
        {
            tmProgressionFlux.Stop();
        }

        private void TimerResume()
        {
            tmProgressionFlux.Start();
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