using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonkeyMusicCloud.Client.Exceptions;
using MonkeyMusicCloud.Test.Helper.EventCatchers;

namespace MonkeyMusicCloud.Test.Client.Exceptions
{
    [TestClass]
    public class PlayExceptionModelShould
    {
        [TestMethod]
        public void RaiseCurrentSongFinishedAtIntantiate()
        {
            PlayerEventCatcher catcher = new PlayerEventCatcher();

            new PlayException();

            Assert.IsTrue(catcher.SongFinishedInvoked);
        }
    }
}
