/***
 * Note to self: Varje rad kommer att ha meta data för kolumnnen,
 * för att optimera storlek så hade man kanske kunnat tänka sig
 * att bara spara en rad med metadata för samtliga rader...
 *
 *
 * TODO:Kolla om vi kan slå ihop detta med FieldVal
 *
 */

using System;
using System.Collections.Generic;

namespace PutteMySQL.Core {
	public class DbRow {
		public IList<DbColumn> Columns;

		public DbRow() {
			this.Columns = new List<DbColumn>();
		}

		public void AddColumn(DbColumn column) {
			this.Columns.Add(column);
		}

		public void AddColumn(DbFieldType fieldType, DbFieldValue fieldValue) {
			var column = new DbColumn(fieldType, fieldValue);	
			AddColumn(column);
		}

		public int ColumnCount() {
			return Columns.Count;
		}

		public DbFieldValue GetFieldValue(string name) {
			DbFieldValue fieldVal = null;

			var column = GetColumn(name);
			if (column != null) {
				fieldVal = column.FieldValue;
			}

			return fieldVal;
		}

		public int GetInt(string name) {
			var fieldValue = GetFieldValue(name).Value;
			var result = int.TryParse(fieldValue, out var intRes) ? intRes : int.MinValue;

			return result;
		}

		public double GetDouble(string name) {
			var fieldValue = GetFieldValue(name).Value;
			var result = double.TryParse(fieldValue, out var intRes) ? intRes : double.MinValue;

			return result;
		}

		public long GetLong(string name) {
			var fieldValue = GetFieldValue(name).Value;
			var result = long.TryParse(fieldValue, out var intRes) ? intRes : long.MinValue;

			return result;
		}

		public string GetStr(string name) {
			var result = string.Empty;
			var fieldVal = GetFieldValue(name);

			if (fieldVal != null) {
				result = fieldVal.Value;
			}

			return result;
		}

		public DateTime GetDate(string name) {
			var fieldValue = GetFieldValue(name).Value;
			var result = DateTime.TryParse(fieldValue, out var dateRes) ? dateRes : DateTime.MinValue;

			return result;
		}

		public DbColumn GetColumn(string name) {
			DbColumn result = null;

			foreach (var column in Columns) {
				if (column.FieldType.Name.Equals(name)) {
					result = column;
					break;
				}
			}

			return result;
		}
	}
}
