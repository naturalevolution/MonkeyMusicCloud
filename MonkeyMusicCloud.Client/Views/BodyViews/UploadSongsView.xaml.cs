using System.Windows;
using System.Windows.Forms;
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
                LblRootPath.Content = folderDialog.SelectedPath;
            }
        }
    }
}
