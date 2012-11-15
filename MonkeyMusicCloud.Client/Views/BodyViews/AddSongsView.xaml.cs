using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MicroMvvm;
using MonkeyMusicCloud.Client.Events;
using MonkeyMusicCloud.Resource;

namespace MonkeyMusicCloud.Client.Views.BodyViews
{
    /// <summary>
    /// Logique d'interaction pour AddSongsView.xaml
    /// </summary>
    public partial class AddSongsView : UserControl, IBodyView
    {
        public AddSongsView()
        {
            InitializeComponent();
        }

        private object dummyNode = null;

        public string SelectedImagePath { get; set; }

        private void ControlLoaded(object sender, RoutedEventArgs e)
        {
            foreach (string drive in Directory.GetLogicalDrives())
            {
                TreeViewItem item = new TreeViewItem {Header = drive, Tag = drive};
                item.Items.Add(dummyNode);
                item.Expanded += FolderExpanded;
                foldersItem.Items.Add(item);
            }
        }


        //TODO A Refaire, il me semble qu'on gérer ca directemnt en mvvm
        void FolderExpanded(object sender, RoutedEventArgs e)
        {
            TreeViewItem item = (TreeViewItem)sender;
            if (item.Items.Count == 1 && item.Items[0] == dummyNode)
            {
                item.Items.Clear();
                foreach (string directory in Directory.GetDirectories(item.Tag.ToString()))
                {
                    TreeViewItem subitem = new TreeViewItem
                                               {
                                                   Header = directory.Substring(directory.LastIndexOf("\\") + 1),
                                                   Tag = directory
                                               };
                    subitem.Items.Add(dummyNode);
                    subitem.Expanded += FolderExpanded;
                    item.Items.Add(subitem);
                }
            }
        }

        public string MenuLabel
        {
            get { return MusicResource.MenuAddMusics; }
        }

        // Pas moyen d'hériter d'une classe abstraite à cause du xaml + partial class
        public ICommand ChangeViewCommand
        {
            get { return new RelayCommand(ChangeViewExecute, () => true); }
        }

        // Pas moyen d'hériter d'une classe abstraite à cause du xaml + partial class
        public void ChangeViewExecute()
        {
            EventsManager.InvokeChangeContentView(this);
        }
    }
}
