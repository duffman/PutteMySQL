using System;

namespace PutteMySQL.Utils {
    public static class Log {
		public static void Error(string message) {
		}

	    public static void Exception(string message, Exception e) {
	    }

		public static void Exception(object caller, string message, Exception e) {
		}
	}
}
