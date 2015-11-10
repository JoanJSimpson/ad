using Gtk;
using System;
using System.Collections;
using SerpisAd;
using System.Data;

namespace PCategoria
{
	public partial class BorraView : Gtk.Window
	{
		public BorraView () : 
				base(Gtk.WindowType.Toplevel)
		{
			this.Build ();

			btGuardar.Activated += delegate {
			
				borrarCategoria ();
			};
		}

		private void borrarCategoria(){
			IDbCommand dbCommand = App.Instance.DbConnection.CreateCommand ();
			dbCommand.CommandText = "delete from categoria " +
				"where values (@selec) = '(@total)'";
			string algo";

			string nombre = entryNombre.Text;
			int id = int.Parse(entryId.Text);

			DbCommandHelper.AddParameter (dbCommand, "nombre", nombre);
			DbCommandHelper.AddParameter (dbCommand, "id", id);

			dbCommand.ExecuteNonQuery();
		}
	}
}

