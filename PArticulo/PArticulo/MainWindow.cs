using System;
using Gtk;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

public partial class MainWindow: Gtk.Window
{	
	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();

		Console.WriteLine ("MainWindow.ctor.");
		MySqlConnection mySqlConnection = new MySqlConnection (
			"Database=dbprueba;Data Source=localhost;User Id=root;Password=sistemas"
			);
		mySqlConnection.Open ();
		MySqlCommand mySqlCommand = mySqlConnection.CreateCommand ();
		mySqlCommand.CommandText = "select * from articulo ";
		MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader ();

		//ESTA ES LA FORMA EN QUE LO SOLUCIONA LUIS

/*		//añado columnas
		treeView.AppendColumn ("id", new CellRendererText (), "text", 0); 
		treeView.AppendColumn ("nombre", new CellRendererText (), "text", 1);

		ListStore listStore = new ListStore (typeof(String), typeof(String));
 * 		
 * 		while (mySqlDataReader.Read()) {
 * 			listStore.AppendValues (mySqlDataReader [0].ToString(), mySqlDataReader [1].ToString());
 * 		}
 * 
 * 		mySqlDataReader.Close ();
 * 
 * 		treeView.Model = listStore;
 * 
 * 		mySqlConnection.Close ();
 * 
 * 
 * */

		//ASI LO SOLUCIONÉ YO

		string[] columnNames = getColumnNames (mySqlDataReader);

//		while (mySqlDataReader.Read()) {
//			Console.WriteLine ("id={0} nombre={1}", mySqlDataReader [0], mySqlDataReader [1]);
//			
//		}
		//establezco el modelo
		Type[] type = new Type[columnNames.Length];

		//Sacamos los nombres de las columnas y los
		//Ponemos en el treeView
		for (int i=0; i<columnNames.Length; i++){
			type [i] = typeof(String);
			treeView.AppendColumn (columnNames[i].ToString(), new CellRendererText (), "text", i);
		}
//		treeView.AppendColumn ("id", new CellRendererText (), "text", 0); 
//		treeView.AppendColumn ("nombre", new CellRendererText (), "text", 1);

		ListStore listStore = new ListStore (type);
		//ListStore listStore = new ListStore (typeof(String), typeof(String), typeof(String), typeof(String));

		//ToDO rellenar listStore
//		while (mySqlDataReader.Read()) {
//			listStore.AppendValues (mySqlDataReader [0].ToString(), mySqlDataReader [1]);
//			treeView.Model = listStore;
//		}

//		int count = mySqlDataReader.FieldCount;
//		string line = "";
//		for (int index = 0; index < count; index++)
//			line = line + mySqlDataReader [index] + " ";

//		Console.WriteLine (line);

		//ponemos los valores de la base de datos en sus columnas
		while (mySqlDataReader.Read()) {
			int count = mySqlDataReader.FieldCount;

			string[] values = new string[count];
			for (int i=0;i<values.Length;i++){
				//values [0] = mySqlDataReader [0].ToString ();
				//values [1] = mySqlDataReader [1].ToString ();
				values [i] = mySqlDataReader [i].ToString ();
			}
			listStore.AppendValues (values);
			treeView.Model = listStore;
		}



		mySqlDataReader.Close ();
		mySqlConnection.Close ();
	}
//	private static void showColumnNames(MySqlDataReader mySqlDataReader) {
//		//Console.WriteLine ("showColumnNames...");
//		string[] columnNames = getColumnNames (mySqlDataReader);
//		//Console.WriteLine (string.Join (", ", columnNames));
	//}

	//metodo getColumnNames para sacar nombre de columnas
	public static string[] getColumnNames(MySqlDataReader mySqlDataReader){
		int count = mySqlDataReader.FieldCount;
		List<string> columnNames = new List<string> ();
		for (int index = 0; index < count; index++) 
			columnNames.Add (mySqlDataReader.GetName (index));
		return columnNames.ToArray();
	}


	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}
}
