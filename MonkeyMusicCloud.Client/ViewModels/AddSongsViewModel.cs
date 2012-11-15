using System.Windows.Input;
using MicroMvvm;
using MonkeyMusicCloud.Client.Utilities;
using MonkeyMusicCloud.Domain.Model;
using MonkeyMusicCloud.Utilities.Interface;

namespace MonkeyMusicCloud.Client.ViewModels
{
    public class AddSongsViewModel : ViewModelBase
    {
        private IStreamHelper StreamHelper { get { return StreamInstance.GetInstance().StreamHelper; } }
        public string SongPath { get; set; }

        public void AddSongExecute()
        {
            if (!string.IsNullOrEmpty(SongPath))
            {
                File file = new File(StreamHelper.ReadToEnd(SongPath));
                Song song = new Song(file);
                Service.AddASong(song);
            }
        }

        public ICommand AddSongCommand { get { return new RelayCommand(AddSongExecute, () => true); } }
    }
}
