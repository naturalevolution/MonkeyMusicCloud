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

            Assert.AreEqual(2, viewModel.Items.Count );
            
        }

        [TestMethod]
        public void RaiseChangeContentViewEventWhenOpenMenuItemDemand()
        {
            MenuViewModel viewModel = new MenuViewModel();
            ContentBodyEventCatcher catcher = new ContentBodyEventCatcher();
            MenuItem item = new MenuItem();

            viewModel.OpenItemCommand.Execute(item);
            
            Assert.IsTrue(catcher.ChangeContentViewInvoked);
            Assert.AreEqual(catcher.MenuItem, item);
        }
    }
}
