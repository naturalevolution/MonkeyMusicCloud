﻿using System.Collections.ObjectModel;
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


        public MenuViewModel()
        {
            Items = new ObservableCollection<MenuItem>()
                        {
                            new MenuItem {Label = MusicResource.MenuAddMusics, View = new AddSongsView()}
                        };



        }

        private ObservableCollection<MenuItem> items;
        public ObservableCollection<MenuItem> Items
        {
            get { return items; }
            set { 
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