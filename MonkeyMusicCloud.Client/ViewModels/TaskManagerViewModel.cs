using System.Collections.ObjectModel;
using System.Linq;
using MonkeyMusicCloud.Client.Observers;
using MonkeyMusicCloud.Client.ViewModels.SubViewModels;

namespace MonkeyMusicCloud.Client.ViewModels
{
    public class TaskManagerViewModel : ViewModelBase
    {
        private ObservableCollection<Task> taskList;

        public TaskManagerViewModel()
        {
            TaskList = new ObservableCollection<Task>();
            TaskObserver.AddTask += AddNewTask;
        }

        public ObservableCollection<Task> TaskList
        {
            get { return taskList; }
            set
            {
                taskList = value;
                RaisePropertyChanged("TaskList");
            }
        }

        public bool ThreadInProgress { get; set; }

        private void AddNewTask(Task taskToAdd)
        {
            TaskList.Add(taskToAdd);
            taskToAdd.ActionFinished += delegate { RunNextTask(taskToAdd); };
            if (!ThreadInProgress)
            {
                taskToAdd.DoActionInNewThread();
                ThreadInProgress = true;
            }
        }

        private void RunNextTask(Task taskToAdd)
        {
            TaskList.Remove(taskToAdd);
            if (TaskList.Any())
            {
                TaskList.First().DoActionInNewThread();
            }
            else
            {
                ThreadInProgress = false;
            }
        }
    }
}