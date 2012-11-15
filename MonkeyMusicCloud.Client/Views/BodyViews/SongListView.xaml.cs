using System.Windows.Controls;
using System.Windows.Input;
using MicroMvvm;
using MonkeyMusicCloud.Client.Events;
using MonkeyMusicCloud.Resource;

namespace MonkeyMusicCloud.Client.Views.BodyViews
{
    public partial class SongListView : UserControl, IBodyView
    {
        public SongListView()
        {
            InitializeComponent();
        }

        public string MenuLabel
        {
            get { return MusicResource.MenuSongList; }
        }

        // Pas moyen d'hériter d'une classe abstraite à cause du xaml + partial class
        public ICommand ChangeViewCommand
        {
            get { return new RelayCommand(ChangeViewExecute, () => true); }
        }

        // Pas moyen d'hériter d'une classe abstraite à cause du xaml + partial class
        public void ChangeViewExecute()
        {
            EventsManager.InvokeChangeContentView(this);
        }
    }
}