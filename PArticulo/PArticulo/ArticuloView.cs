using Gtk;
using System;
using System.Collections;
using SerpisAd;
using System.Data;

namespace PArticulo
{
	public partial class ArticuloView : Gtk.Window
	{
		public ArticuloView () : 
				base(Gtk.WindowType.Toplevel)
		{
			this.Build ();
			//entryNombre.Text = "nuevo";
			QueryResult queryResult = PersisterHelper.Get ("select * from categoria");
			ComboBoxHelper.Fill (comboBoxCategoria, queryResult);

			saveAction.Activated += delegate {
				save();
			};


//			CellRendererText cellRendererText = new CellRendererText ();
//			comboBoxCategoria.PackStart (cellRendererText, false);
//			comboBoxCategoria.SetCellDataFunc (cellRendererText, 
//				delegate(CellLayout cell_layout, CellRenderer cell, TreeModel tree_model, TreeIter iter) {
//					IList row = (IList)tree_model.GetValue(iter, 0);
//					cellRendererText.Text = row[1].ToString();
//				});
//			ListStore listStore = new ListStore (typeof(IList));
//			foreach (IList row in queryResult.Rows)
//				listStore.AppendValues (row);
//			comboBoxCategoria.Model = listStore;

			//comboBoxCategoria.
			//spinButtonPrecio.Value = 1.5;

		}
		private void save(){
			IDbCommand dbCommand = App.Instance.DbConnection.CreateCommand ();
			dbCommand.CommandText = "insert into articulo (nombre, categoria, precio) " +
				"values (@nombre, @categoria, @precio)";


			string nombre = entryNombre.Text;
			object categoria = ComboBoxHelper.GetId(comboBoxCategoria);
			decimal precio = Convert.ToDecimal(spinButtonPrecio.Value);


			DbCommandHelper.AddParameter (dbCommand, "nombre", nombre);
//			IDbDataParameter dbDataParameterNombre = dbCommand.CreateParameter ();
//			dbDataParameterNombre.ParameterName = "nombre";
//			dbDataParameterNombre.Value = nombre;
//			dbCommand.Parameters.Add (dbDataParameterNombre);


			DbCommandHelper.AddParameter (dbCommand, "categoria", categoria);
//			IDbDataParameter dbDataParameterCategoria = dbCommand.CreateParameter ();
//			dbDataParameterCategoria.ParameterName = "categoria";
//			dbDataParameterCategoria.Value = categoria;
//			dbCommand.Parameters.Add (dbDataParameterCategoria);

			DbCommandHelper.AddParameter (dbCommand, "precio", precio);
//			IDbDataParameter dbDataParameterPrecio = dbCommand.CreateParameter ();
//			dbDataParameterPrecio.ParameterName = "precio";
//			dbDataParameterPrecio.Value = precio;
//			dbCommand.Parameters.Add (dbDataParameterPrecio);

			dbCommand.ExecuteNonQuery();
		}




	}
}

