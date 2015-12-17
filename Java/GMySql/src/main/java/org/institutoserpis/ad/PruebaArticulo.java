package org.institutoserpis.ad;

import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.SQLException;

public class PruebaArticulo {

	public static void main(String[] args) throws SQLException {
		Connection connection = DriverManager.getConnection(
			"jdbc:mysql://localhost/dbprueba", "root", "sistemas");
		connection.close();
		System.out.println("fin");
	}

}
