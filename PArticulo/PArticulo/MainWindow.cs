using System;
using Gtk;

using SerpisAd;
using PArticulo;
using System.Collections;
using System.Data;

public partial class MainWindow: Gtk.Window
{	
	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();
		Title = "Artículo";
		QueryResult queryResult = PersisterHelper.Get ("select * from articulo");
		TreeViewHelper.Fill (treeView, queryResult);

		
		deleteAction.Sensitive = false;
		editAction.Sensitive = false;

		newAction.Activated += delegate {
			new ArticuloView();
		};

		editAction.Activated += delegate {
			object id = TreeViewHelper.GetId(treeView);
			new ArticuloView(id).Show();
			fillTreeView();
		};

		deleteAction.Activated += delegate {
			object id = TreeViewHelper.GetId(treeView);
			//Console.WriteLine ("click en deleteAction id={0}", id);
			delete(id);
			fillTreeView();
		};

		treeView.Selection.Changed += delegate {
			//Console.WriteLine("Ha ocurrido treeview.Selection.Changed");
			bool isSelected = TreeViewHelper.IsSelected(treeView);
			deleteAction.Sensitive = isSelected;
			editAction.Sensitive = isSelected;
		};

	}



	private void delete(object id){
		if (WindowsHelper.ConfirmDelete (this)) {
			IDbCommand dbCommand = App.Instance.DbConnection.CreateCommand ();
			dbCommand.CommandText = "delete from articulo where id = " +
				id;
			dbCommand.ExecuteNonQuery ();
		}
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}

	//boton actualizar
	protected void OnRefreshActionActivated (object sender, EventArgs e)
	{
		fillTreeView ();

	}
	private void fillTreeView(){
		QueryResult queryResult = PersisterHelper.Get ("select * from articulo");
		TreeViewHelper.Fill (treeView, queryResult);
	}
}
