﻿using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonkeyMusicCloud.Client.ViewModels.SubViewModels;
using MonkeyMusicCloud.Test.Helper;

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
            Assert.AreEqual(TaskState.Finished, fakeTask.State);
        }
    }
    
    public class FakeTask : Task
    {
        protected override void DoAction()
        {
            doActionCalled = true;
        }
        public bool doActionCalled = false;
    }
}
