package org.institutoserpis.ad;

import java.math.BigDecimal;

import javax.swing.JFrame;
import javax.swing.JOptionPane;
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




public class PruebaArticuloJOption {

private enum Action {Salir, Nuevo, Editar, Eliminar, Consultar};
	
	private static Scanner scanner = new Scanner(System.in);
	
	//======================================================
	// Metodos Scanner
	//======================================================
	
	private static Action scanAction(){
		while (true){
			String action = JOptionPane.showInputDialog("0-Salir\n1-Nuevo\n2-Editar\n3-Eliminar\n4-Consultar");
			//String action = scanner.nextLine().trim();
			/*if(action.equals("") && JOptionPane.YES_OPTION){
				
			}*/
			if (action.matches("[01234]"))
				return Action.values()[Integer.parseInt(action)];
			else if(action.equals(""))
				JOptionPane.showMessageDialog(null, "Introduzca un número", "Error", JOptionPane.WARNING_MESSAGE);
			else
				JOptionPane.showMessageDialog(null, "Opción Invalida", "Error", JOptionPane.WARNING_MESSAGE);
		}
	}
	
	private static String scanString(String label){
		//System.out.print(label);
		//return scanner.nextLine().trim();
		String texto = JOptionPane.showInputDialog(null, "Ingrese el "+label, label, JOptionPane.INFORMATION_MESSAGE);
		return texto;
	}
	
	private static Long scanLong(String label){
		while (true){
			//System.out.print(label);
			//String data = scanner.nextLine().trim();
			String data = JOptionPane.showInputDialog(null, "Ingrese la "+label, label, JOptionPane.INFORMATION_MESSAGE);
			try{
				return Long.parseLong(data);
			}catch (NumberFormatException ex){
				//System.out.println("Debe ser un numero");
				JOptionPane.showMessageDialog(null, "Debe ingresar un numero", "Error", JOptionPane.WARNING_MESSAGE);
			}
		}
	}
	
	private static BigDecimal scanBigDecimal(String label){
		while (true){
			//System.out.print(label);
			//String data = scanner.nextLine().trim();
			String data = JOptionPane.showInputDialog(null, "Ingrese el "+label, label, JOptionPane.INFORMATION_MESSAGE);
			DecimalFormat decimalFormat = (DecimalFormat)DecimalFormat.getInstance();
			decimalFormat.setParseBigDecimal(true);
			try{
				return (BigDecimal)decimalFormat.parse(data);
			}catch (ParseException e){
				//System.out.println("Debe ser un número decimal");
				JOptionPane.showMessageDialog(null, "Debe ingresar un numero decimal", "Error", JOptionPane.WARNING_MESSAGE);
				
			}
		}
	}
	
	private static Categoria scanCategoria(String label){
		Categoria categoria = new Categoria();
		//categoria.setId(scanLong(    "ID de Categoría: "));
		categoria.setId(scanIdCategoria(  "Id de categoría: ", consultarCategorias()));
		//categoria.setNombre(scanIdCategoria(   "Nombre de Categoría: ", consultarCategorias()));
		
		return categoria;
	}
	
	//Cargar un spinner con las categorias que existen
	
	private static Long scanIdCategoria(String label, List<Categoria> cat){
		String nombres[] = new String[cat.size()];
		Long id[] = new Long[cat.size()];
		for (int i=0;i<cat.size();i++){
			nombres[i]=cat.get(i).getNombre();
			id[i]=cat.get(i).getId();
		}
		JFrame frame = new JFrame("Dialogo");
		Long idSel=null;
		String nomSel = (String) JOptionPane.showInputDialog(frame, "Selecciona una categoria", 
				"Categoria",JOptionPane.QUESTION_MESSAGE,null,nombres, nombres[0]);
		for (int i=0; i<cat.size();i++){
			if (nomSel.equals(nombres[i])){
				idSel=id[i];
			}
		}
		return idSel;
	}
	
	private static Articulo scanArticulo(){
		Articulo articulo = new Articulo();
		articulo.setNombre(scanString(    " Nombre: "));
		articulo.setCategoria(scanCategoria(" Categoria: "));
		articulo.setPrecio(scanBigDecimal(" Precio: "));
		return articulo;
	}
	
	private static Articulo scanArticuloMod(Articulo articulo){
		
		articulo.setNombre(scanString(    " Nombre: "));
		Categoria cat = new Categoria();
		//Long id = scanLong(  "ID de Categoría: ");
		//cat.setId(id);
		articulo.setCategoria(scanCategoria(" Categoria: "));
		//articulo.setCategoria(cat);
		articulo.setPrecio(scanBigDecimal(" Precio: "));
		return articulo;
	}
	

	//======================================================
	// Metodos
	//======================================================
	
	private static List<Categoria> consultarCategorias(){
		EntityManagerFactory entityManagerFactory = 
				Persistence.createEntityManagerFactory("org.institutoserpis.ad");
		
		EntityManager entityManager = entityManagerFactory.createEntityManager();
		entityManager.getTransaction().begin();
		List<Categoria> categorias = entityManager.createQuery("from Categoria", Categoria.class).getResultList();
		Categoria cat[] = new Categoria[categorias.size()];
		//for (Categoria categoria : categorias){
		for (int i=0; i< categorias.size();i++){
			cat[i] = categorias.get(i);
		}
		entityManager.close();
		
		entityManagerFactory.close();
		return categorias;
		
	}
	
	private static void consultar(){
		String datos="<html><table border=1><tr><td>ID</td><td>Nombre</td><td>Categoria ID</td><td>Categoria Nombre</td><td>Precio</td></tr>";
		//JOptionPane.showConfirmDialog(null, datos);
		System.out.println("======== Consultar artículo ========");
		EntityManagerFactory entityManagerFactory = 
				Persistence.createEntityManagerFactory("org.institutoserpis.ad");
		
		EntityManager entityManager = entityManagerFactory.createEntityManager();
		entityManager.getTransaction().begin();
		List<Articulo> articulos = entityManager.createQuery("from Articulo", Articulo.class).getResultList();
		for (Articulo articulo : articulos){
			Categoria cat = articulo.getCategoria();
			datos+="<tr><td>"+articulo.getId()+"</td>"+
					"<td>"+articulo.getNombre()+"</td>";
			if (articulo.getCategoria() == null){
				datos+="<td>"+null+"</td>"+
						"<td>"+null+"</td>";
			}else{
				datos+="<td>"+articulo.getCategoria().getId()+"</td>"+
						"<td>"+articulo.getCategoria().getNombre()+"</td>";
			}
			datos+="<td>"+articulo.getPrecio()+"</td></tr>";
		}
			
		JOptionPane.showConfirmDialog(null, datos, "Consultar Artículo", JOptionPane.PLAIN_MESSAGE);
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
		String datos="<html><table border=1><tr><td>ID</td><td>Nombre</td><td>Categoria</td><td>Precio</td></tr>";
		//System.out.println("======== Consultar artículo ========");
		EntityManagerFactory entityManagerFactory = 
				Persistence.createEntityManagerFactory("org.institutoserpis.ad");
		
		EntityManager em = entityManagerFactory.createEntityManager();
		List<Articulo> articulos = em.createQuery("from Articulo", Articulo.class).getResultList();
		for (Articulo articulo : articulos){
			datos+="<tr><td>"+articulo.getId()+"</td>"+
					"<td>"+articulo.getNombre()+"</td>";
			if (articulo.getCategoria() == null){
				datos+="<td>"+null+"</td>";
			}else{
				datos+="<td>"+articulo.getCategoria().getNombre()+"</td>";
			}
			datos+="<td>"+articulo.getPrecio()+"</td></tr>";
		}
		Long id = Long.parseLong(JOptionPane.showInputDialog(null, datos +"\nIntroduce el id a editar", "Editar Artículo", JOptionPane.INFORMATION_MESSAGE));
		
		//Long id = scanLong("Introduzca el id del articulo a editar: ");
				try {
					Articulo articulo = em.find(Articulo.class, id);
					em.getTransaction().begin();
					articulo = scanArticuloMod(articulo);
					em.getTransaction().commit();
					String datos2 = "Articulo "+id+" modificado correctamente";
					JOptionPane.showConfirmDialog(null, datos2, "Editar Articulo", JOptionPane.PLAIN_MESSAGE);
					
				} catch (Exception e) {
					String datos2 = "Error al modificar el Articulo con id "+id;
					JOptionPane.showConfirmDialog(null, datos2, "Error", JOptionPane.WARNING_MESSAGE);
					em.close();
				}
		
	}
	
	//===================================================================================
	//			METODO ELIMINAR
	//===================================================================================
	
	
	
	private static void eliminar(){
		System.out.println("======== Eliminar artículo ========");
		String datos="<html><table border=1><tr><td>ID</td><td>Nombre</td><td>Categoria</td><td>Precio</td></tr>";
		//JOptionPane.showConfirmDialog(null, datos);
		//System.out.println("======== Consultar artículo ========");
		EntityManagerFactory entityManagerFactory = 
				Persistence.createEntityManagerFactory("org.institutoserpis.ad");
		
		EntityManager em = entityManagerFactory.createEntityManager();
		List<Articulo> articulos = em.createQuery("from Articulo", Articulo.class).getResultList();
		for (Articulo articulo : articulos){
			datos+="<tr><td>"+articulo.getId()+"</td>"+
					"<td>"+articulo.getNombre()+"</td>";
			if (articulo.getCategoria() == null){
				datos+="<td>"+null+"</td>";
			}else{
				datos+="<td>"+articulo.getCategoria().getNombre()+"</td>";
			}
			datos+="<td>"+articulo.getPrecio()+"</td></tr>";
		}
		
		Long id = Long.parseLong(JOptionPane.showInputDialog(null, datos +"\nIntroduce el id a eliminar", "Eliminar Artículo", JOptionPane.INFORMATION_MESSAGE));
		int ax = JOptionPane.showConfirmDialog(null, "Estas Seguro?");
        if(ax == JOptionPane.YES_OPTION){
        	try {
    			Articulo articulo = em.find(Articulo.class, id);
    			em.getTransaction().begin();
    			em.remove(articulo);
    			em.getTransaction().commit();
    			String datos2 = "Articulo "+id+" eliminado correctamente";
    			JOptionPane.showConfirmDialog(null, datos2, "Eliminar Articulo", JOptionPane.PLAIN_MESSAGE);
    			
    			
    		} catch (Exception e) {
    			String datos2 = "Error al eliminar el Articulo con id "+id;
    			JOptionPane.showConfirmDialog(null, datos2, "Error", JOptionPane.WARNING_MESSAGE);
    			e.printStackTrace();
    			em.close();
    		}
        }
		
	}
		
	
	//===================================================================================
	//			MAIN
	//===================================================================================
		
	public static void main (String[] args) {

		//Desde aqui Luis
		Logger.getLogger("org.hibernate").setLevel(Level.SEVERE);
		
	
		consultar();
		//Desde aqui lo mio
		
		while (true){
			Action action = scanAction();
			if (action == Action.Salir) break;
			if (action == Action.Nuevo) nuevo();
			if (action == Action.Editar) editar();
			if (action == Action.Eliminar) eliminar();
			if (action == Action.Consultar) consultar();
			System.out.println();
		}
	}

}