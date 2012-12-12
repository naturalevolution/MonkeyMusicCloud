using System.ComponentModel;
using System.Threading;

namespace MonkeyMusicCloud.Client.ViewModels.SubViewModels
{
    public abstract class Task : ViewModelBase
    {
        public BackgroundWorker Worker { get; set; }
        public TaskState State { get; set; }

        public Task()
        {
            Worker = new BackgroundWorker();
            Worker.DoWork += delegate { DoAction(); };
            Worker.RunWorkerCompleted += delegate
            {
                State = TaskState.Finished;
                if (ActionFinished != null)
                {
                    ActionFinished.Invoke();    
                }
            };
            State = TaskState.Waiting;
        }

        public virtual void DoActionInNewThread()
        {
            State = TaskState.Running;
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
    public enum TaskState
    {
        Waiting,
        Running,
        Finished
    }
}
