using MonkeyMusicCloud.Client.ViewModels.SubViewModels.Tasks;

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
