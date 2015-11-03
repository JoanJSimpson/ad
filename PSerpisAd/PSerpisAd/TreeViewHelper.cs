using System;
using System.Collections;
using Gtk;

namespace SerpisAd
{
	public class TreeViewHelper
	{
		public static void Fill (TreeView treeView, QueryResult queryResult)
		{
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
			ListStore listStore = new ListStore (typeof(IList));
			foreach (IList row in queryResult.Rows) {
				listStore.AppendValues (row);

			}
			treeView.Model = listStore;

		}
	}
}

