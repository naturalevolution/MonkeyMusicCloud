using System.ComponentModel;
using System.Threading;

namespace MonkeyMusicCloud.Client.ViewModels.SubViewModels
{
    public abstract class Task : ViewModelBase
    {
        public BackgroundWorker Worker { get; set; }

        public Task()
        {
            Worker = new BackgroundWorker();
            Worker.DoWork += delegate { DoAction(); };
            Worker.RunWorkerCompleted += delegate { ActionFinished.Invoke(); };
        }

        public virtual void DoActionInNewThread()
        {
            Worker.RunWorkerAsync();
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
