using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonkeyMusicCloud.Client.Service.Proxy;
using MonkeyMusicCloud.Client.Utilities;
using MonkeyMusicCloud.Test.Helper;
using MonkeyMusicCloud.Utilities.Interface;
using Moq;

namespace MonkeyMusicCloud.Test.Client.ViewModels
{
    [TestClass]
    public class ViewModelsBaseTest
    {
        protected Mock<IMusicService> MockService { get; set; }
        protected Mock<IImageSearch> MockImageSearch { get; set; }
        protected Mock<IStreamHelper> MockStreamHelper { get; set; }
        protected Mock<IMusicPlayer> MockMusicPlayer { get; set; }

        [TestInitialize]
        public virtual void Initialize()
        {
            MockService = new Mock<IMusicService>();
            new ServiceInstance(MockService.Object);
            MockService.Setup(s => s.GetMediaFileById(It.IsAny<Guid>())).Returns(Create.MediaFile);

            MockStreamHelper = new Mock<IStreamHelper>();
            new StreamInstance(MockStreamHelper.Object);

            MockMusicPlayer = new Mock<IMusicPlayer>();
            new MusicPlayerInstance(MockMusicPlayer.Object);

            MockImageSearch = new Mock<IImageSearch>();
            new ImageSearchInstance(MockImageSearch.Object);
        }
    }
}
