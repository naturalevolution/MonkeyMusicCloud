using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MonkeyMusicCloud.Client.Observers;
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

        private void DragOverMethode(object sender, DragEventArgs e)
        {
            e.Effects = e.Data.GetDataPresent(typeof(ObservableCollection<Song>)) ? DragDropEffects.Copy : DragDropEffects.None;
        }

        private void LvSongList_OnDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(ObservableCollection<Song>)))
            {
                e.Effects = DragDropEffects.Copy;
                IList songsToAdd = (IList)e.Data.GetData(typeof(ObservableCollection<Song>));
                PlayerObserver.NotifyAddToPlayList(new ObservableCollection<Song>(songsToAdd.Cast<Song>()));
            }
            else
            {
                e.Effects = DragDropEffects.None;
            }
        }
    }
}