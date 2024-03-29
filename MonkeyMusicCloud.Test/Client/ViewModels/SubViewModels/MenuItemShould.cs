﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonkeyMusicCloud.Client.ViewModels.SubViewModels;
using MonkeyMusicCloud.Client.Views.BodyViews;
using Moq;

namespace MonkeyMusicCloud.Test.Client.ViewModels.SubViewModels
{
    [TestClass]
    public class MenuItemShould
    {
        [TestMethod]
        public void InstantiateCorrectly()
        {
            Mock<IBodyView> view = new Mock<IBodyView>();
            const string label = "label";
            const string imagePath = "imagePath";

            MenuItem item = new MenuItem
                                {
                                    Label = label,
                                    View = view.Object,
                                    ImagePath = imagePath
                                };

            Assert.AreEqual(label, item.Label);
            Assert.AreEqual(view.Object, item.View);
            Assert.AreEqual(imagePath, item.ImagePath);
        }
    }
}
