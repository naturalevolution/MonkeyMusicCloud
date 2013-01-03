using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonkeyMusicCloud.Client.ViewModels.SubViewModels.Tasks;

namespace MonkeyMusicCloud.Test.Client.ViewModels.SubViewModels
{
    [TestClass]
    public class TaskShould
    {
        [TestMethod]
        public void RunInANewThreadTheAction()
        {
            FakeTask fakeTask = new FakeTask();
            Assert.AreEqual(TaskState.Waiting, fakeTask.State);

            fakeTask.DoActionInNewThread();
            Assert.AreEqual(TaskState.Running, fakeTask.State);

            while (fakeTask.Worker.IsBusy){}
            Assert.IsTrue(fakeTask.doActionCalled);
        }



        private class FakeTask : Task
        {
            protected override void DoAction()
            {
                doActionCalled = true;
            }

            public override string StringDescription
            {
                get
                {
                    throw new NotImplementedException();
                }
            }

            public bool doActionCalled = false;
        }
    }
}
