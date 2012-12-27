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
        public SongListView()
        {
            InitializeComponent();
        }

        static PropertyMetadata Propertymetadata = new PropertyMetadata(new ObservableCollection<Song>(), SongListPropertyChanged);
        
        public static readonly DependencyProperty MyCustomProperty = DependencyProperty.Register("SongList", typeof(ObservableCollection<Song>), typeof(SongListView),
                                                                            Propertymetadata, SongListValidate);

        private static void SongListPropertyChanged(DependencyObject dobj, DependencyPropertyChangedEventArgs e)
        {
            ((SongListView) dobj).LvSongList.ItemsSource = (ObservableCollection<Song>) e.NewValue;
        }

        private static bool SongListValidate(object Value)
        {
            return true;
        }

        public ObservableCollection<Song> SongList
        {
            get
            {
                return GetValue(MyCustomProperty) as ObservableCollection<Song>;
            }
            set
            {
                SetValue(MyCustomProperty, value);
            }
        }

        private void listViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ListViewItem item = sender as ListViewItem;
            if (item != null && item.Content.GetType() == typeof(Song))
            {
                Song song = (Song) item.Content;
                ((SongListViewModel)DataContext).AddOneSongToPlayListCommand.Execute(song);
            }
        }
    }
}