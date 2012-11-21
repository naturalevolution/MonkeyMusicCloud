using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonkeyMusicCloud.Client.Observers;
using MonkeyMusicCloud.Client.ViewModels;
using MonkeyMusicCloud.Client.ViewModels.SubViewModels;
using MonkeyMusicCloud.Client.Views.BodyViews;
using Moq;

namespace MonkeyMusicCloud.Test.Client.ViewModels
{
    [TestClass]
    public class ContentBodyViewModelShould
    {
        [TestMethod]
        public void CorrectlyInitialize()
        {
            ContentBodyViewModel viewModel = new ContentBodyViewModel();

            Assert.AreEqual(2, viewModel.Views.Count );
            Assert.IsNull(viewModel.SelectedItem);
        }


        [TestMethod]
        public void RefreshItemsWhenAnChangeContentViewEventIsCatched()
        {
            const string label = "label";
            Mock<IBodyView> view = new Mock<IBodyView>();
            MenuItem item = new MenuItem {Label = label, View = view.Object};
            ContentBodyViewModel viewModel = new ContentBodyViewModel();

            ContentBodyObserver.NotifyChangeContentView(item);

            Assert.AreEqual(3, viewModel.Views.Count);
            Assert.AreEqual(item, viewModel.Views[2]);
            Assert.AreEqual(item, viewModel.SelectedItem);
        }
    }
}
