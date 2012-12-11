using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonkeyMusicCloud.Client.ViewModels.SubViewModels;

namespace MonkeyMusicCloud.Test.Client.ViewModels.SubViewModels
{
    [TestClass]
    public class TaskShould
    {
        [TestMethod]
        public void RunInANewThreadTheAction()
        {
            FakeTask fakeTask = new FakeTask();

            fakeTask.DoActionInNewThread();

            while (fakeTask.Thread.IsAlive){}
            Assert.IsTrue(fakeTask.doActionCalled);
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
