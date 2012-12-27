using System.Collections.ObjectModel;
using System.Configuration;
using System.Windows.Input;
using MicroMvvm;
using MonkeyMusicCloud.Client.Observers;
using MonkeyMusicCloud.Client.ViewModels.SubViewModels;
using MonkeyMusicCloud.Client.Views.BodyViews;
using MonkeyMusicCloud.Resource;

namespace MonkeyMusicCloud.Client.ViewModels
{
    public class MenuViewModel : ViewModelBase
    {
        private ObservableCollection<MenuItem> items;

        public MenuViewModel()
        {
            Items = new ObservableCollection<MenuItem>()
                {
                    new MenuItem
                        {
                            Label = MusicResource.UploadMenu,
                            View = new AddSongsView(),
                            ImagePath = ConfigurationManager.AppSettings["ImageFolder"] + "Upload.png"
                        },
                    new MenuItem
                        {
                            Label = MusicResource.LibraryMenu,
                            View = new LibraryView(),
                            ImagePath = ConfigurationManager.AppSettings["ImageFolder"] + "Library.png"
                        }
                };
        }

        public ObservableCollection<MenuItem> Items
        {
            get { return items; }
            set
            {
                items = value;
                RaisePropertyChanged("Items");
            }
        }

        public ICommand OpenItemCommand
        {
            get { return new RelayCommand<MenuItem>(OpenItemCommandExecute); }
        }

        private void OpenItemCommandExecute(MenuItem item)
        {
            ContentBodyObserver.NotifyChangeContentView(item);
        }
    }
}