-- phpMyAdmin SQL Dump
-- version 4.4.13.1
-- http://www.phpmyadmin.net
--
-- Servidor: localhost:3306
-- Tiempo de generación: 02-02-2016 a las 00:16:50
-- Versión del servidor: 5.6.26
-- Versión de PHP: 5.5.29

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de datos: `dbprueba`
--

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `articulo`
--

CREATE TABLE IF NOT EXISTS `articulo` (
  `id` bigint(20) NOT NULL,
  `nombre` varchar(50) COLLATE utf8_spanish_ci NOT NULL,
  `categoria` bigint(20) DEFAULT NULL,
  `precio` decimal(10,2) DEFAULT NULL
) ENGINE=InnoDB AUTO_INCREMENT=36 DEFAULT CHARSET=utf8 COLLATE=utf8_spanish_ci;

--
-- Volcado de datos para la tabla `articulo`
--

INSERT INTO `articulo` (`id`, `nombre`, `categoria`, `precio`) VALUES
(1, 'artículo 100000', 1, '10000.00'),
(2, 'artículo 2', 2, '2.00'),
(3, 'artículo 10', 2, '3.50'),
(4, 'artículo 4', 2, '0.00'),
(7, 'articulo nuevo', 1, '0.00'),
(9, 'articulo otro', 1, '0.00'),
(10, 'nuevo artículo 3300', 2, '2000.00'),
(11, 'nuevo 10', 2, '0.00'),
(12, 'nuevo 11', 2, '10.50'),
(13, 'nuevo 13', NULL, '11.30'),
(15, 'nuevo 15', NULL, '13.40'),
(16, 'nuevo 16', 1, '16.16'),
(18, 'nuevo', 2, '15.00'),
(19, 'nuevo 19', 1, '12.00'),
(20, 'java', 3, '12.23'),
(26, 'miaau', 3, '12.00'),
(28, 'joanmodificado', 2, '123.00'),
(29, 'articulo1000', 2, '12.00'),
(30, 'nuevo Thu Jan 28 08:37:13 CET 2016', NULL, NULL),
(31, 'nuevo Thu Jan 28 08:39:09 CET 2016', NULL, NULL),
(32, 'jojo', 3, '2.00'),
(34, 'asdsad', 2, '12.00'),
(35, 'pepepaco', 2, '123.00');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `categoria`
--

CREATE TABLE IF NOT EXISTS `categoria` (
  `id` bigint(20) NOT NULL,
  `nombre` varchar(50) COLLATE utf8_spanish_ci NOT NULL
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8 COLLATE=utf8_spanish_ci;

--
-- Volcado de datos para la tabla `categoria`
--

INSERT INTO `categoria` (`id`, `nombre`) VALUES
(1, 'categoría 1'),
(2, 'categoría 2'),
(3, 'categoría 3');

--
-- Índices para tablas volcadas
--

--
-- Indices de la tabla `articulo`
--
ALTER TABLE `articulo`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `nombre` (`nombre`),
  ADD KEY `categoria` (`categoria`);

--
-- Indices de la tabla `categoria`
--
ALTER TABLE `categoria`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `nombre` (`nombre`);

--
-- AUTO_INCREMENT de las tablas volcadas
--

--
-- AUTO_INCREMENT de la tabla `articulo`
--
ALTER TABLE `articulo`
  MODIFY `id` bigint(20) NOT NULL AUTO_INCREMENT,AUTO_INCREMENT=36;
--
-- AUTO_INCREMENT de la tabla `categoria`
--
ALTER TABLE `categoria`
  MODIFY `id` bigint(20) NOT NULL AUTO_INCREMENT,AUTO_INCREMENT=4;
--
-- Restricciones para tablas volcadas
--

--
-- Filtros para la tabla `articulo`
--
ALTER TABLE `articulo`
  ADD CONSTRAINT `articulocategoria` FOREIGN KEY (`categoria`) REFERENCES `categoria` (`id`);

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
