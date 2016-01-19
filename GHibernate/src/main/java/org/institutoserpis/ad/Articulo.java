package org.institutoserpis.ad;

import javax.persistence.Entity;

@Entity
public class Articulo {
	
	private long id;
	private String nombre;
	private long categoria;
	private long precio;
	
	
	
	public Articulo(long id, String nombre, long categoria, long precio) {
		super();
		this.id = id;
		this.nombre = nombre;
		this.categoria = categoria;
		this.precio = precio;
	}
	
	public long getId() {
		return id;
	}
	public void setId(long id) {
		this.id = id;
	}
	public String getNombre() {
		return nombre;
	}
	public void setNombre(String nombre) {
		this.nombre = nombre;
	}
	public long getCategoria() {
		return categoria;
	}
	public void setCategoria(long categoria) {
		this.categoria = categoria;
	}
	public long getPrecio() {
		return precio;
	}
	public void setPrecio(long precio) {
		this.precio = precio;
	}
	
	

}
