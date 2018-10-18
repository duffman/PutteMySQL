using PutteMySQL.Types;

namespace PutteMySQL.Extensions {
	public static class TypeExtensions {
		public static string StringValue(this SslMode mode) {
			string strValue;

			switch (mode) {
				case SslMode.Preferred:
					strValue = "1";
					break;
				case SslMode.Required:
					strValue = "2";
					break;
				case SslMode.VerifyCA:
					strValue = "3";
					break;
				case SslMode.VerifyFull:
					strValue = "4";
					break;
				default:
					strValue = "0";
					break;
			}

			return strValue;
		}
	}
}
