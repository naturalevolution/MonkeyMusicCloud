using System.Collections.ObjectModel;
using System.Windows.Input;
using MicroMvvm;
using MonkeyMusicCloud.Client.Observers;
using MonkeyMusicCloud.Client.ViewModels.BodyViewModels;
using MonkeyMusicCloud.Client.ViewModels.SubViewModels;
using MonkeyMusicCloud.Client.Views.BodyViews;
using MonkeyMusicCloud.Resource;

namespace MonkeyMusicCloud.Client.ViewModels
{
    public class ContentBodyViewModel : ViewModelBase
    {
        

        public ContentBodyViewModel ()
        {
            Items = new ObservableCollection<MenuItem>()
                        {
                            new MenuItem {Label = MusicResource.MenuSongList, View = new SearchView()},
                            new MenuItem {Label = MusicResource.MenuAddMusics, View = new AddSongsView()}
                        };

            ContentBodyObserver.ChangeContentView += delegate(MenuItem item)
                                                         {
                                                             Items.Add(item);
                                                             SelectedItem = item;
                                                         };


        }

        public ObservableCollection<MenuItem> Items { get; set; }

        private MenuItem selectedItem;
        public MenuItem SelectedItem
        {
            get {
                return selectedItem;
            }
            set {
                selectedItem = value;
                RaisePropertyChanged("SelectedItem");
            }
        }
        
        public ICommand CloseCommand
        {
            get { return new RelayCommand<MenuItem>(CloseExecute); }
        }

        private void CloseExecute(MenuItem item)
        {
            Items.Remove(item);
        }
    }
}
