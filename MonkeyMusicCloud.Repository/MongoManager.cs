using System.Configuration;
using MongoDB.Driver;

namespace MonkeyMusicCloud.Repository
{
    public class MongoManager
    {
        private static MongoManager instance;
        public MongoDatabase Database { get; set; }
        static readonly object InstanceLock = new object();

        public MongoManager()
        {
            MongoServer server = MongoServer.Create();
            server.Connect();
            Database = server.GetDatabase(ConfigurationManager.AppSettings["DataBaseName"]);
        }

        public static MongoManager GetInstance()
        {
            lock (InstanceLock)
            {
                return instance ?? (instance = new MongoManager());
            }
        }
    }
}

