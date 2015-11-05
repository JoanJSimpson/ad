using Gtk;
using System;
using System.Collections;
using SerpisAd;
using System.Data;

namespace PCategoria
{
	public partial class CategoriaView : Gtk.Window
	{
		public CategoriaView () : 
			base(Gtk.WindowType.Toplevel)
		{
			this.Build ();
	
			saveAction.Activated += delegate {
				save();
			};

		}

		private void save(){
			IDbCommand dbCommand = App.Instance.DbConnection.CreateCommand ();
			dbCommand.CommandText = "insert into categoria (categoria) " +
				"values (@categoria)";

			string categoria = entryNombre.Text;

			DbCommandHelper.AddParameter (dbCommand, "categoria", categoria);


			dbCommand.ExecuteNonQuery();
		}




	}
}
