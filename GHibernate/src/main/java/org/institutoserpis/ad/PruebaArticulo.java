package org.institutoserpis.ad;

import java.math.BigDecimal;
import java.sql.SQLException;
import java.text.DecimalFormat;
import java.text.ParseException;
import java.util.List;
import java.util.Scanner;

import javax.persistence.EntityManager;
import javax.persistence.EntityManagerFactory;
import javax.persistence.Persistence;






public class PruebaArticulo {

private enum Action {Salir, Nuevo, Editar, Eliminar, Consultar};
	
	private static Scanner scanner = new Scanner(System.in);
	
	private static Action scanAction(){
		while (true){
			System.out.print("0-Salir 1-Nuevo 2-Editar 3-Eliminar 4-Consultar: ");
			String action = scanner.nextLine().trim();
			if (action.matches("[012345]"))
				return Action.values()[Integer.parseInt(action)];
			System.out.println("Opción Invalida");
		}
	}
	
	private static String scanString(String label){
		System.out.print(label);
		return scanner.nextLine().trim();
	}
	
	private static Long scanLong(String label){
		while (true){
			System.out.print(label);
			String data = scanner.nextLine().trim();
			try{
				return Long.parseLong(data);
			}catch (NumberFormatException ex){
				System.out.println("Debe ser un numero");
			}
		}
	}
	
	private static BigDecimal scanBigDecimal(String label){
		while (true){
			System.out.print(label);
			String data = scanner.nextLine().trim();
			DecimalFormat decimalFormat = (DecimalFormat)DecimalFormat.getInstance();
			decimalFormat.setParseBigDecimal(true);
			try{
				return (BigDecimal)decimalFormat.parse(data);
			}catch (ParseException e){
				System.out.println("Debe ser un número decimal");
			}
		}
	}
	
	
	
	//======================================================
	// Metodos
	//======================================================
	private static Articulo scanArticulo(){
		Articulo articulo = new Articulo();
		articulo.setNombre(scanString(    "     Nombre: "));
		articulo.setCategoria(scanLong(   "  Categoria: "));
		articulo.setPrecio(scanBigDecimal("     Precio: "));
		return articulo;
	}
	
	private static void consultar(){
		System.out.println("======== Consultar artículo ========");
		EntityManagerFactory entityManagerFactory = 
				Persistence.createEntityManagerFactory("org.institutoserpis.ad");
		
		EntityManager entityManager = entityManagerFactory.createEntityManager();
		entityManager.getTransaction().begin();
		List<Articulo> articulos = entityManager.createQuery("from Articulo", Articulo.class).getResultList();
		for (Articulo articulo : articulos)
			System.out.printf("%5s %-30s %5s %10s\n", 
					articulo.getId(), 
					articulo.getNombre(), 
					articulo.getCategoria(), 
					articulo.getPrecio()
			);
		entityManager.getTransaction().commit();
		entityManager.close();
		
		entityManagerFactory.close();
		
	}
	
	private static void nuevo(){
		System.out.println("======== Nuevo artículo ========");
		EntityManagerFactory entityManagerFactory = 
				Persistence.createEntityManagerFactory("org.institutoserpis.ad");
		Articulo articulo = scanArticulo();
		EntityManager entityManager = entityManagerFactory.createEntityManager();
		entityManager.getTransaction().begin();
		entityManager.persist( articulo );
		entityManager.getTransaction().commit();
		entityManager.close();
	}
	
	private static void editar(){
		System.out.println("======== Editar artículo ========");
		//System.out.println("It doesn't developed yet");
		Long id = scanLong("Introduzca el id del articulo a editar: ");
		Articulo articulo =  scanArticulo();
		
		
		/*
		 * try{
			if (updatePreparedStatement == null)
				updatePreparedStatement = connection.prepareStatement(updateSql);
			updatePreparedStatement.setString(1, articulo.nombre);
			updatePreparedStatement.setLong(2, articulo.categoria);
			updatePreparedStatement.setBigDecimal(3,  articulo.precio);
			updatePreparedStatement.setLong(4,  id);
			int count = updatePreparedStatement.executeUpdate();
			if (count == 1)
				System.out.println("artículo guardado");
			else
				System.out.println("Error: No existe artículo con ese id");
		}
		catch (SQLException ex){
			showSQLException(ex);
		}
		 */
		
	}
	
	private static void eliminar(){
		System.out.println("======== Eliminar artículo ========");
		Long id = scanLong(" Introduce el id a eliminar: ");
		EntityManagerFactory emf =
				Persistence.createEntityManagerFactory("org.institutoserpis.ad");
				EntityManager em = emf.createEntityManager();
				try {
				 
					Articulo articulo = em.find(Articulo.class, id);
					em.getTransaction().begin();
					em.remove(articulo);
					em.getTransaction().commit();
				} catch (Exception e) {
					em.close();
				}
	}
	
	
	public static void main (String[] args) {

		
		while (true){
			Action action = scanAction();
			if (action == Action.Salir) break;
			if (action == Action.Nuevo) nuevo();
			if (action == Action.Editar) editar();
			if (action == Action.Eliminar) eliminar();
			if (action == Action.Consultar) consultar();
			System.out.println();
		}
		
		/*
		EntityManagerFactory entityManagerFactory = 
				Persistence.createEntityManagerFactory("org.institutoserpis.ad");
		
		EntityManager entityManager = entityManagerFactory.createEntityManager();
		entityManager.getTransaction().begin();
		List<Articulo> articulos = entityManager.createQuery("from Articulo", Articulo.class).getResultList();
		for (Articulo articulo : articulos)
			System.out.printf("%5s %-30s %5s %10s\n", 
					articulo.getId(), 
					articulo.getNombre(), 
					articulo.getCategoria(), 
					articulo.getPrecio()
			);
		entityManager.getTransaction().commit();
		entityManager.close();
		
		entityManagerFactory.close();
		*/
	}

}