using System;
using System.Reflection;

namespace PReflection
{
	class MainClass
	{
		public static void Main (string[] args)
		{
//			int i=33;
//			Type typeI = i.GetType ();
//			showType (typeI);
//
//			string s="Hola";
//			Type typeS = s.GetType ();
//			showType (typeS);
//
//			Type typeX = typeof(string);
//			showType (typeX);
//
//			Type typeO = typeof(object);
//			showType (typeO);

			Type typeFoo = typeof(Foo);
			//showType (typeof(Foo));

			Articulo articulo = new Articulo ();
			//showType (articulo.GetType ());
//			articulo.Nombre = "nuevo 33";
//			articulo.Categoria = 2;
//			articulo.Precio = decimal.Parse("3,5");
			setValues (articulo, 
			           new object[] { 33L, "Nuevo 33 modificado", 3L, decimal.Parse("33,33") });
			showObject (articulo);


		}

		private static void showType (Type type){
			Console.WriteLine ("type.Name={0} type.FullName={1} type.BaseType.Name={2}",
			                   type.Name, type.FullName, type.BaseType.Name);
			PropertyInfo[] propertyInfos = type.GetProperties ();
	
			foreach (PropertyInfo propertyInfo in propertyInfos) {
				Console.WriteLine ("propertyInfo.Name = {0} propertyInfo.PropertyType = {1}",
				                   propertyInfo.Name, propertyInfo.PropertyType);
			}
		}

		private static void showObject(object obj){

			Type type = obj.GetType ();
			PropertyInfo[] propertyInfos = type.GetProperties ();

			foreach (PropertyInfo propertyInfo in propertyInfos) {
				Console.WriteLine("{0}={1}",
				                  propertyInfo.Name, propertyInfo.GetValue (obj, null));
			}


		}



		private static void setValues(object obj, object[] values){
			Type type = obj.GetType ();
			PropertyInfo[] propertyInfos = type.GetProperties ();
			int index = 0;
			foreach (PropertyInfo propertyInfo in propertyInfos) {
				propertyInfo.SetValue (obj, values[index++],null);
			}
		}




	}
	public class Foo {
		private object id;

		public object Id {
			get {
				return id;
			}
			set {
				id = value;
			}
		}

		private string name;


		public string Name {
			get {
				return name;
			}
			set {
				name = value;
			}
		}
	}

	[Entity]
	[Table("articulos")]
	public class Articulo
	{
		public Articulo ()
		{
		}

		private object id;
		private string nombre;
		private object categoria;
		private decimal precio;

		public object Id {
			get { 
				return id;
			}
			set { 
				id = value;
			}
		}

		public string Nombre {
			get {
				return nombre;
			}
			set { 
				nombre = value; 
			}
		}


		public object Categoria {
			get {
				return categoria;
			}
			set {
				categoria = value;
			}
		}


		public decimal Precio {
			get {
				return precio;
			}
			set {
				precio = value;
			}
		}
	}
}
