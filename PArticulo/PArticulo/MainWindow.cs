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
		Console.WriteLine ("MainWindow ctor.");
		QueryResult queryResult = PersisterHelper.Get ("select * from articulo");
		TreeViewHelper.Fill (treeView, queryResult);

		newAction.Activated += delegate {
			new ArticuloView();
		};

		deleteAction.Activated += delegate {
			object id = GetId(treeView);
			Console.WriteLine ("click en deleteAction id={0}", id);

		};

		treeView.Selection.Changed += delegate {
			Console.WriteLine("Ha ocurrido treeview.selection.changed");
			deleteAction.Sensitive = GetId(treeView) !=null;
		};
		//{
		//newAction.Activated += newActionActivated;
	}

	public object GetId(TreeView treeview){
		TreeIter treeIter;
		if (!treeView.Selection.GetSelected (out treeIter))
			return null;
		IList row = (IList)treeView.Model.GetValue (treeIter, 0);
		return row [0];
	}

//	void newActionActivated (object sender, EventArgs e)
//	{
//		new ArticuloView ();
//	}

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
