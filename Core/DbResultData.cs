using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace PutteMySQL.Core {
	public interface IDbResultData {
		DbTableFields Fields { get; set; }
		IList<DbRow> Rows { get; set; }
		void ReadData(MySqlDataReader dataReader);
	}

	public class DbResultData : IDbResultData {
		public DbTableFields Fields { get; set; }
		public IList<DbRow> Rows { get; set; }

		public DbResultData(MySqlDataReader dataReader = null) {
			Rows = new List<DbRow>();

			if (dataReader != null && !dataReader.IsClosed) {
				ReadData(dataReader);
			}
		}

		public bool ReadData(MySqlDataReader dataReader) {
			Fields = new DbTableFields(dataReader);
			var result = true;

			if (dataReader.IsClosed) {
				throw new Exception("DataReader is Closed");
			}

			while (dataReader.Read()) {
				var row = new DbRow();

				try {
					for (var i = 0; i < Fields.Fields.Count; i++) {
						var field = Fields.Fields[i];

						var fieldData = dataReader.IsDBNull(i) ? "" : dataReader.GetString(field.Name);

						var column = new DbColumn(field, new DbFieldValue(fieldData));
						row.AddColumn(column);
					}
				}
				catch (Exception ex) {
					Console.WriteLine("DbResultData :: ReadData :: DataReader ::  " + ex.Message);
					result = false;
				}

				if (result) {
					Rows.Add(row);
				}
			}

			return result;
		}

		void IDbResultData.ReadData(MySqlDataReader dataReader) {
			throw new NotImplementedException();
		}
	}
}