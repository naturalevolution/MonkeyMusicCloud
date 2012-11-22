using System.Windows.Input;
using MicroMvvm;
using MonkeyMusicCloud.Client.Observers;

namespace MonkeyMusicCloud.Client.ViewModels.BodyViewModels
{
    public class SearchViewModel : ViewModelBase
    {
        public ICommand SearchSongsListCommand
        {
            get { return new RelayCommand<string>(SearchSongsExecute); }
        }

        private void SearchSongsExecute(string filter)
        {
            ContentBodyObserver.NotifyNewSeach(Service.SearchSongs(filter), filter);
        }
    }
}