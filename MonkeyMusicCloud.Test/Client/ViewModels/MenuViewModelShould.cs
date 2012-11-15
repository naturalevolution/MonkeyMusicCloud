using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonkeyMusicCloud.Client.ViewModels;

namespace MonkeyMusicCloud.Test.Client.ViewModels
{
    [TestClass]
    public class MenuViewModelShould
    {
       
        [TestMethod]
        public void InstantiateBodyViews()
        {
            MenuViewModel viewModel = new MenuViewModel();

            Assert.AreEqual(2, viewModel.BodyViews.Count);
        }
    }
}
