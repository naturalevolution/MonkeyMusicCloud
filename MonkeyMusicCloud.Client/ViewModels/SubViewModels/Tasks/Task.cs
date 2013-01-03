using System.ComponentModel;

namespace MonkeyMusicCloud.Client.ViewModels.SubViewModels.Tasks
{
    public abstract class Task : ViewModelBase
    {
        private TaskState state;

        public Task()
        {
            Worker = new BackgroundWorker();
            Worker.DoWork += delegate { DoAction(); };
            Worker.RunWorkerCompleted += delegate
                {
                    if (ActionFinished != null)
                    {
                        ActionFinished.Invoke();
                    }
                };
            State = TaskState.Waiting;
        }

        public BackgroundWorker Worker { get; set; }

        public TaskState State
        {
            get { return state; }
            set
            {
                state = value;
                RaisePropertyChanged("State");
            }
        }

        public abstract string StringDescription { get; }

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
        Running
    }
}