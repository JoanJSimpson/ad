using System;
using Gtk;
using MySql.Data.MySqlClient;

namespace SerpisAd
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			App.Instance.DbConnection =new MySqlConnection (
					"Database=dbprueba;Data Source=localhost;User Id=root;Password=sistemas");
			App.Instance.DbConnection.Open ();

			Application.Init ();
			MainWindow win = new MainWindow ();
			win.Show ();
			Application.Run ();

			App.Instance.DbConnection.Close ();
		}
	}
}
