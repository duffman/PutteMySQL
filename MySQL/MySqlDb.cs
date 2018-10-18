using System;
using System.Data;
using System.IO;
using System.Text;
using MySql.Data.MySqlClient;
using PutteMySQL.Core;
using PutteMySQL.Extensions;
using PutteMySQL.Types;
using PutteMySQL.Utils;

namespace PutteMySQL.MySQL {
	public class MySqlDb : IMysqlDb {
		private const int DB_DEFAULT_PORT = 3306;
		public const string CONNECTION_STRING = "SERVER={0};DATABASE={1};UID={2};PASSWORD={3}; SslMode={4}";
		public string ConnectionString = string.Empty;

		private MySqlConnection connection = null;
		private IDbSettings dbSettings;

		public string ServerUri = null;
		public string DatabaseName = null;
		public string User = null;
		public string Password = null;
		public int Port = DB_DEFAULT_PORT;
		public SslMode SslMode = SslMode.None;

		public MySqlDb(string connectionString) {
			Connect(connectionString);
		}

		public MySqlDb(IDbSettings settings) {
			if (settings == null) {
				Log.Exception("DB Settings is Null", new Exception("DB Error"));
			}

			Initialize(settings);
		}

		public MySqlDb(string dbServer, string dbName, string dbUser,
			string dbPassword, SslMode sslMode, int dbPort = DB_DEFAULT_PORT
			) {
			var settings = new DbSettings(
				dbServer, dbName, dbUser, dbPassword, sslMode, dbPort
			);

			Initialize(settings);
		}

		public MySqlDb() {}

		public void Initialize(IDbSettings settings) {
			try {
				ValidateConectionSettings(settings);
				
			} catch (Exception ex) {
				Log.Exception("Cannot Init Database", ex);
				return;
			}
			
			this.dbSettings = settings;

			ServerUri = settings.DbServer;
			DatabaseName = settings.DbName;
			User = settings.DbUser;
			Password = settings.DbPassword;
			Port = settings.DbPort;

			CreateConnection();
		}
		
		private void CreateConnection() {
			ConnectionString = string.Format(
				CONNECTION_STRING,
				ServerUri,
				DatabaseName,
				User,
				Password,
				SslMode.StringValue()
			);

			Connect(ConnectionString);
		}

		private void Connect(string connectionString) {
			this.connection = new MySqlConnection(ConnectionString);
		}

		private void ValidateConectionSettings(IDbSettings settings) {
			var errorMessage = "{0} is missing";
			string missingSetting = null;

			if (string.IsNullOrEmpty(settings.DbServer))
				missingSetting = "ServerUri";

			if (string.IsNullOrEmpty(settings.DbName))
				missingSetting = "DatabaseName";

			if (string.IsNullOrEmpty(settings.DbUser))
				missingSetting = "User";

			if (string.IsNullOrEmpty(settings.DbPassword))
				missingSetting = "Password";

			if (string.IsNullOrEmpty(missingSetting)) return;
			errorMessage = string.Format(errorMessage, missingSetting);
			throw new Exception(errorMessage);
		}

		public virtual IDbConnectResult OpenConnection() {
			var result = new DbConnectResult();

			try {
				if (this.connection == null) {
					CreateConnection();
				}
				if (connection.State != ConnectionState.Open) {
					connection.Open();
				}

				if (connection.State == ConnectionState.Open) {
					result.Success = true;
				}

			} catch (MySqlException exception) {
				result.SetException(exception);
				result.Success = false;
			}

			return result;
		}

		public MySqlConnection GetConnection() {
			return connection;
		}

		public virtual bool StartTransaction(bool disableAutoCommit = true) {
			var result = true;
			var command = new StringBuilder();

			if (disableAutoCommit) {
				result = SetAutoCommit(false);
			}

			command.Append("START TRANSACTION;");

			try {
				result = result && Execute(command.ToString()).Success;
			}
			catch (Exception ex) {
				result = false;
			}

			return result;
		}

		/// <summary>
		/// Set Mysql Auto Commit Mode
		/// </summary>
		/// <param name="enabled"></param>
		/// <returns></returns>
		public bool SetAutoCommit(bool enabled) {
			var command = new StringBuilder("SET AUTOCOMMIT=")
				.Append(Convert.ToString(enabled)).Append(";");

			try {
				var result = Execute(command.ToString());
				return result.Success;
			}
			catch (Exception ex) {
				return false;
			}
		}

		public virtual bool FinishTransaction(bool commit, bool restoreAutoCommit = true) {
			var command = commit ? "COMMIT;" : "ROLLBACK;";
			try {
				var result = Execute(command.ToString());

				// Fire and forget since this is not required for the overall result
				if (restoreAutoCommit) SetAutoCommit(true); 

				return result.Success;
			}
			catch (Exception ex) {
				return false;
			}
		}

		public virtual IDbResult Execute(string sql, bool keepConnection = true) {
			var result = new DbResult();

			var connectResult = OpenConnection();

			try {
				if (connectResult.Success) {
					var cmd = new MySqlCommand(sql, connection);
					result.AffectedRows = cmd.ExecuteNonQuery();
					result.InsertId = cmd.LastInsertedId;

					if (!keepConnection)
						CloseConnection();
				}
			} catch (Exception e) {
				result.Error = e;
				result.Success = false;
				Log.Error(e.Message);
			}

			return result;
		}

		public bool CloseConnection() {
			try {
				connection.Close();
				return true;
			} catch (MySqlException ex) {
				Log.Error("CloseConnection: " + ex.Message);
				return false;
			}
		}

		public virtual IDbResultData Query(string query) {
			var dbResultData = new DbResultData();

			var connectResult = OpenConnection();
			if (!connectResult.Success) {
				throw connectResult.Exception;
			}

			try {
				var cmd = new MySqlCommand(query, connection);
				var dataReader = cmd.ExecuteReader();

				dbResultData.ReadData(dataReader);
				dataReader.Close();
			}
			catch (Exception exception) {
				Log.Exception("RunQuery", exception);
			}

			return dbResultData;
		}
	}
}
