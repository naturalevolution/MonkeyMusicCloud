using System.Collections.ObjectModel;
using MonkeyMusicCloud.Client.ViewModels.SubViewModels;
using MonkeyMusicCloud.Domain.Model;

namespace MonkeyMusicCloud.Client.Observers
{
    public class TaskObserver
    {
        public static event AddTaskListHandler AddTask;
        public static void NotifyAddTask(Task task)
        {
            AddTaskListHandler handler = AddTask;
            if (handler != null) handler(task);
        }
    }

    public delegate void AddTaskListHandler(Task task);
}
