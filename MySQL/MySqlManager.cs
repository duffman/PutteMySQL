using PutteMySQL.Core;
using PutteMySQL.Types;

namespace PutteMySQL.MySQL {
	public sealed class MySqlManager {
		private MySqlDb db;

		// Explicit static constructor to tell C# compiler
		// not to mark type as before field init
		static MySqlManager() { }

		private MySqlManager() {}

		public static MySqlManager Instance { get; } = new MySqlManager();

		private void Initialize(string dbServer, string dbName, string dbUser, string dbPassword, SslMode sslMode, string id) {
			if (this.db == null) {
				this.db = new MySqlDb(dbServer, dbName, dbUser, dbPassword, sslMode);
			}
		}

		public IDbResultData ExecuteQuery(string query) {
			return db.Query(query);
		}
	}
}
