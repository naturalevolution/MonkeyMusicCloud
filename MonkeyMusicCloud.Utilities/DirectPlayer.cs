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
        public void Play(byte[] file)
        {
            Stop();
            string path = ConfigurationManager.AppSettings["MediaCache"] + file.Length +".mp3";
            File.WriteAllBytes(path, file);
            objFilterGraph = new FilgraphManager();
            objFilterGraph.RenderFile(path);
            objMediaPosition = objFilterGraph as IMediaPosition;

            objFilterGraph.Run();

            Timer tmProgressionFlux = new Timer(1000) {Enabled = true}; 
            tmProgressionFlux.Elapsed += TmProgressionFluxTick;
        }

        public void Stop()
        {
            if (objFilterGraph != null)
            {
                objFilterGraph.Stop();
                objMediaPosition.CurrentPosition = 0;
            }
            objFilterGraph = null;
        }

        private void TmProgressionFluxTick(object sender, EventArgs e)
        {
            int total = (int) objMediaPosition.Duration;
            int current = (int) objMediaPosition.CurrentPosition;
            int purcentage = 100*current/total;
            PurcentagePlayed.Invoke(purcentage);
            if (purcentage == 100)
            {
                SongFinished.Invoke();
            }
        }
        public event PurcentagePlayedHandler PurcentagePlayed;
        public event SongFinishedHandler SongFinished;
    }
}
