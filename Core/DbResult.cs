using System;

namespace PutteMySQL.Core {
	public interface IDbResult {
		int AffectedRows { get; set; }
		long InsertId { get; set; }
		DbResultData ResultData { get; set; }
		bool Success { get; set; }
		Exception Error { get; set; }
		string GetErrorMessage();
	}

	public class DbResult : IDbResult {
		public int AffectedRows { get; set; }
		public long InsertId { get; set; }
		public DbResultData ResultData { get; set; }
		public bool Success { get; set; }
		public Exception Error { get; set; }

		public DbResult() {
			Success = true;
		}

		public string GetErrorMessage() {
			var result = "Exception is NULL";
			if (Error != null) result = Error.Message;
			return result;
		}
	}
}