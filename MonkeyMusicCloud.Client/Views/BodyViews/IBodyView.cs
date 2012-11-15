using System.Windows.Input;

namespace MonkeyMusicCloud.Client.Views.BodyViews
{
    public interface IBodyView 
    {
        string MenuLabel { get; }

        ICommand ChangeViewCommand { get; }

        void ChangeViewExecute();
    }
}
