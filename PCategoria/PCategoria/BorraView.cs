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
			//dbCommand.CommandText = "delete from categoria " +
				//"where {0} = @param";

			string deleteSql = "delete from categoria " +
				"where {0} = (param)"+" values(@param)";
			string fieldName="";

			string param="";
			string nombre = entryNombre.Text;
			String id = entryId.Text;
			if (!nombre.Equals ("")) {
				fieldName = "nombre";
				Console.WriteLine ("Entra en nombre");
				param = nombre;
			} else if (!id.Equals("")) {
				fieldName = "id";
				Console.WriteLine ("Entra en id");
				param = id;
			} else {
				//que salga una ventana diciendo que seleecione algo
			}

			string inyectar = string.Format (deleteSql, fieldName);
			dbCommand.CommandText = inyectar;
			Console.WriteLine (inyectar+": Param: "+param);

			DbCommandHelper.AddParameter (dbCommand, "param", param);

			dbCommand.ExecuteNonQuery();
		}
	}
}

