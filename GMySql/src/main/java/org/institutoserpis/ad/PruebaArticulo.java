package org.institutoserpis.ad;

import java.io.IOException;
import java.math.BigDecimal;
import java.math.BigInteger;
import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.Scanner;

import com.mysql.jdbc.Statement;

public class PruebaArticulo {
	
	static Scanner tcl = new Scanner(System.in);

	public static void main(String[] args) throws SQLException, IOException  {
		
		
		
		boolean acabado =false;
		
		while (acabado==false){
			int eleccion = menu();
			
			switch (eleccion){
				case 0:
					acabado = true;
					break;
				case 1:
					//System.out.println("Ha elegido: "+uno);
					cls();
					System.out.println("******************************");
					System.out.println("Visualizar base de datos");
					System.out.println("******************************");
					visualizar();
					System.out.println("\n\nPulsa Intro para continuar.");
					System.in.read();
					break;
				case 2:
					cls();
					System.out.println("******************************");
					System.out.println("Crear articulo en la base de datos");
					System.out.println("******************************");
					crear();
					System.out.println("\n\nPulsa Intro para continuar.");
					System.in.read();
					break;
				case 3:
					cls();
					System.out.println("******************************");
					System.out.println("Actualizar articulo en la base de datos");
					System.out.println("******************************");
					actualizar();
					System.out.println("\n\nPulsa Intro para continuar.");
					System.in.read();
					break;
				case 4:
					cls();
					System.out.println("******************************");
					System.out.println("Eliminar articulo en la base de datos");
					System.out.println("******************************");
					borrar();
					System.out.println("\n\nPulsa Intro para continuar.");
					System.in.read();
					break;
				default:
					cls();
					System.out.println("No permitido.");
					System.out.println("\n\nPulsa Intro para continuar.");
					System.in.read();
					break;
			}
		}
		System.out.println("\nFIN");
		
	}
	
	
	
	
	public static Connection conexion() throws SQLException{
		Connection connection = DriverManager.getConnection(
				"jdbc:mysql://localhost/dbprueba", "root", "sistemas");
		return connection;
		
	}
	
	public static int menu(){
		System.out.println("********************");
		System.out.println("Menú selección SQL" );
		System.out.println("********************");
		String uno = "1- Visualizar Articulos";
		String dos = "2- Crear Articulo";
		String tres = "3- Actualizar Articulo";
		String cuatro = "4- Borrar Articulo";
		System.out.println(uno);
		System.out.println(dos);
		System.out.println(tres);
		System.out.println(cuatro);
		System.out.println("0- Salir");

		System.out.print("\nElija opción: ");
		int eleccion = tcl.nextInt();
		return eleccion;
		
	}
	
	public static void visualizar() throws SQLException{
		//Creamos la conexion
		Connection connection = conexion();
		//Escribimos la consulta
		String query = "SELECT * FROM articulo";
		ResultSet rs = ver(connection, query);
		
		while (rs.next()){
		      int id = rs.getInt("id");
		      String nombre = rs.getString("nombre");
			  BigDecimal precio = rs.getBigDecimal("precio");
		      BigInteger categoria = (BigInteger.valueOf(rs.getInt("categoria")));
		      
		      //imprimimos los resultados
		     
			   System.out.format("%s, %s, %s, %s\n",id, nombre, categoria, precio);
		}
		connection.close();
	}//fin metodo visualizar
	
	
	
	
	
	public static ResultSet ver(Connection connection, String query) throws SQLException{
		
		// create the java statement
		Statement st = (Statement) connection.createStatement();
		// execute the query, and get a java resultset
		ResultSet rs = st.executeQuery(query);
		return rs;
	}//final ver
	
	public static void crear() throws SQLException{
		//Creamos la conexion
		Connection connection = conexion();
		tcl.nextLine();
		
		System.out.print("Introduzca el nombre del producto: ");
		String nombre = tcl.nextLine();
		String query = "SELECT * FROM categoria";
		ResultSet rs = ver(connection, query);
		String cat="";
		while(rs.next()){
			cat+=rs.getString("nombre")+" ";
		}
		System.out.println("Recuerde que la categoria solo puede ser: "+cat);
		
		System.out.print("Introduzca la categoria del producto: ");
		
		BigInteger categoria = (BigInteger.valueOf(tcl.nextInt()));
		System.out.print("Introduzca el precio del producto: ");
		BigDecimal precio = tcl.nextBigDecimal();
		
		insertData(connection, "articulo", nombre, categoria, precio);
		connection.close();
	}
	
	public static void actualizar() throws SQLException{
		String resp="";
		String nombre="";
		BigInteger categoria=null;
		BigDecimal precio=null;
		//Creamos la conexion
		Connection connection = conexion();
		System.out.println("Aqui tiene una relacion de los articulos que puede actualizar: ");
		visualizar();
		
		System.out.print("Introduzca la id a modificar: ");
		BigInteger id= BigInteger.valueOf(tcl.nextInt());
		String query = "SELECT * FROM articulo where id="+id;
		
		ResultSet rs = ver(connection, query);
		while(rs.next()){
			nombre= rs.getString("nombre");
			//System.out.println("Nombre: "+nombre);
			categoria=BigInteger.valueOf(rs.getInt("categoria"));
			//System.out.println("Categoria: "+categoria);
			precio=rs.getBigDecimal("precio");
			//System.out.println("Nombre: "+nombre+"\nCategoria: "+categoria+"\nPrecio: "+precio);
		}
		tcl.nextLine();
		
		System.out.print("¿Quiere modificar el nombre? (S/N): ");
		resp=tcl.nextLine();
		if ((resp.equals("S")) || (resp.equals("s"))){
			System.out.print("Introduce el nuevo nombre: ");
			nombre=tcl.nextLine();
		}
		System.out.print("¿Quiere modificar la categoria? (S/N): ");
		resp=tcl.nextLine();
		if ((resp.equals("S")) || (resp.equals("s"))){
			query = "SELECT * FROM categoria";
			rs = ver(connection, query);
			String cat="";
			while(rs.next()){
				cat+=rs.getString("nombre")+" ";
			}
			System.out.println("Recuerde que la categoria solo puede ser: "+cat);
			System.out.print("Introduce la nueva categoria: ");
			categoria=BigInteger.valueOf(tcl.nextInt());
			tcl.nextLine();
		}
		System.out.print("¿Quiere modificar el precio? (S/N): ");
		resp=tcl.nextLine();
		if ((resp.equals("S")) || (resp.equals("s"))){
			System.out.print("Introduce el nuevo precio: ");
			precio = tcl.nextBigDecimal();
			tcl.nextLine();
		}
	
		
		updateData(connection, "articulo", nombre, categoria, precio, id);
		
		
		connection.close();
	}
	
	public static void borrar() throws SQLException{
		//Creamos la conexion
		Connection connection = conexion();
		tcl.nextLine();
		
		System.out.println("Aqui tiene una relacion de los articulos que puede borrar: ");
		visualizar();
		System.out.print("Introduzca la ID que desea eliminar: ");
		BigInteger id= (BigInteger.valueOf(tcl.nextInt()));
		
		deleteData(connection, "articulo", id);
		connection.close();
	}
	
	public static void insertData(Connection connection, String table_name, String nombre, BigInteger categoria, BigDecimal precio) {
        try {
        	String Query = "INSERT INTO "+ table_name +" (nombre, categoria, precio) VALUES ('" + nombre + "', '" + categoria + "', '" + precio + "')";
    		
            Statement st = (Statement) connection.createStatement();
            st.executeUpdate(Query);
            System.out.println("Datos almacenados de forma exitosa");
        } catch (SQLException ex) {
        	System.out.println(ex);
        	System.out.println("Error en el almacenamiento de datos");
        }
    }//final insertData
	
	public static void updateData(Connection connection, String table_name, String nombre, BigInteger categoria, BigDecimal precio, BigInteger id) {
        try {
        	String Query = "UPDATE " + table_name + " SET nombre='" + nombre +"', categoria='" + categoria + "', precio='" + precio + "' WHERE id='" + id + "'";
        	//String Query = "INSERT INTO "+ table_name +" (nombre, categoria, precio) VALUES ('" + nombre + "', '" + categoria + "', '" + precio + "')";
    		
            Statement st = (Statement) connection.createStatement();
            st.executeUpdate(Query);
            System.out.println("Datos almacenados de forma exitosa");
        } catch (SQLException ex) {
        	System.out.println(ex);
        	System.out.println("Error en el almacenamiento de datos");
        }
    }//final refreshData
	
	public static void deleteData(Connection connection, String table_name, BigInteger id) {
        try {
        	String Query = "DELETE FROM " + table_name + " WHERE id='" + id + "'";
        	
            Statement st = (Statement) connection.createStatement();
            st.executeUpdate(Query);
            System.out.println("Datos eliminados de forma exitosa");
        } catch (SQLException ex) {
        	System.out.println(ex);
        	System.out.println("Error en la eliminacion de datos");
        }
    }//final deleteData
	
	
	public final static void cls()
	{
	    try
	    {
	        final String os = System.getProperty("os.name");

	        if (os.contains("Windows"))
	        {
	            Runtime.getRuntime().exec("cls");
	        }
	        else
	        {
	            Runtime.getRuntime().exec("clear");
	        }
	    }
	    catch (final Exception e)
	    {
	        System.out.println("Error al ejecutar cls");
	    }
	}

}