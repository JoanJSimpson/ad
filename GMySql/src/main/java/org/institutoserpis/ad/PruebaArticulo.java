package org.institutoserpis.ad;

import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.ResultSet;
import java.sql.SQLException;

import com.mysql.jdbc.Statement;

public class PruebaArticulo {

	public static void main(String[] args) throws SQLException  {
		
		Connection connection = conexion();
	
	
	// our SQL SELECT query. 
    String query = "SELECT * FROM articulo";

    // create the java statement
    Statement st = (Statement) connection.createStatement();
     
    // execute the query, and get a java resultset
    ResultSet rs = st.executeQuery(query);
     
    // iterate through the java resultset
    while (rs.next())
    {
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
	System.out.println("fin");
    
	}
	
	public static Connection conexion() throws SQLException{
		Connection connection = DriverManager.getConnection(
				"jdbc:mysql://localhost/dbprueba", "root", "sistemas");
		return connection;
		
	}

}