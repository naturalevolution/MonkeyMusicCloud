using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.IO;
using System.Linq;
using MonkeyMusicCloud.Client.Views.CustomControls.OrgTree;

namespace MonkeyMusicCloud.Client.ViewModels.BodyViewModels
{
    public class LibraryViewModel : ViewModelBase
    {
        private ObservableCollection<OrgViewItem> folders;

        public LibraryViewModel()
        {
            SearchForArtists();
        }

        public ObservableCollection<OrgViewItem> Folders  
        {
            get { return folders; }
            set { 
                folders = value;
                RaisePropertyChanged("Folders");
            }
        }

        private void SearchForArtists()
        {
            string rootPath = ConfigurationManager.AppSettings["LibraryRoot"];
            ObservableCollection<OrgItem> orgItems = new ObservableCollection<OrgItem>();
            IEnumerable<string> artistPathes = Directory.GetDirectories(rootPath);
            foreach (string artistPath in artistPathes)
            {
                ObservableCollection<Album> albumPath = new ObservableCollection<Album>(Directory.GetDirectories(artistPath).Select(oi => new Album(Path.GetFileName(oi), null)));
                Artist artist = new Artist(Path.GetFileName(artistPath), new ObservableCollection<OrgItem>(albumPath));
                orgItems.Add(artist);
            }

            Folders = OrgViewItem.GetViewArray(orgItems);
        }
    }
}