using System;
using Gtk;
using SerpisAd;

public partial class MainWindow: Gtk.Window
{	
	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();

		Console.WriteLine ("MainWindow ctor.");

		QueryResult queryResult = PersisterHelper.Get ("select * from articulo");

		TreeViewHelper.Fill (treeView, queryResult);

	}



//	private Type[] getTypes(int count) {
//		List<Type> types = new List<Type> ();
//		for (int index = 0; index < count; index++)
//			types.Add (typeof(string));
//		return types.ToArray ();
//	}
//

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}
}
