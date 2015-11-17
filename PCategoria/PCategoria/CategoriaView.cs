using Gtk;
using System;
using System.Collections;
using SerpisAd;
using System.Data;

namespace PCategoria
{
	public partial class CategoriaView : Gtk.Window
	{
		object id;
		public CategoriaView () : base(Gtk.WindowType.Toplevel)
		{
			this.Build ();
			Title = "Nueva Categoría";
	
			saveAction.Activated += delegate {
				save();
				Destroy();
			};

		}
		//this() llama al constructor por defecto
		public CategoriaView(object id) : this() {
			Title = "Editar Categoría";
			this.id = id;
			load ();
		}

		private void load(){
			IDbCommand dbCommand = App.Instance.DbConnection.CreateCommand ();
			dbCommand.CommandText = "select * from categoria where id = @id";
			DbCommandHelper.AddParameter (dbCommand, "id", id);
			IDataReader dataReader = dbCommand.ExecuteReader ();
			if (!dataReader.Read ()) {
				//ToDO throw Exception
				return;
			}
			string nombre = (string)dataReader ["nombre"];
			dataReader.Close ();
			entryNombre.Text = nombre;

		}

		private void save(){
			IDbCommand dbCommand = App.Instance.DbConnection.CreateCommand ();
			dbCommand.CommandText = "insert into categoria (nombre) " +
				"values (@nombre)";

			string nombre = entryNombre.Text;

			DbCommandHelper.AddParameter (dbCommand, "nombre", nombre);

			dbCommand.ExecuteNonQuery();
		}






	}
}

