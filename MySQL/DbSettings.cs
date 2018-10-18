using PutteMySQL.Types;

namespace PutteMySQL.MySQL {
	public interface IDbSettings {
		string DbServer { get; set; }
		string DbName { get; set; }
		string DbUser { get; set; }
		string DbPassword { get; set; }
		SslMode DbSslMode { get; set; }
		int DbPort { get; set; }
	}

	public class DbSettings : IDbSettings {
	    public string DbServer { get; set; }
	    public string DbName { get; set; }
		public string DbUser { get; set; }
		public string DbPassword { get; set; }
		public SslMode DbSslMode { get; set; }
		public int DbPort { get; set; }

		public DbSettings(
				string dbServer, string dbName, string dbUser,
				string dbPassword, SslMode dbUseSsl, int dbPort = 3306)
		{
			DbServer   = dbServer;
			DbName     = dbName;
			DbUser     = dbUser;
			DbPassword = dbPassword;
			DbPort     = dbPort;
		}
	}
}
