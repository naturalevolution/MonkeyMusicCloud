using System.Collections.ObjectModel;

namespace MonkeyMusicCloud.Client.ViewModels.SubViewModels.OrgItems
{
    public class OrgItem : ViewModelBase
    {
        private ObservableCollection<OrgItem> children;
        private string fullPath;
        private string isExpanded;
        private string name;

        public OrgItem(ObservableCollection<OrgItem> children, string fullPath, string name)
        {
            Children = children;
            FullPath = fullPath;
            Name = name;
        }

        public ObservableCollection<OrgItem> Children
        {
            get { return children; }
            set
            {
                children = value;
                RaisePropertyChanged("Children");
            }
        }

        public string FullPath
        {
            get { return fullPath; }
            set
            {
                fullPath = value;
                RaisePropertyChanged("FullPath");
            }
        }

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                RaisePropertyChanged("Name");
            }
        }

        public string IsExpanded
        {
            get { return isExpanded; }
            set
            {
                isExpanded = value;
                RaisePropertyChanged("IsExpanded");
            }
        }
    }
}