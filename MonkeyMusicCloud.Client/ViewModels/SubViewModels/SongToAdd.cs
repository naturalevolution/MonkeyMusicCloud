using MonkeyMusicCloud.Domain.Model;

namespace MonkeyMusicCloud.Client.ViewModels.SubViewModels
{
    public class SongToAdd : ViewModelBase
    {
        public Song Song { get; set; }

        public bool IsSelected { get; set; }
        
        public string Path { get; set; }

        public MediaFile MediaFile{ get; set;}
    }
}
