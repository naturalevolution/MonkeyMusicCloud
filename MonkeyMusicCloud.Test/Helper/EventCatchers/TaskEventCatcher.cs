using System.Collections.Generic;
using MonkeyMusicCloud.Client.Observers;
using MonkeyMusicCloud.Client.ViewModels.SubViewModels.Tasks;

namespace MonkeyMusicCloud.Test.Helper.EventCatchers
{
    public class TaskEventCatcher
    {
        public TaskEventCatcher()
        {
            TaskListToAdd = new List<Task>();
            TaskObserver.AddTask += delegate(Task task) {
                AddTaskInvoked = true;
                TaskListToAdd.Add(task);
                AddTaskTimes++;
            };
        }

        public int AddTaskTimes = 0;
        public IList<Task> TaskListToAdd { get; set; }
        public bool AddTaskInvoked = false;
    }
}
