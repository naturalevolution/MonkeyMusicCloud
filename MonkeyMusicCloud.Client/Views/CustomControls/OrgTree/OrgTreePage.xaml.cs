using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace MonkeyMusicCloud.Client.Views.CustomControls.OrgTree
{
    public partial class OrgTreePage : UserControl
    {

        static PropertyMetadata Propertymetadata = new PropertyMetadata(new ObservableCollection<OrgViewItem>(), SongListPropertyChanged);

        public static readonly DependencyProperty ItemSourceProperty = DependencyProperty.Register("ItemSource", typeof(ObservableCollection<OrgViewItem>), typeof(OrgTreePage),
                                                                            Propertymetadata, SongListValidate);
        
        private static void SongListPropertyChanged(DependencyObject dobj, DependencyPropertyChangedEventArgs e)
        {
            ((OrgTreePage)dobj).Tree.ItemsSource = (ObservableCollection<OrgViewItem>)e.NewValue;
        }

        private static bool SongListValidate(object Value)
        {
            return true;
        }
        public OrgTreePage()
        {
            InitializeComponent();
        }


        public ObservableCollection<OrgViewItem> ItemSource
        {
            get { return (ObservableCollection<OrgViewItem>)GetValue(ItemSourceProperty); }
            set { SetValue(ItemSourceProperty, value); }
        }
    }
}
