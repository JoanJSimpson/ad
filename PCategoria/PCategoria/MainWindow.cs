using System;
using Gtk;
using PCategoria;
using SerpisAd;

public partial class MainWindow: Gtk.Window
{	
	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();
		QueryResult queryResult = PersisterHelper.Get ("select * from categoria");
		TreeViewHelper.Fill (treeView, queryResult);

		newAction.Activated += delegate {
			new CategoriaView();
		};


	}

	//boton actualizar
	protected void OnRefreshActionActivated (object sender, EventArgs e)
	{
		QueryResult queryResult = PersisterHelper.Get ("select * from categoria");

		TreeViewHelper.Fill (treeView, queryResult);
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}

}
