create table articulo(
    id bigint AUTO_INCREMENT PRIMARY KEY,
    nombre varchar(50) not null UNIQUE,
    categoria bigint,
    precio decimal(10,2)
    );

create table categoria(
    id bigint AUTO_INCREMENT PRIMARY KEY,
    nombre varchar(50) not null UNIQUE
    );
