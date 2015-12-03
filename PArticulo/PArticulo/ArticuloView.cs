using Gtk;
using System;
using System.Collections;
using System.Data;
using SerpisAd;

namespace PArticulo
{
	public partial class ArticuloView : Gtk.Window
	{
		private Articulo articulo;

		public ArticuloView () : base(Gtk.WindowType.Toplevel)
		{
			articulo = new Articulo ();
			articulo.Nombre = "";
			init ();
			saveAction.Activated += delegate {	insert();	};
		}

		public ArticuloView(object id) :base(WindowType.Toplevel) {
			articulo = ArticuloPersister.Load(id);
			init ();
			saveAction.Activated += delegate {	update();	};
		}

		private void init() {
			this.Build ();
			entryNombre.Text = articulo.Nombre;
			QueryResult queryResult = PersisterHelper.Get ("select * from categoria");
			ComboBoxHelper.Fill (comboBoxCategoria, queryResult, articulo.Categoria);
			spinButtonPrecio.Value = Convert.ToDouble(articulo.Precio);
		}

		private void updateModel() {
			articulo.Nombre = entryNombre.Text;
			articulo.Categoria = ComboBoxHelper.GetId (comboBoxCategoria);
			articulo.Precio = Convert.ToDecimal(spinButtonPrecio.Value);
		}

		private void insert() {
			updateModel ();
			Persister.Insert (articulo);
			Destroy ();
		}

		private void update() {
			updateModel ();
			ArticuloPersister.Update (articulo);
			Destroy ();
		}

	}
}


