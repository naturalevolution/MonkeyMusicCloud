using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.IO;
using System.Linq;
using MonkeyMusicCloud.Client.ViewModels.SubViewModels.OrgItems;

namespace MonkeyMusicCloud.Client.ViewModels.BodyViewModels
{
    public class LibraryViewModel : ViewModelBase
    {
        private ObservableCollection<OrgItem> folders;

        public LibraryViewModel()
        {
            SearchForArtists();
        }

        public ObservableCollection<OrgItem> Folders
        {
            get { return folders; }
            set
            {
                folders = value;
                RaisePropertyChanged("Folders");
            }
        }

        private string RootPath
        {
            get { return ConfigurationManager.AppSettings["LibraryRoot"]; }
        }

        private void SearchForArtists()
        {
            ObservableCollection<OrgItem> orgItems = new ObservableCollection<OrgItem>();
            IEnumerable<string> artistPathes = Directory.GetDirectories(RootPath);
            foreach (string artistPath in artistPathes)
            {
                ObservableCollection<OrgItem> albumPath =
                    new ObservableCollection<OrgItem>(Directory.GetDirectories(artistPath)
                                                               .Select(
                                                                   oi =>
                                                                   new OrgItem(new ObservableCollection<OrgItem>(), oi,
                                                                               Path.GetFileName(oi))));
                OrgItem artist = new OrgItem(new ObservableCollection<OrgItem>(albumPath), artistPath,
                                             Path.GetFileName(artistPath));
                orgItems.Add(artist);
            }

            Folders = orgItems;
        }
    }
}