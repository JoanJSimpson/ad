using System;
using System.Data;
using System.Reflection;
using System.Collections.Generic;

namespace SerpisAd
{
	public class Persister
	{
		private const string INSERT_SQL = "insert into {0} ({1}) values ({2})";

		public static int Insert(object obj) {
			IDbCommand dbCommand = App.Instance.DbConnection.CreateCommand ();
			dbCommand.CommandText = getInsertSql (obj.GetType());
			addParameters (dbCommand, obj);
			return dbCommand.ExecuteNonQuery();
		}

		private static void addParameters(IDbCommand dbCommand, object obj) {
			Type type = obj.GetType ();
			PropertyInfo[] propertyInfos = type.GetProperties ();
			foreach (PropertyInfo propertyInfo in propertyInfos)
				if (!propertyInfo.Name.Equals ("Id")) {
				string name = propertyInfo.Name.ToLower ();
				object value = propertyInfo.GetValue (obj, null);
				DbCommandHelper.AddParameter (dbCommand, name, value);
			}
		}

		private static string getInsertSql(Type type) {
			string tableName = type.Name.ToLower ();
			string[] fieldNames = getFieldNames (type);
			string[] paramNames = getParamNames (fieldNames);
			return string.Format (INSERT_SQL, tableName, string.Join (",", fieldNames), string.Join (",", paramNames)); 
		}

		private static string[] getFieldNames(Type type) {
			List<string> fieldNames = new List<string> ();
			PropertyInfo[] propertyInfos = type.GetProperties ();
			foreach (PropertyInfo propertyInfo in propertyInfos)
				if (!propertyInfo.Name.Equals ("Id"))
					fieldNames.Add (propertyInfo.Name.ToLower());
			return fieldNames.ToArray ();
		}

		private static string[] getParamNames(string[] fieldNames) {
			List<string> paramNames = new List<string> ();
			foreach (string fieldName in fieldNames)
				paramNames.Add("@" + fieldName);
			return paramNames.ToArray ();
		}
	}
}
