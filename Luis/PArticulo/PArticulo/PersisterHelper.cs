using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

namespace PArticulo
{
	public class PersisterHelper
	{
		public static QueryResult Get(string selectText) {
		
			IDbConnection dbConnection = App.Instance.DbConnection;
			IDbCommand dbCommand = dbConnection.CreateCommand ();
			dbCommand.CommandText = selectText;
			//ToDO completar
			IDataReader dataReader = dbCommand.ExecuteReader ();
			QueryResult queryResult = new QueryResult ();			
			queryResult.ColumnNames = getColumnNames (dataReader);
			List<IList> rows = new List<IList> ();

			while (dataReader.Read()) {
				//listStore.AppendValues (dataReader [0].ToString(), dataReader [1].ToString());
				rows.Add (getRows.dataReader());
			}
			queryResult.Rows=rows;

			dataReader.Close ();
			dbConnection.Close ();
			return queryResult;

		
		}

		private static string[] getColumnNames(IDataReader dataReader) {
			List<string> columnNames = new List<string> ();
			int count = dataReader.FieldCount;
			for (int index = 0; index < count; index++)
				columnNames.Add (dataReader.GetName (index));
			return columnNames.ToArray ();
		}

		private static IList getRows(IDataReader dataReader) {
			List<object> values = new List<object> ();
			int count = dataReader.FieldCount;
			for (int index = 0; index < count; index++)
				values.Add (dataReader [index]);
			return values;
		}


	}
}

