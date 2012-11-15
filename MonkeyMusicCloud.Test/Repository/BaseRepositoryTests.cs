using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonkeyMusicCloud.Repository;

namespace MonkeyMusicCloud.Test.Repository
{

    [TestClass]
    public class BaseRepositoryTests
    {
        //TODO trouver le moyen de créer des transactions
        [TestCleanup]
        public void CleanUp()
        {
            MongoManager.GetInstance().Database.Drop();
        }
    }
}