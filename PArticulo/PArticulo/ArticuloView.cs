using Gtk;
using System;
using System.Collections;
using SerpisAd;
using System.Data;

namespace PArticulo
{
	public partial class ArticuloView : Gtk.Window
	{
		private object id;
		private object categoria;
		public ArticuloView () : 
				base(Gtk.WindowType.Toplevel)
		{

			this.Build ();
			Title = "Artículo Nuevo";
			QueryResult queryResult = PersisterHelper.Get ("select * from categoria");
			ComboBoxHelper.Fill (comboBoxCategoria, queryResult, categoria);

			saveAction.Activated += delegate {
				save();
			};
//			
		}

		//this() llama al constructor por defecto
		public ArticuloView(object id) : this() {
			Title = "Editar Artículo";
			this.id = id;
			load ();
		}

		private void load(){
			IDbCommand dbCommand = App.Instance.DbConnection.CreateCommand ();
			dbCommand.CommandText = "select * from articulo where id = @id";
			DbCommandHelper.AddParameter (dbCommand, "id", id);
			IDataReader dataReader = dbCommand.ExecuteReader ();
			if (!dataReader.Read ()) {
				//ToDO throw Exception
				return;
			}
			string nombre = (string)dataReader ["nombre"];
			categoria = dataReader ["categoria"];
			if (categoria is DBNull) {
				categoria = null;
			}
			decimal precio = (decimal)dataReader ["precio"];
			dataReader.Close ();
			entryNombre.Text = nombre;
			//ToDO posicionarnos en comboBoxCategoria
			spinButtonPrecio.Value = Convert.ToDouble (precio);


		}

		private void save(){
			IDbCommand dbCommand = App.Instance.DbConnection.CreateCommand ();
			dbCommand.CommandText = "insert into articulo (nombre, categoria, precio) " +
				"values (@nombre, @categoria, @precio)";

			string nombre = entryNombre.Text;
			categoria = ComboBoxHelper.GetId(comboBoxCategoria);
			decimal precio = Convert.ToDecimal(spinButtonPrecio.Value);

			DbCommandHelper.AddParameter (dbCommand, "nombre", nombre);

			DbCommandHelper.AddParameter (dbCommand, "categoria", categoria);

			DbCommandHelper.AddParameter (dbCommand, "precio", precio);

			dbCommand.ExecuteNonQuery();
		}




	}
}

