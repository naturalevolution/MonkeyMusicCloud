using System.Threading;

namespace MonkeyMusicCloud.Client.ViewModels.SubViewModels
{
    public abstract class Task : ViewModelBase
    {
        public Thread Thread { get; set; }
        public Task()
        {
            Thread = new Thread(DoAction);
        }

        public virtual void DoActionInNewThread()
        {
            Thread.Start();
        }

        protected abstract void DoAction();

        public event ActionFinishedHandler ActionFinished;
        public void NotifyActionFinished()
        {
            ActionFinishedHandler handler = ActionFinished;
            if (handler != null) handler();
        }

    }

    public delegate void ActionFinishedHandler();
}
