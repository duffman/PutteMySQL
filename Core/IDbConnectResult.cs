using MySql.Data.MySqlClient;

namespace PutteMySQL.Core {
	public interface IDbConnectResult {
		MySqlException Exception { get; set; }
		bool Success { get; set; }
		void SetException(MySqlException ex);
	}
}