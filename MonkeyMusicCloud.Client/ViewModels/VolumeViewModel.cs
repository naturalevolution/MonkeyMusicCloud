namespace MonkeyMusicCloud.Client.ViewModels
{
    public class VolumeViewModel : ViewModelBase
    {
        public VolumeViewModel()
        {
            Volume = 1;
        }

        private double volume;
        public double Volume
        {
            get { return volume; }
            set { 
                volume = value;
                MusicPlayer.ChangeVolume(volume);
                RaisePropertyChanged("Volume");}
        }
    }
}