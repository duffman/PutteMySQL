using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace PutteMySQL.Core {
	public class DbTableFields {
		public IList<DbFieldType> Fields;

		public DbTableFields(MySqlDataReader dataReader) {
			Fields = new List<DbFieldType>();
			if (dataReader != null) {
				SetFields(dataReader);
			}	
		}

		public void AddField(DbFieldType field) {
			Fields.Add(field);
		}

		public void AddField(string name, Type type, string typeName) {
			var dbField = new DbFieldType {
				Name = name,
				Type = type,
				TypeName = typeName
			};

			AddField(dbField);
		}

		public void SetFields(MySqlDataReader dataReader) {
			for (var col = 0; col < dataReader.FieldCount; col++) {
				AddField(
					dataReader.GetName(col).ToString(),
					dataReader.GetFieldType(col),
					dataReader.GetDataTypeName(col)
				);
			}
		}
	}
}
