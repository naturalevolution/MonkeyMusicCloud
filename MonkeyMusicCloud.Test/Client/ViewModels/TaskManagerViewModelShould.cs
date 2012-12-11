using System.Collections.ObjectModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonkeyMusicCloud.Client.Observers;
using MonkeyMusicCloud.Client.ViewModels;
using MonkeyMusicCloud.Client.ViewModels.SubViewModels;
using Moq;

namespace MonkeyMusicCloud.Test.Client.ViewModels
{
    [TestClass]
    public class TaskManagerViewModelShould
    {

        [TestMethod]
        public void CreatedCorrectly()
        {
            TaskManagerViewModel viewModel = new TaskManagerViewModel();

            Assert.AreEqual(0, viewModel.TaskList.Count);
        }


        [TestMethod]
        public void AddTaskAndRunItWhenTaskEventIsCatched()
        {
            Mock<Task> mockTask = new Mock<Task>();
            TaskManagerViewModel viewModel = new TaskManagerViewModel();

            TaskObserver.NotifyAddTask(mockTask.Object);

            Assert.AreEqual(1, viewModel.TaskList.Count);
            Assert.IsTrue(viewModel.ThreadInProgress);
            mockTask.Verify(mt => mt.DoActionInNewThread(), Times.Once());
            CollectionAssert.Contains(viewModel.TaskList, mockTask.Object);

        }

        [TestMethod]
        public void AddTaskAndDoNotRunItIfThreadInProgressWhenTaskEventIsCatched()
        {
            Mock<Task> mockTask = new Mock<Task>();
            TaskManagerViewModel viewModel = new TaskManagerViewModel
                {
                    ThreadInProgress = true, 
                    TaskList = new ObservableCollection<Task>{new Mock<Task>().Object}
                };

            TaskObserver.NotifyAddTask(mockTask.Object);

            Assert.AreEqual(2, viewModel.TaskList.Count);
            mockTask.Verify(mt => mt.DoActionInNewThread(), Times.Never());
            CollectionAssert.Contains(viewModel.TaskList, mockTask.Object);
        }

        [TestMethod]
        public void RunNextTaskWhenTheFirstIsFinished()
        {

            Mock<Task> mockTask1 = new Mock<Task>();
            Mock<Task> mockTask2 = new Mock<Task>();
            Mock<Task> mockTask3 = new Mock<Task>();
            TaskManagerViewModel viewModel = new TaskManagerViewModel();
            
            TaskObserver.NotifyAddTask(mockTask1.Object);
            TaskObserver.NotifyAddTask(mockTask2.Object);
            TaskObserver.NotifyAddTask(mockTask3.Object);

            mockTask1.Object.NotifyActionFinished();

            mockTask2.Verify(mt => mt.DoActionInNewThread(), Times.Once());
            Assert.IsTrue(viewModel.ThreadInProgress);
            CollectionAssert.DoesNotContain(viewModel.TaskList, mockTask1.Object);



            mockTask2.Object.NotifyActionFinished();

            mockTask3.Verify(mt => mt.DoActionInNewThread(), Times.Once());
            Assert.IsTrue(viewModel.ThreadInProgress);
            CollectionAssert.DoesNotContain(viewModel.TaskList, mockTask2.Object);
        }

        [TestMethod]
        public void StopAllActionIfTheLastTaskIsFinished()
        {

            Mock<Task> mockTask1 = new Mock<Task>();
            TaskManagerViewModel viewModel = new TaskManagerViewModel();

            TaskObserver.NotifyAddTask(mockTask1.Object);

            mockTask1.Object.NotifyActionFinished();

            Assert.AreEqual(0, viewModel.TaskList.Count); 
            Assert.IsFalse(viewModel.ThreadInProgress);
        }
    }
}
