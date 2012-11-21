using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using MicroMvvm;
using MonkeyMusicCloud.Client.Observers;
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
