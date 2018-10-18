using System;

namespace PutteMySQL.Core {
	public interface IDbDataReader {
		int FieldCount { get; set; }
		string GetName(int pos);
		Type GetFieldType(int pos);
		string GetDataTypeName(int pos);

		bool Read();
		string GetString(string colName);
	}
}
