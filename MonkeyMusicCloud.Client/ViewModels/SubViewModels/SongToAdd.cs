using System;
using MonkeyMusicCloud.Domain.Model;

namespace MonkeyMusicCloud.Client.ViewModels.SubViewModels
{
    public class SongToAdd : ViewModelBase
    {
        public Song Song { get; set; }

        private bool isSelected;
        public bool IsSelected
        {
            get { return isSelected; }
            set { 
                isSelected = value;
                RaisePropertyChanged("IsSelected");
            }
        }

        public string Path { get; set; }
    }
}
