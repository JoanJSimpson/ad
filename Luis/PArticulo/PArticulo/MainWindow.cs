using System;
using System.Data;
using System.Collections.Generic;
using Gtk;

using PArticulo;
using System.Collections;

public partial class MainWindow: Gtk.Window
{	
	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();

		Console.WriteLine ("MainWindow ctor.");
		QueryResult queryResult = PersisterHelper.Get ("select * from articulo");

		string[] columnNames = queryResult.ColumnNames;
		CellRendererText cellRendererText = new CellRendererText();

		for (int index = 0; index < columnNames.Length; index++) {
			int column = index;
			treeView.AppendColumn (columnNames [index], cellRendererText, 
			    delegate(TreeViewColumn tree_column, CellRenderer cell, TreeModel tree_model, TreeIter iter) {
				IList row = (IList)tree_model.GetValue(iter, 0);
				//Si queremos que salga <vacio> en los campos que no haya nada pondremos: 
				//if (row[column] == DBNull.Value)
				//	cellRendererText.Text = "<vacio>";
				//else
					cellRendererText.Text = row[column].ToString();

			});
		}

		//ListStore listStore = new ListStore (typeof(String), typeof(String));
		//Type[] types = getTypes (dataReader.FieldCount);
		ListStore listStore = new ListStore (typeof(IList));
		foreach (IList row in queryResult.Rows) {
			listStore.AppendValues (row);

		}

		treeView.Model = listStore;

	}



	private Type[] getTypes(int count) {
		List<Type> types = new List<Type> ();
		for (int index = 0; index < count; index++)
			types.Add (typeof(string));
		return types.ToArray ();
	}


	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}
}
