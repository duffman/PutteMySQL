namespace PutteMySQL.Core {
	public class DbColumn {
		public DbFieldType FieldType;
		public DbFieldValue FieldValue;

		public DbColumn(DbFieldType fieldType, DbFieldValue fieldValue) {
			this.FieldType = fieldType;
			this.FieldValue = fieldValue;
		}
	}
}
