using System;
using Gtk;
using PCategoria;
using SerpisAd;

public partial class MainWindow: Gtk.Window
{	
	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();
		QueryResult queryResult = PersisterHelper.Get ("select * from categoria order by id");
		TreeViewHelper.Fill (treeView, queryResult);

		newAction.Activated += delegate {
			new CategoriaView();
		};
		//boton borrar
		borrarCategoria.Activated += delegate {
			new BorraView ();
		};


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

