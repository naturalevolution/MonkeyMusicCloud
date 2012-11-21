using MonkeyMusicCloud.Client.ViewModels.SubViewModels;

namespace MonkeyMusicCloud.Client.Observers
{
    public class ContentBodyObserver
    {
        public static event ChangeContentViewHandler ChangeContentView;
        public static void NotifyChangeContentView(MenuItem item)
        {
            ChangeContentViewHandler handler = ChangeContentView;
            if (handler != null) handler(item);
        }
    }

    public delegate void ChangeContentViewHandler(MenuItem item);
}
