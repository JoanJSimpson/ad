package org.institutoserpis.ad;

import java.io.IOException;
import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.Scanner;

import com.mysql.jdbc.Statement;

public class PruebaArticulo {
	
	static Scanner tcl = new Scanner(System.in);

	public static void main(String[] args) throws SQLException, IOException  {
		
		String uno = "1- Visualizar Articulos";
		String dos = "2- Crear Articulo";
		String tres = "3- Actualizar Articulo";
		String cuatro = "4- Borrar Articulo";
		
		
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
					System.out.println("Ha elegido: "+tres);
					break;
				case 4:
					cls();
					System.out.println("Ha elegido: "+cuatro);
					break;
				default:
					cls();
					System.out.println("No permitido.");
					System.out.println("\n\nPulsa Intro para continuar.");
					System.in.read();
					break;
			}
		}
		System.out.println("FIN");
		
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
		//escribimos el query 
		String query = "SELECT * FROM articulo";
		// create the java statement
		Statement st = (Statement) connection.createStatement();
		// execute the query, and get a java resultset
		ResultSet rs = st.executeQuery(query);
		// iterate through the java resultset
		while (rs.next()){
		      int id = rs.getInt("id");
		      String nombre = rs.getString("nombre");
			  Double precio = rs.getDouble("precio");
		      //String lastName = rs.getString("precio");
		      //boolean isAdmin = rs.getBoolean("is_admin");
		      //int numPoints = rs.getInt("num_points");
			       
		      // print the results
		      //System.out.format("%s, %s, %s, %s, %s, %s\n", id, firstName, lastName, dateCreated, isAdmin, numPoints);
			   System.out.format("%s, %s, %s\n",id, nombre, precio);
		}
		connection.close();
	}
	
	public static void crear(){
		
	}
	
	
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