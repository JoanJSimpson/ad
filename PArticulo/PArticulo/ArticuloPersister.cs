using System;
using System.Data;
using SerpisAd;

namespace PArticulo
{
	public class ArticuloPersister
	{
		public static Articulo Load(object id){
			//throw NotImplementedException();
			Articulo articulo = new Articulo ();
			articulo.Id = id;
			IDbCommand dbCommand = App.Instance.DbConnection.CreateCommand ();
			dbCommand.CommandText = "select * from articulo where id = @id";
			DbCommandHelper.AddParameter (dbCommand, "id", id);
			IDataReader dataReader = dbCommand.ExecuteReader ();
			try{
				dataReader.Read();

			}catch (Exception e){
				Console.WriteLine (e+ "dataReader no puede leer");
			}
//			if (!dataReader.Read ()) {
//				//ToDO throw Exception
//				Console.WriteLine("dataReader no puede leer");
//				return;
//			}
			articulo.Nombre = (string)dataReader ["nombre"];
			articulo.Categoria = dataReader ["categoria"];
			if (articulo.Categoria is DBNull) {
				articulo.Categoria = null;
			}
			try {
				articulo.Precio = (decimal)dataReader ["precio"];
			}catch{
				articulo.Precio = 0;
			}

			dataReader.Close ();
			return articulo;


		}
		public static void Insert(Articulo articulo){
			IDbCommand dbCommand = App.Instance.DbConnection.CreateCommand ();


			dbCommand.CommandText = "insert into articulo (nombre, categoria, precio) " +
				"values (@nombre, @categoria, @precio)";


			DbCommandHelper.AddParameter (dbCommand, "nombre", articulo.Nombre);

			DbCommandHelper.AddParameter (dbCommand, "categoria", articulo.Categoria);

			DbCommandHelper.AddParameter (dbCommand, "precio", articulo.Precio);

			dbCommand.ExecuteNonQuery();
		}

		public static void Update (Articulo articulo){
			IDbCommand dbCommand = App.Instance.DbConnection.CreateCommand ();
			dbCommand.CommandText = "UPDATE articulo SET nombre = @nombre, categoria = @categoria, precio = @precio where id = @id";


			DbCommandHelper.AddParameter (dbCommand, "nombre", articulo.Nombre);
			DbCommandHelper.AddParameter (dbCommand, "categoria", articulo.Categoria);
			DbCommandHelper.AddParameter (dbCommand, "precio", articulo.Precio);
			DbCommandHelper.AddParameter (dbCommand, "id", articulo.Id);

			dbCommand.ExecuteNonQuery();
		}
	}
}

