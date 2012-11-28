using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using MonkeyMusicCloud.Client.Views.BodyViews;
using MonkeyMusicCloud.Client.Views.CustomControls;
using MonkeyMusicCloud.Domain.Model;

namespace MonkeyMusicCloud.Client.Views.CustomViews
{
    public partial class AlbumListView : UserControl, IBodyView
    {
        public AlbumListView()
        {
            InitializeComponent();
        }

        static PropertyMetadata Propertymetadata = new PropertyMetadata(new ObservableCollection<string>(), AlbumListPropertyChanged);
        
        public static readonly DependencyProperty MyCustomProperty = DependencyProperty.Register("AlbumList", typeof(ObservableCollection<string>), 
                                                                                                 typeof(AlbumListView), Propertymetadata, AlbumListValidate);

        private static void AlbumListPropertyChanged(DependencyObject dobj, DependencyPropertyChangedEventArgs e)
        {
            ObservableCollection<string> albums = (ObservableCollection<string>) e.NewValue;
            ((AlbumListView)dobj).elementFlow.ItemsSource = albums;
        }

        private static bool AlbumListValidate(object Value)
        {
            return true;
        }

        public ObservableCollection<string> AlbumList
        {
            get
            {
                return GetValue(MyCustomProperty) as ObservableCollection<string>;
            }
            set
            {
                SetValue(MyCustomProperty, value);
            }
        }
    }
}