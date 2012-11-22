using System.Collections.ObjectModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonkeyMusicCloud.Client.Observers;
using MonkeyMusicCloud.Client.ViewModels;
using MonkeyMusicCloud.Client.ViewModels.SubViewModels;
using MonkeyMusicCloud.Client.Views.BodyViews;
using MonkeyMusicCloud.Domain.Model;
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

            Assert.AreEqual(0, viewModel.Items.Count );
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

            Assert.AreEqual(1, viewModel.Items.Count);
            Assert.AreEqual(item, viewModel.Items[0]);
            Assert.AreEqual(item, viewModel.SelectedItem);
        }

        [TestMethod]
        public void RemoveItemOnCloseDemand()
        {
            const string label = "label";
            Mock<IBodyView> view = new Mock<IBodyView>();
            MenuItem item = new MenuItem { Label = label, View = view.Object };
            ContentBodyViewModel viewModel = new ContentBodyViewModel {Items = new ObservableCollection<MenuItem> {item}};

            viewModel.CloseCommand.Execute(item);

            Assert.AreEqual(0, viewModel.Items.Count);
        }

        [TestMethod]
        public void OpenNewSongListViewWhenNewSearchEventIsCatched()
        {
            const string search = "label";
            ContentBodyViewModel viewModel = new ContentBodyViewModel();

            ContentBodyObserver.NotifyNewSeach(new ObservableCollection<Song>(), search);

            Assert.AreEqual(1, viewModel.Items.Count);
            Assert.AreEqual("Search : " + search, viewModel.Items[0].Label);
            Assert.AreEqual(viewModel.Items[0], viewModel.SelectedItem);
        }
    }
}
