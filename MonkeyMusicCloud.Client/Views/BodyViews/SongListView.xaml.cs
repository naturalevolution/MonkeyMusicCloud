using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
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
            ((SongListView) dobj).lvSongList.ItemsSource = (ObservableCollection<Song>) e.NewValue;
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
    }
}