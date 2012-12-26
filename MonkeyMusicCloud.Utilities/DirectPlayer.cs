using System;
using System.Configuration;
using System.IO;
using System.Timers;
using MonkeyMusicCloud.Utilities.Interface;
using QuartzTypeLib;

namespace MonkeyMusicCloud.Utilities
{
    public class DirectPlayer : IMusicPlayer
    {
        private FilgraphManager objFilterGraph;
        private IMediaPosition objMediaPosition;
        private Timer tmProgressionFlux = new Timer(500);
        
        private static DirectPlayer instance;
        static readonly object InstanceLock = new object();

        public static DirectPlayer GetInstance()
        {
            lock (InstanceLock)
            {
                string directoryPath = ConfigurationManager.AppSettings["MediaCache"];
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }
                return instance ?? (instance = new DirectPlayer());
            }
        }

        //TODO Fichier déjà utilisé.... surement par Quartz. Essayer de libérer les ressources de la librairie
        public void Play(Guid id, byte[] file)
        {
            //pb d'accès concurrenciel
            string path = ConfigurationManager.AppSettings["MediaCache"] + id + ".mp3";
            File.WriteAllBytes(path, file);
            objFilterGraph = new FilgraphManager();
            objFilterGraph.RenderFile(path);
            objMediaPosition = objFilterGraph as IMediaPosition;

            objFilterGraph.Run();

            tmProgressionFlux = new Timer(1000) { Enabled = true }; 
            tmProgressionFlux.Elapsed += TmProgressionFluxTick;
            TimerResume();
        }
        public void Stop()
        {
            if (objFilterGraph != null)
            {
                objFilterGraph.Stop();
                
                objMediaPosition.CurrentPosition = 0;
            }
            objFilterGraph = null;
            TimerPause();
        }


        private void TmProgressionFluxTick(object sender, EventArgs e)
        {
            int total = (int) objMediaPosition.Duration;
            int current = (int) objMediaPosition.CurrentPosition;
            int purcentage = 100*current/total;
            PurcentagePlayed.Invoke(current, total);
            if (purcentage == 100)
            {
                SongFinished.Invoke();
            }
        }

        public void Pause()
        {
            objFilterGraph.Pause();
            TimerPause();
        }

        private void TimerPause()
        {
            tmProgressionFlux.Stop();
        }

        public void Resume()
        {
            objFilterGraph.Run();
            TimerResume();
        }

        private void TimerResume()
        {
            tmProgressionFlux.Start();
        }

        public void PlayAt(double purcentage)
        {
            Pause();
            int totalTime = (int) objMediaPosition.Duration;
            objMediaPosition.CurrentPosition =  totalTime * purcentage / 100;
            Resume();
        }

        public event PurcentagePlayedHandler PurcentagePlayed;
        public event SongFinishedHandler SongFinished;
        
    }
}
