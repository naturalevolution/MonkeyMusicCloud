using System;
using System.Windows.Controls;
using System.Windows.Input;
using MicroMvvm;
using MonkeyMusicCloud.Client.Events;
using MonkeyMusicCloud.Resource;

namespace MonkeyMusicCloud.Client.Views.BodyViews
{
    /// <summary>
    /// Logique d'interaction pour AddSongsView.xaml
    /// </summary>
    public partial class SongDetailView : UserControl,  IBodyView

    {
        public SongDetailView()
        {
            InitializeComponent();
        }

        public string MenuLabel
        {
            get { return MusicResource.MenuAddMusics; }
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
