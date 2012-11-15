using System.Windows;
using System.Windows.Controls;
using MonkeyMusicCloud.Client.Events;
using MonkeyMusicCloud.Client.Views.BodyViews;

namespace MonkeyMusicCloud.Client.Views
{
    /// <summary>
    /// Logique d'interaction pour ContentBodyView.xaml
    /// </summary>
    public partial class ContentBodyView : UserControl
    {
        public ContentBodyView()
        {
            InitializeComponent();
            EventsManager.ChangeContentView += delegate(IBodyView view)
                                                   {
                                                       gContent.Children.Clear();
                                                       gContent.Children.Add((UIElement) view);
                                                   };
        }
        
    }
}