using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MonkeyMusicCloud.Client.ViewModels.BodyViewModels;
using MonkeyMusicCloud.Domain.Model;

namespace MonkeyMusicCloud.Client.Views.BodyViews
{
    public partial class SongListView : UserControl, IBodyView
    {
        private static PropertyMetadata Propertymetadata = new PropertyMetadata(new ObservableCollection<Song>(),
                                                                                SongListPropertyChanged);

        public static readonly DependencyProperty MyCustomProperty = DependencyProperty.Register("SongList", typeof (ObservableCollection<Song>),
                                                                                                 typeof (SongListView), Propertymetadata,
                                                                                                 SongListValidate);


        public SongListView()
        {
            InitializeComponent();
        }

        public ObservableCollection<Song> SongList
        {
            get { return GetValue(MyCustomProperty) as ObservableCollection<Song>; }
            set { SetValue(MyCustomProperty, value); }
        }

        private static void SongListPropertyChanged(DependencyObject dobj, DependencyPropertyChangedEventArgs e)
        {
            ((SongListView) dobj).LvSongList.ItemsSource = (ObservableCollection<Song>) e.NewValue;
        }

        private static bool SongListValidate(object Value)
        {
            return true;
        }

        private void ListViewItemMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ListViewItem item = sender as ListViewItem;
            if (item != null && item.Content.GetType() == typeof (Song))
            {
                Song song = (Song) item.Content;
                ((SongListViewModel) DataContext).AddOneSongToPlayListCommand.Execute(song);
            }
        }


        private void List_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && LvSongList.SelectedItems != null)
            {
                DataObject obj = new DataObject();
                obj.SetData(typeof (ObservableCollection<Song>), LvSongList.SelectedItems);
                DragDrop.DoDragDrop(LvSongList, obj, DragDropEffects.Copy | DragDropEffects.Move);
            }
        }

    }
}