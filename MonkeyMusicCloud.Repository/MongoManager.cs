#region Usings

using System.Configuration;
using MongoDB.Driver;

#endregion

namespace MonkeyMusicCloud.Repository
{
    public class MongoManager
    {
        private static MongoManager instance;
        private static readonly object InstanceLock = new object();

        public MongoManager()
        {
            MongoServer server = MongoServer.Create();
            server.Connect();
            Database = server.GetDatabase(ConfigurationManager.AppSettings["DataBaseName"]);
        }

        public MongoDatabase Database { get; set; }

        public static MongoManager GetInstance()
        {
            lock (InstanceLock)
            {
                return instance ?? (instance = new MongoManager());
            }
        }
    }
}