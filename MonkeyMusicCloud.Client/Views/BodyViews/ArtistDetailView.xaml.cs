using System.Windows.Controls;

namespace MonkeyMusicCloud.Client.Views.BodyViews
{
    /// <summary>
    /// Logique d'interaction pour ArtistDetailView.xaml
    /// </summary>
    public partial class ArtistDetailView : UserControl, IBodyView
    {
        public ArtistDetailView(string artist)
        {
            InitializeComponent();
            Artist.Content = artist;
        }
    }
}
