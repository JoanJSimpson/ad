using Gtk;
using System;
using System.Collections;
using SerpisAd;
using System.Data;

namespace PArticulo
{
	public delegate void SaveDelegate();
	public partial class ArticuloView : Gtk.Window
	{
		private object id = null;
		private object categoria = null;
		private string nombre = "";
		private decimal precio = 0;
		private SaveDelegate save;

		public ArticuloView () : base(Gtk.WindowType.Toplevel)
		{

			string titulo = "Artículo Nuevo";
			init (titulo);
			save = insert;
		}

		public ArticuloView(object id): base(Gtk.WindowType.Toplevel){
			string titulo = "Editar Artículo";
			this.id = id;
			load ();
			init (titulo);
			save = update;
		}

		private void load(){
			IDbCommand dbCommand = App.Instance.DbConnection.CreateCommand ();
			dbCommand.CommandText = "select * from articulo where id = @id";
			DbCommandHelper.AddParameter (dbCommand, "id", id);
			IDataReader dataReader = dbCommand.ExecuteReader ();
			if (!dataReader.Read ()) {
				//ToDO throw Exception
				Console.WriteLine("dataReader no puede leer");
				return;
			}
			nombre = (string)dataReader ["nombre"];
			categoria = dataReader ["categoria"];
			if (categoria is DBNull) {
				categoria = null;
			}
			try {
				precio = (decimal)dataReader ["precio"];
			}catch{
				precio = 0;
			}
			dataReader.Close ();

		}

		private void init(String titulo){

			this.Build ();
			Title = titulo;
			entryNombre.Text = nombre;
			QueryResult queryResult = PersisterHelper.Get ("select * from categoria");
			ComboBoxHelper.Fill (comboBoxCategoria, queryResult, categoria);
			spinButtonPrecio.Value = Convert.ToDouble (precio);
			saveAction.Activated += delegate {
				save();
			};

		}

		 private void update(){
			IDbCommand dbCommand = App.Instance.DbConnection.CreateCommand ();
			dbCommand.CommandText = "UPDATE articulo SET nombre = @nombre, categoria = @categoria, precio = @precio where id = @id";

			nombre = entryNombre.Text;
			categoria = ComboBoxHelper.GetId(comboBoxCategoria);
			precio = Convert.ToDecimal(spinButtonPrecio.Value);

			DbCommandHelper.AddParameter (dbCommand, "nombre", nombre);
			DbCommandHelper.AddParameter (dbCommand, "categoria", categoria);
			DbCommandHelper.AddParameter (dbCommand, "precio", precio);
			DbCommandHelper.AddParameter (dbCommand, "id", id);

			dbCommand.ExecuteNonQuery();
			this.Destroy();

		}

		private void insert(){
			IDbCommand dbCommand = App.Instance.DbConnection.CreateCommand ();
			
			nombre = entryNombre.Text;
			categoria = ComboBoxHelper.GetId(comboBoxCategoria);
			precio = Convert.ToDecimal(spinButtonPrecio.Value);

			dbCommand.CommandText = "insert into articulo (nombre, categoria, precio) " +
				"values (@nombre, @categoria, @precio)";


			DbCommandHelper.AddParameter (dbCommand, "nombre", nombre);

			DbCommandHelper.AddParameter (dbCommand, "categoria", categoria);

			DbCommandHelper.AddParameter (dbCommand, "precio", precio);

			dbCommand.ExecuteNonQuery();
		}


	}
}

