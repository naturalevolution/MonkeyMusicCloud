//using System.Configuration;
//using System.IO;
//using System.Timers;
//using IrrKlang;
//using MonkeyMusicCloud.Utilities.Interface;
//using QuartzTypeLib;

//namespace MonkeyMusicCloud.Utilities
//{
//    public class KlangPlayer : IMusicPlayer
//    {
//        private static KlangPlayer instance;
//        readonly ISoundEngine engine = new ISoundEngine();
//        static readonly object InstanceLock = new object();
//        private ISound ActualSound { get; set; } 
        
//        public static KlangPlayer GetInstance()
//        {
//            lock (InstanceLock)
//            {
//                string directoryPath = ConfigurationManager.AppSettings["MediaCache"];
//                if (!Directory.Exists(directoryPath))
//                {
//                    Directory.CreateDirectory(directoryPath);
//                }
//                return instance ?? (instance = new KlangPlayer());
//            }
//        }


//        public void Play(byte[] file)
//        {
//            Stop();
//            string path = ConfigurationManager.AppSettings["MediaCache"] + "Actual.mp3";
//            File.WriteAllBytes(path, file);
//            ActualSound = engine.Play3D(path, 1, 1, 1);

            


//            Timer timer = new Timer(1000);
//            timer.Elapsed += NotifyTimeElapse;
//            timer.Start();
            
//        }

//        private void NotifyTimeElapse(object sender, ElapsedEventArgs elapsedEventArgs)
//        {
//        }

//        public void Stop()
//        {
//            engine.RemoveAllSoundSources();
//        }

//        public event PurcentagePlayedHandler PurcentagePlayed;
//    }
//}
