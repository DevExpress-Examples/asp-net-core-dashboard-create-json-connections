using DevExpress.DashboardWeb;
using DevExpress.DataAccess.ConnectionParameters;
using DevExpress.DataAccess.Web;

namespace SaveJsonConnectionTutorial {
    public class ConnectionStringProvider : IDataSourceWizardConnectionStringsStorage {
        readonly Dictionary<string, DataConnectionParametersBase> storage = new Dictionary<string, DataConnectionParametersBase>();
        public Dictionary<string, string> GetConnectionDescriptions() {
            return storage.ToDictionary(p => p.Key, p => p.Key);
        }
        public DataConnectionParametersBase GetDataConnectionParameters(string name) {
            return storage[name];
        }
        public void SaveDataConnectionParameters(string name, DataConnectionParametersBase connectionParameters, bool saveCredentials) {
            storage[name] = connectionParameters;
        }
    }

    // Allows you to save and use connections created at runtime in addition to predefined connection strings.
    public class ConnectionStringsProviderEx : IDataSourceWizardConnectionStringsStorage {
        readonly IDataSourceWizardConnectionStringsProvider provider;
        static readonly Dictionary<string, DataConnectionParametersBase> storage = new Dictionary<string, DataConnectionParametersBase>();

        public ConnectionStringsProviderEx(IDataSourceWizardConnectionStringsProvider provider) {
            this.provider = provider;
        }

        public Dictionary<string, string> GetConnectionDescriptions() {
            var result = provider.GetConnectionDescriptions();
            foreach (var pair in storage) {
                if (!result.ContainsKey(pair.Key))
                    result[pair.Key] = pair.Key;
            }
            return result;
        }

        public DataConnectionParametersBase GetDataConnectionParameters(string name) {
            if (provider.GetConnectionDescriptions().ContainsKey(name))
                return provider.GetDataConnectionParameters(name);
            DataConnectionParametersBase fromStorage;
            _ = storage.TryGetValue(name, out fromStorage);
            return fromStorage;
        }

        public void SaveDataConnectionParameters(string name, DataConnectionParametersBase connectionParameters, bool saveCredentials) {
            storage[name] = connectionParameters;
        }
    }
}