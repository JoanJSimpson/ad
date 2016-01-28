package org.institutoserpis.ad;

import java.math.BigDecimal;
import java.sql.SQLException;
import java.text.DecimalFormat;
import java.text.ParseException;
import java.util.Date;
import java.util.List;
import java.util.Scanner;
import java.util.logging.Level;
import java.util.logging.Logger;

import javax.persistence.EntityManager;
import javax.persistence.EntityManagerFactory;
import javax.persistence.Persistence;




public class PruebaArticuloLuis {

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
	// Metodos Luis
	//======================================================
	private static void show(Articulo articulo){
		System.out.printf("%5s %-40s %5s %10s\n",
				articulo.getId(),
				articulo.getNombre(),
				articulo.getCategoria(),
				articulo.getPrecio()
				);
	}
	
	private static void query(){

		System.out.println("Query: ");
			
			EntityManager entityManager = entityManagerFactory.createEntityManager();
			entityManager.getTransaction().begin();
			List<Articulo> articulos = entityManager.createQuery("from Articulo", Articulo.class).getResultList();
			for (Articulo articulo : articulos)
				show(articulo);
			entityManager.getTransaction().commit();
			entityManager.close();
		}
	
	private static Long persist(){	
		System.out.println("Persist: ");
		EntityManager entityManager = entityManagerFactory.createEntityManager();
		entityManager.getTransaction().begin();
		Articulo articulo = new Articulo();
		articulo.setNombre("nuevo "+ new Date());
		entityManager.persist(articulo);
		entityManager.getTransaction().commit();
		entityManager.close();
		show(articulo);
		return articulo.getId();
	}
	
	private static void find(Long id){
		System.out.println("Find: "+id);
		EntityManager entityManager = entityManagerFactory.createEntityManager();
		entityManager.getTransaction().begin();
		Articulo articulo = entityManager.find(Articulo.class, id);
		
		entityManager.getTransaction().commit();
		entityManager.close();
		show(articulo);
	}
	
	private static void remove(Long id){
		System.out.println("Remove: "+id);
		EntityManager entityManager = entityManagerFactory.createEntityManager();
		entityManager.getTransaction().begin();
		Articulo articulo = entityManager.find(Articulo.class, id);
		entityManager.remove(articulo);
		
		entityManager.getTransaction().commit();
		entityManager.close();
		show(articulo);
	}
	
	private static void update(Long id){
		System.out.println("Update: "+id);
		EntityManager entityManager = entityManagerFactory.createEntityManager();
		entityManager.getTransaction().begin();
		Articulo articulo = entityManager.find(Articulo.class, id);
		articulo.setNombre("Modificado "+new Date());
		
		entityManager.getTransaction().commit();
		entityManager.close();
		show(articulo);
	}
	
	//======================================================
	// Metodos Joan
	//======================================================
	private static Articulo scanArticulo(){
		Articulo articulo = new Articulo();
		articulo.setNombre(scanString(    "     Nombre: "));
		articulo.setCategoria(scanLong(   "  Categoria: "));
		articulo.setPrecio(scanBigDecimal("     Precio: "));
		return articulo;
	}
	
	private static Articulo scanArticuloMod(Articulo articulo){
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
		EntityManagerFactory emf =
				Persistence.createEntityManagerFactory("org.institutoserpis.ad");
				EntityManager em = emf.createEntityManager();
				try {
					Articulo articulo = em.find(Articulo.class, id);
					em.getTransaction().begin();
					articulo = scanArticuloMod(articulo);
					em.merge(articulo);
					//em.remove(articulo);
					em.getTransaction().commit();
					System.out.println("Articulo "+id+" modificado correctamente");
				} catch (Exception e) {
					System.out.println("Error al modificar el Articulo con id: "+id+"\n"
							+ "Comprueba que existe ese id");
					em.close();
				}
		
		/*Long id = scanLong("Introduzca el id del articulo a editar: ");
		Articulo articulo =  scanArticulo();
		articulo.setId(id);
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
					System.out.println("Articulo "+id+" eliminado correctamente");
				} catch (Exception e) {
					System.out.println("Error al eliminar el Articulo con id: "+id+"\n"
							+ "Comprueba que existe ese id");
					em.close();
				}
	}
	
	private static EntityManagerFactory entityManagerFactory;
	
	public static void main (String[] args) {

		//Desde aqui Luis
		Logger.getLogger("org.hibernate").setLevel(Level.SEVERE);
		
		System.out.println("inicio");
		entityManagerFactory = 
				Persistence.createEntityManagerFactory("org.institutoserpis.ad");
		
		Long articuloId = persist();
		find(articuloId);
		update(articuloId);
		remove(articuloId);
		query();
		entityManagerFactory.close();
		
		//Desde aqui lo mio
		/*
		while (true){
			Action action = scanAction();
			if (action == Action.Salir) break;
			if (action == Action.Nuevo) nuevo();
			if (action == Action.Editar) editar();
			if (action == Action.Eliminar) eliminar();
			if (action == Action.Consultar) consultar();
			System.out.println();
		}*/
		
	
	}
	

}