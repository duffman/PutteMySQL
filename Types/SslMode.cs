namespace PutteMySQL.Types {
	public enum SslMode {
		None       = 0, // Do not use SSL
		Preferred  = 1, // Use SSL, if server supports it
		Required   = 2, // Always use SSL. Deny connection if server does not support SSL. Do not perform server certificate validation. This is the default SSL mode when not specified as part of the connection string
		VerifyCA   = 3, // Always use SSL. Validate server SSL certificate, but different host name mismatch
		VerifyFull = 4  // Always use SSL and perform full certificate validation
	}
}
