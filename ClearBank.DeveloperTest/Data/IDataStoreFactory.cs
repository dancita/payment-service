using System.Configuration;

namespace ClearBank.DeveloperTest.Data
{
    public interface IDataStoreFactory
    {
        public const string backupDataStoreType = "Backup";

        public IDataStore CreateDataStore()
        {
            var dataStoreType = ConfigurationManager.AppSettings["DataStoreType"];

            return dataStoreType == backupDataStoreType ? new BackupAccountDataStore() : new AccountDataStore();
        }
    }
}
