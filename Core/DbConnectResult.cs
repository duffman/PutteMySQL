using MySql.Data.MySqlClient;

namespace PutteMySQL.Core {
	public class DbConnectResult : IDbConnectResult {
		private MySqlException dbException = null;

		public MySqlException Exception {
			get => dbException;
			set => SetException(value);
		}

		public bool Success { get; set; }

		public string ExceptionMessage() {
			return Exception != null ? Exception.Message : "";
		}

		public void SetException(MySqlException exception) {
			Success = exception != null;
			this.dbException = exception;
		}
	}
}
