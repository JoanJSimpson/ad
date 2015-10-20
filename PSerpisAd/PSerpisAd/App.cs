using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace SerpisAd
{
	public class App
	{
		private static App instance = new App();
		public static App Instance {
			get { return instance;}
		}

		private App() {
		}

		private IDbConnection dbConnection;
		public IDbConnection DbConnection {
			get { return dbConnection;}
			set { dbConnection = value;}
		}

	}
}

