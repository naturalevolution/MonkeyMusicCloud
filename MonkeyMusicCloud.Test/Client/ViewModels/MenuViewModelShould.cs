using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonkeyMusicCloud.Client.ViewModels;
using MonkeyMusicCloud.Client.ViewModels.SubViewModels;
using MonkeyMusicCloud.Test.Helper;

namespace MonkeyMusicCloud.Test.Client.ViewModels
{
    [TestClass]
    public class MenuViewModelShould
    {
        [TestMethod]
        public void CorrectlyInitialize()
        {
            MenuViewModel viewModel = new MenuViewModel();

            Assert.AreEqual(1, viewModel.Items.Count );
        }

        [TestMethod]
        public void RaiseChangeContentViewEventWhenOpenMenuItemDemand()
        {
            MenuViewModel viewModel = new MenuViewModel();
            EventCatcher catcher = new EventCatcher();
            MenuItem item = new MenuItem();

            viewModel.OpenItemCommand.Execute(item);
            
            Assert.IsTrue(catcher.ChangeContentViewInvoked);
            Assert.AreEqual(catcher.MenuItem, item);
        }
    }
}
