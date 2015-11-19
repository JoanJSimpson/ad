using Gtk;
using System;
using System.Collections;
using SerpisAd;
using System.Data;

namespace PCategoria
{
	public delegate void SaveDelegate();
	public partial class CategoriaView : Gtk.Window
	{
		private SaveDelegate save;
		private object id = null;
		private string nombre = "";
		public CategoriaView () : base(Gtk.WindowType.Toplevel)
		{
			string titulo = "Nueva Categoría";
			init (titulo);
			save = insert;
		}

		public CategoriaView(object id) : base(Gtk.WindowType.Toplevel){
			string titulo = "Editar Categoría";
			this.id = id;
			load ();
			init (titulo);
			save = update;
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
			nombre = (string)dataReader ["nombre"];
			dataReader.Close ();

		}

		private void init(String titulo){
			this.Build();
			Title = titulo;
			entryNombre.Text = nombre;
			saveAction.Activated += delegate {
				save();
				Destroy();
			};
		}

		private void insert(){
			IDbCommand dbCommand = App.Instance.DbConnection.CreateCommand ();
			dbCommand.CommandText = "insert into categoria (nombre) " +
				"values (@nombre)";

			nombre = entryNombre.Text;

			DbCommandHelper.AddParameter (dbCommand, "nombre", nombre);

			dbCommand.ExecuteNonQuery();
		}

		private void update(){
			IDbCommand dbCommand = App.Instance.DbConnection.CreateCommand ();
			dbCommand.CommandText = "UPDATE categoria SET nombre = @nombre where id = @id";

			nombre = entryNombre.Text;

			DbCommandHelper.AddParameter (dbCommand, "nombre", nombre);
			DbCommandHelper.AddParameter (dbCommand, "id", id);

			dbCommand.ExecuteNonQuery();
			this.Destroy();

		}






	}
}

