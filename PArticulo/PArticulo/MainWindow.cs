using System;
using Gtk;

using SerpisAd;
using PArticulo;
using System.Collections;

public partial class MainWindow: Gtk.Window
{	
	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();
		Title = "Artículo";
		Console.WriteLine ("MainWindow ctor.");
		QueryResult queryResult = PersisterHelper.Get ("select * from articulo");
		TreeViewHelper.Fill (treeView, queryResult);

		newAction.Activated += delegate {
			new ArticuloView();
		};

		deleteAction.Activated += delegate {
			object id = TreeViewHelper.GetId(treeView);
			Console.WriteLine ("click en deleteAction id={0}", id);
			delete(id);

		};

		treeView.Selection.Changed += delegate {
			Console.WriteLine("Ha ocurrido treeview.Selection.Changed");
			deleteAction.Sensitive = TreeViewHelper.IsSelected(treeView);
		};
		//{
		//newAction.Activated += newActionActivated;
	}

	private void delete(object id){
		if (WindowsHelper.ConfirmDelete(this)) {
			Console.WriteLine ("Dice que eliminar si");
		} else {
			Console.WriteLine ("Dice que eliminar no");
		}

	}



//	void newActionActivated (object sender, EventArgs e)
//	{
//		new ArticuloView ();
//	}
	//Metodo para ver si hay seleccionados


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
