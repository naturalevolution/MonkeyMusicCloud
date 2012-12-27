using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonkeyMusicCloud.Client.Exceptions;
using MonkeyMusicCloud.Test.Helper.EventCatchers;

namespace MonkeyMusicCloud.Test.Client.Exceptions
{
    [TestClass]
    public class PlayerViewModelShould
    {
        [TestMethod]
        public void RaiseNewCatchedExceptionEventAtInstantiate()
        {
            ExceptionEventCatcher catcher = new ExceptionEventCatcher();

            BaseException exception = new FakeBaseException();

            Assert.IsTrue(catcher.ExceptionEventInvoked);
            Assert.AreEqual(exception,  catcher.Exception);
        }

        private class FakeBaseException : BaseException{}
    }
}
