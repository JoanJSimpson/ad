using System;
using Gtk;
using PCategoria;
using SerpisAd;
using System.Data;

public partial class MainWindow: Gtk.Window
{	
	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();
		Title = "Categoría";
		QueryResult queryResult = PersisterHelper.Get ("select * from categoria order by id");
		TreeViewHelper.Fill (treeView, queryResult);

		borrarCategoria.Sensitive = false;
		editAction.Sensitive = false;

		newAction.Activated += delegate {
			new CategoriaView();
		};
		//boton borrar
		borrarCategoria.Activated += delegate {
			object id = TreeViewHelper.GetId(treeView);
			//Console.WriteLine ("click en deleteAction id={0}", id);
			delete(id);
			fillTreeView();

		};
		editAction.Activated += delegate {
			object id = TreeViewHelper.GetId(treeView);
			new CategoriaView(id).Show();
		};

		treeView.Selection.Changed += delegate {
			bool isSelected = TreeViewHelper.IsSelected(treeView);
			borrarCategoria.Sensitive = isSelected;
			editAction.Sensitive = isSelected;
		};
	}

	private void delete(object id){
		if (WindowsHelper.ConfirmDelete (this)) {
			IDbCommand dbCommand = App.Instance.DbConnection.CreateCommand ();
			dbCommand.CommandText = "delete from categoria where id = " +
				id;
			dbCommand.ExecuteNonQuery ();
		}
	}

	//boton actualizar
	protected void OnRefreshActionActivated (object sender, EventArgs e)
	{
		fillTreeView ();
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}
	private void fillTreeView(){
		QueryResult queryResult = PersisterHelper.Get ("select * from categoria order by id");
		TreeViewHelper.Fill (treeView, queryResult);
	}

}

