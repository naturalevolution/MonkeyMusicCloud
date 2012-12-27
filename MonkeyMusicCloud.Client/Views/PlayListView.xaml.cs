using System.Windows.Controls;
using System.Windows.Input;
using MonkeyMusicCloud.Client.ViewModels;
using MonkeyMusicCloud.Domain.Model;

namespace MonkeyMusicCloud.Client.Views
{
    /// <summary>
    /// Logique d'interaction pour PlayListView.xaml
    /// </summary>
    public partial class PlayListView : UserControl
    {
        public PlayListView()
        {
            InitializeComponent();
        }

        private void ListViewItemMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ListViewItem item = sender as ListViewItem;
            if (item != null && item.Content.GetType() == typeof(Song))
            {
                Song song = (Song)item.Content;
                ((PlayListViewModel)DataContext).PlaySongCommand.Execute(song);
            }
        }
    }
}