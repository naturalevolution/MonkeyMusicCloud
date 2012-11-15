using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonkeyMusicCloud.Client.Service.Proxy;
using MonkeyMusicCloud.Client.Utilities;
using MonkeyMusicCloud.Utilities.Interface;
using Moq;

namespace MonkeyMusicCloud.Test.Client.ViewModels
{
    [TestClass]
    public class ViewModelsBaseTest
    {
        protected Mock<IMusicService> Service { get; set; }
        protected Mock<IStreamHelper> StreamHelper { get; set; }
        protected Mock<IMusicPlayer> MusicPlayer { get; set; }

        [TestInitialize]
        public void Initialize()
        {
            Service = new Mock<IMusicService>();
            new ServiceInstance(Service.Object);

            StreamHelper = new Mock<IStreamHelper>();
            new StreamInstance(StreamHelper.Object);

            MusicPlayer = new Mock<IMusicPlayer>();
            new MusicPlayerInstance(MusicPlayer.Object);
        }
    }
}
