using System;
using Gtk;

using SerpisAd;
using PArticulo;

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
		//newAction.Activated += newActionActivated;
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

}
