using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using MicroMvvm;
using MonkeyMusicCloud.Client.Events;
using MonkeyMusicCloud.Resource;
using UserControl = System.Windows.Controls.UserControl;

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

        private void OpenSongFolderDialog(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();
            DialogResult result = folderDialog.ShowDialog();
            if(result == DialogResult.OK)
            {
                lblRootPath.Content = folderDialog.SelectedPath;
            }
        }
    }
}
