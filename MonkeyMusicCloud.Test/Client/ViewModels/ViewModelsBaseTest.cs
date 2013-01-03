using System;
using System.Windows;
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
        protected Mock<IMusicService> MockService { get; set; }
        protected Mock<IImageSearch> MockImageSearch { get; set; }
        protected Mock<IStreamHelper> MockStreamHelper { get; set; }
        protected Mock<IMusicPlayer> MockMusicPlayer { get; set; }

        [TestInitialize]
        public virtual void Initialize()
        {
            MockService = new Mock<IMusicService>();
            new ServiceInstance(MockService.Object);

            MockStreamHelper = new Mock<IStreamHelper>();
            new StreamInstance(MockStreamHelper.Object);

            MockMusicPlayer = new Mock<IMusicPlayer>();
            new MusicPlayerInstance(MockMusicPlayer.Object);

            MockImageSearch = new Mock<IImageSearch>();
            new ImageSearchInstance(MockImageSearch.Object);

            LoadResourcesFiles();
        }

        private void LoadResourcesFiles()
        {
            if (Application.Current == null)
            {
                new Application();

                Application.Current.Resources.MergedDictionaries.Add(
                    Application.LoadComponent(
                        new Uri("MonkeyMusicCloud.Client;component/Themes/Theme.xaml",
                        UriKind.Relative)) as ResourceDictionary);
                Application.Current.Resources.MergedDictionaries.Add(
                    Application.LoadComponent(
                        new Uri("MonkeyMusicCloud.Client;component/Themes/Styles.xaml",
                        UriKind.Relative)) as ResourceDictionary);
                Application.Current.Resources.MergedDictionaries.Add(
                    Application.LoadComponent(
                        new Uri("MonkeyMusicCloud.Client;component/Themes/Templates.xaml",
                        UriKind.Relative)) as ResourceDictionary);
                
            }
        }
    }
}