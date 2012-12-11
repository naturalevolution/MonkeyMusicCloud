using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using MonkeyMusicCloud.Client.Observers;
using MonkeyMusicCloud.Client.ViewModels.SubViewModels;

namespace MonkeyMusicCloud.Client.ViewModels
{
    public class TaskManagerViewModel
    {
        public TaskManagerViewModel()
        {
            TaskList = new ObservableCollection<Task>();
            TaskObserver.AddTask += AddNewTask;
        }

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

        private ObservableCollection<Task> _taskList;
        public ObservableCollection<Task> TaskList
        {
            get { return _taskList; }
            set { _taskList = value; }
        }

        public bool ThreadInProgress { get; set; }
    }
}