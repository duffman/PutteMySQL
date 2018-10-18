using System;

namespace PutteMySQL.Core {
	public class DbFieldValue {
		public string Value;

		public DbFieldValue(string value) {
			this.Value = value;
		}

		public int AsInt() {
			return Convert.ToInt32(Value);
		}

		public bool AsBool() {
			var strVal = Value.ToLower();
			strVal = strVal.Equals("true") ? "1" : strVal;

			return Convert.ToBoolean(Value);
		}

		public double AsDouble() {
			double result = 0;

			if (double.TryParse(Value, out var doubleVal)) {
				result = doubleVal;
			}

			return result;
		}
	}
}
