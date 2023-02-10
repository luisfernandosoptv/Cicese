using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataBase_Estructura : MonoBehaviour
{
    public static string estructura_query= @"
BEGIN TRANSACTION;
CREATE TABLE IF NOT EXISTS `extras` (
	`extra_id`	INTEGER NOT NULL UNIQUE,
	`nombre`	varchar,
	PRIMARY KEY(`extra_id` AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS `puntajes` (
	`usuario_id`	int,
	`estado_id`	int,
	`municipio_id`	int,
	`puntos`	int,
	FOREIGN KEY(`estado_id`) REFERENCES `estados`(`estado_id`) on delete cascade,
	FOREIGN KEY(`municipio_id`) REFERENCES `municipios`(`municipio_id`) on delete cascade,
	FOREIGN KEY(`usuario_id`) REFERENCES `usuarios`(`usuario_id`) on delete cascade
);
CREATE TABLE IF NOT EXISTS `registro_juego` (
	`registro_juego_id`	INTEGER NOT NULL UNIQUE,
	`usuario_id`	int,
	`estado_id`	int,
	`municipio_id`	int,
	`puntos`	int,
	`vidas`	int,
	`energia`	int,
	`salud`	int,
	`fecha_inicio`	datetime,
	`fecha_termino`	datetime,
	FOREIGN KEY(`usuario_id`) REFERENCES `usuarios`(`usuario_id`) on delete cascade,
	FOREIGN KEY(`estado_id`) REFERENCES `estados`(`estado_id`) on delete cascade,
	FOREIGN KEY(`municipio_id`) REFERENCES `municipios`(`municipio_id`) on delete cascade,
	PRIMARY KEY(`registro_juego_id` AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS `registro_sesion` (
	`registro_sesion_id`	INTEGER NOT NULL UNIQUE,
	`sesion_id`	INTEGER,
	`usuario_id`	int,
	`fecha_inicio`	datetime,
	`fecha_termino`	datetime,
	FOREIGN KEY(`usuario_id`) REFERENCES `usuarios`(`usuario_id`) on delete cascade,
	PRIMARY KEY(`registro_sesion_id` AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS `extras_usuarios` (
	`usuario_id`	int,
	`extra_id`	int,
	`completado`	INTEGER,
	FOREIGN KEY(`usuario_id`) REFERENCES `usuarios`(`usuario_id`) on delete cascade,
	FOREIGN KEY(`extra_id`) REFERENCES `extras`(`extra_id`) on delete cascade
);
CREATE TABLE IF NOT EXISTS `derivados_elementos` (
	`cantidad`	REAL,
	`derivado_id`	int,
	`tipo_derivado`	varchar,
	`grupo`	varchar,
	`costo`	REAL,
	`elemento_id`	int,
	`tipo_elemento`	varchar
);
CREATE TABLE IF NOT EXISTS `enfermedades_usuarios` (
	`enfermedad_id`	INTEGER,
	`usuario_id`	INTEGER,
	`cantidad`	INTEGER,
	FOREIGN KEY(`enfermedad_id`) REFERENCES `enfermedades`(`enfermedad_id`) on delete cascade,
	FOREIGN KEY(`usuario_id`) REFERENCES `usuarios`(`usuario_id`) on delete cascade
);
CREATE TABLE IF NOT EXISTS `quiz_respuesta` (
	`respuesta_id`	INTEGER NOT NULL UNIQUE,
	`pregunta_id`	INTEGER,
	`texto`	varchar,
	`tipo_respuesta`	int,
	`ruta_imagen`	varchar,
	`nombre`	varchar,
	PRIMARY KEY(`respuesta_id`)
);
CREATE TABLE IF NOT EXISTS `inventario_usuarios` (
	`usuario_id`	int,
	`elemento_id`	int,
	`tipo`	varchar,
	`cantidad`	int,
	`resistencia`	float DEFAULT 100,
	`equipado`	INTEGER DEFAULT 0,
	FOREIGN KEY(`usuario_id`) REFERENCES `usuarios`(`usuario_id`) on delete cascade
);
CREATE TABLE IF NOT EXISTS `estados` (
	`estado_id`	INTEGER NOT NULL UNIQUE,
	`nombre_esp`	varchar,
	PRIMARY KEY(`estado_id` AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS `alimentos` (
	`alimento_id`	INTEGER NOT NULL UNIQUE,
	`categoria`	varchar,
	`nombre_singular_esp`	varchar,
	`nombre_plural_esp`	varchar,
	`nombre_singular_wix`	varchar,
	`nombre_plural_wix`	varchar,
	`puntaje`	int,
	`energia`	int,
	`ruta_imagen`	varchar,
	`recolectado`	varchar,
	`secuencia_audios`	varchar,
	PRIMARY KEY(`alimento_id` AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS `armas` (
	`arma_id`	INTEGER NOT NULL UNIQUE,
	`categoria`	TEXT,
	`nombre_singular_esp`	varchar,
	`nombre_plural_esp`	varchar,
	`nombre_singular_wix`	varchar,
	`nombre_plural_wix`	varchar,
	`nivel`	INTEGER,
	`puntaje`	INTEGER,
	`resistencia`	INTEGER,
	`daño`	REAL,
	`ruta_imagen`	varchar,
	`recolectado`	varchar,
	`secuencia_audios`	TEXT,
	PRIMARY KEY(`arma_id` AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS `objetos_especiales` (
	`objeto_especial_id`	INTEGER NOT NULL UNIQUE,
	`nombre_singular_esp`	varchar,
	`nombre_plural_esp`	varchar,
	`nombre_singular_wix`	varchar,
	`nombre_plural_wix`	varchar,
	`puntaje`	INTEGER,
	`ruta_imagen`	varchar,
	`recolectado`	varchar,
	`secuencia_audios`	TEXT,
	PRIMARY KEY(`objeto_especial_id` AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS `objetos_espirituales` (
	`objeto_espiritual_id`	INTEGER NOT NULL UNIQUE,
	`nombre_singular_esp`	TEXT,
	`nombre_plural_esp`	TEXT,
	`nombre_singular_wix`	TEXT,
	`nombre_plural_wix`	TEXT,
	`salud`	INTEGER,
	`energia`	INTEGER,
	`puntos`	INTEGER,
	`ruta_imagen`	TEXT,
	`recolectado`	TEXT,
	`secuencia_audios`	TEXT,
	PRIMARY KEY(`objeto_espiritual_id` AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS `quiz_pista` (
	`pista_id`	INTEGER NOT NULL UNIQUE,
	`pregunta_id`	INTEGER,
	`texto`	varchar,
	`costo`	int,
	`secuencia_audios`	TEXT,
	PRIMARY KEY(`pista_id`)
);
CREATE TABLE IF NOT EXISTS `vestimentas` (
	`vestimenta_id`	INTEGER NOT NULL UNIQUE,
	`categoria`	varchar,
	`nombre_singular_esp`	varchar,
	`nombre_plural_esp`	varchar,
	`nombre_singular_wix`	varchar,
	`nombre_plural_wix`	varchar,
	`nivel`	int,
	`puntaje`	INTEGER,
	`salud`	INTEGER,
	`velocidad`	float,
	`saltos`	float,
	`resistencia`	float,
	`ruta_imagen`	varchar,
	`secuencia_audios`	TEXT,
	PRIMARY KEY(`vestimenta_id` AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS `enemigos` (
	`enemigo_id`	INTEGER NOT NULL UNIQUE,
	`nombre_singular_esp`	TEXT,
	`vida`	INTEGER,
	`puntaje`	INTEGER,
	`daño_vida`	float,
	`daño_energia`	float,
	PRIMARY KEY(`enemigo_id` AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS `animales_enfermedades` (
	`animal_id`	INTEGER,
	`enfermedad_id`	INTEGER,
	FOREIGN KEY(`enfermedad_id`) REFERENCES `enfermedades`(`enfermedad_id`) on delete cascade,
	FOREIGN KEY(`animal_id`) REFERENCES `animales`(`animal_id`) on delete cascade
);
CREATE TABLE IF NOT EXISTS `curaciones_enfermedades` (
	`curacion_id`	INTEGER,
	`enfermedad_id`	INTEGER,
	FOREIGN KEY(`curacion_id`) REFERENCES `curaciones`(`curacion_id`) on delete cascade,
	FOREIGN KEY(`enfermedad_id`) REFERENCES `enfermedades`(`enfermedad_id`) on delete cascade
);
CREATE TABLE IF NOT EXISTS `animales` (
	`animal_id`	INTEGER NOT NULL UNIQUE,
	`categoria`	varchar,
	`nombre_singular_esp`	varchar,
	`nombre_plural_esp`	varchar,
	`nombre_singular_wix`	varchar,
	`nombre_plural_wix`	varchar,
	`vida`	INTEGER,
	`puntaje`	INTEGER,
	`daño_salud`	float,
	`daño_energia`	float,
	`da_carne`	INTEGER DEFAULT 1,
	`ruta_imagen`	TEXT,
	PRIMARY KEY(`animal_id` AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS `usuarios` (
	`usuario_id`	INTEGER NOT NULL UNIQUE,
	`usuario_server_id`	int,
	`usuario_local_id`	int,
	`contraseña`	int,
	`nombres`	varchar,
	`apellido_paterno`	varchar,
	`apellido_materno`	varchar,
	`fecha_nacimiento`	varchar,
	`sexo`	char DEFAULT 'M',
	`pais`	varchar DEFAULT 'México',
	`estado`	varchar DEFAULT 'Nayarit',
	`municipio`	varchar DEFAULT 'Tepic',
	`colonia`	varchar DEFAULT 'Centro',
	`calles`	varchar DEFAULT 'Centro',
	`escuela`	varchar DEFAULT 'Centro',
	`grado`	varchar DEFAULT 1,
	`grupo`	varchar DEFAULT 'A',
	`ruta_imagen`	varchar DEFAULT 'default_H',
	`activo`	INTEGER DEFAULT 1,
	PRIMARY KEY(`usuario_id` AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS `_Configuracion` (
	`configuracion_id`	INTEGER NOT NULL UNIQUE,
	`version`	INTEGER,
	`volumen_musica`	REAL DEFAULT 1,
	`volumen_efectos`	REAL DEFAULT 1,
	`volumen_habla`	REAL DEFAULT 1,
	PRIMARY KEY(`configuracion_id` AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS `enfermedades` (
	`enfermedad_id`	INTEGER NOT NULL UNIQUE,
	`categoria`	TEXT,
	`nombre_esp_singular`	varchar,
	`nombre_esp_plural`	varchar,
	`nombre_wix_singular`	varchar,
	`nombre_wix_plural`	varchar,
	`energia`	float,
	`salud`	float,
	`ruta_imagen`	varchar,
	`recolectado`	TEXT,
	`secuencia_audios`	varchar,
	PRIMARY KEY(`enfermedad_id` AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS `diarios_viajes_usuarios` (
	`diario_viaje_id`	INTEGER,
	`usuario_id`	INTEGER,
	`obtenido`	INTEGER,
	FOREIGN KEY(`usuario_id`) REFERENCES `usuarios`(`usuario_id`) on delete cascade,
	FOREIGN KEY(`diario_viaje_id`) REFERENCES `diarios_viajes`(`diario_viaje_id`) on delete cascade
);
CREATE TABLE IF NOT EXISTS `estrellas` (
	`estado_id`	INTEGER,
	`municipio_id`	INTEGER,
	`tipo`	TEXT,
	`usuario_id`	INTEGER,
	`cantidad`	INTEGER DEFAULT 0,
	FOREIGN KEY(`municipio_id`) REFERENCES `municipios`(`municipio_id`) on delete cascade,
	FOREIGN KEY(`usuario_id`) REFERENCES `usuarios`(`usuario_id`) on delete cascade,
	FOREIGN KEY(`estado_id`) REFERENCES `estados`(`estado_id`) on delete cascade
);
CREATE TABLE IF NOT EXISTS `estados_desbloqueados` (
	`usuario_id`	int,
	`estado_id`	int,
	`municipio_id`	int,
	`juego_desbloqueado`	INTEGER DEFAULT 0,
	`quiz_desbloqueado`	INTEGER DEFAULT 0,
	`juego_completado`	INTEGER DEFAULT 0,
	`quiz_completado`	INTEGER DEFAULT 0,
	FOREIGN KEY(`usuario_id`) REFERENCES `usuarios`(`usuario_id`) on delete cascade,
	FOREIGN KEY(`municipio_id`) REFERENCES `municipios`(`municipio_id`) on delete cascade,
	FOREIGN KEY(`estado_id`) REFERENCES `estados`(`estado_id`) on delete cascade
);
CREATE TABLE IF NOT EXISTS `conversaciones_usuarios` (
	`conversacion_grupo`	INTEGER,
	`usuario_id`	INTEGER,
	FOREIGN KEY(`usuario_id`) REFERENCES `usuarios`(`usuario_id`) on delete cascade
);
CREATE TABLE IF NOT EXISTS `temas_por_nivel` (
	`municipio_id`	INTEGER,
	`tema_id`	INTEGER,
	`descripcion`	varchar,
	`ruta_imagen`	varchar,
	`secuencia_audios`	TEXT,
	PRIMARY KEY(`tema_id` AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS `sitios_importantes` (
	`sitio_importante_id`	INTEGER NOT NULL UNIQUE,
	`titulo`	varchar,
	`descripcion`	varchar,
	`ruta_imagen`	varchar,
	`secuencia_audios`	TEXT,
	PRIMARY KEY(`sitio_importante_id` AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS `curaciones` (
	`curacion_id`	INTEGER NOT NULL UNIQUE,
	`categoria`	TEXT,
	`nombre_singular_esp`	varchar,
	`salud`	INTEGER,
	`energia`	INTEGER,
	`ruta_imagen`	varchar,
	PRIMARY KEY(`curacion_id` AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS `explicacion_inicio` (
	`estado_id`	INTEGER,
	`municipio_id`	INTEGER,
	`nombre_esp`	varchar,
	`descripcion_alimentos`	varchar,
	`descripcion_animales`	varchar,
	`secuencia_audio`	TEXT,
	`ruta_imagen1`	varchar,
	`ruta_imagen2`	varchar,
	`ruta_imagen3`	varchar,
	`ruta_imagenComida1`	varchar,
	`ruta_imagenComida2`	varchar,
	`ruta_imagenComida3`	varchar
);
CREATE TABLE IF NOT EXISTS `municipios` (
	`estado_id`	INTEGER,
	`municipio_id`	INTEGER NOT NULL UNIQUE,
	`nombre_esp`	varchar,
	`descripcion_esp`	varchar,
	`secuencia_audios`	TEXT,
	FOREIGN KEY(`estado_id`) REFERENCES `estados`(`estado_id`) on delete cascade,
	PRIMARY KEY(`municipio_id` AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS `plantas_medicinales` (
	`planta_medicinal_id`	INTEGER NOT NULL UNIQUE,
	`nombre_singular_esp`	TEXT,
	`nombre_plural_esp`	TEXT,
	`nombre_singular_wix`	TEXT,
	`nombre_plural_wix`	TEXT,
	`ruta_imagen`	TEXT,
	`recolectado`	TEXT,
	`secuencia_audios`	TEXT,
	PRIMARY KEY(`planta_medicinal_id` AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS `conversaciones` (
	`conversacion_id`	INTEGER NOT NULL UNIQUE,
	`municipio`	TEXT,
	`categoria`	TEXT,
	`grupo`	INTEGER,
	`genero`	TEXT,
	`nombre_quien_habla`	TEXT,
	`tema`	TEXT,
	`dialogo`	TEXT,
	`botones`	TEXT,
	`ruta_imagen_quien_habla`	TEXT,
	`secuencia_audios`	TEXT,
	PRIMARY KEY(`conversacion_id` AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS `textos` (
	`texto_id`	INTEGER,
	`lugar`	varchar,
	`contenido_esp`	varchar
);
CREATE TABLE IF NOT EXISTS `objetivos` (
	`objetivo_id`	INTEGER NOT NULL UNIQUE,
	`estado_id`	INTEGER,
	`municipio_id`	INTEGER,
	`municipio`	TEXT,
	`tipo`	TEXT,
	`elemento_id`	INTEGER,
	`cantidad`	INTEGER,
	`accion`	TEXT,
	`texto`	TEXT,
	FOREIGN KEY(`municipio_id`) REFERENCES `municipios`(`municipio_id`) on delete cascade,
	FOREIGN KEY(`estado_id`) REFERENCES `estados`(`estado_id`) on delete cascade,
	PRIMARY KEY(`objetivo_id` AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS `quiz_pregunta` (
	`pregunta_id`	INTEGER NOT NULL UNIQUE,
	`municipio_id`	varchar,
	`titulo`	varchar,
	`texto_pregunta`	varchar,
	`puntos_primera`	int,
	`puntos_segunda`	int,
	`tipo_de_pregunta`	int,
	`dificultad`	INTEGER,
	`ruta_imagen`	varchar,
	`secuencia_audios`	TEXT,
	PRIMARY KEY(`pregunta_id` AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS `diarios_viajes` (
	`diario_viaje_id`	INTEGER NOT NULL UNIQUE,
	`orden_aparicion`	INTEGER,
	`elemento_id`	INTEGER,
	`accion`	TEXT,
	`tipo_elemento`	TEXT,
	`tipo`	TEXT,
	`nivel`	TEXT,
	`elemento_singular_esp`	TEXT,
	`elemento_plural_esp`	TEXT,
	`elemento_singular_wix`	TEXT,
	`elemento_plural_wix`	TEXT,
	`texto`	TEXT,
	`ruta_imagen`	TEXT,
	`secuencia_audios`	TEXT,
	PRIMARY KEY(`diario_viaje_id` AUTOINCREMENT)
);
INSERT INTO `extras` VALUES (1,'Primera vez historia');
INSERT INTO `extras` VALUES (2,'Primera vez tutorial');
INSERT INTO `extras` VALUES (3,'Primera vez en tienda de intercambios');
INSERT INTO `extras` VALUES (4,'Primera vez en tienda de vestimenta');
INSERT INTO `extras` VALUES (5,'Primera vez en tienda de curaciones');
INSERT INTO `extras` VALUES (6,'Primera vez en tienda de armas');
INSERT INTO `extras` VALUES (7,'Primera vez en tienda de alimentación');
INSERT INTO `extras` VALUES (8,'Primera vez en tienda de artesanias');
INSERT INTO `extras` VALUES (9,'Primera vez en tienda de materiales');
INSERT INTO `extras` VALUES (10,'Primera vez instalado');
INSERT INTO `extras` VALUES (11,'Primera vez explicación quiz');
INSERT INTO `extras` VALUES (12,'Segunda vez explicación quiz');
INSERT INTO `extras` VALUES (13,'Primera vez mapa');
INSERT INTO `extras` VALUES (14,'Primera vez Xapawiyemeta');
INSERT INTO `extras` VALUES (15,'Primera vez Hauxamanaka');
INSERT INTO `extras` VALUES (16,'Primera vez Wirikuta');
INSERT INTO `puntajes` VALUES (1,1,1,0);
INSERT INTO `puntajes` VALUES (1,1,2,0);
INSERT INTO `puntajes` VALUES (1,1,3,0);
INSERT INTO `puntajes` VALUES (1,1,4,0);
INSERT INTO `puntajes` VALUES (1,1,5,0);
INSERT INTO `puntajes` VALUES (1,1,6,0);
INSERT INTO `puntajes` VALUES (1,1,7,0);
INSERT INTO `puntajes` VALUES (1,2,8,0);
INSERT INTO `puntajes` VALUES (1,2,9,0);
INSERT INTO `puntajes` VALUES (1,2,10,0);
INSERT INTO `puntajes` VALUES (1,2,11,0);
INSERT INTO `puntajes` VALUES (1,3,12,0);
INSERT INTO `puntajes` VALUES (1,3,13,0);
INSERT INTO `puntajes` VALUES (1,3,14,0);
INSERT INTO `puntajes` VALUES (1,3,15,0);
INSERT INTO `puntajes` VALUES (1,3,16,0);
INSERT INTO `puntajes` VALUES (1,4,17,0);
INSERT INTO `puntajes` VALUES (1,4,18,0);
INSERT INTO `puntajes` VALUES (1,4,19,0);
INSERT INTO `puntajes` VALUES (1,4,20,0);
INSERT INTO `puntajes` VALUES (1,4,21,0);
INSERT INTO `puntajes` VALUES (1,5,22,0);
INSERT INTO `puntajes` VALUES (1,5,23,0);
INSERT INTO `puntajes` VALUES (1,5,24,0);
INSERT INTO `puntajes` VALUES (1,5,25,0);
INSERT INTO `puntajes` VALUES (1,5,26,0);
INSERT INTO `puntajes` VALUES (1,6,27,0);
INSERT INTO `puntajes` VALUES (1,6,28,0);
INSERT INTO `puntajes` VALUES (1,6,29,0);
INSERT INTO `puntajes` VALUES (1,6,30,0);
INSERT INTO `registro_sesion` VALUES (6857,-1,1,'2022-10-16 23:51:24','2022-10-16 23:51:24');
INSERT INTO `extras_usuarios` VALUES (1,1,0);
INSERT INTO `extras_usuarios` VALUES (1,2,0);
INSERT INTO `extras_usuarios` VALUES (1,3,0);
INSERT INTO `extras_usuarios` VALUES (1,4,0);
INSERT INTO `extras_usuarios` VALUES (1,5,0);
INSERT INTO `extras_usuarios` VALUES (1,6,0);
INSERT INTO `extras_usuarios` VALUES (1,7,0);
INSERT INTO `extras_usuarios` VALUES (1,8,0);
INSERT INTO `extras_usuarios` VALUES (1,9,0);
INSERT INTO `extras_usuarios` VALUES (1,10,0);
INSERT INTO `extras_usuarios` VALUES (1,11,0);
INSERT INTO `extras_usuarios` VALUES (1,12,0);
INSERT INTO `extras_usuarios` VALUES (1,13,0);
INSERT INTO `extras_usuarios` VALUES (1,14,0);
INSERT INTO `extras_usuarios` VALUES (1,15,0);
INSERT INTO `extras_usuarios` VALUES (1,16,0);
INSERT INTO `derivados_elementos` VALUES (1.0,1,'ALIMENTO','G_1',1.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,2,'ALIMENTO','G_1',1.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,3,'ALIMENTO','G_1',1.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,4,'ALIMENTO','G_1',1.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,5,'ALIMENTO','G_1',1.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,6,'ALIMENTO','G_1',1.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,7,'ALIMENTO','G_1',1.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,8,'ALIMENTO','G_1',1.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,9,'ALIMENTO','G_1',1.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,10,'ALIMENTO','G_1',1.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,11,'ALIMENTO','G_1',1.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,12,'ALIMENTO','G_1',1.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,13,'ALIMENTO','G_1',1.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,14,'ALIMENTO','G_1',1.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,15,'ALIMENTO','G_1',1.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,16,'ALIMENTO','G_1',1.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,17,'ALIMENTO','G_1',1.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,18,'ALIMENTO','G_1',1.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,19,'ALIMENTO','G_1',1.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,20,'ALIMENTO','G_1',1.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,21,'ALIMENTO','G_1',1.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,22,'ALIMENTO','G_1',1.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,23,'ALIMENTO','G_1',1.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,24,'ALIMENTO','G_1',1.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,25,'ALIMENTO','G_1',1.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,26,'ALIMENTO','G_1',1.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,27,'ALIMENTO','G_1',1.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,28,'ALIMENTO','G_1',1.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,29,'ALIMENTO','G_1',1.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,30,'ALIMENTO','G_1',1.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,31,'ALIMENTO','G_1',1.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,32,'ALIMENTO','G_1',1.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,33,'ALIMENTO','G_1',1.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,34,'ALIMENTO','G_1',1.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,35,'ALIMENTO','G_1',1.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,36,'ALIMENTO','G_1',1.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,37,'ALIMENTO','G_1',1.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,38,'ALIMENTO','G_1',1.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,39,'ALIMENTO','G_1',8.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,40,'ALIMENTO','G_1',2.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,41,'ALIMENTO','G_1',2.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,42,'ALIMENTO','G_1',5.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,43,'ALIMENTO','G_1',5.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,44,'ALIMENTO','G_1',8.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,45,'ALIMENTO','G_1',3.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,46,'ALIMENTO','G_1',7.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,47,'ALIMENTO','G_1',3.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,48,'ALIMENTO','G_1',4.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,49,'ALIMENTO','G_1',5.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,50,'ALIMENTO','G_1',8.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,51,'ALIMENTO','G_1',6.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,52,'ALIMENTO','G_1',3.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,53,'ALIMENTO','G_1',3.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,54,'ALIMENTO','G_1',5.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,55,'ALIMENTO','G_1',6.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,56,'ALIMENTO','G_1',4.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,57,'ALIMENTO','G_1',15.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,58,'ALIMENTO','G_1',15.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,59,'ALIMENTO','G_1',13.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,60,'ALIMENTO','G_1',10.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,61,'ALIMENTO','G_1',15.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,62,'ALIMENTO','G_1',15.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,63,'ALIMENTO','G_1',13.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,64,'ALIMENTO','G_1',15.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,65,'ALIMENTO','G_1',15.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,66,'ALIMENTO','G_1',13.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,67,'ALIMENTO','G_1',15.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,68,'ALIMENTO','G_1',15.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,69,'ALIMENTO','G_1',10.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,70,'ALIMENTO','G_1',8.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,71,'ALIMENTO','G_1',15.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,72,'ALIMENTO','G_1',8.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,73,'ALIMENTO','G_1',13.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,74,'ALIMENTO','G_1',15.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,75,'ALIMENTO','G_1',15.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,76,'ALIMENTO','G_1',15.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,77,'ALIMENTO','G_1',10.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,78,'ALIMENTO','G_1',15.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,79,'ALIMENTO','G_1',10.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,80,'ALIMENTO','G_1',13.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,81,'ALIMENTO','G_1',15.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,82,'ALIMENTO','G_1',15.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,83,'ALIMENTO','G_1',13.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,84,'ALIMENTO','G_1',8.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,85,'ALIMENTO','G_1',10.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,86,'ALIMENTO','G_1',15.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,87,'ALIMENTO','G_1',15.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,88,'ALIMENTO','G_1',13.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,89,'ALIMENTO','G_1',3.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,90,'ALIMENTO','G_1',2.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,91,'ALIMENTO','G_1',3.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,92,'ALIMENTO','G_1',3.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,93,'ALIMENTO','G_1',2.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,94,'ALIMENTO','G_1',3.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,95,'ALIMENTO','G_1',3.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,96,'ALIMENTO','G_1',3.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,97,'ALIMENTO','G_1',3.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,98,'ALIMENTO','G_1',2.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,99,'ALIMENTO','G_1',3.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,100,'ALIMENTO','G_1',3.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,101,'ALIMENTO','G_1',2.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,102,'ALIMENTO','G_1',3.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,103,'ALIMENTO','G_1',3.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,104,'ALIMENTO','G_1',3.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,1,'ARMA','G_1',50.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,2,'ARMA','G_1',25.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,3,'ARMA','G_1',20.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,4,'ARMA','G_1',50.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,5,'ARMA','G_1',40.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,6,'ARMA','G_1',20.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,7,'ARMA','G_1',1.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,8,'ARMA','G_1',60.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,9,'ARMA','G_1',35.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,10,'ARMA','G_1',30.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,11,'ARMA','G_1',60.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,12,'ARMA','G_1',50.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,13,'ARMA','G_1',30.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,14,'ARMA','G_1',70.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,15,'ARMA','G_1',45.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,16,'ARMA','G_1',40.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,17,'ARMA','G_1',70.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,18,'ARMA','G_1',60.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,19,'ARMA','G_1',40.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,1,'CURACION','G_1',5.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,2,'CURACION','G_1',5.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,3,'CURACION','G_1',10.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,4,'CURACION','G_1',25.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,5,'CURACION','G_1',13.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,6,'CURACION','G_1',10.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,7,'CURACION','G_1',5.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,8,'CURACION','G_1',13.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,9,'CURACION','G_1',5.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,10,'CURACION','G_1',25.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,11,'CURACION','G_1',13.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,12,'CURACION','G_1',5.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,1,'MATERIAL','G_1',25.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,2,'MATERIAL','G_1',25.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,3,'MATERIAL','G_1',25.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,4,'MATERIAL','G_1',65.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,5,'MATERIAL','G_1',25.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,6,'MATERIAL','G_1',25.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,7,'MATERIAL','G_1',25.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,1,'OBJETO_ESPECIAL','G_1',125.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,2,'OBJETO_ESPECIAL','G_1',125.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,3,'OBJETO_ESPECIAL','G_1',125.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,4,'OBJETO_ESPECIAL','G_1',125.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,5,'OBJETO_ESPECIAL','G_1',125.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,6,'OBJETO_ESPECIAL','G_1',125.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,7,'OBJETO_ESPECIAL','G_1',125.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,8,'OBJETO_ESPECIAL','G_1',125.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,9,'OBJETO_ESPECIAL','G_1',125.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,10,'OBJETO_ESPECIAL','G_1',125.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,11,'OBJETO_ESPECIAL','G_1',125.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,12,'OBJETO_ESPECIAL','G_1',125.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,13,'OBJETO_ESPECIAL','G_1',125.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,14,'OBJETO_ESPECIAL','G_1',65.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,1,'OBJETO_ESPIRITUAL','G_1',225.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,2,'OBJETO_ESPIRITUAL','G_1',225.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,3,'OBJETO_ESPIRITUAL','G_1',225.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,4,'OBJETO_ESPIRITUAL','G_1',225.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,5,'OBJETO_ESPIRITUAL','G_1',225.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,1,'PLANTA_MEDICINAL','G_1',50.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,2,'PLANTA_MEDICINAL','G_1',50.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,3,'PLANTA_MEDICINAL','G_1',50.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,4,'PLANTA_MEDICINAL','G_1',50.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,5,'PLANTA_MEDICINAL','G_1',50.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,6,'PLANTA_MEDICINAL','G_1',50.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,7,'PLANTA_MEDICINAL','G_1',50.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,8,'PLANTA_MEDICINAL','G_1',50.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,9,'PLANTA_MEDICINAL','G_1',50.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,10,'PLANTA_MEDICINAL','G_1',50.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,1,'VESTIMENTA','G_1',25.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,2,'VESTIMENTA','G_1',50.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,3,'VESTIMENTA','G_1',65.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,4,'VESTIMENTA','G_1',25.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,5,'VESTIMENTA','G_1',30.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,6,'VESTIMENTA','G_1',40.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,7,'VESTIMENTA','G_1',30.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,8,'VESTIMENTA','G_1',40.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,9,'VESTIMENTA','G_1',65.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,10,'VESTIMENTA','G_1',25.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,11,'VESTIMENTA','G_1',25.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,12,'VESTIMENTA','G_1',40.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,13,'VESTIMENTA','G_1',50.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,14,'VESTIMENTA','G_1',50.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,15,'VESTIMENTA','G_1',40.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,16,'VESTIMENTA','G_1',60.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,17,'VESTIMENTA','G_1',50.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,18,'VESTIMENTA','G_1',60.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,19,'VESTIMENTA','G_1',70.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,20,'VESTIMENTA','G_1',0.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,105,'ALIMENTO','G_1',11.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,106,'ALIMENTO','G_1',11.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,107,'ALIMENTO','G_1',11.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,108,'ALIMENTO','G_1',11.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,109,'ALIMENTO','G_1',11.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,110,'ALIMENTO','G_1',11.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,111,'ALIMENTO','G_1',11.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,112,'ALIMENTO','G_1',11.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,113,'ALIMENTO','G_1',11.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,114,'ALIMENTO','G_1',11.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,115,'ALIMENTO','G_1',11.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,116,'ALIMENTO','G_1',5.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,117,'ALIMENTO','G_1',5.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,118,'ALIMENTO','G_1',5.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (10.0,20,'ARMA','G_1',1.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (10.0,21,'ARMA','G_1',1.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (10.0,22,'ARMA','G_1',1.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,13,'CURACION','G_1',5.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,14,'CURACION','G_1',5.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,15,'CURACION','G_1',25.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,16,'CURACION','G_1',20.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,17,'CURACION','G_1',10.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,18,'CURACION','G_1',15.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,19,'CURACION','G_1',25.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,20,'CURACION','G_1',225.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,21,'CURACION','G_1',225.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,8,'MATERIAL','G_1',125.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,11,'PLANTA_MEDICINAL','G_1',50.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,12,'PLANTA_MEDICINAL','G_1',50.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,13,'PLANTA_MEDICINAL','G_1',50.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,14,'PLANTA_MEDICINAL','G_1',50.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,15,'PLANTA_MEDICINAL','G_1',50.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,16,'PLANTA_MEDICINAL','G_1',50.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,17,'PLANTA_MEDICINAL','G_1',50.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,18,'PLANTA_MEDICINAL','G_1',50.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,19,'PLANTA_MEDICINAL','G_1',50.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,119,'ALIMENTO','G_1',5.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,23,'ARMA','G_1',80.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,24,'ARMA','G_1',90.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,25,'ARMA','G_1',100.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,26,'ARMA','G_1',55.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,27,'ARMA','G_1',65.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,28,'ARMA','G_1',75.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,29,'ARMA','G_1',50.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,30,'ARMA','G_1',60.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,31,'ARMA','G_1',70.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,32,'ARMA','G_1',80.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,33,'ARMA','G_1',90.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,34,'ARMA','G_1',100.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,35,'ARMA','G_1',70.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,36,'ARMA','G_1',80.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,37,'ARMA','G_1',90.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,38,'ARMA','G_1',50.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,39,'ARMA','G_1',60.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,40,'ARMA','G_1',70.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,22,'CURACION','G_1',45.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,23,'CURACION','G_1',65.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,24,'CURACION','G_1',90.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,25,'CURACION','G_1',5.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,120,'ALIMENTO','G_1',5.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,121,'ALIMENTO','G_1',5.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,122,'ALIMENTO','G_1',5.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,123,'ALIMENTO','G_1',5.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,21,'VESTIMENTA','G_1',80.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,22,'VESTIMENTA','G_1',90.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,23,'VESTIMENTA','G_1',100.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,24,'VESTIMENTA','G_1',70.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,25,'VESTIMENTA','G_1',80.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,26,'VESTIMENTA','G_1',90.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,27,'VESTIMENTA','G_1',60.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,28,'VESTIMENTA','G_1',70.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `derivados_elementos` VALUES (1.0,29,'VESTIMENTA','G_1',80.0,-1,'ALIMENTO-MAIZ');
INSERT INTO `quiz_respuesta` VALUES (1,1,'<i>Ikɨri</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (2,1,'<i>Múme</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (3,1,'<i>Haxi</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (4,1,'<i>Ikú</i>',1,'','');
INSERT INTO `quiz_respuesta` VALUES (5,2,'<i>Xutsíte</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (6,2,'<i>Kukuríte</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (7,2,'<i>Iku’ute</i>',1,'','');
INSERT INTO `quiz_respuesta` VALUES (8,2,'<i>Múmete</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (9,3,'Chile',0,'Images/Wixarika/Alimentos/Chile','');
INSERT INTO `quiz_respuesta` VALUES (10,3,'Maíz',1,'Images/Wixarika/Alimentos/Maíz','');
INSERT INTO `quiz_respuesta` VALUES (11,3,'Nopal',0,'Images/Wixarika/Alimentos/Nopal 1','');
INSERT INTO `quiz_respuesta` VALUES (12,3,'Frijoles',0,'Images/Wixarika/Alimentos/Frijol','');
INSERT INTO `quiz_respuesta` VALUES (13,4,'Maíz',1,'','');
INSERT INTO `quiz_respuesta` VALUES (14,4,'Nopal',0,'','');
INSERT INTO `quiz_respuesta` VALUES (15,4,'Frijol',0,'','');
INSERT INTO `quiz_respuesta` VALUES (16,4,'Chile',0,'','');
INSERT INTO `quiz_respuesta` VALUES (17,5,'<i>Múme</i>',1,'','');
INSERT INTO `quiz_respuesta` VALUES (18,5,'<i>Kukúri</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (19,5,'<i>Ikú</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (20,5,'<i>Xútsi</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (21,6,'<i>Haxite</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (22,6,'<i>Múmete</i>',1,'','');
INSERT INTO `quiz_respuesta` VALUES (23,6,'<i>Iku’ute</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (24,6,'<i>Ikɨríte</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (25,7,'Nopales',0,'Images/Wixarika/Alimentos/Nopal','');
INSERT INTO `quiz_respuesta` VALUES (26,7,'Maíces',0,'Images/Wixarika/Alimentos/Maíz','');
INSERT INTO `quiz_respuesta` VALUES (27,7,'Frijoles',1,'Images/Wixarika/Alimentos/Arrayanes','');
INSERT INTO `quiz_respuesta` VALUES (28,7,'Chiles',0,'Images/Wixarika/Alimentos/Frijol','');
INSERT INTO `quiz_respuesta` VALUES (29,8,'<i>Retsi</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (30,8,'<i>Nawá</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (31,8,'<i>Tsinari</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (32,8,'<i>Ha’a</i>',1,'','');
INSERT INTO `quiz_respuesta` VALUES (33,9,'Tejuino',0,'Images/Wixarika/Alimentos/Tejuino','');
INSERT INTO `quiz_respuesta` VALUES (34,9,'Atole',0,'Images/Wixarika/Alimentos/Atole','');
INSERT INTO `quiz_respuesta` VALUES (35,9,'Leche',0,'Images/Wixarika/Alimentos/Leche','');
INSERT INTO `quiz_respuesta` VALUES (36,9,'Agua',1,'Images/Wixarika/Alimentos/Agua','');
INSERT INTO `quiz_respuesta` VALUES (37,10,'<i>Kɨxaɨ</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (38,10,'<i>Tétsu</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (39,10,'<i>Pa’apa</i>',1,'','');
INSERT INTO `quiz_respuesta` VALUES (40,10,'<i>Taku</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (41,11,'Taco',0,'Images/Wixarika/Alimentos/Taco de frijol','');
INSERT INTO `quiz_respuesta` VALUES (42,11,'Tortilla',1,'Images/Wixarika/Alimentos/Tortilla','');
INSERT INTO `quiz_respuesta` VALUES (43,11,'Tamal',0,'Images/Wixarika/Alimentos/Tamal de cerdo','');
INSERT INTO `quiz_respuesta` VALUES (44,11,'Tostada',0,'Images/Wixarika/Alimentos/Tostada','');
INSERT INTO `quiz_respuesta` VALUES (45,12,'<i>Tuixuyeutanaka</i>',1,'','');
INSERT INTO `quiz_respuesta` VALUES (46,12,'<i>Wakana</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (47,12,'<i>Tuixu</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (48,12,'<i>Ke’etsé</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (49,13,'<i>Maxa</i>',1,'','');
INSERT INTO `quiz_respuesta` VALUES (50,13,'<i>Máye</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (51,13,'<i>Tuixuyeutanaka</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (52,13,'<i>Tuuká</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (53,14,'<i>Tuixuyeutanaka</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (54,14,'<i>Máye</i>',1,'','');
INSERT INTO `quiz_respuesta` VALUES (55,14,'<i>Tuuká</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (56,14,'<i>Maxa</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (57,15,'<i>Maxa</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (58,15,'<i>Máye</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (59,15,'<i>Tuuká</i>',1,'','');
INSERT INTO `quiz_respuesta` VALUES (60,15,'<i>Tuixuyeutanaka</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (61,16,'<i>Nawaxa</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (62,16,'<i>Tɨɨpi</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (63,16,'<i>Kutsira</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (64,16,'<i>Kɨyé</i>',1,'','');
INSERT INTO `quiz_respuesta` VALUES (65,17,'La playa',0,'','');
INSERT INTO `quiz_respuesta` VALUES (66,17,'Del Nayar',0,'','');
INSERT INTO `quiz_respuesta` VALUES (67,17,'El bosque',0,'','');
INSERT INTO `quiz_respuesta` VALUES (68,17,'Mesa del Nayar',1,'','');
INSERT INTO `quiz_respuesta` VALUES (69,18,'El bosque',0,'','');
INSERT INTO `quiz_respuesta` VALUES (70,18,'La playa',0,'','');
INSERT INTO `quiz_respuesta` VALUES (71,18,'Santa Teresa',1,'','');
INSERT INTO `quiz_respuesta` VALUES (72,18,'Del Nayar',0,'','');
INSERT INTO `quiz_respuesta` VALUES (73,19,'<i>Tupiri</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (74,19,'<i>Mara’kame</i>',1,'','');
INSERT INTO `quiz_respuesta` VALUES (75,19,'<i>Kawiteru</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (76,19,'<i>Tatuwani</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (77,20,'<i>Mara’kame</i>',1,'','');
INSERT INTO `quiz_respuesta` VALUES (78,20,'<i>Tiɨkitame</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (79,20,'<i>Titsatsaweme</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (80,20,'<i>Watame</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (81,21,'<i>Muka´etsa</i>',1,'','');
INSERT INTO `quiz_respuesta` VALUES (82,21,'<i>Tiyuitɨwame</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (83,21,'<i>Tewakame</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (84,21,'<i>Ketsɨtame</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (85,22,'Agua',0,'','');
INSERT INTO `quiz_respuesta` VALUES (86,22,'Coamil',1,'','');
INSERT INTO `quiz_respuesta` VALUES (87,22,'Arena',0,'','');
INSERT INTO `quiz_respuesta` VALUES (88,22,'Composta',0,'','');
INSERT INTO `quiz_respuesta` VALUES (89,23,'Nopales y hongos',0,'','');
INSERT INTO `quiz_respuesta` VALUES (90,23,'Flores de colores',0,'','');
INSERT INTO `quiz_respuesta` VALUES (91,23,'Maíz, frijoles, calabazas, chiles y flores de cempasúchil',1,'','');
INSERT INTO `quiz_respuesta` VALUES (92,23,'Árboles y arbustos',0,'','');
INSERT INTO `quiz_respuesta` VALUES (93,24,'<i>Múme</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (94,24,'<i>Ikú yɨwi</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (95,24,'<i>Ikɨri</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (96,24,'<i>Ikú taxawime</i>',1,'','');
INSERT INTO `quiz_respuesta` VALUES (97,25,'Maíz morado',0,'','');
INSERT INTO `quiz_respuesta` VALUES (98,25,'Maíz de colores',0,'','');
INSERT INTO `quiz_respuesta` VALUES (99,25,'Maíz amarillo',1,'','');
INSERT INTO `quiz_respuesta` VALUES (100,25,'Maíz negro',0,'','');
INSERT INTO `quiz_respuesta` VALUES (101,26,'<i>Kukúri</i>',1,'','');
INSERT INTO `quiz_respuesta` VALUES (102,26,'<i>Ikú</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (103,26,'<i>Múme</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (104,26,'<i>Ikɨri</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (105,27,'<i>Múmete</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (106,27,'<i>Kukuríte</i>',1,'','');
INSERT INTO `quiz_respuesta` VALUES (107,27,'<i>Ikɨríte</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (108,27,'<i>Iku’ute</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (109,28,'Frijol',0,'Images/Wixarika/Alimentos/Frijol','');
INSERT INTO `quiz_respuesta` VALUES (110,28,'Maíz',0,'Images/Wixarika/Alimentos/Maíz','');
INSERT INTO `quiz_respuesta` VALUES (111,28,'Kukúri',1,'Images/Wixarika/Alimentos/Chile','');
INSERT INTO `quiz_respuesta` VALUES (112,28,'Elote',0,'Images/Wixarika/Alimentos/Elote (1)','');
INSERT INTO `quiz_respuesta` VALUES (113,29,'Pipián de <i>tuixuyeutanaka</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (114,29,'<i>Tuixuyeutanaka itsari</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (115,29,'Albóndiga de <i>tuixuyeutanaka</i>',1,'','');
INSERT INTO `quiz_respuesta` VALUES (116,29,'<i>Tuixuyeutanaka ikwaiyári</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (117,30,'<i>Xupaxi</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (118,30,'<i>Tsuirá</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (119,30,'<i>Pexuri</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (120,30,'<i>Kexiu</i>',1,'','');
INSERT INTO `quiz_respuesta` VALUES (121,31,'<i>Tétsute</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (122,31,'<i>Xupaxite</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (123,31,'<i>Kexiute</i>',1,'','');
INSERT INTO `quiz_respuesta` VALUES (124,31,'<i>Tsuiráte</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (125,32,'<i>Kúu</i>',1,'','');
INSERT INTO `quiz_respuesta` VALUES (126,32,'<i>Imukwi</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (127,32,'<i>Ke’etsé</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (128,32,'<i>Ɨkwi</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (129,33,'<i>Orekanu</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (130,33,'<i>Wáxa</i>',1,'','');
INSERT INTO `quiz_respuesta` VALUES (131,33,'<i>Mantsaniya</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (132,33,'<i>Eɨkariti</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (133,34,'<i>Kɨyé</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (134,34,'<i>Tɨɨpi</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (135,34,'<i>Kutsira</i>',1,'','');
INSERT INTO `quiz_respuesta` VALUES (136,34,'<i>Ha’tsa</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (137,35,'Del Nayar',0,'','');
INSERT INTO `quiz_respuesta` VALUES (138,35,'Las iglesias',0,'','');
INSERT INTO `quiz_respuesta` VALUES (139,35,'La Yesca',0,'','');
INSERT INTO `quiz_respuesta` VALUES (140,35,'La laguna de Guadalupe Ocotán',1,'','');
INSERT INTO `quiz_respuesta` VALUES (141,36,'<i>Watame</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (142,36,'<i>Muka’etsa</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (143,36,'<i>Tiweweiyame</i>',1,'','');
INSERT INTO `quiz_respuesta` VALUES (144,36,'<i>Itsɨkame</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (145,37,'Para realizar rituales sagrados',1,'','');
INSERT INTO `quiz_respuesta` VALUES (146,37,'Para obtener pieles',0,'','');
INSERT INTO `quiz_respuesta` VALUES (147,37,'Para alimentarse',0,'','');
INSERT INTO `quiz_respuesta` VALUES (148,37,'Para el comercio',0,'','');
INSERT INTO `quiz_respuesta` VALUES (149,38,'El camino del corazón',1,'','');
INSERT INTO `quiz_respuesta` VALUES (150,38,'Los lazos del corazón',0,'','');
INSERT INTO `quiz_respuesta` VALUES (151,38,'El camino <i>Wixárika',0,'','');
INSERT INTO `quiz_respuesta` VALUES (152,38,'El corazón del pueblo',0,'','');
INSERT INTO `quiz_respuesta` VALUES (153,39,'Por las deidades.',0,'','');
INSERT INTO `quiz_respuesta` VALUES (154,39,'A través del costumbre',1,'','');
INSERT INTO `quiz_respuesta` VALUES (155,39,'Por las tradiciones',0,'','');
INSERT INTO `quiz_respuesta` VALUES (156,39,'A través del corazón',0,'','');
INSERT INTO `quiz_respuesta` VALUES (157,40,'<i>Kwarɨpa</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (158,40,'<i>Yɨɨna</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (159,40,'<i>Uwá</i>',1,'','');
INSERT INTO `quiz_respuesta` VALUES (160,40,'<i>Muxu’uri</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (161,41,'Caña',1,'Images/Wixarika/Alimentos/Caña 1x','');
INSERT INTO `quiz_respuesta` VALUES (162,41,'Guamúchil',0,'Images/Wixarika/Alimentos/Guamúchil','');
INSERT INTO `quiz_respuesta` VALUES (163,41,'Ciruela',0,'Images/Wixarika/Alimentos/Ciruela española','');
INSERT INTO `quiz_respuesta` VALUES (164,41,'Tuna',0,'Images/Wixarika/Alimentos/Tuna','');
INSERT INTO `quiz_respuesta` VALUES (165,42,'<i>Xiete</i>',1,'','');
INSERT INTO `quiz_respuesta` VALUES (166,42,'<i>Pexúri</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (167,42,'<i>Ruritse</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (168,42,'<i>Tsakaka</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (169,43,'Dulce',0,'Images/Wixarika/Alimentos/Dulce','');
INSERT INTO `quiz_respuesta` VALUES (170,43,'Piloncillo',0,'Images/Wixarika/Alimentos/Piloncillo','');
INSERT INTO `quiz_respuesta` VALUES (171,43,'Pinole',0,'Images/Wixarika/Alimentos/Pinole 1','');
INSERT INTO `quiz_respuesta` VALUES (172,43,'Miel',1,'Images/Wixarika/Alimentos/Miel 1','');
INSERT INTO `quiz_respuesta` VALUES (173,44,'<i>Ruritse</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (174,44,'<i>Pexúri</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (175,44,'<i>Xiete</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (176,44,'<i>Tsakaka</i>',1,'','');
INSERT INTO `quiz_respuesta` VALUES (177,45,'<i>Ketsɨ itsari</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (178,45,'<i>Ketsɨ wiyamari</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (179,45,'<i>Ketsɨ warikietɨ</i>',1,'','');
INSERT INTO `quiz_respuesta` VALUES (180,45,'<i>Ketsɨ</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (181,46,'<i>Kwixɨ</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (182,46,'<i>Ke’etsé</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (183,46,'<i>Ketsɨ</i>',1,'','');
INSERT INTO `quiz_respuesta` VALUES (184,46,'<i>Ha’axi</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (185,47,'<i>Untsa</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (186,47,'<i>Yáavi</i>',1,'','');
INSERT INTO `quiz_respuesta` VALUES (187,47,'<i>Kauxai</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (188,47,'<i>Xiete</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (189,48,'<i>Kauxai</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (190,48,'<i>Untsa</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (191,48,'<i>Xiete</i>',1,'','');
INSERT INTO `quiz_respuesta` VALUES (192,48,'<i>Yáavi</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (193,49,'<i>Xiete</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (194,49,'<i>Untsa</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (195,49,'<i>Yáavi</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (196,49,'<i>Kauxai</i>',1,'','');
INSERT INTO `quiz_respuesta` VALUES (197,50,'<i>Ha’tsa</i>',1,'','');
INSERT INTO `quiz_respuesta` VALUES (198,50,'<i>Nawaxa</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (199,50,'<i>Kutsira</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (200,50,'<i>Kɨyé</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (201,51,'La Yesca',0,'','');
INSERT INTO `quiz_respuesta` VALUES (202,51,'La laguna de Santa María del Oro',1,'','');
INSERT INTO `quiz_respuesta` VALUES (203,51,'Del Nayar',0,'','');
INSERT INTO `quiz_respuesta` VALUES (204,51,'Las minas de Santa María del Oro',0,'','');
INSERT INTO `quiz_respuesta` VALUES (205,52,'<i>Titsatsaweme</i>',1,'','');
INSERT INTO `quiz_respuesta` VALUES (206,52,'<i>Tiyuitɨwame</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (207,52,'<i>Muka’etsa</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (208,52,'<i>Ketsɨtame</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (209,53,'<i>Xaweruxite</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (210,53,'<i>Kemarite</i>',1,'','');
INSERT INTO `quiz_respuesta` VALUES (211,53,'<i>Xapatuxite</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (212,53,'<i>Kamixate</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (213,54,'De telas de colores',0,'','');
INSERT INTO `quiz_respuesta` VALUES (214,54,'De manta blanca y listones',0,'','');
INSERT INTO `quiz_respuesta` VALUES (215,54,'De manta blanca bordada con estambre',1,'','');
INSERT INTO `quiz_respuesta` VALUES (216,54,'De algodón y estambre',0,'','');
INSERT INTO `quiz_respuesta` VALUES (217,55,'Nuestra madre el mar',0,'','');
INSERT INTO `quiz_respuesta` VALUES (218,55,'Nuestro hermano mayor Venado Azul',1,'','');
INSERT INTO `quiz_respuesta` VALUES (219,55,'Nuestra madre tierra',0,'','');
INSERT INTO `quiz_respuesta` VALUES (220,55,'Nuestra madre águila',0,'','');
INSERT INTO `quiz_respuesta` VALUES (221,56,'Espuma de mar',0,'','');
INSERT INTO `quiz_respuesta` VALUES (222,56,'Peyote y maíz',1,'','');
INSERT INTO `quiz_respuesta` VALUES (223,56,'Lluvia y viento',0,'','');
INSERT INTO `quiz_respuesta` VALUES (224,56,'Cactus',0,'','');
INSERT INTO `quiz_respuesta` VALUES (225,57,'San Blas',0,'','');
INSERT INTO `quiz_respuesta` VALUES (226,57,'La Yesca',0,'','');
INSERT INTO `quiz_respuesta` VALUES (227,57,'Mesa del Nayar',0,'','');
INSERT INTO `quiz_respuesta` VALUES (228,57,'<i>Wirikuta</i>',1,'','');
INSERT INTO `quiz_respuesta` VALUES (229,58,'<i>Muxu’uri</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (230,58,'<i>Xa’ata</i>',1,'','');
INSERT INTO `quiz_respuesta` VALUES (231,58,'<i>Ka’arú</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (232,58,'<i>Uwá</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (233,59,'Plátano',0,'Images/Wixarika/Alimentos/Plátano','');
INSERT INTO `quiz_respuesta` VALUES (234,59,'Caña',0,'Images/Wixarika/Alimentos/Caña 1x','');
INSERT INTO `quiz_respuesta` VALUES (235,59,'Guamúchil',0,'Images/Wixarika/Alimentos/Guamúchil','');
INSERT INTO `quiz_respuesta` VALUES (236,59,'Jícama',1,'Images/Wixarika/Alimentos/Jícama','');
INSERT INTO `quiz_respuesta` VALUES (237,60,'<i>Kwarɨpa</i>',1,'','');
INSERT INTO `quiz_respuesta` VALUES (238,60,'<i>Tsikwai</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (239,60,'<i>Uwakí</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (240,60,'<i>Yɨɨna</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (241,61,'Tuna',0,'Images/Wixarika/Alimentos/Tuna','');
INSERT INTO `quiz_respuesta` VALUES (242,61,'Nanchi',0,'Images/Wixarika/Alimentos/Nanchi 2','');
INSERT INTO `quiz_respuesta` VALUES (243,61,'Ciruela',1,'Images/Wixarika/Alimentos/Ciruela española','');
INSERT INTO `quiz_respuesta` VALUES (244,61,'Arrayan',0,'Images/Wixarika/Alimentos/Arrayan 2','');
INSERT INTO `quiz_respuesta` VALUES (245,62,'Pipián de maxa',0,'','');
INSERT INTO `quiz_respuesta` VALUES (246,62,'<i>Maxa itsari</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (247,62,'<i>Maxa</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (248,62,'<i>Maxa ikwaiyári</i>',1,'','');
INSERT INTO `quiz_respuesta` VALUES (249,63,'<i>Yervawena</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (250,63,'<i>Mantsaniya</i>',1,'','');
INSERT INTO `quiz_respuesta` VALUES (251,63,'<i>Wáxa</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (252,63,'<i>Orekanu</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (253,64,'Potrero de palmita',1,'','');
INSERT INTO `quiz_respuesta` VALUES (254,64,'Cerro de San Juan',0,'','');
INSERT INTO `quiz_respuesta` VALUES (255,64,'Presa de aguamilpa',0,'','');
INSERT INTO `quiz_respuesta` VALUES (256,64,'Tepic',0,'','');
INSERT INTO `quiz_respuesta` VALUES (257,65,'<i>Tituayame</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (258,65,'<i>Ikwai wewiwame</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (259,65,'<i>Tiyuitɨwame</i>',1,'','');
INSERT INTO `quiz_respuesta` VALUES (260,65,'<i>Watame</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (261,66,'<i>Xaweri</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (262,66,'<i>Kwikarí</i>',1,'','');
INSERT INTO `quiz_respuesta` VALUES (263,66,'<i>Kwikariyari</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (264,66,'<i>Tunuiya</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (265,67,'Sones, baladas y mariachi',0,'','');
INSERT INTO `quiz_respuesta` VALUES (266,67,'Cantos espirituales y cantos sagrados',0,'','');
INSERT INTO `quiz_respuesta` VALUES (267,67,'Música norteña y música ranchera',0,'','');
INSERT INTO `quiz_respuesta` VALUES (268,67,'Canto sagrado, música tradicional y música regional',1,'','');
INSERT INTO `quiz_respuesta` VALUES (269,68,'Historias sobre el universo',0,'','');
INSERT INTO `quiz_respuesta` VALUES (270,68,'Oraciones',0,'','');
INSERT INTO `quiz_respuesta` VALUES (271,68,'Historias sobre los orígenes',1,'','');
INSERT INTO `quiz_respuesta` VALUES (272,68,'Canciones',0,'','');
INSERT INTO `quiz_respuesta` VALUES (273,69,'<i>Tsikwai</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (274,69,'<i>Ha’yewaxi</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (275,69,'<i>Ma’aku</i>',1,'','');
INSERT INTO `quiz_respuesta` VALUES (276,69,'<i>Kwarɨpa</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (277,70,'Guayaba',0,'Images/Wixarika/Alimentos/Guayaba 1','');
INSERT INTO `quiz_respuesta` VALUES (278,70,'Ciruela',0,'Images/Wixarika/Alimentos/Ciruela española','');
INSERT INTO `quiz_respuesta` VALUES (279,70,'Arrayan',0,'Images/Wixarika/Alimentos/Arrayan 2','');
INSERT INTO `quiz_respuesta` VALUES (280,70,'Mango',1,'Images/Wixarika/Alimentos/Mango','');
INSERT INTO `quiz_respuesta` VALUES (281,71,'<i>Tsikwai</i>',1,'','');
INSERT INTO `quiz_respuesta` VALUES (282,71,'<i>Ma’aku</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (283,71,'<i>Kwarɨpa</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (284,71,'<i>Ha’yewaxi</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (285,72,'Guayaba',0,'Images/Wixarika/Alimentos/Guayaba 1','');
INSERT INTO `quiz_respuesta` VALUES (286,72,'Arrayan',1,'Images/Wixarika/Alimentos/Arrayan 2','');
INSERT INTO `quiz_respuesta` VALUES (287,72,'Mango',0,'Images/Wixarika/Alimentos/Mango','');
INSERT INTO `quiz_respuesta` VALUES (288,72,'Ciruela',0,'Images/Wixarika/Alimentos/Ciruela española','');
INSERT INTO `quiz_respuesta` VALUES (289,73,'<i>Tekɨ itsari</i>',1,'','');
INSERT INTO `quiz_respuesta` VALUES (290,73,'<i>Tátsiu itsari</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (291,73,'<i>Weurai itsari</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (292,73,'<i>Tuixu yeutanaka itsari</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (293,74,'<i>Xɨye</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (294,74,'<i>Tekɨ</i>',1,'','');
INSERT INTO `quiz_respuesta` VALUES (295,74,'<i>Mitsu</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (296,74,'<i>Tátsiu</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (297,75,'<i>Tuuká</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (298,75,'<i>Xiete</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (299,75,'<i>Teruka</i>',1,'','');
INSERT INTO `quiz_respuesta` VALUES (300,75,'<i>Curupo</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (301,76,'Tepic',0,'','');
INSERT INTO `quiz_respuesta` VALUES (302,76,'Río Santiago',1,'','');
INSERT INTO `quiz_respuesta` VALUES (303,76,'Cerro de San Juan',0,'','');
INSERT INTO `quiz_respuesta` VALUES (304,76,'Santiago Ixcuintla',0,'','');
INSERT INTO `quiz_respuesta` VALUES (305,77,'Presa de aguamilpa',0,'','');
INSERT INTO `quiz_respuesta` VALUES (306,77,'Isla de Mexcaltitán',1,'','');
INSERT INTO `quiz_respuesta` VALUES (307,77,'Santiago Ixcuintla',0,'','');
INSERT INTO `quiz_respuesta` VALUES (308,77,'Tepic',0,'','');
INSERT INTO `quiz_respuesta` VALUES (309,78,'<i>Kwatsa</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (310,78,'<i>Werika</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (311,78,'<i>Witse</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (312,78,'<i>Mikɨri</i>',1,'','');
INSERT INTO `quiz_respuesta` VALUES (313,79,'Cuervo',0,'','');
INSERT INTO `quiz_respuesta` VALUES (314,79,'Águila',0,'','');
INSERT INTO `quiz_respuesta` VALUES (315,79,'Halcón',0,'','');
INSERT INTO `quiz_respuesta` VALUES (316,79,'Lechuza',1,'','');
INSERT INTO `quiz_respuesta` VALUES (317,80,'Cerca de casas y caminos',1,'','');
INSERT INTO `quiz_respuesta` VALUES (318,80,'En los ríos',0,'','');
INSERT INTO `quiz_respuesta` VALUES (319,80,'En los sitios sagrados',0,'','');
INSERT INTO `quiz_respuesta` VALUES (320,80,'En las escuelas',0,'','');
INSERT INTO `quiz_respuesta` VALUES (321,81,'<i>Neixa</i>',1,'','');
INSERT INTO `quiz_respuesta` VALUES (322,81,'<i>Kwikari</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (323,81,'<i>Ixɨarari</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (324,81,'<i>Kakaɨyarita</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (325,82,'Las tormentas',0,'','');
INSERT INTO `quiz_respuesta` VALUES (326,82,'El inicio de año',0,'','');
INSERT INTO `quiz_respuesta` VALUES (327,82,'El nacimiento de las deidades',0,'','');
INSERT INTO `quiz_respuesta` VALUES (328,82,'El ciclo del cultivo de maíz',1,'','');
INSERT INTO `quiz_respuesta` VALUES (329,83,'<i>Tiwatuiya</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (330,83,'<i>Uyé</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (331,83,'<i>Hɨri</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (332,83,'<i>Tukipa</i>',1,'','');
INSERT INTO `quiz_respuesta` VALUES (333,84,'<i>Kií</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (334,84,'<i>Tukipa</i>',1,'','');
INSERT INTO `quiz_respuesta` VALUES (335,84,'<i>Xiriki</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (336,84,'<i>Tuki</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (337,85,'Una iglesia',0,'','');
INSERT INTO `quiz_respuesta` VALUES (338,85,'Un espacio para la meditación',0,'','');
INSERT INTO `quiz_respuesta` VALUES (339,85,'Un espacio de culto de las deidades',1,'','');
INSERT INTO `quiz_respuesta` VALUES (340,85,'Una escuela',0,'','');
INSERT INTO `quiz_respuesta` VALUES (341,86,'<i>Tuki y xiriki</i>',1,'','');
INSERT INTO `quiz_respuesta` VALUES (342,86,'<i>Hɨri y watsíya</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (343,86,'<i>Te’erɨ y ke’ekari</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (344,86,'<i>Haramara y haita</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (345,87,'<i>Uwakí</i>',1,'','');
INSERT INTO `quiz_respuesta` VALUES (346,87,'<i>Ka’arú</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (347,87,'<i>Kwarɨpa</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (348,87,'<i>Tsikwai</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (349,88,'Ciruela',0,'Images/Wixarika/Alimentos/Ciruela española','');
INSERT INTO `quiz_respuesta` VALUES (350,88,'Arrayan',0,'Images/Wixarika/Alimentos/Arrayan 2','');
INSERT INTO `quiz_respuesta` VALUES (351,88,'Nanchi',1,'Images/Wixarika/Alimentos/Nanchi','');
INSERT INTO `quiz_respuesta` VALUES (352,88,'Plátano',0,'Images/Wixarika/Alimentos/Plátanos 1x','');
INSERT INTO `quiz_respuesta` VALUES (353,89,'<i>Uwakí</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (354,89,'<i>Tsikwai</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (355,89,'<i>Kwarɨpa</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (356,89,'<i>Ka’arú</i>',1,'','');
INSERT INTO `quiz_respuesta` VALUES (357,90,'Nanchi',0,'Images/Wixarika/Alimentos/Nanchi','');
INSERT INTO `quiz_respuesta` VALUES (358,90,'Plátano',1,'Images/Wixarika/Alimentos/Plátano','');
INSERT INTO `quiz_respuesta` VALUES (359,90,'Ciruela',0,'Images/Wixarika/Alimentos/Ciruela española','');
INSERT INTO `quiz_respuesta` VALUES (360,90,'Arrayan',0,'Images/Wixarika/Alimentos/Arrayan 2','');
INSERT INTO `quiz_respuesta` VALUES (361,91,'Pipián de <i>tuixu</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (362,91,'Pipián de <i>tátsiu</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (363,91,'Pipián de <i>weurai</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (364,91,'Pipián de <i>ke’etse</i>',1,'','');
INSERT INTO `quiz_respuesta` VALUES (365,92,'<i>Ruritse</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (366,92,'<i>Pexúri</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (367,92,'<i>Ka’arú wiyamatɨ</i>',1,'','');
INSERT INTO `quiz_respuesta` VALUES (368,92,'<i>Paní</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (369,93,'<i>Wakana</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (370,93,'<i>Tuixuyeutanaka</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (371,93,'<i>Tuixu</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (372,93,'<i>Ke’etsé</i>',1,'','');
INSERT INTO `quiz_respuesta` VALUES (373,94,'<i>Ɨkwi</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (374,94,'<i>Ha’axi</i>',1,'','');
INSERT INTO `quiz_respuesta` VALUES (375,94,'<i>Ke’etsé</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (376,94,'<i>Téka</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (377,95,'Un sitio sagrado',0,'','');
INSERT INTO `quiz_respuesta` VALUES (378,95,'Una ofrenda para las deidades',1,'','');
INSERT INTO `quiz_respuesta` VALUES (379,95,'Una comida tradicional',0,'','');
INSERT INTO `quiz_respuesta` VALUES (380,95,'Un adorno',0,'','');
INSERT INTO `quiz_respuesta` VALUES (381,96,'Un centro ceremonial',0,'','');
INSERT INTO `quiz_respuesta` VALUES (382,96,'La cosmovisión Wixárika',0,'','');
INSERT INTO `quiz_respuesta` VALUES (383,96,'Un portal al inframundo',0,'','');
INSERT INTO `quiz_respuesta` VALUES (384,96,'Los cinco puntos cardinales del <i>Wixárika</i>',1,'','');
INSERT INTO `quiz_respuesta` VALUES (385,97,'Puerto de San Blas',0,'','');
INSERT INTO `quiz_respuesta` VALUES (386,97,'Santiago Ixcuintla',0,'','');
INSERT INTO `quiz_respuesta` VALUES (387,97,'<i>Haramara</i>',1,'','');
INSERT INTO `quiz_respuesta` VALUES (388,97,'Tepic',0,'','');
INSERT INTO `quiz_respuesta` VALUES (389,98,'La Yesca, Nayarit',0,'','');
INSERT INTO `quiz_respuesta` VALUES (390,98,'San Blas, Nayarit',1,'','');
INSERT INTO `quiz_respuesta` VALUES (391,98,'Tepic, Nayarit',0,'','');
INSERT INTO `quiz_respuesta` VALUES (392,98,'Tuxpan, Nayarit',0,'','');
INSERT INTO `quiz_respuesta` VALUES (393,99,'Nuestro abuelo fuego',0,'','');
INSERT INTO `quiz_respuesta` VALUES (394,99,'Nuestra madre el mar',1,'','');
INSERT INTO `quiz_respuesta` VALUES (395,99,'Nuestra madre tierra',0,'','');
INSERT INTO `quiz_respuesta` VALUES (396,99,'Nuestra madre águila',0,'','');
INSERT INTO `quiz_respuesta` VALUES (397,100,'Rocas',0,'','');
INSERT INTO `quiz_respuesta` VALUES (398,100,'Espuma de mar',0,'','');
INSERT INTO `quiz_respuesta` VALUES (399,100,'Cerros',0,'','');
INSERT INTO `quiz_respuesta` VALUES (400,100,'Nubes y lluvia',1,'','');
INSERT INTO `quiz_respuesta` VALUES (401,101,'<i>Mawari</i>',1,'','');
INSERT INTO `quiz_respuesta` VALUES (402,101,'<i>Xátsika</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (403,101,'<i>Kwikari</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (404,101,'<i>Kemari</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (405,102,'Comida para los muertos',0,'','');
INSERT INTO `quiz_respuesta` VALUES (406,102,'Un regalo para tus amigos',0,'','');
INSERT INTO `quiz_respuesta` VALUES (407,102,'Un objeto espiritual para las deidades',1,'','');
INSERT INTO `quiz_respuesta` VALUES (408,102,'Un adorno para las casas',0,'','');
INSERT INTO `quiz_respuesta` VALUES (409,103,'Para regalarlas a tus amigos',0,'','');
INSERT INTO `quiz_respuesta` VALUES (410,103,'Para agradecer o pedir a las deidades',1,'','');
INSERT INTO `quiz_respuesta` VALUES (411,103,'Para decorar las casas',0,'','');
INSERT INTO `quiz_respuesta` VALUES (412,103,'Para enviar mensajes a otros <i>wixaritari</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (413,104,'<i>Ikú tataɨrawi</i>',1,'','');
INSERT INTO `quiz_respuesta` VALUES (414,104,'<i>Ikɨri</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (415,104,'<i>Ikú taxawime</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (416,104,'<i>Ikú</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (417,105,'Maíz blanco',0,'','');
INSERT INTO `quiz_respuesta` VALUES (418,105,'Maíz morado',1,'','');
INSERT INTO `quiz_respuesta` VALUES (419,105,'Maíz amarillo',0,'','');
INSERT INTO `quiz_respuesta` VALUES (420,105,'Maíz rojo',0,'','');
INSERT INTO `quiz_respuesta` VALUES (421,106,'<i>Ikú taxawime</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (422,106,'<i>Ikú</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (423,106,'<i>Ikú yɨwi</i>',1,'','');
INSERT INTO `quiz_respuesta` VALUES (424,106,'<i>Ikɨri</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (425,107,'<i>Hayaári</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (426,107,'<i>Retsi</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (427,107,'<i>Hamuitsi</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (428,107,'<i>Tsinari</i>',1,'','');
INSERT INTO `quiz_respuesta` VALUES (429,108,'<i>Ikɨri paniyari</i>',1,'','');
INSERT INTO `quiz_respuesta` VALUES (430,108,'<i>Ruritse</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (431,108,'<i>Paní</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (432,108,'<i>Ka’arú wiyamatɨ</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (433,109,'<i>Kɨyé</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (434,109,'<i>Hatsaruni</i>',1,'','');
INSERT INTO `quiz_respuesta` VALUES (435,109,'<i>Kutsira</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (436,109,'<i>Ha’tsa</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (437,110,'Valparaíso',0,'','');
INSERT INTO `quiz_respuesta` VALUES (438,110,'San Blas',0,'','');
INSERT INTO `quiz_respuesta` VALUES (439,110,'<i>Tuapurie</i>',1,'','');
INSERT INTO `quiz_respuesta` VALUES (440,110,'Tepic',0,'','');
INSERT INTO `quiz_respuesta` VALUES (441,111,'San Blas',0,'','');
INSERT INTO `quiz_respuesta` VALUES (442,111,'Valparaíso',0,'','');
INSERT INTO `quiz_respuesta` VALUES (443,111,'Tepic',0,'','');
INSERT INTO `quiz_respuesta` VALUES (444,111,'<i>Xurahue Muyaca</i>',1,'','');
INSERT INTO `quiz_respuesta` VALUES (445,112,'Nuestra madre águila',1,'','');
INSERT INTO `quiz_respuesta` VALUES (446,112,'Nuestro abuelo fuego',0,'','');
INSERT INTO `quiz_respuesta` VALUES (447,112,'Nuestro hermano mayor Venado Azul',0,'','');
INSERT INTO `quiz_respuesta` VALUES (448,112,'Nuestra madre el mar',0,'','');
INSERT INTO `quiz_respuesta` VALUES (449,113,'<i>Xuku’uri ɨkame</i>',1,'','');
INSERT INTO `quiz_respuesta` VALUES (450,113,'<i>Tupiri</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (451,113,'<i>Mara’kame</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (452,113,'<i>Tatuwani</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (453,114,'<i>Mara’kame</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (454,114,'<i>Xuku’uri ɨkame</i>',1,'','');
INSERT INTO `quiz_respuesta` VALUES (455,114,'<i>Kawiteru</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (456,114,'<i>Hikuritame</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (457,115,'<i>Hapani</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (458,115,'<i>Wáxa</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (459,115,'<i>Hikuri</i>',1,'','');
INSERT INTO `quiz_respuesta` VALUES (460,115,'<i>Uxa</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (461,116,'El sol y la luna',0,'','');
INSERT INTO `quiz_respuesta` VALUES (462,116,'La espuma de mar de <i>Tatei Haramara',0,'','');
INSERT INTO `quiz_respuesta` VALUES (463,116,'La tierra y la lluvia',0,'','');
INSERT INTO `quiz_respuesta` VALUES (464,116,'Los cuernos de <i>Tamatzi Kauyumari',1,'','');
INSERT INTO `quiz_respuesta` VALUES (465,117,'Las historias sobre el origen de los <i>wixaritari',1,'','');
INSERT INTO `quiz_respuesta` VALUES (466,117,'La leyenda del Venado Azul',0,'','');
INSERT INTO `quiz_respuesta` VALUES (467,117,'Las historias sobre el fuego',0,'','');
INSERT INTO `quiz_respuesta` VALUES (468,117,'La leyenda de <i>Watakame',0,'','');
INSERT INTO `quiz_respuesta` VALUES (469,118,'Las casas',0,'','');
INSERT INTO `quiz_respuesta` VALUES (470,118,'Los sitios sagrados',1,'','');
INSERT INTO `quiz_respuesta` VALUES (471,118,'Las escuelas',0,'','');
INSERT INTO `quiz_respuesta` VALUES (472,118,'Los <i>tukipa</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (473,119,'<i>Haramara</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (474,119,'<i>Hauxa Manaka</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (475,119,'<i>Wirikuta</i>',1,'','');
INSERT INTO `quiz_respuesta` VALUES (476,119,'<i>Te’ekata</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (477,120,'<i>Kukúri</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (478,120,'<i>Tsinakari</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (479,120,'<i>Xa’ata</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (480,120,'<i>Túmati</i>',1,'','');
INSERT INTO `quiz_respuesta` VALUES (481,121,'Jitomate',1,'Images/Wixarika/Alimentos/Jitomate','');
INSERT INTO `quiz_respuesta` VALUES (482,121,'Limón',0,'Images/Wixarika/Alimentos/Limón 1','');
INSERT INTO `quiz_respuesta` VALUES (483,121,'Chile',0,'Images/Wixarika/Alimentos/Chile','');
INSERT INTO `quiz_respuesta` VALUES (484,121,'Jícama',0,'Images/Wixarika/Alimentos/Jícama','');
INSERT INTO `quiz_respuesta` VALUES (485,122,'<i>Tátsiu itsari</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (486,122,'<i>Weurai itsari</i>',1,'','');
INSERT INTO `quiz_respuesta` VALUES (487,122,'<i>Ketsɨ itsari</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (488,122,'<i>Wakana itsari</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (489,123,'<i>Kwixɨ</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (490,123,'<i>Wakana</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (491,123,'<i>Weurai</i>',1,'','');
INSERT INTO `quiz_respuesta` VALUES (492,123,'<i>Peexá</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (493,124,'<i>Mantsaniya</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (494,124,'<i>Eɨkariti</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (495,124,'<i>Yervawena</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (496,124,'<i>Ɨrawe emɨtimariwe</i>',1,'','');
INSERT INTO `quiz_respuesta` VALUES (497,125,'Plateros',1,'','');
INSERT INTO `quiz_respuesta` VALUES (498,125,'Valparaíso',0,'','');
INSERT INTO `quiz_respuesta` VALUES (499,125,'San Blas',0,'','');
INSERT INTO `quiz_respuesta` VALUES (500,125,'Fresnillo',0,'','');
INSERT INTO `quiz_respuesta` VALUES (501,126,'<i>Teruka</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (502,126,'<i>Ɨkwi</i>',1,'','');
INSERT INTO `quiz_respuesta` VALUES (503,126,'<i>Téka</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (504,126,'<i>Ke’etsé</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (505,127,'Iguana',0,'','');
INSERT INTO `quiz_respuesta` VALUES (506,127,'Camaleón cornudo',0,'','');
INSERT INTO `quiz_respuesta` VALUES (507,127,'Lagartija',1,'','');
INSERT INTO `quiz_respuesta` VALUES (508,127,'Alacrán',0,'','');
INSERT INTO `quiz_respuesta` VALUES (509,128,'Jabalí',0,'','');
INSERT INTO `quiz_respuesta` VALUES (510,128,'Venado azul',0,'','');
INSERT INTO `quiz_respuesta` VALUES (511,128,'Águila real',0,'','');
INSERT INTO `quiz_respuesta` VALUES (512,128,'Venado cola blanca',1,'','');
INSERT INTO `quiz_respuesta` VALUES (513,129,'Corazón',1,'','');
INSERT INTO `quiz_respuesta` VALUES (514,129,'Espíritu',0,'','');
INSERT INTO `quiz_respuesta` VALUES (515,129,'Alma',0,'','');
INSERT INTO `quiz_respuesta` VALUES (516,129,'Mente',0,'','');
INSERT INTO `quiz_respuesta` VALUES (517,130,'Las comunidades originarias',0,'','');
INSERT INTO `quiz_respuesta` VALUES (518,130,'La cultura y peregrinaciones Wixárika',1,'','');
INSERT INTO `quiz_respuesta` VALUES (519,130,'El corazón',0,'','');
INSERT INTO `quiz_respuesta` VALUES (520,130,'Las deidades',0,'','');
INSERT INTO `quiz_respuesta` VALUES (521,131,'<i>Tayu</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (522,131,'<i>Tatei Haramara</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (523,131,'<i>Tamatzi Kauyumari</i>',1,'','');
INSERT INTO `quiz_respuesta` VALUES (524,131,'<i>Tatewari</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (525,132,'<i>Ha’yewaxi</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (526,132,'<i>Kwarɨpa</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (527,132,'<i>Yɨɨna</i>',1,'','');
INSERT INTO `quiz_respuesta` VALUES (528,132,'<i>Muxu’uri</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (529,133,'Guayaba',0,'Images/Wixarika/Alimentos/Guayaba 1','');
INSERT INTO `quiz_respuesta` VALUES (530,133,'Guamúchil',0,'Images/Wixarika/Alimentos/Guamúchil','');
INSERT INTO `quiz_respuesta` VALUES (531,133,'Tuna',1,'Images/Wixarika/Alimentos/Tuna','');
INSERT INTO `quiz_respuesta` VALUES (532,133,'Ciruela',0,'Images/Wixarika/Alimentos/Ciruela','');
INSERT INTO `quiz_respuesta` VALUES (533,134,'<i>Ké’uxa</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (534,134,'<i>Yekwa</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (535,134,'<i>Múme</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (536,134,'<i>Ye’eri</i>',1,'','');
INSERT INTO `quiz_respuesta` VALUES (537,135,'Camote',1,'Images/Wixarika/Alimentos/Camote','');
INSERT INTO `quiz_respuesta` VALUES (538,135,'Hongo',0,'Images/Wixarika/Alimentos/Hongo','');
INSERT INTO `quiz_respuesta` VALUES (539,135,'Frijol',0,'Images/Wixarika/Alimentos/Frijol','');
INSERT INTO `quiz_respuesta` VALUES (540,135,'Quelite',0,'Images/Wixarika/Alimentos/Quelites','');
INSERT INTO `quiz_respuesta` VALUES (541,136,'<i>Weurai itsari</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (542,136,'Enchilada de <i>wakana</i>',1,'','');
INSERT INTO `quiz_respuesta` VALUES (543,136,'<i>Wakana itsari</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (544,136,'Enchilada de <i>weurai</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (545,137,'<i>Tawari</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (546,137,'<i>Weurai</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (547,137,'<i>Wakana</i>',1,'','');
INSERT INTO `quiz_respuesta` VALUES (548,137,'<i>Ketsɨ</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (549,138,'Un adorno',0,'','');
INSERT INTO `quiz_respuesta` VALUES (550,138,'Una comida tradicional',0,'','');
INSERT INTO `quiz_respuesta` VALUES (551,138,'Un sitio sagrado',0,'','');
INSERT INTO `quiz_respuesta` VALUES (552,138,'Una ofrenda para las deidades',1,'','');
INSERT INTO `quiz_respuesta` VALUES (553,139,'El contacto con el mestizo.',1,'','');
INSERT INTO `quiz_respuesta` VALUES (554,139,'Los disfraces.',0,'','');
INSERT INTO `quiz_respuesta` VALUES (555,139,'El contacto con el fuego.',0,'','');
INSERT INTO `quiz_respuesta` VALUES (556,139,'Las pascuas.',0,'','');
INSERT INTO `quiz_respuesta` VALUES (557,140,'Valparaíso',0,'','');
INSERT INTO `quiz_respuesta` VALUES (558,140,'<i>Makuipa</i> (Cerro del Padre)',1,'','');
INSERT INTO `quiz_respuesta` VALUES (559,140,'Fresnillo',0,'','');
INSERT INTO `quiz_respuesta` VALUES (560,140,'Zacatecas',0,'','');
INSERT INTO `quiz_respuesta` VALUES (561,141,'Nuestro abuelo fuego',0,'','');
INSERT INTO `quiz_respuesta` VALUES (562,141,'Nuestra madre el mar',0,'','');
INSERT INTO `quiz_respuesta` VALUES (563,141,'Dios del fuego primigenio',1,'','');
INSERT INTO `quiz_respuesta` VALUES (564,141,'Nuestra madre agua sagrada',0,'','');
INSERT INTO `quiz_respuesta` VALUES (565,142,'Nuestra madre el mar',0,'','');
INSERT INTO `quiz_respuesta` VALUES (566,142,'Nuestra madre agua sagrada',0,'','');
INSERT INTO `quiz_respuesta` VALUES (567,142,'Nuestra madre águila',0,'','');
INSERT INTO `quiz_respuesta` VALUES (568,142,'Diosa de la tierra',1,'','');
INSERT INTO `quiz_respuesta` VALUES (569,143,'Fiesta del solsticio de verano',1,'','');
INSERT INTO `quiz_respuesta` VALUES (570,143,'Fiesta de los primeros frutos',0,'','');
INSERT INTO `quiz_respuesta` VALUES (571,143,'Fiesta del tambor',0,'','');
INSERT INTO `quiz_respuesta` VALUES (572,143,'Fiesta de los elotes',0,'','');
INSERT INTO `quiz_respuesta` VALUES (573,144,'Veladoras',0,'','');
INSERT INTO `quiz_respuesta` VALUES (574,144,'Flechas, jícaras, máscaras, ojos de dios y tablillas',1,'','');
INSERT INTO `quiz_respuesta` VALUES (575,144,'Muñecos de paja',0,'','');
INSERT INTO `quiz_respuesta` VALUES (576,144,'Biblias',0,'','');
INSERT INTO `quiz_respuesta` VALUES (577,145,'Una espina',0,'','');
INSERT INTO `quiz_respuesta` VALUES (578,145,'Un nopal',0,'','');
INSERT INTO `quiz_respuesta` VALUES (579,145,'Un cactus sin espinas',1,'','');
INSERT INTO `quiz_respuesta` VALUES (580,145,'Una droga',0,'','');
INSERT INTO `quiz_respuesta` VALUES (581,146,'En la playa',0,'','');
INSERT INTO `quiz_respuesta` VALUES (582,146,'En las planicies del sur',0,'','');
INSERT INTO `quiz_respuesta` VALUES (583,146,'En las lagunas',0,'','');
INSERT INTO `quiz_respuesta` VALUES (584,146,'En los desiertos del norte de México',1,'','');
INSERT INTO `quiz_respuesta` VALUES (585,147,'De 30 a 50 años',0,'','');
INSERT INTO `quiz_respuesta` VALUES (586,147,'Un mes',0,'','');
INSERT INTO `quiz_respuesta` VALUES (587,147,'De 15 a 20 años',1,'','');
INSERT INTO `quiz_respuesta` VALUES (588,147,'Una semana',0,'','');
INSERT INTO `quiz_respuesta` VALUES (589,148,'Solamente los <i>wixaritari</i>',1,'','');
INSERT INTO `quiz_respuesta` VALUES (590,148,'Nadie puede recolectarlo',0,'','');
INSERT INTO `quiz_respuesta` VALUES (591,148,'Solamente los mestizos',0,'','');
INSERT INTO `quiz_respuesta` VALUES (592,148,'Todos pueden recolectarlo',0,'','');
INSERT INTO `quiz_respuesta` VALUES (593,149,'Se caza al jabalí y se recolectan hongos',0,'','');
INSERT INTO `quiz_respuesta` VALUES (594,149,'Se caza al venado, se recolecta peyote y se transporta agua',1,'','');
INSERT INTO `quiz_respuesta` VALUES (595,149,'Se cazan iguanas y se transporta tierra',0,'','');
INSERT INTO `quiz_respuesta` VALUES (596,149,'Se caza al águila y se transporta fuego',0,'','');
INSERT INTO `quiz_respuesta` VALUES (597,150,'Los ancianos',0,'','');
INSERT INTO `quiz_respuesta` VALUES (598,150,'Los hongos',0,'','');
INSERT INTO `quiz_respuesta` VALUES (599,150,'Los antepasados',1,'','');
INSERT INTO `quiz_respuesta` VALUES (600,150,'Las flores',0,'','');
INSERT INTO `quiz_respuesta` VALUES (601,151,'Para pedir dinero',0,'','');
INSERT INTO `quiz_respuesta` VALUES (602,151,'Para invocar el frío',0,'','');
INSERT INTO `quiz_respuesta` VALUES (603,151,'Para pedir salud',0,'','');
INSERT INTO `quiz_respuesta` VALUES (604,151,'Para invocar a la lluvia',1,'','');
INSERT INTO `quiz_respuesta` VALUES (605,152,'<i>Iku’ yuawime</i>',1,'','');
INSERT INTO `quiz_respuesta` VALUES (606,152,'<i>Ikú</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (607,152,'<i>Ikú taxawime</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (608,152,'<i>Ikɨri</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (609,153,'Maíz blanco',0,'','');
INSERT INTO `quiz_respuesta` VALUES (610,153,'Maíz azul',1,'','');
INSERT INTO `quiz_respuesta` VALUES (611,153,'Maíz amarillo',0,'','');
INSERT INTO `quiz_respuesta` VALUES (612,153,'Maíz rojo',0,'','');
INSERT INTO `quiz_respuesta` VALUES (613,154,'<i>Xupaxi</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (614,154,'<i>Taku</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (615,154,'<i>Múmete takuyari</i>',1,'','');
INSERT INTO `quiz_respuesta` VALUES (616,154,'<i>Tétsu</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (617,155,'<i>Wakaxi</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (618,155,'<i>Tuixuyeutanaka</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (619,155,'<i>Wakana</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (620,155,'<i>Tuixu</i>',1,'','');
INSERT INTO `quiz_respuesta` VALUES (621,156,'<i>Nawaxa</i>',1,'','');
INSERT INTO `quiz_respuesta` VALUES (622,156,'<i>Hatsaruni</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (623,156,'<i>Ha’tsa</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (624,156,'<i>Kutsira</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (625,157,'Zacatecas',0,'','');
INSERT INTO `quiz_respuesta` VALUES (626,157,'<i>Huahuatsari</i>',1,'','');
INSERT INTO `quiz_respuesta` VALUES (627,157,'Fresnillo',0,'','');
INSERT INTO `quiz_respuesta` VALUES (628,157,'Villa de Ramos',0,'','');
INSERT INTO `quiz_respuesta` VALUES (629,158,'Fresnillo',0,'','');
INSERT INTO `quiz_respuesta` VALUES (630,158,'Villa de Ramos',0,'','');
INSERT INTO `quiz_respuesta` VALUES (631,158,'<i>Cuhixu Uheni</i>',1,'','');
INSERT INTO `quiz_respuesta` VALUES (632,158,'Zacatecas',0,'','');
INSERT INTO `quiz_respuesta` VALUES (633,159,'Villa de Ramos',0,'','');
INSERT INTO `quiz_respuesta` VALUES (634,159,'Zacatecas',0,'','');
INSERT INTO `quiz_respuesta` VALUES (635,159,'Fresnillo',0,'','');
INSERT INTO `quiz_respuesta` VALUES (636,159,'<i>Tatei Matiniere</i>',1,'','');
INSERT INTO `quiz_respuesta` VALUES (637,160,'<i>Kawiteru</i>',1,'','');
INSERT INTO `quiz_respuesta` VALUES (638,160,'<i>Mara’kame</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (639,160,'<i>Tatuwani</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (640,160,'<i>Xuku’uri ɨkame</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (641,161,'Doctores',0,'','');
INSERT INTO `quiz_respuesta` VALUES (642,161,'Ancianos sabios',1,'','');
INSERT INTO `quiz_respuesta` VALUES (643,161,'Maestros',0,'','');
INSERT INTO `quiz_respuesta` VALUES (644,161,'Gobernadores',0,'','');
INSERT INTO `quiz_respuesta` VALUES (645,162,'Chamanes',0,'','');
INSERT INTO `quiz_respuesta` VALUES (646,162,'Maestros',0,'','');
INSERT INTO `quiz_respuesta` VALUES (647,162,'Ancianos sabios',1,'','');
INSERT INTO `quiz_respuesta` VALUES (648,162,'Doctores',0,'','');
INSERT INTO `quiz_respuesta` VALUES (649,163,'Morrales',0,'','');
INSERT INTO `quiz_respuesta` VALUES (650,163,'Escudos',0,'','');
INSERT INTO `quiz_respuesta` VALUES (651,163,'Flechas',0,'','');
INSERT INTO `quiz_respuesta` VALUES (652,163,'Collares, pulseras y aretes',1,'','');
INSERT INTO `quiz_respuesta` VALUES (653,164,'Morrales',1,'','');
INSERT INTO `quiz_respuesta` VALUES (654,164,'Collares',0,'','');
INSERT INTO `quiz_respuesta` VALUES (655,164,'Aretes',0,'','');
INSERT INTO `quiz_respuesta` VALUES (656,164,'Pulseras',0,'','');
INSERT INTO `quiz_respuesta` VALUES (657,165,'<i>Tuwaxate</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (658,165,'<i>Kuka tiwameté</i>',1,'','');
INSERT INTO `quiz_respuesta` VALUES (659,165,'<i>Hɨiyamete</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (660,165,'<i>Kakaíte</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (661,166,'<i>Hɨiyamete</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (662,166,'<i>Kakaíte</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (663,166,'<i>Matsiwate</i>',1,'','');
INSERT INTO `quiz_respuesta` VALUES (664,166,'<i>Tuwaxate</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (665,167,'<i>Kakaíte</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (666,167,'<i>Hɨiyamete</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (667,167,'<i>Tuwaxate</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (668,167,'<i>Nakɨtsate</i>',1,'','');
INSERT INTO `quiz_respuesta` VALUES (669,168,'<i>Kɨtsiɨrite</i>',1,'','');
INSERT INTO `quiz_respuesta` VALUES (670,168,'<i>Iwíte</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (671,168,'<i>Kamixate</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (672,168,'<i>Xikurite</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (673,169,'Negro, gris y café',0,'','');
INSERT INTO `quiz_respuesta` VALUES (674,169,'Blanco, amarillo, rojo, morado y azul.',1,'','');
INSERT INTO `quiz_respuesta` VALUES (675,169,'Rosa, naranja y verde',0,'','');
INSERT INTO `quiz_respuesta` VALUES (676,169,'Dorado y plateado',0,'','');
INSERT INTO `quiz_respuesta` VALUES (677,170,'La Madre del elote',0,'','');
INSERT INTO `quiz_respuesta` VALUES (678,170,'El Dios del campo',0,'','');
INSERT INTO `quiz_respuesta` VALUES (679,170,'La Madre del maíz',1,'','');
INSERT INTO `quiz_respuesta` VALUES (680,170,'El Padre del maíz',0,'','');
INSERT INTO `quiz_respuesta` VALUES (681,171,'<i>Taraki</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (682,171,'<i>Kweetsi</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (683,171,'<i>Karimutsi</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (684,171,'<i>Tsíweri</i>',1,'','');
INSERT INTO `quiz_respuesta` VALUES (685,172,'Gualumbos',1,'Images/Wixarika/Alimentos/Gualumbo','');
INSERT INTO `quiz_respuesta` VALUES (686,172,'Pochote',0,'Images/Wixarika/Alimentos/Pochote','');
INSERT INTO `quiz_respuesta` VALUES (687,172,'Habas',0,'Images/Wixarika/Alimentos/Habas','');
INSERT INTO `quiz_respuesta` VALUES (688,172,'Guajes',0,'Images/Wixarika/Alimentos/Guaje rojo','');
INSERT INTO `quiz_respuesta` VALUES (689,173,'<i>Tsíweri</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (690,173,'<i>Kweetsi</i>',1,'','');
INSERT INTO `quiz_respuesta` VALUES (691,173,'<i>Taraki</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (692,173,'<i>Karimutsi</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (693,174,'Guajes',0,'Images/Wixarika/Alimentos/Guaje rojo','');
INSERT INTO `quiz_respuesta` VALUES (694,174,'Gualumbos',0,'Images/Wixarika/Alimentos/Gualumbo','');
INSERT INTO `quiz_respuesta` VALUES (695,174,'Habas',1,'Images/Wixarika/Alimentos/Habas','');
INSERT INTO `quiz_respuesta` VALUES (696,174,'Pochote',0,'Images/Wixarika/Alimentos/Pochote','');
INSERT INTO `quiz_respuesta` VALUES (697,175,'<i>Ketsɨ itsari</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (698,175,'<i>Tekɨ itsari</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (699,175,'<i>Wakana itsari</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (700,175,'<i>Tátsiu itsari</i>',1,'','');
INSERT INTO `quiz_respuesta` VALUES (701,176,'<i>Tátsiu</i>',1,'','');
INSERT INTO `quiz_respuesta` VALUES (702,176,'<i>Mitsu</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (703,176,'<i>Tekɨ</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (704,176,'<i>Tsɨkɨ</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (705,177,'Santo Domingo',0,'','');
INSERT INTO `quiz_respuesta` VALUES (706,177,'<i>Maxa Yapa</i>',1,'','');
INSERT INTO `quiz_respuesta` VALUES (707,177,'Villa de Ramos',0,'','');
INSERT INTO `quiz_respuesta` VALUES (708,177,'Zacatecas',0,'','');
INSERT INTO `quiz_respuesta` VALUES (709,178,'Nuestra madre el mar',0,'','');
INSERT INTO `quiz_respuesta` VALUES (710,178,'Nuestra madre tierra',0,'','');
INSERT INTO `quiz_respuesta` VALUES (711,178,'Nuestra madre agua sagrada',1,'','');
INSERT INTO `quiz_respuesta` VALUES (712,178,'Nuestro abuelo fuego',0,'','');
INSERT INTO `quiz_respuesta` VALUES (713,179,'Santo Domingo',0,'','');
INSERT INTO `quiz_respuesta` VALUES (714,179,'<i>Mɨ tɨranitsie</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (715,179,'Villa de Ramos',0,'','');
INSERT INTO `quiz_respuesta` VALUES (716,179,'<i>Kutsaraɨpa</i>',1,'','');
INSERT INTO `quiz_respuesta` VALUES (717,180,'Violín y guitarra',1,'','');
INSERT INTO `quiz_respuesta` VALUES (718,180,'Tambor y trompeta',0,'','');
INSERT INTO `quiz_respuesta` VALUES (719,180,'Flauta y piano',0,'','');
INSERT INTO `quiz_respuesta` VALUES (720,180,'Marimba',0,'','');
INSERT INTO `quiz_respuesta` VALUES (721,181,'Tempestad de La Yesca, Nayarit',0,'','');
INSERT INTO `quiz_respuesta` VALUES (722,181,'El Venado Azul de Nueva Colonia, Jalisco',1,'','');
INSERT INTO `quiz_respuesta` VALUES (723,181,'Viento Wixárika de Tepic, Nayarit',0,'','');
INSERT INTO `quiz_respuesta` VALUES (724,181,'El Venado de Chapala, Jalisco',0,'','');
INSERT INTO `quiz_respuesta` VALUES (725,182,'Para fines textiles',0,'','');
INSERT INTO `quiz_respuesta` VALUES (726,182,'Para hacer pegamento',0,'','');
INSERT INTO `quiz_respuesta` VALUES (727,182,'Para fines medicinales y rituales',1,'','');
INSERT INTO `quiz_respuesta` VALUES (728,182,'Para hacer postres',0,'','');
INSERT INTO `quiz_respuesta` VALUES (729,183,'Las piedras',0,'','');
INSERT INTO `quiz_respuesta` VALUES (730,183,'El cactus',0,'','');
INSERT INTO `quiz_respuesta` VALUES (731,183,'Las rosas',0,'','');
INSERT INTO `quiz_respuesta` VALUES (732,183,'El peyote',1,'','');
INSERT INTO `quiz_respuesta` VALUES (733,184,'Realizar una ceremonia de purificación y abstinencias',1,'','');
INSERT INTO `quiz_respuesta` VALUES (734,184,'Orar a las deidades',0,'','');
INSERT INTO `quiz_respuesta` VALUES (735,184,'Realizar una ceremonia de consagración',0,'','');
INSERT INTO `quiz_respuesta` VALUES (736,184,'Bailar a las deidades',0,'','');
INSERT INTO `quiz_respuesta` VALUES (737,185,'Correr y saltar',0,'','');
INSERT INTO `quiz_respuesta` VALUES (738,185,'Vestir un atuendo especial y escuchar el canto sagrado',1,'','');
INSERT INTO `quiz_respuesta` VALUES (739,185,'Bailar y cantar',0,'','');
INSERT INTO `quiz_respuesta` VALUES (740,185,'Vestir una túnica y escuchar música',0,'','');
INSERT INTO `quiz_respuesta` VALUES (741,186,'<i>Kweetsi</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (742,186,'<i>Taraki</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (743,186,'<i>Karimutsi</i>',1,'','');
INSERT INTO `quiz_respuesta` VALUES (744,186,'<i>Tsíweri</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (745,187,'Habas',0,'Images/Wixarika/Alimentos/Habas','');
INSERT INTO `quiz_respuesta` VALUES (746,187,'Guajes',0,'Images/Wixarika/Alimentos/Guaje rojo','');
INSERT INTO `quiz_respuesta` VALUES (747,187,'Gualumbos',0,'Images/Wixarika/Alimentos/Gualumbo','');
INSERT INTO `quiz_respuesta` VALUES (748,187,'Pochote',1,'Images/Wixarika/Alimentos/Pochote','');
INSERT INTO `quiz_respuesta` VALUES (749,188,'<i>Ikɨri warikietɨ</i>',1,'','');
INSERT INTO `quiz_respuesta` VALUES (750,188,'<i>Ikɨri</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (751,188,'<i>Ikɨri kwitsarietɨ</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (752,188,'<i>Ikú</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (753,189,'<i>Tuixu</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (754,189,'<i>Wakaxi</i>',1,'','');
INSERT INTO `quiz_respuesta` VALUES (755,189,'<i>Tuixuyeutanaka</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (756,189,'<i>Wakana</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (757,190,'<i>Charcas</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (758,190,'Santo Domingo',0,'','');
INSERT INTO `quiz_respuesta` VALUES (759,190,'<i>Tuy Mayau</i>',1,'','');
INSERT INTO `quiz_respuesta` VALUES (760,190,'Villa de Ramos',0,'','');
INSERT INTO `quiz_respuesta` VALUES (761,191,'Villa de Ramos',0,'','');
INSERT INTO `quiz_respuesta` VALUES (762,191,'Charcas',0,'','');
INSERT INTO `quiz_respuesta` VALUES (763,191,'Santo Domingo',0,'','');
INSERT INTO `quiz_respuesta` VALUES (764,191,'<i>Tuy Mayau/Huacuri Quitenie</i>',1,'','');
INSERT INTO `quiz_respuesta` VALUES (765,192,'<i>Kwixɨ</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (766,192,'<i>Peexá</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (767,192,'<i>Werika</i>',1,'','');
INSERT INTO `quiz_respuesta` VALUES (768,192,'<i>Kwatsa</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (769,193,'Águila real',1,'','');
INSERT INTO `quiz_respuesta` VALUES (770,193,'Cuervo',0,'','');
INSERT INTO `quiz_respuesta` VALUES (771,193,'Pájaro',0,'','');
INSERT INTO `quiz_respuesta` VALUES (772,193,'Halcón',0,'','');
INSERT INTO `quiz_respuesta` VALUES (773,194,'Cera',0,'','');
INSERT INTO `quiz_respuesta` VALUES (774,194,'Plumas de águila real',1,'','');
INSERT INTO `quiz_respuesta` VALUES (775,194,'Madera',0,'','');
INSERT INTO `quiz_respuesta` VALUES (776,194,'Pintura',0,'','');
INSERT INTO `quiz_respuesta` VALUES (777,195,'Dinero',0,'','');
INSERT INTO `quiz_respuesta` VALUES (778,195,'Animales silvestres',0,'','');
INSERT INTO `quiz_respuesta` VALUES (779,195,'Instrumentos musicales',0,'','');
INSERT INTO `quiz_respuesta` VALUES (780,195,'Animales espirituales',1,'','');
INSERT INTO `quiz_respuesta` VALUES (781,196,'<i>El mara’kame</i>',1,'','');
INSERT INTO `quiz_respuesta` VALUES (782,196,'<i>El tatuwani</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (783,196,'<i>El xuku’uri ɨkame</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (784,196,'<i>El kawiteru</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (785,197,'Buenas cosechas y fertilidad',0,'','');
INSERT INTO `quiz_respuesta` VALUES (786,197,'Que no envíen enfermedades y que ayuden a resolver los problemas de la comunidad',1,'','');
INSERT INTO `quiz_respuesta` VALUES (787,197,'Dinero y oro',0,'','');
INSERT INTO `quiz_respuesta` VALUES (788,197,'Que llueva y no haya sequía',0,'','');
INSERT INTO `quiz_respuesta` VALUES (789,198,'Cactus',0,'','');
INSERT INTO `quiz_respuesta` VALUES (790,198,'Pochote',0,'','');
INSERT INTO `quiz_respuesta` VALUES (791,198,'<i>Peyote</i>',1,'','');
INSERT INTO `quiz_respuesta` VALUES (792,198,'Nopal',0,'','');
INSERT INTO `quiz_respuesta` VALUES (793,199,'Agua',0,'','');
INSERT INTO `quiz_respuesta` VALUES (794,199,'Cactus',0,'','');
INSERT INTO `quiz_respuesta` VALUES (795,199,'Piedras',0,'','');
INSERT INTO `quiz_respuesta` VALUES (796,199,'Peyote',1,'','');
INSERT INTO `quiz_respuesta` VALUES (797,200,'<i>Los wixaritari</i>',1,'','');
INSERT INTO `quiz_respuesta` VALUES (798,200,'Los animales',0,'','');
INSERT INTO `quiz_respuesta` VALUES (799,200,'Los mestizos',0,'','');
INSERT INTO `quiz_respuesta` VALUES (800,200,'Los cactus',0,'','');
INSERT INTO `quiz_respuesta` VALUES (801,201,'La fiesta del tambor',0,'','');
INSERT INTO `quiz_respuesta` VALUES (802,201,'La peregrinación a <i>Wirikuta</i>',1,'','');
INSERT INTO `quiz_respuesta` VALUES (803,201,'La peregrinación al centro de la tierra',0,'','');
INSERT INTO `quiz_respuesta` VALUES (804,201,'La peregrinación al mar',0,'','');
INSERT INTO `quiz_respuesta` VALUES (805,202,'El que no ve y quiere ver',0,'','');
INSERT INTO `quiz_respuesta` VALUES (806,202,'El que puede soñar',0,'','');
INSERT INTO `quiz_respuesta` VALUES (807,202,'El que no sabe y va a saber',1,'','');
INSERT INTO `quiz_respuesta` VALUES (808,202,'El que puede viajar',0,'','');
INSERT INTO `quiz_respuesta` VALUES (809,203,'Los que pueden viajar',0,'','');
INSERT INTO `quiz_respuesta` VALUES (810,203,'Quienes peregrinan por última vez',0,'','');
INSERT INTO `quiz_respuesta` VALUES (811,203,'Los que pueden soñar',0,'','');
INSERT INTO `quiz_respuesta` VALUES (812,203,'Quienes peregrinan por primera vez a <i>Wirikuta</i>',1,'','');
INSERT INTO `quiz_respuesta` VALUES (813,204,'Diosa de la lluvia del poniente',1,'','');
INSERT INTO `quiz_respuesta` VALUES (814,204,'Nuestra abuela tierra',0,'','');
INSERT INTO `quiz_respuesta` VALUES (815,204,'Nuestra madre agua sagrada',0,'','');
INSERT INTO `quiz_respuesta` VALUES (816,204,'Nuestra madre águila',0,'','');
INSERT INTO `quiz_respuesta` VALUES (817,205,'<i>Ma’ara</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (818,205,'<i>Narakaxi</i>',1,'','');
INSERT INTO `quiz_respuesta` VALUES (819,205,'<i>Ka’arú</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (820,205,'<i>Ha’yewaxi</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (821,206,'Pitahaya',0,'Images/Wixarika/Alimentos/Pitahaya','');
INSERT INTO `quiz_respuesta` VALUES (822,206,'Guayaba',0,'Images/Wixarika/Alimentos/Guayaba 1','');
INSERT INTO `quiz_respuesta` VALUES (823,206,'Naranja',1,'Images/Wixarika/Alimentos/Naranja','');
INSERT INTO `quiz_respuesta` VALUES (824,206,'Plátano',0,'Images/Wixarika/Alimentos/Plátano','');
INSERT INTO `quiz_respuesta` VALUES (825,207,'<i>Uwakí</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (826,207,'<i>Yɨɨna</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (827,207,'<i>Kwarɨpa</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (828,207,'<i>Piní</i>',1,'','');
INSERT INTO `quiz_respuesta` VALUES (829,208,'Higo',1,'Images/Wixarika/Alimentos/Higo','');
INSERT INTO `quiz_respuesta` VALUES (830,208,'Ciruela',0,'Images/Wixarika/Alimentos/Ciruela española','');
INSERT INTO `quiz_respuesta` VALUES (831,208,'Tuna',0,'Images/Wixarika/Alimentos/Tuna','');
INSERT INTO `quiz_respuesta` VALUES (832,208,'Nanchi',0,'Images/Wixarika/Alimentos/Nanchi 2','');
INSERT INTO `quiz_respuesta` VALUES (833,209,'<i>Hamuitsi</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (834,209,'<i>Narakaxi hayaári</i>',1,'','');
INSERT INTO `quiz_respuesta` VALUES (835,209,'<i>Nawá</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (836,209,'<i>Uwá hayaári</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (837,210,'<i>Taku</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (838,210,'<i>Kexiu</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (839,210,'<i>Xupaxi</i>',1,'','');
INSERT INTO `quiz_respuesta` VALUES (840,210,'<i>Tétsu</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (841,211,'Un centro ceremonial',0,'','');
INSERT INTO `quiz_respuesta` VALUES (842,211,'Una artesanía <i>Wixárika</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (843,211,'Un sitio sagrado',0,'','');
INSERT INTO `quiz_respuesta` VALUES (844,211,'Una ofrenda para las deidades',1,'','');
INSERT INTO `quiz_respuesta` VALUES (845,212,'El mundo de las deidades y la cosmovisión del pueblo Wixárika',1,'','');
INSERT INTO `quiz_respuesta` VALUES (846,212,'El viaje al inframundo',0,'','');
INSERT INTO `quiz_respuesta` VALUES (847,212,'El mundo de las sombras y la cosmovisión del inframundo',0,'','');
INSERT INTO `quiz_respuesta` VALUES (848,212,'El sueño <i>Wixárika</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (849,213,'Como regalo',0,'','');
INSERT INTO `quiz_respuesta` VALUES (850,213,'Para conocer el estado oculto o auténtico de las cosas',1,'','');
INSERT INTO `quiz_respuesta` VALUES (851,213,'Como cuadro de decoración',0,'','');
INSERT INTO `quiz_respuesta` VALUES (852,213,'Para cortar alimentos',0,'','');
INSERT INTO `quiz_respuesta` VALUES (853,214,'<i>Charcas</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (854,214,'<i>Wirikuta</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (855,214,'Santo Domingo',1,'','');
INSERT INTO `quiz_respuesta` VALUES (856,214,'Real de Catorce',0,'','');
INSERT INTO `quiz_respuesta` VALUES (857,215,'Nuestro abuelo fuego',0,'','');
INSERT INTO `quiz_respuesta` VALUES (858,215,'Diosa de la lluvia del poniente',0,'','');
INSERT INTO `quiz_respuesta` VALUES (859,215,'Nuestra madre agua sagrada',0,'','');
INSERT INTO `quiz_respuesta` VALUES (860,215,'Nuestro bisabuelo cola de venado',1,'','');
INSERT INTO `quiz_respuesta` VALUES (861,216,'Al universo',1,'','');
INSERT INTO `quiz_respuesta` VALUES (862,216,'Al cielo',0,'','');
INSERT INTO `quiz_respuesta` VALUES (863,216,'Al infinito',0,'','');
INSERT INTO `quiz_respuesta` VALUES (864,216,'A la tierra',0,'','');
INSERT INTO `quiz_respuesta` VALUES (865,217,'Nuestra madre agua sagrada',0,'','');
INSERT INTO `quiz_respuesta` VALUES (866,217,'Nuestro padre el Sol',1,'','');
INSERT INTO `quiz_respuesta` VALUES (867,217,'Diosa de la lluvia del poniente',0,'','');
INSERT INTO `quiz_respuesta` VALUES (868,217,'Nuestro bisabuelo cola de venado',0,'','');
INSERT INTO `quiz_respuesta` VALUES (869,218,'Zacatecas',0,'','');
INSERT INTO `quiz_respuesta` VALUES (870,218,'Tepic',0,'','');
INSERT INTO `quiz_respuesta` VALUES (871,218,'<i>Wirikuta</i>',1,'','');
INSERT INTO `quiz_respuesta` VALUES (872,218,'Durango',0,'','');
INSERT INTO `quiz_respuesta` VALUES (873,219,'Tepic',0,'','');
INSERT INTO `quiz_respuesta` VALUES (874,219,'Durango',0,'','');
INSERT INTO `quiz_respuesta` VALUES (875,219,'Zacatecas',0,'','');
INSERT INTO `quiz_respuesta` VALUES (876,219,'<i>Hauxa Manaka</i>',1,'','');
INSERT INTO `quiz_respuesta` VALUES (877,220,'Pintura, estambre, plumas u objetos',1,'','');
INSERT INTO `quiz_respuesta` VALUES (878,220,'Plumones',0,'','');
INSERT INTO `quiz_respuesta` VALUES (879,220,'Chaquira, listones y telas',0,'','');
INSERT INTO `quiz_respuesta` VALUES (880,220,'Colores',0,'','');
INSERT INTO `quiz_respuesta` VALUES (881,221,'En la Laguna de Chapala',0,'','');
INSERT INTO `quiz_respuesta` VALUES (882,221,'En el desierto de Wirikuta, en Reunari (Cerro Quemado)',1,'','');
INSERT INTO `quiz_respuesta` VALUES (883,221,'En el Río Santiago',0,'','');
INSERT INTO `quiz_respuesta` VALUES (884,221,'En el desierto de Sonora',0,'','');
INSERT INTO `quiz_respuesta` VALUES (885,222,'En los pozos',0,'','');
INSERT INTO `quiz_respuesta` VALUES (886,222,'En el centro',0,'','');
INSERT INTO `quiz_respuesta` VALUES (887,222,'En las cuevas sagradas de donde salieron los antepasados',1,'','');
INSERT INTO `quiz_respuesta` VALUES (888,222,'En los campos',0,'','');
INSERT INTO `quiz_respuesta` VALUES (889,223,'Una cena de agradecimiento',0,'','');
INSERT INTO `quiz_respuesta` VALUES (890,223,'Una ceremonia de bautizo',0,'','');
INSERT INTO `quiz_respuesta` VALUES (891,223,'Una oración',0,'','');
INSERT INTO `quiz_respuesta` VALUES (892,223,'Una ceremonia de representación del nacimiento de los ancestros',1,'','');
INSERT INTO `quiz_respuesta` VALUES (893,224,'A los otros sitios sagrados',1,'','');
INSERT INTO `quiz_respuesta` VALUES (894,224,'A los tukipa',0,'','');
INSERT INTO `quiz_respuesta` VALUES (895,224,'A sus casas',0,'','');
INSERT INTO `quiz_respuesta` VALUES (896,224,'A las iglesias',0,'','');
INSERT INTO `quiz_respuesta` VALUES (897,225,'<i>Ikú</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (898,225,'<i>Ikú tuuxá</i>',1,'','');
INSERT INTO `quiz_respuesta` VALUES (899,225,'<i>Ikɨri</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (900,225,'<i>Iku’ yuawime</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (901,226,'Maíz azul',0,'','');
INSERT INTO `quiz_respuesta` VALUES (902,226,'Maíz amarillo',0,'','');
INSERT INTO `quiz_respuesta` VALUES (903,226,'Maíz blanco',1,'','');
INSERT INTO `quiz_respuesta` VALUES (904,226,'Maíz rojo',0,'','');
INSERT INTO `quiz_respuesta` VALUES (905,227,'<i>Paní</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (906,227,'<i>Tsakaka</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (907,227,'<i>Xiete</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (908,227,'<i>Pexúri</i>',1,'','');
INSERT INTO `quiz_respuesta` VALUES (909,228,'<i>Uwá hayaári</i>',1,'','');
INSERT INTO `quiz_respuesta` VALUES (910,228,'<i>Kamaika hayaári</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (911,228,'<i>Narakaxi hayaári</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (912,228,'<i>Hayaári</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (913,229,'<i>Charcas</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (914,229,'San Antonio de Padua',1,'','');
INSERT INTO `quiz_respuesta` VALUES (915,229,'Real de Catorce',0,'','');
INSERT INTO `quiz_respuesta` VALUES (916,229,'Mezquital',0,'','');
INSERT INTO `quiz_respuesta` VALUES (917,230,'<i>Tiɨkitame</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (918,230,'<i>Tupiri</i>',1,'','');
INSERT INTO `quiz_respuesta` VALUES (919,230,'<i>Tiyuuayewamame</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (920,230,'<i>Tituayame</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (921,231,'Doctores',0,'','');
INSERT INTO `quiz_respuesta` VALUES (922,231,'Gobernadores',0,'','');
INSERT INTO `quiz_respuesta` VALUES (923,231,'Maestros',0,'','');
INSERT INTO `quiz_respuesta` VALUES (924,231,'Policías',1,'','');
INSERT INTO `quiz_respuesta` VALUES (925,232,'Por ser un plaguicida natural',1,'','');
INSERT INTO `quiz_respuesta` VALUES (926,232,'Por sus vitaminas',0,'','');
INSERT INTO `quiz_respuesta` VALUES (927,232,'Por ser bonitas',0,'','');
INSERT INTO `quiz_respuesta` VALUES (928,232,'Por sus colores',0,'','');
INSERT INTO `quiz_respuesta` VALUES (929,233,'Para que adornen el coamil',0,'','');
INSERT INTO `quiz_respuesta` VALUES (930,233,'Para que aporten nitrógeno al suelo',1,'','');
INSERT INTO `quiz_respuesta` VALUES (931,233,'Para alimentar a las aves',0,'','');
INSERT INTO `quiz_respuesta` VALUES (932,233,'Para espantar animales salvajes',0,'','');
INSERT INTO `quiz_respuesta` VALUES (933,234,'Para alimentar a las aves',0,'','');
INSERT INTO `quiz_respuesta` VALUES (934,234,'Para espantar animales salvajes',0,'','');
INSERT INTO `quiz_respuesta` VALUES (935,234,'Para que sus hojas protejan al suelo de la erosión',1,'','');
INSERT INTO `quiz_respuesta` VALUES (936,234,'Para que adornen el coamil',0,'','');
INSERT INTO `quiz_respuesta` VALUES (937,235,'7',0,'','');
INSERT INTO `quiz_respuesta` VALUES (938,235,'3',0,'','');
INSERT INTO `quiz_respuesta` VALUES (939,235,'1',0,'','');
INSERT INTO `quiz_respuesta` VALUES (940,235,'5',1,'','');
INSERT INTO `quiz_respuesta` VALUES (941,236,'<i>Yekwa</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (942,236,'<i>Aɨraxa</i>',1,'','');
INSERT INTO `quiz_respuesta` VALUES (943,236,'<i>Na’akari</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (944,236,'<i>Xútsi</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (945,237,'<i>Xutsíte</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (946,237,'<i>Aɨraxate</i>',1,'','');
INSERT INTO `quiz_respuesta` VALUES (947,237,'<i>Yekwa’ate</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (948,237,'<i>Na’akarite</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (949,238,'Nopal',0,'Images/Wixarika/Alimentos/Nopal 1 1x','');
INSERT INTO `quiz_respuesta` VALUES (950,238,'Verdolaga',1,'Images/Wixarika/Alimentos/Verdolaga','');
INSERT INTO `quiz_respuesta` VALUES (951,238,'Hongos',0,'Images/Wixarika/Alimentos/Hongo','');
INSERT INTO `quiz_respuesta` VALUES (952,238,'Calabacita',0,'Images/Wixarika/Alimentos/Calabacitas','');
INSERT INTO `quiz_respuesta` VALUES (953,239,'<i>Ké’uxa</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (954,239,'<i>Ikú</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (955,239,'<i>Aɨraxa</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (956,239,'<i>Haxi</i>',1,'','');
INSERT INTO `quiz_respuesta` VALUES (957,240,'Maíz',0,'Images/Wixarika/Alimentos/Maíz blanco','');
INSERT INTO `quiz_respuesta` VALUES (958,240,'Quelite',0,'Images/Wixarika/Alimentos/Quelites','');
INSERT INTO `quiz_respuesta` VALUES (959,240,'Guaje',1,'Images/Wixarika/Alimentos/Guaje rojo','');
INSERT INTO `quiz_respuesta` VALUES (960,240,'Verdolaga',0,'Images/Wixarika/Alimentos/Jícama','');
INSERT INTO `quiz_respuesta` VALUES (961,241,'<i>Pa’apa</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (962,241,'Quesadilla con aɨraxate',1,'','');
INSERT INTO `quiz_respuesta` VALUES (963,241,'<i>Kexiu</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (964,241,'Quesadilla con yekwa’ate',0,'','');
INSERT INTO `quiz_respuesta` VALUES (965,242,'<i>Eɨkariti</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (966,242,'<i>Wáxa</i>',0,'','');
INSERT INTO `quiz_respuesta` VALUES (967,242,'<i>Orekanu</i>',1,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (968,242,'<i>Hikuri</i>',0,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (969,243,'<i>Hatsaruni</i>',0,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (970,243,'<i>Nawaxa</i>',0,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (971,243,'<i>Mɨtsɨtɨari</i>',0,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (972,243,'<i>Tɨɨpi</i>',1,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (973,244,'<i>Ɨ’rɨ</i>',1,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (974,244,'<i>Kɨyé</i>',0,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (975,244,'<i>Ha’úte</i>',0,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (976,244,'<i>Kaunari</i>',0,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (977,245,'<i>Kaunarite</i>',0,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (978,245,'<i>Ɨ’rɨte</i>',1,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (979,245,'<i>Kɨyéxi</i>',0,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (980,245,'<i>Ha’utete</i>',0,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (981,246,'Para la agricultura',0,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (982,246,'Para la pesca',0,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (983,246,'Para la cacería',1,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (984,246,'Para las artesanías',0,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (985,247,'Pueblo nuevo',0,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (986,247,'Durango',0,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (987,247,'Mezquital',0,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (988,247,'San Bernardino de Milpillas',1,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (989,248,'La cacería ritual del venado',1,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (990,248,'La tradición de los elotes',0,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (991,248,'La cacería del lobo',0,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (992,248,'Las ceremonias del mar',0,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (993,249,'3',0,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (994,249,'5',1,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (995,249,'7',0,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (996,249,'4',0,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (997,250,'Una dirección del camino',0,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (998,250,'Un animal silvestre',0,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (999,250,'Una dirección del universo y un animal',1,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1000,250,'Una deidad',0,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1001,251,'<i>Aɨraxa</i>',0,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1002,251,'<i>Xútsi</i>',0,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1003,251,'<i>Na’akari</i>',1,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1004,251,'<i>Yekwa</i>',0,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1005,252,'Calabacita',0,'Images/Wixarika/Alimentos/Jícama',NULL);
INSERT INTO `quiz_respuesta` VALUES (1006,252,'Verdolaga',0,'Images/Wixarika/Alimentos/Verdolaga',NULL);
INSERT INTO `quiz_respuesta` VALUES (1007,252,'Champiñón',0,'Images/Wixarika/Alimentos/Hongo',NULL);
INSERT INTO `quiz_respuesta` VALUES (1008,252,'Nopal',1,'Images/Wixarika/Alimentos/Nopal 1 1x',NULL);
INSERT INTO `quiz_respuesta` VALUES (1009,253,'<i>Yɨɨna</i>',0,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1010,253,'<i>Uwakí</i>',0,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1011,253,'<i>Kwarɨpa</i>',0,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1012,253,'<i>Muxu’uri</i>',1,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1013,254,'Nanchi',0,'Images/Wixarika/Alimentos/Nanchi 2',NULL);
INSERT INTO `quiz_respuesta` VALUES (1014,254,'Ciruela',0,'Images/Wixarika/Alimentos/Ciruela española',NULL);
INSERT INTO `quiz_respuesta` VALUES (1015,254,'Guamúchil',1,'Images/Wixarika/Alimentos/Guamúchil',NULL);
INSERT INTO `quiz_respuesta` VALUES (1016,254,'Tuna',0,'Images/Wixarika/Alimentos/Tuna',NULL);
INSERT INTO `quiz_respuesta` VALUES (1017,255,'<i>Múme kwakwaxitɨ</i>',1,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1018,255,'<i>Ikɨri wawarikitɨka</i>',0,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1019,255,'<i>Kukuríte tsunariyari</i>',0,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1020,255,'<i>Ikɨri kwitsarietɨka</i>',0,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1021,256,'<i>Ikɨri kwitsarietɨka</i>',0,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1022,256,'<i>Múme wiyamarietɨka</i>',1,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1023,256,'<i>Ikɨri wawarikitɨka</i>',0,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1024,256,'<i>Kukuríte tsunariyari</i>',0,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1025,257,'<i>Kɨrapu</i>',0,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1026,257,'<i>Mantsaniya</i>',0,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1027,257,'<i>Eɨkariti</i>',1,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1028,257,'<i>Yervawena</i>',0,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1029,258,'Pueblo nuevo',0,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1030,258,'Durango',0,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1031,258,'Mezquital',0,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1032,258,'Cinco de Mayo',1,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1033,259,'<i>Haikɨ</i>',1,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1034,259,'<i>Imukwi</i>',0,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1035,259,'<i>Ɨkwi</i>',0,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1036,259,'<i>Ke’etsé</i>',0,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1037,260,'Vaca',0,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1038,260,'Serpiente azul',1,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1039,260,'Pollo',0,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1040,260,'Cerdo',0,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1041,261,'Maestro',0,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1042,261,'Coamilero',0,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1043,261,'Artesano',1,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1044,261,'Chamán',0,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1045,262,'Pantalones cortos y chalecos de manta',0,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1046,262,'Pantalón de mezclilla y camisa de algodón',0,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1047,262,'Túnicas de manta bordadas',0,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1048,262,'Pantalón y camisa de manta bordada',1,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1049,263,'Sombrero, morral, faja y huaraches',1,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1050,263,'Pulseras y collares',0,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1051,263,'Sombrero, pañuelo y huaraches',0,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1052,263,'Bufanda y guantes',0,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1053,264,'1',0,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1054,264,'5',1,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1055,264,'2',0,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1056,264,'7',0,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1057,265,'<i>Tayau</i>',0,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1058,265,'<i>Tatei Yurienáka</i>',0,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1059,265,'<i>Tamatzi Kauyumárie</i>',1,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1060,265,'<i>Tatei Kutsaraɨpa</i>',0,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1061,266,'<i>Na’akarite</i>',0,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1062,266,'<i>Xata’ate</i>',0,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1063,266,'<i>Túmatite</i>',0,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1064,266,'<i>Ké’uxate</i>',1,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1065,267,'Quelites',1,'Images/Wixarika/Alimentos/Quelites',NULL);
INSERT INTO `quiz_respuesta` VALUES (1066,267,'Nopales',0,'Images/Wixarika/Alimentos/Nopal',NULL);
INSERT INTO `quiz_respuesta` VALUES (1067,267,'Jitomates',0,'Images/Wixarika/Alimentos/Jitomate',NULL);
INSERT INTO `quiz_respuesta` VALUES (1068,267,'Jícamas',0,'Images/Wixarika/Alimentos/Jícama',NULL);
INSERT INTO `quiz_respuesta` VALUES (1069,268,'<i>Ye’erite</i>',0,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1070,268,'<i>Xutsi hatsiyarite</i>',1,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1071,268,'<i>Haxite</i>',0,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1072,268,'<i>Xutsíte</i>',0,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1073,269,'Guajes',0,'Images/Wixarika/Alimentos/Guaje rojo',NULL);
INSERT INTO `quiz_respuesta` VALUES (1074,269,'Calabacitas',0,'Images/Wixarika/Alimentos/Calabacitas',NULL);
INSERT INTO `quiz_respuesta` VALUES (1075,269,'Semillas de calabaza',1,'Images/Wixarika/Alimentos/Semillas de calabaza',NULL);
INSERT INTO `quiz_respuesta` VALUES (1076,269,'Hongos',0,'Images/Wixarika/Alimentos/Hongo',NULL);
INSERT INTO `quiz_respuesta` VALUES (1077,270,'<i>Ha’a</i>',0,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1078,270,'<i>Tsinari</i>',0,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1079,270,'<i>Nawá</i>',0,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1080,270,'<i>Hamuitsi</i>',1,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1081,271,'<i>Paní</i>',1,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1082,271,'<i>Ikɨri paniyari</i>',0,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1083,271,'<i>Ruritse</i>',0,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1084,271,'<i>Pexúri</i>',0,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1085,272,'Un sitio sagrado',0,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1086,272,'Una ofrenda para las deidades',1,NULL,'-');
INSERT INTO `quiz_respuesta` VALUES (1087,272,'Un centro ceremonial',0,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1088,272,'Una artesanía Wixárika',0,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1089,273,'A lo masculino',0,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1090,274,'El día y la noche',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1091,274,'Depósito de tierra',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1092,274,'Lo bueno y lo malo',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1093,274,'Depósito de vida',1,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1094,275,'<i>Hauxa Manaka</i>',1,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1095,275,'Durango',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1096,275,'Canatlán',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1097,275,'Pueblo nuevo',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1098,276,'Nuestra madre agua sagrada',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1099,276,'Nuestra madre tierra',1,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1100,276,'Nuestro padre el Sol',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1101,276,'Nuestra madre el mar',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1102,277,'La abundancia del campo',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1103,277,'Las intensas lluvias',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1104,277,'La fertilidad del suelo',1,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1105,277,'Las sequías',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1106,278,'Con una veladora',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1107,278,'Con una vasija de porcelana',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1108,278,'Con un ojo de dios',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1109,278,'Con un cántaro de barro',1,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1110,279,'La cosecha de los primeros frutos',1,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1111,279,'La temporada de lluvias',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1112,279,'La recolección de hongos',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1113,279,'El inicio de la primevera',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1114,280,'Quesadillas y chicuatol',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1115,280,'Tamales y tejuino',1,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1116,280,'Pozole y agua',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1117,280,'Pan y atole',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1118,281,'Los agricultores',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1119,281,'Los perros y gatos',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1120,281,'Los niños y niñas',1,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1121,281,'El ganado',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1122,282,'<i>Watakame</i>',1,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1123,282,'<i>Tayau</i>',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1124,282,'<i>Muwieri</i>',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1125,282,'<i>Tatewari</i>',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1126,283,'Chocolates y dulces',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1127,283,'Maíz y frijoles',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1128,283,'Monedas de oro',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1129,283,'Maíz, una brasa y a su perra negra',1,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1130,284,'Bicicleta',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1131,284,'Canoa',1,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1132,284,'Tren',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1133,284,'Carreta',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1134,285,'<i>Ikú tuuxá</i>',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1135,285,'<i>Ikú</i>',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1136,285,'<i>Ikú mɨxeta</i>',1,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1137,285,'<i>Ikɨri</i>',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1138,286,'<i>Na’akari</i>',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1139,286,'<i>Yekwa</i>',1,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1140,286,'<i>Aɨraxa</i>',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1141,286,'<i>Xútsi</i>',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1142,287,'<i>Yekwa’ate</i>',1,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1143,287,'<i>Kukuríte</i>',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1144,287,'<i>Xata’ate</i>',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1145,287,'<i>Túmatite</i>',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1146,288,'Nopales',0,'Images/Wixarika/Alimentos/Nopal','');
INSERT INTO `quiz_respuesta` VALUES (1147,288,'Calabacitas',0,'Images/Wixarika/Alimentos/Calabacitas','');
INSERT INTO `quiz_respuesta` VALUES (1148,288,'Verdolagas',0,'Images/Wixarika/Alimentos/Verdolaga','');
INSERT INTO `quiz_respuesta` VALUES (1149,288,'Hongos',1,'Images/Wixarika/Alimentos/Hongo','');
INSERT INTO `quiz_respuesta` VALUES (1150,289,'Quesadilla con aɨraxate',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1151,289,'<i>Yekwa’ate itsari</i>',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1152,289,'Quesadilla con <i>yekwa’ate</i>',1,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1153,289,'<i>Yekwa’ate</i>',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1154,290,'<i>Eɨkariti</i>',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1155,290,'<i>Uyuri</i>',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1156,290,'<i>Orekanu</i>',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1157,290,'<i>Kɨrapu</i>',1,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1158,291,'<i>Mɨtsɨtɨari</i>',1,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1159,291,'<i>Kutsira</i>',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1160,291,'<i>Nawaxa</i>',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1161,291,'<i>Hatsaruni</i>',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1162,292,'Durango',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1163,292,'Colonia Hatmasie',1,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1164,292,'Jalisco',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1165,292,'Huejiquilla',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1166,293,'<i>Mara’kame</i>',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1167,293,'<i>Xuku’uri ɨkame</i>',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1168,293,'<i>Tatuwani</i>',1,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1169,293,'<i>Kawiteru</i>',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1170,294,'Los policías',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1171,294,'Los chamanes',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1172,294,'El pueblo',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1173,294,'Los ancianos sabios',1,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1174,295,'Pequeño violín, pequeña guitarra y maracas',1,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1175,295,'Flauta y armónica',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1176,295,'Pequeño tambor y pequeño piano',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1177,295,'Marimba',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1178,296,'7',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1179,296,'5',1,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1180,296,'3',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1181,296,'4',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1182,297,'Deidades masculinas',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1183,297,'Deidades cazadoras',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1184,297,'Deidades femeninas',1,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1185,297,'Deidades mensajeras',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1186,298,'<i>Ha’yewaxi</i>',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1187,298,'<i>Ma’ara</i>',1,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1188,298,'<i>Ma’aku</i>',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1189,298,'<i>Narakaxi</i>',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1190,299,'Mango',0,'Images/Wixarika/Alimentos/Mango','');
INSERT INTO `quiz_respuesta` VALUES (1191,299,'Guayaba',0,'Images/Wixarika/Alimentos/Guayaba 1','');
INSERT INTO `quiz_respuesta` VALUES (1192,299,'Naranja',0,'Images/Wixarika/Alimentos/Naranja','');
INSERT INTO `quiz_respuesta` VALUES (1193,299,'Pitahaya',1,'Images/Wixarika/Alimentos/Pitahaya','');
INSERT INTO `quiz_respuesta` VALUES (1194,300,'<i>Ma’ara</i>',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1195,300,'<i>Ha’yewaxi</i>',1,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1196,300,'<i>Kwarɨpa</i>',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1197,300,'<i>Ma’aku</i>',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1198,301,'Pitahaya',0,'Images/Wixarika/Alimentos/Pitahaya','');
INSERT INTO `quiz_respuesta` VALUES (1199,301,'Mango',0,'Images/Wixarika/Alimentos/Mango','');
INSERT INTO `quiz_respuesta` VALUES (1200,301,'Guayaba',1,'Images/Wixarika/Alimentos/Guayaba 1','');
INSERT INTO `quiz_respuesta` VALUES (1201,301,'Ciruela',0,'Images/Wixarika/Alimentos/Ciruela española','');
INSERT INTO `quiz_respuesta` VALUES (1202,302,'<i>Itsari</i>',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1203,302,'<i>Xupaxi</i>',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1204,302,'<i>kwitsari</i>',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1205,302,'<i>Tsuirá</i>',1,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1206,303,'<i>Te’akata</i>',1,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1207,303,'Huejiquilla',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1208,303,'<i>Mezquitic</i>',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1209,303,'Jalisco',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1210,304,'Jalisco',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1211,304,'Santa Catarina Cuexcomatitlán',1,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1212,304,'<i>Mezquitic</i>',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1213,304,'Huejiquilla',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1214,305,'Huejiquilla',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1215,305,'Jalisco',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1216,305,'San Sebastián Teponahuaxtlán',1,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1217,305,'<i>Mezquitic</i>',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1218,306,'Nuestro padre el Sol',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1219,306,'Dios del fuego primigenio',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1220,306,'Nuestra madre el mar',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1221,306,'Nuestro abuelo fuego',1,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1222,307,'<i>Tatewari</i>',1,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1223,307,'<i>Tsipúrawi</i>',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1224,307,'<i>Tayau</i>',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1225,307,'<i>Wewetsári</i>',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1226,308,'<i>Tsipúrawi</i>',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1227,308,'<i>Tatewari</i>',1,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1228,308,'<i>Wewetsári</i>',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1229,308,'<i>Tayau</i>',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1230,309,'Fiesta del piano',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1231,309,'Celebración de los muertos',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1232,309,'Fiesta del tambor',1,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1233,309,'Celebración de año nuevo',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1234,310,'Para invocar la lluvia',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1235,310,'Para que se asiente el corazón en los niños',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1236,310,'Para que haya buenas cosechas',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1237,310,'Para que se asiente el kupuri (aliento) en los niños',1,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1238,311,'Águilas',1,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1239,311,'Venados',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1240,311,'Perros',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1241,311,'Jabalíes',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1242,312,'Pastel de elote',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1243,312,'Elote tatemado',1,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1244,312,'Atole de ciruela',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1245,312,'Pan de elote',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1246,313,'La lluvia',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1247,313,'El agua',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1248,313,'La luna',1,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1249,313,'La naturaleza',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1250,314,'<i>Wirikuta</i>',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1251,314,'<i>Makuipa</i>',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1252,314,'<i>Xapawiyemeta</i>',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1253,314,'<i>Te’akata</i>',1,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1254,315,'<i>Tayau</i>',1,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1255,315,'<i>Utútawi</i>',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1256,315,'<i>Tatewari</i>',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1257,315,'<i>Wewetsári</i>',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1258,316,'<i>Ha’a</i>',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1259,316,'<i>Taú</i>',1,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1260,316,'<i>Mimierika</i>',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1261,316,'<i>Tái</i>',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1262,317,'<i>Taú</i>',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1263,317,'<i>Mimierika</i>',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1264,317,'<i>Tái</i>',1,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1265,317,'<i>Ha’a</i>',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1266,318,'<i>Unétsi</i>(bebés)',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1267,318,'<i>Neɨkixiwi</i>(enemigos)',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1268,318,'<i>Ne aurie muka</i> (vecinos)',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1269,318,'<i>Hewiixi</i> (caníbales)',1,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1270,319,'Tlacuache',1,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1271,319,'Zorrillo',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1272,319,'Armadillo',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1273,319,'Zorro',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1274,320,'Con las deidades',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1275,320,'Con las comunidades Wixárika',1,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1276,320,'Con los animales',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1277,320,'Con los mestizos',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1278,321,'<i>Hauxa Manaka</i>',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1279,321,'<i>Xapawiyemeta</i>',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1280,321,'<i>Te’akata</i>',1,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1281,321,'<i>Makuipa</i>',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1282,322,'<i>Wáxa</i>',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1283,322,'<i>Ha’yewaxi</i>',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1284,322,'<i>Ma’ara</i>',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1285,322,'<i>Kamaika</i>',1,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1286,323,'Guayaba',0,'Images/Wixarika/Alimentos/Guayaba 1','');
INSERT INTO `quiz_respuesta` VALUES (1287,323,'Pitahaya',0,'Images/Wixarika/Alimentos/Pitahaya','');
INSERT INTO `quiz_respuesta` VALUES (1288,323,'Jamaica',1,'Images/Wixarika/Alimentos/Jamaica','');
INSERT INTO `quiz_respuesta` VALUES (1289,323,'Milpa',0,'Images/Wixarika/Alimentos/Milpa 2 3x','');
INSERT INTO `quiz_respuesta` VALUES (1290,324,'<i>Túmati</i>',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1291,324,'<i>Uyuri</i>',1,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1292,324,'<i>Tsinakari</i>',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1293,324,'<i>Xútsi</i>',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1294,325,'Calabacita',0,'Images/Wixarika/Alimentos/Calabacitas','');
INSERT INTO `quiz_respuesta` VALUES (1295,325,'Limón',0,'Images/Wixarika/Alimentos/Limón','');
INSERT INTO `quiz_respuesta` VALUES (1296,325,'Cebolla',1,'Images/Wixarika/Alimentos/Cebolla','');
INSERT INTO `quiz_respuesta` VALUES (1297,325,'Jitomate',0,'Images/Wixarika/Alimentos/Jitomate','');
INSERT INTO `quiz_respuesta` VALUES (1298,326,'<i>Xupaxi</i>',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1299,326,'<i>Tétsu</i>',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1300,326,'<i>Itsari</i>',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1301,326,'<i>kwitsari</i>',1,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1302,327,'<i>Kamaika hayaári</i>',1,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1303,327,'<i>Uwá hayaári</i>',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1304,327,'<i>Hayaári</i>',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1305,327,'<i>Narakaxi hayaári</i>',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1306,328,'<i>Kɨrapu</i>',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1307,328,'<i>Yervawena</i>',1,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1308,328,'<i>Orekanu</i>',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1309,328,'<i>Mantsaniya</i>',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1310,329,'Bolaños',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1311,329,'<i>Mezquitic</i>',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1312,329,'Tuxpan de Bolaños',1,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1313,329,'Huejiquilla',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1314,330,'Tlacuache',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1315,330,'Zorrillo',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1316,330,'Gato',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1317,330,'Zorro',1,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1318,331,'Falda, camisa y zapatos',1,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1319,331,'Pantalón, playera y zapatos',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1320,331,'Short, camisa y huraches',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1321,331,'Vestido y huaraches',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1322,332,'Gorro, bufanda y guantes',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1323,332,'Collares, pulseras y aretes',1,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1324,332,'Diademas y listones',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1325,332,'Relojes y anillos',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1326,333,'Mochila',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1327,333,'Bufanda',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1328,333,'Morral',1,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1329,333,'Gorro',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1330,334,'<i>Yekwa</i>',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1331,334,'<i>Na’akari</i>',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1332,334,'<i>Xútsi</i>',1,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1333,334,'<i>Aɨraxa</i>',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1334,335,'Calabacita',1,'Images/Wixarika/Alimentos/Calabacitas','');
INSERT INTO `quiz_respuesta` VALUES (1335,335,'Champiñón',0,'Images/Wixarika/Alimentos/Hongo','');
INSERT INTO `quiz_respuesta` VALUES (1336,335,'Verdolaga',0,'Images/Wixarika/Alimentos/Verdolaga','');
INSERT INTO `quiz_respuesta` VALUES (1337,335,'Nopal',0,'Images/Wixarika/Alimentos/Nopal','');
INSERT INTO `quiz_respuesta` VALUES (1338,336,'<i>Tsinakari</i>',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1339,336,'<i>Túmati</i>',1,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1340,336,'vestido y huaraches',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1341,336,'<i>Haxi</i>',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1342,337,'Jícama',0,'Images/Wixarika/Alimentos/Jícama','');
INSERT INTO `quiz_respuesta` VALUES (1343,337,'Limón',0,'Images/Wixarika/Alimentos/Limón','');
INSERT INTO `quiz_respuesta` VALUES (1344,337,'Guaje',1,'Images/Wixarika/Alimentos/Guaje rojo','');
INSERT INTO `quiz_respuesta` VALUES (1345,337,'Jitomate',0,'Images/Wixarika/Alimentos/Jitomate','');
INSERT INTO `quiz_respuesta` VALUES (1346,338,'<i>Tsinari</i>',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1347,338,'<i>Retsi</i>',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1348,338,'<i>Hayaári</i>',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1349,338,'<i>Nawá</i>',1,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1350,339,'Una ofrenda para las deidades',1,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1351,339,'Un centro ceremonial',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1352,339,'Una artesanía Wixárika',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1353,339,'Un sitio sagrado',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1354,340,'La noche',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1355,340,'Lo masculino',1,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1356,340,'El día',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1357,340,'Lo femenino',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1358,341,'Adornos',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1359,341,'Mensajes',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1360,341,'Cazar al venado y luchar con la oscuridad',1,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1361,341,'Decoración',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1362,342,'Decoración',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1363,342,'Adornos',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1364,342,'Mensajes',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1365,342,'Ofrendas, cazar y enviar castigos',1,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1366,343,'<i>Xapawiyemeta</i>',1,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1367,343,'<i>Mezquitic</i>',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1368,343,'Chapala',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1369,343,'Bolaños',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1370,344,'Nuestra madre el mar',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1371,344,'Diosa madre del sur',1,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1372,344,'Nuestra madre tierra',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1373,344,'Nuestra madre agua sagrada',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1374,345,'Máscaras, capas y centros',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1375,345,'Arcos y flechas',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1376,345,'Sonajas, ojos de dios y flechas',1,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1377,345,'Tablillas y jicaras',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1378,346,'En la casa del abuelo paterno del niño o niña',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1379,346,'En el <i>tukipa (centro ceremonial) de origen del abuelo paterno del niño o niña',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1380,346,'En la casa del abuelo materno del niño o niña',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1381,346,'En el <i>tukipa (centro ceremonial) de origen del abuelo materno del niño o niña',1,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1382,347,'Porque regalan tambores',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1383,347,'Por los primeros frutos',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1384,347,'Por el uso constante de tambores',1,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1385,347,'Por la despedida de las lluvias',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1386,348,'9',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1387,348,'5',1,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1388,348,'2',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1389,348,'6',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1390,349,'<i>Taku</i>',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1391,349,'<i>Pexuri</i>',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1392,349,'<i>Tétsu</i>',1,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1393,349,'<i>kwitsari</i>',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1394,350,'<i>Untsa</i>',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1395,350,'<i>Kauxai</i>',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1396,350,'<i>Yáavi</i>',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1397,350,'<i>Ɨxawe</i>',1,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1398,351,'<i>Uxa</i>',1,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1399,351,'<i>Hikuri</i>',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1400,351,'<i>Ɨpá</i>',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1401,351,'<i>Kɨrapu</i>',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1402,352,'<i>Imúmui</i>',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1403,352,'<i>Utsí</i>',1,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1404,352,'<i>Ha’tsa</i>',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1405,352,'<i>Tɨɨpi</i>',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1406,353,'<i>Tupiri</i>',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1407,353,'<i>Tatuwani</i>',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1408,353,'<i>Hikuritame</i>',1,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1409,353,'<i>Kawiteru</i>',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1410,354,'Jicarero',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1411,354,'Chamán',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1412,354,'Anciano sabio',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1413,354,'Peyotero',1,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1414,355,'Con velorio, despedida física y del alma',1,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1415,355,'Con veladoras, comida y música',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1416,355,'Con velorio, incineración y entierro',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1417,355,'Con abrazos, oraciones y bailes',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1418,356,'Irikixa',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1419,356,'<i>Mɨɨkí Kwevíxa</i>',1,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1420,356,'<i>Yuimakwaxa</i>',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1421,356,'<i>Maríxa</i>',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1422,357,'Invocar a las sombras',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1423,357,'Llamar al más allá',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1424,357,'Invocar o llamar al muerto',1,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1425,357,'Llamar al inframundo',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1426,358,'Fiesta para dar la bienvenidad a los parientes',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1427,358,'Ceremonia de bienvenida al inframundo',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1428,358,'Fiesta de los difuntos',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1429,358,'Ceremonia para que el difunto se despida de sus parientes',1,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1430,359,'<i>Mɨɨkí</i>',1,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1431,359,'<i>Neɨkixiwima</i>',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1432,359,'<i>Ne aurie muka</i>',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1433,359,'<i>Yeikame</i>',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1434,360,'Visita a sus parientes',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1435,360,'Recorre toda su vida',1,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1436,360,'Visita el cielo',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1437,360,'Recorre el inframundo',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1438,361,'Bolillo',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1439,361,'Huesos',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1440,361,'Tortilla',1,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1441,361,'Croquetas',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1442,362,'Se ahogan',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1443,362,'Los golpean los animales',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1444,362,'Se queman',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1445,362,'Los aplasta una roca',1,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1446,363,'<i>Pexuri</i>',1,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1447,363,'<i>Kexiu</i>',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1448,363,'<i>Kɨxaɨ</i>',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1449,363,'<i>Tsuirá</i>',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1450,364,'<i>Wakana</i>',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1451,364,'<i>Wirɨkɨ</i>',1,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1452,364,'<i>Weurai</i>',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1453,364,'<i>Kwatsa</i>',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1454,365,'Cuervo',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1455,365,'Pollo',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1456,365,'Zopilote',1,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1457,365,'Güilota',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1458,366,'Llama a las deidades',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1459,366,'Llama a las sombras del inframundo',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1460,366,'Llama a los familiares',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1461,366,'Llama el alma del difunto del inframundo',1,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1462,367,'Lo espera con todo lo que le gustaba, lo saluda, lo llora y lo despiden',1,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1463,367,'Oran por el alma del difunto',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1464,367,'Lo espera con comida, música y regalos',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1465,367,'Bailan por el descanso del difunto',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1466,368,'Centro ceremonial',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1467,368,'Un pequeño santuario',1,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1468,368,'Templo',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1469,368,'Una pequeña iglesia',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1470,369,'<i>Mitsu</i>',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1471,369,'<i>Ɨxawe</i>',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1472,369,'<i>Tsɨkɨ</i>',1,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1473,369,'<i>Untsa</i>',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1474,370,'Cuando muera irá al infierno',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1475,370,'Cuando muera el perro no lo ayudará a cruzar el río',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1476,370,'Cuando muera irá al inframundo',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1477,370,'Cuando muera el perro va a estar esperándolo para morderlo',1,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1478,371,'Cuando muera el perro le dará agua, comida y buenos deseos',1,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1479,371,'Cuando muera el perro lo abrazará',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1480,371,'Cuando muera el perro jugará con la persona',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1481,371,'Cuando muera no irá al inframundo',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1482,372,'<i>Eɨkariti</i>',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1483,372,'<i>Ɨpá</i>',1,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1484,372,'<i>Ɨrawe emɨtimariwe</i>',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1485,372,'<i>Uxa</i>',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1486,373,'Chupa almas',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1487,373,'Roba sangre',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1488,373,'Chupa sangre',1,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1489,373,'Roba almas',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1490,374,'Al inframundo',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1491,374,'A la vida',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1492,374,'A la noche',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1493,374,'A la muerte',1,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1494,375,'Que las personas se pierdan para quitarles la vida',1,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1495,375,'Que las personas pierdan la razón',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1496,375,'Que las personas encuentren su camino',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1497,375,'Que las personas vayan al inframundo',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1498,376,'Rata',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1499,376,'Murciélago',1,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1500,376,'Hiena',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1501,376,'Búho',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1502,377,'Deidades',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1503,377,'Gobernadores y polícias',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1504,377,'Antepasados divinizados y deidades',1,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1505,377,'Ancianos sabios y chamanes',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1506,378,'Porque no peregrinan',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1507,378,'Porque no hacen sus oraciones',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1508,378,'Porque no hacen ofrendas a las deidades',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1509,378,'Porque trasgreden las normas de convivencia',1,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1510,379,'Son castigados por las deidades y los parientes muertos',1,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1511,379,'Son castigados por los hombres',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1512,379,'Son castigados por los polícias',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1513,379,'Son castigados por las sombras',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1514,380,'Con cárcel',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1515,380,'Con enfermedades o desgracias',1,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1516,380,'Con falta de dinero y trabajo',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1517,380,'Con accidentes',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1518,381,'Que su alma no llegue al cielo',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1519,381,'Que su alma no esté con su familia',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1520,381,'Que su alma quede prisionera eternamente en el inframundo',1,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1521,381,'Que su alma no encuentre el camino',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1522,382,'En las escuelas',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1523,382,'Cerca de los campos de flores',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1524,382,'En el cementerio',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1525,382,'Cerca de su casa para que no se pierda',1,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1526,383,'Comida y juguetes',1,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1527,383,'Una canción',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1528,383,'Leche y ropa',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1529,383,'Un baño',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1530,384,'Van al cielo',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1531,384,'Siguen formando parte de la familia',1,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1532,384,'Van al purgatorio',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1533,384,'Siguen formando parte de la comunidad',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1534,385,'Con tepehuaje',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1535,385,'Con tomillo',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1536,385,'Con peyote',1,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1537,385,'Con cactus',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1538,386,'Con un ojo de dios',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1539,386,'Se induce a una visión',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1540,386,'Con sus pensamientos',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1541,386,'Se induce a un sueño sagrado',1,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1542,387,'Se convive, danza y ríe con el difunto',1,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1543,387,'Se llora con el difunto',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1544,387,'Se habla con el difunto',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1545,387,'Se pelea con el difunto',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1546,388,'Venado',0,'Images/Wixarika/Animales/Venado/Venado 1','');
INSERT INTO `quiz_respuesta` VALUES (1547,388,'Jabalí',1,'Images/Wixarika/Animales/Jabali/Jabalí 1 x1','');
INSERT INTO `quiz_respuesta` VALUES (1548,388,'Iguana',0,'Images/Wixarika/Animales/Iguana/Iguana 1','');
INSERT INTO `quiz_respuesta` VALUES (1549,388,'Cerdo',0,'Images/Wixarika/Animales/Cerdo/Cerdo 1','');
INSERT INTO `quiz_respuesta` VALUES (1550,389,'Jabalí',0,'Images/Wixarika/Animales/Jabali/Jabalí 1 x1','');
INSERT INTO `quiz_respuesta` VALUES (1551,389,'Puma',0,'Images/Wixarika/Animales/Puma/Puma','');
INSERT INTO `quiz_respuesta` VALUES (1552,389,'Venado',1,'Images/Wixarika/Animales/Venado/Venado 1','');
INSERT INTO `quiz_respuesta` VALUES (1553,389,'Iguana',0,'Images/Wixarika/Animales/Iguana/Iguana 1','');
INSERT INTO `quiz_respuesta` VALUES (1554,390,'Iguana',0,'Images/Wixarika/Animales/Iguana/Iguana 1','');
INSERT INTO `quiz_respuesta` VALUES (1555,390,'Jabalí',0,'Images/Wixarika/Animales/Jabali/Jabalí 1 x1','');
INSERT INTO `quiz_respuesta` VALUES (1556,390,'Venado',0,'Images/Wixarika/Animales/Venado/Venado 1','');
INSERT INTO `quiz_respuesta` VALUES (1557,390,'Puma',1,'Images/Wixarika/Animales/Puma/Puma','');
INSERT INTO `quiz_respuesta` VALUES (1558,391,'Araña',1,'Images/Wixarika/Animales/Araña/Araña f1','');
INSERT INTO `quiz_respuesta` VALUES (1559,391,'Abeja',0,'Images/Wixarika/Animales/Abeja/Abeja 8','');
INSERT INTO `quiz_respuesta` VALUES (1560,391,'Alacrán',0,'Images/Wixarika/Animales/Alacran/Alacran 3','');
INSERT INTO `quiz_respuesta` VALUES (1561,391,'Oruga',0,'Images/Wixarika/Animales/Oruga/Oruga','');
INSERT INTO `quiz_respuesta` VALUES (1562,392,'Alacrán',0,'Images/Wixarika/Animales/Alacran/Alacran 3','');
INSERT INTO `quiz_respuesta` VALUES (1563,392,'Serpiente',1,'Images/Wixarika/Animales/Serpiente/Serpiente 6','');
INSERT INTO `quiz_respuesta` VALUES (1564,392,'Abeja',0,'Images/Wixarika/Animales/Abeja/Abeja 8','');
INSERT INTO `quiz_respuesta` VALUES (1565,392,'Iguana',0,'Images/Wixarika/Animales/Iguana/Iguana 1','');
INSERT INTO `quiz_respuesta` VALUES (1566,393,'Iguana',0,'Images/Wixarika/Animales/Iguana/Iguana 1','');
INSERT INTO `quiz_respuesta` VALUES (1567,393,'Serpiente',0,'Images/Wixarika/Animales/Serpiente/Serpiente 6','');
INSERT INTO `quiz_respuesta` VALUES (1568,393,'Pescado',1,'Images/Wixarika/Animales/Pez/Pez 9','');
INSERT INTO `quiz_respuesta` VALUES (1569,393,'Abeja',0,'Images/Wixarika/Animales/Abeja/Abeja 8','');
INSERT INTO `quiz_respuesta` VALUES (1570,394,'Venado',0,'Images/Wixarika/Animales/Venado/Venado 1','');
INSERT INTO `quiz_respuesta` VALUES (1571,394,'Puma',0,'Images/Wixarika/Animales/Puma/Puma','');
INSERT INTO `quiz_respuesta` VALUES (1572,394,'Jabalí',0,'Images/Wixarika/Animales/Jabali/Jabalí 1 x1','');
INSERT INTO `quiz_respuesta` VALUES (1573,394,'Coyote',1,'Images/Wixarika/Animales/Coyote/Coyote 1','');
INSERT INTO `quiz_respuesta` VALUES (1574,395,'Abeja',1,'Images/Wixarika/Animales/Abeja/Abeja 8','');
INSERT INTO `quiz_respuesta` VALUES (1575,395,'Serpiente',0,'Images/Wixarika/Animales/Serpiente/Serpiente 6','');
INSERT INTO `quiz_respuesta` VALUES (1576,395,'Araña',0,'Images/Wixarika/Animales/Araña/Araña f1','');
INSERT INTO `quiz_respuesta` VALUES (1577,395,'Alacrán',0,'Images/Wixarika/Animales/Alacran/Alacran 3','');
INSERT INTO `quiz_respuesta` VALUES (1578,396,'Coyote',0,'Images/Wixarika/Animales/Coyote/Coyote 1','');
INSERT INTO `quiz_respuesta` VALUES (1579,396,'Zorro',1,'Images/Wixarika/Animales/Zorro/Zorro 1','');
INSERT INTO `quiz_respuesta` VALUES (1580,396,'Puma',0,'Images/Wixarika/Animales/Puma/Puma','');
INSERT INTO `quiz_respuesta` VALUES (1581,396,'Venado',0,'Images/Wixarika/Animales/Venado/Venado 1','');
INSERT INTO `quiz_respuesta` VALUES (1582,397,'Conejo',0,'Images/Wixarika/Animales/Conejo/Conejo 1','');
INSERT INTO `quiz_respuesta` VALUES (1583,397,'Araña',0,'Images/Wixarika/Animales/Araña/Araña f1','');
INSERT INTO `quiz_respuesta` VALUES (1584,397,'Ardilla',1,'Images/Wixarika/Animales/Ardilla/Ardilla 2','');
INSERT INTO `quiz_respuesta` VALUES (1585,397,'Abeja',0,'Images/Wixarika/Animales/Abeja/Abeja 8','');
INSERT INTO `quiz_respuesta` VALUES (1586,398,'Araña',0,'Images/Wixarika/Animales/Araña/Araña f1','');
INSERT INTO `quiz_respuesta` VALUES (1587,398,'Serpiente',0,'Images/Wixarika/Animales/Serpiente/Serpiente 6','');
INSERT INTO `quiz_respuesta` VALUES (1588,398,'Abeja',0,'Images/Wixarika/Animales/Abeja/Abeja 8','');
INSERT INTO `quiz_respuesta` VALUES (1589,398,'Alacrán',1,'Images/Wixarika/Animales/Alacran/Alacran 3','');
INSERT INTO `quiz_respuesta` VALUES (1590,399,'Lechuza',1,'Images/Wixarika/Animales/Buho/Lechuza NPC 7','');
INSERT INTO `quiz_respuesta` VALUES (1591,399,'Halcón',0,'Images/Wixarika/Animales/Halcón/halcón','');
INSERT INTO `quiz_respuesta` VALUES (1592,399,'Águila',0,'Images/Wixarika/Animales/Aguila/Águila 1','');
INSERT INTO `quiz_respuesta` VALUES (1593,399,'Cuervo',0,'Images/Wixarika/Animales/Cuervo/Cuervo','');
INSERT INTO `quiz_respuesta` VALUES (1594,400,'Abeja',0,'Images/Wixarika/Animales/Abeja/Abeja 8','');
INSERT INTO `quiz_respuesta` VALUES (1595,400,'Iguana',1,'Images/Wixarika/Animales/Iguana/Iguana 1','');
INSERT INTO `quiz_respuesta` VALUES (1596,400,'Araña',0,'Images/Wixarika/Animales/Araña/Araña f1','');
INSERT INTO `quiz_respuesta` VALUES (1597,400,'Serpiente',0,'Images/Wixarika/Animales/Serpiente/Serpiente 6','');
INSERT INTO `quiz_respuesta` VALUES (1598,401,'Cocodrilo',1,'Images/Wixarika/Animales/Cocodrilo/Cocodrilo 1 x1','');
INSERT INTO `quiz_respuesta` VALUES (1599,401,'Serpiente',0,'Images/Wixarika/Animales/Serpiente/Serpiente 6','');
INSERT INTO `quiz_respuesta` VALUES (1600,401,'Iguana',0,'Images/Wixarika/Animales/Iguana/Iguana 1','');
INSERT INTO `quiz_respuesta` VALUES (1601,401,'Lagartija',0,'Images/Wixarika/Animales/Lagartija/Lagartija 1','');
INSERT INTO `quiz_respuesta` VALUES (1602,402,'Águila',0,'Images/Wixarika/Animales/Aguila/Águila 1','');
INSERT INTO `quiz_respuesta` VALUES (1603,402,'Güilota',1,'Images/Wixarika/Animales/Guilota/Guilota 1','');
INSERT INTO `quiz_respuesta` VALUES (1604,402,'Lechuza',0,'Images/Wixarika/Animales/Buho/Lechuza NPC 7','');
INSERT INTO `quiz_respuesta` VALUES (1605,402,'Pollo',0,'Images/Wixarika/Animales/Gallina/Gallina 3 x1','');
INSERT INTO `quiz_respuesta` VALUES (1606,403,'Iguana',0,'Images/Wixarika/Animales/Iguana/Iguana 1','');
INSERT INTO `quiz_respuesta` VALUES (1607,403,'Cocodrilo',0,'Images/Wixarika/Animales/Cocodrilo/Cocodrilo 1 x1','');
INSERT INTO `quiz_respuesta` VALUES (1608,403,'Lagartija',1,'Images/Wixarika/Animales/Lagartija/Lagartija 1','');
INSERT INTO `quiz_respuesta` VALUES (1609,403,'Serpiente',0,'Images/Wixarika/Animales/Serpiente/Serpiente 6','');
INSERT INTO `quiz_respuesta` VALUES (1610,404,'Águila',0,'Images/Wixarika/Animales/Aguila/Águila 1','');
INSERT INTO `quiz_respuesta` VALUES (1611,404,'Güilota',0,'Images/Wixarika/Animales/Guilota/Guilota 1','');
INSERT INTO `quiz_respuesta` VALUES (1612,404,'Lechuza',0,'Images/Wixarika/Animales/Buho/Lechuza NPC 7','');
INSERT INTO `quiz_respuesta` VALUES (1613,404,'Pollo',1,'Images/Wixarika/Animales/Gallina/Gallina 3 x1','');
INSERT INTO `quiz_respuesta` VALUES (1614,405,'Cerdo',1,'Images/Wixarika/Animales/Cerdo/Cerdo 1','');
INSERT INTO `quiz_respuesta` VALUES (1615,405,'Vaca',0,'Images/Wixarika/Animales/Vaca/Vaca 7 x1','');
INSERT INTO `quiz_respuesta` VALUES (1616,405,'Jabalí',0,'Images/Wixarika/Animales/Jabali/Jabalí 1 x1','');
INSERT INTO `quiz_respuesta` VALUES (1617,405,'Pollo',0,'Images/Wixarika/Animales/Gallina/Gallina 3 x1','');
INSERT INTO `quiz_respuesta` VALUES (1618,406,'Perro',0,'Images/Wixarika/Iconos/Perro','');
INSERT INTO `quiz_respuesta` VALUES (1619,406,'Conejo',1,'Images/Wixarika/Animales/Conejo/Conejo 1','');
INSERT INTO `quiz_respuesta` VALUES (1620,406,'Gato',0,'Images/Wixarika/Animales/Gato/Gato','');
INSERT INTO `quiz_respuesta` VALUES (1621,406,'Ardilla',0,'Images/Wixarika/Animales/Ardilla/Ardilla 2','');
INSERT INTO `quiz_respuesta` VALUES (1622,407,'Cerdo',0,'Images/Wixarika/Animales/Cerdo/Cerdo 1','');
INSERT INTO `quiz_respuesta` VALUES (1623,407,'Oveja',0,'Images/Wixarika/Animales/Oveja/Oveja 1 x1','');
INSERT INTO `quiz_respuesta` VALUES (1624,407,'Vaca',1,'Images/Wixarika/Animales/Vaca/Vaca 7 x1','');
INSERT INTO `quiz_respuesta` VALUES (1625,407,'Pollo',0,'Images/Wixarika/Animales/Gallina/Gallina 3 x1','');
INSERT INTO `quiz_respuesta` VALUES (1626,408,'Pollo',0,'Images/Wixarika/Animales/Gallina/Gallina 3 x1','');
INSERT INTO `quiz_respuesta` VALUES (1627,408,'Halcón',0,'Images/Wixarika/Animales/Halcón/halcón','');
INSERT INTO `quiz_respuesta` VALUES (1628,408,'Güilota',0,'Images/Wixarika/Animales/Guilota/Guilota 1','');
INSERT INTO `quiz_respuesta` VALUES (1629,408,'Águila real',1,'Images/Wixarika/Animales/Aguila/Águila 1','');
INSERT INTO `quiz_respuesta` VALUES (1630,409,'Serpiente',1,'Images/Wixarika/Animales/Serpiente/Serpiente 6','');
INSERT INTO `quiz_respuesta` VALUES (1631,409,'Iguana',0,'Images/Wixarika/Animales/Iguana/Iguana 1','');
INSERT INTO `quiz_respuesta` VALUES (1632,409,'Cocodrilo',0,'Images/Wixarika/Animales/Cocodrilo/Cocodrilo 1 x1','');
INSERT INTO `quiz_respuesta` VALUES (1633,409,'Lagartija',0,'Images/Wixarika/Animales/Lagartija/Lagartija 1','');
INSERT INTO `quiz_respuesta` VALUES (1634,410,'Perro',0,'Images/Wixarika/Iconos/Perro','');
INSERT INTO `quiz_respuesta` VALUES (1635,410,'Lobo',1,'Images/Wixarika/Animales/Lobo/Lobo 1','');
INSERT INTO `quiz_respuesta` VALUES (1636,410,'Puma',0,'Images/Wixarika/Animales/Puma/Puma','');
INSERT INTO `quiz_respuesta` VALUES (1637,410,'Coyote',0,'Images/Wixarika/Animales/Coyote/Coyote 1','');
INSERT INTO `quiz_respuesta` VALUES (1638,411,'Águila',0,'Images/Wixarika/Animales/Aguila/Águila 1','');
INSERT INTO `quiz_respuesta` VALUES (1639,411,'Cuervo',0,'Images/Wixarika/Animales/Cuervo/Cuervo','');
INSERT INTO `quiz_respuesta` VALUES (1640,411,'Zopilote',1,'Images/Wixarika/Animales/Zopilote/Zopilote 4','');
INSERT INTO `quiz_respuesta` VALUES (1641,411,'Lechuza',0,'Images/Wixarika/Animales/Buho/Lechuza NPC 7','');
INSERT INTO `quiz_respuesta` VALUES (1642,412,'La Yesca, Nayarit',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1643,412,'San Blas, Nayarit',1,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1644,412,'Tepic, Nayarit',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1645,412,'Tuxpan, Nayarit',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1646,413,'<i>Taikairiya</i>',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1647,413,'<i>Ha’utete</i>',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1648,413,'<i>Waxiewe</i>',1,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1649,413,'<i>Ku’unita</i>',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1650,414,'Santo Domingo, San Luis Potosí',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1651,414,'Charcas, San Luis Potosí',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1652,414,'Villa de Ramos, San Luis Potosí',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1653,414,'Real de Catorces, San Luis Potosí',1,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1654,415,'<i>Wirikuta</i>',1,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1655,415,'<i>Hauxa Manaka</i>',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1656,415,'<i>Xapawiyemeta</i>',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1657,415,'<i>Haramara</i>',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1658,416,'<i>Haramara</i>',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1659,416,'<i>Wirikuta</i>',1,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1660,416,'<i>Hauxa Manaka</i>',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1661,416,'<i>Xapawiyemeta</i>',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1662,417,'Mezquital, Durango',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1663,417,'Poanas, Durango',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1664,417,'Cerro Gordo, Durango',1,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1665,417,'Súchil, Durango',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1666,418,'<i>Xapawiyemeta</i>',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1667,418,'<i>Te’ekata</i>',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1668,418,'<i>Wirikuta</i>',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1669,418,'<i>Hauxa Manaka</i>',1,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1670,419,'Tequila, Jalisco',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1671,419,'Guadalajara, Jalisco',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1672,419,'Bolaños, Jalisco',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1673,419,'Mezquitic, Jalisco',1,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1674,420,'<i>Wirikuta</i>',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1675,420,'<i>Te’ekata</i>',1,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1676,420,'<i>Haramara</i>',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1677,420,'<i>Hauxa Manaka</i>',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1678,421,'<i>Te’ekata</i>',1,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1679,421,'<i>Xapawiyemeta</i>',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1680,421,'<i>Haramara</i>',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1681,421,'<i>Hauxa Manaka</i>',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1682,422,'<i>Haramara</i>',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1683,422,'<i>Te’ekata</i>',1,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1684,422,'<i>Wirikuta</i>',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1685,422,'<i>Xapawiyemeta</i>',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1686,423,'<i>Wirikuta</i>',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1687,423,'<i>Hauxa Manaka</i>',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1688,423,'<i>Te’ekata</i>',1,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1689,423,'<i>Xapawiyemeta</i>',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1690,424,'Tequila, Jalisco',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1691,424,'En el cerro de los alacranes, en Bolaños, Jalisco',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1692,424,'Villa Guerrero, Jalisco',0,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1693,424,'En La Isla de los Alacranes, en el Lago de Chapala, Jalisco',1,NULL,'');
INSERT INTO `quiz_respuesta` VALUES (1694,425,'El paso de una absoluta oscuridad a un amanecer',1,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1695,425,'El fuego y el agua',0,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1696,425,'La lucha de la luz contra la obscuridad',0,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1697,425,'El sol y la luna',0,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1698,426,'<i>Utútawi</i>',0,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1699,426,'<i>Tamatzi Paritsika</i>',1,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1700,426,'<i>Wewetsári</i>',0,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1701,426,'<i>Tututáka Pitsitéka</i>',0,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1702,427,'<i>Te’ekata</i>',0,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1703,427,'<i>Wirikuta</i>',0,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1704,427,'<i>Reunari</i>',1,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1705,427,'<i>Haramara</i>',0,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1706,428,'Para tener fiestas en la comunidad',0,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1707,428,'Para garantizar la continuidad del pueblo <i>Wixárika</i>',1,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1708,428,'Para vender artesanías',0,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1709,428,'Para recibir la primavera',0,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1710,429,'<i>Tamatzi Kauyumárie</i>',1,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1711,429,'<i>Tatei Xapawiyeme</i>',0,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1712,429,'<i>Tamatzi Paritsika</i>',0,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1713,429,'<i>Tatei Namakate Uteanaka</i>',0,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1714,430,'Playa, mar, lagos, ríos, campos, cerros y montañas',0,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1715,430,'<i>Haramara, Wirikuta, Hauxa Manaka, Te''akata y Xapawiyemeta</i>',1,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1716,430,'Centros ceremoniales',0,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1717,430,'<i>Haramara, Wirikuta, Hauxa Manaka y Xapawiyemeta</i>',0,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1718,431,'El corazón, alma y espíritu',0,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1719,431,'Norte, sur, este, oeste y centro',1,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1720,431,'Norte, sur, este y oeste',0,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1721,431,'Norte, sur, oriente y poniente',0,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1722,432,'<i>Tatewari, Tayau, Takutsi Nakawé, Naɨrɨ y T’kákame</i>',0,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1723,432,'Norte, sur, este, oeste y centro',0,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1724,432,'<i>Wewetsári, Utútawi, Tsipúrawi, Tutu Háuki y Tututáka Pitsitéka</i>',1,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1725,432,'No hay cazadores <i>Wixárika</i>',0,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1726,433,'Olla',0,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1727,433,'Vaso',0,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1728,433,'Jícara',1,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1729,433,'Tablilla',0,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1730,434,'<i>Kaitsa</i>',0,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1731,434,'<i>Kanarite</i>',1,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1732,434,'<i>Xaweri</i>',0,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1733,434,'<i>Kanari</i>',0,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1734,435,'<i>Kanarite</i>',0,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1735,435,'<i>Kaitsa</i>',0,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1736,435,'<i>Xaweri</i>',1,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1737,435,'<i>Kanari</i>',0,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1738,436,'<i>Xaweri</i>',0,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1739,436,'<i>Kanari</i>',1,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1740,436,'<i>Kaitsa</i>',0,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1741,436,'<i>Kanarite</i>',0,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1742,437,'<i>Kaitsa</i>',1,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1743,437,'<i>Kanari</i>',0,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1744,437,'<i>Kanarite</i>',0,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1745,437,'<i>Xaweri</i>',0,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1746,438,'Centro ceremonial',1,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1747,438,'Un tipo de casa',0,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1748,438,'Salón de eventos',0,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1749,438,'Bodega tradicional',0,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1750,439,'<i>Tuki y xiriki</i>',1,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1751,439,'Templo mayor y templos tradicionales',0,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1752,439,'<i>Haita y ku’unita</i>',0,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1753,439,'Templos <i>Wixárika</i>',0,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1754,440,'De la diosa de la lluvia',0,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1755,440,'De los dioses del fuego y el sol',1,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1756,440,'De las madres del maíz',0,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1757,440,'De todas las deidades',0,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1758,441,'Para que no se enferme',0,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1759,441,'Para que se envenene',0,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1760,441,'Para que dé más leche',1,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1761,441,'Para que tenga manchas',0,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1762,442,'Bailar',0,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1763,442,'Tejer',0,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1764,442,'Sembrar',0,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1765,442,'Cocinar',1,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1766,443,'Para aderezar platillos',1,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1767,443,'Para rituales sagrados',0,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1768,443,'Para pintar las casas',0,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1769,443,'Para comer',0,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1770,273,'a lo femenino',0,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1771,273,'a los niños',0,NULL,NULL);
INSERT INTO `quiz_respuesta` VALUES (1772,273,'a los ancianos',0,NULL,NULL);
INSERT INTO `estados` VALUES (1,'Nayarit');
INSERT INTO `estados` VALUES (2,'Zacatecas');
INSERT INTO `estados` VALUES (3,'San Luis Potosí');
INSERT INTO `estados` VALUES (4,'Durango');
INSERT INTO `estados` VALUES (5,'Jalisco');
INSERT INTO `estados` VALUES (6,'Inframundo');
INSERT INTO `alimentos` VALUES (1,'FRUTA','Arrayan','Arrayanes','Tsikwai','Tsikwaita',10,3,'Images/Wixarika/Alimentos/Arrayanes','¡Recolectaste <i>tsikwai</i> (arrayan)!
El <i>tsikwai</i> (arrayan) se utiliza para hacer atoles.','1,2');
INSERT INTO `alimentos` VALUES (2,'FRUTA','Caña','Cañas','Uwá','Uwa’ate',10,3,'Images/Wixarika/Alimentos/Caña','¡Recolectaste <i>uwá</i> (caña)!','3');
INSERT INTO `alimentos` VALUES (3,'FRUTA','Ciruela','Ciruelas','Kwarɨpa','Kwarɨpate',10,3,'Images/Wixarika/Alimentos/Ciruela 1x','¡Recolectaste <i>kwarɨpa</i> (ciruela)!','4');
INSERT INTO `alimentos` VALUES (4,'FRUTA','Guamúchil','Guamúchiles','Muxu’uri','Muxu’urite',10,3,'Images/Wixarika/Alimentos/Guamúchil','¡Recolectaste <i>muxu’uri</i> (<i>guamúchil</i>)!','5');
INSERT INTO `alimentos` VALUES (5,'FRUTA','Guayaba','Guayabas','Ha’yewaxi','Ha’yewaxite',10,3,'Images/Wixarika/Alimentos/Guayaba','¡Recolectaste <i>ha’yewaxi</i> (guayaba)!','6');
INSERT INTO `alimentos` VALUES (6,'FRUTA','Higo','Higos','Piní','Piní',10,3,'Images/Wixarika/Alimentos/Higo','¡Recolectaste <i>piní</i> (higo)! ','7');
INSERT INTO `alimentos` VALUES (7,'FRUTA','Mango','Mangos','Ma’aku','Ma’akute',10,3,'Images/Wixarika/Alimentos/Mango','¡Recolectaste <i>máacu</i> (mango)!','8');
INSERT INTO `alimentos` VALUES (8,'FRUTA','Nanchi','Nanchis','Uwakí','Uwakiite',10,3,'Images/Wixarika/Alimentos/Nanchi','¡Recolectaste <i>uwakí</i> (nanchi)!','9');
INSERT INTO `alimentos` VALUES (9,'FRUTA','Naranja','Naranjas','Narakaxi','Narakaxite',10,3,'Images/Wixarika/Alimentos/Naranja','¡Recolectaste <i>narakaxi</i> (naranja)! ','10');
INSERT INTO `alimentos` VALUES (10,'FRUTA','Pitahaya','Pitahayas','Ma’ara','Ma’arate',10,3,'Images/Wixarika/Alimentos/Pitahaya','¡Recolectaste <i>ma’ara</i> (pitahaya)! ','11');
INSERT INTO `alimentos` VALUES (11,'FRUTA','Plátano','Plátanos','Ka’arú','Ka’arute',10,3,'Images/Wixarika/Alimentos/Plátano','¡Recolectaste <i>kaárú</i> (plátano)!','12');
INSERT INTO `alimentos` VALUES (12,'FRUTA','Tuna','Tunas','Yɨɨna','Yɨna’ate',10,3,'Images/Wixarika/Alimentos/Tuna','¡Recolectaste <i> yɨɨna</i>  (tuna)!','13');
INSERT INTO `alimentos` VALUES (13,'FRUTA','Jamaica','Jamaicas','Kamaika','Kamaikas',10,3,'Images/Wixarika/Alimentos/Jamaica','¡Has recolectado <i>kamaika</i> (jamaica)!
La <i>kamaika</i> (jamaica) se utiliza para preparar bebidas y mermeladas.','14,15');
INSERT INTO `alimentos` VALUES (14,'VERDURA','Calabacita','Calabacitas','Xútsi','Xutsíte',10,3,'Images/Wixarika/Alimentos/Calabacitas','¡Recolectaste <i>xútsi</i> (calabacita)! 
La <i>xútsi</i> (calabacita) es un alimento importante de la gastronomía <i>Wixárika</i>, se cultiva en los coamiles.','16,17');
INSERT INTO `alimentos` VALUES (15,'VERDURA','Camote','Camotes','Ye’eri','Ye’erite',10,3,'Images/Wixarika/Alimentos/Camote','¡Recolectaste <i>ye’eri</i> (camote)! ','18');
INSERT INTO `alimentos` VALUES (16,'VERDURA','Champiñon','Champiñones','U tuxa yekwa','U tuxa yekwa’ate',10,3,'Images/Wixarika/Alimentos/Hongo','¡Recolectaste <i>u tuxa yekwa’ate</i> (champiñones)! ','19');
INSERT INTO `alimentos` VALUES (17,'VERDURA','Chile','Chiles','Kukúri','Kukuríte',10,3,'Images/Wixarika/Alimentos/Chile','¡Recolectaste<i> kukúri </i>(chile)! 
El <i>kukúri</i> (chile) se utiliza para hacer salsas y se cultiva en los coamiles.','20,21');
INSERT INTO `alimentos` VALUES (18,'VERDURA','Elote','Elotes','Ikɨri','Ikɨríte',10,3,'Images/Wixarika/Alimentos/Elote','¡Recolectaste <i>ikɨri</i> (elote)!','22');
INSERT INTO `alimentos` VALUES (19,'VERDURA','Frijol','Frijoles','Múme','Múmete',10,3,'Images/Wixarika/Alimentos/Frijoles','¡Recolectaste <i>múme</i> (frijol)!
El <i>múme</i> (frijol) es uno de los principales alimentos de la gastronomía <i>Wixárika</i>, se cultiva en los coamiles.','23,24');
INSERT INTO `alimentos` VALUES (20,'VERDURA','Guaje','Guajes','Haxi','Haxite',10,3,'Images/Wixarika/Alimentos/Guaje rojo','¡Recolectaste <i>haxi</i> (guaje)!','25');
INSERT INTO `alimentos` VALUES (21,'VERDURA','Hongo','Hongos','Yekwa','Yekwa’ate',10,3,'Images/Wixarika/Alimentos/Hongo','¡Recolectaste <i>yekwa’ate</i> (hongos)!
Los <i>yekwa’ate</i> (hongos) son recolectados en los caminos durante la temporada de lluvias.','26,27');
INSERT INTO `alimentos` VALUES (22,'VERDURA','Jícama','Jícamas','Xa’ata','Xata’ate',10,3,'Images/Wixarika/Alimentos/Jícama','¡Recolectaste <i>xa´ata</i> (jicama)!','28');
INSERT INTO `alimentos` VALUES (23,'VERDURA','Jítomate','Jítomates','Túmati','Túmatite',10,3,'Images/Wixarika/Alimentos/Jitomate','¡Recolectaste <i>túmati</i> (jitomate)! ','29');
INSERT INTO `alimentos` VALUES (24,'VERDURA','Limón','Limones','Tsinakari','Tsinakarite',10,3,'Images/Wixarika/Alimentos/Limón','¡Recolectaste <i>tsinakari</i> (limón)!.','30');
INSERT INTO `alimentos` VALUES (25,'VERDURA','Nopal','Nopales','Na’akari','Na’akarite',10,3,'Images/Wixarika/Alimentos/Nopal','¡Recolectaste <i>na’akari</i> (nopal)! ','31');
INSERT INTO `alimentos` VALUES (26,'VERDURA','Quelite','Quelites','Ké’uxa','Ké’uxate',10,3,'Images/Wixarika/Alimentos/Quelites','¡Recolectaste <i>ké’uxate</i> (quelites)! 
Los <i>ké’uxate</i> (quelites) se guisan y siven para rellenar quesadillas.','32,33');
INSERT INTO `alimentos` VALUES (27,'VERDURA','Semilla de calabaza','Semillas de calabaza','Xutsi hatsiyari','Xutsi hatsiyarite',10,3,'Images/Wixarika/Alimentos/Semilla de calabaza','¡Recolectaste <i>xutsi hatsiyarite</i> (semillas de calabaza)! ','34');
INSERT INTO `alimentos` VALUES (28,'VERDURA','Verdolaga','Verdolagas','Aɨraxa','Aɨraxate',10,3,'Images/Wixarika/Alimentos/Verdolaga','¡Recolectaste <i>aɨraxate</i> (verdolagas)!
Las <i>aɨraxate</i> (verdolagas) se guisan y siven para rellenar quesadillas.','35,36');
INSERT INTO `alimentos` VALUES (29,'MAIZ','Maíz','Maíces','Ikú','Iku’ute',10,3,'Images/Wixarika/Alimentos/Maíz','¡Recolectaste <i>ikú</i> (maíz)!
El <i>ikú</i> (maíz) es el alimento más importante para los <i>wixaritari</i>, es su fuente principal de comida.','37,38');
INSERT INTO `alimentos` VALUES (30,'MAIZ','Maíz amarillo','Maíces amarillos','Ikú taxawime','Iku’ute taxawime',10,3,'Images/Wixarika/Alimentos/Maíz amarillo','¡Recolectaste <i>ikú taxawime</i> (maíz amarillo)!
El <i>ikú taxawime</i> (maíz amarillo) se utiliza como alimento para los animales.','39,40');
INSERT INTO `alimentos` VALUES (31,'MAIZ','Maíz azul','Maíces azules','Ikú yuawime','Iku’ute yuawime',10,3,'Images/Wixarika/Alimentos/Maíz azul','¡Recolectaste <i>ikú yuawime</i> (maíz azul)! 
El <i>ikú yuawime</i> (maíz azul) se utiliza para la preparación de tortillas, atole, pinole, y para dar color a los alimentos.','41,42');
INSERT INTO `alimentos` VALUES (32,'MAIZ','Maíz blanco','Maíces blancos','Ikú tuuxá','Iku’ute tuxame',10,3,'Images/Wixarika/Alimentos/Maíz blanco','¡Recolectaste <i>iIú tuuxá</i> (maíz blanco)! 
El <i>iIú tuuxá</i> (maíz blanco) se utiliza en las ceremonias religiosas.','43,44');
INSERT INTO `alimentos` VALUES (33,'MAIZ','Maíz morado','Maíces morados','Ikú tataɨrawi','Iku’ute taɨrawime',10,3,'Images/Wixarika/Alimentos/Maíz morado','¡Recolectaste <i>ikú tataɨrawi</i> (maíz morado)! 
El <i>ikú tataɨrawi</i> (maíz morado) se utiliza para la preparación de tortillas y para dar color a los alimentos.','45,46');
INSERT INTO `alimentos` VALUES (34,'MAIZ','Maíz negro','Maíces negros','Ikú yɨwi','Iku’ute yɨyɨwi',10,3,'Images/Wixarika/Alimentos/Maíz morado','¡Recolectaste <i>ikú yɨwi</i> (maíz negro)! 
Las festividades <i>Wixárika</i> dependen del ciclo de cultivo del <i>ikú</i> (maíz), y al mismo tiempo el <i>ikú</i> (maíz) depende de estos ritos para crecer sano y fuerte.','47,48');
INSERT INTO `alimentos` VALUES (35,'MAIZ','Maíz rojo','Maíces rojos','Ikú mɨxeta','Iku’ute xetame',10,3,'Images/Wixarika/Alimentos/Maíz rojo','¡Recolectaste <i>ikú mɨxeta</i> (maíz rojo)! 
El pan horneado, pinole, atole y tejuino son algunos de los productos tradicionales a base de <i>ikú</i> (maíz).','49,50');
INSERT INTO `alimentos` VALUES (36,'ORIGEN_ANINAL','Carne de ardilla','Carne de ardillas','Tekɨ','Tekɨri',10,20,'Images/Wixarika/Alimentos/Chuletas de cerdo fritas','¡Cazaste <i>tekɨ</i> (ardilla)!','51');
INSERT INTO `alimentos` VALUES (37,'ORIGEN_ANINAL','Carne de cerdo','Carne de cerdos','Tuixu','Tuixuri',10,20,'Images/Wixarika/Alimentos/Carne cerdo','¡Cazaste <i>tuixu</i> (cerdo)!','52');
INSERT INTO `alimentos` VALUES (38,'ORIGEN_ANINAL','Carne de conejo','Carne de conejos','Tátsiu','Tatsiurixi',10,20,'Images/Wixarika/Alimentos/Carne conejo','¡Cazaste <i>tátsiu</i> (conejo)!','53');
INSERT INTO `alimentos` VALUES (39,'ORIGEN_ANINAL','Carne de güilota','Carne de güilotas','Weurai','Weuraixi',10,20,'Images/Wixarika/Alimentos/Alitas de pollo fritas','¡Cazaste <i>weurai</i> (güilota)!','54');
INSERT INTO `alimentos` VALUES (40,'ORIGEN_ANINAL','Carne de iguana','Carne de iguanas','Keetse','Ketse’ete',10,20,'Images/Wixarika/Alimentos/Carne iguana','¡Cazaste <i>ke’etsé</i> (iguana)! 
La <i>ke’etsé</i> (iguana) es consumida tradicionalmente en pipián.','55,56');
INSERT INTO `alimentos` VALUES (41,'ORIGEN_ANINAL','Carne de jabalí','Carne de jabalís','Tuixuyeutanaka','Tuixuriyeutari',10,20,'Images/Wixarika/Alimentos/Carne cerdo','¡Cazaste <i>tuixuyeutanaka</i> (jabalí)! 
El <i>tuixuyeutanaka</i> (jabalí) es consumido tradicionalmente en albóndigas.','57,58');
INSERT INTO `alimentos` VALUES (42,'ORIGEN_ANINAL','Carne de pescado','Carne de pescados','Ketsɨ','Ketsɨte',10,20,'Images/Wixarika/Alimentos/Pescado','¡Pescaste <i>ketsí</i> (pescado)!
La sangre de <i>ketsí</i> (pescado) se utiliza en los rituales sagrados cuando no se caza un venado.','59,60');
INSERT INTO `alimentos` VALUES (43,'ORIGEN_ANINAL','Carne de pollo','Carne de pollos','Wakana','Wakana',10,20,'Images/Wixarika/Alimentos/Carne guilota','¡Cazaste <i>wakana</i> (pollo)!','61');
INSERT INTO `alimentos` VALUES (44,'ORIGEN_ANINAL','Carne de venado','Carnes de venados','Maxa','Maxatsi',10,20,'Images/Wixarika/Alimentos/Carne ardilla','¡Cazaste <i>maxa</i> (venado)! 
El <i>maxa</i> (venado) es consumido tradicionalmente en mole.','62,63');
INSERT INTO `alimentos` VALUES (45,'BEBIDA','Agua','Agua','Ha’a','Ha’a',10,3,'Images/Wixarika/Alimentos/Agua','¡Recolectaste <i>ha’a</i> (agua)! ','64');
INSERT INTO `alimentos` VALUES (46,'BEBIDA','Agua de jamaica','Aguas de jamaica','Kamaika hayaári','Kamaika hayaári',10,3,'Images/Wixarika/Alimentos/Agua de jamaica','¡Recolectaste <i>kamaika hayaári</i> (agua de jamaica)!','65');
INSERT INTO `alimentos` VALUES (47,'BEBIDA','Atole','Atole','Hamuitsi','Hamuitsi',10,3,'Images/Wixarika/Alimentos/Atole','¡Recolectaste <i>hamuitsi</i> (atole)! ','66');
INSERT INTO `alimentos` VALUES (48,'BEBIDA','Chicuatol','Chicuatoles','Tsinari','Tsinarite',10,3,'Images/Wixarika/Alimentos/Chicuatol','¡Recolectaste <i>tsinari</i> (chicuatol)!','67');
INSERT INTO `alimentos` VALUES (49,'BEBIDA','Jugo','Jugos','Hayaári','Mɨti iwa',10,3,'Images/Wixarika/Alimentos/Jugo de naranja','¡Recolectaste <i>hayaári</i> (jugo)!','68');
INSERT INTO `alimentos` VALUES (50,'BEBIDA','Jugo de caña','Jugos de caña','Uwá hayaári','Uwá hayaári',10,3,'Images/Wixarika/Alimentos/Jugo de caña','¡Recolectaste <i>uwá hayaári</i> (jugo de caña)!','69');
INSERT INTO `alimentos` VALUES (51,'BEBIDA','Jugo de naranja','Jugos de naranja','Narakaxi hayaári','Narakaxi hayaári',10,3,'Images/Wixarika/Alimentos/Jugo de naranja','¡Recolectaste <i>narakaxi hayaári</i> (jugo de naranja)!','70');
INSERT INTO `alimentos` VALUES (52,'BEBIDA','Leche','Leches','Retsi','Retsi',10,3,'Images/Wixarika/Alimentos/Leche','¡Recolectaste <i>retsi</i> (leche)!','71');
INSERT INTO `alimentos` VALUES (53,'BEBIDA','Tejuino','Tejuinos','Nawá','Nawa’ate',10,3,'Images/Wixarika/Alimentos/Atole o tejuino','¡Recolectaste <i>nawá</i> (tejuino)!','72');
INSERT INTO `alimentos` VALUES (54,'POSTRE','Dulce','Dulces','Ruritse','Ruritséte',10,5,'Images/Wixarika/Alimentos/Dulce','¡Recolectaste <i>ruritse</i> (dulce)!','73');
INSERT INTO `alimentos` VALUES (55,'POSTRE','Miel','Mieles','Xiete','Xiete',10,5,'Images/Wixarika/Alimentos/Miel','¡Recolectaste <i>xiete</i> (miel)! 
La <i>xiete</i> (miel) se utiliza para endulzar postres y bebidas tradicionales.','74,75');
INSERT INTO `alimentos` VALUES (56,'POSTRE','Pan','Panes','Paní','Panite',10,5,'Images/Wixarika/Alimentos/Pan','¡Recolectaste <i>paní</i> (pan)! ','76');
INSERT INTO `alimentos` VALUES (57,'POSTRE','Pan de elote','Panes de elote','Ikɨri paniyari','Ikɨri paniyari',10,5,'Images/Wixarika/Alimentos/Pan de elote','¡Recolectaste <i>ikɨri paniyari</i> (pan de elote)!','77');
INSERT INTO `alimentos` VALUES (58,'POSTRE','Plátano frito','Plátanos fritos','Ka’arú wiyamatɨ','Ka’arúte wiyamatika',10,5,'Images/Wixarika/Alimentos/Plátano frito','¡Recolectaste <i>Ka’arú wiyamatɨ</i> (plátano frito)!','78');
INSERT INTO `alimentos` VALUES (59,'POSTRE','Piloncillo','Piloncillos','Tsakaka','Tsakakate',10,5,'Images/Wixarika/Alimentos/Dulce','¡Recolectaste <i>tsakaka</i> (piloncillo)!','79');
INSERT INTO `alimentos` VALUES (60,'POSTRE','Pinole','Pinole','Pexúri','Pexúrite',10,5,'Images/Wixarika/Alimentos/Pinole','¡Recolectaste <i>pexúri</i> (pinole)!','80');
INSERT INTO `alimentos` VALUES (61,'COMIDA_MAIZ','Chilaquil','Chilaquiles','Xupaxi','Xupaxite',10,5,'Images/Wixarika/Alimentos/Chilaquil','¡Recolectaste <i>xupaxi</i> (chilaquil)!','81');
INSERT INTO `alimentos` VALUES (62,'COMIDA_MAIZ','Elote asado','Elotes asados','Ikɨri warikietɨ','Ikɨri wawarikitɨka',10,5,'Images/Wixarika/Alimentos/Elote asado','¡Recolectaste <i>ikɨri warikietɨ</i> (elote asado)!','82');
INSERT INTO `alimentos` VALUES (63,'COMIDA_MAIZ','Elote cocido','Elotes cocidos','Ikɨri kwitsarietɨ','Ikɨri kwitsarietɨka',10,5,'Images/Wixarika/Alimentos/Elote cocido','¡Recolectaste <i>ikɨri kwitsarietɨ</i> (elotes cocidos)!','83');
INSERT INTO `alimentos` VALUES (64,'COMIDA_MAIZ','Gordita','Gorditas','Tsuirá','Tsuiráte',10,5,'Images/Wixarika/Alimentos/Gordita','¡Recolectaste <i>tsuirá</i> (gordita)!','84');
INSERT INTO `alimentos` VALUES (65,'COMIDA_MAIZ','Taco','Tacos','Taku','Takuxi',10,5,'Images/Wixarika/Alimentos/Taco de frijol','¡Recolectaste <i>taku</i> (taco)!','85');
INSERT INTO `alimentos` VALUES (66,'COMIDA_MAIZ','Tortilla','Tortillas','Pa’apa','Papa’ate',10,5,'Images/Wixarika/Alimentos/Tortilla','¡Recolectaste <i>pa’apa</i> (tortilla)!','86');
INSERT INTO `alimentos` VALUES (67,'COMIDA_MAIZ','Tostada','Tostadas','Kɨxaɨ','Kɨxaɨte',10,5,'Images/Wixarika/Alimentos/Tostada','¡Recolectaste <i>kɨxaɨ</i> (tostada)!','87');
INSERT INTO `alimentos` VALUES (68,'COMIDA_MAIZ','Quesadilla','Quesadillas','Quesadilla','Quesadillas',10,5,'Images/Wixarika/Alimentos/Quesadilla','¡Recolectaste quesadillas!','88');
INSERT INTO `alimentos` VALUES (69,'COMIDA_MAIZ','Quesadilla con verdolaga','Quesadillas con verdolagas','Quesadilla con aɨraxate','Quesadillas con aɨraxate',10,5,'Images/Wixarika/Alimentos/Quesadilla con verdolaga','¡Recolectaste quesadilla con <i>aɨraxate</i>!','89');
INSERT INTO `alimentos` VALUES (70,'COMIDA_MAIZ','Tamal','Tamales','Tétsu','Tétsute',10,5,'Images/Wixarika/Alimentos/Tamal','¡Recolectaste <i>tétsu</i> (tamal)!','90');
INSERT INTO `alimentos` VALUES (71,'COMIDA_MAIZ','Tamal de rajas','Tamales de rajas','Kukuri tétsuyari','Kukuri tétsuteyari',10,5,'Images/Wixarika/Alimentos/Tamal de rajas','¡Recolectaste <i>kukuri tétsuyari</i> (tamal de rajas)!','91');
INSERT INTO `alimentos` VALUES (72,'COMIDA_FRIJOL','Frijol cocido','Frijoles cocidos','Múme kwakwaxitɨ','Múme kwakwaxitɨ',10,5,'Images/Wixarika/Alimentos/Frijol cocido','¡Recolectaste <i>múme kwakwaxitɨ</i> (frijol cocido)!','92');
INSERT INTO `alimentos` VALUES (73,'COMIDA_FRIJOL','Frijol frito','Frijoles fritos','Múme wiyamari','Múme wiyamarietɨka',10,5,'Images/Wixarika/Alimentos/Frijol frito','¡Recolectaste <i>múme wiyamari</i> (frijol cocido)!','93');
INSERT INTO `alimentos` VALUES (74,'COMIDA_FRIJOL','Taco de frijol','Tacos de frijoles','Múmete takuyari','Múmete takuxiyari',10,5,'Images/Wixarika/Alimentos/Taco de frijol','¡Recolectaste <i>múmete takuyari</i> (taco de frijol)! ','94');
INSERT INTO `alimentos` VALUES (75,'COMIDA_SOPA','Sopa','Sopas','Xupaxi','Xupaxite',10,5,'Images/Wixarika/Alimentos/Sopa de hongos','¡Recolectaste <i>xupaxi</i> (sopa)!','95');
INSERT INTO `alimentos` VALUES (76,'COMIDA_SOPA','Sopa de hongos','Sopas de hongos','Yekwa’ate itsari','Yekwa’ate itsari',10,5,'Images/Wixarika/Alimentos/Sopa de hongos','¡Recolectaste <i>yekwa’ate itsari</i> (sopa de hongos)!','96');
INSERT INTO `alimentos` VALUES (77,'COMIDA_SOPA','Sopa de verduras','Sopas de verduras','Mɨtitsitsiɨrawi ikwaiyari','Mɨtitsitsiɨrawi ikwaiyari',10,5,'Images/Wixarika/Alimentos/Sopa de verduras','¡Recolectaste <i>mɨtitsitsiɨrawi ikwaiyari</i> (sopa de verduras)!','97');
INSERT INTO `alimentos` VALUES (78,'COMIDA','Caldo','Caldos','Itsari','Itsarite',10,10,'Images/Wixarika/Alimentos/Sopa de hongos','¡Recolectaste <i>itsari</i> (caldo)!','98');
INSERT INTO `alimentos` VALUES (79,'COMIDA','Huevo','Huevos','Tawari','Tawarite',10,10,'Images/Wixarika/Alimentos/Huevo 1','¡Recolectaste <i>tawari</i> (huevo)!','99');
INSERT INTO `alimentos` VALUES (80,'COMIDA','Mole','Moles','Pexuri',NULL,10,10,'Images/Wixarika/Alimentos/Mole de jabalí','¡Recolectaste <i>pexuri</i> (mole)!','100');
INSERT INTO `alimentos` VALUES (81,'COMIDA','Pozole','Pozoles','kwitsari','Kwitsarite',10,10,'Images/Wixarika/Alimentos/Pozole de cerdo','¡Recolectaste <i>kwitsari</i> (pozole)!','101');
INSERT INTO `alimentos` VALUES (82,'COMIDA','Queso','Quesos','Kexiu','Kexiute',10,10,'Images/Wixarika/Alimentos/Queso','¡Recolectaste <i>kexiu</i>(queso)! ','102');
INSERT INTO `alimentos` VALUES (83,'COMIDA','Salsa','Salsas','Kukuri tsunariyari','Kukuríte tsunariyari',10,10,'Images/Wixarika/Alimentos/Salsa','¡Recolectaste <i>kukuri tsunariyari</i> (salsa)!','103');
INSERT INTO `alimentos` VALUES (84,'COMIDA_JABALI','Albondiga de jabalí','Albondigas de jabalí','Albóndiga de tuixuyeutanaka','Albóndigas de tuixuyeutanaka',10,20,'Images/Wixarika/Alimentos/Albóndiga de jabalí','¡Recolectaste albóndigas de <i>tuixuyeutanaka</i>! ','104');
INSERT INTO `alimentos` VALUES (85,'COMIDA_JABALI','Caldo de jabalí','Caldos de jabalí','Tuixu yeutanaka itsari','Tuixuri wa’itsariyari',10,20,'Images/Wixarika/Alimentos/Caldo de jabalí','¡Recolectaste <i>tuixu yeutanaka itsari</i>(caldo de jabalí)!','105');
INSERT INTO `alimentos` VALUES (86,'COMIDA_JABALI','Mole de jabalí','Moles de jabalí','Tuixu yeutanaka ikwaiyári','Tuixuri yeutari wa’ikwaiyari',10,20,'Images/Wixarika/Alimentos/Mole de jabalí','¡Recolectaste <i>tuixu yeutanaka ikwaiyári</i>(mole de jabalí)!','106');
INSERT INTO `alimentos` VALUES (87,'COMIDA_VENADO','Caldo de venado','Caldos de venado','Maxa itsari','Maxatsi wa’itsari',10,20,'Images/Wixarika/Alimentos/Caldo de venado','¡Recolectaste <i>maxa itsari</i>(caldo de venado)!','107');
INSERT INTO `alimentos` VALUES (88,'COMIDA_VENADO','Mole de venado','Moles de venado','Maxa ikwaiyári','Maxatsi wa’ikwaiyari',10,20,'Images/Wixarika/Alimentos/Mole de venado','¡Recolectaste <i>maxa ikwaiyári</i> (mole de venado)! ','108');
INSERT INTO `alimentos` VALUES (89,'COMIDA_ARDILLA','Caldo de ardilla','Caldos de ardilla','Tekɨ itsari','Tekɨri wa’itsarite',10,20,'Images/Wixarika/Alimentos/Caldo de ardilla','¡Recolectaste <i>tekɨ itsari</i> (caldo de ardilla)! ','109');
INSERT INTO `alimentos` VALUES (90,'COMIDA_ARDILLA','Mole de ardilla','Moles de ardilla','Tekɨ ikwaiyári','Tekɨri wa’ikwaiyari',10,20,'Images/Wixarika/Alimentos/Mole de ardilla','¡Recolectaste <i>tekɨ ikwaiyári</i>(mole de ardilla)!','110');
INSERT INTO `alimentos` VALUES (91,'COMIDA_IGUANA','Pipián de iguana','Pipián de iguana','Pipián de ke’etse','Pipián de ke’etse',10,20,'Images/Wixarika/Alimentos/Pipián de iguana','¡Recolectaste Pipián de <i>ke’etse</i>! ','111');
INSERT INTO `alimentos` VALUES (92,'COMIDA_PESCADO','Caldo de pescado','Caldos de pescado','Ketsɨ itsari','Ketsɨte wa’itsari',10,20,'Images/Wixarika/Alimentos/Caldo de pescado','¡Recolectaste <i>ketsɨ itsari</i>(caldo de pescado)!','112');
INSERT INTO `alimentos` VALUES (93,'COMIDA_PESCADO','Chicharrón de pescado','Chicharrones de pescado','Ketsɨ wiyamari','Ketsɨte wa wiyamari',10,20,'Images/Wixarika/Alimentos/Chicharrón de pescado','¡Recolectaste <i>ketsɨ wiyamari</i>(chicharrón de pescado)!','113');
INSERT INTO `alimentos` VALUES (94,'COMIDA_PESCADO','Pescado Sarandeado','Pescados Sarandeado','Ketsɨ warikietɨ','Ketsɨte me warikietɨkaitɨ',10,20,'Images/Wixarika/Alimentos/Pescado Sarandeado','¡Recolectaste <i>ketsɨ warikietɨ</i> (pescado sarandeado)! ','114');
INSERT INTO `alimentos` VALUES (95,'COMIDA_GUILOTA','Caldo de güilota','Caldos de güilota','Weurai itsari','Weuraixi wa’itsari',10,20,'Images/Wixarika/Alimentos/Caldo de güilota','¡Recolectaste <i>weurai itsari</i> (caldo de güilota)! ','115');
INSERT INTO `alimentos` VALUES (96,'COMIDA_GUILOTA','Mole de güilota','Moles de güilota','Weurai ikwaiyári','Weuraixi wa’ikwaiyari',10,20,'Images/Wixarika/Alimentos/Mole de güilota','¡Recolectaste <i>weurai ikwaiyári</i> (mole de güilota)! ','116');
INSERT INTO `alimentos` VALUES (97,'COMIDA_GUILOTA','Pipián de güilota','Pipián de güilota','Pipián de weurai','Pipián de weurai',10,20,'Images/Wixarika/Alimentos/Pipián de güilota','¡Recolectaste <i>pipián de weurai</i> (pipián de güilota)! ','117');
INSERT INTO `alimentos` VALUES (98,'COMIDA_POLLO','Caldo de pollo','Caldos de pollo','Wakana itsari','Wakanarí wa itsariyari',10,20,'Images/Wixarika/Alimentos/Caldo de pollo','¡Recolectaste <i>wakana itsari</i> (caldo de pollo)! ','118');
INSERT INTO `alimentos` VALUES (99,'COMIDA_POLLO','Enchilada de pollo','Enchiladas de pollo','Enchilada de wakana','Enchiladas de wakana',10,20,'Images/Wixarika/Alimentos/Enchilada de pollo','¡Recolectaste enchilada de <i>wakana</i> (enchilada de pollo)! ','119');
INSERT INTO `alimentos` VALUES (100,'COMIDA_POLLO','Mole de pollo','Moles de pollo','Wakana ikwaiyári','Wakanari wa’ikwaiyari',10,20,'Images/Wixarika/Alimentos/Mole de pollo','¡Recolectaste <i>wakana ikwaiyári</i>(mole de pollo)!','120');
INSERT INTO `alimentos` VALUES (101,'COMIDA_POLLO','Pozole de pollo','Pozoles de pollo','Wakana kwitsariyari','Wakana wa’kwitsariyari',10,20,'Images/Wixarika/Alimentos/Pozole de cerdo','¡Recolectaste <i>wakana kwitsariyari</i>(pozole de cerdo)!','121');
INSERT INTO `alimentos` VALUES (102,'COMIDA_POLLO','Tamal de pollo','Tamales de pollo','Wakana tetsuyari','Wakana tetsuteyari',10,20,'Images/Wixarika/Alimentos/Tamal de pollo','¡Recolectaste <i>wakana tetsuyari</i>(tamal de pollo)!','122');
INSERT INTO `alimentos` VALUES (103,'COMIDA_CERDO','Pipián de cerdo','Pipián de cerdo','Pipián de tuixu','Pipián de tuixu',10,20,'Images/Wixarika/Alimentos/Pipián de cerdo','¡Recolectaste <i>pipián de tuixu</i>(pipián de cerdo)! ','123');
INSERT INTO `alimentos` VALUES (104,'COMIDA_CERDO','Pozole de cerdo','Pozoles de cerdo','Tuixu kwitsariyari','Tuixuri wa kwitsariyari',10,20,'Images/Wixarika/Alimentos/Pozole de cerdo','¡Recolectaste <i>tuixu kwitsariyari</i>(pozole de cerdo)!','124');
INSERT INTO `alimentos` VALUES (105,'COMIDA_CERDO','Tamal de cerdo','Tamales de cerdo','Tuixu tetsuyari','Tuixu tetsuteyari',10,20,'Images/Wixarika/Alimentos/Tamal de cerdo','¡Recolectaste <i>tuixu tetsuyari</i>(tamal de cerdo)!','125');
INSERT INTO `alimentos` VALUES (106,'COMIDA_CONEJO','Mole de conejo','Moles de conejo','Tátsiu ikwaiyári','Tatsiurixi wa’ikwaiyari',10,20,'Images/Wixarika/Alimentos/Mole de conejo','¡Recolectaste <i>tátsiu ikwaiyári</i>(mole de conejo)! ','126');
INSERT INTO `alimentos` VALUES (107,'COMIDA_CONEJO','Caldo de conejo','Caldos de conejo','Tátsiu itsari','Tatsiurixi wa’itsárite',10,20,'Images/Wixarika/Alimentos/Caldo de conejo','¡Recolectaste <i>tátsiu itsari</i> (caldo de conejo)! ','127');
INSERT INTO `alimentos` VALUES (108,'OBJETOS','Conchita','Conchitas','Conchita','Conchitas',10,5,'Images/Wixarika/Vegetacion/Plantas de Agua/Concha 1','¡Recolectaste una concha! 
Intercámbiala por <i>ikú</i> (maíz).','128,129');
INSERT INTO `alimentos` VALUES (109,'OBJETOS','Caracol','Caracoles','Caracol','Caracoles',10,5,'Images/Wixarika/Vegetacion/Plantas de agua/Caracol 3x','¡Recolectaste un caracol! 
Intercámbialo por <i>ikú</i> (maíz).','130,131');
INSERT INTO `alimentos` VALUES (110,'VERDURA','Pochote','Pochotes','Pochote','Pochotes',10,3,'Images/Wixarika/Alimentos/Pochote','¡Recolectaste pochote!','132');
INSERT INTO `alimentos` VALUES (111,'VERDURA','Cebolla','Cebollas','Cebolla','Cebollas',10,3,'Images/Wixarika/Alimentos/Cebolla','¡Recolectaste cebolla!','133');
INSERT INTO `alimentos` VALUES (112,'VERDURA','Haba','Habas','Haba','Habas',10,3,'Images/Wixarika/Alimentos/Habas','¡Recolectaste Habas!',NULL);
INSERT INTO `alimentos` VALUES (113,'VERDURA','Gualumbo','Gualumbos','Gualumbo','Gualumbos',10,3,'Images/Wixarika/Alimentos/Gualumbo rama 1','¡Recolectaste Gualumbo!',NULL);
INSERT INTO `armas` VALUES (1,'ARCO','Arco','Arcos','Tɨɨpi','Tɨɨpite',1,200,30,-2.0,'Images/Wixarika/Armas/Arco','¡Obtuviste un <i>tɨɨpi</i> (arco)!
El <i>tɨɨpi</i> (arco) se utiliza para la cacería, especialmente, en el ritual sagrado de la caza del venado cola blanca.','1,2');
INSERT INTO `armas` VALUES (2,'CUCHILLO','Cuchillo','Cuchillos','Nawaxa','Nawaxate',1,200,15,-2.0,'Images/Wixarika/Armas/Cuchillo C1','¡Obtuviste un <i>nawaxa</i> (cuchillo)!
Utiliza el <i>nawaxa</i> (cuchillo) para defenderte y romper objetos.','3,4');
INSERT INTO `armas` VALUES (3,'CUÑA','Cuña','Cuñas','Mɨtsɨtɨari','Mɨtsɨtɨarite',1,200,20,-1.0,'Images/Wixarika/Armas/Cuña 1','¡Obtuviste una <i>mɨtsɨtɨari</i> (cuña)!
La <i>mɨtsɨtɨari</i> (cuña) se utiliza para cortar madera o trabajar el campo.','5,6');
INSERT INTO `armas` VALUES (4,'HACHA','Hacha','Hachas','Ha’tsa','Ha’tsate',1,200,30,-4.0,'Images/Wixarika/Armas/Hacha 2','¡Obtuviste una <i>ha’tsa</i> (hacha)!
La <i>ha’tsa</i> (hacha) se utiliza para talar árboles.','7,8');
INSERT INTO `armas` VALUES (5,'MACHETE','Machete','Machetes','Kutsira','Kutsirate',1,200,30,-3.0,'Images/Wixarika/Armas/Machete 2','¡Obtuviste un <i>kutsira</i> (machete)!
El <i>kutsira</i> (machete) se utiliza para trabajar en los cultivos.','9,10');
INSERT INTO `armas` VALUES (6,'MALLA','Malla de cacería','Mallas de cacería','Winiyari','Winiyarite',1,200,1,-0.5,'Images/Wixarika/Armas/Malla de cacería','¡Obtuviste una <i>winiyari</i> (malla de cacería)!
Utiliza la <i>winiyari</i> (malla de cacería) para cazar.','11,12');
INSERT INTO `armas` VALUES (7,'PALO','Palo','Palos','Kɨyé','Kɨyéxi',1,0,100,-0.5,'Images/Wixarika/Armas/Palo','¡Encontraste un <i>kɨyé</i> (palo)! 
Utiliza el <i>kɨyé</i> (palo) para defenderte y romper objetos.','13,14');
INSERT INTO `armas` VALUES (8,'ARCO','Arco','Arcos','Tɨɨpi','Tɨɨpite',2,300,40,-2.5,'Images/Wixarika/Armas/Arco','¡Obtuviste un <i>tɨɨpi</i> (arco)!
El <i>tɨɨpi</i> (arco) se utiliza para la cacería, especialmente, en el ritual sagrado de la caza del venado cola blanca.','15,16');
INSERT INTO `armas` VALUES (9,'CUCHILLO','Cuchillo','Cuchillos','Nawaxa','Nawaxate',2,300,20,-2.5,'Images/Wixarika/Armas/Cuchillo C2','¡Obtuviste un <i>nawaxa</i> (cuchillo)!
Utiliza el <i>nawaxa</i> (cuchillo) para defenderte y romper objetos.','17,18');
INSERT INTO `armas` VALUES (10,'CUÑA','Cuña','Cuñas','Mɨtsɨtɨari','Mɨtsɨtɨarite',2,300,30,-1.5,'Images/Wixarika/Armas/Cuña 2','¡Obtuviste una <i>mɨtsɨtɨari</i> (cuña)!
La <i>mɨtsɨtɨari</i> (cuña) se utiliza para cortar madera o trabajar el campo.','19,20');
INSERT INTO `armas` VALUES (11,'HACHA','Hacha','Hachas','Ha’tsa','Ha’tsate',2,300,40,-4.5,'Images/Wixarika/Armas/Hacha 3','¡Obtuviste una <i>ha’tsa</i> (hacha)!
La <i>ha’tsa</i> (hacha) se utiliza para talar árboles.','21,22');
INSERT INTO `armas` VALUES (12,'MACHETE','Machete','Machetes','Kutsira','Kutsirate',2,300,40,-3.5,'Images/Wixarika/Armas/Machete 3','¡Obtuviste un <i>kutsira</i> (machete)!
El <i>kutsira</i> (machete) se utiliza para trabajar en los cultivos.','23,24');
INSERT INTO `armas` VALUES (13,'MALLA','Malla de cacería','Malla de cacerías','Winiyari','Winiyarite',2,300,1,-0.5,'Images/Wixarika/Armas/Malla de cacería','¡Obtuviste una <i>winiyari</i> (malla de cacería)!
Utiliza la <i>winiyari</i> (malla de cacería) para cazar.','25,26');
INSERT INTO `armas` VALUES (14,'ARCO','Arco','Arcos','Tɨɨpi','Tɨɨpite',3,400,50,-3.0,'Images/Wixarika/Armas/Arco','¡Obtuviste un <i>tɨɨpi</i> (arco)!
El <i>tɨɨpi</i> (arco) se utiliza para la cacería, especialmente, en el ritual sagrado de la caza del venado cola blanca.','27,28');
INSERT INTO `armas` VALUES (15,'CUCHILLO','Cuchillo','Cuchillos','Nawaxa','Nawaxate',3,400,25,-3.0,'Images/Wixarika/Armas/Cuchillo C3','¡Obtuviste un <i>nawaxa</i> (cuchillo)!
Utiliza el <i>nawaxa</i> (cuchillo) para defenderte y romper objetos.','29,30');
INSERT INTO `armas` VALUES (16,'CUÑA','Cuña','Cuñas','Mɨtsɨtɨari','Mɨtsɨtɨarite',3,400,40,-2.0,'Images/Wixarika/Armas/Cuña 3','¡Obtuviste una <i>mɨtsɨtɨari</i> (cuña)!
La <i>mɨtsɨtɨari</i> (cuña) se utiliza para cortar madera o trabajar el campo.','31,32');
INSERT INTO `armas` VALUES (17,'HACHA','Hacha','Hachas','Ha’tsa','Ha’tsate',3,400,50,-5.0,'Images/Wixarika/Armas/Hacha 4','¡Obtuviste una <i>ha’tsa</i> (hacha)!
La <i>ha’tsa</i> (hacha) se utiliza para talar árboles.','33,34');
INSERT INTO `armas` VALUES (18,'MACHETE','Machete','Machetes','Kutsira','Kutsirate',3,400,50,-4.0,'Images/Wixarika/Armas/Machete 4','¡Obtuviste un <i>kutsira</i> (machete)!
El <i>kutsira</i> (machete) se utiliza para trabajar en los cultivos.','35,36');
INSERT INTO `armas` VALUES (19,'MALLA','Malla de cacería','Malla de cacerías','Winiyari','Winiyarite',3,400,1,-0.5,'Images/Wixarika/Armas/Malla de cacería','¡Obtuviste una <i>winiyari</i> (malla de cacería)!
Utiliza la <i>winiyari</i> (malla de cacería) para cazar.','37,38');
INSERT INTO `armas` VALUES (20,'FLECHA','Flecha','Flechas','10 Ɨ’rɨte','10 Ɨ’rɨte',1,200,1,-1.0,'Images/Wixarika/Armas/Flechas pack','¡Obtuviste unas <i>ɨ’rɨte</i> (flechas)!
Las <i>ɨ’rɨte</i> (flechas) se utilizan para la cacería del <i>maxa</i> (venado) y luchar contra las fuerzas de la oscuridad.','39,40');
INSERT INTO `armas` VALUES (21,'FLECHA','Flecha','Flechas','10 Ɨ’rɨte','10 Ɨ’rɨte',2,300,1,-2.0,'Images/Wixarika/Armas/Flechas pack','¡Obtuviste unas <i>ɨ’rɨte</i> (flechas)!
Las <i>ɨ’rɨte</i> (flechas) se utilizan para la cacería del <i>maxa</i> (venado) y luchar contra las fuerzas de la oscuridad.','41,42');
INSERT INTO `armas` VALUES (22,'FLECHA','Flecha','Flechas','10 Ɨ’rɨte','10 Ɨ’rɨte',3,400,1,-3.0,'Images/Wixarika/Armas/Flechas pack','¡Obtuviste unas <i>ɨ’rɨte</i> (flechas)!
Las <i>ɨ’rɨte</i> (flechas) se utilizan para la cacería del <i>maxa</i> (venado) y luchar contra las fuerzas de la oscuridad.','43,44');
INSERT INTO `armas` VALUES (23,'ARCO','Arco','Arcos','Tɨɨpi','Tɨɨpite',4,600,65,-3.5,'Images/Wixarika/Armas/Arco','¡Obtuviste un <i>tɨɨpi</i> (arco)!
El <i>tɨɨpi</i> (arco) se utiliza para la cacería, especialmente, en el ritual sagrado de la caza del venado cola blanca.','45,46');
INSERT INTO `armas` VALUES (24,'ARCO','Arco','Arcos','Tɨɨpi','Tɨɨpite',5,800,80,-6.5,'Images/Wixarika/Armas/Arco','¡Obtuviste un <i>tɨɨpi</i> (arco)!
El <i>tɨɨpi</i> (arco) se utiliza para la cacería, especialmente, en el ritual sagrado de la caza del venado cola blanca.','47,48');
INSERT INTO `armas` VALUES (25,'ARCO','Arco','Arcos','Tɨɨpi','Tɨɨpite',6,1000,100,-7.0,'Images/Wixarika/Armas/Arco','¡Obtuviste un <i>tɨɨpi</i> (arco)!
El <i>tɨɨpi</i> (arco) se utiliza para la cacería, especialmente, en el ritual sagrado de la caza del venado cola blanca.','49,50');
INSERT INTO `armas` VALUES (26,'CUCHILLO','Cuchillo','Cuchillos','Nawaxa','Nawaxate',4,600,35,-3.5,'Images/Wixarika/Armas/Cuchillo C3','¡Obtuviste un <i>nawaxa</i> (cuchillo)!
Utiliza el <i>nawaxa</i> (cuchillo) para defenderte y romper objetos.','51,52');
INSERT INTO `armas` VALUES (27,'CUCHILLO','Cuchillo','Cuchillos','Nawaxa','Nawaxate',5,800,45,-4.0,'Images/Wixarika/Armas/Cuchillo C4','¡Obtuviste un <i>nawaxa</i> (cuchillo)!
Utiliza el <i>nawaxa</i> (cuchillo) para defenderte y romper objetos.','53,54');
INSERT INTO `armas` VALUES (28,'CUCHILLO','Cuchillo','Cuchillos','Nawaxa','Nawaxate',6,1000,60,-4.5,'Images/Wixarika/Armas/Cuchillo C5','¡Obtuviste un <i>nawaxa</i> (cuchillo)!
Utiliza el <i>nawaxa</i> (cuchillo) para defenderte y romper objetos.','55,56');
INSERT INTO `armas` VALUES (29,'CUÑA','Cuña','Cuñas','Mɨtsɨtɨari','Mɨtsɨtɨarite',4,600,55,-2.5,'Images/Wixarika/Armas/Cuña 4','¡Obtuviste una <i>mɨtsɨtɨari</i> (cuña)!
La <i>mɨtsɨtɨari</i> (cuña) se utiliza para cortar madera o trabajar el campo.','57,58');
INSERT INTO `armas` VALUES (30,'CUÑA','Cuña','Cuñas','Mɨtsɨtɨari','Mɨtsɨtɨarite',5,800,70,-3.0,'Images/Wixarika/Armas/Cuña 5','¡Obtuviste una <i>mɨtsɨtɨari</i> (cuña)!
La <i>mɨtsɨtɨari</i> (cuña) se utiliza para cortar madera o trabajar el campo.','59,60');
INSERT INTO `armas` VALUES (31,'CUÑA','Cuña','Cuñas','Mɨtsɨtɨari','Mɨtsɨtɨarite',6,1000,90,-3.5,'Images/Wixarika/Armas/Cuña 6','¡Obtuviste una <i>mɨtsɨtɨari</i> (cuña)!
La <i>mɨtsɨtɨari</i> (cuña) se utiliza para cortar madera o trabajar el campo.','61,62');
INSERT INTO `armas` VALUES (32,'HACHA','Hacha','Hachas','Ha’tsa','Ha’tsate',4,600,70,-5.5,'Images/Wixarika/Armas/Hacha 5','¡Obtuviste una <i>ha’tsa</i> (hacha)!
La <i>ha’tsa</i> (hacha) se utiliza para talar árboles.','69,70');
INSERT INTO `armas` VALUES (33,'HACHA','Hacha','Hachas','Ha’tsa','Ha’tsate',5,800,90,-6.0,'Images/Wixarika/Armas/Hacha 6','¡Obtuviste una <i>ha’tsa</i> (hacha)!
La <i>ha’tsa</i> (hacha) se utiliza para talar árboles.','71,72');
INSERT INTO `armas` VALUES (34,'HACHA','Hacha','Hachas','Ha’tsa','Ha’tsate',6,1000,120,-7.0,'Images/Wixarika/Armas/Hacha 7','¡Obtuviste una <i>ha’tsa</i> (hacha)!
La <i>ha’tsa</i> (hacha) se utiliza para talar árboles.','73,74');
INSERT INTO `armas` VALUES (35,'MACHETE','Machete','Machetes','Kutsira','Kutsirate',4,600,65,-4.5,'Images/Wixarika/Armas/Machete 5','¡Obtuviste un <i>kutsira</i> (machete)!
El <i>kutsira</i> (machete) se utiliza para trabajar en los cultivos.','75,76');
INSERT INTO `armas` VALUES (36,'MACHETE','Machete','Machetes','Kutsira','Kutsirate',5,800,80,-5.0,'Images/Wixarika/Armas/Machete 6','¡Obtuviste un <i>kutsira</i> (machete)!
El <i>kutsira</i> (machete) se utiliza para trabajar en los cultivos.','77,78');
INSERT INTO `armas` VALUES (37,'MACHETE','Machete','Machetes','Kutsira','Kutsirate',6,1000,100,-5.5,'Images/Wixarika/Armas/Machete 7','¡Obtuviste un <i>kutsira</i> (machete)!
El <i>kutsira</i> (machete) se utiliza para trabajar en los cultivos.','79,80');
INSERT INTO `armas` VALUES (38,'MALLA','Malla de cacería','Mallas de cacería','Winiyari','Winiyarite',4,600,1,-0.5,'Images/Wixarika/Armas/Malla de cacería','¡Obtuviste una <i>winiyari</i> (malla de cacería)!
Utiliza la <i>winiyari</i> (malla de cacería) para cazar.','81,82');
INSERT INTO `armas` VALUES (39,'MALLA','Malla de cacería','Malla de cacerías','Winiyari','Winiyarite',5,800,1,-0.5,'Images/Wixarika/Armas/Malla de cacería','¡Obtuviste una <i>winiyari</i> (malla de cacería)!
Utiliza la <i>winiyari</i> (malla de cacería) para cazar.','83,84');
INSERT INTO `armas` VALUES (40,'MALLA','Malla de cacería','Malla de cacerías','Winiyari','Winiyarite',6,1000,1,-0.5,'Images/Wixarika/Armas/Malla de cacería','¡Obtuviste una <i>winiyari</i> (malla de cacería)!
Utiliza la <i>winiyari</i> (malla de cacería) para cazar.','85,86');
INSERT INTO `armas` VALUES (41,'ANTORCHA','Antorcha','Antorchas','Utsí','Utsi taiyari',1,500,1,-0.5,NULL,'¡Obtuviste una <i>utsí</i> (antorcha)!
Utiliza la <i>utsí</i> (antorcha) para iluminar zonas obscuras.','87,88');
INSERT INTO `armas` VALUES (42,'AZADON','Azadón','Azadones','Hatsaruni','Hatsarunite',1,100,1,-0.5,NULL,'¡Obtuviste un <i>hatsaruni</i> (azadón)!
El <i>hatsaruni</i> (azadón) se utiliza para trabajar en los cultivos.','89,90');
INSERT INTO `armas` VALUES (43,'ESCALERA','Escalera','Escaleras','Imúmui','Imúmuite',1,100,1,0.0,NULL,'¡Obtuviste una <i>imúmui</i> (escalera)!
Utiliza la <i>imúmui</i> (escalera) para subir o bajar árboles y cerros.','91,92');
INSERT INTO `armas` VALUES (44,'PETACA','Petaca','Petacas','Kiriwa','Kiriwate',1,100,1,0.0,'','¡Obtuviste una <i>kiriwa</i> (petaca)!
Utiliza la <i>kiriwa</i> (petaca) para guardar objetos.','93,94');
INSERT INTO `armas` VALUES (45,'RED','Red de pesca','Redes de pesca','Wipí','Wipiite',1,100,1,0.0,NULL,'¡Obtuviste una <i>wipí</i> (red de pesca)!
Utiliza la <i>wipí</i> (red de pesca) para pescar.','95,96');
INSERT INTO `armas` VALUES (46,'ROCA','Roca','Rocas','Ha’úte','Ha’utete',1,100,1,0.0,NULL,'¡Encontraste una <i>ha’úte</i> (roca)!
Utiliza la <i>ha’úte</i> (roca) para defenderte y romper objetos.','97,98');
INSERT INTO `armas` VALUES (47,'SOGA','Soga/Cuerda','Sogas/Cuerdas','Kaunari','Kaunarite',1,100,1,0.0,NULL,'¡Obtuviste una <i>kaunari</i> (soga)!
Utiliza la <i>kaunari</i> (soga) para subir o bajar árboles y cerros.','99,100');
INSERT INTO `armas` VALUES (48,'TIJERA','Tijera','Tijeras','Tixikame','Tixikamete',1,100,1,0.0,NULL,'¡Obtuviste unas <i>tixikamete</i> (tijeras)!
Utiliza las <i>tixikamete</i> (tijeras) para cortar objetos.','101,102');
INSERT INTO `objetos_especiales` VALUES (1,'Guitarra pequeña','Guitarras','Kanari','Kanarite',500,'Images/Wixarika/Objetos_Especiales/Guitarra pequeña',NULL,NULL);
INSERT INTO `objetos_especiales` VALUES (2,'Maraca','Maracas','Kaitsa','Kaitsate',300,'Images/Wixarika/Objetos_Especiales/Maraca',NULL,NULL);
INSERT INTO `objetos_especiales` VALUES (3,'Tambor','Tambores','Tepu','Tepute',300,'Images/Wixarika/Objetos_Especiales/Tambor',NULL,NULL);
INSERT INTO `objetos_especiales` VALUES (4,'Violín','Violines','Xaweri','Xawerite',500,'Images/Wixarika/Objetos_Especiales/Violín',NULL,NULL);
INSERT INTO `objetos_especiales` VALUES (5,'Espejo','Espejos','Xikɨri','Xikɨrite',200,'Images/Wixarika/Objetos_Especiales/Espejo',NULL,NULL);
INSERT INTO `objetos_especiales` VALUES (6,'Plumon','Plumones','Ti’utɨwawe','Ti’utɨwamete',300,'Images/Wixarika/Objetos_Especiales/Plumon',NULL,NULL);
INSERT INTO `objetos_especiales` VALUES (7,'Juguete','Juguetes','Waikari','Waikarite',300,'Images/Wixarika/Objetos_Especiales/Juguete',NULL,NULL);
INSERT INTO `objetos_especiales` VALUES (8,'Muñeca','Muñecas','Muneka ','Munekarite',300,'Images/Wixarika/Objetos_Especiales/Muñeca',NULL,NULL);
INSERT INTO `objetos_especiales` VALUES (9,'Olla','Ollas','Xa’ari','Xa’arite',300,'Images/Wixarika/Objetos_Especiales/Olla',NULL,NULL);
INSERT INTO `objetos_especiales` VALUES (10,'Pintura','Pinturas','Tiutɨwame','Ti utɨwamete ',300,'Images/Wixarika/Objetos_Especiales/Pintura',NULL,NULL);
INSERT INTO `objetos_especiales` VALUES (11,'Campana de becerro','Campanas de becerro','Tsikeru kapanaya','Tsikerutsixi wakapanate',300,'Images/Wixarika/Objetos_Especiales/Campana de becerro',NULL,NULL);
INSERT INTO `objetos_especiales` VALUES (12,'Tijera','Tijeras ','Tixikame','Tixikamete',300,'Images/Wixarika/Objetos_Especiales/Tijera',NULL,NULL);
INSERT INTO `objetos_especiales` VALUES (13,'Azadón ','Azadones','Hatsaruni','Hatsarunite',300,'Images/Wixarika/Objetos_Especiales/Azadón ',NULL,NULL);
INSERT INTO `objetos_especiales` VALUES (14,'Vela ','Velas','Katira','Katirate',500,'Images/Wixarika/Objetos_Especiales/Vela ',NULL,NULL);
INSERT INTO `objetos_espirituales` VALUES (1,'Ojo de dios','Ojos de dios','Tsik ɨri','Tsik ɨrite',30,20,1000,'Images/Wixarika/Objetos_Espirituales/Ojo de dios','¡Formaste tu primera piníte (ofrenda)!

Has obtenido un tsik ɨrite (ojo de dios), ¡un artículo muy especial!

El tsik ɨrite es una ofrenda para una deidad. Consiste en cinco rombos de cruces de madera que son tejidos con wíta (estambre) de colores.

Simboliza los cinco puntos cardinales del Wixárika; norte, sur, oriente y poniente, arriba y abajo. Sirve para ver y entender las cosas desconocidas.

',NULL);
INSERT INTO `objetos_espirituales` VALUES (2,'Jícara','Jícaras','Xuku’uri','Xukúrite',30,20,1000,'Images/Wixarika/Objetos_Espirituales/Jícara','La xukúri se vinculan con las deidades femeninas, como el maíz, la fertilidad, la temporada de lluvias, el poniente, el inframundo, la oscuridad y la noche.',NULL);
INSERT INTO `objetos_espirituales` VALUES (3,'Flecha','Flechas','Ɨ’rɨte','Ɨ’rɨte',30,20,1000,'Images/Wixarika/Objetos_Espirituales/Flecha','Las ɨ´rɨte simbolizan el género masculino por ser un elemento empleado para la caza.Se les atribuyen los poderes místicos que poseen las aves, por que se adornan con plumas del ave relacionada con la deidad a la que se le hará la petición.',NULL);
INSERT INTO `objetos_espirituales` VALUES (4,'Tablilla nierika','Tablilla nierika','Nierika','Nierikate',50,20,1000,'Images/Wixarika/Objetos_Espirituales/Tablilla','Has obtenido un nierika (tablilla de estambre).

Las figuras plasmadas en la tablilla están relacionadas con experiencias producidas por el peyote, en las que se cree es posible ver los rostros de las deidades. 

Las nierika son resultado de una visión mística producida por el peyote, son una oración en una ofrenda y una imagen gráfica de los dioses.

',NULL);
INSERT INTO `objetos_espirituales` VALUES (5,'Máscara','Máscaras','Tsikwaki nierikaya','Tsikwaki nierikaya',50,20,1000,'Images/Wixarika/Objetos_Espirituales/Máscara','Las máscaras representan un momento imprescindible en la cultura wixárika: el contacto con el mestizo. ',NULL);
INSERT INTO `quiz_pista` VALUES (1,1,'<i>Ikɨri</i> significa elote.',3,'1');
INSERT INTO `quiz_pista` VALUES (2,1,'<i>Múme</i> significa frijol.',5,'2');
INSERT INTO `quiz_pista` VALUES (3,1,'<i>Haxi</i> significa guaje.',7,'3');
INSERT INTO `quiz_pista` VALUES (4,2,'<i>Xutsíte</i> significa calabacitas.',3,'4');
INSERT INTO `quiz_pista` VALUES (5,2,'<i>Kukuríte</i> significa chiles.',5,'5');
INSERT INTO `quiz_pista` VALUES (6,2,'<i>Múmete</i> significa frijoles.',7,'6');
INSERT INTO `quiz_pista` VALUES (7,3,'Chile no significa <i>ikú</i>.',3,'7');
INSERT INTO `quiz_pista` VALUES (8,3,'Nopal no significa <i>ikú</i>.',5,'8');
INSERT INTO `quiz_pista` VALUES (9,3,'Frijoles no significa <i>ikú</i>.',7,'9');
INSERT INTO `quiz_pista` VALUES (10,4,'El nopal no es un alimento de importancia espiritual.',3,'10');
INSERT INTO `quiz_pista` VALUES (11,4,'El frijol no es un alimento de importancia espiritual.',5,'11');
INSERT INTO `quiz_pista` VALUES (12,4,'El chile no es un alimento de importancia espiritual.',7,'12');
INSERT INTO `quiz_pista` VALUES (13,5,'<i>Kukúri</i> significa chile.',3,'13');
INSERT INTO `quiz_pista` VALUES (14,5,'<i>Ikú</i> significa maíz.',5,'14');
INSERT INTO `quiz_pista` VALUES (15,5,'<i>Xútsi</i> significa calabacita.',7,'15');
INSERT INTO `quiz_pista` VALUES (16,6,'<i>Haxite</i> significa guajes.',3,'16');
INSERT INTO `quiz_pista` VALUES (17,6,'<i>Iku’ute</i> significa maíces.',5,'17');
INSERT INTO `quiz_pista` VALUES (18,6,'<i>Ikɨríte</i> significa elotes.',7,'18');
INSERT INTO `quiz_pista` VALUES (19,7,'Nopales no significa <i>múme</i>.',3,'19');
INSERT INTO `quiz_pista` VALUES (20,7,'Maíces no significa <i>múme</i>.',5,'20');
INSERT INTO `quiz_pista` VALUES (21,7,'Chiles no significa <i>múme</i>.',7,'21');
INSERT INTO `quiz_pista` VALUES (22,8,'<i>Retsi</i> significa leche.',3,'22');
INSERT INTO `quiz_pista` VALUES (23,8,'<i>Nawá</i> significa tejuino.',5,'23');
INSERT INTO `quiz_pista` VALUES (24,8,'<i>Tsinari</i> significa chicuatol.',7,'24');
INSERT INTO `quiz_pista` VALUES (25,9,'Tejuino no significa <i>ha’a</i>.',3,'25');
INSERT INTO `quiz_pista` VALUES (26,9,'Atole no significa <i>ha’a</i>.',5,'26');
INSERT INTO `quiz_pista` VALUES (27,9,'Leche no significa <i>ha’a</i>.',7,'27');
INSERT INTO `quiz_pista` VALUES (28,10,'<i>Kɨxaɨ</i> significa tostada.',3,'28');
INSERT INTO `quiz_pista` VALUES (29,10,'<i>Tétsu</i> significa tamal.',5,'29');
INSERT INTO `quiz_pista` VALUES (30,10,'<i>Taku</i> significa taco.',7,'30');
INSERT INTO `quiz_pista` VALUES (31,11,'Taco no significa <i>pa’apa</i>.',3,'31');
INSERT INTO `quiz_pista` VALUES (32,11,'Tamal no significa <i>pa’apa</i>.',5,'32');
INSERT INTO `quiz_pista` VALUES (33,11,'Tostada no significa <i>pa’apa</i>.',7,'33');
INSERT INTO `quiz_pista` VALUES (34,12,'<i>Wakana</i> significa jabalí.',3,'34');
INSERT INTO `quiz_pista` VALUES (35,12,'<i>Tuixu</i> significa jabalí.',5,'35');
INSERT INTO `quiz_pista` VALUES (36,12,'<i>Ke’etsé</i> significa jabalí.',7,'36');
INSERT INTO `quiz_pista` VALUES (37,13,'<i>Máye</i> significa venado.',3,'37');
INSERT INTO `quiz_pista` VALUES (38,13,'<i>Tuixuyeutanaka</i> significa venado.',5,'38');
INSERT INTO `quiz_pista` VALUES (39,13,'<i>Tuuká</i> significa venado.',7,'39');
INSERT INTO `quiz_pista` VALUES (40,14,'<i>Tuixuyeutanaka</i> significa puma.',3,'40');
INSERT INTO `quiz_pista` VALUES (41,14,'<i>Tuuká</i> significa puma.',5,'41');
INSERT INTO `quiz_pista` VALUES (42,14,'<i>Maxa</i> significa puma.',7,'42');
INSERT INTO `quiz_pista` VALUES (43,15,'<i>Maxa</i> significa araña.',3,'43');
INSERT INTO `quiz_pista` VALUES (44,15,'<i>Máye</i> significa araña.',5,'44');
INSERT INTO `quiz_pista` VALUES (45,15,'<i>Tuixuyeutanaka</i> significa araña.',7,'45');
INSERT INTO `quiz_pista` VALUES (46,16,'<i>Nawaxa</i> significa cuchillo.',3,'46');
INSERT INTO `quiz_pista` VALUES (47,16,'<i>Tɨɨpi</i> significa arco.',5,'47');
INSERT INTO `quiz_pista` VALUES (48,16,'<i>Kutsira</i> significa machete.',7,'48');
INSERT INTO `quiz_pista` VALUES (49,17,'La playa no es un sitio sagrado.',3,'49');
INSERT INTO `quiz_pista` VALUES (50,17,'Del Nayar no es un sitio sagrado.',5,'50');
INSERT INTO `quiz_pista` VALUES (51,17,'El bosque no es un sitio sagrado.',7,'51');
INSERT INTO `quiz_pista` VALUES (52,18,'El bosque no es un sitio sagrado.',3,'52');
INSERT INTO `quiz_pista` VALUES (53,18,'La playa no es un sitio sagrado.',5,'53');
INSERT INTO `quiz_pista` VALUES (54,18,'Del Nayar no es un sitio sagrado.',7,'54');
INSERT INTO `quiz_pista` VALUES (55,19,'<i>Tupiri</i> significa policía.',3,'55');
INSERT INTO `quiz_pista` VALUES (56,19,'<i>Kawiteru</i> significa anciano sabio.',5,'56');
INSERT INTO `quiz_pista` VALUES (57,19,'<i>Tatuwani</i> significa gobernador.',7,'57');
INSERT INTO `quiz_pista` VALUES (58,20,'Un <i>tiɨkitame</i> no ayuda a curar a las personas, no oficia actos religiosos y no es una autoridad en las comunidades.',3,'58');
INSERT INTO `quiz_pista` VALUES (59,20,'Un<i> titsatsawem</i>e no ayuda a curar a las personas, no oficia actos religiosos y no es una autoridad en las comunidades.',5,'59');
INSERT INTO `quiz_pista` VALUES (60,20,'Un <i>watame</i> no ayuda a curar a las personas, no oficia actos religiosos y no es una autoridad en las comunidades.',7,'60');
INSERT INTO `quiz_pista` VALUES (61,21,'<i>Tiyuitɨwame</i> significa músico.',3,'61');
INSERT INTO `quiz_pista` VALUES (62,21,'<i>Tewakame</i> significa ganadero.',5,'62');
INSERT INTO `quiz_pista` VALUES (63,21,'<i>Ketsɨtame</i> significa pescador.',7,'63');
INSERT INTO `quiz_pista` VALUES (64,22,'En el agua no siembran sus cultivos.',3,'64');
INSERT INTO `quiz_pista` VALUES (65,22,'En la arena no siembran sus cultivos.',5,'65');
INSERT INTO `quiz_pista` VALUES (66,22,'En la composta no siembran sus cultivos.',7,'66');
INSERT INTO `quiz_pista` VALUES (67,23,'En el coamil no se siembran nopales y hongos.',3,'67');
INSERT INTO `quiz_pista` VALUES (68,23,'En el coamil no se siembran flores de colores.',5,'68');
INSERT INTO `quiz_pista` VALUES (69,23,'En el coamil no se siembran árboles y arbustos.',7,'69');
INSERT INTO `quiz_pista` VALUES (70,24,'<i>Múme</i> significa frijol.',3,'70');
INSERT INTO `quiz_pista` VALUES (71,24,'<i>Ikú yɨwi</i> significa maíz negro.',5,'71');
INSERT INTO `quiz_pista` VALUES (72,24,'<i>Ikɨri</i> significa elote',7,'72');
INSERT INTO `quiz_pista` VALUES (73,25,'El maíz morado no se utiliza como alimento para los animales.',3,'73');
INSERT INTO `quiz_pista` VALUES (74,25,'El maíz de colores no se utiliza como alimento para los animales.',5,'74');
INSERT INTO `quiz_pista` VALUES (75,25,'El maíz negro no se utiliza como alimento para los animales.',7,'75');
INSERT INTO `quiz_pista` VALUES (76,26,'<i>Ikú</i> significa maíz.',3,'76');
INSERT INTO `quiz_pista` VALUES (77,26,'<i>Múme</i> significa frijol.',5,'77');
INSERT INTO `quiz_pista` VALUES (78,26,'<i>Ikɨri</i> significa elote.',7,'78');
INSERT INTO `quiz_pista` VALUES (79,27,'<i>Múmete</i> significa frijoles.',3,'79');
INSERT INTO `quiz_pista` VALUES (80,27,'<i>Ikɨríte</i> significa elotes.',5,'80');
INSERT INTO `quiz_pista` VALUES (81,27,'<i>Iku’ute</i> significa maíces.',7,'81');
INSERT INTO `quiz_pista` VALUES (82,28,'Frijoles no significa <i>kukúri</i>.',3,'82');
INSERT INTO `quiz_pista` VALUES (83,28,'Maíz no significa <i>kukúri</i>.',5,'83');
INSERT INTO `quiz_pista` VALUES (84,28,'Elote no significa <i>kukúri</i>.',7,'84');
INSERT INTO `quiz_pista` VALUES (85,29,'Pipián de <i>tuixuyeutanaka</i> significa pipián de jabalí.',3,'85');
INSERT INTO `quiz_pista` VALUES (86,29,'<i>Tuixuyeutanaka</i> itsari significa caldo de jabalí.',5,'86');
INSERT INTO `quiz_pista` VALUES (87,29,'<i>Tuixuyeutanaka ikwaiyári</i> significa mole de jabalí.',7,'87');
INSERT INTO `quiz_pista` VALUES (88,30,'<i>Xupaxi</i> significa sopa.',3,'88');
INSERT INTO `quiz_pista` VALUES (89,30,'<i>Tsuirá</i> significa gordita.',5,'89');
INSERT INTO `quiz_pista` VALUES (90,30,'<i>Pexuri</i> significa mole.',7,'90');
INSERT INTO `quiz_pista` VALUES (91,31,'<i>Tétsute</i> significa tamales.',3,'91');
INSERT INTO `quiz_pista` VALUES (92,31,'<i>Xupaxite</i> significa chilaquiles.',5,'92');
INSERT INTO `quiz_pista` VALUES (93,31,'<i>Tsuiráte</i> significa gorditas.',7,'93');
INSERT INTO `quiz_pista` VALUES (94,32,'<i>Imukwi</i> significa salamandra.',3,'94');
INSERT INTO `quiz_pista` VALUES (95,32,'<i>Ke’etsé</i> significa iguana.',5,'95');
INSERT INTO `quiz_pista` VALUES (96,32,'<i>Ɨkwi</i> significa lagartija.',7,'96');
INSERT INTO `quiz_pista` VALUES (97,33,'<i>Orekanu</i> significa orégano.',3,'97');
INSERT INTO `quiz_pista` VALUES (98,33,'<i>Mantsaniya</i> significa manzanilla.',5,'98');
INSERT INTO `quiz_pista` VALUES (99,33,'<i>Eɨkariti</i> significa gordolobo.',7,'99');
INSERT INTO `quiz_pista` VALUES (100,34,'<i>Kɨyé</i> significa palo.',3,'100');
INSERT INTO `quiz_pista` VALUES (101,34,'<i>Tɨɨpi</i> significa arco.',5,'101');
INSERT INTO `quiz_pista` VALUES (102,34,'<i>Ha’tsa</i> significa hacha.',7,'102');
INSERT INTO `quiz_pista` VALUES (103,35,'Del Nayar no es un sitio sagrado.',3,'103');
INSERT INTO `quiz_pista` VALUES (104,35,'Las iglesias no son un sitio sagrado.',5,'104');
INSERT INTO `quiz_pista` VALUES (105,35,'La Yesca no es un sitio sagrado.',7,'105');
INSERT INTO `quiz_pista` VALUES (106,36,'<i>Watame</i> significa cuamilero.',3,'106');
INSERT INTO `quiz_pista` VALUES (107,36,'<i>Muka’etsa</i> significa agricultor.',5,'107');
INSERT INTO `quiz_pista` VALUES (108,36,'<i>Itsɨkame</i> significa autoridad.',7,'108');
INSERT INTO `quiz_pista` VALUES (109,37,'Los <i>wixaritari</i> no cazan para obtener pieles.',3,'109');
INSERT INTO `quiz_pista` VALUES (110,37,'Los <i>wixaritari</i> cazan para alimentarse, pero no pricipalmente.',5,'110');
INSERT INTO `quiz_pista` VALUES (111,37,'Los <i>wixaritari</i> no cazan para el comercio.',7,'111');
INSERT INTO `quiz_pista` VALUES (112,38,'Los lazos del corazón no son el <i>nana’iyari</i>.',3,'112');
INSERT INTO `quiz_pista` VALUES (113,38,'El camino Wixárika no es el <i>nana’iyari</i>.',5,'113');
INSERT INTO `quiz_pista` VALUES (114,38,'El corazón del pueblo no es el <i>nana’iyari</i>.',7,'114');
INSERT INTO `quiz_pista` VALUES (115,39,'No se vive y reproduce por las deidades.',3,'115');
INSERT INTO `quiz_pista` VALUES (116,39,'No se vive y reproduce por las tradiciones',5,'116');
INSERT INTO `quiz_pista` VALUES (117,39,'No se vive y reproduce a través del corazón.',7,'117');
INSERT INTO `quiz_pista` VALUES (118,40,'<i>Kwarɨpa</i> significa ciruela.',3,'118');
INSERT INTO `quiz_pista` VALUES (119,40,'<i>Yɨɨna</i> significa tuna.',5,'119');
INSERT INTO `quiz_pista` VALUES (120,40,'<i>Muxu’uri</i> significa guamúchil.',7,'120');
INSERT INTO `quiz_pista` VALUES (121,41,'Guamúchil no significa <i>uwá</i>.',3,'121');
INSERT INTO `quiz_pista` VALUES (122,41,'Ciruela no significa <i>uwá</i>.',5,'122');
INSERT INTO `quiz_pista` VALUES (123,41,'Tuna no significa <i>uwá</i>.',7,'123');
INSERT INTO `quiz_pista` VALUES (124,42,'<i>Pexúri</i> significa pinole.',3,'124');
INSERT INTO `quiz_pista` VALUES (125,42,'<i>Ruritse</i> significa dulce.',5,'125');
INSERT INTO `quiz_pista` VALUES (126,42,'<i>Tsakaka</i> significa piloncillo.',7,'126');
INSERT INTO `quiz_pista` VALUES (127,43,'Dulce no significa <i>xiete</i>.',3,'127');
INSERT INTO `quiz_pista` VALUES (128,43,'Piloncillo no significa <i>xiete</i>.',5,'128');
INSERT INTO `quiz_pista` VALUES (129,43,'Pinole no significa <i>xiete</i>.',7,'129');
INSERT INTO `quiz_pista` VALUES (130,44,'<i>Ruritse</i> significa dulce.',3,'130');
INSERT INTO `quiz_pista` VALUES (131,44,'<i>Pexúri</i> significa pinole.',5,'131');
INSERT INTO `quiz_pista` VALUES (132,44,'<i>Xiete</i> significa miel.',7,'132');
INSERT INTO `quiz_pista` VALUES (133,45,'<i>Ketsɨ itsari</i> significa caldo de pescado.',3,'133');
INSERT INTO `quiz_pista` VALUES (134,45,'<i>Ketsɨ wiyamari</i> significa chicharrón de pescado.',5,'134');
INSERT INTO `quiz_pista` VALUES (135,45,'<i>Ketsɨ</i> significa pescado.',7,'135');
INSERT INTO `quiz_pista` VALUES (136,46,'<i>Kwixɨ</i> significa águila.',3,'136');
INSERT INTO `quiz_pista` VALUES (137,46,'<i>Ke’etsé</i> significa iguana.',5,'137');
INSERT INTO `quiz_pista` VALUES (138,46,'<i>Ha’axi</i> significa cocodrilo.',7,'138');
INSERT INTO `quiz_pista` VALUES (139,47,'<i>Untsa</i> significa lince.',3,'139');
INSERT INTO `quiz_pista` VALUES (140,47,'<i>Kauxai</i> significa zorro.',5,'140');
INSERT INTO `quiz_pista` VALUES (141,47,'<i>Xiete</i> significa abeja.',7,'141');
INSERT INTO `quiz_pista` VALUES (142,48,'<i>Kauxai</i> significa zorro.',3,'142');
INSERT INTO `quiz_pista` VALUES (143,48,'<i>Untsa</i> significa lince.',5,'143');
INSERT INTO `quiz_pista` VALUES (144,48,'<i>Yáavi</i> significa coyote.',7,'144');
INSERT INTO `quiz_pista` VALUES (145,49,'<i>Xiete</i> significa abeja.',3,'145');
INSERT INTO `quiz_pista` VALUES (146,49,'<i>Untsa</i> significa lince.',5,'146');
INSERT INTO `quiz_pista` VALUES (147,49,'<i>Yáavi</i> significa coyote.',7,'147');
INSERT INTO `quiz_pista` VALUES (148,50,'<i>Nawaxa</i> significa cuchillo.',3,'148');
INSERT INTO `quiz_pista` VALUES (149,50,'<i>Kutsira</i> significa machete.',5,'149');
INSERT INTO `quiz_pista` VALUES (150,50,'<i>Kɨyé</i> significa palo.',7,'150');
INSERT INTO `quiz_pista` VALUES (151,51,'La Yesca no es un sitio sagrado.',3,'151');
INSERT INTO `quiz_pista` VALUES (152,51,'Del Nayar no es un sitio sagrado.',5,'152');
INSERT INTO `quiz_pista` VALUES (153,51,'Las minas de Santa María del Oro no son un sitio sagrado.',7,'153');
INSERT INTO `quiz_pista` VALUES (154,52,'<i>Tiyuitɨwame</i> significa músico.',3,'154');
INSERT INTO `quiz_pista` VALUES (155,52,'<i>Muka’etsa</i> significa agricultor.',5,'155');
INSERT INTO `quiz_pista` VALUES (156,52,'<i>Ketsɨtame</i> significa pescador.',7,'156');
INSERT INTO `quiz_pista` VALUES (157,53,'<i>Xaweruxite</i> significa pantalones.',3,'157');
INSERT INTO `quiz_pista` VALUES (158,53,'<i>Xapatuxite</i> significa zapatos.',5,'158');
INSERT INTO `quiz_pista` VALUES (159,53,'<i>Kamixate</i> significa camisas.',7,'159');
INSERT INTO `quiz_pista` VALUES (160,54,'Un <i>kemari</i> (traje tradicional <i>Wixárika</i>) no es de telas de colores.',3,'160');
INSERT INTO `quiz_pista` VALUES (161,54,'Un <i>kemari</i> (traje tradicional <i>Wixárika</i>) no es de manta blanca y listones.',5,'161');
INSERT INTO `quiz_pista` VALUES (162,54,'Un <i>kemari</i> (traje tradicional <i>Wixárika</i>) no es de algodón y estambre.',7,'162');
INSERT INTO `quiz_pista` VALUES (163,55,'<i>Tatei Haramara</i> es Nuestra madre el mar',3,'163');
INSERT INTO `quiz_pista` VALUES (164,55,'<i>Tatei Yurienáka</i> es Nuestra madre tierra.',5,'164');
INSERT INTO `quiz_pista` VALUES (165,55,'<i>Tatei Wexica Wimari</i> es Nuestra madre águila.',7,'165');
INSERT INTO `quiz_pista` VALUES (166,56,'No se convierte en espuma de mar',3,'166');
INSERT INTO `quiz_pista` VALUES (167,56,'No se convierte en lluvia y viento',5,'167');
INSERT INTO `quiz_pista` VALUES (168,56,'No se convierte en cactus',7,'168');
INSERT INTO `quiz_pista` VALUES (169,57,'No los guio a San Blas.',3,'169');
INSERT INTO `quiz_pista` VALUES (170,57,'No los guio a La Yesca.',5,'170');
INSERT INTO `quiz_pista` VALUES (171,57,'No los guio a Mesa del Nayar.',7,'171');
INSERT INTO `quiz_pista` VALUES (172,58,'<i>Muxu’uri</i> significa guamúchil.',3,'172');
INSERT INTO `quiz_pista` VALUES (173,58,'<i>Ka’aru</i> significa plátano.',5,'173');
INSERT INTO `quiz_pista` VALUES (174,58,'<i>Uwá</i> significa caña.',7,'174');
INSERT INTO `quiz_pista` VALUES (175,59,'Plátano no significa <i>xa’ata</i>.',3,'175');
INSERT INTO `quiz_pista` VALUES (176,59,'Caña no significa <i>xa’ata</i>.',5,'176');
INSERT INTO `quiz_pista` VALUES (177,59,'Guamúchil no significa <i>xa’ata</i>.',7,'177');
INSERT INTO `quiz_pista` VALUES (178,60,'<i>Tsikwai</i> significa arrayan.',3,'178');
INSERT INTO `quiz_pista` VALUES (179,60,'<i>Uwakí</i> significa nanchi.',5,'179');
INSERT INTO `quiz_pista` VALUES (180,60,'<i>Yɨɨna</i> significa tuna.',7,'180');
INSERT INTO `quiz_pista` VALUES (181,61,'<i>Tsikwai</i> no significa kwarɨpa.',3,'181');
INSERT INTO `quiz_pista` VALUES (182,61,'<i>Uwakí</i> no significa kwarɨpa.',5,'182');
INSERT INTO `quiz_pista` VALUES (183,61,'<i>Yɨɨna</i> no significa kwarɨpa.',7,'183');
INSERT INTO `quiz_pista` VALUES (184,62,'Pipián de <i>maxa</i> no significa mole de venado.',3,'184');
INSERT INTO `quiz_pista` VALUES (185,62,'<i>Maxa itsari</i> no significa mole de venado.',5,'185');
INSERT INTO `quiz_pista` VALUES (186,62,'<i>Maxa</i> no significa mole de venado.',7,'186');
INSERT INTO `quiz_pista` VALUES (187,63,'<i>Yervawena</i> significa hierbabuena.',3,'187');
INSERT INTO `quiz_pista` VALUES (188,63,'<i>Wáxa</i> significa milpa.',5,'188');
INSERT INTO `quiz_pista` VALUES (189,63,'<i>Orekanu</i> significa orégano.',7,'189');
INSERT INTO `quiz_pista` VALUES (190,64,'Cerro de San Juan no es un sitio sagrado.',3,'190');
INSERT INTO `quiz_pista` VALUES (191,64,'Presa de aguamilpa no es un sitio sagrado.',5,'191');
INSERT INTO `quiz_pista` VALUES (192,64,'Tepic no es un sitio sagrado.',7,'192');
INSERT INTO `quiz_pista` VALUES (193,65,'<i>Tituayame</i> significa vendedor.',3,'193');
INSERT INTO `quiz_pista` VALUES (194,65,'<i>Ikwai wewiwame</i> significa cocinero.',5,'194');
INSERT INTO `quiz_pista` VALUES (195,65,'<i>Watame</i> significa cuamilero.',7,'195');
INSERT INTO `quiz_pista` VALUES (196,66,'<i>Xaweri</i> significa música tradicional.',3,'196');
INSERT INTO `quiz_pista` VALUES (197,66,'<i>Kwikariyari</i> significa música regional.',5,'197');
INSERT INTO `quiz_pista` VALUES (198,66,'<i>Tunuiya</i> significa canto sagrado.',7,'198');
INSERT INTO `quiz_pista` VALUES (199,67,'Los sones, baladas y mariachi no son música tradicional <i>Wixárika</i>.',3,'199');
INSERT INTO `quiz_pista` VALUES (200,67,'Los cantos espirituales y cantos sagrados no son música tradicional <i>Wixárika</i>.',5,'200');
INSERT INTO `quiz_pista` VALUES (201,67,'La música norteña y música ranchera no son música tradicional <i>Wixárika</i>.',7,'201');
INSERT INTO `quiz_pista` VALUES (202,68,'No recrean historias sobre el universo',3,'202');
INSERT INTO `quiz_pista` VALUES (203,68,'No recrean oraciones',5,'203');
INSERT INTO `quiz_pista` VALUES (204,68,'No recrean canciones',7,'204');
INSERT INTO `quiz_pista` VALUES (205,69,'<i>Tsikwai</i> significa arrayan.',3,'205');
INSERT INTO `quiz_pista` VALUES (206,69,'<i>Ha’yewaxi</i> significa guayaba.',5,'206');
INSERT INTO `quiz_pista` VALUES (207,69,'<i>Kwarɨpa</i> significa ciruela.',7,'207');
INSERT INTO `quiz_pista` VALUES (208,70,'Guayaba no significa <i>ma’aku</i>.',3,'208');
INSERT INTO `quiz_pista` VALUES (209,70,'Ciruela no significa <i>ma’aku</i>.',5,'209');
INSERT INTO `quiz_pista` VALUES (210,70,'Arrayan no significa <i>ma’aku</i>.',7,'210');
INSERT INTO `quiz_pista` VALUES (211,71,'<i>Ma’aku</i> significa mango.',3,'211');
INSERT INTO `quiz_pista` VALUES (212,71,'<i>Kwarɨpa</i> significa ciruela.',5,'212');
INSERT INTO `quiz_pista` VALUES (213,71,'<i>Ha’yewaxi</i> significa guayaba.',7,'213');
INSERT INTO `quiz_pista` VALUES (214,72,'Guayaba no significa <i>tsikwai</i>.',3,'214');
INSERT INTO `quiz_pista` VALUES (215,72,'Mango no significa <i>tsikwai</i>.',5,'215');
INSERT INTO `quiz_pista` VALUES (216,72,'Ciruela no significa <i>tsikwai</i>.',7,'216');
INSERT INTO `quiz_pista` VALUES (217,73,'<i>Tátsiu itsari</i> significa caldo de conejo.',7,'217');
INSERT INTO `quiz_pista` VALUES (218,73,'<i>Weurai itsari</i> significa caldo de güilota.',5,'218');
INSERT INTO `quiz_pista` VALUES (219,73,'<i>Tuixuyeutanaka itsari</i> significa caldo de jabalí.',3,'219');
INSERT INTO `quiz_pista` VALUES (220,74,'<i>Xɨye</i> significa armadillo.',7,'220');
INSERT INTO `quiz_pista` VALUES (221,74,'<i>Mitsu</i> significa gato.',5,'221');
INSERT INTO `quiz_pista` VALUES (222,74,'<i>Tátsiu</i> significa conejo.',3,'222');
INSERT INTO `quiz_pista` VALUES (223,75,'<i>Tuuká</i> significa araña.',7,'223');
INSERT INTO `quiz_pista` VALUES (224,75,'<i>Xiete</i> significa abeja.',5,'224');
INSERT INTO `quiz_pista` VALUES (225,75,'<i>Curupo</i> significa caracol.',3,'225');
INSERT INTO `quiz_pista` VALUES (226,76,'Tepic no es un sitio sagrado.',7,'226');
INSERT INTO `quiz_pista` VALUES (227,76,'El cerro de San Juan no es un sitio sagrado.',5,'227');
INSERT INTO `quiz_pista` VALUES (228,76,'Santiago Ixcuintla no es un sitio sagrado.',3,'228');
INSERT INTO `quiz_pista` VALUES (229,77,'La presa de aguamilpa no es un sitio sagrado.',7,'229');
INSERT INTO `quiz_pista` VALUES (230,77,'Santiago Ixcuintla no es un sitio sagrado.',5,'230');
INSERT INTO `quiz_pista` VALUES (231,77,'Tepic no es un sitio sagrado.',3,'231');
INSERT INTO `quiz_pista` VALUES (232,78,'<i>Kwatsa</i> significa cuervo.',7,'232');
INSERT INTO `quiz_pista` VALUES (233,78,'<i>Werika</i> significa águila real.',5,'233');
INSERT INTO `quiz_pista` VALUES (234,78,'<i>Witse</i> significa halcón.',3,'234');
INSERT INTO `quiz_pista` VALUES (235,79,'El cuervo no es un animal mensajero.',7,'235');
INSERT INTO `quiz_pista` VALUES (236,79,'El águila no es un animal mensajero.',5,'236');
INSERT INTO `quiz_pista` VALUES (237,79,'El halcón no es un animal mensajero.',3,'237');
INSERT INTO `quiz_pista` VALUES (238,80,'En los ríos no aparecen los animales mensajeros.',7,'238');
INSERT INTO `quiz_pista` VALUES (239,80,'En los sitios sagrados no aparecen los animales mensajeros.',5,'239');
INSERT INTO `quiz_pista` VALUES (240,80,'En las escuelas no aparecen los animales mensajeros.',3,'240');
INSERT INTO `quiz_pista` VALUES (241,81,'<i>Kwikari</i> significa música.',7,'241');
INSERT INTO `quiz_pista` VALUES (242,81,'<i>Ixɨarari</i> significa festividad.',5,'242');
INSERT INTO `quiz_pista` VALUES (243,81,'<i>Kakaɨyarita</i> significa sitio sagrado.',3,'243');
INSERT INTO `quiz_pista` VALUES (244,82,'Las principales neixa no celebran las tormentas.',7,'244');
INSERT INTO `quiz_pista` VALUES (245,82,'Las principales <i>neixa no celebran el inicio de año.',5,'245');
INSERT INTO `quiz_pista` VALUES (246,82,'Las principales <i>neixa</i> no celebran el nacimiento de las deidades.',3,'246');
INSERT INTO `quiz_pista` VALUES (247,83,'Las fiestas no se realizan en el <i>tiwatuiya</i>.',7,'247');
INSERT INTO `quiz_pista` VALUES (248,83,'Las fiestas no se realizan en el <i>uyé</i>.',5,'248');
INSERT INTO `quiz_pista` VALUES (249,83,'Las fiestas no se realizan en el <i>hɨri</i>.',3,'249');
INSERT INTO `quiz_pista` VALUES (250,84,'<i>Kií</i> significa casa.',7,'250');
INSERT INTO `quiz_pista` VALUES (251,84,'<i>Xiriki</i> no significa centro ceremonial.',5,'251');
INSERT INTO `quiz_pista` VALUES (252,84,'<i>Tuki</i> no significa centro ceremonial.',3,'252');
INSERT INTO `quiz_pista` VALUES (253,85,'El <i>tukipa</i> no es una iglesia.',7,'253');
INSERT INTO `quiz_pista` VALUES (254,85,'El <i>tukipa</i> no es un espacio para la meditación.',5,'254');
INSERT INTO `quiz_pista` VALUES (255,85,'El <i>tukipa</i> no es una escuela.',3,'255');
INSERT INTO `quiz_pista` VALUES (256,86,'<i>Hɨri y watsíya</i> no integran el tukipa.',7,'256');
INSERT INTO `quiz_pista` VALUES (257,86,'<i>Te’erɨ y ke’ekari</i> no integran el tukipa.',5,'257');
INSERT INTO `quiz_pista` VALUES (258,86,'<i>Haramara y <i>haita</i> no integran el <i>tukipa</i>.',3,'258');
INSERT INTO `quiz_pista` VALUES (259,87,'<i>Ka’arú</i> significa plátano.',7,'259');
INSERT INTO `quiz_pista` VALUES (260,87,'<i>Kwarɨpa</i> significa ciruela.',5,'260');
INSERT INTO `quiz_pista` VALUES (261,87,'<i>Tsikwai</i> significa arrayan.',3,'261');
INSERT INTO `quiz_pista` VALUES (262,88,'Ciruela no es <i>uwakí</i>.',7,'262');
INSERT INTO `quiz_pista` VALUES (263,88,'Arrayan no es <i>uwakí</i>.',5,'263');
INSERT INTO `quiz_pista` VALUES (264,88,'Plátano no es <i>uwakí</i>.',3,'264');
INSERT INTO `quiz_pista` VALUES (265,89,'<i>Uwakí</i> significa nanchi.',7,'265');
INSERT INTO `quiz_pista` VALUES (266,89,'<i>Tsikwai</i> significa arrayan.',5,'266');
INSERT INTO `quiz_pista` VALUES (267,89,'<i>Kwarɨpa</i> significa ciruela.',3,'267');
INSERT INTO `quiz_pista` VALUES (268,90,'Nanchi no es <i>ka’arú</i>.',7,'268');
INSERT INTO `quiz_pista` VALUES (269,90,'Ciruela no es <i>ka’arú</i>.',5,'269');
INSERT INTO `quiz_pista` VALUES (270,90,'Arrayan no es <i>ka’arú</i>.',3,'270');
INSERT INTO `quiz_pista` VALUES (271,91,'Pipián de <i>tuixu</i> significa pipián de cerdo.',7,'271');
INSERT INTO `quiz_pista` VALUES (272,91,'Pipián de <i>tátsiu</i> significa pipián de conejo.',5,'272');
INSERT INTO `quiz_pista` VALUES (273,91,'Pipián de <i>weurai</i> significa pipián de güilota.',3,'273');
INSERT INTO `quiz_pista` VALUES (274,92,'<i>Ruritse</i> significa dulce.',7,'274');
INSERT INTO `quiz_pista` VALUES (275,92,'<i>Pexúri</i> significa pinole.',5,'275');
INSERT INTO `quiz_pista` VALUES (276,92,'<i>Paní</i> significa pan.',3,'276');
INSERT INTO `quiz_pista` VALUES (277,93,'<i>Wakana</i> significa pollo.',7,'277');
INSERT INTO `quiz_pista` VALUES (278,93,'<i>Tuixuyeutanaka</i> significa jabalí.',5,'278');
INSERT INTO `quiz_pista` VALUES (279,93,'<i>Tuixu</i> significa cerdo.',3,'279');
INSERT INTO `quiz_pista` VALUES (280,94,'<i>Ɨkwi</i> significa lagartija.',7,'280');
INSERT INTO `quiz_pista` VALUES (281,94,'<i>Ke’etsé</i> significa iguana.',5,'281');
INSERT INTO `quiz_pista` VALUES (282,94,'<i>Téka</i> significa camaleón cornudo.',3,'282');
INSERT INTO `quiz_pista` VALUES (283,95,'Un <i>tsikɨri</i> no es un sitio sagrado.',7,'283');
INSERT INTO `quiz_pista` VALUES (284,95,'Un <i>tsikɨri</i> no es una comida tradicional.',5,'284');
INSERT INTO `quiz_pista` VALUES (285,95,'Un <i>tsikɨri</i> no es un adorno.',3,'285');
INSERT INTO `quiz_pista` VALUES (286,96,'Un <i>tsikɨri</i> no simboliza un centro ceremonial.',7,'286');
INSERT INTO `quiz_pista` VALUES (287,96,'Un <i>tsikɨri</i> no simboliza la cosmovisión Wixárika.',5,'287');
INSERT INTO `quiz_pista` VALUES (288,96,'Un <i>tsikɨri</i> no simboliza un portal al inframundo.',3,'288');
INSERT INTO `quiz_pista` VALUES (289,97,'El Puerto de San Blas no es un sitio sagrado.',7,'289');
INSERT INTO `quiz_pista` VALUES (290,97,'Santiago Ixcuintla no es un sitio sagrado.',5,'290');
INSERT INTO `quiz_pista` VALUES (291,97,'Tepic no es un sitio sagrado.',3,'291');
INSERT INTO `quiz_pista` VALUES (292,98,'<i>Haramara</i> no está en La Yesca.',7,'292');
INSERT INTO `quiz_pista` VALUES (293,98,'<i>Haramara</i> no está en Tepic.',5,'293');
INSERT INTO `quiz_pista` VALUES (294,98,'<i>Haramara</i> no está en Tuxpan.',3,'294');
INSERT INTO `quiz_pista` VALUES (295,99,'<i>Tatewarí</i> es Nuestro abuelo fuego.',7,'295');
INSERT INTO `quiz_pista` VALUES (296,99,'<i>Tatei Yurienáka</i> es Nuestra madre tierra.',5,'296');
INSERT INTO `quiz_pista` VALUES (297,99,'<i>Tatei Wexica Wimari</i> es Nuestra madre águila.',3,'297');
INSERT INTO `quiz_pista` VALUES (298,100,'<i>Tatei Haramara</i> no da origen a las rocas.',7,'298');
INSERT INTO `quiz_pista` VALUES (299,100,'<i>Tatei Haramara</i> no da origen a la espuma de mar.',5,'299');
INSERT INTO `quiz_pista` VALUES (300,100,'<i>Tatei Haramara</i> no da origen a los cerros.',3,'300');
INSERT INTO `quiz_pista` VALUES (301,101,'<i>Xátsika</i> significa leyenda.',7,'301');
INSERT INTO `quiz_pista` VALUES (302,101,'<i>Kwikari</i> significa música.',5,'302');
INSERT INTO `quiz_pista` VALUES (303,101,'<i>Kemari</i> significa traje.',3,'303');
INSERT INTO `quiz_pista` VALUES (304,102,'La comida para los muertos no es una ofrenda.',7,'304');
INSERT INTO `quiz_pista` VALUES (305,102,'Un regalo para tus amigos no es una ofrenda.',5,'305');
INSERT INTO `quiz_pista` VALUES (306,102,'Un adorno para las casas no es una ofrenda.',3,'306');
INSERT INTO `quiz_pista` VALUES (307,103,'No son para regalarlas a tus amigos.',7,'307');
INSERT INTO `quiz_pista` VALUES (308,103,'No son para decorar las casas.',5,'308');
INSERT INTO `quiz_pista` VALUES (309,103,'No son para enviar mensajes a otros <i>wixaritari</i>.',3,'309');
INSERT INTO `quiz_pista` VALUES (310,104,'<i>Ikɨri</i> significa elote.',7,'310');
INSERT INTO `quiz_pista` VALUES (311,104,'<i>Ikú taxawime</i> significa maíz amarillo.',5,'311');
INSERT INTO `quiz_pista` VALUES (312,104,'<i>Ikú</i> significa maíz.',3,'312');
INSERT INTO `quiz_pista` VALUES (313,105,'El maíz blanco no se utiliza para preparar tortillas y dar color a los alimentos.',7,'313');
INSERT INTO `quiz_pista` VALUES (314,105,'El maíz amarillo no se utiliza para preparar tortillas y dar color a los alimentos.',5,'314');
INSERT INTO `quiz_pista` VALUES (315,105,'El maíz rojo no se utiliza para preparar tortillas y dar color a los alimentos.',3,'315');
INSERT INTO `quiz_pista` VALUES (316,106,'<i>Ikú taxawime</i> significa maíz amarillo.',7,'316');
INSERT INTO `quiz_pista` VALUES (317,106,'<i>Ikú</i> significa maíz.',5,'317');
INSERT INTO `quiz_pista` VALUES (318,106,'<i>Ikɨri</i> significa elote.',3,'318');
INSERT INTO `quiz_pista` VALUES (319,107,'<i>Hayaári</i> significa jugo.',7,'319');
INSERT INTO `quiz_pista` VALUES (320,107,'<i>Retsi</i> significa leche.',5,'320');
INSERT INTO `quiz_pista` VALUES (321,107,'<i>Hamuitsi</i> significa atole.',3,'321');
INSERT INTO `quiz_pista` VALUES (322,108,'<i>Ruritse</i> significa dulce.',7,'322');
INSERT INTO `quiz_pista` VALUES (323,108,'<i>Paní</i> significa pan.',5,'323');
INSERT INTO `quiz_pista` VALUES (324,108,'<i>Ka’arú wiyamatɨ</i> significa plátano frito.',3,'324');
INSERT INTO `quiz_pista` VALUES (325,109,'<i>Kɨyé</i> significa palo.',7,'325');
INSERT INTO `quiz_pista` VALUES (326,109,'<i>Kutsira</i> significa machete.',5,'326');
INSERT INTO `quiz_pista` VALUES (327,109,'<i>Ha’tsa</i> significa hacha.',3,'327');
INSERT INTO `quiz_pista` VALUES (328,110,'Valparaíso no es un sitio sagrado.',7,'328');
INSERT INTO `quiz_pista` VALUES (329,110,'San Blas no es un sitio sagrado.',5,'329');
INSERT INTO `quiz_pista` VALUES (330,110,'Tepic no es un sitio sagrado.',3,'330');
INSERT INTO `quiz_pista` VALUES (331,111,'San Blas no es un sitio sagrado.',7,'331');
INSERT INTO `quiz_pista` VALUES (332,111,'Valparaíso no es un sitio sagrado.',5,'332');
INSERT INTO `quiz_pista` VALUES (333,111,'Tepic no es un sitio sagrado.',3,'333');
INSERT INTO `quiz_pista` VALUES (334,112,'<i>Tatewarí</i> es Nuestro abuelo fuego.',7,'334');
INSERT INTO `quiz_pista` VALUES (335,112,'<i>Tamatzi Kauyumárie</i> es Nuestro hermano mayor Venado Azul',5,'335');
INSERT INTO `quiz_pista` VALUES (336,112,'<i>Tatei Haramara</i> es Nuestra madre el mar',3,'336');
INSERT INTO `quiz_pista` VALUES (337,113,'<i>Tupiri</i> significa policía.',7,'337');
INSERT INTO `quiz_pista` VALUES (338,113,'<i>Mara’kame</i> significa chamán.',5,'338');
INSERT INTO `quiz_pista` VALUES (339,113,'<i>Tatuwani</i> significa gobernador.',3,'339');
INSERT INTO `quiz_pista` VALUES (340,114,'Un <i>mara’kame</i> no cuida el tukipa (centro ceremonial).',7,'340');
INSERT INTO `quiz_pista` VALUES (341,114,'Un <i>kawiteru</i> no cuida el tukipa (centro ceremonial).',5,'341');
INSERT INTO `quiz_pista` VALUES (342,114,'Un <i>hikuritame</i> no cuida el tukipa (centro ceremonial).',3,'342');
INSERT INTO `quiz_pista` VALUES (343,115,'<i>Hapani</i> significa planta para huesos rotos.',7,'343');
INSERT INTO `quiz_pista` VALUES (344,115,'<i>Wáxa</i> significa milpa.',5,'344');
INSERT INTO `quiz_pista` VALUES (345,115,'<i>Uxa</i> significa planta para pintar el cabello.',3,'345');
INSERT INTO `quiz_pista` VALUES (346,116,'El sol y la luna no dieron origen al peyote.',7,'346');
INSERT INTO `quiz_pista` VALUES (347,116,'La espuma de mar de <i>Tatei Haramara</i> no dieron origen al peyote.',5,'347');
INSERT INTO `quiz_pista` VALUES (348,116,'La tierra y la lluvia no dieron origen al peyote.',3,'348');
INSERT INTO `quiz_pista` VALUES (349,117,'La leyenda del Venado Azul no se recrea durante las peregrinaciones.',7,'349');
INSERT INTO `quiz_pista` VALUES (350,117,'Las historias sobre el fuego no se recrean durante las peregrinaciones.',5,'350');
INSERT INTO `quiz_pista` VALUES (351,117,'La leyenda de <i>Watakame</i> no se recrea durante las peregrinaciones.',3,'351');
INSERT INTO `quiz_pista` VALUES (352,118,'Las casas no se visitan durante las peregrinaciones.',7,'352');
INSERT INTO `quiz_pista` VALUES (353,118,'Las escuelas no se visitan durante las peregrinaciones.',5,'353');
INSERT INTO `quiz_pista` VALUES (354,118,'Los <i>tukipa</i> no se visitan durante las peregrinaciones.',3,'354');
INSERT INTO `quiz_pista` VALUES (355,119,'A <i>Haramara</i> no se realiza la peregrinación más importante.',7,'355');
INSERT INTO `quiz_pista` VALUES (356,119,'A <i>Hauxa Manaka</i> no se realiza la peregrinación más importante.',5,'356');
INSERT INTO `quiz_pista` VALUES (357,119,'A <i>Te’ekata</i> no se realiza la peregrinación más importante.',3,'357');
INSERT INTO `quiz_pista` VALUES (358,120,'<i>Kukúri</i> significa chile.',7,'358');
INSERT INTO `quiz_pista` VALUES (359,120,'<i>Tsinakari</i> significa limón.',5,'359');
INSERT INTO `quiz_pista` VALUES (360,120,'<i>Xa’ata</i> significa jícama.',3,'360');
INSERT INTO `quiz_pista` VALUES (361,121,'Limón no significa <i>túmati</i>.',7,'361');
INSERT INTO `quiz_pista` VALUES (362,121,'Chile no significa <i>túmati</i>.',5,'362');
INSERT INTO `quiz_pista` VALUES (363,121,'Jícama no significa <i>túmati</i>.',3,'363');
INSERT INTO `quiz_pista` VALUES (364,122,'<i>Tátsiu itsari</i> significa caldo de conejo.',7,'364');
INSERT INTO `quiz_pista` VALUES (365,122,'<i>Ketsɨ itsari</i> significa caldo de pescado.',5,'365');
INSERT INTO `quiz_pista` VALUES (366,122,'<i>Wakana itsari</i> significa caldo de pollo.',3,'366');
INSERT INTO `quiz_pista` VALUES (367,123,'<i>Kwixɨ</i> significa águila.',7,'367');
INSERT INTO `quiz_pista` VALUES (368,123,'<i>Wakana</i> significa pollo.',5,'368');
INSERT INTO `quiz_pista` VALUES (369,123,'<i>Peexá</i> significa tecolote.',3,'369');
INSERT INTO `quiz_pista` VALUES (370,124,'<i>Mantsaniya</i> significa manzanilla.',7,'370');
INSERT INTO `quiz_pista` VALUES (371,124,'<i>Eɨkariti</i> significa eucalipto.',5,'371');
INSERT INTO `quiz_pista` VALUES (372,124,'<i>Yervawena</i> significa hierbabuena.',3,'372');
INSERT INTO `quiz_pista` VALUES (373,125,'Valparaíso no es un sitio sagrado.',7,'373');
INSERT INTO `quiz_pista` VALUES (374,125,'San Blas no es un sitio sagrado.',5,'374');
INSERT INTO `quiz_pista` VALUES (375,125,'Fresnillo no es un sitio sagrado.',3,'375');
INSERT INTO `quiz_pista` VALUES (376,126,'<i>Teruka significa alacrán.',7,'376');
INSERT INTO `quiz_pista` VALUES (377,126,'<i>Téka</i> significa camaleón cornudo.',5,'377');
INSERT INTO `quiz_pista` VALUES (378,126,'<i>Ke’etsé</i> significa iguana.',3,'378');
INSERT INTO `quiz_pista` VALUES (379,127,'La iguana no es un animal espiritual.',7,'379');
INSERT INTO `quiz_pista` VALUES (380,127,'El camaleón cornudo no es un animal espiritual.',5,'380');
INSERT INTO `quiz_pista` VALUES (381,127,'El alacrán no es un animal espiritual.',3,'381');
INSERT INTO `quiz_pista` VALUES (382,128,'El jabalí no se caza durante la peregrinación a <i>Wirikuta</i>.',7,'382');
INSERT INTO `quiz_pista` VALUES (383,128,'El venado azul no se caza durante la peregrinación a <i>Wirikuta</i>.',5,'383');
INSERT INTO `quiz_pista` VALUES (384,128,'El águila real no se caza durante la peregrinación a <i>Wirikuta</i>.',3,'384');
INSERT INTO `quiz_pista` VALUES (385,129,'Espíritu no significa <i>’iyari</i>.',7,'385');
INSERT INTO `quiz_pista` VALUES (386,129,'Alma no significa <i>’iyari</i>.',5,'386');
INSERT INTO `quiz_pista` VALUES (387,129,'Mente no significa <i>’iyari</i>.',3,'387');
INSERT INTO `quiz_pista` VALUES (388,130,'Las comunidades originarias no constituyen el <i>nana’iyari</i>.',7,'388');
INSERT INTO `quiz_pista` VALUES (389,130,'El corazón no constituye el <i>nana’iyari</i>.',5,'389');
INSERT INTO `quiz_pista` VALUES (390,130,'Las deidades no constituyen el <i>nana’iyari</i>.',3,'390');
INSERT INTO `quiz_pista` VALUES (391,131,'Tayu no hizo el <i>nana’iyari</i>.',7,'391');
INSERT INTO `quiz_pista` VALUES (392,131,'Tatei Haramara no hizo el <i>nana’iyari</i>.',5,'392');
INSERT INTO `quiz_pista` VALUES (393,131,'Tatewari no hizo el <i>nana’iyari</i>.',3,'393');
INSERT INTO `quiz_pista` VALUES (394,132,'<i>Ha’yewaxi</i> significa guayaba.',7,'394');
INSERT INTO `quiz_pista` VALUES (395,132,'<i>Kwarɨpa</i> significa ciruela.',5,'395');
INSERT INTO `quiz_pista` VALUES (396,132,'<i>Muxu’uri</i> significa guamúchil.',3,'396');
INSERT INTO `quiz_pista` VALUES (397,133,'Guayaba no significa <i>yɨɨna</i>.',7,'397');
INSERT INTO `quiz_pista` VALUES (398,133,'Guamúchil no significa <i>yɨɨna</i>.',5,'398');
INSERT INTO `quiz_pista` VALUES (399,133,'Ciruela no significa <i>yɨɨna</i>.',3,'399');
INSERT INTO `quiz_pista` VALUES (400,134,'<i>Ké’uxa</i> significa quelite.',7,'400');
INSERT INTO `quiz_pista` VALUES (401,134,'<i>Yekwa</i> significa hongo.',5,'401');
INSERT INTO `quiz_pista` VALUES (402,134,'<i>Múme</i> significa frijol.',3,'402');
INSERT INTO `quiz_pista` VALUES (403,135,'Hongo no significa <i>ye’eri</i>.',7,'403');
INSERT INTO `quiz_pista` VALUES (404,135,'Frijol no significa <i>ye’eri</i>.',5,'404');
INSERT INTO `quiz_pista` VALUES (405,135,'Quelite no significa <i>ye’eri</i>.',3,'405');
INSERT INTO `quiz_pista` VALUES (406,136,'<i>Weurai itsari</i> significa caldo de güilota.',7,'406');
INSERT INTO `quiz_pista` VALUES (407,136,'<i>Wakana itsari</i> significa caldo de pollo.',5,'407');
INSERT INTO `quiz_pista` VALUES (408,136,'Enchilada de <i>weurai</i> significa enchilada de güilota.',3,'408');
INSERT INTO `quiz_pista` VALUES (409,137,'<i>Tawari</i> significa huevo.',7,'409');
INSERT INTO `quiz_pista` VALUES (410,137,'<i>Weurai</i> significa güilota.',5,'410');
INSERT INTO `quiz_pista` VALUES (411,137,'<i>Ketsɨ</i> significa pescado.',3,'411');
INSERT INTO `quiz_pista` VALUES (412,138,'Un <i>tsikɨri</i> no es un adorno.',7,'412');
INSERT INTO `quiz_pista` VALUES (413,138,'Un <i>tsikɨri</i> no es una comida tradicional.',5,'413');
INSERT INTO `quiz_pista` VALUES (414,138,'Un <i>tsikɨri</i> no es un sitio sagrado.',3,'414');
INSERT INTO `quiz_pista` VALUES (415,139,'No representa los disfraces.',7,'415');
INSERT INTO `quiz_pista` VALUES (416,139,'No representa el contacto con el fuego.',5,'416');
INSERT INTO `quiz_pista` VALUES (417,139,'No representa las pascuas.',3,'417');
INSERT INTO `quiz_pista` VALUES (418,140,'Valparaíso no es un sitio sagrado.',7,'418');
INSERT INTO `quiz_pista` VALUES (419,140,'Fresnillo no es un sitio sagrado.',5,'419');
INSERT INTO `quiz_pista` VALUES (420,140,'Zacatecas no es un sitio sagrado.',3,'420');
INSERT INTO `quiz_pista` VALUES (421,141,'<i>Tatewarí</i> es Nuestro abuelo fuego.',7,'421');
INSERT INTO `quiz_pista` VALUES (422,141,'<i>Tatei Haramara</i> es Nuestra madre el mar',5,'422');
INSERT INTO `quiz_pista` VALUES (423,141,'<i>Tatei Kutsaraɨpa</i> es Nuestra madre agua sagrada.',3,'423');
INSERT INTO `quiz_pista` VALUES (424,142,'<i>Tatei Haramara</i> es Nuestra madre el mar.',7,'424');
INSERT INTO `quiz_pista` VALUES (425,142,'<i>Tatei Kutsaraɨpa</i> es Nuestra madre agua sagrada.',5,'425');
INSERT INTO `quiz_pista` VALUES (426,142,'<i>Tatei Wexica Wimari</i> es Nuestra madre águila.',3,'426');
INSERT INTO `quiz_pista` VALUES (427,143,'Fiesta de los primeros frutos',7,'427');
INSERT INTO `quiz_pista` VALUES (428,143,'Fiesta del tambor',5,'428');
INSERT INTO `quiz_pista` VALUES (429,143,'Fiesta de los elotes',3,'429');
INSERT INTO `quiz_pista` VALUES (430,144,'No elaboran veladoras.',7,'430');
INSERT INTO `quiz_pista` VALUES (431,144,'No elaboran muñecos de paja.',5,'431');
INSERT INTO `quiz_pista` VALUES (432,144,'No elaboran biblias.',3,'432');
INSERT INTO `quiz_pista` VALUES (433,145,'El peyote no es una espina.',7,'433');
INSERT INTO `quiz_pista` VALUES (434,145,'El peyote no es un nopal.',5,'434');
INSERT INTO `quiz_pista` VALUES (435,145,'El peyote no es una droga.',3,'435');
INSERT INTO `quiz_pista` VALUES (436,146,'El peyote no crece en la playa.',7,'436');
INSERT INTO `quiz_pista` VALUES (437,146,'El peyote no crece en las planicies del sur.',5,'437');
INSERT INTO `quiz_pista` VALUES (438,146,'El peyote no crece en las lagunas.',3,'438');
INSERT INTO `quiz_pista` VALUES (439,147,'El peyote no tarda en crecer de 30 a 50 años.',7,'439');
INSERT INTO `quiz_pista` VALUES (440,147,'El peyote no crece en un mes.',5,'440');
INSERT INTO `quiz_pista` VALUES (441,147,'El peyote no crece en una semana.',3,'441');
INSERT INTO `quiz_pista` VALUES (442,148,'Algunas personas pueden recolectarlo.',7,'442');
INSERT INTO `quiz_pista` VALUES (443,148,'Los mestizos no pueden reclectarlo.',5,'443');
INSERT INTO `quiz_pista` VALUES (444,148,'No todos pueden recolectarlo.',3,'444');
INSERT INTO `quiz_pista` VALUES (445,149,'No se caza al jabalí y no se recolectan hongos.',7,'445');
INSERT INTO `quiz_pista` VALUES (446,149,'No se cazan iguanas y no se transporta tierra.',5,'446');
INSERT INTO `quiz_pista` VALUES (447,149,'No se caza al águila y no se transporta fuego.',3,'447');
INSERT INTO `quiz_pista` VALUES (448,150,'Los ancianos no transmiten conocimiento por medio de visiones.',7,'448');
INSERT INTO `quiz_pista` VALUES (449,150,'Los hongos no transmiten conocimiento por medio de visiones.',5,'449');
INSERT INTO `quiz_pista` VALUES (450,150,'Las flores no transmiten conocimiento por medio de visiones.',3,'450');
INSERT INTO `quiz_pista` VALUES (451,151,'No se transporta agua para pedir dinero.',7,'451');
INSERT INTO `quiz_pista` VALUES (452,151,'No se transporta agua para invocar el frío.',5,'452');
INSERT INTO `quiz_pista` VALUES (453,151,'No se transporta agua para pedir salud.',3,'453');
INSERT INTO `quiz_pista` VALUES (454,152,'<i>Ikú</i> significa maíz.',7,'454');
INSERT INTO `quiz_pista` VALUES (455,152,'<i>Ikú taxawime</i> significa maíz amarillo.',5,'455');
INSERT INTO `quiz_pista` VALUES (456,152,'<i>Ikɨri</i> significa elote.',3,'456');
INSERT INTO `quiz_pista` VALUES (457,153,'El maíz blanco no se utiliza para preparar tortillas, atole y pinole.',7,'457');
INSERT INTO `quiz_pista` VALUES (458,153,'El maíz amarillo no se utiliza para preparar tortillas, atole y pinole.',5,'458');
INSERT INTO `quiz_pista` VALUES (459,153,'El maíz rojo no se utiliza para preparar tortillas, atole y pinole.',3,'459');
INSERT INTO `quiz_pista` VALUES (460,154,'<i>Xupaxi</i> significa sopa.',7,'460');
INSERT INTO `quiz_pista` VALUES (461,154,'<i>Taku</i> significa taco.',5,'461');
INSERT INTO `quiz_pista` VALUES (462,154,'<i>Tétsu</i> significa tamal.',3,'462');
INSERT INTO `quiz_pista` VALUES (463,155,'<i>Wakaxi</i> significa vaca.',7,'463');
INSERT INTO `quiz_pista` VALUES (464,155,'<i>Tuixuyeutanaka</i> significa jabalí.',5,'464');
INSERT INTO `quiz_pista` VALUES (465,155,'<i>Wakana</i> significa pollo.',3,'465');
INSERT INTO `quiz_pista` VALUES (466,156,'<i>Hatsaruni</i> significa azadón.',7,'466');
INSERT INTO `quiz_pista` VALUES (467,156,'<i>Ha’tsa</i> significa hacha.',5,'467');
INSERT INTO `quiz_pista` VALUES (468,156,'<i>Kutsira</i> significa machete.',3,'468');
INSERT INTO `quiz_pista` VALUES (469,157,'Zacatecas no es un sitio sagrado.',7,'469');
INSERT INTO `quiz_pista` VALUES (470,157,'Fresnillo no es un sitio sagrado.',5,'470');
INSERT INTO `quiz_pista` VALUES (471,157,'Villa de Ramos no es un sitio sagrado.',3,'471');
INSERT INTO `quiz_pista` VALUES (472,158,'Fresnillo no es un sitio sagrado.',7,'472');
INSERT INTO `quiz_pista` VALUES (473,158,'Villa de Ramos no es un sitio sagrado.',5,'473');
INSERT INTO `quiz_pista` VALUES (474,158,'Zacatecas no es un sitio sagrado.',3,'474');
INSERT INTO `quiz_pista` VALUES (475,159,'Villa de Ramos no es un sitio sagrado.',7,'475');
INSERT INTO `quiz_pista` VALUES (476,159,'Zacatecas no es un sitio sagrado.',5,'476');
INSERT INTO `quiz_pista` VALUES (477,159,'Fresnillo no es un sitio sagrado.',3,'477');
INSERT INTO `quiz_pista` VALUES (478,160,'<i>Mara’kame</i> significa chamán.',7,'478');
INSERT INTO `quiz_pista` VALUES (479,160,'<i>Tatuwani</i> significa gobernador.',5,'479');
INSERT INTO `quiz_pista` VALUES (480,160,'<i>Xuku’uri ɨkame</i> significa jicarero.',3,'480');
INSERT INTO `quiz_pista` VALUES (481,161,'Los doctores no son la autoridad máxima en las comunidades.',7,'481');
INSERT INTO `quiz_pista` VALUES (482,161,'Los maestros no son la autoridad máxima en las comunidades.',5,'482');
INSERT INTO `quiz_pista` VALUES (483,161,'Los gobernadores no son la autoridad máxima en las comunidades.',3,'483');
INSERT INTO `quiz_pista` VALUES (484,162,'Los chamanes no sueñan a los nuevos funcionarios del gobierno comunal.',7,'484');
INSERT INTO `quiz_pista` VALUES (485,162,'Los maestros no sueñan a los nuevos funcionarios del gobierno comunal.',5,'485');
INSERT INTO `quiz_pista` VALUES (486,162,'Los doctores no sueñan a los nuevos funcionarios del gobierno comunal.',3,'486');
INSERT INTO `quiz_pista` VALUES (487,163,'No elaboran morrales de chaquira.',7,'487');
INSERT INTO `quiz_pista` VALUES (488,163,'No elaboran escudos de chaquira.',5,'488');
INSERT INTO `quiz_pista` VALUES (489,163,'No elaboran flechas de chaquira.',3,'489');
INSERT INTO `quiz_pista` VALUES (490,164,'No elaboran collares de estambre.',7,'490');
INSERT INTO `quiz_pista` VALUES (491,164,'No elaboran aretes de estambre.',5,'491');
INSERT INTO `quiz_pista` VALUES (492,164,'No elaboran pulseras de estambre.',3,'492');
INSERT INTO `quiz_pista` VALUES (493,165,'<i>Tuwaxate</i> significa capas.',7,'493');
INSERT INTO `quiz_pista` VALUES (494,165,'<i>Hɨiyamete</i> significa cinturones.',5,'494');
INSERT INTO `quiz_pista` VALUES (495,165,'<i>Kakaíte</i> significa huaraches.',3,'495');
INSERT INTO `quiz_pista` VALUES (496,166,'<i>Hɨiyamete</i> significa cinturones.',7,'496');
INSERT INTO `quiz_pista` VALUES (497,166,'<i>Kakaíte</i> significa huaraches.',5,'497');
INSERT INTO `quiz_pista` VALUES (498,166,'<i>Tuwaxate</i> significa capas.',3,'498');
INSERT INTO `quiz_pista` VALUES (499,167,'<i>Kakaíte</i> significa huaraches.',7,'499');
INSERT INTO `quiz_pista` VALUES (500,167,'<i>Hɨiyamete</i> significa cinturones.',5,'500');
INSERT INTO `quiz_pista` VALUES (501,167,'<i>Tuwaxate</i> significa capas.',3,'501');
INSERT INTO `quiz_pista` VALUES (502,168,'<i>Iwíte</i> significa faldas.',7,'502');
INSERT INTO `quiz_pista` VALUES (503,168,'<i>Kamixate</i> significa camisas.',5,'503');
INSERT INTO `quiz_pista` VALUES (504,168,'<i>Xikurite</i> significa pañuelos.',3,'504');
INSERT INTO `quiz_pista` VALUES (505,169,'No hay maíz negro, gris y café.',7,'505');
INSERT INTO `quiz_pista` VALUES (506,169,'No hay maíz rosa, naranja y verde.',5,'506');
INSERT INTO `quiz_pista` VALUES (507,169,'No hay maíz dorado y plateado.',3,'507');
INSERT INTO `quiz_pista` VALUES (508,170,'La Madre del elote no les dio el maíz a los wixaritari</i>.',7,'508');
INSERT INTO `quiz_pista` VALUES (509,170,'El Dios del campo no les dio el maíz a los <i>wixaritari</i>.',5,'509');
INSERT INTO `quiz_pista` VALUES (510,170,'El Padre del maíz no les dio el maíz a los <i>wixaritari</i>.',3,'510');
INSERT INTO `quiz_pista` VALUES (511,171,'<i>Taraki</i> significa bulbos.',7,'511');
INSERT INTO `quiz_pista` VALUES (512,171,'<i>Kweets</i>i significa habas.',5,'512');
INSERT INTO `quiz_pista` VALUES (513,171,'<i>Karimutsi</i> significa pochote.',3,'513');
INSERT INTO `quiz_pista` VALUES (514,172,'Pochote no significa <i>tsíweri</i>.',7,'514');
INSERT INTO `quiz_pista` VALUES (515,172,'Habas no significa <i>tsíweri</i>.',5,'515');
INSERT INTO `quiz_pista` VALUES (516,172,'Guajes no significa <i>tsíweri</i>.',3,'516');
INSERT INTO `quiz_pista` VALUES (517,173,'<i>Tsíweri</i> significa gualumbos.',7,'517');
INSERT INTO `quiz_pista` VALUES (518,173,'<i>Taraki</i> significa bulbos.',5,'518');
INSERT INTO `quiz_pista` VALUES (519,173,'<i>Karimutsi</i> significa pochote.',3,'519');
INSERT INTO `quiz_pista` VALUES (520,174,'Guajes no significa <i>kweetsi</i>.',7,'520');
INSERT INTO `quiz_pista` VALUES (521,174,'Gualumbos no significa <i>kweetsi</i>.',5,'521');
INSERT INTO `quiz_pista` VALUES (522,174,'Pochote no significa <i>kweetsi</i>.',3,'522');
INSERT INTO `quiz_pista` VALUES (523,175,'<i>Ketsɨ itsari</i> significa caldo de pescado.',7,'523');
INSERT INTO `quiz_pista` VALUES (524,175,'<i>Tekɨ itsari</i> significa caldo de ardilla.',5,'524');
INSERT INTO `quiz_pista` VALUES (525,175,'<i>Wakana itsari</i> significa caldo de pollo.',3,'525');
INSERT INTO `quiz_pista` VALUES (526,176,'<i>Mitsu</i> significa gato.',7,'526');
INSERT INTO `quiz_pista` VALUES (527,176,'<i>Tekɨ</i> significa ardilla.',5,'527');
INSERT INTO `quiz_pista` VALUES (528,176,'<i>Tsɨkɨ</i> significa perro.',3,'528');
INSERT INTO `quiz_pista` VALUES (529,177,'Santo Domingo no es un sitio sagrado.',7,'529');
INSERT INTO `quiz_pista` VALUES (530,177,'Villa de Ramos no es un sitio sagrado.',5,'530');
INSERT INTO `quiz_pista` VALUES (531,177,'Zacatecas no es un sitio sagrado.',3,'531');
INSERT INTO `quiz_pista` VALUES (532,178,'<i>Tatei Haramara</i> es Nuestra madre el mar.',7,'532');
INSERT INTO `quiz_pista` VALUES (533,178,'<i>Tatei Yurienáka</i> es Nuestra madre tierra.',5,'533');
INSERT INTO `quiz_pista` VALUES (534,178,'<i>Tatewari</i> es Nuestro abuelo fuego.',3,'534');
INSERT INTO `quiz_pista` VALUES (535,179,'No se reunieron en Santo Domingo.',7,'535');
INSERT INTO `quiz_pista` VALUES (536,179,'No se reunieron en <i>Mɨ tɨranitsie</i>.',5,'536');
INSERT INTO `quiz_pista` VALUES (537,179,'No se reunieron en Villa de Ramos.',3,'537');
INSERT INTO `quiz_pista` VALUES (538,180,'No se utiliza tambor y trompeta.',7,'538');
INSERT INTO `quiz_pista` VALUES (539,180,'No se utiliza flauta y piano.',5,'539');
INSERT INTO `quiz_pista` VALUES (540,180,'No se utiliza marimba.',3,'540');
INSERT INTO `quiz_pista` VALUES (541,181,'Tempestad de La Yesca, Nayarit no es el conjunto más famoso.',7,'541');
INSERT INTO `quiz_pista` VALUES (542,181,'Viento <i>Wixárika</i> de Tepic, Nayarit no es el conjunto más famoso.',5,'542');
INSERT INTO `quiz_pista` VALUES (543,181,'El Venado de Chapala, Jalisco no es el conjunto más famoso.',3,'543');
INSERT INTO `quiz_pista` VALUES (544,182,'No se utiliza para fines textiles.',7,'544');
INSERT INTO `quiz_pista` VALUES (545,182,'No se utiliza para hacer pegamento.',5,'545');
INSERT INTO `quiz_pista` VALUES (546,182,'No se utiliza para hacer postres.',3,'546');
INSERT INTO `quiz_pista` VALUES (547,183,'Las piedras no son la medicina más potente para ahuyentar el mal o las influencias sobrenaturales.',7,'547');
INSERT INTO `quiz_pista` VALUES (548,183,'El cactus no es la medicina más potente para ahuyentar el mal o las influencias sobrenaturales.',5,'548');
INSERT INTO `quiz_pista` VALUES (549,183,'Las rosas no son la medicina más potente para ahuyentar el mal o las influencias sobrenaturales.',3,'549');
INSERT INTO `quiz_pista` VALUES (550,184,'No se necesita orar a las deidades.',7,'550');
INSERT INTO `quiz_pista` VALUES (551,184,'No se necesita realizar una ceremonia de consagración.',5,'551');
INSERT INTO `quiz_pista` VALUES (552,184,'No se necesita bailar a las deidades.',3,'552');
INSERT INTO `quiz_pista` VALUES (553,185,'No se debe correr y saltar.',7,'553');
INSERT INTO `quiz_pista` VALUES (554,185,'No se debe bailar y cantar.',5,'554');
INSERT INTO `quiz_pista` VALUES (555,185,'No se debe vestir una túnica y escuchar música.',3,'555');
INSERT INTO `quiz_pista` VALUES (556,186,'<i>Kweetsi</i> significa habas.',7,'556');
INSERT INTO `quiz_pista` VALUES (557,186,'<i>Taraki</i> significa bulbos.',5,'557');
INSERT INTO `quiz_pista` VALUES (558,186,'<i>Tsíweri</i> significa gualumbos.',3,'558');
INSERT INTO `quiz_pista` VALUES (559,187,'Habas no significa <i>karimutsi</i>.',7,'559');
INSERT INTO `quiz_pista` VALUES (560,187,'Guajes no significa <i>karimutsi</i>.',5,'560');
INSERT INTO `quiz_pista` VALUES (561,187,'Gualumbos no significa <i>karimutsi</i>.',3,'561');
INSERT INTO `quiz_pista` VALUES (562,188,'<i>Ikɨri</i> significa elote.',7,'562');
INSERT INTO `quiz_pista` VALUES (563,188,'<i>Ikɨri kwitsarietɨ</i> significa elote cocido.',5,'563');
INSERT INTO `quiz_pista` VALUES (564,188,'<i>Ikú</i> significa maíz.',3,'564');
INSERT INTO `quiz_pista` VALUES (565,189,'<i>Tuixu</i> significa cerdo.',7,'565');
INSERT INTO `quiz_pista` VALUES (566,189,'<i>Tuixuyeutanaka</i> significa jabalí.',5,'566');
INSERT INTO `quiz_pista` VALUES (567,189,'<i>Wakana</i> significa pollo.',3,'567');
INSERT INTO `quiz_pista` VALUES (568,190,'<i>Charcas</i> no es un sitio sagrado.',7,'568');
INSERT INTO `quiz_pista` VALUES (569,190,'Santo Domingo no es un sitio sagrado.',5,'569');
INSERT INTO `quiz_pista` VALUES (570,190,'Villa de Ramos no es un sitio sagrado.',3,'570');
INSERT INTO `quiz_pista` VALUES (571,191,'Villa de Ramos no es un sitio sagrado.',7,'571');
INSERT INTO `quiz_pista` VALUES (572,191,'Charcas no es un sitio sagrado.',5,'572');
INSERT INTO `quiz_pista` VALUES (573,191,'Santo Domingo no es un sitio sagrado.',3,'573');
INSERT INTO `quiz_pista` VALUES (574,192,'<i>Kwixɨ</i> significa águila.',7,'574');
INSERT INTO `quiz_pista` VALUES (575,192,'<i>Peexá</i> significa pájaro.',5,'575');
INSERT INTO `quiz_pista` VALUES (576,192,'<i>Kwatsa</i> significa cuervo.',3,'576');
INSERT INTO `quiz_pista` VALUES (577,193,'El cuervo no es un animal espiritual.',7,'577');
INSERT INTO `quiz_pista` VALUES (578,193,'El pájaro no es un animal espiritual.',5,'578');
INSERT INTO `quiz_pista` VALUES (579,193,'El halcón no es un animal espiritual.',3,'579');
INSERT INTO `quiz_pista` VALUES (580,194,'No utilizan cera.',7,'580');
INSERT INTO `quiz_pista` VALUES (581,194,'No utilizan madera.',5,'581');
INSERT INTO `quiz_pista` VALUES (582,194,'No utilizan pintura.',3,'582');
INSERT INTO `quiz_pista` VALUES (583,195,'No bordan dinero.',7,'583');
INSERT INTO `quiz_pista` VALUES (584,195,'No bordan animales silvestres.',5,'584');
INSERT INTO `quiz_pista` VALUES (585,195,'No bordan instrumentos musicales.',3,'585');
INSERT INTO `quiz_pista` VALUES (586,196,'El <i>tatuwani</i> no establece contacto con las deidades.',7,'586');
INSERT INTO `quiz_pista` VALUES (587,196,'El <i>xuku’uri ɨkame</i> no establece contacto con las deidades.',5,'587');
INSERT INTO `quiz_pista` VALUES (588,196,'El <i>kawiteru</i> no establece contacto con las deidades.',3,'588');
INSERT INTO `quiz_pista` VALUES (589,197,'No le pide buenas cosechas y fertilidad.',7,'589');
INSERT INTO `quiz_pista` VALUES (590,197,'No le pide dinero y oro.',5,'590');
INSERT INTO `quiz_pista` VALUES (591,197,'No le pide que llueva y no haya sequía.',3,'591');
INSERT INTO `quiz_pista` VALUES (592,198,'El cactus no se relaciona con las festividades, peregrinaciones y ofrendas.',7,'592');
INSERT INTO `quiz_pista` VALUES (593,198,'El pochote no se relaciona con las festividades, peregrinaciones y ofrendas.',5,'593');
INSERT INTO `quiz_pista` VALUES (594,198,'El nopal no se relaciona con las festividades, peregrinaciones y ofrendas.',3,'594');
INSERT INTO `quiz_pista` VALUES (595,199,'No se recolecta agua.',7,'595');
INSERT INTO `quiz_pista` VALUES (596,199,'No se recolectan cactus.',5,'596');
INSERT INTO `quiz_pista` VALUES (597,199,'No se recolectan piedras.',3,'597');
INSERT INTO `quiz_pista` VALUES (598,200,'Los animales no son los guardianes del peyote.',7,'598');
INSERT INTO `quiz_pista` VALUES (599,200,'Los mestizos no son los guardianes del peyote.',5,'599');
INSERT INTO `quiz_pista` VALUES (600,200,'Los cactus no son los guardianes del peyote.',3,'600');
INSERT INTO `quiz_pista` VALUES (601,201,'La fiesta del tambor no es la celebración más importante.',7,'601');
INSERT INTO `quiz_pista` VALUES (602,201,'La peregrinación al centro de la tierra no es la celebración más importante.',5,'602');
INSERT INTO `quiz_pista` VALUES (603,201,'La peregrinación al mar no es la celebración más importante.',3,'603');
INSERT INTO `quiz_pista` VALUES (604,202,'El que no ve y quiere ver no significa <i>matewáme</i>.',7,'604');
INSERT INTO `quiz_pista` VALUES (605,202,'El que puede soñar no significa <i>matewáme</i>.',5,'605');
INSERT INTO `quiz_pista` VALUES (606,202,'El que puede viajar no significa <i>matewáme</i>.',3,'606');
INSERT INTO `quiz_pista` VALUES (607,203,'Los que pueden viajar no son nombrados <i>matewáme</i>.',7,'607');
INSERT INTO `quiz_pista` VALUES (608,203,'Quienes peregrinan por última vez no son nombrados <i>matewáme</i>.',5,'608');
INSERT INTO `quiz_pista` VALUES (609,203,'Los que pueden soñar no son nombrados <i>matewáme</i>.',3,'609');
INSERT INTO `quiz_pista` VALUES (610,204,'<i>Takutsi Nakawé</i> es Nuestra abuela tierra.',7,'610');
INSERT INTO `quiz_pista` VALUES (611,204,'<i>Tatei Kutsaraɨpa</i> es Nuestra madre agua sagrada.',5,'611');
INSERT INTO `quiz_pista` VALUES (612,204,'<i>Tatei Wexica Wimari</i> es Nuestra madre águila.',3,'612');
INSERT INTO `quiz_pista` VALUES (613,205,'<i>Ma’ara</i> significa pitahaya.',7,'613');
INSERT INTO `quiz_pista` VALUES (614,205,'<i>Ka’arú</i> significa plátano.',5,'614');
INSERT INTO `quiz_pista` VALUES (615,205,'<i>Ha’yewaxi</i> significa guayaba.',3,'615');
INSERT INTO `quiz_pista` VALUES (616,206,'Pitahaya no significa <i>narakaxi</i>.',7,'616');
INSERT INTO `quiz_pista` VALUES (617,206,'Guayaba no significa <i>narakaxi</i>.',5,'617');
INSERT INTO `quiz_pista` VALUES (618,206,'Plátano no significa <i>narakaxi</i>.',3,'618');
INSERT INTO `quiz_pista` VALUES (619,207,'<i>Uwakí</i> significa nanchi.',7,'619');
INSERT INTO `quiz_pista` VALUES (620,207,'<i>Yɨɨna</i> significa tuna.',5,'620');
INSERT INTO `quiz_pista` VALUES (621,207,'<i>Kwarɨpa</i> significa ciruela.',3,'621');
INSERT INTO `quiz_pista` VALUES (622,208,'Ciruela no significa <i>piní</i>.',7,'622');
INSERT INTO `quiz_pista` VALUES (623,208,'Tuna no significa <i>piní</i>.',5,'623');
INSERT INTO `quiz_pista` VALUES (624,208,'Nanchi no significa <i>piní</i>.',3,'624');
INSERT INTO `quiz_pista` VALUES (625,209,'<i>Hamuitsi</i> significa atole.',7,'625');
INSERT INTO `quiz_pista` VALUES (626,209,'<i>Nawá</i> significa tejuino.',5,'626');
INSERT INTO `quiz_pista` VALUES (627,209,'<i>Uwá hayaári</i> significa jugo de caña.',3,'627');
INSERT INTO `quiz_pista` VALUES (628,210,'<i>Taku</i> significa taco.',7,'628');
INSERT INTO `quiz_pista` VALUES (629,210,'<i>Kexiu</i> significa queso.',5,'629');
INSERT INTO `quiz_pista` VALUES (630,210,'<i>Tétsu</i> significa tamal.',3,'630');
INSERT INTO `quiz_pista` VALUES (631,211,'Una <i>nierika</i> no es un centro ceremonial.',7,'631');
INSERT INTO `quiz_pista` VALUES (632,211,'Una <i>nierika</i> no es una artesanía Wixárika.',5,'632');
INSERT INTO `quiz_pista` VALUES (633,211,'Una <i>nierika</i> no es un sitio sagrado.',3,'633');
INSERT INTO `quiz_pista` VALUES (634,212,'No representa el viaje al inframundo.',7,'634');
INSERT INTO `quiz_pista` VALUES (635,212,'No representa el mundo de las sombras y la cosmovisión del inframundo.',5,'635');
INSERT INTO `quiz_pista` VALUES (636,212,'No representa el sueño <i>Wixárika</i>.',3,'636');
INSERT INTO `quiz_pista` VALUES (637,213,'No se utiliza como regalo.',7,'637');
INSERT INTO `quiz_pista` VALUES (638,213,'No se utiliza como cuadro de decoración.',5,'638');
INSERT INTO `quiz_pista` VALUES (639,213,'No se utiliza para cortar alimentos.',3,'639');
INSERT INTO `quiz_pista` VALUES (640,214,'Charcas no es un sitio sagrado.',7,'640');
INSERT INTO `quiz_pista` VALUES (641,214,'Santo Domingo no es un sitio sagrado.',5,'641');
INSERT INTO `quiz_pista` VALUES (642,214,'Real de Catorce no es un sitio sagrado.',3,'642');
INSERT INTO `quiz_pista` VALUES (643,215,'<i>Tatewari</i> es Nuestro abuelo fuego.',7,'643');
INSERT INTO `quiz_pista` VALUES (644,215,'<i>Tatéi Matiniéri</i> es la Diosa de la lluvia del poniente.',5,'644');
INSERT INTO `quiz_pista` VALUES (645,215,'<i>Tatei Kutsaraɨpa</i> es Nuestra madre agua sagrada.',3,'645');
INSERT INTO `quiz_pista` VALUES (646,216,'No representa al cielo.',7,'646');
INSERT INTO `quiz_pista` VALUES (647,216,'No representa al infinito.',5,'647');
INSERT INTO `quiz_pista` VALUES (648,216,'No representa a la tierra.',3,'648');
INSERT INTO `quiz_pista` VALUES (649,217,'<i>Tatei Kutsaraɨpa</i> es Nuestra madre agua sagrada.',7,'649');
INSERT INTO `quiz_pista` VALUES (650,217,'<i>Tatéi Matiniéri</i> es la Diosa de la lluvia del poniente.',5,'650');
INSERT INTO `quiz_pista` VALUES (651,217,'<i>Tututzi Maxa Kwaxi</i> es Nuestro bisabuelo cola de venado.',3,'651');
INSERT INTO `quiz_pista` VALUES (652,218,'No nace en Zacatecas.',7,'652');
INSERT INTO `quiz_pista` VALUES (653,218,'No nace en Tepic.',5,'653');
INSERT INTO `quiz_pista` VALUES (654,218,'No nace en Durango.',3,'654');
INSERT INTO `quiz_pista` VALUES (655,219,'No nace en Tepic.',7,'655');
INSERT INTO `quiz_pista` VALUES (656,219,'No nace en Durango.',5,'656');
INSERT INTO `quiz_pista` VALUES (657,219,'No nace en Zacatecas.',3,'657');
INSERT INTO `quiz_pista` VALUES (658,220,'No se plasman con plumones.',7,'658');
INSERT INTO `quiz_pista` VALUES (659,220,'No se plasman con chaquira, listones y telas.',5,'659');
INSERT INTO `quiz_pista` VALUES (660,220,'No se plasman con colores.',3,'660');
INSERT INTO `quiz_pista` VALUES (661,221,'No finaliza en la Laguna de Chapala.',7,'661');
INSERT INTO `quiz_pista` VALUES (662,221,'No finaliza en el Río Santiago.',5,'662');
INSERT INTO `quiz_pista` VALUES (663,221,'No finaliza en el desierto de Sonora.',3,'663');
INSERT INTO `quiz_pista` VALUES (664,222,'No dejan ofrendas en los pozos.',7,'664');
INSERT INTO `quiz_pista` VALUES (665,222,'No dejan ofrendas en el centro.',5,'665');
INSERT INTO `quiz_pista` VALUES (666,222,'No dejan ofrendas en los campos.',3,'666');
INSERT INTO `quiz_pista` VALUES (667,223,'No se hace una cena de agradecimiento.',7,'667');
INSERT INTO `quiz_pista` VALUES (668,223,'No se hace una ceremonia de bautizo.',5,'668');
INSERT INTO `quiz_pista` VALUES (669,223,'No se hace una oración.',3,'669');
INSERT INTO `quiz_pista` VALUES (670,224,'No se van a los tukipa.',7,'670');
INSERT INTO `quiz_pista` VALUES (671,224,'No se van a sus casas.',5,'671');
INSERT INTO `quiz_pista` VALUES (672,224,'No se van a las iglesias.',3,'672');
INSERT INTO `quiz_pista` VALUES (673,225,'<i>Ikú</i> significa maíz.',7,'673');
INSERT INTO `quiz_pista` VALUES (674,225,'<i>Ikɨri</i> significa elote.',5,'674');
INSERT INTO `quiz_pista` VALUES (675,225,'<i>Iku’ yuawime</i> significa maíz azul.',3,'675');
INSERT INTO `quiz_pista` VALUES (676,226,'El maíz azul no se utiliza en las ceremonias religiosas.',7,'676');
INSERT INTO `quiz_pista` VALUES (677,226,'El maíz amarillo no se utiliza en las ceremonias religiosas.',5,'677');
INSERT INTO `quiz_pista` VALUES (678,226,'El maíz rojo no se utiliza en las ceremonias religiosas.',3,'678');
INSERT INTO `quiz_pista` VALUES (679,227,'<i>Paní</i> significa pan.',7,'679');
INSERT INTO `quiz_pista` VALUES (680,227,'<i>Tsakaka</i> significa piloncillo.',5,'680');
INSERT INTO `quiz_pista` VALUES (681,227,'<i>Xiete</i> significa miel.',3,'681');
INSERT INTO `quiz_pista` VALUES (682,228,'<i>Kamaika hayaári</i> significa agua de jamaica.',7,'682');
INSERT INTO `quiz_pista` VALUES (683,228,'<i>Narakaxi hayaári</i> significa agua de naranja.',5,'683');
INSERT INTO `quiz_pista` VALUES (684,228,'<i>Hayaári</i> significa jugo.',3,'684');
INSERT INTO `quiz_pista` VALUES (685,229,'<i>Charcas</i> no es un sitio sagrado.',7,'685');
INSERT INTO `quiz_pista` VALUES (686,229,'Real de Catorce no es un sitio sagrado.',5,'686');
INSERT INTO `quiz_pista` VALUES (687,229,'<i>Mezquital</i> no es un sitio sagrado.',3,'687');
INSERT INTO `quiz_pista` VALUES (688,230,'<i>Tiɨkitame</i> significa maestro.',7,'688');
INSERT INTO `quiz_pista` VALUES (689,230,'<i>Tiyuuayewamame</i> significa médico.',5,'689');
INSERT INTO `quiz_pista` VALUES (690,230,'<i>Tituayame</i> significa vendedor.',3,'690');
INSERT INTO `quiz_pista` VALUES (691,231,'Los doctores no son los mensajeros de las comunidades.',7,'691');
INSERT INTO `quiz_pista` VALUES (692,231,'Los gobernadores no son los mensajeros de las comunidades.',5,'692');
INSERT INTO `quiz_pista` VALUES (693,231,'Los maestros no son los mensajeros de las comunidades.',3,'693');
INSERT INTO `quiz_pista` VALUES (694,232,'No se siembran por sus vitaminas.',7,'694');
INSERT INTO `quiz_pista` VALUES (695,232,'No se siembran por ser bonitas.',5,'695');
INSERT INTO `quiz_pista` VALUES (696,232,'No se siembran por sus colores.',3,'696');
INSERT INTO `quiz_pista` VALUES (697,233,'No se siembran para que adornen el <i>coamil</i>.',7,'697');
INSERT INTO `quiz_pista` VALUES (698,233,'No se siembran para alimentar a las aves.',5,'698');
INSERT INTO `quiz_pista` VALUES (699,233,'No se siembran para espantar animales salvajes.',3,'699');
INSERT INTO `quiz_pista` VALUES (700,234,'No se siembran para alimentar a las aves.',7,'700');
INSERT INTO `quiz_pista` VALUES (701,234,'No se siembran para espantar animales salvajes.',5,'701');
INSERT INTO `quiz_pista` VALUES (702,234,'No se siembran para que adornen el <i>coamil</i>.',3,'702');
INSERT INTO `quiz_pista` VALUES (703,235,'El 7 no es un número importante en la cosmogonía <i>Wixárika</i>.',7,'703');
INSERT INTO `quiz_pista` VALUES (704,235,'El 3 no es un número importante en la cosmogonía <i>Wixárika</i>.',5,'704');
INSERT INTO `quiz_pista` VALUES (705,235,'El 1 no es un número importante en la cosmogonía <i>Wixárika</i>.',3,'705');
INSERT INTO `quiz_pista` VALUES (706,236,'<i>Yekwa</i> significa champiñón.',7,'706');
INSERT INTO `quiz_pista` VALUES (707,236,'<i>Na’akari</i> significa nopal.',5,'707');
INSERT INTO `quiz_pista` VALUES (708,236,'<i>Xútsi</i> significa calabacita.',3,'708');
INSERT INTO `quiz_pista` VALUES (709,237,'<i>Xutsíte</i> significa calabacitas.',7,'709');
INSERT INTO `quiz_pista` VALUES (710,237,'<i>Yekwa’ate</i> significa hongos.',5,'710');
INSERT INTO `quiz_pista` VALUES (711,237,'<i>Na’akarite</i> significa nopales.',3,'711');
INSERT INTO `quiz_pista` VALUES (712,238,'Nopal no significa <i>aɨraxa</i>.',7,'712');
INSERT INTO `quiz_pista` VALUES (713,238,'Hongos no significa <i>aɨraxa</i>.',5,'713');
INSERT INTO `quiz_pista` VALUES (714,238,'Calabacita no significa <i>aɨraxa</i>.',3,'714');
INSERT INTO `quiz_pista` VALUES (715,239,'<i>Ké’uxa</i> significa quelite.',7,'715');
INSERT INTO `quiz_pista` VALUES (716,239,'<i>Ikú</i> significa maíz.',5,'716');
INSERT INTO `quiz_pista` VALUES (717,239,'<i>Aɨraxa</i> significa verdolaga.',3,'717');
INSERT INTO `quiz_pista` VALUES (718,240,'Maíz no es <i>haxi</i>.',7,'718');
INSERT INTO `quiz_pista` VALUES (719,240,'Quelite no es <i>haxi</i>.',5,'719');
INSERT INTO `quiz_pista` VALUES (720,240,'Verdolaga no es <i>haxi</i>.',3,'720');
INSERT INTO `quiz_pista` VALUES (721,241,'<i>Pa’apa</i> significa tortilla.',7,'721');
INSERT INTO `quiz_pista` VALUES (722,241,'<i>Kexiu</i> significa queso.',5,'722');
INSERT INTO `quiz_pista` VALUES (723,241,'Quesadilla con <i>yekwa’ate</i> significa quesadilla con hongos.',3,'723');
INSERT INTO `quiz_pista` VALUES (724,242,'<i>Eɨkariti</i> significa eucalipto.',7,'724');
INSERT INTO `quiz_pista` VALUES (725,242,'<i>Wáxa</i> significa milpa.',5,'725');
INSERT INTO `quiz_pista` VALUES (726,242,'<i>Hikuri</i> significa peyote.',3,'726');
INSERT INTO `quiz_pista` VALUES (727,243,'<i>Hatsaruni</i> significa azadón.',7,'727');
INSERT INTO `quiz_pista` VALUES (728,243,'<i>Nawaxa</i> significa cuchillo.',5,'728');
INSERT INTO `quiz_pista` VALUES (729,243,'<i>Mɨtsɨtɨari</i> significa cuña.',3,'729');
INSERT INTO `quiz_pista` VALUES (730,244,'<i>Kɨyé</i> significa palo.',7,'730');
INSERT INTO `quiz_pista` VALUES (731,244,'<i>Ha’úte</i> significa roca.',5,'731');
INSERT INTO `quiz_pista` VALUES (732,244,'<i>Kaunari</i> significa soga.',3,'732');
INSERT INTO `quiz_pista` VALUES (733,245,'<i>Kaunarite</i> significa sogas.',7,'733');
INSERT INTO `quiz_pista` VALUES (734,245,'<i>Kɨyéxi</i> significa palos.',5,'734');
INSERT INTO `quiz_pista` VALUES (735,245,'<i>Ha’utete</i> significa rocas.',3,'735');
INSERT INTO `quiz_pista` VALUES (736,246,'No se utilizan para la agricultura.',7,'736');
INSERT INTO `quiz_pista` VALUES (737,246,'No se utilizan para la pesca.',5,'737');
INSERT INTO `quiz_pista` VALUES (738,246,'No se utilizan para las artesanías.',3,'738');
INSERT INTO `quiz_pista` VALUES (739,247,'Pueblo nuevo no es un sitio sagrado.',7,'739');
INSERT INTO `quiz_pista` VALUES (740,247,'Durango no es un sitio sagrado.',5,'740');
INSERT INTO `quiz_pista` VALUES (741,247,'Mezquital no es un sitio sagrado.',3,'741');
INSERT INTO `quiz_pista` VALUES (742,248,'La tradición de los elotes no es una tradición es esencial.',7,'742');
INSERT INTO `quiz_pista` VALUES (743,248,'La cacería del lobo no es una tradición es esencial.',5,'743');
INSERT INTO `quiz_pista` VALUES (744,248,'Las ceremonias del mar no es una tradición es esencial.',3,'744');
INSERT INTO `quiz_pista` VALUES (745,249,'No son 3 cazadores cósmicos.',7,'745');
INSERT INTO `quiz_pista` VALUES (746,249,'No son 7 cazadores cósmicos.',5,'746');
INSERT INTO `quiz_pista` VALUES (747,249,'No son 4 cazadores cósmicos.',3,'747');
INSERT INTO `quiz_pista` VALUES (748,250,'No representan una dirección del camino.',7,'748');
INSERT INTO `quiz_pista` VALUES (749,250,'No representan un animal silvestre.',5,'749');
INSERT INTO `quiz_pista` VALUES (750,250,'No representan una deidad.',3,'750');
INSERT INTO `quiz_pista` VALUES (751,251,'<i>Aɨraxa</i> significa verdolaga.',7,'751');
INSERT INTO `quiz_pista` VALUES (752,251,'<i>Xútsi</i> significa calabacita.',5,'752');
INSERT INTO `quiz_pista` VALUES (753,251,'<i>Yekwa</i> significa champiñón.',3,'753');
INSERT INTO `quiz_pista` VALUES (754,252,'Calabacita no significa <i>na’akari</i>.',7,'754');
INSERT INTO `quiz_pista` VALUES (755,252,'Verdolaga no significa <i>na’akari</i>.',5,'755');
INSERT INTO `quiz_pista` VALUES (756,252,'Champiñón no significa <i>na’akari</i>.',3,'756');
INSERT INTO `quiz_pista` VALUES (757,253,'<i>Yɨɨna</i> significa tuna.',7,'757');
INSERT INTO `quiz_pista` VALUES (758,253,'<i>Uwakí</i> significa nanchi.',5,'758');
INSERT INTO `quiz_pista` VALUES (759,253,'<i>Kwarɨpa</i> significa ciruela.',3,'759');
INSERT INTO `quiz_pista` VALUES (760,254,'Nanchi no es <i>muxu’uri</i>.',7,'760');
INSERT INTO `quiz_pista` VALUES (761,254,'Ciruela no es <i>muxu’uri</i>.',5,'761');
INSERT INTO `quiz_pista` VALUES (762,254,'Tuna no es <i>muxu’uri</i>.',3,'762');
INSERT INTO `quiz_pista` VALUES (763,255,'<i>Ikɨri wawarikitɨka</i> significa elotes asados.',7,'763');
INSERT INTO `quiz_pista` VALUES (764,255,'<i>Kukuríte tsunariyari</i> significa salsas.',5,'764');
INSERT INTO `quiz_pista` VALUES (765,255,'<i>Ikɨri kwitsarietɨka</i> significa elotes cocidos.',3,'765');
INSERT INTO `quiz_pista` VALUES (766,256,'<i>Ikɨri kwitsarietɨka</i> significa elotes cocidos.',7,'766');
INSERT INTO `quiz_pista` VALUES (767,256,'<i>Ikɨri wawarikitɨka</i> significa elotes asados.',5,'767');
INSERT INTO `quiz_pista` VALUES (768,256,'<i>Kukuríte tsunariyari</i> significa salsas.',3,'768');
INSERT INTO `quiz_pista` VALUES (769,257,'<i>Kɨrapu</i> significa clavo.',7,'769');
INSERT INTO `quiz_pista` VALUES (770,257,'<i>Mantsaniya</i> significa manzanilla.',5,'770');
INSERT INTO `quiz_pista` VALUES (771,257,'<i>Yervawena</i> significa hierbabuena.',3,'771');
INSERT INTO `quiz_pista` VALUES (772,258,'Pueblo nuevo no es un sitio sagrado.',7,'772');
INSERT INTO `quiz_pista` VALUES (773,258,'Durango no es un sitio sagrado.',5,'773');
INSERT INTO `quiz_pista` VALUES (774,258,'Mezquital no es un sitio sagrado.',3,'774');
INSERT INTO `quiz_pista` VALUES (775,259,'<i>Imukwi</i> significa salamandra.',7,'775');
INSERT INTO `quiz_pista` VALUES (776,259,'<i>Ɨkwi</i> significa lagartija.',5,'776');
INSERT INTO `quiz_pista` VALUES (777,259,'<i>Ke’etsé</i> significa iguana.',3,'777');
INSERT INTO `quiz_pista` VALUES (778,260,'La vaca no es un animal espiritual.',7,'778');
INSERT INTO `quiz_pista` VALUES (779,260,'El pollo no es un animal espiritual.',5,'779');
INSERT INTO `quiz_pista` VALUES (780,260,'El cerdo no es un animal espiritual.',3,'780');
INSERT INTO `quiz_pista` VALUES (781,261,'El maestro no elabora los trajes tradicionales <i>Wixárika</i>.',7,'781');
INSERT INTO `quiz_pista` VALUES (782,261,'El coamilero no elabora los trajes tradicionales <i>Wixárika</i>.',5,'782');
INSERT INTO `quiz_pista` VALUES (783,261,'El chamán no elabora los trajes tradicionales <i>Wixárika</i>.',3,'783');
INSERT INTO `quiz_pista` VALUES (784,262,'No visten pantalones cortos y chalecos de manta.',7,'784');
INSERT INTO `quiz_pista` VALUES (785,262,'No visten pantalón de mezclilla y camisa de algodón.',5,'785');
INSERT INTO `quiz_pista` VALUES (786,262,'No visten túnicas de manta bordadas.',3,'786');
INSERT INTO `quiz_pista` VALUES (787,263,'No usan pulseras y collares.',7,'787');
INSERT INTO `quiz_pista` VALUES (788,263,'No usan sombrero, pañuelo y huaraches.',5,'788');
INSERT INTO `quiz_pista` VALUES (789,263,'No usan bufanda y guantes.',3,'789');
INSERT INTO `quiz_pista` VALUES (790,264,'No tiene 1 faceta.',7,'790');
INSERT INTO `quiz_pista` VALUES (791,264,'No tiene 2 facetas.',5,'791');
INSERT INTO `quiz_pista` VALUES (792,264,'No tiene 7 facetas.',3,'792');
INSERT INTO `quiz_pista` VALUES (793,265,'<i>Tayau</i> es Nuestro padre el Sol.',7,'793');
INSERT INTO `quiz_pista` VALUES (794,265,'<i>Tatei Yurienáka</i> es Nuestra madre tierra.',5,'794');
INSERT INTO `quiz_pista` VALUES (795,265,'<i>Tatei Kutsaraɨpa</i> es Nuestra madre agua sagrada.',3,'795');
INSERT INTO `quiz_pista` VALUES (796,266,'<i>Na’akarite</i> significa nopales.',7,'796');
INSERT INTO `quiz_pista` VALUES (797,266,'<i>Xata’ate</i> significa jícamas.',5,'797');
INSERT INTO `quiz_pista` VALUES (798,266,'<i>Túmatite</i> significa jitomates.',3,'798');
INSERT INTO `quiz_pista` VALUES (799,267,'Nopales no significa <i>ké’uxate</i>.',7,'799');
INSERT INTO `quiz_pista` VALUES (800,267,'Jitomates no significa <i>ké’uxate</i>.',5,'800');
INSERT INTO `quiz_pista` VALUES (801,267,'Jícamas no significa <i>ké’uxate</i>.',3,'801');
INSERT INTO `quiz_pista` VALUES (802,268,'<i>Ye’erite</i> significa camotes.',7,'802');
INSERT INTO `quiz_pista` VALUES (803,268,'<i>Haxite</i> significa guajes.',5,'803');
INSERT INTO `quiz_pista` VALUES (804,268,'<i>Xutsíte</i> significa calabacitas.',3,'804');
INSERT INTO `quiz_pista` VALUES (805,269,'Guajes no significa <i>xutsi hatsiyarite</i>.',7,'805');
INSERT INTO `quiz_pista` VALUES (806,269,'Calabacitas no significa <i>xutsi hatsiyarite</i>.',5,'806');
INSERT INTO `quiz_pista` VALUES (807,269,'Hongos no significa <i>xutsi hatsiyarite</i>.',3,'807');
INSERT INTO `quiz_pista` VALUES (808,270,'<i>Ha’a</i> significa agua.',7,'808');
INSERT INTO `quiz_pista` VALUES (809,270,'<i>Tsinari</i> significa chicuatol.',5,'809');
INSERT INTO `quiz_pista` VALUES (810,270,'<i>Nawá</i> significa tejuino.',3,'810');
INSERT INTO `quiz_pista` VALUES (811,271,'<i>Ikɨri paniyari</i> significa pan de elote.',7,'811');
INSERT INTO `quiz_pista` VALUES (812,271,'<i>Ruritse</i> significa dulce.',5,'812');
INSERT INTO `quiz_pista` VALUES (813,271,'<i>Pexúri</i> significa pinole.',3,'813');
INSERT INTO `quiz_pista` VALUES (814,272,'Una <i>xukúri</i> no es un sitio sagrado.',7,'814');
INSERT INTO `quiz_pista` VALUES (815,272,'Una <i>xukúri</i> no es un centro ceremonial.',5,'815');
INSERT INTO `quiz_pista` VALUES (816,272,'Una <i>xukúri</i> no es una artesanía Wixárika.',3,'816');
INSERT INTO `quiz_pista` VALUES (817,273,'No representa a lo masculino.',7,'817');
INSERT INTO `quiz_pista` VALUES (818,273,'No representa a la luna.',5,'818');
INSERT INTO `quiz_pista` VALUES (819,273,'No representa al sol.',3,'819');
INSERT INTO `quiz_pista` VALUES (820,274,'No simbolizan el día y la noche.',7,'820');
INSERT INTO `quiz_pista` VALUES (821,274,'No simbolizan depósito de tierra.',5,'821');
INSERT INTO `quiz_pista` VALUES (822,274,'No simbolizan lo bueno y lo malo.',3,'822');
INSERT INTO `quiz_pista` VALUES (823,275,'Durango no es un sitio sagrado.',7,'823');
INSERT INTO `quiz_pista` VALUES (824,275,'Canatlán no es un sitio sagrado.',5,'824');
INSERT INTO `quiz_pista` VALUES (825,275,'Pueblo nuevo no es un sitio sagrado.',3,'825');
INSERT INTO `quiz_pista` VALUES (826,276,'<i>Tatei Kutsaraɨpa</i> es Nuestra madre agua sagrada.',7,'826');
INSERT INTO `quiz_pista` VALUES (827,276,'<i>Tayau</i> es Nuestro padre el Sol.',5,'827');
INSERT INTO `quiz_pista` VALUES (828,276,'<i>Tatei Haramara</i> es Nuestra madre el mar.',3,'828');
INSERT INTO `quiz_pista` VALUES (829,277,'No es responsable de la abundancia del campo.',7,'829');
INSERT INTO `quiz_pista` VALUES (830,277,'No es responsable de las intensas lluvias.',5,'830');
INSERT INTO `quiz_pista` VALUES (831,277,'No es responsable de las sequías.',3,'831');
INSERT INTO `quiz_pista` VALUES (832,278,'No es representada con una veladora.',7,'832');
INSERT INTO `quiz_pista` VALUES (833,278,'No es representada con una vasija de porcelana',5,'833');
INSERT INTO `quiz_pista` VALUES (834,278,'No es representada con un ojo de dios.',3,'834');
INSERT INTO `quiz_pista` VALUES (835,279,'No celebra la temporada de lluvias.',7,'835');
INSERT INTO `quiz_pista` VALUES (836,279,'No celebra la recolección de hongos.',5,'836');
INSERT INTO `quiz_pista` VALUES (837,279,'No celebra el inicio de la primevera.',3,'837');
INSERT INTO `quiz_pista` VALUES (838,280,'No se preparan quesadillas y chicuatol.',7,'838');
INSERT INTO `quiz_pista` VALUES (839,280,'No se preparan pozole y agua.',5,'839');
INSERT INTO `quiz_pista` VALUES (840,280,'No se preparan pan y atole.',3,'840');
INSERT INTO `quiz_pista` VALUES (841,281,'Los agricultores no reciben los primeros frutos.',7,'841');
INSERT INTO `quiz_pista` VALUES (842,281,'Los perros y gatos no reciben los primeros frutos.',5,'842');
INSERT INTO `quiz_pista` VALUES (843,281,'El ganado no recibe los primeros frutos.',3,'843');
INSERT INTO `quiz_pista` VALUES (844,282,'<i>Tayau</i> no sobrevivió al gran diluvio.',7,'844');
INSERT INTO `quiz_pista` VALUES (845,282,'<i>Muwieri</i> no sobrevivió al gran diluvio.',5,'845');
INSERT INTO `quiz_pista` VALUES (846,282,'<i>Tatewari</i> no sobrevivió al gran diluvio.',3,'846');
INSERT INTO `quiz_pista` VALUES (847,283,'<i>Watakame</i> no llevó chocolates y dulces.',7,'847');
INSERT INTO `quiz_pista` VALUES (848,283,'<i>Watakame</i> no llevó maíz y frijoles.',5,'848');
INSERT INTO `quiz_pista` VALUES (849,283,'<i>Watakame</i> no llevó monedas de oro.',3,'849');
INSERT INTO `quiz_pista` VALUES (850,284,'<i>Watakame</i> no sobrevivió en bicicleta.',7,'850');
INSERT INTO `quiz_pista` VALUES (851,284,'<i>Watakame</i> no sobrevivió en tren.',5,'851');
INSERT INTO `quiz_pista` VALUES (852,284,'<i>Watakame</i> no sobrevivió en su carreta.',3,'852');
INSERT INTO `quiz_pista` VALUES (853,285,'<i>Ikú tuuxá</i> significa maíz blanco.',7,'853');
INSERT INTO `quiz_pista` VALUES (854,285,'<i>Ikú</i> significa maíz.',5,'854');
INSERT INTO `quiz_pista` VALUES (855,285,'<i>Ikɨri</i> significa elote.',3,'855');
INSERT INTO `quiz_pista` VALUES (856,286,'<i>Na’akari</i> significa nopal.',7,'856');
INSERT INTO `quiz_pista` VALUES (857,286,'<i>Aɨraxa</i> significa verdolaga.',5,'857');
INSERT INTO `quiz_pista` VALUES (858,286,'<i>Xútsi</i> significa calabacita.',3,'858');
INSERT INTO `quiz_pista` VALUES (859,287,'<i>Kukuríte</i> significa chiles.',7,'859');
INSERT INTO `quiz_pista` VALUES (860,287,'<i>Xata’ate</i> significa jícamas.',5,'860');
INSERT INTO `quiz_pista` VALUES (861,287,'<i>Túmatite</i> significa jitomates.',3,'861');
INSERT INTO `quiz_pista` VALUES (862,288,'Nopales no significa <i>yekwa’ate</i>.',7,'862');
INSERT INTO `quiz_pista` VALUES (863,288,'Calabacitas no significa <i>yekwa’ate</i>.',5,'863');
INSERT INTO `quiz_pista` VALUES (864,288,'Verdolagas no significa <i>yekwa’ate</i>.',3,'864');
INSERT INTO `quiz_pista` VALUES (865,289,'Quesadilla con <i>aɨraxate</i> significa quesadilla con verdolagas.',7,'865');
INSERT INTO `quiz_pista` VALUES (866,289,'<i>Yekwa’ate itsari</i> significa sopa de hongos.',5,'866');
INSERT INTO `quiz_pista` VALUES (867,289,'<i>Yekwa’ate</i> significa hongos.',3,'867');
INSERT INTO `quiz_pista` VALUES (868,290,'<i>Eɨkariti</i> significa eucalipto.',7,'868');
INSERT INTO `quiz_pista` VALUES (869,290,'<i>Uyuri</i> significa cebolla.',5,'869');
INSERT INTO `quiz_pista` VALUES (870,290,'<i>Orekanu</i> significa orégano.',3,'870');
INSERT INTO `quiz_pista` VALUES (871,291,'<i>Kutsira</i> significa machete.',7,'871');
INSERT INTO `quiz_pista` VALUES (872,291,'<i>Nawaxa</i> significa cuchillo.',5,'872');
INSERT INTO `quiz_pista` VALUES (873,291,'<i>Hatsaruni</i> significa azadón.',3,'873');
INSERT INTO `quiz_pista` VALUES (874,292,'Durango no es un sitio sagrado.',7,'874');
INSERT INTO `quiz_pista` VALUES (875,292,'Jalisco no es un sitio sagrado.',5,'875');
INSERT INTO `quiz_pista` VALUES (876,292,'Huejiquilla no es un sitio sagrado.',3,'876');
INSERT INTO `quiz_pista` VALUES (877,293,'<i>Mara’kame</i> significa chamán.',7,'877');
INSERT INTO `quiz_pista` VALUES (878,293,'<i>Xuku’uri ɨkame</i> significa jicarero.',5,'878');
INSERT INTO `quiz_pista` VALUES (879,293,'<i>Kawiteru</i> significa anciano sabio.',3,'879');
INSERT INTO `quiz_pista` VALUES (880,294,'Los policías no eligen a los gobernadores.',7,'880');
INSERT INTO `quiz_pista` VALUES (881,294,'Los chamanes no eligen a los gobernadores.',5,'881');
INSERT INTO `quiz_pista` VALUES (882,294,'El pueblo no elige a los gobernadores.',3,'882');
INSERT INTO `quiz_pista` VALUES (883,295,'La flauta y armónica no son instrumentos tradicionales <i>Wixárika</i>.',7,'883');
INSERT INTO `quiz_pista` VALUES (884,295,'El pequeño tambor y pequeño piano no son instrumentos tradicionales <i>Wixárika</i>.',5,'884');
INSERT INTO `quiz_pista` VALUES (885,295,'La marimba no son instrumentos tradicionales <i>Wixárika</i>.',3,'885');
INSERT INTO `quiz_pista` VALUES (886,296,'No son 7 las direcciones del universo.',7,'886');
INSERT INTO `quiz_pista` VALUES (887,296,'No son 3 las direcciones del universo.',5,'887');
INSERT INTO `quiz_pista` VALUES (888,296,'No son 4 las direcciones del universo.',3,'888');
INSERT INTO `quiz_pista` VALUES (889,297,'Las deidades masculinas no representan las direcciones del universo.',7,'889');
INSERT INTO `quiz_pista` VALUES (890,297,'Las deidades cazadoras no representan las direcciones del universo.',5,'890');
INSERT INTO `quiz_pista` VALUES (891,297,'Las deidades mensajeras no representan las direcciones del universo.',3,'891');
INSERT INTO `quiz_pista` VALUES (892,298,'<i>Ha’yewaxi</i> significa guayaba.',7,'892');
INSERT INTO `quiz_pista` VALUES (893,298,'<i>Ma’aku</i> significa mango.',5,'893');
INSERT INTO `quiz_pista` VALUES (894,298,'<i>Narakaxi</i> significa naranja.',3,'894');
INSERT INTO `quiz_pista` VALUES (895,299,'Mango no significa <i>ma’ara</i>.',7,'895');
INSERT INTO `quiz_pista` VALUES (896,299,'Guayaba no significa <i>ma’ara</i>.',5,'896');
INSERT INTO `quiz_pista` VALUES (897,299,'Naranja no significa <i>ma’ara</i>.',3,'897');
INSERT INTO `quiz_pista` VALUES (898,300,'<i>Ma’ara</i> significa pitahaya.',7,'898');
INSERT INTO `quiz_pista` VALUES (899,300,'<i>Kwarɨpa</i> significa ciruela.',5,'899');
INSERT INTO `quiz_pista` VALUES (900,300,'<i>Ma’aku</i> significa mango.',3,'900');
INSERT INTO `quiz_pista` VALUES (901,301,'Pitahaya no significa <i>ha’yewaxi</i>.',7,'901');
INSERT INTO `quiz_pista` VALUES (902,301,'Mango no significa <i>ha’yewaxi</i>.',5,'902');
INSERT INTO `quiz_pista` VALUES (903,301,'Ciruela no significa <i>ha’yewaxi</i>.',3,'903');
INSERT INTO `quiz_pista` VALUES (904,302,'<i>Itsari</i> significa caldo.',7,'904');
INSERT INTO `quiz_pista` VALUES (905,302,'<i>Xupaxi</i> significa chilaquiles.',5,'905');
INSERT INTO `quiz_pista` VALUES (906,302,'<i>kwitsari</i> significa pozole.',3,'906');
INSERT INTO `quiz_pista` VALUES (907,303,'Huejiquilla no es un sitio sagrado.',7,'907');
INSERT INTO `quiz_pista` VALUES (908,303,'<i>Mezquitic</i> no es un sitio sagrado.',5,'908');
INSERT INTO `quiz_pista` VALUES (909,303,'Jalisco no es un sitio sagrado.',3,'909');
INSERT INTO `quiz_pista` VALUES (910,304,'Jalisco no es un sitio sagrado.',7,'910');
INSERT INTO `quiz_pista` VALUES (911,304,'<i>Mezquitic</i> no es un sitio sagrado.',5,'911');
INSERT INTO `quiz_pista` VALUES (912,304,'Huejiquilla no es un sitio sagrado.',3,'912');
INSERT INTO `quiz_pista` VALUES (913,305,'Huejiquilla no es un sitio sagrado.',7,'913');
INSERT INTO `quiz_pista` VALUES (914,305,'Jalisco no es un sitio sagrado.',5,'914');
INSERT INTO `quiz_pista` VALUES (915,305,'<i>Mezquitic</i> no es un sitio sagrado.',3,'915');
INSERT INTO `quiz_pista` VALUES (916,306,'<i>Tayau</i> es Nuestro padre el Sol.',7,'916');
INSERT INTO `quiz_pista` VALUES (917,306,'<i>Naɨrɨ</i> es Dios del fuego primigenio.',5,'917');
INSERT INTO `quiz_pista` VALUES (918,306,'<i>Tatei Haramara</i> es Nuestra madre el mar.',3,'918');
INSERT INTO `quiz_pista` VALUES (919,307,'<i>Tsipúrawi</i> no es la deidad tutelar de los <i>mara’kate</i> (chamanes).',7,'919');
INSERT INTO `quiz_pista` VALUES (920,307,'<i>Tayau</i> no es la deidad tutelar de los <i>mara’kate</i> (chamanes).',5,'920');
INSERT INTO `quiz_pista` VALUES (921,307,'<i>Wewetsári</i> no es la deidad tutelar de los <i>mara’kate</i> (chamanes).',3,'921');
INSERT INTO `quiz_pista` VALUES (922,308,'<i>Tsipúrawi</i> no es considerado “el gran transformador”.',7,'922');
INSERT INTO `quiz_pista` VALUES (923,308,'<i>Wewetsári</i> no es considerado “el gran transformador”.',5,'923');
INSERT INTO `quiz_pista` VALUES (924,308,'<i>Tayau</i> no es considerado “el gran transformador”.',3,'924');
INSERT INTO `quiz_pista` VALUES (925,309,'En la fiesta del piano no se hace una peregrinación imaginaria a <i>Wirikuta</i>.',7,'925');
INSERT INTO `quiz_pista` VALUES (926,309,'En la celebración de los muertos no se hace una peregrinación imaginaria a <i>Wirikuta</i>.',5,'926');
INSERT INTO `quiz_pista` VALUES (927,309,'En la celebración de año nuevo no se hace una peregrinación imaginaria a <i>Wirikuta</i>.',3,'927');
INSERT INTO `quiz_pista` VALUES (928,310,'No se hace para invocar la lluvia.',7,'928');
INSERT INTO `quiz_pista` VALUES (929,310,'No se hace para que se asiente el corazón en los niños.',5,'929');
INSERT INTO `quiz_pista` VALUES (930,310,'No se hace para que haya buenas cosechas.',3,'930');
INSERT INTO `quiz_pista` VALUES (931,311,'No son convertidos en venados.',7,'931');
INSERT INTO `quiz_pista` VALUES (932,311,'No son convertidos en perros.',5,'932');
INSERT INTO `quiz_pista` VALUES (933,311,'No son convertidos en jabalíes.',3,'933');
INSERT INTO `quiz_pista` VALUES (934,312,'No ganan pastel de elote.',7,'934');
INSERT INTO `quiz_pista` VALUES (935,312,'No ganan atole de ciruela.',5,'935');
INSERT INTO `quiz_pista` VALUES (936,312,'No ganan pan de elote.',3,'936');
INSERT INTO `quiz_pista` VALUES (937,313,'La lluvia no era su madre.',7,'937');
INSERT INTO `quiz_pista` VALUES (938,313,'El agua no era su madre.',5,'938');
INSERT INTO `quiz_pista` VALUES (939,313,'La naturaleza no era su madre.',3,'939');
INSERT INTO `quiz_pista` VALUES (940,314,'No fue arrojado en <i>Wirikuta</i>.',7,'940');
INSERT INTO `quiz_pista` VALUES (941,314,'No fue arrojado en <i>Makuipa</i>.',5,'941');
INSERT INTO `quiz_pista` VALUES (942,314,'No fue arrojado en <i>Xapawiyemeta</i>.',3,'942');
INSERT INTO `quiz_pista` VALUES (943,315,'No se transformó en <i>Utútawi</i>.',7,'943');
INSERT INTO `quiz_pista` VALUES (944,315,'No se transformó en <i>Tatewari</i>.',5,'944');
INSERT INTO `quiz_pista` VALUES (945,315,'No se transformó en <i>Wewetsári</i>.',3,'945');
INSERT INTO `quiz_pista` VALUES (946,316,'<i>Ha’a</i> significa agua.',7,'946');
INSERT INTO `quiz_pista` VALUES (947,316,'<i>Mimierika</i> significa trueno.',5,'947');
INSERT INTO `quiz_pista` VALUES (948,316,'<i>Tái</i> significa fuego.',3,'948');
INSERT INTO `quiz_pista` VALUES (949,317,'<i>Taú</i> significa sol.',7,'949');
INSERT INTO `quiz_pista` VALUES (950,317,'<i>Mimierika</i> significa trueno.',5,'950');
INSERT INTO `quiz_pista` VALUES (951,317,'<i>Ha’a</i> significa agua.',3,'951');
INSERT INTO `quiz_pista` VALUES (952,318,'Los <i>unétsi</i> (bebés) no es la respuesta.',7,'952');
INSERT INTO `quiz_pista` VALUES (953,318,'Los <i>neɨkixiwi</i> (enemigos) no es la respuesta.',5,'953');
INSERT INTO `quiz_pista` VALUES (954,318,'Los <i>ne aurie muka</i> (vecinos) no es la respuesta.',3,'954');
INSERT INTO `quiz_pista` VALUES (955,319,'El zorrillo no robó el fuego.',7,'955');
INSERT INTO `quiz_pista` VALUES (956,319,'El armadillo no robó el fuego.',5,'956');
INSERT INTO `quiz_pista` VALUES (957,319,'El zorro no robó el fuego.',3,'957');
INSERT INTO `quiz_pista` VALUES (958,320,'No lo compartió con las deidades.',7,'958');
INSERT INTO `quiz_pista` VALUES (959,320,'No lo compartió con los animales.',5,'959');
INSERT INTO `quiz_pista` VALUES (960,320,'No lo compartió con los mestizos.',3,'960');
INSERT INTO `quiz_pista` VALUES (961,321,'No habita en <i>Hauxa Manaka</i>.',7,'961');
INSERT INTO `quiz_pista` VALUES (962,321,'No habita en <i>Xapawiyemeta</i>.',5,'962');
INSERT INTO `quiz_pista` VALUES (963,321,'No habita en <i>Makuipa</i>.',3,'963');
INSERT INTO `quiz_pista` VALUES (964,322,'<i>Wáxa</i> significa milpa.',7,'964');
INSERT INTO `quiz_pista` VALUES (965,322,'<i>Ha’yewaxi</i> significa guayaba.',5,'965');
INSERT INTO `quiz_pista` VALUES (966,322,'<i>Ma’ara</i> significa pitahaya.',3,'966');
INSERT INTO `quiz_pista` VALUES (967,323,'Guayaba no significa <i>kamaika</i>.',7,'967');
INSERT INTO `quiz_pista` VALUES (968,323,'Pitahaya no significa <i>kamaika</i>.',5,'968');
INSERT INTO `quiz_pista` VALUES (969,323,'Milpa no significa <i>kamaika</i>.',3,'969');
INSERT INTO `quiz_pista` VALUES (970,324,'<i>Túmati</i> significa jitomate.',7,'970');
INSERT INTO `quiz_pista` VALUES (971,324,'<i>Tsinakari</i> significa limón.',5,'971');
INSERT INTO `quiz_pista` VALUES (972,324,'<i>Xútsi</i> significa calabacita.',3,'972');
INSERT INTO `quiz_pista` VALUES (973,325,'Cabalacita no significa <i>uyuri</i>.',7,'973');
INSERT INTO `quiz_pista` VALUES (974,325,'Limón no significa <i>uyuri</i>.',5,'974');
INSERT INTO `quiz_pista` VALUES (975,325,'Jitomate no significa <i>uyuri</i>.',3,'975');
INSERT INTO `quiz_pista` VALUES (976,326,'<i>Xupaxi</i> significa sopa.',7,'976');
INSERT INTO `quiz_pista` VALUES (977,326,'<i>Tétsu</i> significa tamal.',5,'977');
INSERT INTO `quiz_pista` VALUES (978,326,'<i>Itsari</i> significa caldo.',3,'978');
INSERT INTO `quiz_pista` VALUES (979,327,'<i>Uwá hayaári</i> significa jugo de caña.',7,'979');
INSERT INTO `quiz_pista` VALUES (980,327,'<i>Hayaári</i> significa jugo.',5,'980');
INSERT INTO `quiz_pista` VALUES (981,327,'<i>Narakaxi hayaári</i> significa jugo de naranja.',3,'981');
INSERT INTO `quiz_pista` VALUES (982,328,'<i>Kɨrapu</i> significa clavo.',7,'982');
INSERT INTO `quiz_pista` VALUES (983,328,'<i>Orekanu</i> significa orégano.',5,'983');
INSERT INTO `quiz_pista` VALUES (984,328,'<i>Mantsaniya</i> significa manzanilla.',3,'984');
INSERT INTO `quiz_pista` VALUES (985,329,'Bolaños no es un sitio sagrado.',7,'985');
INSERT INTO `quiz_pista` VALUES (986,329,'<i>Mezquitic</i> no es un sitio sagrado.',5,'986');
INSERT INTO `quiz_pista` VALUES (987,329,'Huejiquilla no es un sitio sagrado.',3,'987');
INSERT INTO `quiz_pista` VALUES (988,330,'El tlacuache no es un animal mensajero.',7,'988');
INSERT INTO `quiz_pista` VALUES (989,330,'El zorrillo no es un animal mensajero.',5,'989');
INSERT INTO `quiz_pista` VALUES (990,330,'El gato no es un animal mensajero.',3,'990');
INSERT INTO `quiz_pista` VALUES (991,331,'No visten pantalón, playera y zapatos.',7,'991');
INSERT INTO `quiz_pista` VALUES (992,331,'No visten short, camisa y huraches.',5,'992');
INSERT INTO `quiz_pista` VALUES (993,331,'No visten vestido y huaraches.',3,'993');
INSERT INTO `quiz_pista` VALUES (994,332,'No utilizan gorro, bufanda y guantes.',7,'994');
INSERT INTO `quiz_pista` VALUES (995,332,'No utilizan diademas y listones.',5,'995');
INSERT INTO `quiz_pista` VALUES (996,332,'No utilizan relojes y anillos.',3,'996');
INSERT INTO `quiz_pista` VALUES (997,333,'No utilizan mochila.',7,'997');
INSERT INTO `quiz_pista` VALUES (998,333,'No utilizan bufanda.',5,'998');
INSERT INTO `quiz_pista` VALUES (999,333,'No utilizan gorro.',3,'999');
INSERT INTO `quiz_pista` VALUES (1000,334,'<i>Yekwa</i> significa champiñón.',7,'1000');
INSERT INTO `quiz_pista` VALUES (1001,334,'<i>Na’akari</i> significa nopal.',5,'1001');
INSERT INTO `quiz_pista` VALUES (1002,334,'<i>Aɨraxa</i> significa verdolaga.',3,'1002');
INSERT INTO `quiz_pista` VALUES (1003,335,'Champiñón no significa <i>xútsi</i>.',7,'1003');
INSERT INTO `quiz_pista` VALUES (1004,335,'Verdolaga no significa <i>xútsi</i>.',5,'1004');
INSERT INTO `quiz_pista` VALUES (1005,335,'Nopal no significa <i>xútsi</i>.',3,'1005');
INSERT INTO `quiz_pista` VALUES (1006,336,'<i>Túmati</i> significa jitomate.',7,'1006');
INSERT INTO `quiz_pista` VALUES (1007,336,'<i>Xa’ata</i> significa jícama.',5,'1007');
INSERT INTO `quiz_pista` VALUES (1008,336,'<i>Haxi</i> significa guaje.',3,'1008');
INSERT INTO `quiz_pista` VALUES (1009,337,'Jícama no significa <i>tsinakari</i>.',7,'1009');
INSERT INTO `quiz_pista` VALUES (1010,337,'Guaje no significa <i>tsinakari</i>.',5,'1010');
INSERT INTO `quiz_pista` VALUES (1011,337,'Jitomate no significa <i>tsinakari</i>.',3,'1011');
INSERT INTO `quiz_pista` VALUES (1012,338,'<i>Tsinari</i> significa chicuatol.',7,'1012');
INSERT INTO `quiz_pista` VALUES (1013,338,'<i>Retsi</i> significa leche.',5,'1013');
INSERT INTO `quiz_pista` VALUES (1014,338,'<i>Hayaári</i> significa jugo.',3,'1014');
INSERT INTO `quiz_pista` VALUES (1015,339,'Una <i>ɨ’rɨ</i> no es un centro ceremonial.',7,'1015');
INSERT INTO `quiz_pista` VALUES (1016,339,'Una <i>ɨ’rɨ</i> no es una artesanía Wixárika.',5,'1016');
INSERT INTO `quiz_pista` VALUES (1017,339,'Una <i>ɨ’rɨ</i> no es un sitio sagrado.',3,'1017');
INSERT INTO `quiz_pista` VALUES (1018,340,'No representa la noche.',7,'1018');
INSERT INTO `quiz_pista` VALUES (1019,340,'No representa el día.',5,'1019');
INSERT INTO `quiz_pista` VALUES (1020,340,'No representa lo femenino.',3,'1020');
INSERT INTO `quiz_pista` VALUES (1021,341,'No se utilizan para adornos.',7,'1021');
INSERT INTO `quiz_pista` VALUES (1022,341,'No se utilizan para mensajes.',5,'1022');
INSERT INTO `quiz_pista` VALUES (1023,341,'No se utilizan para decoración.',3,'1023');
INSERT INTO `quiz_pista` VALUES (1024,342,'No se utilizan para decoración.',7,'1024');
INSERT INTO `quiz_pista` VALUES (1025,342,'No se utilizan para adornos.',5,'1025');
INSERT INTO `quiz_pista` VALUES (1026,342,'No se utilizan para mensajes.',3,'1026');
INSERT INTO `quiz_pista` VALUES (1027,343,'<i>Mezquitic</i> no es un sitio sagrado.',7,'1027');
INSERT INTO `quiz_pista` VALUES (1028,343,'Chapala no es un sitio sagrado.',5,'1028');
INSERT INTO `quiz_pista` VALUES (1029,343,'Bolaños no es un sitio sagrado.',3,'1029');
INSERT INTO `quiz_pista` VALUES (1030,344,'<i>Tatei Haramara</i> es Nuestra madre el mar.',7,'1030');
INSERT INTO `quiz_pista` VALUES (1031,344,'<i>Tatei Yurienáka</i> es Nuestra madre tierra.',5,'1031');
INSERT INTO `quiz_pista` VALUES (1032,344,'<i>Tatei Kutsaraɨpa</i> es Nuestra madre agua sagrada.',3,'1032');
INSERT INTO `quiz_pista` VALUES (1033,345,'No elaboran máscaras, capas y centros.',7,'1033');
INSERT INTO `quiz_pista` VALUES (1034,345,'No elaboran arcos y flechas.',5,'1034');
INSERT INTO `quiz_pista` VALUES (1035,345,'No elaboran tablillas y jicaras.',3,'1035');
INSERT INTO `quiz_pista` VALUES (1036,346,'No se realizan en la casa del abuelo paterno del niño o niña.',7,'1036');
INSERT INTO `quiz_pista` VALUES (1037,346,'No se realizan en el tukipa (centro ceremonial) de origen del abuelo paterno del niño o niña.',5,'1037');
INSERT INTO `quiz_pista` VALUES (1038,346,'No se realizan en la casa del abuelo materno del niño o niña.',3,'1038');
INSERT INTO `quiz_pista` VALUES (1039,347,'No es porque regalan tambores.',7,'1039');
INSERT INTO `quiz_pista` VALUES (1040,347,'No es por los primeros frutos.',5,'1040');
INSERT INTO `quiz_pista` VALUES (1041,347,'No es por la despedida de las lluvias.',3,'1041');
INSERT INTO `quiz_pista` VALUES (1042,348,'No son 9 las diosas del agua.',7,'1042');
INSERT INTO `quiz_pista` VALUES (1043,348,'No son 2 las diosas del agua.',5,'1043');
INSERT INTO `quiz_pista` VALUES (1044,348,'No son 6 las diosas del agua.',3,'1044');
INSERT INTO `quiz_pista` VALUES (1045,349,'<i>Taku</i> significa taco.',7,'1045');
INSERT INTO `quiz_pista` VALUES (1046,349,'<i>Pexuri</i> significa mole.',5,'1046');
INSERT INTO `quiz_pista` VALUES (1047,349,'<i>kwitsari</i> significa pozole.',3,'1047');
INSERT INTO `quiz_pista` VALUES (1048,350,'<i>Untsa</i> significa lince.',7,'1048');
INSERT INTO `quiz_pista` VALUES (1049,350,'<i>Kauxai</i> significa zorro.',5,'1049');
INSERT INTO `quiz_pista` VALUES (1050,350,'<i>Yáavi</i> significa coyote.',3,'1050');
INSERT INTO `quiz_pista` VALUES (1051,351,'<i>Hikuri</i> significa peyote.',7,'1051');
INSERT INTO `quiz_pista` VALUES (1052,351,'<i>Ɨpá</i> significa tepehuaje.',5,'1052');
INSERT INTO `quiz_pista` VALUES (1053,351,'<i>Kɨrapu</i> significa clavo.',3,'1053');
INSERT INTO `quiz_pista` VALUES (1054,352,'<i>Imúmui</i> significa escalera.',7,'1054');
INSERT INTO `quiz_pista` VALUES (1055,352,'<i>Ha’tsa</i> significa hacha.',5,'1055');
INSERT INTO `quiz_pista` VALUES (1056,352,'<i>Tɨɨpi</i> significa arco.',3,'1056');
INSERT INTO `quiz_pista` VALUES (1057,353,'<i>Tupiri</i> significa policía.',7,'1057');
INSERT INTO `quiz_pista` VALUES (1058,353,'<i>Tatuwani</i> significa gobernador.',5,'1058');
INSERT INTO `quiz_pista` VALUES (1059,353,'<i>Kawiteru</i> significa anciano sabio.',3,'1059');
INSERT INTO `quiz_pista` VALUES (1060,354,'El jicarerono se encarga de recolectar el peyote.',7,'1060');
INSERT INTO `quiz_pista` VALUES (1061,534,'El chamán no se encarga de recolectar el peyote.',5,'1061');
INSERT INTO `quiz_pista` VALUES (1062,354,'El anciano sabio no se encarga de recolectar el peyote.',3,'1062');
INSERT INTO `quiz_pista` VALUES (1063,355,'No se honra a los difuntos con veladoras, comida y música.',7,'1063');
INSERT INTO `quiz_pista` VALUES (1064,355,'No se honra a los difuntos con velorio, incineración y entierro.',5,'1064');
INSERT INTO `quiz_pista` VALUES (1065,355,'No se honra a los difuntos con abrazos, oraciones y bailes.',3,'1065');
INSERT INTO `quiz_pista` VALUES (1066,356,'<i>Irikixa</i> no es la ceremonia para despedir el alma.',7,'1066');
INSERT INTO `quiz_pista` VALUES (1067,356,'<i>Yuimakwaxa</i> no es la ceremonia para despedir el alma.',5,'1067');
INSERT INTO `quiz_pista` VALUES (1068,356,'<i>Maríxa</i> no es la ceremonia para despedir el alma.',3,'1068');
INSERT INTO `quiz_pista` VALUES (1069,357,'No significa invocar a las sombras.',7,'1069');
INSERT INTO `quiz_pista` VALUES (1070,357,'No significa llamar al más allá.',5,'1070');
INSERT INTO `quiz_pista` VALUES (1071,357,'No significa llamar al inframundo.',3,'1071');
INSERT INTO `quiz_pista` VALUES (1072,358,'No es la fiesta para dar la bienvenidad a los parientes.',7,'1072');
INSERT INTO `quiz_pista` VALUES (1073,358,'No es la ceremonia de bienvenida al inframundo.',5,'1073');
INSERT INTO `quiz_pista` VALUES (1074,358,'No es la fiesta de los difuntos.',3,'1074');
INSERT INTO `quiz_pista` VALUES (1075,359,'<i>Neɨkixiwima</i> significa enemigos.',7,'1075');
INSERT INTO `quiz_pista` VALUES (1076,359,'<i>Ne aurie</i> muka significa vecino.',5,'1076');
INSERT INTO `quiz_pista` VALUES (1077,359,'<i>Yeikame</i> significa viajero.',3,'1077');
INSERT INTO `quiz_pista` VALUES (1078,360,'No visita a sus parientes.',7,'1078');
INSERT INTO `quiz_pista` VALUES (1079,360,'No visita el cielo.',5,'1079');
INSERT INTO `quiz_pista` VALUES (1080,360,'No recorre el inframundo.',3,'1080');
INSERT INTO `quiz_pista` VALUES (1081,361,'No debe darle bolillo.',7,'1081');
INSERT INTO `quiz_pista` VALUES (1082,361,'No debe darle huesos.',5,'1082');
INSERT INTO `quiz_pista` VALUES (1083,361,'No debe darle croquetas.',3,'1083');
INSERT INTO `quiz_pista` VALUES (1084,362,'No se ahogan.',7,'1084');
INSERT INTO `quiz_pista` VALUES (1085,362,'No los golpean los animales.',5,'1085');
INSERT INTO `quiz_pista` VALUES (1086,362,'No se queman.',3,'1086');
INSERT INTO `quiz_pista` VALUES (1087,363,'<i>Kexiu</i> significa queso.',7,'1087');
INSERT INTO `quiz_pista` VALUES (1088,363,'<i>Kɨxaɨ</i> significa tostada.',5,'1088');
INSERT INTO `quiz_pista` VALUES (1089,363,'<i>Tsuirá</i> significa gordita.',3,'1089');
INSERT INTO `quiz_pista` VALUES (1090,364,'<i>Wakana</i> significa pollo.',7,'1090');
INSERT INTO `quiz_pista` VALUES (1091,364,'<i>Weurai</i> significa güilota.',5,'1091');
INSERT INTO `quiz_pista` VALUES (1092,364,'<i>Kwatsa</i> significa cuervo.',3,'1092');
INSERT INTO `quiz_pista` VALUES (1093,365,'El cuervo no es un animal espiritual.',7,'1093');
INSERT INTO `quiz_pista` VALUES (1094,365,'El pollo no es un animal espiritual.',5,'1094');
INSERT INTO `quiz_pista` VALUES (1095,365,'La güilota no es un animal espiritual.',3,'1095');
INSERT INTO `quiz_pista` VALUES (1096,366,'No llama a las deidades.',7,'1096');
INSERT INTO `quiz_pista` VALUES (1097,366,'No llama a las sombras del inframundo.',5,'1097');
INSERT INTO `quiz_pista` VALUES (1098,366,'No llama a los familiares.',3,'1098');
INSERT INTO `quiz_pista` VALUES (1099,367,'No oran por el alma del difunto.',7,'1099');
INSERT INTO `quiz_pista` VALUES (1100,367,'No lo esperan con comida, música y regalos.',5,'1100');
INSERT INTO `quiz_pista` VALUES (1101,367,'No bailan por el descanso del difunto.',3,'1101');
INSERT INTO `quiz_pista` VALUES (1102,368,'No es un centro ceremonial.',7,'1102');
INSERT INTO `quiz_pista` VALUES (1103,368,'No es un templo.',5,'1103');
INSERT INTO `quiz_pista` VALUES (1104,368,'No es una pequeña iglesia.',3,'1104');
INSERT INTO `quiz_pista` VALUES (1105,369,'<i>Mitsu</i> significa gato.',7,'1105');
INSERT INTO `quiz_pista` VALUES (1106,369,'<i>Ɨxawe</i> significa lobo.',5,'1106');
INSERT INTO `quiz_pista` VALUES (1107,369,'<i>Untsa</i> significa lince.',3,'1107');
INSERT INTO `quiz_pista` VALUES (1108,370,'Las personas que no cuidan a su perro negro no van al infierno.',7,'1108');
INSERT INTO `quiz_pista` VALUES (1109,370,'Los perros negros no ayudan a cruzar el río.',5,'1109');
INSERT INTO `quiz_pista` VALUES (1110,370,'Las personas que no cuidan a su perro negro no van al inframundo.',3,'1110');
INSERT INTO `quiz_pista` VALUES (1111,371,'Los perros negros no abrazan a las personas cuando mueren.',7,'1111');
INSERT INTO `quiz_pista` VALUES (1112,371,'Los perros negros no juegan con las personas cuando mueren.',5,'1112');
INSERT INTO `quiz_pista` VALUES (1113,371,'Todas las personas pasan por el inframundo cuando mueren.',3,'1113');
INSERT INTO `quiz_pista` VALUES (1114,372,'<i>Eɨkariti</i> significa eucalipto.',7,'1114');
INSERT INTO `quiz_pista` VALUES (1115,372,'<i>Ɨrawe emɨtimariwe</i> significa gordolobo.',5,'1115');
INSERT INTO `quiz_pista` VALUES (1116,372,'<i>Uxa</i> significa planta para pintar el rostro.',3,'1116');
INSERT INTO `quiz_pista` VALUES (1117,373,'No es el chupa almas.',7,'1117');
INSERT INTO `quiz_pista` VALUES (1118,373,'No es el roba sangre.',5,'1118');
INSERT INTO `quiz_pista` VALUES (1119,373,'No es el roba almas.',3,'1119');
INSERT INTO `quiz_pista` VALUES (1120,374,'No representa al inframundo.',7,'1120');
INSERT INTO `quiz_pista` VALUES (1121,374,'No representa a la vida.',5,'1121');
INSERT INTO `quiz_pista` VALUES (1122,374,'No representa a la noche.',3,'1122');
INSERT INTO `quiz_pista` VALUES (1123,375,'No hace que las personas pierdan la razón.',7,'1123');
INSERT INTO `quiz_pista` VALUES (1124,375,'No hace que las personas encuentren su camino.',5,'1124');
INSERT INTO `quiz_pista` VALUES (1125,375,'No hace que las personas vayan al inframundo.',3,'1125');
INSERT INTO `quiz_pista` VALUES (1126,376,'No se presenta como rata.',7,'1126');
INSERT INTO `quiz_pista` VALUES (1127,376,'No se presenta como hiena.',5,'1127');
INSERT INTO `quiz_pista` VALUES (1128,376,'No se presenta como búho.',3,'1128');
INSERT INTO `quiz_pista` VALUES (1129,377,'Las deidades no rigen la sociedad de los vivos.',7,'1129');
INSERT INTO `quiz_pista` VALUES (1130,377,'Gobernadores y polícias no rigen la sociedad de los vivos.',5,'1130');
INSERT INTO `quiz_pista` VALUES (1131,377,'Ancianos sabios y chamanes no rigen la sociedad de los vivos.',3,'1131');
INSERT INTO `quiz_pista` VALUES (1132,378,'No son castigados porque no peregrinan.',7,'1132');
INSERT INTO `quiz_pista` VALUES (1133,378,'No son castigados porque no hacen sus oraciones.',5,'1133');
INSERT INTO `quiz_pista` VALUES (1134,378,'No son castigados porque no hacen ofrendas a las deidades.',3,'1134');
INSERT INTO `quiz_pista` VALUES (1135,379,'No son castigados por los hombres.',7,'1135');
INSERT INTO `quiz_pista` VALUES (1136,379,'No son castigados por los polícias.',5,'1136');
INSERT INTO `quiz_pista` VALUES (1137,379,'No son castigados por las sombras.',3,'1137');
INSERT INTO `quiz_pista` VALUES (1138,380,'No son castigados con cárcel.',7,'1138');
INSERT INTO `quiz_pista` VALUES (1139,380,'No son castigados con falta de dinero y trabajo.',5,'1139');
INSERT INTO `quiz_pista` VALUES (1140,380,'No son castigados con accidentes.',3,'1140');
INSERT INTO `quiz_pista` VALUES (1141,381,'El peor castigo no es que su alma no llegue al cielo.',7,'1141');
INSERT INTO `quiz_pista` VALUES (1142,381,'El peor castigo no es que su alma no esté con su familia.',5,'1142');
INSERT INTO `quiz_pista` VALUES (1143,381,'El peor castigo no es que su alma no encuentre el camino.',3,'1143');
INSERT INTO `quiz_pista` VALUES (1144,382,'No son enterrados en las escuelas.',7,'1144');
INSERT INTO `quiz_pista` VALUES (1145,382,'No son enterrados cerca de los campos de flores.',5,'1145');
INSERT INTO `quiz_pista` VALUES (1146,382,'No son enterrados en el cementerio.',3,'1146');
INSERT INTO `quiz_pista` VALUES (1147,383,'No se les ofrece una canción.',7,'1147');
INSERT INTO `quiz_pista` VALUES (1148,383,'No se les ofrece leche y ropa.',5,'1148');
INSERT INTO `quiz_pista` VALUES (1149,383,'No se les ofrece un baño.',3,'1149');
INSERT INTO `quiz_pista` VALUES (1150,384,'No van al cielo.',7,'1150');
INSERT INTO `quiz_pista` VALUES (1151,384,'No van al purgatorio.',5,'1151');
INSERT INTO `quiz_pista` VALUES (1152,384,'No siguen formando parte de la comunidad.',3,'1152');
INSERT INTO `quiz_pista` VALUES (1153,385,'No se comunica con tepehuaje.',7,'1153');
INSERT INTO `quiz_pista` VALUES (1154,385,'No se comunica con tomillo.',5,'1154');
INSERT INTO `quiz_pista` VALUES (1155,385,'No se comunica con cactus.',3,'1155');
INSERT INTO `quiz_pista` VALUES (1156,386,'No se comunica con un ojo de dios.',7,'1156');
INSERT INTO `quiz_pista` VALUES (1157,386,'No se comunica con una visión.',5,'1157');
INSERT INTO `quiz_pista` VALUES (1158,386,'No se comunica con sus pensamientos.',3,'1158');
INSERT INTO `quiz_pista` VALUES (1159,387,'No se llora con el difunto.',7,'1159');
INSERT INTO `quiz_pista` VALUES (1160,387,'No se habla con el difunto.',5,'1160');
INSERT INTO `quiz_pista` VALUES (1161,387,'No se pelea con el difunto.',3,'1161');
INSERT INTO `quiz_pista` VALUES (1162,388,'Venado no significa <i>tuixuyeutanaka</i>.',7,'1162');
INSERT INTO `quiz_pista` VALUES (1163,388,'Iguana no significa <i>tuixuyeutanaka</i>.',5,'1163');
INSERT INTO `quiz_pista` VALUES (1164,388,'Cerdo no significa <i>tuixuyeutanaka</i>.',3,'1164');
INSERT INTO `quiz_pista` VALUES (1165,389,'Jabalí no significa <i>maxa</i>.',7,'1165');
INSERT INTO `quiz_pista` VALUES (1166,389,'Puma no significa <i>maxa</i>.',5,'1166');
INSERT INTO `quiz_pista` VALUES (1167,389,'Iguana no significa <i>maxa</i>.',3,'1167');
INSERT INTO `quiz_pista` VALUES (1168,390,'Iguana no significa <i>máye</i>.',7,'1168');
INSERT INTO `quiz_pista` VALUES (1169,390,'Jabalí no significa <i>máye</i>.',5,'1169');
INSERT INTO `quiz_pista` VALUES (1170,390,'Venado no significa <i>máye</i>.',3,'1170');
INSERT INTO `quiz_pista` VALUES (1171,391,'Abeja no significa <i>tuuká</i>.',7,'1171');
INSERT INTO `quiz_pista` VALUES (1172,391,'Alacrán no significa <i>tuuká</i>.',5,'1172');
INSERT INTO `quiz_pista` VALUES (1173,391,'Oruga no significa <i>tuuká</i>.',3,'1173');
INSERT INTO `quiz_pista` VALUES (1174,392,'Alacrán no significa <i>kúu</i>.',7,'1174');
INSERT INTO `quiz_pista` VALUES (1175,392,'Abeja no significa <i>kúu</i>.',5,'1175');
INSERT INTO `quiz_pista` VALUES (1176,392,'Iguana no significa <i>kúu</i>.',3,'1176');
INSERT INTO `quiz_pista` VALUES (1177,393,'Iguana no significa <i>ketsɨ</i>.',7,'1177');
INSERT INTO `quiz_pista` VALUES (1178,393,'Serpiente no significa <i>ketsɨ</i>.',5,'1178');
INSERT INTO `quiz_pista` VALUES (1179,393,'Abeja no significa <i>ketsɨ</i>.',3,'1179');
INSERT INTO `quiz_pista` VALUES (1180,394,'Venado no significa <i>yáavi</i>.',7,'1180');
INSERT INTO `quiz_pista` VALUES (1181,394,'Puma no significa <i>yáavi</i>.',5,'1181');
INSERT INTO `quiz_pista` VALUES (1182,394,'Jabalí no significa <i>yáavi</i>.',3,'1182');
INSERT INTO `quiz_pista` VALUES (1183,395,'Serpiente no significa <i>xiete</i>.',7,'1183');
INSERT INTO `quiz_pista` VALUES (1184,395,'Araña no significa <i>xiete</i>.',5,'1184');
INSERT INTO `quiz_pista` VALUES (1185,395,'Alacrán no significa <i>xiete</i>.',3,'1185');
INSERT INTO `quiz_pista` VALUES (1186,396,'Coyote no significa <i>kauxai</i>.',7,'1186');
INSERT INTO `quiz_pista` VALUES (1187,396,'Puma no significa <i>kauxai</i>.',5,'1187');
INSERT INTO `quiz_pista` VALUES (1188,396,'Venado no significa <i>kauxai</i>.',3,'1188');
INSERT INTO `quiz_pista` VALUES (1189,397,'Conejo no significa <i>tekɨ</i>.',7,'1189');
INSERT INTO `quiz_pista` VALUES (1190,397,'Araña no significa <i>tekɨ</i>.',5,'1190');
INSERT INTO `quiz_pista` VALUES (1191,397,'Abeja no significa <i>tekɨ</i>.',3,'1191');
INSERT INTO `quiz_pista` VALUES (1192,398,'Araña no significa <i>teruka</i>.',7,'1192');
INSERT INTO `quiz_pista` VALUES (1193,398,'Serpiente no significa <i>teruka</i>.',5,'1193');
INSERT INTO `quiz_pista` VALUES (1194,398,'Abeja no significa <i>teruka</i>.',3,'1194');
INSERT INTO `quiz_pista` VALUES (1195,399,'Halcón no significa <i>mikɨri</i>.',7,'1195');
INSERT INTO `quiz_pista` VALUES (1196,399,'Águila no significa <i>mikɨri</i>.',5,'1196');
INSERT INTO `quiz_pista` VALUES (1197,399,'Cuervo no significa <i>mikɨri</i>.',3,'1197');
INSERT INTO `quiz_pista` VALUES (1198,400,'Abeja no significa <i>ke’etsé</i>.',7,'1198');
INSERT INTO `quiz_pista` VALUES (1199,400,'Araña no significa <i>ke’etsé</i>.',5,'1199');
INSERT INTO `quiz_pista` VALUES (1200,400,'Serpiente no significa <i>ke’etsé</i>.',3,'1200');
INSERT INTO `quiz_pista` VALUES (1201,401,'Serpiente no significa <i>ha’axi</i>.',7,'1201');
INSERT INTO `quiz_pista` VALUES (1202,401,'Iguana no significa <i>ha’axi</i>.',5,'1202');
INSERT INTO `quiz_pista` VALUES (1203,401,'Lagartija no significa <i>ha’axi</i>.',3,'1203');
INSERT INTO `quiz_pista` VALUES (1204,402,'Águila no significa <i>weurai</i>.',7,'1204');
INSERT INTO `quiz_pista` VALUES (1205,402,'Lechuza no significa <i>weurai</i>.',5,'1205');
INSERT INTO `quiz_pista` VALUES (1206,402,'Pollo no significa <i>weurai</i>.',3,'1206');
INSERT INTO `quiz_pista` VALUES (1207,403,'Iguana no significa <i>ɨkwi</i>.',7,'1207');
INSERT INTO `quiz_pista` VALUES (1208,403,'Salamandra no significa <i>ɨkwi</i>.',5,'1208');
INSERT INTO `quiz_pista` VALUES (1209,403,'Serpiente no significa <i>ɨkwi</i>.',3,'1209');
INSERT INTO `quiz_pista` VALUES (1210,404,'Águila no significa <i>wakana</i>.',7,'1210');
INSERT INTO `quiz_pista` VALUES (1211,404,'Güilota no significa <i>wakana</i>.',5,'1211');
INSERT INTO `quiz_pista` VALUES (1212,404,'Lechuza no significa <i>wakana</i>.',3,'1212');
INSERT INTO `quiz_pista` VALUES (1213,405,'Vaca no significa <i>tuixu</i>.',7,'1213');
INSERT INTO `quiz_pista` VALUES (1214,405,'Jabalí no significa <i>tuixu</i>.',5,'1214');
INSERT INTO `quiz_pista` VALUES (1215,405,'Pollo no significa <i>tuixu</i>.',3,'1215');
INSERT INTO `quiz_pista` VALUES (1216,406,'Perro no significa <i>tátsiu</i>.',7,'1216');
INSERT INTO `quiz_pista` VALUES (1217,406,'Gato no significa <i>tátsiu</i>.',5,'1217');
INSERT INTO `quiz_pista` VALUES (1218,406,'Ardilla no significa <i>tátsiu</i>.',3,'1218');
INSERT INTO `quiz_pista` VALUES (1219,407,'Cerdono significa <i>wakaxi</i>.',7,'1219');
INSERT INTO `quiz_pista` VALUES (1220,407,'Oveja no significa <i>wakaxi</i>.',5,'1220');
INSERT INTO `quiz_pista` VALUES (1221,407,'Pollo no significa <i>wakaxi</i>.',3,'1221');
INSERT INTO `quiz_pista` VALUES (1222,408,'Pollo no significa <i>werika</i>.',7,'1222');
INSERT INTO `quiz_pista` VALUES (1223,408,'Halcón no significa <i>werika</i>.',5,'1223');
INSERT INTO `quiz_pista` VALUES (1224,408,'Güilota no significa <i>werika</i>.',3,'1224');
INSERT INTO `quiz_pista` VALUES (1225,409,'Iguana no significa <i>haikɨ</i>.',7,'1225');
INSERT INTO `quiz_pista` VALUES (1226,409,'Salamandra no significa <i>haikɨ</i>.',5,'1226');
INSERT INTO `quiz_pista` VALUES (1227,409,'Lagartija no significa <i>haikɨ</i>.',3,'1227');
INSERT INTO `quiz_pista` VALUES (1228,410,'Perro no significa <i>ɨxawe</i>.',7,'1228');
INSERT INTO `quiz_pista` VALUES (1229,410,'Puma no significa <i>ɨxawe</i>.',5,'1229');
INSERT INTO `quiz_pista` VALUES (1230,410,'Coyote no significa <i>ɨxawe</i>.',3,'1230');
INSERT INTO `quiz_pista` VALUES (1231,411,'Águila no significa <i>wirɨkɨ</i>.',7,'1231');
INSERT INTO `quiz_pista` VALUES (1232,411,'Cuervo no significa <i>wirɨkɨ</i>.',5,'1232');
INSERT INTO `quiz_pista` VALUES (1233,411,'Lechuza no significa <i>wirɨkɨ</i>.',3,'1233');
INSERT INTO `quiz_pista` VALUES (1234,412,'<i>Haramara</i> no está en La Yesca.',7,'1234');
INSERT INTO `quiz_pista` VALUES (1235,412,'<i>Haramara</i> no está en Tepic.',5,'1235');
INSERT INTO `quiz_pista` VALUES (1236,412,'<i>Haramara</i> no está en Tuxpan.',3,'1236');
INSERT INTO `quiz_pista` VALUES (1237,413,'<i>Taikairiya</i> no es la roca blanca de Haramara.',7,'1237');
INSERT INTO `quiz_pista` VALUES (1238,413,'<i>Ha’utete</i> no es la roca blanca de Haramara.',5,'1238');
INSERT INTO `quiz_pista` VALUES (1239,413,'<i>Ku’unita</i> no es la roca blanca de Haramara.',3,'1239');
INSERT INTO `quiz_pista` VALUES (1240,414,'<i>Wirikuta</i> no está en Santo Domingo.',7,'1240');
INSERT INTO `quiz_pista` VALUES (1241,414,'<i>Wirikuta</i> no está en Charcas.',5,'1241');
INSERT INTO `quiz_pista` VALUES (1242,414,'<i>Wirikuta</i> no está en Villa de Ramos.',3,'1242');
INSERT INTO `quiz_pista` VALUES (1243,415,'La creación del mundo no ocurrió en <i>Hauxa Manaka</i>.',7,'1243');
INSERT INTO `quiz_pista` VALUES (1244,415,'La creación del mundo no ocurrió en <i>Xapawiyemeta</i>.',5,'1244');
INSERT INTO `quiz_pista` VALUES (1245,415,'La creación del mundo no ocurrió en <i>Haramara</i>.',3,'1245');
INSERT INTO `quiz_pista` VALUES (1246,416,'El sol no se levanta por <i>Haramara</i>.',7,'1246');
INSERT INTO `quiz_pista` VALUES (1247,416,'El sol no se levanta por <i>Hauxa Manaka</i>.',5,'1247');
INSERT INTO `quiz_pista` VALUES (1248,416,'El sol no se levanta por <i>Xapawiyemeta</i>.',3,'1248');
INSERT INTO `quiz_pista` VALUES (1249,417,'<i>Hauxa Manaka</i> no está en Mezquital.',7,'1249');
INSERT INTO `quiz_pista` VALUES (1250,417,'<i>Hauxa Manaka</i> no está en Poanas.',5,'1250');
INSERT INTO `quiz_pista` VALUES (1251,417,'<i>Hauxa Manaka</i> no está en Suchíl.',3,'1251');
INSERT INTO `quiz_pista` VALUES (1252,418,'<i>Xapawiyeme</i> no es la casa de <i>Tututzi Maxa Kwaxi</i>.',7,'1252');
INSERT INTO `quiz_pista` VALUES (1253,418,'<i>Te’ekata</i> no es la casa de <i>Tututzi Maxa Kwaxi</i>.',5,'1253');
INSERT INTO `quiz_pista` VALUES (1254,418,'<i>Wirikuta</i> no es la casa de <i>Tututzi Maxa Kwaxi</i>.',3,'1254');
INSERT INTO `quiz_pista` VALUES (1255,419,'<i>Te’ekata</i> no está en Tequila, Jalisco.',7,'1255');
INSERT INTO `quiz_pista` VALUES (1256,419,'<i>Te’ekata</i> no está en Guadalajara, Jalisco.',5,'1256');
INSERT INTO `quiz_pista` VALUES (1257,419,'<i>Te’ekata</i> no está en Bolaños, Jalisco.',3,'1257');
INSERT INTO `quiz_pista` VALUES (1258,420,'<i>Wirikuta</i> no es el santuario de <i>Tatewari.',7,'1258');
INSERT INTO `quiz_pista` VALUES (1259,420,'<i>Haramara</i> no es el santuario de <i>Tatewari.',5,'1259');
INSERT INTO `quiz_pista` VALUES (1260,420,'<i>Hauxa Manaka</i> no es el santuario de <i>Tatewari.',3,'1260');
INSERT INTO `quiz_pista` VALUES (1261,421,'<i>Xapawiyemeta</i> no tuvo lugar la gesta universal.',7,'1261');
INSERT INTO `quiz_pista` VALUES (1262,421,'<i>Haramara</i> no tuvo lugar la gesta universal.',5,'1262');
INSERT INTO `quiz_pista` VALUES (1263,421,'<i>Hauxa Manaka</i> no tuvo lugar la gesta universal.',3,'1263');
INSERT INTO `quiz_pista` VALUES (1264,422,'<i>Wirikuta</i> no es el centro del universo.',7,'1264');
INSERT INTO `quiz_pista` VALUES (1265,422,'<i>Xapawiyemeta</i> no es el centro del universo.',5,'1265');
INSERT INTO `quiz_pista` VALUES (1266,422,'<i>Wirikuta</i> no es el centro del universo.',3,'1266');
INSERT INTO `quiz_pista` VALUES (1267,423,'En <i>Hauxa Manaka</i> no arrojaron a un niño enfermo al fuego.',7,'1267');
INSERT INTO `quiz_pista` VALUES (1268,423,'En <i>Te’ekata</i> no arrojaron a un niño enfermo al fuego.',5,'1268');
INSERT INTO `quiz_pista` VALUES (1269,423,'En <i>Xapawiyemeta</i> no arrojaron a un niño enfermo al fuego.',3,'1269');
INSERT INTO `quiz_pista` VALUES (1270,424,'<i>Xapawiyemeta</i> no está en Tequila.',7,'1270');
INSERT INTO `quiz_pista` VALUES (1271,424,'<i>Xapawiyemeta</i> no está en el cerro de los alacranes, en Bolaños.',5,'1271');
INSERT INTO `quiz_pista` VALUES (1272,424,'<i>Xapawiyemeta</i> no está en Villa Guerrero.',3,'1272');
INSERT INTO `quiz_pista` VALUES (1273,425,'No representa el fuego y el agua.',7,'1273');
INSERT INTO `quiz_pista` VALUES (1274,425,'No representa la lucha de la luz contra la obscuridad.',5,'1274');
INSERT INTO `quiz_pista` VALUES (1275,425,'No representa el sol y la luna.',3,'1275');
INSERT INTO `quiz_pista` VALUES (1276,426,'<i>Utútawi</i> no luchó contra seres inframundanos.',7,'1276');
INSERT INTO `quiz_pista` VALUES (1277,426,'<i>Wewetsári</i> no luchó contra seres inframundanos.',5,'1277');
INSERT INTO `quiz_pista` VALUES (1278,426,'<i>Tututáka Pitsitéka</i> no luchó contra seres inframundanos.',3,'1278');
INSERT INTO `quiz_pista` VALUES (1279,427,'No salió por <i>Te’ekata</i>.',7,'1279');
INSERT INTO `quiz_pista` VALUES (1280,427,'No salió por <i>Wirikuta</i>.',5,'1280');
INSERT INTO `quiz_pista` VALUES (1281,427,'No salió por <i>Haramara</i>.',3,'1281');
INSERT INTO `quiz_pista` VALUES (1282,428,'No deben realizarse para tener fiestas en la comunidad.',7,'1282');
INSERT INTO `quiz_pista` VALUES (1283,428,'No deben realizarse para vender artesanías.',5,'1283');
INSERT INTO `quiz_pista` VALUES (1284,428,'No deben realizarse para recibir la primavera.',3,'1284');
INSERT INTO `quiz_pista` VALUES (1285,429,'<i>Tatei Xapawiyeme</i> no tiene 5 facetas.',7,'1285');
INSERT INTO `quiz_pista` VALUES (1286,429,'<i>Tamatzi Paritsika</i> no tiene 5 facetas.',5,'1286');
INSERT INTO `quiz_pista` VALUES (1287,429,'<i>Tatei Wexica Wimari</i> no tiene 5 facetas.',3,'1287');
INSERT INTO `quiz_pista` VALUES (1288,430,'La playa, mar, lagos, ríos, campos, cerros y montañas no son sitios sagrados.',7,'1288');
INSERT INTO `quiz_pista` VALUES (1289,430,'Los centros ceremoniales no son sitios sagrados.',5,'1289');
INSERT INTO `quiz_pista` VALUES (1290,430,'Existen cinco sitios sagrados.',3,'1290');
INSERT INTO `quiz_pista` VALUES (1291,431,'El corazón, alma y espíritu no son las cinco direcciones.',7,'1291');
INSERT INTO `quiz_pista` VALUES (1292,431,'Existen cinco direcciones <i>Wixárika</i>.',5,'1292');
INSERT INTO `quiz_pista` VALUES (1293,431,'Existen cinco direcciones <i>Wixárika</i>.',3,'1293');
INSERT INTO `quiz_pista` VALUES (1294,432,'<i>Tatewari, Tayau, Takutsi Nakawé, Naɨrɨ y T’kákame<i> no son cazadores <i>Wixárika</i>.',7,'1294');
INSERT INTO `quiz_pista` VALUES (1295,432,'Norte, sur, este, oeste y centro no son cazadores <i>Wixárika</i>.',5,'1295');
INSERT INTO `quiz_pista` VALUES (1296,432,'Existen cinco cazadores.',3,'1296');
INSERT INTO `quiz_pista` VALUES (1297,433,'No se deposita en una olla.',7,'1297');
INSERT INTO `quiz_pista` VALUES (1298,433,'No se deposita en un vaso.',5,'1298');
INSERT INTO `quiz_pista` VALUES (1299,443,'No se deposita en una tablilla.',3,'1299');
INSERT INTO `quiz_pista` VALUES (1300,434,'<i>Kaitsa</i> significa maracas.',7,'1300');
INSERT INTO `quiz_pista` VALUES (1301,434,'<i>Xaweri</i> significa violín.',5,'1301');
INSERT INTO `quiz_pista` VALUES (1302,434,'<i>Kanari</i> significa guitarra.',3,'1302');
INSERT INTO `quiz_pista` VALUES (1303,435,'<i>Kanarite</i> significa instrumentos.',7,'1303');
INSERT INTO `quiz_pista` VALUES (1304,435,'<i>Kaitsa</i> significa maracas.',5,'1304');
INSERT INTO `quiz_pista` VALUES (1305,435,'<i>Kanari</i> significa guitarra.',3,'1305');
INSERT INTO `quiz_pista` VALUES (1306,436,'<i>Xaweri</i> significa violín.',7,'1306');
INSERT INTO `quiz_pista` VALUES (1307,436,'<i>Kaitsa</i> significa maracas.',5,'1307');
INSERT INTO `quiz_pista` VALUES (1308,436,'<i>Kanarite</i> significa instrumentos.',3,'1308');
INSERT INTO `quiz_pista` VALUES (1309,437,'<i>Kanari</i> significa guitarra.',7,'1309');
INSERT INTO `quiz_pista` VALUES (1310,437,'<i>Kanarite</i> significa instrumentos.',5,'1310');
INSERT INTO `quiz_pista` VALUES (1311,437,'<i>Xaweri</i> significa violín.',3,'1311');
INSERT INTO `quiz_pista` VALUES (1312,438,'Un <i>tukipa</i> no es un tipo de casa.',7,'1312');
INSERT INTO `quiz_pista` VALUES (1313,438,'Un <i>tukipa</i> no es un salón de eventos.',5,'1313');
INSERT INTO `quiz_pista` VALUES (1314,438,'Un <i>tukipa</i> no es una bodega tradicional.',3,'1314');
INSERT INTO `quiz_pista` VALUES (1315,439,'El templo mayor y templos tradicionales no integran el <i>tukipa</i>.',7,'1315');
INSERT INTO `quiz_pista` VALUES (1316,439,'<i>Haita y ku’unita</i> no integran el <i>tukipa</i>.',5,'1316');
INSERT INTO `quiz_pista` VALUES (1317,439,'Los templos <i>Wixárika</i> no integran el <i>tukipa</i>.',3,'1317');
INSERT INTO `quiz_pista` VALUES (1318,440,'Las serpientes no dependen de la diosa de la lluvia.',7,'1318');
INSERT INTO `quiz_pista` VALUES (1319,440,'Las serpientes no dependen de las madres del maíz.',5,'1319');
INSERT INTO `quiz_pista` VALUES (1320,440,'Las serpientes no dependen de todas las deidades.',3,'1320');
INSERT INTO `quiz_pista` VALUES (1321,441,'No se frota para que se envenene.',7,'1321');
INSERT INTO `quiz_pista` VALUES (1322,441,'No se frota para que dé más leche.',5,'1322');
INSERT INTO `quiz_pista` VALUES (1323,441,'No se frota para que tenga manchas.',3,'1323');
INSERT INTO `quiz_pista` VALUES (1324,442,'No conceden la habilidad de bailar.',7,'1324');
INSERT INTO `quiz_pista` VALUES (1325,442,'No conceden la habilidad de sembrar.',5,'1325');
INSERT INTO `quiz_pista` VALUES (1326,442,'No conceden la habilidad de cocinar.',3,'1326');
INSERT INTO `quiz_pista` VALUES (1327,443,'No se utiliza para aderezar platillos.',7,'1327');
INSERT INTO `quiz_pista` VALUES (1328,443,'No se utiliza para pintar las casas.',5,'1328');
INSERT INTO `quiz_pista` VALUES (1329,443,'No se utiliza para comer.',3,'1329');
INSERT INTO `vestimentas` VALUES (1,'ACCESORIO','Arete','Aretes','Nakɨtsa ','Nakɨtsate',1,200,5,1.0,1.0,100.0,'Images/Wixarika/Vestimentas/Arete 1',NULL);
INSERT INTO `vestimentas` VALUES (2,'PRENDA','Camisa','Camisas','Kamixa','Kamixate',1,500,10,1.0,1.0,100.0,'Images/Wixarika/Vestimentas/Camisa',NULL);
INSERT INTO `vestimentas` VALUES (3,'ACCESORIO','Capa','Capas','Tuwaxa','Tuwaxate',1,300,50,1.0,1.0,100.0,'Images/Wixarika/Vestimentas/Capa',NULL);
INSERT INTO `vestimentas` VALUES (4,'ACCESORIO','Collar','Collares','Kuka tɨwame','Kuka tiwameté',1,200,5,1.0,1.0,100.0,'Images/Wixarika/Vestimentas/Collar 1',NULL);
INSERT INTO `vestimentas` VALUES (5,'ACCESORIO','Cinturón','Cinturones','Hɨiyame','Hɨiyamete',1,300,10,1.0,1.0,100.0,'Images/Wixarika/Vestimentas/Cinturon',NULL);
INSERT INTO `vestimentas` VALUES (6,'PRENDA','Falda','Faldas','Íwi','Iwíte',1,500,10,1.0,1.0,100.0,'Images/Wixarika/Vestimentas/Falda',NULL);
INSERT INTO `vestimentas` VALUES (7,'HUARACHE_HOMBRE','Huarache','Huaraches','Kakaí','Kakaíte',1,300,10,1.2,1.0,100.0,'Images/Wixarika/Vestimentas/Huarache hombre',NULL);
INSERT INTO `vestimentas` VALUES (8,'MORRAL','Morral ','Morrales','Kɨtsiɨri','Kɨtsiɨrite',1,500,10,1.0,1.0,100.0,'Images/Wixarika/Vestimentas/Morral',NULL);
INSERT INTO `vestimentas` VALUES (9,'PRENDA','Pantalón','Pantalones','Xaweruxi','Xaweruxite',1,500,10,1.0,1.0,100.0,'Images/Wixarika/Vestimentas/Pantalón',NULL);
INSERT INTO `vestimentas` VALUES (10,'ACCESORIO','Pañuelo','Pañuelos','Tuwaxa','Tuwaxate',1,200,5,1.0,1.0,100.0,'Images/Wixarika/Vestimentas/Pañuelo',NULL);
INSERT INTO `vestimentas` VALUES (11,'ACCESORIO','Pulsera','Pulseras','Matsɨwa','Matsiwate',1,200,10,1.0,1.0,100.0,'Images/Wixarika/Vestimentas/Pulsera',NULL);
INSERT INTO `vestimentas` VALUES (12,'SOMBRERO','Sombrero','Sombreros','Xupureru','Xupurerute',1,1000,20,1.0,1.0,100.0,'Images/Wixarika/Vestimentas/Sombreros/Sombrero 1',NULL);
INSERT INTO `vestimentas` VALUES (13,'TRAJE_HOMBRE','Traje de hombre','Trajes de hombre','Kemari de Hombre','Kemarite de Hombre',2,1200,70,1.0,1.0,100.0,'Images/Wixarika/Vestimentas/Traje de hombre',NULL);
INSERT INTO `vestimentas` VALUES (14,'SOMBRERO','Sombrero','Sombreros','Xupureru','Xupurerute',2,1000,30,1.0,1.0,100.0,'Images/Wixarika/Vestimentas/Sombreros/Sombrero 2',NULL);
INSERT INTO `vestimentas` VALUES (15,'HUARACHE_HOMBRE','Huarache','Huaraches','Kakaí','Kakaíte',2,400,10,1.5,1.5,100.0,'Images/Wixarika/Vestimentas/Huarache hombre',NULL);
INSERT INTO `vestimentas` VALUES (16,'TRAJE_HOMBRE','Traje de hombre','Trajes de hombre','Kemari de Hombre','Kemarite de Hombre',3,1500,100,1.0,1.0,100.0,'Images/Wixarika/Vestimentas/Traje de hombre',NULL);
INSERT INTO `vestimentas` VALUES (17,'HUARACHE_HOMBRE','Huarache','Huaraches','Kakaíte','Kakaíte',3,500,10,2.0,1.5,100.0,'Images/Wixarika/Vestimentas/Huarache hombre',NULL);
INSERT INTO `vestimentas` VALUES (18,'SOMBRERO','Sombrero','Sombreros','Xupureru','Xupurerute',3,1000,40,1.0,1.0,100.0,'Images/Wixarika/Vestimentas/Sombreros/Sombrero 3',NULL);
INSERT INTO `vestimentas` VALUES (19,'TRAJE_HOMBRE','Traje de hombre','Trajes de hombre','Kemari de Hombre','Kemarite de Hombre',1,1000,50,1.0,1.0,100.0,'Images/Wixarika/Vestimentas/Traje de hombre',NULL);
INSERT INTO `vestimentas` VALUES (20,'TRAJE_HOMBRE','Sin traje','Sin traje',NULL,NULL,1,0,0,1.0,1.0,100.0,'Images/Wixarika/Vestimentas/Traje de hombre',NULL);
INSERT INTO `vestimentas` VALUES (21,'TRAJE_HOMBRE','Traje de hombre','Trajes de hombre','Kemari de Hombre','Kemarite de Hombre',4,1700,130,1.0,1.0,100.0,'Images/Wixarika/Vestimentas/Traje de hombre',NULL);
INSERT INTO `vestimentas` VALUES (22,'TRAJE_HOMBRE','Traje de hombre','Trajes de hombre','Kemari de Hombre','Kemarite de Hombre',5,2000,150,1.0,1.0,100.0,'Images/Wixarika/Vestimentas/Traje de hombre',NULL);
INSERT INTO `vestimentas` VALUES (23,'TRAJE_HOMBRE','Traje de hombre','Trajes de hombre','Kemari de Hombre','Kemarite de Hombre',6,2500,180,1.0,1.0,100.0,'Images/Wixarika/Vestimentas/Traje de hombre',NULL);
INSERT INTO `vestimentas` VALUES (24,'SOMBRERO','Sombrero','Sombreros','Xupureru','Xupurerute',4,1000,50,1.0,1.0,100.0,'Images/Wixarika/Vestimentas/Sombreros/Sombrero 4',NULL);
INSERT INTO `vestimentas` VALUES (25,'SOMBRERO','Sombrero','Sombreros','Xupureru','Xupurerute',5,1000,60,1.0,1.0,100.0,'Images/Wixarika/Vestimentas/Sombreros/Sombrero 5',NULL);
INSERT INTO `vestimentas` VALUES (26,'SOMBRERO','Sombrero','Sombreros','Xupureru','Xupurerute',6,1000,70,1.0,1.0,100.0,'Images/Wixarika/Vestimentas/Sombreros/Sombrero 6',NULL);
INSERT INTO `vestimentas` VALUES (27,'HUARACHE_HOMBRE','Huarache','Huaraches','Kakaí','Kakaíte',4,600,10,2.2,1.7,100.0,'Images/Wixarika/Vestimentas/Huarache hombre',NULL);
INSERT INTO `vestimentas` VALUES (28,'HUARACHE_HOMBRE','Huarache','Huaraches','Kakaí','Kakaíte',5,700,10,2.3,1.7,100.0,'Images/Wixarika/Vestimentas/Huarache hombre',NULL);
INSERT INTO `vestimentas` VALUES (29,'HUARACHE_HOMBRE','Huarache','Huaraches','Kakaí','Kakaíte',6,800,10,2.5,2.0,100.0,'Images/Wixarika/Vestimentas/Huarache hombre',NULL);
INSERT INTO `enemigos` VALUES (1,'Jefe 1',100,1000,-5.0,0.0);
INSERT INTO `enemigos` VALUES (2,'Jefe 2',200,1000,-6.0,0.0);
INSERT INTO `enemigos` VALUES (3,'Jefe 3',300,1000,-7.0,0.0);
INSERT INTO `enemigos` VALUES (4,'Jefe 4',400,1000,-8.0,0.0);
INSERT INTO `enemigos` VALUES (5,'Jefe 5',600,1000,-9.0,0.0);
INSERT INTO `enemigos` VALUES (6,'Jefe 6',700,1000,-10.0,0.0);
INSERT INTO `enemigos` VALUES (7,'Jefe 7',800,1000,-10.0,0.0);
INSERT INTO `enemigos` VALUES (8,'Demonio 1',10,100,-5.0,0.0);
INSERT INTO `enemigos` VALUES (9,'Sombra',10,100,-5.0,0.0);
INSERT INTO `animales_enfermedades` VALUES (1,1);
INSERT INTO `animales_enfermedades` VALUES (4,2);
INSERT INTO `animales_enfermedades` VALUES (5,3);
INSERT INTO `animales_enfermedades` VALUES (14,4);
INSERT INTO `animales_enfermedades` VALUES (16,5);
INSERT INTO `animales_enfermedades` VALUES (23,6);
INSERT INTO `animales_enfermedades` VALUES (28,7);
INSERT INTO `animales_enfermedades` VALUES (34,8);
INSERT INTO `animales_enfermedades` VALUES (36,9);
INSERT INTO `animales_enfermedades` VALUES (41,10);
INSERT INTO `animales_enfermedades` VALUES (43,11);
INSERT INTO `curaciones_enfermedades` VALUES (1,1);
INSERT INTO `curaciones_enfermedades` VALUES (2,2);
INSERT INTO `curaciones_enfermedades` VALUES (3,3);
INSERT INTO `curaciones_enfermedades` VALUES (4,4);
INSERT INTO `curaciones_enfermedades` VALUES (5,5);
INSERT INTO `curaciones_enfermedades` VALUES (6,6);
INSERT INTO `curaciones_enfermedades` VALUES (7,7);
INSERT INTO `curaciones_enfermedades` VALUES (8,8);
INSERT INTO `curaciones_enfermedades` VALUES (9,9);
INSERT INTO `curaciones_enfermedades` VALUES (10,10);
INSERT INTO `curaciones_enfermedades` VALUES (11,11);
INSERT INTO `curaciones_enfermedades` VALUES (12,12);
INSERT INTO `curaciones_enfermedades` VALUES (13,13);
INSERT INTO `curaciones_enfermedades` VALUES (14,14);
INSERT INTO `animales` VALUES (1,'ENEMIGO','Abeja','Abejas','Xiete','Xietexi',1,100,-1.0,0.0,0,'Images/Wixarika/Enfermedades/Piquete de abeja');
INSERT INTO `animales` VALUES (2,'ESPIRITUAL','Águila','Águilas','Kwixɨ','Kwixɨri',1,0,0.0,0.0,0,'Images/Wixarika/Animales/Aguila/Águila 2');
INSERT INTO `animales` VALUES (3,'ESPIRITUAL','Águila real','Águilas reales','Werika','Werikaxi',1,0,0.0,0.0,0,'Images/Wixarika/Animales/Aguila Real/Águila real 7');
INSERT INTO `animales` VALUES (4,'ENEMIGO','Alacrán','Alacranes','Teruka',NULL,1,100,-2.0,0.0,0,'Images/Wixarika/Animales/Alacran/Alacran 1');
INSERT INTO `animales` VALUES (5,'ENEMIGO','Araña','Arañas','Tuuká','Tuukatsi',1,100,-2.0,0.0,0,'Images/Wixarika/Animales/Araña/Araña f3');
INSERT INTO `animales` VALUES (6,'ENEMIGO','Ardilla','Ardillas','Tekɨ','Tekɨri',1,100,0.0,0.0,1,'Images/Wixarika/Animales/Ardilla/Ardilla 1');
INSERT INTO `animales` VALUES (7,'OTRO','Armadillo','Armadillos','Xɨye','Xɨyetsi',1,0,0.0,0.0,0,'Images/Wixarika/Animales/Ardilla/Ardilla 1');
INSERT INTO `animales` VALUES (8,'ENEMIGO','Bagre','Bagres','Ketsɨ','Ketsɨte',1,100,0.0,0.0,1,'Images/Wixarika/Animales/Pez/Pez 9');
INSERT INTO `animales` VALUES (9,'OTRO','Caballo','Caballos','Caballo','Caballos',1,100,0.0,0.0,0,'Images/Wixarika/Enemigos/Caballo');
INSERT INTO `animales` VALUES (10,'ESPIRITUAL','Camaleón cornudo','Camaleónes cornudos','Téka','Tekaxi',10,350,-10.0,0.0,0,'Images/Wixarika/Enemigos/Camaleón cornudo');
INSERT INTO `animales` VALUES (11,'ENEMIGO','Cangrejo','Cangrejos','Rharhapai',NULL,1,100,0.0,0.0,0,'');
INSERT INTO `animales` VALUES (12,'ENEMIGO','Caracol','Caracoles','Curupo',NULL,1,100,0.0,0.0,0,'Images/Wixarika/Vegetacion/Plantas de Agua/Caracol 1x');
INSERT INTO `animales` VALUES (13,'ENEMIGO','Cerdo','Cerdos','Tuixu','Tuixuri',3,200,-5.0,0.0,1,'Images/Wixarika/Animales/Cerdo/Cerdo 1');
INSERT INTO `animales` VALUES (14,'ENEMIGO','Cocodrilo','Cocodrilos','Ha’axi','Haxitsi',8,200,-10.0,0.0,0,'Images/Wixarika/Animales/Cocodrilo/Cocodrilo 1 x1');
INSERT INTO `animales` VALUES (15,'ENEMIGO','Conejo','Conejos','Tátsiu','Tatsiurixi',2,100,0.0,0.0,1,'Images/Wixarika/Animales/Conejo/Conejo 1');
INSERT INTO `animales` VALUES (16,'ENEMIGO','Coyote','Coyotes','Yáavi','Yáavixi',8,300,-5.0,0.0,0,'Images/Wixarika/Animales/Coyote/Coyote 1');
INSERT INTO `animales` VALUES (17,'OTRO','Cuervo','Cuervos','Kwatsa','Kwatsári',1,0,0.0,0.0,0,'Images/Wixarika/Animales/Cuervo/Cuervo');
INSERT INTO `animales` VALUES (18,'OTRO','Gato','Gatos','Mitsu','Mitsu yeutanaka',1,0,0.0,0.0,0,'Images/Wixarika/Enemigos/Gato');
INSERT INTO `animales` VALUES (19,'ENEMIGO','Gato montés','Gatos montés','Mitsu yeutanaka','Mitsuri yeutari',10,350,-10.0,0.0,0,'Images/Wixarika/Enemigos/Gato');
INSERT INTO `animales` VALUES (20,'ENEMIGO','Güilota','Güilotas','Weurai','Weuraixi',1,150,0.0,0.0,1,'Images/Wixarika/Animales/Güilota/Guilota 6');
INSERT INTO `animales` VALUES (21,'ESPIRITUAL','Halcón','Halcónes','Witse','Witseri',1,0,0.0,0.0,0,'Images/Wixarika/Animales/Halcón/Halcón');
INSERT INTO `animales` VALUES (22,'ENEMIGO','Iguana','Iguanas','Ke´etsé','Ketse´ete',1,100,0.0,0.0,1,'Images/Wixarika/Animales/Aguila/Águila 2');
INSERT INTO `animales` VALUES (23,'ENEMIGO','Jabalí','Jabalís','Tuixuyeutanaka','Tuixuriyeutari',4,200,-3.0,0.0,1,'Images/Wixarika/Animales/Jabali/Jabalí 1 x1');
INSERT INTO `animales` VALUES (24,'ENEMIGO','Jaguar','Jaguares','Tɨwe','Tɨwexi',10,350,-10.0,0.0,0,'Images/Wixarika/Animales/Lince/Puma 1');
INSERT INTO `animales` VALUES (25,'ESPIRITUAL','Lagartija','Lagartijas','Ɨkwi','Ɨkwixi',1,0,0.0,0.0,0,'Images/Wixarika/Iconos/Lagartija');
INSERT INTO `animales` VALUES (26,'MENSAJERO','Lechuza','Lechuzas','Miikɨiri','Mikɨri',1,0,0.0,0.0,0,'Images/Wixarika/Animales/Buho/Lechuza 1');
INSERT INTO `animales` VALUES (27,'ENEMIGO','Lince','Linces','Untsa','Untsari',10,350,-10.0,0.0,0,'Images/Wixarika/Animales/Lince/Puma 1');
INSERT INTO `animales` VALUES (28,'ENEMIGO','Lobo','Lobos','Ɨrawe','Ɨrawetsixi',8,300,-5.0,0.0,0,'Images/Wixarika/Animales/Lobo/Lobo 1');
INSERT INTO `animales` VALUES (29,'OTRO','Oveja','Ovejas',NULL,NULL,1,0,0.0,0.0,0,'Images/Wixarika/Animales/Oveja/Oveja 1 x1');
INSERT INTO `animales` VALUES (30,'MENSAJERO','Pájaro tipo mini tecolote','Pájaros tipo mini tecolotes','Peexá','Peexátsi',1,0,0.0,0.0,0,'Images/Wixarika/Enemigos/Pájaro tipo mini tecolote');
INSERT INTO `animales` VALUES (31,'OTRO','Perro','Perros','Tsɨkɨ','Tsɨikɨri',1,0,0.0,0.0,0,'Images/Wixarika/Enemigos/Perro');
INSERT INTO `animales` VALUES (32,'OTRO','Pez','Peces','Ketsɨ','Ketsíte',1,100,0.0,0.0,1,'Images/Wixarika/Animales/Pez/Pez 9');
INSERT INTO `animales` VALUES (33,'ENEMIGO','Pollo','Pollos','Wakana','Wakanari',1,200,0.0,0.0,1,'Images/Wixarika/Animales/Gallina/Gallina 1 x1');
INSERT INTO `animales` VALUES (34,'ENEMIGO','Puma','Pumas','Máye','Máye',10,350,-10.0,0.0,0,'Images/Wixarika/Animales/Lince/Puma 1');
INSERT INTO `animales` VALUES (36,'ENEMIGO','Serpiente','Serpientes','Kúu','Kuterixi',1,200,-5.0,0.0,0,'Images/Wixarika/Animales/Serpiente/Serpiente 1');
INSERT INTO `animales` VALUES (37,'ESPIRITUAL','Serpiente azul','Serpientes azules','Haikɨ','Haikɨxi',1,0,0.0,0.0,0,'Images/Wixarika/Animales/Serpiente/Serpiente 1');
INSERT INTO `animales` VALUES (38,'ESPIRITUAL','Serpiente cascabel','Serpientes de cascabel','Xáye','Xayetsi',1,0,0.0,0.0,0,'Images/Wixarika/Animales/Serpiente/Serpiente 1');
INSERT INTO `animales` VALUES (39,'MENSAJERO','Tecolote','Tecolotes','Miikɨiri','Mikɨri',1,0,0.0,0.0,0,'Images/Wixarika/Animales/Buho/Lechuza NPC 7');
INSERT INTO `animales` VALUES (40,'OTRO','Vaca','Vacas','Wakaxi','Wakaitsixi',1,0,0.0,0.0,0,'Images/Wixarika/Animales/Vaca/Vaca 5 x1');
INSERT INTO `animales` VALUES (41,'ENEMIGO','Venado','Venados','Maxa','Maxatsi',8,400,-8.0,0.0,1,'Images/Wixarika/Animales/Venado/Venado 1');
INSERT INTO `animales` VALUES (42,'ESPIRITUAL','Zopilote','Zopilotes','Wirɨkɨ','Wirɨkɨxɨ',1,0,0.0,0.0,0,'Images/Wixarika/Animales/Zopilote/Zopilote 4');
INSERT INTO `animales` VALUES (43,'ENEMIGO','Zorro','Zorros','Kauxai','Kauxaitsi',3,200,-5.0,0.0,0,'Images/Wixarika/Animales/Zorro/Zorro 1');
INSERT INTO `animales` VALUES (44,'ENEMIGO','Murcielago','Murcielagos',NULL,NULL,1,100,-5.0,0.0,0,'Images/Wixarika/Animales/Murcielago/Murcielago 1');
INSERT INTO `usuarios` VALUES (1,1,1,1,'DEMO',NULL,NULL,NULL,'M','México','Nayarit','Tepic','Centro','Centro','Centro','1','A','default_H',1);
INSERT INTO `_Configuracion` VALUES (1,1,0.08541197,0.01225168,0.6569754);
INSERT INTO `enfermedades` VALUES (1,'PIQUETE','Piquete de abeja','Piquetes de abeja','Xiete keiyari','Xietexi wa keiyari',-12.0,-4.0,'Images/Wixarika/Enfermedades/Piquete de abeja','Te picó una <i>xiete</i> (abeja).
Ve a la tienda del <i>mara’kame</i> (chamán) para curarte.','1,2');
INSERT INTO `enfermedades` VALUES (2,'PIQUETE','Piquete de alacrán','Piquetes de alacrán','Alacrán','Alacranes',-12.0,-4.0,'Images/Wixarika/Enfermedades/Piquete','Te picó un alacrán.
Ve a la tienda del <i>mara’kame</i> (chamán) para mejorar tu barra de vida.','3,4');
INSERT INTO `enfermedades` VALUES (3,'PIQUETE','Piquete de araña','Piquetes de araña','Tuuká','Tuukatsi',-12.0,-5.0,'Images/Wixarika/Enfermedades/Mordedura','Te picó una <i>tuuká</i> (araña).
Ve a la tienda del <i>mara’kame</i> (chamán) para curarte.','5,6');
INSERT INTO `enfermedades` VALUES (4,'MORDIDA','Mordida de cocodrilo','Mordidas de cocodrilo','Ha’axi','Haxitsi',-12.0,-5.0,'Images/Wixarika/Enfermedades/Mordedura','Te mordió un <i>ha’axi</i> (cocodrilo).
Ve a la tienda del <i>mara’kame</i> (chamán) para mejorar tu barra de vida.','7,8');
INSERT INTO `enfermedades` VALUES (5,'MORDIDA','Mordida coyote','Mordidas coyote','Coyote','Coyotes',-12.0,-5.0,'Images/Wixarika/Enfermedades/Mordedura','Te mordió un coyote.
Ve a la tienda del <i>mara’kame</i> (chamán) para curarte.','9,10');
INSERT INTO `enfermedades` VALUES (6,'MORDIDA','Mordida de jabalí','Mordidas de jabalí','Tuixuyeutanaka','Tuixuriyeutari',-12.0,-5.0,'Images/Wixarika/Enfermedades/Mordedura','Te mordió un <i>tuixuyeutanaka</i> (jabalí).
Ve a la tienda del <i>mara’kame</i> (chamán) para mejorar tu barra de vida.','11,12');
INSERT INTO `enfermedades` VALUES (7,'MORDIDA','Mordida de lobo','Mordidas de lobo','Ɨrawe','Ɨrawe',-12.0,-5.0,'Images/Wixarika/Enfermedades/Mordedura','Te mordió un <i>ɨrawe</i> (lobo).
Ve a la tienda del <i>mara’kame</i> (chamán) para curarte.','13,14');
INSERT INTO `enfermedades` VALUES (8,'MORDIDA','Mordida de puma','Mordidas de puma','Máye','Máye',-12.0,-5.0,'Images/Wixarika/Enfermedades/Mordedura','Te mordió un <i>máye</i> (puma).
Ve a la tienda del <i>mara’kame</i> (chamán) para mejorar tu barra de vida.','15,16');
INSERT INTO `enfermedades` VALUES (9,'ENVENENADO','Envenenado por serpiente','Envenenado por una serpiente','Kúu','Kuterixi',-12.0,-5.0,'Images/Wixarika/Enfermedades/Mordedura','Te enveneno una <i>kúu</i> (serpiente).
Ve a la tienda del <i>mara’kame</i> (chamán) para curarte.','17,18');
INSERT INTO `enfermedades` VALUES (10,'HUESO','Hueso roto por venado','Huesos rotos por venado','Maxa','Maxatsi',-12.0,-5.0,'Images/Wixarika/Enfermedades/Mordedura','Un <i>maxa</i> (venado) te rompió un hueso.
Ve a la tienda del <i>mara’kame</i> (chamán) para mejorar tu barra de vida.','19,20');
INSERT INTO `enfermedades` VALUES (11,'MORDIDA','Mordida de zorro','Mordidas de zorro','Kauxai','Kauxaitsi',-12.0,-4.0,'Images/Wixarika/Enfermedades/Envenenado','Te mordió un <i>kauxai</i> (zorro).
Ve a la tienda del <i>mara’kame</i> (chamán) para curarte.','21,22');
INSERT INTO `enfermedades` VALUES (12,'MOLESTIA','Astillado','Astillados','Astillado','Astillados',-12.0,-4.0,'Images/Wixarika/Enfermedades/Espinado','Te astillaste con maderas rotas.
Ve a la tienda del <i>mara’kame</i> (chamán) para mejorar tu barra de vida.','23,24');
INSERT INTO `enfermedades` VALUES (13,'ENREDADO','Enredado','Enredados','Atorado','Atorado',-12.0,-4.0,'Images/Wixarika/Enfermedades/Espinado','Te enredaste con una teleraña.
Ve a la tienda del <i>mara’kame</i> (chamán) para curarte.','25,26');
INSERT INTO `enfermedades` VALUES (14,'ESPINA','Espinado','Espinados','Xuyá','Xuya’ate',-12.0,-4.0,'Images/Wixarika/Enfermedades/Espinado','Te espinaste con una <i>xuyá</i> (espina).
Ve a la tienda del <i>mara’kame</i> (chamán) para mejorar tu barra de vida.','27,28');
INSERT INTO `estrellas` VALUES (1,1,'JUEGO',1,0);
INSERT INTO `estrellas` VALUES (1,2,'JUEGO',1,0);
INSERT INTO `estrellas` VALUES (1,3,'JUEGO',1,0);
INSERT INTO `estrellas` VALUES (1,4,'JUEGO',1,0);
INSERT INTO `estrellas` VALUES (1,5,'JUEGO',1,0);
INSERT INTO `estrellas` VALUES (1,6,'JUEGO',1,0);
INSERT INTO `estrellas` VALUES (1,7,'JEFE',1,0);
INSERT INTO `estrellas` VALUES (2,8,'JUEGO',1,0);
INSERT INTO `estrellas` VALUES (2,9,'JUEGO',1,0);
INSERT INTO `estrellas` VALUES (2,10,'JUEGO',1,0);
INSERT INTO `estrellas` VALUES (2,11,'JEFE',1,0);
INSERT INTO `estrellas` VALUES (3,12,'JUEGO',1,0);
INSERT INTO `estrellas` VALUES (3,13,'JUEGO',1,0);
INSERT INTO `estrellas` VALUES (3,14,'JUEGO',1,0);
INSERT INTO `estrellas` VALUES (3,15,'JUEGO',1,0);
INSERT INTO `estrellas` VALUES (3,16,'JEFE',1,0);
INSERT INTO `estrellas` VALUES (4,17,'JUEGO',1,0);
INSERT INTO `estrellas` VALUES (4,18,'JUEGO',1,0);
INSERT INTO `estrellas` VALUES (4,19,'JUEGO',1,0);
INSERT INTO `estrellas` VALUES (4,20,'JUEGO',1,0);
INSERT INTO `estrellas` VALUES (4,21,'JEFE',1,0);
INSERT INTO `estrellas` VALUES (5,22,'JUEGO',1,0);
INSERT INTO `estrellas` VALUES (5,23,'JUEGO',1,0);
INSERT INTO `estrellas` VALUES (5,24,'JUEGO',1,0);
INSERT INTO `estrellas` VALUES (5,25,'JUEGO',1,0);
INSERT INTO `estrellas` VALUES (5,26,'JEFE',1,0);
INSERT INTO `estrellas` VALUES (6,27,'JUEGO',1,0);
INSERT INTO `estrellas` VALUES (6,28,'JUEGO',1,0);
INSERT INTO `estrellas` VALUES (6,29,'JUEGO',1,0);
INSERT INTO `estrellas` VALUES (6,30,'JEFE',1,0);
INSERT INTO `estrellas` VALUES (1,1,'QUIZ',1,0);
INSERT INTO `estrellas` VALUES (1,2,'QUIZ',1,0);
INSERT INTO `estrellas` VALUES (1,3,'QUIZ',1,0);
INSERT INTO `estrellas` VALUES (1,4,'QUIZ',1,0);
INSERT INTO `estrellas` VALUES (1,5,'QUIZ',1,0);
INSERT INTO `estrellas` VALUES (1,6,'QUIZ',1,0);
INSERT INTO `estrellas` VALUES (1,7,'JEFE',1,0);
INSERT INTO `estrellas` VALUES (2,8,'QUIZ',1,0);
INSERT INTO `estrellas` VALUES (2,9,'QUIZ',1,0);
INSERT INTO `estrellas` VALUES (2,10,'QUIZ',1,0);
INSERT INTO `estrellas` VALUES (2,11,'JEFE',1,0);
INSERT INTO `estrellas` VALUES (3,12,'QUIZ',1,0);
INSERT INTO `estrellas` VALUES (3,13,'QUIZ',1,0);
INSERT INTO `estrellas` VALUES (3,14,'QUIZ',1,0);
INSERT INTO `estrellas` VALUES (3,15,'QUIZ',1,0);
INSERT INTO `estrellas` VALUES (3,16,'JEFE',1,0);
INSERT INTO `estrellas` VALUES (4,17,'QUIZ',1,0);
INSERT INTO `estrellas` VALUES (4,18,'QUIZ',1,0);
INSERT INTO `estrellas` VALUES (4,19,'QUIZ',1,0);
INSERT INTO `estrellas` VALUES (4,20,'QUIZ',1,0);
INSERT INTO `estrellas` VALUES (4,21,'JEFE',1,0);
INSERT INTO `estrellas` VALUES (5,22,'QUIZ',1,0);
INSERT INTO `estrellas` VALUES (5,23,'QUIZ',1,0);
INSERT INTO `estrellas` VALUES (5,24,'QUIZ',1,0);
INSERT INTO `estrellas` VALUES (5,25,'QUIZ',1,0);
INSERT INTO `estrellas` VALUES (5,26,'JEFE',1,0);
INSERT INTO `estrellas` VALUES (6,27,'QUIZ',1,0);
INSERT INTO `estrellas` VALUES (6,28,'QUIZ',1,0);
INSERT INTO `estrellas` VALUES (6,29,'QUIZ',1,0);
INSERT INTO `estrellas` VALUES (6,30,'JEFE',1,0);
INSERT INTO `estados_desbloqueados` VALUES (1,1,1,1,0,0,0);
INSERT INTO `estados_desbloqueados` VALUES (1,1,2,0,0,0,0);
INSERT INTO `estados_desbloqueados` VALUES (1,1,3,0,0,0,0);
INSERT INTO `estados_desbloqueados` VALUES (1,1,4,0,0,0,0);
INSERT INTO `estados_desbloqueados` VALUES (1,1,5,0,0,0,0);
INSERT INTO `estados_desbloqueados` VALUES (1,1,6,0,0,0,0);
INSERT INTO `estados_desbloqueados` VALUES (1,1,7,0,0,0,0);
INSERT INTO `estados_desbloqueados` VALUES (1,2,8,0,0,0,0);
INSERT INTO `estados_desbloqueados` VALUES (1,2,9,0,0,0,0);
INSERT INTO `estados_desbloqueados` VALUES (1,2,10,0,0,0,0);
INSERT INTO `estados_desbloqueados` VALUES (1,2,11,0,0,0,0);
INSERT INTO `estados_desbloqueados` VALUES (1,3,12,0,0,0,0);
INSERT INTO `estados_desbloqueados` VALUES (1,3,13,0,0,0,0);
INSERT INTO `estados_desbloqueados` VALUES (1,3,14,0,0,0,0);
INSERT INTO `estados_desbloqueados` VALUES (1,3,15,0,0,0,0);
INSERT INTO `estados_desbloqueados` VALUES (1,3,16,0,0,0,0);
INSERT INTO `estados_desbloqueados` VALUES (1,4,17,0,0,0,0);
INSERT INTO `estados_desbloqueados` VALUES (1,4,18,0,0,0,0);
INSERT INTO `estados_desbloqueados` VALUES (1,4,19,0,0,0,0);
INSERT INTO `estados_desbloqueados` VALUES (1,4,20,0,0,0,0);
INSERT INTO `estados_desbloqueados` VALUES (1,4,21,0,0,0,0);
INSERT INTO `estados_desbloqueados` VALUES (1,5,22,0,0,0,0);
INSERT INTO `estados_desbloqueados` VALUES (1,5,23,0,0,0,0);
INSERT INTO `estados_desbloqueados` VALUES (1,5,24,0,0,0,0);
INSERT INTO `estados_desbloqueados` VALUES (1,5,25,0,0,0,0);
INSERT INTO `estados_desbloqueados` VALUES (1,5,26,0,0,0,0);
INSERT INTO `estados_desbloqueados` VALUES (1,6,27,0,0,0,0);
INSERT INTO `estados_desbloqueados` VALUES (1,6,28,0,0,0,0);
INSERT INTO `estados_desbloqueados` VALUES (1,6,29,0,0,0,0);
INSERT INTO `estados_desbloqueados` VALUES (1,6,30,0,0,0,0);
INSERT INTO `temas_por_nivel` VALUES (1,1,'• Los principales alimentos de la gastronomía <i>Wixárika</i> (maíz, frijol, agua y tortilla).
• Quién es el <i>mara’kame</i> (chamán).
• La forma tradicional de agricultura (coamiles).
• Y sobre los lugares sagrados Mesa del Nayar y Santa Teresa.','Images/Wixarika/Alimentos/Maíz','1,2,3,4');
INSERT INTO `temas_por_nivel` VALUES (2,2,'• Los principales alimentos de la gastronomía <i>Wixárika</i> (maíz amarillo, chile, queso y albóndigas de jabalí).
• Las plantas medicinales (milpa).
• La forma tradicional de caza (caza ritual).
• El camino del corazón.
• Y sobre la Laguna de Guadalupe Ocotán, un lugar sagrado.','Images/Wixarika/Animales/Jabali/Jabalí 4 x1','5,6,7,8,9');
INSERT INTO `temas_por_nivel` VALUES (3,3,'• Los principales alimentos de la gastronomía <i>Wixárika</i> (caña, miel, piloncillo y pescado).
• La vestimenta del pueblo <i>Wixárika</i>.
• La deidad <i>Tamatzi Kauyumari</i> (Nuestro hermano mayor Venado Azul). 
• Y sobre la Laguna de Santa María del Oro, un lugar sagrado.','Images/Wixarika/Deidades/Venado azul','10,11,12,13');
INSERT INTO `temas_por_nivel` VALUES (4,4,'• Los principales alimentos de la gastronomía <i>Wixárika</i> (jícama, ciruela y mole de venado).
• Las plantas medicinales (mazanilla).
• La música <i>Wixárika</i>.
• Los rituales <i>Wixárika</i>.
• Y sobre Potrero palmita, un lugar sagrado.','Images/Wixarika/Personajes/Hombres/Músico 1','14,15,16,17,18');
INSERT INTO `temas_por_nivel` VALUES (5,5,'• Los principales alimentos de la gastronomía <i>Wixárika</i> (mango, arrayan y caldo de ardilla).
• Los animales mensajeros (lechuza).
• Festividades de la cultura <i>Wixárika</i>.
• Los centros ceremoniales (<i>Tukipa</i>).
• Y sobre los lugares sagrados la Isla de Mexcaltitán y el Río Santiago.','Images/Wixarika/Diario_Viaje/Tukipa','19,20,21,22,23');
INSERT INTO `temas_por_nivel` VALUES (6,6,'• Los principales alimentos de la gastronomía <i>Wixárika</i> (nanchi, plátano, pipián de iguana y plátano frito).
• Las ofrendas <i>Wixárika</i> (ojos de dios).
• La deidad <i>Tatei Haramara</i> (Nuestra madre del mar).
• Y sobre el sitio sagrado <i>Haramara</i>.','Images/Wixarika/Escenario/Carteles/Tatei Haramara 1x','24,25,26,27');
INSERT INTO `temas_por_nivel` VALUES (7,7,'JEFE_FINAL','',NULL);
INSERT INTO `temas_por_nivel` VALUES (8,8,'• Los principales alimentos de la gastronomía <i>Wixárika</i> (maíz morado, maíz negro, chicuatol y pan de elote).
• Quién es el <i>xuku’uri ɨkame</i> (jicarero).
• La deidad <i>Tatei Wexica Wimari</i> (Nuestra madre águila).
• La forma tradicional de agricultura (coamiles).
• El <i>Hikuri</i> (peyote).
• La peregrinación a <i>Wirikuta</i>.
• Y sobre los lugares sagrados <i>Tuapurie</i> y <i>Xurahue Muyaca</i>.','Images/Wixarika/Vegetacion/Peyote 1','28,29,30,31,32,33,34');
INSERT INTO `temas_por_nivel` VALUES (9,9,'• Los principales alimentos de la gastronomía <i>Wixárika</i> (jítomate, miel y caldo de güilota).
• Las plantas medicinales (Gordolobo).
• La forma tradicional de caza (caza ritual).
• Los animales espirituales (lagartija).
• El <i>nana’iyari</i> (el costumbre <i>Wixárika</i>).
• Y sobre Plateros, una localidad donde hay asentamientos <i>Wixárika</i>.','Images/Wixarika/Iconos/Lagartija','35,36,37,38,39,40');
INSERT INTO `temas_por_nivel` VALUES (10,10,'• Los principales alimentos de la gastronomía <i>Wixárika</i> (tuna, camote y enchiladas).
• El <i>Hikuri</i> (peyote).
• La peregrinación a <i>Wirikuta</i>.
• Las ofrendas <i>Wixárika</i> (máscaras).
• Las deidades <i>Nairy</i> (dios del fuego primigenio) y <i>Takutsi Nakawé</i> (Nuestra abuela tierra).
• Y sobre el lugar sagrado <i>Makuipa</i> (Cerro del Padre).','Images/Wixarika/Alimentos/Tuna','41,42,43,44,45,46');
INSERT INTO `temas_por_nivel` VALUES (11,11,'JEFE_FINAL','',NULL);
INSERT INTO `temas_por_nivel` VALUES (12,12,'• Los principales alimentos de la gastronomía <i>Wixárika</i> (maíz azul, frijol y tacos).
• Quién es el <i>kawiteru</i> (anciano sabio).
• La vestimenta del pueblo <i>Wixárika</i>.
• Los <i>kawitu</i> (mitos <i>Wixárika</i>).
• Y sobre los lugares sagrados <i>Huahuatsari</i>, <i>Cuhixu Uheni</i> y <i>Tatei Matiniere</i>.','Images/Wixarika/Alimentos/Maíz morado','47,48,49,50');
INSERT INTO `temas_por_nivel` VALUES (13,13,'• Los principales alimentos de la gastronomía <i>Wixárika</i> (guambulos, habas y caldo).
• La deidad <i>Tamatzi Kauyumari</i> (Nuestro hermano mayor Venado Azul). 
• La música <i>Wixárika</i>.
• El <i>Hikuri</i> (peyote).
• La peregrinación a <i>Wirikuta</i>.
• Y sobre <i>Maxa Yapa</i>, un lugar sagrado.','Images/Wixarika/Vegetacion/Peyote 1','51,52,53,54,55,56');
INSERT INTO `temas_por_nivel` VALUES (14,14,'• Los principales alimentos de la gastronomía <i>Wixárika</i> (chayote, chile y elote asado).
• Las plantas medicinales (peyote).
• Los animales espirituales (águila real).
• Festividades de la cultura <i>Wixárika</i> (sacrificio del toro).
• El <i>Hikuri</i> (peyote).
• La peregrinación a <i>Wirikuta</i>.
• Y sobre los lugares sagrados <i>Tuy Mayau</i> y <i>Huacuri Quitenie</i>.','Images/Wixarika/Animales/Aguila/Águila 1','57,58,59,60,61,62,63');
INSERT INTO `temas_por_nivel` VALUES (15,15,'• Los principales alimentos de la gastronomía <i>Wixárika</i> (naranja, higo y sopa).
• La peregrinación a <i>Wirikuta</i>.
• Las ofrendas <i>Wixárika</i> (tablillas).
• Las deidades <i>Tututzi Maxa Kwaxi</i> (Nuestro bisabuelo Cola de venado) y <i>Tayau</i> (Nuestro padre el Sol).
• Y sobre el sitio sagrado <i>Wirikuta</i>.','Images/Wixarika/Objetos_Espirituales/Tablilla','64,65,66,67,68');
INSERT INTO `temas_por_nivel` VALUES (16,16,'JEFE_FINAL','',NULL);
INSERT INTO `temas_por_nivel` VALUES (17,17,'• Los principales alimentos de la gastronomía <i>Wixárika</i> (maíz blanco, caña y pinole).
• Quién es el <i>Tupiri</i> (policía).
• La forma tradicional de agricultura (coamiles).
• La importancia del número 5 en la cultura <i>Wixárika</i>.
• Y sobre San Antonio de Padua, una localidad donde hay comunidades <i>Wixárika</i>.','Images/Wixarika/Alimentos/Maíz blanco','69,70,71,72,73');
INSERT INTO `temas_por_nivel` VALUES (18,18,'• Los principales alimentos de la gastronomía <i>Wixárika</i> (verdolaga, guaje y quesadillas con verdolagas).
• Las plantas medicinales (orégano).
• La forma tradicional de caza (caza ritual).
• Los cazadores cósmicos
• Y sobre San Bernardino de Milpillas, una localidad donde hay asentamientos <i>Wixárika</i>.','Images/Wixarika/Alimentos/Verdolaga','74,75,76,77,78');
INSERT INTO `temas_por_nivel` VALUES (19,19,'• Los principales alimentos de la gastronomía <i>Wixárika</i> (nopal, guamúchil y frijoles fritos y cocidos).
• Las plantas medicinales (eucalipto).
• Los animales espirituales (serpiente azul).
• La vestimenta del pueblo <i>Wixárika</i>
• Las facetas de <i>Tamatzi Kauyumari</i> (Nuestro hermano mayor Venado Azul). 
• Y sobre Cinco de Mayo, una localidad donde hay comunidades <i>Wixárika</i>.','Images/Wixarika/Animales/Serpiente/Serpiente 1','79,80,81,82,83,84');
INSERT INTO `temas_por_nivel` VALUES (20,20,'• Los principales alimentos de la gastronomía <i>Wixárika</i> (quelites, semillas de calabaza, atole y pan).
• La leyenda de <i>Watakame</i>.
• Las ofrendas <i>Wixárika</i> (jícaras).
• La deidad <i>Tatei Yurienáka</i> (Nuestra madre tierra).
• Y sobre el sitio sagrado <i>Hauxa Manaka</i>.','Images/Wixarika/Escenario/Carteles/Hauxa manaka 1x','85,86,87,88,89');
INSERT INTO `temas_por_nivel` VALUES (21,21,'JEFE_FINAL',NULL,NULL);
INSERT INTO `temas_por_nivel` VALUES (22,22,'• Los principales alimentos de la gastronomía <i>Wixárika</i> (maíz rojo, hongos y quesadillas).
• Quién es el <i>Tatuwani</i> (gobernador).
• Las plantas medicinales (clavo).
• La música <i>Wixárika</i>.
• Las direcciones del universo
• Y sobre Colonia <i>Hatmasie</i>, una localidad donde hay asentamientos <i>Wixárika</i>.','Images/Wixarika/Alimentos/Maíz rojo','90,97,92,93,94,95');
INSERT INTO `temas_por_nivel` VALUES (23,23,'• Los principales alimentos de la gastronomía <i>Wixárika</i> (pitahaya, guayaba y gorditas).
• Festividades de la cultura <i>Wixárika</i>.
• La historia del fuego.
• La deidad <i>Tatewari</i> (Nuestro abuelo fuego).
• Y sobre el sitio sagrado <i>Te’akata</i>.','Images/Wixarika/Deidades/Tatewari 3x','96,97,98,99,100');
INSERT INTO `temas_por_nivel` VALUES (24,24,'• Los principales alimentos de la gastronomía <i>Wixárika</i> (jamaica, cebolla y pozole).
• Las plantas medicinales (hierbabuena).
• Los animales mensajeros (zorro).
• La vestimenta del pueblo <i>Wixárika</i>.
• Y sobre Tuxpan de Bolaños, una localidad donde hay comunidades <i>Wixárika</i>.','Images/Wixarika/Animales/Zorro/Zorro 1','101,102,103,104,105');
INSERT INTO `temas_por_nivel` VALUES (25,25,'• Los principales alimentos de la gastronomía <i>Wixárika</i> (calabacita, limón y tejuino).
• Las diosas de la lluvia.
• Las ofrendas <i>Wixárika</i> (flechas).
• La deidad <i>Tatei Xapawiyeme</i> (Madre diosa del sur).
• Y sobre el sitio sagrado <i>Xapawiyeme</i>.','Images/Wixarika/Escenario/Carteles/Xapawiyeme letrero 1x','106,107,108,109,110');
INSERT INTO `temas_por_nivel` VALUES (26,26,'JEFE_FINAL',NULL,NULL);
INSERT INTO `temas_por_nivel` VALUES (27,27,'• Los principales alimentos de la gastronomía <i>Wixárika</i> (tamal y atole).
• Quién es el <i>hikuritame</i> (peyotero).
• Las plantas medicinales (<i>uxa</i>).
• Y sobre el <i>mɨɨkí kwevíxa</i> (ceremonia de los muertos).','Images/Wixarika/Alimentos/Tamal','111,112,113,114');
INSERT INTO `temas_por_nivel` VALUES (28,28,'• Los principales alimentos de la gastronomía <i>Wixárika</i> (mole).
• Los animales mensajeros (Zopilote).
• El <i>mɨɨkí kwevíxa</i> (ceremonia de los muertos).
• Y sobre el viaje de los mɨɨkite (muertos).','Images/Wixarika/Animales/Zopilote/Zopilote 4','115,116,117,118');
INSERT INTO `temas_por_nivel` VALUES (29,29,'• Los principales alimentos de la gastronomía <i>Wixárika</i> (tejuino).
• Las plantas medicinales (tepehuaje).
• La deidad <i>Tukákame</i> (Dios de la muerte).
• Y sobre la mɨɨkí (muerte).','Images/Wixarika/Alimentos/Tejuino','119,120,121,122');
INSERT INTO `sitios_importantes` VALUES (1,'Mesa del Nayar','Mesa del Nayar es una comunidad serrana, considerada como un lugar sagrado donde se colocan ofrendas para las deidades.
Aquí habitan comunidades <i>Wixárika</i>.','Images/Wixarika/Sitios Importantes/Mesa del Nayar 3x','1,2');
INSERT INTO `sitios_importantes` VALUES (2,'Santa Teresa','Santa Teresa es una comunidad serrana, considerada como un lugar sagrado donde se dejan ofrendas para las deidades.
Aquí habitan comunidades <i>Wixárika</i>.','Images/Wixarika/Sitios Importantes/Santa Teresa 2  3x','3,4');
INSERT INTO `sitios_importantes` VALUES (3,'Laguna de Guadalupe Ocotán','La Laguna de Guadalupe Ocotán es considerada como un lugar sagrado donde se colocan ofrendas para las deidades.','Images/Wixarika/Sitios Importantes/Laguna Gpe. Ocotan','5');
INSERT INTO `sitios_importantes` VALUES (4,'Laguna de Santa María del Oro','La Laguna de Santa María del Oro es considerada como un lugar especial donde se dejan ofrendas para las deidades.
Está ubicada dentro de un cráter volcánico, rodeada por cerros y bosques.','Images/Wixarika/Sitios Importantes/SAMAO 3x','6,7');
INSERT INTO `sitios_importantes` VALUES (5,'Potrero de Palmita','Potrero de Palmita es una comunidad <i>Wixárika</i> ubicada a la orilla de la presa de Agua Milpa, es considerada como un lugar sagrado donde se colocan ofrendas para las deidades.','Images/Wixarika/Sitios Importantes/Potrero de palmita 3x','8');
INSERT INTO `sitios_importantes` VALUES (6,'Isla de Mexcaltitán','La Isla de Mexcaltitán es un sitio turístico, considerado como un lugar especial donde se dejan ofrendas para las deidades.','Images/Wixarika/Sitios Importantes/Isla de mexcaltitán 1 3x','9');
INSERT INTO `sitios_importantes` VALUES (7,'Río Santiago','El Río Santiago es considerado como un lugar especial donde se colocan ofrendas para las deidades.
Nace en Ocotlán, Jalisco y desemboca en el mar de San Blas, Nayarit.','Images/Wixarika/Sitios Importantes/Río Santiago 3x','10,11');
INSERT INTO `sitios_importantes` VALUES (8,'Haramara','<i>Haramara</i> es uno de los sitios sagrados más importantes de la cosmogonía <i>Wixárika</i>.
La roca blanca <i>Waxiewe</i> (blanco vapor), representa la forma física de <i>Tatei Haramara</i> (diosa madre del mar), y es el lugar donde habita. La piedra más pequeña es llamada <i>Cuca Wima</i>.','Images/Wixarika/Sitios Importantes/Haramara 1x','12,13');
INSERT INTO `sitios_importantes` VALUES (9,'JEFE_FINAL','JEFE_FINAL','JEFE_FINAL',NULL);
INSERT INTO `sitios_importantes` VALUES (10,'Tuapurie','<i>Tuapurie</i> es un lugar sagrado donde se dejan ofrendas para las deidades.','Images/Wixarika/Sitios Importantes/Tuapurie','14');
INSERT INTO `sitios_importantes` VALUES (11,'Xurahue Muyaca','<i>Xurahue Muyaca</i> es un lugar especial donde se colocan ofrendas para las deidades.','Images/Wixarika/Sitios Importantes/Xurahue Muyaca','15');
INSERT INTO `sitios_importantes` VALUES (12,'Plateros','Plateros es una localidad donde descansan los <i>wixaritari</i> durante el camino de su peregrinación a <i>Wirikuta</i>.','Images/Wixarika/Sitios Importantes/Plateros','16');
INSERT INTO `sitios_importantes` VALUES (13,'Makuipa (Cerro del Padre)','<i>Makuipa</i> (Cerro del Padre) es un lugar sagrado donde se dejan ofrendas para las deidades.
Aquí durmió una noche <i>Tamatzi Kauyumari</i> (Nuestro hermano mayor Venado Azul), durante su peregrinación a <i>Wirikuta</i>.','Images/Wixarika/Sitios Importantes/Makuipa','17,18');
INSERT INTO `sitios_importantes` VALUES (14,'JEFE_FINAL','JEFE_FINAL','JEFE_FINAL',NULL);
INSERT INTO `sitios_importantes` VALUES (15,'Huahuatsari','<i>Huahuatsari</i> es un lugar especial donde se dejan ofrendas para las deidades.','Images/Wixarika/Sitios Importantes/Huahuatsari','19');
INSERT INTO `sitios_importantes` VALUES (16,'Cuhixu Uheni','<i>Cuhixu Uheni</i> es un lugar sagrado donde se dejan ofrendas para las deidades.','Images/Wixarika/Sitios Importantes/Cuhixu Uheni','20');
INSERT INTO `sitios_importantes` VALUES (17,'Tatei Matiniere','<i>Tatei Matiniere</i> es un lugar especial donde se dejan ofrendas para las deidades.','Images/Wixarika/Sitios Importantes/Tatei Matiniere','21');
INSERT INTO `sitios_importantes` VALUES (18,'Maxa Yapa','<i>Maxa Yapa</i> es un lugar sagrado donde se dejan ofrendas para las deidades.','Images/Wixarika/Sitios Importantes/Maxa Yapa','22');
INSERT INTO `sitios_importantes` VALUES (19,'Tuy Mayau','<i>Tuy Mayau</i> es un lugar especial donde se dejan ofrendas para las deidades.','Images/Wixarika/Sitios Importantes/Tuy Mayau','23');
INSERT INTO `sitios_importantes` VALUES (20,'Huacuri Quitenie','<i>Huacuri Quitenie</i> es un lugar sagrado donde se dejan ofrendas para las deidades.','Images/Wixarika/Sitios Importantes/Huacuri Quitenie','24');
INSERT INTO `sitios_importantes` VALUES (21,'Wirikuta','<i>Wirikuta</i> es uno de los sitios sagrados más importantes de la cosmogonía <i>Wixárika</i>. 
Aquí es donde ocurrió la creación del mundo y por donde se levanta el sol.','Images/Wixarika/Sitios Importantes/Wirikuta (1)','25,26');
INSERT INTO `sitios_importantes` VALUES (22,'JEFE_FINAL','JEFE_FINAL','JEFE_FINAL',NULL);
INSERT INTO `sitios_importantes` VALUES (23,'San Antonio de Padua','San Antonio de Padua es una localidad donde viven pobladores <i>Wixárika</i>.','Images/Wixarika/Sitios Importantes/San Antonio de Padua','27');
INSERT INTO `sitios_importantes` VALUES (24,'San Bernardino de Milpillas','San Bernardino de Milpillas es una localidad donde residen pobladores <i>Wixárika</i>.','Images/Wixarika/Sitios Importantes/San Bernardino de Milpillas','28');
INSERT INTO `sitios_importantes` VALUES (25,'Cinco de Mayo','Cinco de Mayo es una localidad donde se encuentran asentamientos <i>Wixárika</i>.','Images/Wixarika/Sitios Importantes/Cinco de mayo','29');
INSERT INTO `sitios_importantes` VALUES (26,'Hauxa Manaka','<i>Hauxa Manaka</i> es uno de los sitios sagrados más importantes de la cosmogonía <i>Wixárika</i>. 
<i>Hauxa Manaka</i> es la casa de Tututzi Maxa Kwaxi (Nuestro Bisabuelo Cola de Venado). Y es aquí donde Tayau (Nuestro padre sol) regresa a su lugar de origen dando inicio nuevamente a la oscuridad (la noche).','Images/Wixarika/Sitios Importantes/Hauxa Manaka','30,31');
INSERT INTO `sitios_importantes` VALUES (27,'JEFE_FINAL','JEFE_FINAL','JEFE_FINAL',NULL);
INSERT INTO `sitios_importantes` VALUES (28,'Colonia Hatmasie','Colonia <i>Hatmasie</i> es una localidad donde habitan comunidades <i>Wixárika</i>.','Images/Wixarika/Sitios Importantes/Colonia Hatmasie','32');
INSERT INTO `sitios_importantes` VALUES (29,'Te’akata','<i>Te’akata</i> es uno de los sitios sagrados más importantes de la cosmogonía <i>Wixárika</i>. 
<i>Te’akata</i> es el centro del universo, donde tuvo lugar la gesta universal, pues ahí los antepasados arrojaron un niño enfermo al fuego para que se transformara en Tayau (Nuestro padre sol).','Images/Wixarika/Sitios Importantes/Teakata','33,34');
INSERT INTO `sitios_importantes` VALUES (30,'Santa Catarina Cuexcomatitlán','Santa Catarina Cuexcomatitlán es una localidad donde se encuentrancomunidades <i>Wixárika</i>.','Images/Wixarika/Sitios Importantes/Santa Catarina Cuexcomatitlán 1','35');
INSERT INTO `sitios_importantes` VALUES (31,'San Sebastián Teponahuaxtlán','San Sebastián Teponahuaxtlán es una localidad donde habitan comunidades <i>Wixárika</i>.','Images/Wixarika/Sitios Importantes/San Sebastián Teponahuaxtlán','36');
INSERT INTO `sitios_importantes` VALUES (32,'Tuxpan de Bolaños','Tuxpan de Bolaños es una localidad donde se encuentran comunidades <i>Wixárika</i>.','Images/Wixarika/Sitios Importantes/Tuxpan de Bolaños','37');
INSERT INTO `sitios_importantes` VALUES (33,'Xapawiyemeta','<i>Xapawiyemeta</i> es uno de los sitios sagrados más importantes de la cosmogonía <i>Wixárika</i>. 
<i>Xapawiyemeta</i> representa el paso de una absoluta oscuridad a un amanecer.
Aquí <i>Tamatzi Paritsika</i> (Niño sol) luchó contra seres inframundanos que lo querían devorar, para salir por <i>Reunari</i> (Cerro Quemado) convertido en un gran sol.','Images/Wixarika/Sitios Importantes/Xapawiyemeta','38,39,40');
INSERT INTO `sitios_importantes` VALUES (34,'JEFE_FINAL','JEFE_FINAL','JEFE_FINAL',NULL);
INSERT INTO `curaciones` VALUES (1,'ENFERMEDAD','Piquete de abeja',8,0,'Images/Wixarika/Curaciones/Pócima de salud');
INSERT INTO `curaciones` VALUES (2,'ENFERMEDAD','Piquete de alacrán',8,0,'Images/Wixarika/Curaciones/Pócima de salud');
INSERT INTO `curaciones` VALUES (3,'ENFERMEDAD','Piquete de araña',8,0,'Images/Wixarika/Curaciones/Pócima de salud');
INSERT INTO `curaciones` VALUES (4,'ENFERMEDAD','Mordida de cocodrilo',8,0,'Images/Wixarika/Curaciones/Pócima de velocidad');
INSERT INTO `curaciones` VALUES (5,'ENFERMEDAD','Mordida coyote',15,0,'Images/Wixarika/Curaciones/Pócima de salud');
INSERT INTO `curaciones` VALUES (6,'ENFERMEDAD','Mordida de jabalí',8,0,'Images/Wixarika/Curaciones/Pócima de salud');
INSERT INTO `curaciones` VALUES (7,'ENFERMEDAD','Mordida de lobo',8,0,'Images/Wixarika/Curaciones/Pócima de salud');
INSERT INTO `curaciones` VALUES (8,'ENFERMEDAD','Mordida de puma',8,0,'Images/Wixarika/Curaciones/Pócima de velocidad');
INSERT INTO `curaciones` VALUES (9,'ENFERMEDAD','Envenenado por serpiente',8,0,'Images/Wixarika/Curaciones/Pócima de salud');
INSERT INTO `curaciones` VALUES (10,'ENFERMEDAD','Hueso roto por venado',15,0,'Images/Wixarika/Curaciones/Pócima de salud');
INSERT INTO `curaciones` VALUES (11,'ENFERMEDAD','Mordida de zorro',10,0,'Images/Wixarika/Curaciones/Pócima de salud');
INSERT INTO `curaciones` VALUES (12,'ENFERMEDAD','Astillado',0,0,'Images/Wixarika/Enfermedades/Espinado');
INSERT INTO `curaciones` VALUES (13,'ENFERMEDAD','Enredado',0,0,'Images/Wixarika/Curaciones/Pócima de salud');
INSERT INTO `curaciones` VALUES (14,'ENFERMEDAD','Espinado',0,0,'Images/Wixarika/Enfermedades/Espinado');
INSERT INTO `curaciones` VALUES (15,'ENFERMEDAD','Hueso roto por un venado',20,0,'Images/Wixarika/Curaciones/Pócima de salud');
INSERT INTO `curaciones` VALUES (16,'ENFERMEDAD','Mordida de lobo',26,0,'Images/Wixarika/Curaciones/Pócima de velocidad');
INSERT INTO `curaciones` VALUES (17,'POCIMA','Salud nivel 1',10,0,'Images/Wixarika/Curaciones/Remedio 1');
INSERT INTO `curaciones` VALUES (18,'POCIMA','Salud nivel 2',20,0,'Images/Wixarika/Curaciones/Remedio 2');
INSERT INTO `curaciones` VALUES (19,'POCIMA','Salud nivel 3',30,0,'Images/Wixarika/Curaciones/Remedio 3');
INSERT INTO `curaciones` VALUES (20,'NINGUNA','Velocidad nivel 1',0,0,'Images/Wixarika/Curaciones/Pocima_salud_3');
INSERT INTO `curaciones` VALUES (21,'ENFERMEDAD','Curar todo',25,0,'Images/Wixarika/Curaciones/Pócima de salud');
INSERT INTO `curaciones` VALUES (22,'POCIMA','Salud nivel 4',50,0,'Images/Wixarika/Curaciones/Remedio 4');
INSERT INTO `curaciones` VALUES (23,'POCIMA','Salud nivel 5',70,0,'Images/Wixarika/Curaciones/Remedio 5');
INSERT INTO `curaciones` VALUES (24,'POCIMA','Salud nivel 6',100,0,'Images/Wixarika/Curaciones/Remedio 6');
INSERT INTO `curaciones` VALUES (25,'ENFERMEDAD','Espinado',8,0,'Images/Wixarika/Enfermedades/Espinado');
INSERT INTO `explicacion_inicio` VALUES (1,1,'Del Nayar','Recolecta: <i>ikú</i> (maíz) y <i>múme</i> (frijol).','Caza: <i>tuixuyeutanaka</i> (jabalí).','1,2','Images/Wixarika/Animales/Jabali/Jabalí 6 x1','','','Images/Wixarika/Alimentos/Maíz amarillo','Images/Wixarika/Alimentos/Frijol',NULL);
INSERT INTO `explicacion_inicio` VALUES (1,2,'La Yesca','Recolecta: <i>ikú taxawime</i> (maíz amarillo) y <i>kukúri </i>(chile).','Caza: <i>tuixuyeutanaka</i> (jabalí).','3,4','Images/Wixarika/Animales/Jabali/Jabalí 6 x1','','','Images/Wixarika/Alimentos/Maíz amarillo','Images/Wixarika/Alimentos/Chile','');
INSERT INTO `explicacion_inicio` VALUES (1,3,'Santa María del Oro','Recolecta: <i>uwá</i> (caña) y <i>xiete</i> (miel).','Pesca: <i>ketsí</i> (pescado).','4,5','Images/Wixarika/Animales/Pez/Pez 13','','','Images/Wixarika/Alimentos/Caña','Images/Wixarika/Alimentos/Miel','');
INSERT INTO `explicacion_inicio` VALUES (1,4,'Tepic','Recolecta: <i>xa’ata</i> (jicama) y <i>kwarɨpa</i> (ciruela).','Caza: <i>maxa</i> (venado).','6,7','Images/Wixarika/Animales/Venado/Venado 1','','','Images/Wixarika/Alimentos/Jícama','Images/Wixarika/Alimentos/Ciruela','');
INSERT INTO `explicacion_inicio` VALUES (1,5,'Santiago Ixcuintla','Recolecta: <i>máacu</i> (mango) y <i>tsikwai </i> (arrayán).','Caza: <i>tekɨ</i> (ardilla).','8,9','Images/Wixarika/Animales/Ardilla/Ardilla 2','','','Images/Wixarika/Alimentos/Mango','Images/Wixarika/Alimentos/Arrayanes',NULL);
INSERT INTO `explicacion_inicio` VALUES (1,6,'San Blas','Recolecta: <i>uwakí</i> (nanchi) y <i>kaárú</i> (plátano).','Caza: <i>keetse</i> (iguana).','10,11','Images/Wixarika/Animales/Iguana/Iguana 1','','','Images/Wixarika/Alimentos/Nanchi','Images/Wixarika/Alimentos/Plátano','');
INSERT INTO `explicacion_inicio` VALUES (1,7,'Sombra de Nayarit','JEFE_FINAL','Vence a las sombras para avanzar en tu ruta contra las sombras malvadas','','','','','','','');
INSERT INTO `explicacion_inicio` VALUES (2,8,'Valparaíso','Recolecta: <i>ikú yɨwi</i> (maíz morado) y <i>ikú tataɨrawi</i> (maíz negro).','Caza: <i>tuixuyeutanaka</i> (jabalí).','12,13','Images/Wixarika/Animales/Jabali/Jabalí 6 x1','','','Images/Wixarika/Alimentos/Maíz morado','Images/Wixarika/Alimentos/Maíz negro',NULL);
INSERT INTO `explicacion_inicio` VALUES (2,9,'Fresnillo','Recolecta: <i>túmati</i> (jitomate) y <i>xiete</i> (miel).','Caza: <i>weurai</i> (güilota).','14,15','Images/Wixarika/Animales/Guilota/Güilota 1','','','Images/Wixarika/Alimentos/Jítomate','Images/Wixarika/Alimentos/Miel',NULL);
INSERT INTO `explicacion_inicio` VALUES (2,10,'Zacatecas','Recolecta: <i>yɨɨna</i> (tuna) y <i>ye’eri</i> (camote).','Caza: <i>wakana</i> (pollo).','16,17','Images/Wixarika/Animales/Gallina/Gallina 1 x1','',NULL,'Images/Wixarika/Alimentos/Tuna','Images/Wixarika/Alimentos/Camote',NULL);
INSERT INTO `explicacion_inicio` VALUES (2,11,'Sombra de Zacatecas','JEFE_FINAL','Vence a las sombras para avanzar en tu ruta contra las sombras malvadas','','','',NULL,'',NULL,NULL);
INSERT INTO `explicacion_inicio` VALUES (3,12,'Villa de Ramos','Recolecta: <i>ikú yuawime</i> (maíz azul) y <i>múme</i> (frijol).','Caza: <i>tuixu</i> (cerdo).','18,19','Images/Wixarika/Animales/Cerdo/Cerdo 1','','','Images/Wixarika/Alimentos/Maíz azul','Images/Wixarika/Alimentos/Frijol',NULL);
INSERT INTO `explicacion_inicio` VALUES (3,13,'Santo Domingo','Recolecta: <i>tsíweri</i> (gualumbos) y <i>kweetsi</i> (habas).','Caza: <i>tátsiu</i> (conejo).','20,21','Images/Wixarika/Animales/Conejo/Conejo 1','',NULL,'Images/Wixarika/Alimentos/Gualumbo','Images/Wixarika/Alimentos/Habas',NULL);
INSERT INTO `explicacion_inicio` VALUES (3,14,'Charcas','Recolecta: <i>karimutsi</i> (pochote) y <i>kukúri </i>(chile).','Caza: <i>maxa</i> (venado).','22,23','Images/Wixarika/Animales/Venado/Venado 1','',NULL,'Images/Wixarika/Alimentos/Pochote','Images/Wixarika/Alimentos/Chile',NULL);
INSERT INTO `explicacion_inicio` VALUES (3,15,'Real de Catorce','Recolecta: <i>narakaxi</i> (naranja), <i>piní</i> (higo).','Caza: <i>maxa</i> (venado).','24,25','Images/Wixarika/Animales/Venado/Venado 1','',NULL,'Images/Wixarika/Alimentos/Naranja','Images/Wixarika/Alimentos/Higo',NULL);
INSERT INTO `explicacion_inicio` VALUES (3,16,'Sombra de San Luis Potosí','JEFE_FINAL','Vence a las sombras para avanzar en tu ruta contra las sombras malvadas','','','',NULL,'','',NULL);
INSERT INTO `explicacion_inicio` VALUES (4,17,'Mezquital','Recolecta: <i>ikú tuuxá</i> (maíz blanco) y <i>uwá</i> (caña).','Caza: <i>tekɨ</i> (ardilla).','26,27','Images/Wixarika/Animales/Ardilla/Ardilla 2','',NULL,'Images/Wixarika/Alimentos/Maíz blanco','Images/Wixarika/Alimentos/Caña',NULL);
INSERT INTO `explicacion_inicio` VALUES (4,18,'Pueblo Nuevo','Recolecta: <i>aɨraxate</i> (verdolagas) y <i>haxi</i> (guaje).','Caza: <i>keetse</i> (iguana).','28,29','Images/Wixarika/Animales/Iguana/Iguana 1','',NULL,'Images/Wixarika/Alimentos/Verdolaga','Images/Wixarika/Alimentos/Guaje rojo',NULL);
INSERT INTO `explicacion_inicio` VALUES (4,19,'Durango','Recolecta: <i>na’akari</i> (nopal) y <i>muxu’uri</i> (guamúchil).','Caza: <i>weurai</i> (güilota).','30,31','Images/Wixarika/Animales/Guilota/Güilota 1','',NULL,'Images/Wixarika/Alimentos/Nopal','Images/Wixarika/Alimentos/Guamúchil',NULL);
INSERT INTO `explicacion_inicio` VALUES (4,20,'Canatlán','Recolecta: <i>ké’uxate</i> (quelites) y <i>xutsi hatsiyarite</i> (semillas de calabaza).','Caza: <i>wakana</i> (pollo).','32,33','Images/Wixarika/Animales/Gallina/Gallina 1 x1','',NULL,'Images/Wixarika/Alimentos/Quelites','Images/Wixarika/Alimentos/Semilla de calabaza',NULL);
INSERT INTO `explicacion_inicio` VALUES (4,21,'Sombra de Jalisco','JEFE_FINAL','Vence a las sombras para avanzar en tu ruta contra las sombras malvadas','','','',NULL,'','',NULL);
INSERT INTO `explicacion_inicio` VALUES (5,22,'Huejuquilla','Recolecta: <i>ikú mɨxeta</i> (maíz rojo) y <i>yekwa’ate</i> (hongos).','Caza: <i>tuixu</i> (cerdo).','34,35','Images/Wixarika/Animales/Cerdo/Cerdo 1','',NULL,'Images/Wixarika/Alimentos/Maíz rojo','Images/Wixarika/Alimentos/Hongos',NULL);
INSERT INTO `explicacion_inicio` VALUES (5,23,'Mezquitic','Recolecta: <i>ma’ara</i> (pitahaya) y <i>ha’yewaxi</i> (guayaba).','Caza: <i>tátsiu</i> (conejo).','36,37','Images/Wixarika/Animales/Conejo/Conejo 1','',NULL,'Images/Wixarika/Alimentos/Pitahaya','Images/Wixarika/Alimentos/Guayaba',NULL);
INSERT INTO `explicacion_inicio` VALUES (5,24,'Bolaños','Recolecta: <i>kamaika</i> (jamaica) y <i>uyuri</i> (cebolla).','Caza: <i>weurai</i> (güilota).','38,39','Images/Wixarika/Animales/Güilota/Guilota 1','',NULL,'Images/Wixarika/Alimentos/Jamaica','Images/Wixarika/Alimentos/Cebolla','');
INSERT INTO `explicacion_inicio` VALUES (5,25,'Chapala','Recolecta: <i>xútsi</i> (calabacita) y <i>tsinakari</i> (limón).','Pesca: <i>ketsí</i> (pescado).','40,41','Images/Wixarika/Animales/Pez/Pez 13','',NULL,'Images/Wixarika/Alimentos/Calabacita','Images/Wixarika/Alimentos/Limón 1',NULL);
INSERT INTO `explicacion_inicio` VALUES (5,26,'Inframundo','JEFE_FINAL','Vence a las sombras para terminar con todas las sombras malvadas','','','',NULL,'',NULL,NULL);
INSERT INTO `municipios` VALUES (1,1,'Del Nayar','¡Bienvenido a Del Nayar!



Del Nayar es el territorio ancestral de los <i>wixaritari</i>, aquí se encuentran numerosas comunidades <i>Wixárika</i>, principalmente, en las localidades de Jesús María, Mesa del Nayar y Santa Teresa.

El nombre del municipio es en honor al jefe Cora Nayar. ','1,2,3');
INSERT INTO `municipios` VALUES (1,2,'La Yesca','¡Bienvenido a La Yesca!



En La Yesca habitan comunidades <i>Wixárika</i>, especialmente, en las localidades de Puente de Camotlán, Guadalupe Ocotán y Huajimic.

El nombre del lugar se debe a un tipo de madera porosa llamada yesca que abunda en esta zona.','4,5,6');
INSERT INTO `municipios` VALUES (1,3,'Santa María del Oro','¡Bienvenido a Santa María del Oro!



En el municipio se encuentran comunidades <i>Wixárika</i>.

El nombre del lugar es en honor a la patrona del lugar “Santa María” y a las tres minas de oro del poblado.','7,8,9');
INSERT INTO `municipios` VALUES (1,4,'Tepic','¡Bienvenido a Tepic!



En Tepic habitan comunidades <i>Wixárika</i>, la más grande está en el asentamiento de la Zitacua, cuyo nombre significa “Lugar donde crece el maíz”. Las artesanías <i>Wixárika</i> son muy populares aquí.

El nombre del municipio tiene origen náhuatl, significa `Lugar de piedras macizas`.','10,11,12');
INSERT INTO `municipios` VALUES (1,5,'Santiago Ixcuintla','¡Bienvenido a Santiago Ixcuintla!

En este municipio se encuentran sitios turísticos muy importantes como la Isla de Mexcaltitán y el Parque Nacional Isla Isabel.
El nombre del lugar proviene del náhuatl y significa “Lugar de perros”.','13,14,15');
INSERT INTO `municipios` VALUES (1,6,'San Blas','¡Bienvenido a San Blas!

San Blas es una ciudad con mucha relevancia turística para Nayarit por sus playas, historia y gastronomía, como el típico pescado zarandeado y el pan de plantano.
El lugar fue uno de los principales puertos en el océano Pacífico, en la época de la colonia española.
El nombre del municipio es en honor a un fraile llamado Blas de Mendoza.','16,17,18,19');
INSERT INTO `municipios` VALUES (1,7,'JEFE_FINAL','JEFE_FINAL',NULL);
INSERT INTO `municipios` VALUES (2,8,'Valparaíso','¡Bienvenidos a Valparaíso!



En Valparaíso se encuentra la localidad de San Miguel, habitada por comunidades <i>Wixárika</i>.

El nombre del lugar es la conjunción de las palabras valle y paraíso que se interpretan como: Valparaíso.','20,21,22');
INSERT INTO `municipios` VALUES (2,9,'Fresnillo','¡Bienvenidos a Fresnillo!



En el municipio se encuentra la localidad de Plateros, donde descansan los <i>wixaritari</i> durante su peregrinación a <i>Wirikuta</i>.

Fresnillo significa fresno joven (árbol de madera dura y elástica).','23,24,25');
INSERT INTO `municipios` VALUES (2,10,'Zacatecas','¡Bienvenido a Zacatecas!



En Zacatecas se encuentra el Cerro del Padre, un sitio sagrado para el pueblo <i>Wixárika</i>.

El nombre del lugar significa “habitantes de la tierra donde abunda el zacate”.','26,27,28');
INSERT INTO `municipios` VALUES (2,11,'JEFE_FINAL','JEFE_FINAL',NULL);
INSERT INTO `municipios` VALUES (3,12,'Villa de Ramos','¡Bienvenido a Villa de Ramos!



En Charcas se encuentran Huahuatsari, Cuhixu Uheni y Tatei Matiniere, sitios sagrados para la cultura <i>Wixárika</i>.

El nombre del lugar se debe a que, en un domingo de ramos se descubrió una veta de metal en la zona.','29,30,31');
INSERT INTO `municipios` VALUES (3,13,'Santo Domingo','¡Bienvenido a Santo Domingo!



En el municipio se encuentra Maxa Yapa, un sitio sagrado para el pueblo <i>Wixárika</i>.

Santo Domingo debe su nombre al primer rancho asentado en la región.','32,33,34');
INSERT INTO `municipios` VALUES (3,14,'Charcas','¡Bienvenido a Charcas!



En Charcas se encuentran Tuy Mayau y Huacuri Quitenie, sitios sagrados para la cultura <i>Wixárika</i>.

El nombre del lugar lo impusieron los españoles, en referencia a una región minera de Bolivia, ya que en ambos lugares se extrae zinc, plata, cobre.','35,36,37');
INSERT INTO `municipios` VALUES (3,15,'Real de Catorce','¡Bienvenido a Real de Catorce!



En el municipio se encuentra <i>Wirikuta</i>, un sitio sagrado para el pueblo <i>Wixárika</i>.

Real de Catorce está forjado a partir de la cultura minera.','38,39,40');
INSERT INTO `municipios` VALUES (3,16,'JEFE_FINAL','JEFE_FINAL',NULL);
INSERT INTO `municipios` VALUES (4,17,'Mezquital','¡Bienvenido a Mezquital!



En Mezquital habitan la mayor parte de los grupos indígenas del estado, incluidos los <i>wixaritari</i>. 

El nombre del lugar es debido a la abundancia de arbustos del mismo nombre.','41,42,43');
INSERT INTO `municipios` VALUES (4,18,'Pueblo Nuevo','¡Bienvenido a Pueblo Nuevo!



En el municipio se encuentra la localidad de San Bernardino de Milpillas, habitada por comunidades <i>Wixárika</i>.

Se nombró como ``Pueblo Nuevo`` debido a que un grupo de pobladores cercanos llegaron a estas tierras, gracias al buen clima y la alta producción de frutas de la región.','44,45,46');
INSERT INTO `municipios` VALUES (4,19,'Durango','¡Bienvenido a Durango!



En el lugar se encuentra la localidad de Cinco de Mayo, habitada por <i>wixaritari</i>.

Durango significa “vega rodeada de agua y montañas”, otra versión dice que significa `más allá del agua`.','47,48,49');
INSERT INTO `municipios` VALUES (4,20,'Canatlán','¡Bienvenido a Canatlán!



En el municipio se encuentra el Cerro Gordo, un sitio sagrado para el pueblo <i>Wixárika</i>.

Canatlán proviene del náhuatl, significa “lugar con abundante agua”.','50,51,52');
INSERT INTO `municipios` VALUES (4,21,'JEFE_FINAL','JEFE_FINAL',NULL);
INSERT INTO `municipios` VALUES (5,22,'Huejuquilla','¡Bienvenido a Huejuquilla el Alto!



En Huejuquilla el Alto se encuentra la localidad de Colonia Hatmasie, habitada por comunidades <i>Wixárika</i>.

El nombre del lugar viene del náhuatl y se interpreta como `tierra de sauces verdes`.','53,54,55');
INSERT INTO `municipios` VALUES (5,23,'Mezquitic','¡Bienvenido a Mezquitic!



En Mezquitic habitan comunidades <i>Wixárika</i>, especialmente, en las localidades de Santa Catarina Cuexcomatitlán y San Sebastián Teponahuaxtlán.

El nombre del municipio proviene del náhuatl, significa “dentro de los mezquites”.','56,57,58');
INSERT INTO `municipios` VALUES (5,24,'Bolaños','¡Bienvenido a Bolaños!



En el municipio se encuentra la localidad de Tuxpan de Bolaños, habitada por comunidades <i>Wixárika</i>.

El nombre del lugar es en honor al conquistador Toribio de Bolaños.','59,60,61');
INSERT INTO `municipios` VALUES (5,25,'Chapala','¡Bienvenido a Chapala!



En el municipio se encuentra la Isla de los Alacranes, un sitio sagrado para la cultura <i>Wixárika</i>.

Chapala significa “lugar de chapulines sobre el agua” en náhuatl.','62,63,64');
INSERT INTO `municipios` VALUES (5,26,'JEFE_FINAL','JEFE_FINAL','');
INSERT INTO `municipios` VALUES (6,27,'Miiki','¡Bienvenido a Miiki!



Vence a todas las sombras','65,66');
INSERT INTO `municipios` VALUES (6,28,'Hewiixi','¡Bienvenido a Hewiixi!



Vence a todas las sombras','67,68');
INSERT INTO `municipios` VALUES (6,29,'Kieri','¡Bienvenido a Kieri!



Vence a todas las sombras','69,70');
INSERT INTO `municipios` VALUES (6,30,'JEFE_FINAL','JEFE_FINAL','');
INSERT INTO `plantas_medicinales` VALUES (1,'Agave','Agaves','Mai','Maite','','¡Recolectaste <i>mai</i> (agave)!
El <i>mai</i> (agave) es una planta medicinal que se utiliza como digestivo.','1,2');
INSERT INTO `plantas_medicinales` VALUES (2,'Aloe vera','Aloes vera',NULL,NULL,NULL,'¡Recolectaste aloe vera!
El aloe vera es una planta medicinal que se utiliza como desintoxicante.','3,4');
INSERT INTO `plantas_medicinales` VALUES (3,'Cebolla','Cebollas','Uyuri','Uyurite','Images/Wixarika/Alimentos/Cebolla mitad (1)','¡Recolectaste <i>uyuri</i> (cebolla)!
La <i>uyuri</i> (cebolla) es una planta medicinal que se utiliza para las cortadas.','5,6');
INSERT INTO `plantas_medicinales` VALUES (4,'Clavo','Clavos','Kɨrapu','Kɨrapuxi','Images/Wixarika/Planta_Medicinal/Clavo planta','¡Recolectaste <i>kɨrapu</i> (clavo)!
El <i>kɨrapu</i> (clavo) es una planta medicinal que se utiliza para el dolor de muelas.','7,8');
INSERT INTO `plantas_medicinales` VALUES (5,'Cola de caballo',NULL,NULL,NULL,NULL,'¡Recolectaste cola de caballo!
La cola de caballo es una planta medicinal que se utiliza para la caída de cabello .','9,10');
INSERT INTO `plantas_medicinales` VALUES (6,'Estafiate',NULL,NULL,NULL,NULL,'¡Recolectaste estafiate!
El estafiate es una planta medicinal que se utiliza para el dolor de estómago.','11,12');
INSERT INTO `plantas_medicinales` VALUES (7,'Eucalipto','Eucaliptos','Eɨkariti',NULL,'Images/Wixarika/Planta_Medicinal/Eucalipto','¡Recolectaste <i>eɨkariti</i> (eucalipto)!
El <i>eɨkariti</i> (eucalipto) es una planta medicinal que se utiliza para el resfriado.','13,14');
INSERT INTO `plantas_medicinales` VALUES (8,'Gordolobo','Gordolobos','Ɨrawe emɨtimariwe','Ɨrawetsixi ememɨtemamariwawe','Images/Wixarika/Planta_Medicinal/Óregano','¡Recolectaste <i>ɨrawe emɨtimariwe</i> (gordolobo)!
El <i>ɨrawe emɨtimariwe</i> (gordolobo) es una planta medicinal que se utiliza para la tos.','15,16');
INSERT INTO `plantas_medicinales` VALUES (9,'Hierbabuena','Hierbabuenas','Yervawena',NULL,'Images/Wixarika/Planta_Medicinal/Hierbabuena','¡Recolectaste <i>yervawena</i> (hierbabuena)!
La <i>yervawena</i> (hierbabuena) es una planta medicinal que se utiliza para los problemas estomacales.','17,18');
INSERT INTO `plantas_medicinales` VALUES (10,'Manzanilla','Manzanillas','Mantsaniya',NULL,'Images/Wixarika/Planta_Medicinal/Manzanila','¡Recolectaste <i>mantsaniya</i> (mazanilla)!
La <i>mantsaniya</i> (mazanilla) es una planta medicinal que se utiliza como antiinflamatorio.','19,20');
INSERT INTO `plantas_medicinales` VALUES (11,'Milpa','Milpas','Wáxa','Waxate','Images/Wixarika/Planta_Medicinal/Milpa','¡Recolectaste <i>wáxa</i> (milpa)!
La <i>wáxa</i> (milpa) es una planta medicinal que se utiliza como analgésico.','21,22');
INSERT INTO `plantas_medicinales` VALUES (12,'Orégano','Oréganos','Orekanu',NULL,'Images/Wixarika/Planta_Medicinal/Óregano','¡Recolectaste <i>orekanu</i> (orégano)!
El <i>orekanu</i> (orégano) es una planta medicinal que se utiliza para el dolor muscular y de oído.','23,34');
INSERT INTO `plantas_medicinales` VALUES (13,'Peyote','Peyotes','Hikuri','Hikurite','Images/Wixarika/Vegetacion/Peyote 1','¡Recolectaste <i>hikuri</i> (peyote)!
El <i>hikuri</i> (peyote) es una planta medicinal que se utiliza para el dolor de huesos.','25,26');
INSERT INTO `plantas_medicinales` VALUES (14,'Planta para dolor de cabeza o cuerpo cortado','Planta para dolor de cabeza o cuerpo cortado','Tɨpina huaki','Tɨpina huaki',NULL,'¡Recolectaste <i>tɨpina huaki</i>!
El <i>tɨpina huaki</i> es una planta medicinal que se utiliza para el dolor de cabeza.','27,28');
INSERT INTO `plantas_medicinales` VALUES (15,'Planta para dolor de estómago o empacho','Planta para dolor de estómago o empacho','Kɨpaixa (Xuriya kwitayari)','Yuriepakwiniya',NULL,'¡Recolectaste <i>kɨpaixa</i>!
La <i>kɨpaixa</i> es una planta medicinal que se utiliza para el empacho y el dolor de estómago.','29,30');
INSERT INTO `plantas_medicinales` VALUES (16,'Planta para dolor de estómago o empacho','Planta para dolor de estómago o empacho','Mutsirixa','Mutsirixate',NULL,'¡Recolectaste <i>mutsirixa</i>!
La <i>mutsirixa</i> es una planta medicinal que se utiliza para el empacho y el dolor de estómago.','31,32');
INSERT INTO `plantas_medicinales` VALUES (17,'Planta para huesos rotos','Planta para huesos rotos','Hapani','Hapanite',NULL,'¡Recolectaste <i>hapani</i>!
El <i>hapani</i> es una planta medicinal que se utiliza para los huesos rotos.','33,34');
INSERT INTO `plantas_medicinales` VALUES (18,'Planta para pintar el rostro','Plantas para pintar el rostro','Uxa','Uxáte','Images/Wixarika/Planta_Medicinal/Uxa','¡Recolectaste <i>uxa</i>!
La <i>uxa</i> es una planta que se utiliza para pintar el rostro.','35,36');
INSERT INTO `plantas_medicinales` VALUES (19,'Tepehuaje','Tepehuajes','Ɨpá','Haxi wexu','Images/Wixarika/Planta_Medicinal/Tepehuaje','¡Recolectaste <i>ɨpá</i> (tepehuaje)!
El <i>ɨpá</i> (tepehuaje) es una planta medicinal que se utiliza para el dolor de oído.','37,38');
INSERT INTO `plantas_medicinales` VALUES (20,'Tomillo','Tomillos',NULL,NULL,NULL,'¡Recolectaste tomillo!
El tomillo es una planta medicinal que se utiliza como antiséptico.','39,40');
INSERT INTO `conversaciones` VALUES (1,NULL,'NORMAL',1,'Masculino','Tiendero','Tiendas','<i>Yeikame</i> (viajero), si recibes daño de los animales salvajes o te enfermas, en la tienda de <i>panayewe</i> (salud) puedes obtener curaciones y remedios para incrementar tu barra de vida.','Continuar>-1','Images/Wixarika/Personajes/Iconos del Rostro/C Maestro 3x','1');
INSERT INTO `conversaciones` VALUES (2,NULL,'NORMAL',2,'Femenino','Tiendero','Tiendas','<i>Hamiku</i> (amigo), en la tienda de <i>ti’ikwaiwame</i> (alimentos) puedes obtener platillos para incrementar tu barra de energía.','Continuar>-1','Images/Wixarika/Personajes/Iconos del Rostro/C Niña 1 3x','2');
INSERT INTO `conversaciones` VALUES (3,NULL,'NORMAL',3,'Masculino','Tiendero','Tiendas','<i>Yeikame</i> (viajero), en la tienda de <i>patsierika</i> (intercambio) puedes cambiar los elementos que recolectes por <i>ikú </i>(maíz), con el que puedes comprar cosas.','Continuar>-1','Images/Wixarika/Personajes/Iconos del Rostro/C Maestro 3x','3');
INSERT INTO `conversaciones` VALUES (4,NULL,'NORMAL',4,'Femenino','Tiendero','Tiendas','<i>Hamiku</i> (amigo), en la tienda de <i>kemari</i> (vestuario) puedes obtener un <i>kemari</i> (traje) tradicional <i>Wixárika</i>, que te servirá para protegerte del sol.','Continuar>-1','Images/Wixarika/Personajes/Iconos del Rostro/C Niña 1 3x','4');
INSERT INTO `conversaciones` VALUES (5,NULL,'NORMAL',5,'Masculino','Tiendero','Tiendas','<i>Yeikame</i> (viajero), en la tienda de armas puedes obtener nuevas armas o mejorar las que tienes, para defenderte de las sombras y animales salvajes.','Continuar>-1','Images/Wixarika/Personajes/Iconos del Rostro/C Maestro 3x','5');
INSERT INTO `conversaciones` VALUES (6,NULL,'NORMAL',6,'Femenino','Tiendero','Tiendas','<i>Hamiku</i> (amigo), para hablar con una deidad debes llevarle una <i>mawari</i> (ofrenda).','Continuar>-1','Images/Wixarika/Personajes/Iconos del Rostro/C Niña 1 3x','6');
INSERT INTO `conversaciones` VALUES (7,'Del Nayar','NORMAL',7,'Masculino','Mara´kame','Autoridades','<i>Ke’aku</i> (hola) <i>hamiku</i> (amigo), yo soy el <i>mara’kame</i> (chamán) de esta comunidad.','Continuar>8','Images/Wixarika/Personajes/Iconos del Rostro/C Marakame 3x','7');
INSERT INTO `conversaciones` VALUES (8,'Del Nayar','NORMAL',7,'Masculino','Mara´kame','Autoridades','Me encargo de sanar el cuerpo, mente y corazón de los <i>wixaritari</i>. También dirijo las ceremonias sagradas y soy una autoridad en las comunidades.','Continuar>9','Images/Wixarika/Personajes/Iconos del Rostro/C Marakame 3x','8');
INSERT INTO `conversaciones` VALUES (9,'Del Nayar','NORMAL',7,'Masculino','Mara´kame','Autoridades','Tengo la importante misión de conservar y mantener vivas las tradiciones <i>Wixárika</i>.','Continuar>-1','Images/Wixarika/Personajes/Iconos del Rostro/C Marakame 3x','9');
INSERT INTO `conversaciones` VALUES (10,'Valparaíso','NORMAL',8,'Masculino','Xuku’uri ɨkame','Autoridades','<i>Ke’aku</i> (hola) <i>hamiku</i> (amigo), yo soy el <i>xuku’uri ɨkame</i> (jicarero) de esta comunidad.','Continuar>11','Images/Wixarika/Personajes/Iconos del Rostro/Jicarero','10');
INSERT INTO `conversaciones` VALUES (11,'Valparaíso','NORMAL',8,'Masculino','Xuku’uri ɨkame','Autoridades','Me encargo de cuidar el <i>tukipa</i> (centro ceremonial), de realizar las <i>neixa</i> (fiestas ceremoniales) y las peregrinaciones.','Continuar>12','Images/Wixarika/Personajes/Iconos del Rostro/Jicarero','11');
INSERT INTO `conversaciones` VALUES (12,'Valparaíso','NORMAL',8,'Masculino','Xuku’uri ɨkame','Autoridades','El cargo de <i>xuku’uri ɨkame</i> (jicarero) se hereda de los abuelos a nietos.','Continuar>-1','Images/Wixarika/Personajes/Iconos del Rostro/Jicarero','12');
INSERT INTO `conversaciones` VALUES (13,'Villa de Ramos','NORMAL',9,'Masculino','Kawiteru','Autoridades','<i>Ke’aku</i> (hola) <i>hamiku</i> (amigo), yo soy el <i>kawiteru</i> (anciano sabio) de esta comunidad.','Continuar>14','Images/Wixarika/Personajes/Iconos del Rostro/Anciano (1)','13');
INSERT INTO `conversaciones` VALUES (14,'Villa de Ramos','NORMAL',9,'Masculino','Kawiteru','Autoridades','Me encargo de guiar el buen camino de las comunidades <i>Wixárika</i>, de soñar a los nuevos funcionarios del gobierno comunal, y de elegirlos durante la ceremonia de cambio de h’itsɨ (varas o bastones de mando).','Continuar>15','Images/Wixarika/Personajes/Iconos del Rostro/Anciano (1)','14');
INSERT INTO `conversaciones` VALUES (15,'Villa de Ramos','NORMAL',9,'Masculino','Kawiteru','Autoridades','Un <i>kawiteru</i> (anciano sabio) es considerado como la encarnación de los ancestros, por ello, es la autoridad suprema en las comunidades.','Continuar>16','Images/Wixarika/Personajes/Iconos del Rostro/Anciano (1)','15');
INSERT INTO `conversaciones` VALUES (16,'Villa de Ramos','NORMAL',9,'Masculino','Kawiteru','Autoridades','Para ser un <i>kawiteru</i> debes ser el más anciano de la comunidad, y haber pasado por todos los cargos tradicionales.','Continuar>-1','Images/Wixarika/Personajes/Iconos del Rostro/Anciano (1)','16');
INSERT INTO `conversaciones` VALUES (17,'Mezquital','NORMAL',10,'Masculino','Tupiri','Autoridades','<i>Ke’aku</i> (hola) <i>hamiku</i> (amigo), yo soy el <i>tupiri</i> (policía) de esta comunidad.','Continuar>18','Images/Wixarika/Personajes/Iconos del Rostro/Policia (1)','17');
INSERT INTO `conversaciones` VALUES (18,'Mezquital','NORMAL',10,'Masculino','Tupiri','Autoridades','Me encargo de hacer cumplir las leyes comunales y de ser el mensajero de la comunidad.','Continuar>-1','Images/Wixarika/Personajes/Iconos del Rostro/Policia (1)','18');
INSERT INTO `conversaciones` VALUES (19,'Huejiquilla','NORMAL',11,'Masculino','Tatuwani','Autoridades','<i>Ke’aku</i> (hola) <i>hamiku</i> (amigo), yo soy el <i>tatuwani</i> (gobernador) de esta comunidad.','Continuar>20','Images/Wixarika/Personajes/Iconos del Rostro/Gobernador (1)','19');
INSERT INTO `conversaciones` VALUES (20,'Huejiquilla','NORMAL',11,'Masculino','Tatuwani','Autoridades','Me encargo de dirigir las comunidades y soy parte del gobierno comunal.','Continuar>21','Images/Wixarika/Personajes/Iconos del Rostro/Gobernador (1)','20');
INSERT INTO `conversaciones` VALUES (21,'Huejiquilla','NORMAL',11,'Masculino','Tatuwani','Autoridades','El <i>tatuwani </i> (gobernador) es elegido por los <i>kawiterutsixi</i> (ancianos sabios), quienes son las personas más sabías y respetadas de la comunidad.','Continuar>-1','Images/Wixarika/Personajes/Iconos del Rostro/Gobernador (1)','21');
INSERT INTO `conversaciones` VALUES (22,'Miiki','NORMAL',12,'Masculino','Hikuritame','Autoridades','<i>Ke’aku</i> (hola) <i>hamiku</i> (amigo), yo soy el <i>hikuritame</i> (peyotero) de esta comunidad.','Continuar>23','Images/Wixarika/Personajes/Iconos del Rostro/C Marakame 3x','22');
INSERT INTO `conversaciones` VALUES (23,'Miiki','NORMAL',12,'Masculino','Hikuritame','Autoridades','Me encargo de recolectar el peyote durante la peregrinación a <i>Wirikuta</i>.','Continuar>-1','Images/Wixarika/Personajes/Iconos del Rostro/C Marakame 3x','23');
INSERT INTO `conversaciones` VALUES (24,'SAMAO','NORMAL',13,'Masculino','Tamatzi Kauyumari','Deidades','¡<i>Ke’aku</i> (hola) <i>hamiku</i> (amigo)! Yo soy <i>Tamatzi Kauyumari</i> (Nuestro hermano mayor Venado Azul).','Continuar>25','Images/Wixarika/Personajes/Iconos del Rostro/C Venado Azul 3x','24');
INSERT INTO `conversaciones` VALUES (25,'SAMAO','NORMAL',13,'Masculino','Tamatzi Kauyumari','Deidades','Durante la peregrinación a <i>Wirikuta</i> marco el camino sagrado y me sacrifico para renacer en <i>hikuri</i> (peyote) que alimenta tu alma e <i>ikú</i> (maíz) que alimenta tu cuerpo.','Continuar>-1','Images/Wixarika/Personajes/Iconos del Rostro/C Venado Azul 3x','25');
INSERT INTO `conversaciones` VALUES (26,'San blas','NORMAL',14,'Femenino','Tatei Haramara','Deidades','¡<i>Hamiku</i> (amigo) llegaste con <i>Tatei Haramara</i> (Nuestra madre el mar)!','Continuar>27','Images/Wixarika/Personajes/Iconos del Rostro/C Tatei Haramara 3x','26');
INSERT INTO `conversaciones` VALUES (27,'San blas','NORMAL',14,'Femenino','Tatei Haramara','Deidades','El cielo es su cabello adornado de nubes y pájaros, el mar su vestido azul y la espuma de las olas es el encaje que lo adorna.','Continuar>28','Images/Wixarika/Personajes/Iconos del Rostro/C Tatei Haramara 3x','27');
INSERT INTO `conversaciones` VALUES (28,'San blas','NORMAL',14,'Femenino','Tatei Haramara','Deidades','<i>Tatei Haramara</i> da origen a las nubes y a la lluvia al chocar contra la roca blanca <i>Waxiewe</i> (blanco vapor) para convertirse en vapor.','Continuar>29','Images/Wixarika/Personajes/Iconos del Rostro/C Tatei Haramara 3x','28');
INSERT INTO `conversaciones` VALUES (29,'SAMAO','NORMAL',14,'Femenino','Tatei Haramara','Deidades','Para pedirle algo debes dejar tu <i>mawari</i> (ofrenda).','Continuar>-1','Images/Wixarika/Personajes/Iconos del Rostro/C Tatei Haramara 3x','29');
INSERT INTO `conversaciones` VALUES (30,'Valparaíso','NORMAL',15,'Femenino','Tatei Wexica Wimari','Deidades','¡<i>Ke’aku</i> (hola) <i>hamiku</i> (amigo)! Yo soy <i>Tatei Wexica Wimari</i> (Nuestra madre águila).','Continuar>31','Images/Wixarika/Animales/Aguila Real/Águila real 7','30');
INSERT INTO `conversaciones` VALUES (31,'Valparaíso','NORMAL',15,'Femenino','Tatei Wexica Wimari','Deidades','Soy hija de Tatewari (Nuestro abuelo fuego) y Tukutzi (Diosa de la tierra).','Continuar>-1','Images/Wixarika/Animales/Aguila Real/Águila real 7','31');
INSERT INTO `conversaciones` VALUES (32,'Zacatecas','NORMAL',16,'Ambos','Naɨrɨ/Takutsi Nakawé','Deidades','¡<i>Yeikame</i> (viajero) llegaste con <i>Naɨrɨ</i> (Dios del fuego primigenio) y <i>Takutsi Nakawé</i> (Diosa de la tierra)!','Continuar>33','Images/Wixarika/Personajes/Iconos del Rostro/Nairy','32');
INSERT INTO `conversaciones` VALUES (33,'Zacatecas','NORMAL',16,'Ambos','Naɨrɨ/Takutsi Nakawé','Deidades','Son la pareja primordial y divina destronada, que retorna momentáneamente al poder durante <i>namawita neixa</i> (fiesta del solsticio de verano), noche en la que se celebra el derrumbe de los pilares cósmicos y el retorno al caos original.','Continuar>34','Images/Wixarika/Personajes/Iconos del Rostro/Nakawe','33');
INSERT INTO `conversaciones` VALUES (34,'Zacatecas','NORMAL',16,'Ambos','Naɨrɨ/Takutsi Nakawé','Deidades','Para pedirle algo debes dejar tu <i>mawari</i> (ofrenda).','Continuar>-1','Images/Wixarika/Personajes/Iconos del Rostro/Nairy','34');
INSERT INTO `conversaciones` VALUES (35,'Santo Domingo','NORMAL',17,'Femenino','Tatei Kutsaraɨpa','Deidades','¡<i>Hamiku</i> (amigo) llegaste con <i>Tatei Kutsaraɨpa</i> (Nuestra madre agua sagrada)!','Continuar>36','Images/Wixarika/Personajes/Iconos del Rostro/Tatei Kutsaraɨpa 3x (1)','35');
INSERT INTO `conversaciones` VALUES (36,'Santo Domingo','NORMAL',17,'Femenino','Tatei Kutsaraɨpa','Deidades','Es la deidad del agua, se encuentra en Yoliátl, cerca de Salinas, San Luis Potosí. <i>Kutsaraɨpa</i> significa lugar mítico donde se reunieron los antepasados.','Continuar>37','Images/Wixarika/Personajes/Iconos del Rostro/Tatei Kutsaraɨpa 3x (1)','36');
INSERT INTO `conversaciones` VALUES (37,'Santo Domingo','NORMAL',17,'Femenino','Tatei Kutsaraɨpa','Deidades','Para pedirle algo debes dejar tu <i>mawari</i> (ofrenda).','Continuar>-1','Images/Wixarika/Personajes/Iconos del Rostro/Tatei Kutsaraɨpa 3x (1)','37');
INSERT INTO `conversaciones` VALUES (38,'Real de catorce','NORMAL',18,'Masculino','Tututzi Maxa Kwaxi','Deidades','¡<i>Hamiku</i> (amigo) llegaste con <i>Tututzi Maxa Kwaxi</i> (Nuestro bisabuelo Cola de venado)!','Continuar>39','Images/Wixarika/Personajes/Iconos del Rostro/C Marakame 3x','38');
INSERT INTO `conversaciones` VALUES (39,'Real de catorce','NORMAL',18,'Masculino','Tututzi Maxa Kwaxi','Deidades','Es la deidad suprema, representa al universo mismo, es la esencia que se encuentra en cualquier substancia material o inmaterial.','Continuar>40','Images/Wixarika/Personajes/Iconos del Rostro/C Venado Azul 3x','39');
INSERT INTO `conversaciones` VALUES (40,'Real de catorce','NORMAL',18,'Masculino','Tututzi Maxa Kwaxi','Deidades','Para pedirle algo debes dejar tu <i>mawari</i> (ofrenda).','Continuar>-1','Images/Wixarika/Personajes/Iconos del Rostro/C Marakame 3x','40');
INSERT INTO `conversaciones` VALUES (41,'Real de catorce','NORMAL',19,'Masculino','Tayau','Deidades','¡<i>Yeikame</i> (viajero) llegaste con <i>Tayau</i> (Nuestro padre el Sol)!','Continuar>42','Images/Wixarika/Personajes/Iconos del Rostro/Tayau (2)','41');
INSERT INTO `conversaciones` VALUES (42,'Real de catorce','NORMAL',19,'Masculino','Tayau','Deidades','Es la divinidad del sol que nace en <i>Wirikuta</i> y muere en <i>Hauxa Manaka</i>. Durante el día viaja por el cielo, se sienta en su silla de oro al medio día.','Continuar>43','Images/Wixarika/Personajes/Iconos del Rostro/Tayau (2)','42');
INSERT INTO `conversaciones` VALUES (43,'Real de catorce','NORMAL',19,'Masculino','Tayau','Deidades','Para pedirle algo debes dejar tu <i>mawari</i> (ofrenda).','Continuar>-1','Images/Wixarika/Personajes/Iconos del Rostro/Tayau (2)','43');
INSERT INTO `conversaciones` VALUES (44,'Canatlán','NORMAL',20,'Femenino','Tatei Yurienáka','Deidades','¡<i>Hamiku</i> (amigo) llegaste con <i>Tatei Yurienáka</i> (Nuestra madre tierra)!','Continuar>45','Images/Wixarika/Personajes/Iconos del Rostro/C Tatei Yurienáka 3x','44');
INSERT INTO `conversaciones` VALUES (45,'Canatlán','NORMAL',20,'Femenino','Tatei Yurienáka','Deidades','Es la “madre tierra”, responsable de la fertilidad del suelo. Es representada con un cántaro de barro.','Continuar>46','Images/Wixarika/Personajes/Iconos del Rostro/C Tatei Yurienáka 3x','45');
INSERT INTO `conversaciones` VALUES (46,'Canatlán','NORMAL',20,'Femenino','Tatei Yurienáka','Deidades','Para pedirle algo debes dejar tu <i>mawari</i> (ofrenda).','Continuar>-1','Images/Wixarika/Personajes/Iconos del Rostro/C Tatei Yurienáka 3x','46');
INSERT INTO `conversaciones` VALUES (47,'Mezquitic','NORMAL',21,'Masculino','Tatewari','Deidades','¡<i>Yeikame</i> (viajero) llegaste con <i>Tatewari</i> (Nuestro abuelo fuego)!','Continuar>48','Images/Wixarika/Personajes/Iconos del Rostro/C Tatewari 3x','47');
INSERT INTO `conversaciones` VALUES (48,'Mezquitic','NORMAL',21,'Masculino','Tatewari','Deidades','Es un antepasado divinizado, una de las deidades más antiguas. Se le considera como el primer <i>mara’kame</i> (chamán), por ello, es la deidad tutelar de los <i>mara’kate</i> (chamanes).','Continuar>49','Images/Wixarika/Personajes/Iconos del Rostro/C Tatewari 3x','48');
INSERT INTO `conversaciones` VALUES (49,'Mezquitic','NORMAL',21,'Masculino','Tatewari','Deidades','También se le conoce como “el gran transformador” que cambia frío a caliente, crudo a cocido, oscuridad a luz, sólido a cenizas, tierra árida en terreno fértil.','Continuar>50','Images/Wixarika/Personajes/Iconos del Rostro/C Tatewari 3x','49');
INSERT INTO `conversaciones` VALUES (50,'Mezquitic','NORMAL',21,'Masculino','Tatewari','Deidades','Para pedirle algo debes dejar tu <i>mawari</i> (ofrenda).','Continuar>-1','Images/Wixarika/Personajes/Iconos del Rostro/C Tatewari 3x','50');
INSERT INTO `conversaciones` VALUES (51,'Chapala','NORMAL',22,'Femenino','Tatei Xapawiyeme','Deidades','¡<i>Hamiku</i> (amigo) llegaste con <i>Tatei Xapawiyeme</i> (Diosa madre del sur)!','Continuar>52','Images/Wixarika/Personajes/Iconos del Rostro/C Niña 1 3x','51');
INSERT INTO `conversaciones` VALUES (52,'Chapala','NORMAL',22,'Femenino','Tatei Xapawiyeme','Deidades','Es la “madre lluvia”, que se transforma en nubes, lluvia, manantiales, truenos, rayos y hasta en cristales de roca.','Continuar>53','Images/Wixarika/Personajes/Iconos del Rostro/C Niña 1 3x','52');
INSERT INTO `conversaciones` VALUES (53,'Chapala','NORMAL',22,'Femenino','Tatei Xapawiyeme','Deidades','Para pedirle algo debes dejar tu <i>mawari</i> (ofrenda).','Continuar>-1','Images/Wixarika/Personajes/Iconos del Rostro/C Niña 1 3x','53');
INSERT INTO `conversaciones` VALUES (54,'Kieri','NORMAL',23,'Masculino','T’kákame','Deidades','¡<i>Yekaime</i> (viajero) cuidado con <i>Tukákame</i> (Chupa sangre)!','Continuar>55','Images/Wixarika/Animales/Murcielago/Murcielago 1','54');
INSERT INTO `conversaciones` VALUES (55,'Kieri','NORMAL',23,'Masculino','T’kákame','Deidades','Es un murciélago que representa a la muerte. Se adorna con los huesos de difuntos, y le gusta hacer que las personas se pierdan para quitarles la vida.','Continuar>-1','Images/Wixarika/Animales/Murcielago/Murcielago 1','55');
INSERT INTO `conversaciones` VALUES (56,'Del Nayar','NORMAL',24,'Masculino','Agricultor','Ocupaciones','¡Hola <i>hamiku</i> (amigo)! Yo soy un <i>muka’etsa</i> (agricultor).','Continuar>57','Images/Wixarika/Personajes/Iconos del Rostro/Agricultor','56');
INSERT INTO `conversaciones` VALUES (57,'Del Nayar','NORMAL',24,'Masculino','Agricultor','Ocupaciones','Me encargo de cultivar los coamiles, un área de tierra donde se siembra maíz, frijoles, chiles, calabacitas y flores de cempasúchil.','Continuar>58','Images/Wixarika/Personajes/Iconos del Rostro/Agricultor','57');
INSERT INTO `conversaciones` VALUES (58,'Del Nayar','NORMAL',24,'Masculino','Agricultor','Ocupaciones','Las flores de cempasúchil se siembran porque son un plaguicida natural, los frijoles para que aporten nitrógeno al suelo, y las calabacitas para que sus hojas protejan al suelo de la erosión.','Continuar>-1','Images/Wixarika/Personajes/Iconos del Rostro/Agricultor','58');
INSERT INTO `conversaciones` VALUES (59,'La Yesca','NORMAL',25,'Masculino','Cazador','Ocupaciones','¡Hola <i>hamiku</i> (amigo)! Yo soy un <i>tiweweiyame</i> (cazador).','Continuar>60','Images/Wixarika/Personajes/Iconos del Rostro/Cazador (1)','59');
INSERT INTO `conversaciones` VALUES (60,'La Yesca','NORMAL',25,'Masculino','Cazador','Ocupaciones','Me encargo de cazar animales, principalmente, para nuestros rituales sagrados, y, en menor medida para la subsistencia alimenticia.','Continuar>61','Images/Wixarika/Personajes/Iconos del Rostro/Cazador (1)','60');
INSERT INTO `conversaciones` VALUES (61,'La Yesca','NORMAL',25,'Masculino','Cazador','Ocupaciones','Con mi arco y flechas cazo venados, jabalíes y ardillas.','Continuar>-1','Images/Wixarika/Personajes/Iconos del Rostro/Cazador (1)','61');
INSERT INTO `conversaciones` VALUES (62,'SAMAO','NORMAL',26,'Masculino','Artesano','Ocupaciones','¡Hola <i>hamiku</i> (amigo)! Yo soy un <i>titsatsaweme</i> (artesano).','Continuar>63','Images/Wixarika/Personajes/Iconos del Rostro/Artesano','62');
INSERT INTO `conversaciones` VALUES (63,'SAMAO','NORMAL',26,'Masculino','Artesano','Ocupaciones','Me encargo de elaborar los <i>kemarite</i> (trajes tradicionales Wixárika), para los <i>wixaritari</i>.','Continuar>64','Images/Wixarika/Personajes/Iconos del Rostro/Artesano','63');
INSERT INTO `conversaciones` VALUES (64,'SAMAO','NORMAL',26,'Masculino','Artesano','Ocupaciones','Un <i>kemari</i> (traje tradicional Wixárika) se elabora de manta blanca y se borda con estambre, plasmando figuras que representen al <i>wixaritari</i> que lo utilizará.','Continuar>-1','Images/Wixarika/Personajes/Iconos del Rostro/Artesano','64');
INSERT INTO `conversaciones` VALUES (65,'Tepic','NORMAL',27,'Masculino','Músico','Ocupaciones','¡Hola <i>hamiku</i> (amigo)! Yo soy un <i>tiyuitɨwame</i> (músico).','Continuar>66','Images/Wixarika/Personajes/Iconos del Rostro/C Musico 3x','65');
INSERT INTO `conversaciones` VALUES (66,'Tepic','NORMAL',27,'Masculino','Músico','Ocupaciones','Me encargo de interpretar la música tradicional <i>Wixárika</i>, que tiene tres géneros: <i>wawi</i> (canto sagrado), <i>xaweri</i> (música tradicional) y <i>teiwari</i> (música regional).','Continuar>67','Images/Wixarika/Personajes/Iconos del Rostro/C Musico 3x','66');
INSERT INTO `conversaciones` VALUES (67,'Tepic','NORMAL',27,'Masculino','Músico','Ocupaciones','Si encuentras un <i>tiyuitɨwame</i> (músico) pídele una <i>kwikarí</i> (canción).','Continuar>-1','Images/Wixarika/Personajes/Iconos del Rostro/C Musico 3x','67');
INSERT INTO `conversaciones` VALUES (68,'Valparaíso','NORMAL',28,'Masculino','Agricultor','Ocupaciones','¡Hola <i>hamiku</i> (amigo)! Yo soy un <i>muka’etsa</i> (agricultor).','Continuar>69','Images/Wixarika/Personajes/Iconos del Rostro/Agricultor','68');
INSERT INTO `conversaciones` VALUES (69,'Valparaíso','NORMAL',28,'Masculino','Agricultor','Ocupaciones','Me encargo de cultivar los coamiles, donde siembro <i>ikú</i> (maíz), <i>múmete</i> (frijoles), <i>kukuríte</i> (chiles), <i>xutsíte</i> (calabacitas) y flores de cempasúchil.','Continuar>-1','Images/Wixarika/Personajes/Iconos del Rostro/Agricultor','69');
INSERT INTO `conversaciones` VALUES (70,'Fresnillo','NORMAL',29,'Masculino','Cazador','Ocupaciones','¡Hola <i>hamiku</i> (amigo)! Yo soy un <i>tiweweiyame</i> (cazador).','Continuar>71','Images/Wixarika/Personajes/Iconos del Rostro/Cazador (1)','70');
INSERT INTO `conversaciones` VALUES (71,'Fresnillo','NORMAL',29,'Masculino','Cazador','Ocupaciones','Me encargo de cazar al venado cola blanca durante nuestra peregrinación a <i>Wirikuta</i>.','Continuar>72','Images/Wixarika/Personajes/Iconos del Rostro/Cazador (1)','71');
INSERT INTO `conversaciones` VALUES (72,'Fresnillo','NORMAL',29,'Masculino','Cazador','Ocupaciones','La cacería ritual del venado cola blanca es indispensable para que los <i>wixaritari</i> podamos realizar nuestros rituales sagrados.','Continuar>-1','Images/Wixarika/Personajes/Iconos del Rostro/Cazador (1)','72');
INSERT INTO `conversaciones` VALUES (73,'Villa de Ramos','NORMAL',30,'Femenino','Artesana','Ocupaciones','¡Hola <i>hamiku</i> (amigo)! Yo soy una <i>titsatsaweme</i> (artesana).','Continuar>74','Images/Wixarika/Iconos/Artesana','73');
INSERT INTO `conversaciones` VALUES (74,'Villa de Ramos','NORMAL',30,'Femenino','Artesana','Ocupaciones','Me encargo de elaborar artesanías de chaquira como: <i>kuka tiwameté</i> (collares), <i>matsiwate</i> (pulseras) y <i>nakɨtsate</i> (aretes).','Continuar>75','Images/Wixarika/Iconos/Artesana','74');
INSERT INTO `conversaciones` VALUES (75,'Villa de Ramos','NORMAL',30,'Femenino','Artesana','Ocupaciones','También, elaboro kɨtsiɨrite (morrales) de estambre.','Continuar>-1','Images/Wixarika/Iconos/Artesana','75');
INSERT INTO `conversaciones` VALUES (76,'Santo Domingo','NORMAL',31,'Masculino','Músico','Ocupaciones','¡Hola <i>hamiku</i> (amigo)! Yo soy un <i>tiyuitɨwame</i> (músico).','Continuar>77','Images/Wixarika/Personajes/Iconos del Rostro/C Musico 3x','76');
INSERT INTO `conversaciones` VALUES (77,'Santo Domingo','NORMAL',31,'Masculino','Músico','Ocupaciones','Me encargo de interpretar la música tradicional <i>Wixárika</i>, que tiene sones de <i>xaweri</i> (violín) y <i>kanari</i> (guitarra), los cuales se acompañan con versos improvisados y danza zapateada.','Continuar>78','Images/Wixarika/Personajes/Iconos del Rostro/C Musico 3x','77');
INSERT INTO `conversaciones` VALUES (78,'Santo Domingo','NORMAL',31,'Masculino','Músico','Ocupaciones','Actualmente, el conjunto <i>Wixárika</i> más famoso es el Venado Azul de Nueva Colonia, Jalisco.','Continuar>-1','Images/Wixarika/Personajes/Iconos del Rostro/C Musico 3x','78');
INSERT INTO `conversaciones` VALUES (79,'Mezquital','NORMAL',32,'Masculino','Agricultor','Ocupaciones','¡Hola <i>hamiku</i> (amigo)! Yo soy un <i>muka’etsa</i> (agricultor).','Continuar>80','Images/Wixarika/Personajes/Iconos del Rostro/Agricultor','79');
INSERT INTO `conversaciones` VALUES (80,'Mezquital','NORMAL',32,'Masculino','Agricultor','Ocupaciones','En el coamil siembro flores de cempasúchil porque son un plaguicida natural, también, siembro <i>múmete</i> (frijoles) para que aporten nitrógeno al suelo y <i>xutsíte</i> (calabacitas) para que sus hojas protejan al suelo de la erosión.','Continuar>-1','Images/Wixarika/Personajes/Iconos del Rostro/Agricultor','80');
INSERT INTO `conversaciones` VALUES (81,'Pueblo Nuevo','NORMAL',33,'Masculino','Cazador','Ocupaciones','¡Hola <i>hamiku</i> (amigo)! Yo soy un <i>tiweweiyame</i> (cazador).','Continuar>82','Images/Wixarika/Personajes/Iconos del Rostro/Cazador (1)','81');
INSERT INTO `conversaciones` VALUES (82,'Pueblo Nuevo','NORMAL',33,'Masculino','Cazador','Ocupaciones','Los <i>wixaritari</i> cazamos principalmente para realizar nuestros rituales sagrados.','Continuar>83','Images/Wixarika/Personajes/Iconos del Rostro/Cazador (1)','82');
INSERT INTO `conversaciones` VALUES (83,'Pueblo Nuevo','NORMAL',33,'Masculino','Cazador','Ocupaciones','La cacería ritual del <i>maxa</i> (venado) es una tradición escencial para nuestra cultura.','Continuar>-1','Images/Wixarika/Personajes/Iconos del Rostro/Cazador (1)','83');
INSERT INTO `conversaciones` VALUES (84,'Durango','NORMAL',34,'Femenino','Artesana','Ocupaciones','¡Hola <i>hamiku</i> (amigo)! Yo soy una <i>titsatsaweme</i> (artesana).','Continuar>85','Images/Wixarika/Personajes/Iconos del Rostro/C Niña 1 3x','84');
INSERT INTO `conversaciones` VALUES (85,'Durango','NORMAL',34,'Femenino','Artesana','Ocupaciones','Me encargo de elaborar los <i>kemarite</i> (trajes tradicionales Wixárika).','Continuar>86','Images/Wixarika/Personajes/Iconos del Rostro/C Niña 1 3x','85');
INSERT INTO `conversaciones` VALUES (86,'Durango','NORMAL',34,'Femenino','Artesana','Ocupaciones','Los hombres visten un <i>xaweruxi</i> (pantalón) y <i>kamixa</i> (camisa) de manta, bordados con figuras de estambre. También, llevan <i>xupureru</i> (sombrero), <i>kɨtsiɨri</i> (morral), <i>hɨiyame</i> (faja) y <i>kakaíte</i> (huaraches).','Continuar>-1','Images/Wixarika/Personajes/Iconos del Rostro/C Niña 1 3x','86');
INSERT INTO `conversaciones` VALUES (87,'Huejiquilla','NORMAL',35,'Masculino','Músico','Ocupaciones','¡Hola <i>hamiku</i> (amigo)! Yo soy un <i>tiyuitɨwame</i> (músico).','Continuar>88','Images/Wixarika/Personajes/Iconos del Rostro/C Musico 3x','87');
INSERT INTO `conversaciones` VALUES (88,'Huejiquilla','NORMAL',35,'Masculino','Músico','Ocupaciones','Los <i>kanarite</i> (instrumentos) tradicionales <i>Wixárika</i> son el <i>xaberi</i> (pequeño violín) y el <i>kanari</i> (pequeña guitarra), también se utilizan las <i>kaitsa</i> (maracas).','Continuar>89','Images/Wixarika/Personajes/Iconos del Rostro/C Musico 3x','88');
INSERT INTO `conversaciones` VALUES (89,'Huejiquilla','NORMAL',35,'Masculino','Músico','Ocupaciones','Los <i>kanarite</i> (instrumentos) son de fabricación autóctona.','Continuar>-1','Images/Wixarika/Personajes/Iconos del Rostro/C Musico 3x','89');
INSERT INTO `conversaciones` VALUES (90,'Bolaños','NORMAL',36,'Femenino','Artesana','Ocupaciones','¡Hola <i>hamiku</i> (amigo)! Yo soy una <i>titsatsaweme</i> (artesana).','Continuar>91','Images/Wixarika/Iconos/Artesana','90');
INSERT INTO `conversaciones` VALUES (91,'Bolaños','NORMAL',36,'Femenino','Artesana','Ocupaciones','Me encargo de elaborar los <i>kemarite</i> (trajes tradicionales Wixárika).','Continuar>92','Images/Wixarika/Iconos/Artesana','91');
INSERT INTO `conversaciones` VALUES (92,'Bolaños','NORMAL',36,'Femenino','Artesana','Ocupaciones','Las mujeres visten <i>íwi</i> (falda), <i>kamixa</i> (camisa) y <i>xapatuxite</i> (zapatos). También, utilizan <i>kuka tiwameté</i> (collares), <i>matsiwate</i> (pulseras), <i>nakɨtsate</i> (aretes) y  <i>kɨtsiɨri</i> (morral).','Continuar>-1','Images/Wixarika/Iconos/Artesana','92');
INSERT INTO `conversaciones` VALUES (93,'Santiago Ixcuintla','NORMAL',37,'Femenino','Aldeana','Festividades','<i>Yekaime</i> (viajero), en la cultura <i>Wixárika</i> realizamos <i>neixa</i> (fiestas ceremoniales) para celebrar el ciclo del cultivo de maíz.','Continuar>94','Images/Wixarika/Personajes/Iconos del Rostro/C Niña 1 3x','93');
INSERT INTO `conversaciones` VALUES (94,'Santiago Ixcuintla','NORMAL',37,'Femenino','Aldeana','Festividades','Las principales <i>neixa</i> (fiestas ceremoniales) son: <i>hikuli neixa</i> (preparación del coamil), <i>namawita neixa</i> (siembra) y <i>tatei neixa</i> (cosecha de los primeros frutos).','Continuar>95','Images/Wixarika/Personajes/Iconos del Rostro/C Niña 1 3x','94');
INSERT INTO `conversaciones` VALUES (95,'Santiago Ixcuintla','NORMAL',37,'Femenino','Aldeana','Festividades','Las <i>neixa</i> (fiestas ceremoniales) se realizan en los <i>tukipa</i> (centros ceremoniales).','Continuar>-1','Images/Wixarika/Personajes/Iconos del Rostro/C Niña 1 3x','95');
INSERT INTO `conversaciones` VALUES (96,'Charcas','NORMAL',38,'Masculino','Aldeano','Festividades','Durante las <i>neixa</i> (fiestas ceremoniales), el <i>mara’kame</i> (chamán) establece contacto con las deidades por medio del <i>wawi</i> (canto chamánico), las <i>muwierite</i> (varas emplumadas), los <i>xikɨri</i> (espejos) y los cristales.','Continuar>97','Images/Wixarika/Personajes/Iconos del Rostro/C Maestro 3x','96');
INSERT INTO `conversaciones` VALUES (97,'Charcas','NORMAL',38,'Masculino','Aldeano','Festividades','Para pedirles que no envíen enfermedades y ayuden a resolver los problemas de la comunidad.','Continuar>-1','Images/Wixarika/Personajes/Iconos del Rostro/C Maestro 3x','97');
INSERT INTO `conversaciones` VALUES (98,'Canatlán','NORMAL',39,'Femenino','Aldeana','Festividades','La fiesta ceremonial <i>tatei neixa</i> (La danza de nuestras madres), conocida también como la fiesta del <i>tepu</i> (tambor), celebra la cosecha de los primeros frutos, es para despedir a las lluvias, una vez que las milpas han crecido y los elotes están tiernos.','Continuar>99','Images/Wixarika/Personajes/Iconos del Rostro/C Niña 1 3x','98');
INSERT INTO `conversaciones` VALUES (99,'Canatlán','NORMAL',39,'Femenino','Aldeana','Festividades','En esta celebración se preparan <i>tétsute</i> (tamales) y <i>nawá</i> (tejuino), y los niños y niñas reciben los primeros frutos, pues ambos están tiernos.','Continuar>-1','Images/Wixarika/Personajes/Iconos del Rostro/C Niña 1 3x','99');
INSERT INTO `conversaciones` VALUES (100,'Mezquitic','NORMAL',40,'Masculino','Aldeano','Festividades','La fiesta del <i>tepu</i> (tambor) es una celebración para los niños y niñas, en la que se hace una peregrinación imaginaria a <i>Wirikuta</i> con ayuda del <i>mara’kame</i> (chamán), para que el <i>kupuri</i> (aliento) se asiente en el cuerpo de los infantes.','Continuar>101','Images/Wixarika/Personajes/Iconos del Rostro/C Maestro 3x','100');
INSERT INTO `conversaciones` VALUES (101,'Mezquitic','NORMAL',40,'Masculino','Aldeano','Festividades','Para que puedan viajar, los niños son convertidos simbólicamente en <i>kwixɨ</i> (águila). Si los niños y niñas hacen correctamente el viaje imaginario a <i>Wirikuta</i> se ganan un <i>ikɨri</i> (elote) tatemado.','Continuar>-1','Images/Wixarika/Personajes/Iconos del Rostro/C Maestro 3x','101');
INSERT INTO `conversaciones` VALUES (102,'Chapala','NORMAL',41,'Femenino','Aldeana','Festividades','En la fiesta del <i>tepu</i> (tambor) las madres elaboran elementos que los niños y niñas usarán durante la celebración como: <i>kaitsate</i> (sonajas), <i>tsikɨrite</i> (ojos de dios) y <i>ɨ’rɨte</i> (flechas). Estos elementos son bendecidos por el <i>mara’kame</i> (chamán).','Continuar>103','Images/Wixarika/Personajes/Iconos del Rostro/C Niña 1 3x','102');
INSERT INTO `conversaciones` VALUES (103,'Chapala','NORMAL',41,'Femenino','Aldeana','Festividades','Las familias deben realizar el festejo en el <i>tukipa</i> (centro ceremonial) de origen del abuelo materno del niño o niña.','Continuar>-1','Images/Wixarika/Personajes/Iconos del Rostro/C Niña 1 3x','103');
INSERT INTO `conversaciones` VALUES (104,'San blas','OFRENDAS',43,'Femenino','Aldeana','Ofrendas','<i>Yekaime</i> (viajero), en la cultura <i>Wixárika</i> elaboramos <i>mawarite</i> (ofrendas) para nuestras deidades.','Continuar>105','Images/Wixarika/Personajes/Iconos del Rostro/C Niña 1 3x','104');
INSERT INTO `conversaciones` VALUES (105,'San blas','OFRENDAS',43,'Femenino','Aldeana','Ofrendas','Las <i>mawarite</i> (ofrendas) son objetos espirituales que dejamos en los <i>tukipa</i> (centros ceremoniales) para agradecer o hacer peticiones a nuestras deidades.','Continuar>-1','Images/Wixarika/Escenario/Casas/Tukipa','105');
INSERT INTO `conversaciones` VALUES (106,'Zacatecas','OFRENDAS',44,'Masculino','Aldeano','Ofrendas','<i>Hamiku</i> (amigo), las <i>mawarite</i> (ofrendas) que elaboramos para agradecer o hacer peticiones a nuestras deidades son:','Continuar>107','Images/Wixarika/Personajes/Iconos del Rostro/C Maestro 3x','106');
INSERT INTO `conversaciones` VALUES (107,'Zacatecas','OFRENDAS',44,'Masculino','Aldeano','Ofrendas','<i>Ɨ’rɨte</i> (flechas), <i>xuku’urite</i> (jícaras), <i>tsikwaki nierikaya</i> (máscaras), <i>tsikɨrite</i> (ojos de dios) y <i>nierikate</i> (tablillas).','Continuar>-1','Images/Wixarika/Objetos_Espirituales/Jícara','107');
INSERT INTO `conversaciones` VALUES (108,'Real de catorce','OFRENDAS',45,'Femenino','Aldeana','Ofrendas','<i>Yekaime</i> (viajero), las <i>mawarite</i> (ofrendas) las presentamos para agradecer o pedir algo a las deidades.','Continuar>109','Images/Wixarika/Personajes/Iconos del Rostro/C Niña 1 3x','108');
INSERT INTO `conversaciones` VALUES (109,'Real de catorce','OFRENDAS',45,'Femenino','Aldeana','Ofrendas','Los agradecimientos o plegarias se plasman con pintura, estambre, plumas u objetos en miniatura.','Continuar>-1','Images/Wixarika/Materiales/Pluma 1 3x','109');
INSERT INTO `conversaciones` VALUES (110,'San blas','OFRENDAS',46,'Masculino','Mara´kame','Ojo de dios','¡<i>Hamiku</i> (amigo), hice un <i>tsikɨri</i> (ojo de dios)!','Continuar>111','Images/Wixarika/Personajes/Iconos del Rostro/C Marakame 3x','110');
INSERT INTO `conversaciones` VALUES (111,'San blas','OFRENDAS',46,'Masculino','Mara´kame','Ojo de dios','El <i>tsikɨri</i> (ojo de dios) es un objeto espiritual que simboliza los cinco puntos cardinales del <i>Wixárika</i>: norte, sur, este, oeste y centro.','Continuar>112','Images/Wixarika/Objetos_Espirituales/Ojo de dios','111');
INSERT INTO `conversaciones` VALUES (112,'San blas','OFRENDAS',46,'Masculino','Mara´kame','Ojo de dios','El <i>tsikɨri</i> (ojo de dios) se ofrenda a las deidades para pedir por el buen crecimiento de los niños.','Continuar>113','Images/Wixarika/Objetos_Espirituales/Ojo de dios','112');
INSERT INTO `conversaciones` VALUES (113,'San blas','OFRENDAS',46,'Masculino','Mara´kame','Ojo de dios','También, se considera como un escudo protector para los peregrinos y sirve para ver y entender las cosas desconocidas.','Continuar>-1','Images/Wixarika/Personajes/Iconos del Rostro/C Marakame 3x','113');
INSERT INTO `conversaciones` VALUES (114,'Zacatecas','OFRENDAS',47,'Masculino','Jicarero','Máscara','¡<i>Yekaime</i> (viajero), hice una <i>tsikwaki nierikaya</i> (máscara)!','Continuar>115','Images/Wixarika/Personajes/Iconos del Rostro/C Marakame 3x','114');
INSERT INTO `conversaciones` VALUES (115,'Zacatecas','OFRENDAS',47,'Masculino','Jicarero','Máscara','La <i>tsikwaki nierikaya</i> (máscara) es un objeto espiritual que representa un momento imprescindible en la cultura <i>Wixárika</i>: el contacto con el mestizo.','Continuar>116','Images/Wixarika/Objetos_Espirituales/Máscara','115');
INSERT INTO `conversaciones` VALUES (116,'Zacatecas','OFRENDAS',47,'Masculino','Jicarero','Máscara','La <i>tsikwaki nierikaya</i> (máscara) personifica a dioses asociados con los mestizos.','Continuar>-1','Images/Wixarika/Objetos_Espirituales/Máscara','116');
INSERT INTO `conversaciones` VALUES (117,'Real de catorce','OFRENDAS',48,'Masculino','Anciano','Tablilla','¡<i>Hamiku</i> (amigo), hice una <i>nierika</i> (tablilla de estambre)!','Continuar>118','Images/Wixarika/Personajes/Iconos del Rostro/Anciano (1)','117');
INSERT INTO `conversaciones` VALUES (118,'Real de catorce','OFRENDAS',48,'Masculino','Anciano','Tablilla','La <i>nierika</i> (tablilla de estambre) es un objeto espiritual que representa el mundo de las deidades y la cosmovisión del pueblo <i>Wixárika</i>, están inspiradas en sueños o visiones.','Continuar>119','Images/Wixarika/Objetos_Espirituales/Tablilla','118');
INSERT INTO `conversaciones` VALUES (119,'Real de catorce','OFRENDAS',48,'Masculino','Anciano','Tablilla','La <i>nierika</i> (tablilla de estambre) se utiliza para conocer el estado oculto o auténtico de las cosas, pues es un “instrumento para ver”.','Continuar>-1','Images/Wixarika/Objetos_Espirituales/Tablilla','119');
INSERT INTO `conversaciones` VALUES (120,'Canatlán','OFRENDAS',49,'Masculino','Policía','Jícara','¡<i>Yekaime</i> (viajero), hice una <i>xukúri</i> (jícara)!','Continuar>121','Images/Wixarika/Personajes/Iconos del Rostro/Policia (1)','120');
INSERT INTO `conversaciones` VALUES (121,'Canatlán','OFRENDAS',49,'Masculino','Policía','Jícara','La <i>xukúri</i> (jícara) es un objeto espiritual que representa a la <i>’uka</i> (mujer), a lo femenino, es símbolo de depósito de vida pues en ella se transportan agua y semillas.','Continuar>122','Images/Wixarika/Objetos_Espirituales/Jícara','121');
INSERT INTO `conversaciones` VALUES (122,'Canatlán','OFRENDAS',49,'Masculino','Policía','Jícara','La <i>xukúri</i> (jícara) se ofrenda a las deidades y se utiliza para depositar la sangre de los animales sacrificados.','Continuar>-1','Images/Wixarika/Objetos_Espirituales/Jícara','122');
INSERT INTO `conversaciones` VALUES (123,'Chapala','OFRENDAS',50,'Masculino','Gobernador','Flecha','¡<i>Hamiku</i> (amigo), hice una <i>ɨ’rɨ</i> (flecha)!','Continuar>124','Images/Wixarika/Personajes/Iconos del Rostro/Gobernador (1)','123');
INSERT INTO `conversaciones` VALUES (124,'Chapala','OFRENDAS',50,'Masculino','Gobernador','Flecha','La <i>ɨ’rɨ</i> (flecha) es un objeto espiritual que simboliza lo masculino, por ser un elemento empleado para la caza.','Continuar>125','Images/Wixarika/Personajes/Iconos del Rostro/Gobernador (1)','124');
INSERT INTO `conversaciones` VALUES (125,'Chapala','OFRENDAS',50,'Masculino','Gobernador','Flecha','Se cree que, las <i>ɨ ’rɨte</i> (flechas) son utilizadas por las deidades para cazar al <i>maxa</i> (venado) y luchar contra las fuerzas de la oscuridad.','Continuar>126','Images/Wixarika/Objetos_Espirituales/Flecha','125');
INSERT INTO `conversaciones` VALUES (126,'Chapala','OFRENDAS',50,'Masculino','Gobernador','Flecha','Las <i>ɨ ’rɨte</i> (flechas) se utilizan para mandarles castigos a otros hombres cuando “no están haciendo las cosas bien” o no cumplen con los deberes que impone la cultura <i>Wixárika</i>.','Continuar>-1','Images/Wixarika/Objetos_Espirituales/Flecha','126');
INSERT INTO `conversaciones` VALUES (127,'Santiago Ixcuintla','NORMAL',51,'Femenino','Lechuza','Animales mensajeros','¡<i>Ke’aku</i> (hola) <i>hamiku</i> (amigo)! Yo soy una <i>miikɨiri</i> (lechuza).','Continuar>128','Images/Wixarika/Animales/Buho/Lechuza NPC 7','127');
INSERT INTO `conversaciones` VALUES (128,'Santiago Ixcuintla','NORMAL',51,'Femenino','Lechuza','Animales mensajeros','Soy considerada como un animal mensajero, cuando los <i>wixaritari</i> me ven cerca de sus casas o caminos saben que algo importante pasará.','Continuar>129','Images/Wixarika/Animales/Buho/Lechuza NPC 7','128');
INSERT INTO `conversaciones` VALUES (129,'Santiago Ixcuintla','NORMAL',51,'Femenino','Lechuza','Animales mensajeros','Fui enviada para avisarte que se acerca una batalla importante, mejora tu salud y armas.','Continuar>-1','Images/Wixarika/Animales/Buho/Lechuza NPC 7','129');
INSERT INTO `conversaciones` VALUES (130,'Fresnillo','NORMAL',52,'Femenino','Lagartija','Animales espirituales','¡<i>Ke’aku</i> (hola) <i>yeikame</i> (viajero)! Yo soy una <i>ɨkwi</i> (lagartija).','Continuar>131','Images/Wixarika/Animales/Lagartija/Lagartija 4','130');
INSERT INTO `conversaciones` VALUES (131,'Fresnillo','NORMAL',52,'Femenino','Lagartija','Animales espirituales','Soy considerada como un animal espiritual con el que algunos <i>wixaritari</i> se identifican, por ello, bordan mi figura en los <i>kemarite</i> (trajes tradicionales <i>Wixárika</i>).','Continuar>132','Images/Wixarika/Animales/Lagartija/Lagartija 4','131');
INSERT INTO `conversaciones` VALUES (132,'Fresnillo','NORMAL',52,'Femenino','Lagartija','Animales espirituales','Vengo a avisarte que se acerca una batalla importante, mejora tu salud y armas.','Continuar>-1','Images/Wixarika/Animales/Lagartija/Lagartija 4','132');
INSERT INTO `conversaciones` VALUES (133,'Charcas','NORMAL',53,'Femenino','Águila real','Animales espirituales','¡<i>Ke’aku</i> (hola) <i>hamiku</i> (amigo)! Yo soy una <i>werika</i> (águila real).','Continuar>134','Images/Wixarika/Animales/Aguila Real/Águila real 7','133');
INSERT INTO `conversaciones` VALUES (134,'Charcas','NORMAL',53,'Femenino','Águila real','Animales espirituales','Soy considerada como un animal espiritual con el que algunos <i>wixaritari</i> se identifican, por ello, bordan mi figura en los <i>kemarite</i> (trajes tradicionales <i>Wixárika</i>).','Continuar>135','Images/Wixarika/Animales/Aguila Real/Águila real 7','134');
INSERT INTO `conversaciones` VALUES (135,'Charcas','NORMAL',53,'Femenino','Águila real','Animales espirituales','También, utilizan mis plumas para decorar <i>ɨ’rɨte</i> (flechas) y <i>xupurerute</i> (sombreros).','Continuar>136','Images/Wixarika/Animales/Aguila Real/Águila real 7','135');
INSERT INTO `conversaciones` VALUES (136,'Charcas','NORMAL',53,'Femenino','Águila real','Animales espirituales','Vengo a avisarte que se acerca una batalla importante, mejora tu salud y armas.','Continuar>-1','Images/Wixarika/Animales/Aguila Real/Águila real 7','136');
INSERT INTO `conversaciones` VALUES (137,'Durango','NORMAL',54,'Femenino','Serpiente azul','Animales espirituales','¡<i>Ke’aku</i> (hola) <i>yeikame</i> (viajero)! Yo soy una <i>haikɨ</i> (serpiente azul).','Continuar>138','Images/Wixarika/Iconos/Serpiente azul','137');
INSERT INTO `conversaciones` VALUES (138,'Durango','NORMAL',54,'Femenino','Serpiente azul','Animales espirituales','Soy considerada como un animal espiritual, estoy presente en todos los lugares de culto, y obedezco a los dioses del fuego y el sol.','Continuar>139','Images/Wixarika/Iconos/Serpiente azul','138');
INSERT INTO `conversaciones` VALUES (139,'Durango','NORMAL',54,'Femenino','Serpiente azul','Animales espirituales','Se cree que concedo la habilidad de tejer a las mujeres, y que si las vacas son frotadas conmigo no se enferman.','Continuar>140','Images/Wixarika/Iconos/Serpiente azul','139');
INSERT INTO `conversaciones` VALUES (140,'Durango','NORMAL',54,'Femenino','Serpiente azul','Animales espirituales','Vengo a avisarte que se acerca una batalla importante, mejora tu salud y armas.','Continuar>-1','Images/Wixarika/Iconos/Serpiente azul','140');
INSERT INTO `conversaciones` VALUES (141,'Bolaños','NORMAL',55,'Masculino','Zorro','Animales mensajeros','¡<i>Ke’aku</i> (hola) <i>hamiku</i> (amigo)! Yo soy un <i>kauxai</i> (zorro).','Continuar>142','Images/Wixarika/Animales/Zorro/Zorro 1','141');
INSERT INTO `conversaciones` VALUES (142,'Bolaños','NORMAL',55,'Masculino','Zorro','Animales mensajeros','Soy considerado como un animal mensajero, cuando los <i>wixaritari</i> me ven cerca de sus casas o caminos saben que algo importante pasará.','Continuar>143','Images/Wixarika/Animales/Zorro/Zorro 1','142');
INSERT INTO `conversaciones` VALUES (143,'Bolaños','NORMAL',55,'Masculino','Zorro','Animales mensajeros','Fui enviado para avisarte que se acerca una batalla importante, mejora tu salud y armas.','Continuar>-1','Images/Wixarika/Animales/Zorro/Zorro 1','143');
INSERT INTO `conversaciones` VALUES (144,'Hewiixi','NORMAL',56,'Masculino','Zopilote','Animales espirituales','¡<i>Ke’aku</i> (hola) <i>yeikame</i> (viajero)! Yo soy un <i>wirɨkɨ</i> (Zopilote).','Continuar>145','Images/Wixarika/Iconos/Zopilote','144');
INSERT INTO `conversaciones` VALUES (145,'Hewiixi','NORMAL',56,'Masculino','Zopilote','Animales espirituales','Soy considerado como un animal espiritual.','Continuar>146','Images/Wixarika/Iconos/Zopilote','145');
INSERT INTO `conversaciones` VALUES (146,'Hewiixi','NORMAL',56,'Masculino','Zopilote','Animales espirituales','Vengo a avisarte que se acerca una batalla importante, mejora tu salud y armas.','Continuar>-1','Images/Wixarika/Iconos/Zopilote','146');
INSERT INTO `conversaciones` VALUES (147,'San blas','SITIO SAGRADO',57,'Masculino','Mara´kame','Haramara','<i>Hamiku</i> (amigo), te diriges a <i>Haramara</i>, un sitio sagrado ubicado en el mar de San Blas, Nayarit. Representa el oeste (poniente) en el Rombo <i>Wixárika</i>.','Continuar>148','Images/Wixarika/Iconos/Haramara 3x (2)','147');
INSERT INTO `conversaciones` VALUES (148,'San blas','SITIO SAGRADO',57,'Masculino','Mara´kame','Haramara','Los <i>wixaritari</i> creen que el mar es el principio de todo, por eso este sitio se considera como el origen de toda la vida y de las aguas del mundo.','Continuar>149','Images/Wixarika/Iconos/Haramara 3x (2)','148');
INSERT INTO `conversaciones` VALUES (149,'San blas','SITIO SAGRADO',57,'Masculino','Mara´kame','Haramara','Lleva una <i>mawari</i> (ofrenda) para poder hablar con la deidad que ahí se encuentra.','Continuar>-1','Images/Wixarika/Iconos/Haramara 3x (2)','149');
INSERT INTO `conversaciones` VALUES (150,'Real de catorce','SITIO SAGRADO',58,'Masculino','Anciano','Wirikuta','<i>Yeikame</i> (viajero), te diriges a <i>Wirikuta</i>, un sitio sagrado ubicado en el desierto de San Luis Potosí. Representa el este (oriente) en el Rombo <i>Wixárika</i>.','Continuar>151','Images/Wixarika/Personajes/Iconos del Rostro/Anciano (1)','150');
INSERT INTO `conversaciones` VALUES (151,'Real de catorce','SITIO SAGRADO',58,'Masculino','Anciano','Wirikuta','Los <i>wixaritari</i> realizan una peregrinación anual a <i>Wirikuta</i>, el viaje dura quince días.','Continuar>152','Images/Wixarika/Iconos/Wirikuta','151');
INSERT INTO `conversaciones` VALUES (152,'Real de catorce','SITIO SAGRADO',58,'Masculino','Anciano','Wirikuta','En la peregrinación se realizan abstenciones en favor de los antepasados y por faltas cometidas durante todo un año o toda una vida.','Continuar>153','Images/Wixarika/Iconos/Wirikuta','152');
INSERT INTO `conversaciones` VALUES (153,'Real de catorce','SITIO SAGRADO',58,'Masculino','Anciano','Wirikuta','Lleva una <i>mawari</i> (ofrenda) para poder hablar con la deidad que ahí se encuentra.','Continuar>-1','Images/Wixarika/Iconos/Wirikuta','153');
INSERT INTO `conversaciones` VALUES (154,'Canatlán','SITIO SAGRADO',59,'Masculino','Policía','Hauxa Manaka','<i>Hamiku</i> (amigo), te diriges a <i>Hauxa Manaka</i>, un sitio sagrado ubicado en el Cerro Gordo, Durango. Representa el norte en el Rombo <i>Wixárika</i>.','Continuar>155','Images/Wixarika/Personajes/Iconos del Rostro/Policia (1)','154');
INSERT INTO `conversaciones` VALUES (155,'Canatlán','SITIO SAGRADO',59,'Masculino','Policía','Hauxa Manaka','<i>Hauxa Manaka</i> significa “lugar de la madera flotante” o “lugar donde quedó el varado”, porque en este lugar terminó el viaje y quedaron los restos de la canoa que utilizó <i>Watakame</i> para sobrevivir al gran diluvio.','Continuar>156','Images/Wixarika/Iconos/Cerro gordo','155');
INSERT INTO `conversaciones` VALUES (156,'Canatlán','SITIO SAGRADO',59,'Masculino','Policía','Hauxa Manaka','Lleva una <i>mawari</i> (ofrenda) para poder hablar con la deidad que ahí se encuentra.','Continuar>-1','Images/Wixarika/Iconos/Cerro gordo','156');
INSERT INTO `conversaciones` VALUES (157,'Mezquitic','SITIO SAGRADO',60,'Masculino','Gobernador','Te’akata','<i>Yeikame</i> (viajero), te diriges a <i>Te’akata</i>, un sitio sagrado ubicado en Mezquitic, Jalisco. Representa el centro (arriba y abajo) en el Rombo <i>Wixárika</i>.','Continuar>158','Images/Wixarika/Personajes/Iconos del Rostro/Gobernador (1)','157');
INSERT INTO `conversaciones` VALUES (158,'Mezquitic','SITIO SAGRADO',60,'Masculino','Gobernador','Te’akata','<i>Te’akata</i> es considerado como el santuario de <i>Tatewari</i> (Nuestro abuelo fuego).','Continuar>159','Images/Wixarika/Iconos/Tekaata R','158');
INSERT INTO `conversaciones` VALUES (159,'Mezquitic','SITIO SAGRADO',60,'Masculino','Gobernador','Te’akata','Lleva una <i>mawari</i> (ofrenda) para poder hablar con la deidad que ahí se encuentra.','Continuar>-1','Images/Wixarika/Iconos/Tekaata R','159');
INSERT INTO `conversaciones` VALUES (160,'Chapala','SITIO SAGRADO',61,'Masculino','Gobernador','Xapawiyemeta','<i>Hamiku</i> (amigo), te diriges a <i>Xapawiyemeta</i>, un sitio sagrado ubicado en La Isla de los Alacranes, en el Lago de Chapala, Jalisco. Representa el sur en el Rombo <i>Wixárika</i>.','Continuar>161','Images/Wixarika/Personajes/Iconos del Rostro/Gobernador (1)','160');
INSERT INTO `conversaciones` VALUES (161,'Chapala','SITIO SAGRADO',61,'Masculino','Gobernador','Xapawiyemeta','En <i>Xapawiyemeta</i> tocó tierra <i>Watakame</i>, al terminar el viaje en canoa para sobrevivir al diluvio universal. Aquí nació la lluvia y emergió la humanidad.','Continuar>162','Images/Wixarika/Iconos/Isla de los alacranes 1 3x','161');
INSERT INTO `conversaciones` VALUES (162,'Chapala','SITIO SAGRADO',61,'Masculino','Gobernador','Xapawiyemeta','Lleva una <i>mawari</i> (ofrenda) para poder hablar con la deidad que ahí se encuentra.','Continuar>-1','Images/Wixarika/Iconos/Isla de los alacranes 1 3x','162');
INSERT INTO `conversaciones` VALUES (163,'Del Nayar','ALIMENTOS',62,'Femenino','Aldeana','Agua','<i>Hamiku</i> (amigo), te traigo <i>ha’a</i> (agua), te ayudará a incrementar tu barra de energía.','Continuar>-1(ALIMENTO-1-45','Images/Wixarika/Alimentos/Agua','163');
INSERT INTO `conversaciones` VALUES (164,'Del Nayar','ALIMENTOS',63,'Masculino','Aldeano','Tortilla','<i>Yeikame</i> (viajero), te traigo una <i>pa’apa</i> (tortilla), te ayudará a incrementar tu barra de energía.','Continuar>-1(ALIMENTO-1-66','Images/Wixarika/Alimentos/Tortilla','164');
INSERT INTO `conversaciones` VALUES (165,'La Yesca','ALIMENTOS',64,'Femenino','Aldeana','Albóndiga de jabalí','<i>Hamiku</i> (amigo), te traigo una albóndiga de <i>tuixuyeutanaka</i> (jabalí), te ayudará a incrementar tu barra de energía.','Continuar>-1(ALIMENTO-1-84','Images/Wixarika/Alimentos/Albóndiga de jabalí','165');
INSERT INTO `conversaciones` VALUES (166,'La Yesca','ALIMENTOS',65,'Masculino','Aldeano','Queso','<i>Yeikame</i> (viajero), te traigo un <i>kexiu</i> (queso) elaborado por los <i>wixaritari</i>. Intercámbialo por <i>ikú</i> (maíz).','Continuar>-1(ALIMENTO-1-82','Images/Wixarika/Alimentos/Queso fresco 1x','166');
INSERT INTO `conversaciones` VALUES (167,'SAMAO','ALIMENTOS',66,'Femenino','Aldeana','Piloncillo','<i>Hamiku</i> (amigo), te traigo un <i>tsakaka</i> (piloncillo), que se utiliza para endulzar bebidas y postres tradicionales. Intercámbialo por <i>ikú</i> (maíz).','Continuar>-1(ALIMENTO-1-59','Images/Wixarika/Alimentos/Piloncillo 1','167');
INSERT INTO `conversaciones` VALUES (168,'SAMAO','ALIMENTOS',67,'Masculino','Aldeano','Pescado sarandeado','<i>Yeikame</i> (viajero), te traigo <i>ketsɨ warikietɨ</i> (pescado sarandeado), te ayudará a incrementar tu barra de energía.','Continuar>-1(ALIMENTO-1-94','Images/Wixarika/Alimentos/Pescado Sarandeado','168');
INSERT INTO `conversaciones` VALUES (169,'Tepic','ALIMENTOS',68,'Femenino','Aldeana','Mole de venado','<i>Hamiku</i> (amigo), te traigo <i>maxa ikwaiyári</i> (mole de venado), un platillo tradicional <i>Wixárika</i>. Te ayudará a incrementar tu barra de energía.','Continuar>-1(ALIMENTO-1-88','Images/Wixarika/Alimentos/Mole de venado','169');
INSERT INTO `conversaciones` VALUES (170,'Santiago Ixcuintla','ALIMENTOS',69,'Masculino','Aldeano','Caldo de ardilla','<i>Yeikame</i> (viajero), te traigo <i>tekɨ itsari</i> (caldo de ardilla), un platillo tradicional <i>Wixárika</i>. Te ayudará a incrementar tu barra de energía.','Continuar>-1(ALIMENTO-1-89','Images/Wixarika/Alimentos/Caldo de ardilla','170');
INSERT INTO `conversaciones` VALUES (171,'San blas','ALIMENTOS',70,'Femenino','Aldeana','Pipián de iguana','<i>Hamiku</i> (amigo), te traigo pipián de <i>ke’etse</i> (iguana), un platillo tradicional <i>Wixárika</i>. Te ayudará a incrementar tu barra de energía.','Continuar>-1(ALIMENTO-1-91','Images/Wixarika/Alimentos/Pipián de iguana','171');
INSERT INTO `conversaciones` VALUES (172,'San blas','ALIMENTOS',71,'Masculino','Aldeano','Plátano frito','<i>Yeikame</i> (viajero), te traigo un <i>ka’arú wiyamatɨ</i> (plátano frito), te ayudará a incrementar tu barra de energía.','Continuar>-1(ALIMENTO-1-58','Images/Wixarika/Alimentos/Plátano frito','172');
INSERT INTO `conversaciones` VALUES (173,'Valparaíso','ALIMENTOS',72,'Femenino','Aldeana','Chicuatol','<i>Hamiku</i> (amigo), te traigo <i>tsinari</i> (chicuatol), una bebida típica <i>Wixárika</i>. Utilízalo para incrementar tu barra de energía.','Continuar>-1(ALIMENTO-1-48','Images/Wixarika/Alimentos/Chicuatol','173');
INSERT INTO `conversaciones` VALUES (174,'Valparaíso','ALIMENTOS',73,'Masculino','Aldeano','Pan de elote','<i>Yeikame</i> (viajero), te traigo <i>ikɨri paniyari</i> (pan de elote), utilízalo para incrementar tu barra de energía.','Continuar>-1(ALIMENTO-1-57','Images/Wixarika/Alimentos/Pan de elote','174');
INSERT INTO `conversaciones` VALUES (175,'Fresnillo','ALIMENTOS',74,'Femenino','Aldeana','Caldo de güilota','<i>Hamiku</i> (amigo), te traigo <i>weurai itsari</i> (caldo de güilota), utilízalo para incrementar tu barra de energía.','Continuar>-1(ALIMENTO-1-95','Images/Wixarika/Alimentos/Caldo de güilota','175');
INSERT INTO `conversaciones` VALUES (176,'Zacatecas','ALIMENTOS',75,'Masculino','Aldeano','Enchilada de pollo','<i>Yeikame</i> (viajero), te traigo una enchilada de <i>wakana</i> (pollo), utilízala para incrementar tu barra de energía.','Continuar>-1(ALIMENTO-1-99','Images/Wixarika/Alimentos/Enchilada de pollo','176');
INSERT INTO `conversaciones` VALUES (177,'Villa de Ramos','ALIMENTOS',76,'Femenino','Aldeana','Taco de frijoles','<i>Hamiku</i> (amigo), te traigo un <i>múmete takuyari</i> (taco de frijoles), un alimento principal en la gastronomía <i>Wixárika</i>. Te ayudará a incrementar tu barra de energía.','Continuar>-1(ALIMENTO-1-74','Images/Wixarika/Alimentos/Taco de frijol','177');
INSERT INTO `conversaciones` VALUES (178,'Santo Domingo','ALIMENTOS',77,'Masculino','Aldeano','Caldo de conejo','<i>Yeikame</i> (viajero), te traigo <i>tátsiu itsari</i> (caldo de conejo), te ayudará a incrementar tu barra de energía.','Continuar>-1(ALIMENTO-1-107','Images/Wixarika/Alimentos/Caldo de conejo','178');
INSERT INTO `conversaciones` VALUES (179,'Charcas','ALIMENTOS',78,'Femenino','Aldeana','Elote asado','<i>Hamiku</i> (amigo), te traigo un <i>ikɨri warikietɨ</i> (elote asado), un alimento tradicional <i>Wixárika</i>. Te ayudará a incrementar tu barra de energía.','Continuar>-1(ALIMENTO-1-62','Images/Wixarika/Alimentos/Elote asado','179');
INSERT INTO `conversaciones` VALUES (180,'Real de catorce','ALIMENTOS',79,'Masculino','Aldeano','Jugo de naranja','<i>Yeikame</i> (viajero), te traigo <i>narakaxi hayaári</i> (jugo de naranja), te ayudará a incrementar tu barra de energía.','Continuar>-1(ALIMENTO-1-51','Images/Wixarika/Alimentos/Jugo de naranja','180');
INSERT INTO `conversaciones` VALUES (181,'Real de catorce','ALIMENTOS',80,'Femenino','Aldeana','Sopa','<i>Hamiku</i> (amigo), te traigo <i>xupaxi</i> (sopa), te ayudará a incrementar tu barra de energía.','Continuar>-1(ALIMENTO-1-75','Images/Wixarika/Alimentos/Sopa aguada','181');
INSERT INTO `conversaciones` VALUES (182,'Mezquital','ALIMENTOS',81,'Masculino','Aldeano','Pinole','<i>Yeikame</i> (viajero), te traigo <i>pexúri</i> (pinole), un alimento tradicional <i>Wixárika</i>. Utilízalo para incrementar tu barra de energía.','Continuar>-1(ALIMENTO-1-60','Images/Wixarika/Alimentos/Pinole','182');
INSERT INTO `conversaciones` VALUES (183,'Mezquital','ALIMENTOS',82,'Femenino','Aldeana','Jugo de caña','<i>Hamiku</i> (amigo), te traigo <i>uwá hayaári</i> (jugo de caña), utilízalo para incrementar tu barra de energía.','Continuar>-1(ALIMENTO-1-50','Images/Wixarika/Alimentos/Jugo de caña','183');
INSERT INTO `conversaciones` VALUES (184,'Pueblo Nuevo','ALIMENTOS',83,'Masculino','Aldeano','Quesadilla con verdolagas','<i>Yeikame</i> (viajero), te traigo una quesadilla con <i>aɨraxate</i> (verdolagas), un alimento popular <i>Wixárika</i>. Utilízala para incrementar tu barra de energía.','Continuar>-1(ALIMENTO-1-69','Images/Wixarika/Alimentos/Quesadilla con verdolaga','184');
INSERT INTO `conversaciones` VALUES (185,'Durango','ALIMENTOS',84,'Femenino','Aldeana','Frijoles cocidos y fritos','<i>Hamiku</i> (amigo), te traigo <i>múme kwakwaxitɨ</i> (frijoles cocidos) y <i>múme wiyamarietɨka</i> (frijoles fritos), alimentos tradicionales <i>Wixárika</i>. Utilízalos para incrementar tu barra de energía.','Continuar>-1(ALIMENTO-1-72','Images/Wixarika/Alimentos/Frijol cocido','185');
INSERT INTO `conversaciones` VALUES (186,'Canatlán','ALIMENTOS',85,'Masculino','Aldeano','Atole','<i>Yeikame</i> (viajero), te traigo un <i>hamuitsi</i> (atole), una bebida típica <i>Wixárika</i>. Utilízalo para incrementar tu barra de energía.','Continuar>-1(ALIMENTO-1-47','Images/Wixarika/Alimentos/Atole','186');
INSERT INTO `conversaciones` VALUES (187,'Canatlán','ALIMENTOS',86,'Femenino','Aldeana','Pan','<i>Hamiku</i> (amigo), te traigo un <i>paní</i> (pan), utilízalo para incrementar tu barra de energía.','Continuar>-1(ALIMENTO-1-56','Images/Wixarika/Alimentos/Pan dulce','187');
INSERT INTO `conversaciones` VALUES (188,'Huejiquilla','ALIMENTOS',87,'Masculino','Aldeano','Quesadilla con hongos','<i>Yeikame</i> (viajero), te traigo una quesadilla con <i>yekwa’ate</i> (hongos), un alimento popular <i>Wixárika</i>. Te ayudará a incrementar tu barra de energía.','Continuar>-1(ALIMENTO-1-68','Images/Wixarika/Alimentos/Quesadilla','188');
INSERT INTO `conversaciones` VALUES (189,'Mezquitic','ALIMENTOS',88,'Femenino','Aldeana','Gordita','<i>Hamiku</i> (amigo), te traigo <i>tsuirá</i> (gordita), un alimento popular <i>Wixárika</i>. Te ayudará a incrementar tu barra de energía.','Continuar>-1(ALIMENTO-1-64','Images/Wixarika/Alimentos/Gordita','189');
INSERT INTO `conversaciones` VALUES (190,'Bolaños','ALIMENTOS',89,'Masculino','Aldeano','Pozole','<i>Yeikame</i> (viajero), te traigo <i>kwitsari</i> (pozole), te ayudará a incrementar tu barra de energía.','Continuar>-1(ALIMENTO-1-81','Images/Wixarika/Alimentos/Pozole de cerdo','190');
INSERT INTO `conversaciones` VALUES (191,'Bolaños','ALIMENTOS',90,'Femenino','Aldeana','Agua de jamaica','<i>Hamiku</i> (amigo), te traigo <i>kamaika hayaári</i> (agua de jamaica), te ayudará a incrementar tu barra de energía.','Continuar>-1(ALIMENTO-1-46','Images/Wixarika/Alimentos/Agua de jamaica','191');
INSERT INTO `conversaciones` VALUES (192,'Chapala','ALIMENTOS',91,'Masculino','Aldeano','Tejuino','<i>Yeikame</i> (viajero), te traigo un <i>nawá</i> (tejuino), una bebida típica <i>Wixárika</i>. Te ayudará a incrementar tu barra de energía.','Continuar>-1(ALIMENTO-1-53','Images/Wixarika/Alimentos/Atole o tejuino','192');
INSERT INTO `conversaciones` VALUES (193,'Miiki','ALIMENTOS',92,'Femenino','Aldeana','Tamal','<i>Hamiku</i> (amigo), te traigo un <i>tétsu</i> (tamal), un alimento tradicional <i>Wixárika</i>. Utilízalo para incrementar tu barra de energía.','Continuar>-1(ALIMENTO-1-70','Images/Wixarika/Alimentos/Tamal de pollo','193');
INSERT INTO `conversaciones` VALUES (194,'Miiki','ALIMENTOS',93,'Masculino','Aldeano','Atole','<i>Yeikame</i> (viajero), te traigo un <i>hamuitsi</i> (atole), una bebida típica <i>Wixárika</i>. Utilízalo para incrementar tu barra de energía.','Continuar>-1(ALIMENTO-1-47','Images/Wixarika/Alimentos/Atole','194');
INSERT INTO `conversaciones` VALUES (195,'Hewiixi','ALIMENTOS',94,'Femenino','Aldeana','Mole','<i>Hamiku</i> (amigo), te traigo <i>pexuri</i> (mole), un alimento tradicional <i>Wixárika</i>. Utilízalo para incrementar tu barra de energía.','Continuar>-1(ALIMENTO-1-80','Images/Wixarika/Alimentos/Mole de venado','195');
INSERT INTO `conversaciones` VALUES (196,'Kieri','ALIMENTOS',95,'Masculino','Aldeano','Tejuino','<i>Yeikame</i> (viajero), te traigo un <i>nawá</i> (tejuino), una bebida típica <i>Wixárika</i>. Utilízalo para incrementar tu barra de energía.','Continuar>-1(ALIMENTO-1-53','Images/Wixarika/Alimentos/Atole o tejuino','196');
INSERT INTO `conversaciones` VALUES (197,'Del Nayar','CULTURA',96,'Masculino','Mara´kame','Tradición oral','¡Te contaré sobre la tradición oral <i>Wixárika</i>!','Continuar>198','Images/Wixarika/Personajes/Iconos del Rostro/C Marakame 3x','197');
INSERT INTO `conversaciones` VALUES (198,'Del Nayar','CULTURA',96,'Masculino','Mara´kame','Tradición oral','El pueblo <i>Wixárika</i> tiene diversas historias sobre su manera de ver e interpretar el mundo.','Continuar>199','Images/Wixarika/Personajes/Iconos del Rostro/C Marakame 3x','198');
INSERT INTO `conversaciones` VALUES (199,'Del Nayar','CULTURA',96,'Masculino','Mara´kame','Tradición oral','En estas historias se narra el origen de los <i>wixaritari</i>, las deidades y los elementos de la naturaleza.','Continuar>200','Images/Wixarika/Personajes/Iconos del Rostro/C Marakame 3x','199');
INSERT INTO `conversaciones` VALUES (200,'Del Nayar','CULTURA',96,'Masculino','Mara´kame','Tradición oral','Son transmitidas oralmente por los ancianos mayores de las comunidades.','Continuar>-1','Images/Wixarika/Personajes/Iconos del Rostro/C Marakame 3x','200');
INSERT INTO `conversaciones` VALUES (201,'La Yesca','CULTURA',97,'Masculino','Mara´kame','El camino del corazón','¡Te contaré sobre el <i>nana’iyari</i> (el camino del corazón)!','Continuar>202','Images/Wixarika/Personajes/Iconos del Rostro/C Marakame 3x','201');
INSERT INTO `conversaciones` VALUES (202,'La Yesca','CULTURA',97,'Masculino','Mara´kame','El camino del corazón','Los lazos que vinculan a las diferentes comunidades del pueblo <i>Wixárika</i> se expresan a través del <i>nana’iyari</i> (el camino del corazón).','Continuar>203','Images/Wixarika/Diario_Viaje/Camino corazón','202');
INSERT INTO `conversaciones` VALUES (203,'La Yesca','CULTURA',97,'Masculino','Mara´kame','El camino del corazón','El <i>nana’iyari</i> (el camino del corazón) debe ser vivido y reproducido cotidianamente a través del costumbre, es decir, de los hábitos y prácticas <i>Wixárika</i>.','Continuar>-1','Images/Wixarika/Diario_Viaje/Camino corazón','203');
INSERT INTO `conversaciones` VALUES (204,'SAMAO','CULTURA',98,'Masculino','Mara´kame','Venado azul','¡Te contaré sobre <i>Tamatzi Kauyumari</i> (Nuestro hermano mayor Venado Azul)!','Continuar>205','Images/Wixarika/Personajes/Iconos del Rostro/C Marakame 3x','204');
INSERT INTO `conversaciones` VALUES (205,'SAMAO','CULTURA',98,'Masculino','Mara´kame','Venado azul','La <i>xátsika</i> (leyenda) dice que, durante una época de sequía y hambre, los abuelos enviaron a cuatro jóvenes cazadores en busca de alimento.','Continuar>206','Images/Wixarika/Personajes/Iconos del Rostro/C Venado Azul 3x','205');
INSERT INTO `conversaciones` VALUES (206,'SAMAO','CULTURA',98,'Masculino','Mara´kame','Venado azul','En el camino encontraron un <i>maxa</i> (venado) que los guio hasta la zona sagrada de <i>Wirikuta</i>, donde hallaron el <i>hikuri</i> (peyote), un alimento espiritual que les regresó la lluvia, comida y salud.','Continuar>207','Images/Wixarika/Personajes/Iconos del Rostro/C Venado Azul 3x','206');
INSERT INTO `conversaciones` VALUES (207,'SAMAO','CULTURA',98,'Masculino','Mara´kame','Venado azul','Desde entonces, los <i>wixaritari</i> adoran al <i>maxa</i> (venado) que, al mismo tiempo es <i>hikuri</i> (peyote) y <i>ikú</i> (maíz).','Continuar>-1','Images/Wixarika/Personajes/Iconos del Rostro/C Venado Azul 3x','207');
INSERT INTO `conversaciones` VALUES (208,'Tepic','CULTURA',99,'Masculino','Mara´kame','Rituales Wixárika','¡Te contaré sobre los rituales <i>Wixárika</i>!','Continuar>209','Images/Wixarika/Personajes/Iconos del Rostro/C Marakame 3x','208');
INSERT INTO `conversaciones` VALUES (209,'Tepic','CULTURA',99,'Masculino','Mara´kame','Rituales Wixárika','Los rituales <i>Wixárika</i> recrean las historias sobre los orígenes de los <i>wixaritari</i>, y deben realizarse permanentemente para garantizar la continuidad del pueblo <i>Wixárika</i>.','Continuar>210','Images/Wixarika/Personajes/Iconos del Rostro/C Marakame 3x','209');
INSERT INTO `conversaciones` VALUES (210,'Tepic','CULTURA',99,'Masculino','Mara´kame','Rituales Wixárika','Las peregrinaciones, las abstinencias y el consumo de <i>hikuri</i> (peyote) son parte esencial de los rituales que guían la vida de los <i>wixaritari</i>.','Continuar>-1','Images/Wixarika/Personajes/Iconos del Rostro/C Marakame 3x','210');
INSERT INTO `conversaciones` VALUES (211,'Santiago Ixcuintla','CULTURA',100,'Masculino','Mara´kame','Tukipa','¡Te contaré sobre el <i>tukipa</i> (centro ceremonial)!','Continuar>212','Images/Wixarika/Personajes/Iconos del Rostro/C Marakame 3x','211');
INSERT INTO `conversaciones` VALUES (212,'Santiago Ixcuintla','CULTURA',100,'Masculino','Mara´kame','Tukipa','El <i>tukipa</i> (centro ceremonial) es un espacio dedicado al culto de las deidades, se compone de un gran templo llamado <i>tuki</i> y otros templos de menor tamaño conocidos como <i>xiriki</i>.','Continuar>213','Images/Wixarika/Iconos/Tukipa','212');
INSERT INTO `conversaciones` VALUES (213,'Santiago Ixcuintla','CULTURA',100,'Masculino','Mara´kame','Tukipa','El <i>tuki</i> está dedicado a <i>Tatewarí</i> (dios del fuego) y los <i>xiriki</i> a las otras deidades principales.','Continuar>214','Images/Wixarika/Iconos/Tukipa','213');
INSERT INTO `conversaciones` VALUES (214,'Santiago Ixcuintla','CULTURA',100,'Masculino','Mara´kame','Tukipa','En el <i>tukipa</i> (centro ceremonial) celebramos las fiestas <i>Wixárika</i>, durante las cuales se escenifica el drama cósmico-ritual (historias sobre los orígenes de los <i>wixaritari</i>).','Continuar>-1','Images/Wixarika/Iconos/Tukipa','214');
INSERT INTO `conversaciones` VALUES (215,'Valparaíso','CULTURA',101,'Masculino','Jicarero','Peyote','¡Te contaré sobre el <i>hikuri</i> (peyote)!','Continuar>216','Images/Wixarika/Personajes/Iconos del Rostro/C Marakame 3x','215');
INSERT INTO `conversaciones` VALUES (216,'Valparaíso','CULTURA',101,'Masculino','Jicarero','Peyote','La <i>xátsika</i> (leyenda) dice que, <i>Tamatzi Kauyumari</i> (Nuestro hermano mayor Venado Azul), en su esfuerzo por levantar a <i>Tayau</i> (Nuestro padre el Sol) al cielo, perdió parte de sus cuernos, los cuales cayeron a la tierra y germinaron dando origen al <i>hikuri</i> (peyote).','Continuar>217','Images/Wixarika/Iconos/Peyote','216');
INSERT INTO `conversaciones` VALUES (217,'Valparaíso','CULTURA',101,'Masculino','Jicarero','Peyote','Desde entonces, el <i>hikuri</i> (peyote) quedó divinizado en la cultura <i>Wixárika</i>.','Continuar>-1','Images/Wixarika/Iconos/Peyote','217');
INSERT INTO `conversaciones` VALUES (218,'Valparaíso','CULTURA',102,'Masculino','Jicarero','Peregrinación a Wirikuta','¡Te contaré sobre la peregrinación a <i>Wirikuta</i>!','Continuar>219','Images/Wixarika/Personajes/Iconos del Rostro/C Marakame 3x','218');
INSERT INTO `conversaciones` VALUES (219,'Valparaíso','CULTURA',102,'Masculino','Jicarero','Peregrinación a Wirikuta','El pueblo <i>Wixárika</i> recrea las historias sobre su origen, según las cuales, los hombres surgieron del mar y emprendieron el viaje hacia <i>Wirikuta</i> para asistir al nacimiento del sol.','Continuar>220','Images/Wixarika/Iconos/Wirikuta','219');
INSERT INTO `conversaciones` VALUES (220,'Valparaíso','CULTURA',102,'Masculino','Jicarero','Peregrinación a Wirikuta','Por lo cual, anualmente, realizan una peregrinación a <i>Wirikuta</i> que se distribuye entre los cinco sitios sagrados, donde los peregrinos dejan <i>mawarite</i> (ofrendas) para las deidades.','Continuar>-1','Images/Wixarika/Iconos/Wirikuta','220');
INSERT INTO `conversaciones` VALUES (221,'Fresnillo','CULTURA',103,'Masculino','Jicarero','El costumbre','¡Te contaré sobre el <i>nana’iyari</i> (el costumbre)!','Continuar>222','Images/Wixarika/Personajes/Iconos del Rostro/C Marakame 3x','221');
INSERT INTO `conversaciones` VALUES (222,'Fresnillo','CULTURA',103,'Masculino','Jicarero','El costumbre','El <i>nana’iyari</i> (el costumbre) se constituye de diferentes elementos y características de la cultura <i>Wixárika</i>, así como de las peregrinaciones, por ello, es necesario vivirlo.','Continuar>223','Images/Wixarika/Iconos/Ritual Wixárika','222');
INSERT INTO `conversaciones` VALUES (223,'Fresnillo','CULTURA',103,'Masculino','Jicarero','El costumbre','<i>Tamatzi Kauyumari</i>, el Venado Azul, tuvo que hacer el <i>nana’iyari</i> (el costumbre) y peregrinar por los rumbos del universo, para obtener su <i>’iyari</i> (corazón espiritual).','Continuar>-1','Images/Wixarika/Personajes/Iconos del Rostro/C Venado Azul 3x','223');
INSERT INTO `conversaciones` VALUES (224,'Zacatecas','CULTURA',104,'Masculino','Jicarero','Peyote','¡Encontraste <i>hikuri</i> (peyote)!','Continuar>225','Images/Wixarika/Personajes/Iconos del Rostro/C Marakame 3x','224');
INSERT INTO `conversaciones` VALUES (225,'Zacatecas','CULTURA',104,'Masculino','Jicarero','Peyote','El <i>hikuri</i> (peyote) es un cactus sin espinas endémico de los desiertos del norte de México, que comúnmente crece debajo de matorrales. Sus flores son de color rosa pálido.','Continuar>226','Images/Wixarika/Iconos/Peyote','225');
INSERT INTO `conversaciones` VALUES (226,'Zacatecas','CULTURA',104,'Masculino','Jicarero','Peyote','El <i>hikuri</i> (peyote) tarda de 15 a 20 años en crecer, debido a este lento crecimiento y a la sobre-recolección se encuentra en peligro de extinción.','Continuar>227','Images/Wixarika/Iconos/Peyote','226');
INSERT INTO `conversaciones` VALUES (227,'Zacatecas','CULTURA',104,'Masculino','Jicarero','Peyote','Solamente los <i>wixaritari</i> pueden recolectarlo.','Continuar>-1','Images/Wixarika/Iconos/Peyote','227');
INSERT INTO `conversaciones` VALUES (228,'Zacatecas','CULTURA',105,'Masculino','Jicarero','Peregrinación a Wirikuta','¡Te contaré sobre la peregrinación a <i>Wirikuta</i>!','Continuar>229','Images/Wixarika/Personajes/Iconos del Rostro/C Marakame 3x','228');
INSERT INTO `conversaciones` VALUES (229,'Zacatecas','CULTURA',105,'Masculino','Jicarero','Peregrinación a Wirikuta','Durante la peregrinación a <i>Wirikuta</i> se realiza la búsqueda de los antepasados, quienes transmiten el conocimiento por medio de visiones para que el mundo siga en equilibrio.','Continuar>230','Images/Wixarika/Iconos/Wirikuta','229');
INSERT INTO `conversaciones` VALUES (230,'Zacatecas','CULTURA',105,'Masculino','Jicarero','Peregrinación a Wirikuta','También, se caza el <i>maxa</i> (venado), se recolecta el <i>hikuri</i> (peyote) y se transporta agua del mar hacia el desierto y viceversa, para invocar a la lluvia.','Continuar>231','Images/Wixarika/Iconos/Wirikuta','230');
INSERT INTO `conversaciones` VALUES (231,'Zacatecas','CULTURA',105,'Masculino','Jicarero','Peregrinación a Wirikuta','En <i>Wirikuta</i>, los <i>wixaritari</i> agradecen por todo lo que la tierra ofrece.','Continuar>-1','Images/Wixarika/Iconos/Wirikuta','231');
INSERT INTO `conversaciones` VALUES (232,'Villa de Ramos','CULTURA',106,'Masculino','Anciano','Los colores del maíz','¡Te contaré sobre los colores del <i>ikú</i> (maíz)!','Continuar>233','Images/Wixarika/Personajes/Iconos del Rostro/Anciano (1)','232');
INSERT INTO `conversaciones` VALUES (233,'Villa de Ramos','CULTURA',106,'Masculino','Anciano','Los colores del maíz','La <i>xátsika</i> (leyenda) dice que, el pueblo <i>Wixárika</i> estaba cansado por la monotonía de su comida, por lo que, un joven decidió partir en búsqueda de un nuevo alimento.','Continuar>234','Images/Wixarika/Personajes/Iconos del Rostro/Anciano (1)','233');
INSERT INTO `conversaciones` VALUES (234,'Villa de Ramos','CULTURA',106,'Masculino','Anciano','Los colores del maíz','En su camino, encontró una fila de hormigas, que ocultaban <i>ikú</i> (maíz) y decidió seguirlas, sin embargo, por el cansancio se durmió y las hormigas aprovecharon y se comieron su ropa.','Continuar>235','Images/Wixarika/Alimentos/Maíz amarillo','234');
INSERT INTO `conversaciones` VALUES (235,'Villa de Ramos','CULTURA',106,'Masculino','Anciano','Los colores del maíz','Al despertar, se vio desnudo y hambriento, así que se entristeció. Entonces, apareció la Madre del <i>ikú</i> (maíz), quien lo guio hasta donde había <i>ikú</i> (maíz) en abundancia.','Continuar>236','Images/Wixarika/Alimentos/Maíz amarillo','235');
INSERT INTO `conversaciones` VALUES (236,'Villa de Ramos','CULTURA',106,'Masculino','Anciano','Los colores del maíz','Encontró <i>ikú</i> (maíz) blanco, amarillo, rojo, morado y azul. Tomó los cinco tipos de <i>ikú</i> (maíz) de colores y regreso a la comunidad para sembrarlos y cosecharlos.','Continuar>-1','Images/Wixarika/Alimentos/Maíz amarillo','236');
INSERT INTO `conversaciones` VALUES (237,'Santo Domingo','CULTURA',107,'Masculino','Anciano','Peyote','Te contaré sobre los usos del <i>hikuri</i> (peyote).','Continuar>238','Images/Wixarika/Personajes/Iconos del Rostro/Anciano (1)','237');
INSERT INTO `conversaciones` VALUES (238,'Santo Domingo','CULTURA',107,'Masculino','Anciano','Peyote','El <i>hikuri</i> (peyote) tiene una larga tradición de uso dentro de las comunidades <i>Wixárika</i>, esencialmente, con fines medicinales y rituales.','Continuar>239','Images/Wixarika/Iconos/Peyote','238');
INSERT INTO `conversaciones` VALUES (239,'Santo Domingo','CULTURA',107,'Masculino','Anciano','Peyote','Además, de su uso terapéutico, por sus propiedades alucinógenas, el <i>hikuri</i> (peyote) se convirtió en la medicina más potente para ahuyentar el mal o las influencias sobrenaturales.','Continuar>240','Images/Wixarika/Iconos/Peyote','239');
INSERT INTO `conversaciones` VALUES (240,'Santo Domingo','CULTURA',107,'Masculino','Anciano','Peyote','¡Protege al <i>hikuri</i> (peyote)! Está en peligro de extinción.','Continuar>-1','Images/Wixarika/Personajes/Iconos del Rostro/Anciano (1)','240');
INSERT INTO `conversaciones` VALUES (241,'Santo Domingo','CULTURA',108,'Masculino','Anciano','Peregrinación a Wirikuta','¿Qué se necesita para ir a <i>Wirikuta</i>?','Continuar>242','Images/Wixarika/Personajes/Iconos del Rostro/Anciano (1)','241');
INSERT INTO `conversaciones` VALUES (242,'Santo Domingo','CULTURA',108,'Masculino','Anciano','Peregrinación a Wirikuta','Antes de iniciar el viaje, los peregrinos deben realizar una ceremonia de purificación, en la cual confiesan sus faltas cometidas. También, deben realizar abstinencias para estar en condiciones espirituales.','Continuar>243','Images/Wixarika/Iconos/Wirikuta','242');
INSERT INTO `conversaciones` VALUES (243,'Santo Domingo','CULTURA',108,'Masculino','Anciano','Peregrinación a Wirikuta','Durante el viaje, los peregrinos deben vestirse con un atuendo especial <i>Wixárika</i> y escuchar el <i>tunuiya</i> (canto sagrado) de los <i>mara’kate</i> (chamanes).','Continuar>-1','Images/Wixarika/Iconos/Wirikuta','243');
INSERT INTO `conversaciones` VALUES (244,'Charcas','CULTURA',109,'Masculino','Anciano','Peyote','¡Te contaré sobre el <i>hikuri</i> (peyote)!','Continuar>245','Images/Wixarika/Personajes/Iconos del Rostro/Anciano (1)','244');
INSERT INTO `conversaciones` VALUES (245,'Charcas','CULTURA',109,'Masculino','Anciano','Peyote','La vida del pueblo <i>Wixárika</i> gira en torno a un calendario de festividades, peregrinaciones, y ofrendas relacionadas con el <i>hikuri</i> (peyote).','Continuar>246','Images/Wixarika/Iconos/Peyote','245');
INSERT INTO `conversaciones` VALUES (246,'Charcas','CULTURA',109,'Masculino','Anciano','Peyote','La celebración más importante es la peregrinación a <i> Wirikuta</i>, donde se recolecta el <i>hikuri</i> (peyote) para todas las demás ceremonias.','Continuar>247','Images/Wixarika/Iconos/Peyote','246');
INSERT INTO `conversaciones` VALUES (247,'Charcas','CULTURA',109,'Masculino','Anciano','Peyote','Los <i>wixaritari</i> son los guardianes del <i>hikuri</i> (peyote).','Continuar>-1','Images/Wixarika/Iconos/Peyote','247');
INSERT INTO `conversaciones` VALUES (248,'Charcas','CULTURA',110,'Masculino','Anciano','Peregrinación a Wirikuta','¡Te contaré sobre los <i>matewáme</i> (el que no sabe y va a saber)!','Continuar>249','Images/Wixarika/Personajes/Iconos del Rostro/Anciano (1)','248');
INSERT INTO `conversaciones` VALUES (249,'Charcas','CULTURA',110,'Masculino','Anciano','Peregrinación a Wirikuta','Quienes hacen por primera vez la peregrinación a <i>Wirikuta</i> son nombrados <i>matewáme</i> (el que no sabe y va a saber).','Continuar>250','Images/Wixarika/Iconos/Wirikuta','249');
INSERT INTO `conversaciones` VALUES (250,'Charcas','CULTURA',110,'Masculino','Anciano','Peregrinación a Wirikuta','Deben llevar los ojos vendados durante el trayecto, al llegar a donde vive <i>Tatéi Matiniéri</i> (Diosa de la lluvia del poniente), se les retira la venda y se les presenta a la deidad.','Continuar>251','Images/Wixarika/Iconos/Wirikuta','250');
INSERT INTO `conversaciones` VALUES (251,'Charcas','CULTURA',110,'Masculino','Anciano','Peregrinación a Wirikuta','Una vez en <i>Wirikuta</i>, realizan varios rituales y duras pruebas físicas para acceder al <i>hikuri</i> (peyote) y estar en condiciones espirituales.','Continuar>-1','Images/Wixarika/Personajes/Iconos del Rostro/Anciano (1)','251');
INSERT INTO `conversaciones` VALUES (252,'Real de catorce','CULTURA',111,'Masculino','Anciano','Peregrinación a Wirikuta','¡Te contaré sobre la peregrinación a <i>Wirikuta</i>!','Continuar>253','Images/Wixarika/Personajes/Iconos del Rostro/Anciano (1)','252');
INSERT INTO `conversaciones` VALUES (253,'Real de catorce','CULTURA',111,'Masculino','Anciano','Peregrinación a Wirikuta','La peregrinación finaliza en el desierto de <i>Wirikuta</i>, en <i>Reunari</i> (Cerro Quemado), donde surgió <i>Tayau</i> (Nuestro padre el Sol).','Continuar>254','Images/Wixarika/Iconos/Wirikuta','253');
INSERT INTO `conversaciones` VALUES (254,'Real de catorce','CULTURA',111,'Masculino','Anciano','Peregrinación a Wirikuta','Ahí los peregrinos dejan <i>mawarite</i> (ofrendas), especialmente, en las cuevas sagradas de donde salieron los antepasados. Después, se hace una ceremonia de representación del nacimiento de los ancestros.','Continuar>255','Images/Wixarika/Iconos/Wirikuta','254');
INSERT INTO `conversaciones` VALUES (255,'Real de catorce','CULTURA',111,'Masculino','Anciano','Peregrinación a Wirikuta','Por último, los peregrinos se dividen en grupos para viajar a los otros sitios sagrados.','Continuar>-1','Images/Wixarika/Iconos/Wirikuta','255');
INSERT INTO `conversaciones` VALUES (256,'Mezquital','CULTURA',112,'Masculino','Policía','Número 5','¡Te contaré sobre el número 5!','Continuar>257','Images/Wixarika/Personajes/Iconos del Rostro/Policia (1)','256');
INSERT INTO `conversaciones` VALUES (257,'Mezquital','CULTURA',112,'Masculino','Policía','Número 5','El número 5 es importante en la cosmogonía <i>Wixárika</i>, “todo se hace siempre cinco veces”.','Continuar>258','Images/Wixarika/Iconos/Número cinco','257');
INSERT INTO `conversaciones` VALUES (258,'Mezquital','CULTURA',112,'Masculino','Policía','Número 5','Son 5 los lugares sagrados, 5 las direcciones del universo, 5 las facetas de <i>Tamatzi Kauyumari</i> (Nuestro hermano mayor Venado Azul), 5 las diosas del maíz y 5 cazadores cósmicos.','Continuar>-1','Images/Wixarika/Iconos/Número cinco','258');
INSERT INTO `conversaciones` VALUES (259,'Pueblo Nuevo','CULTURA',113,'Masculino','Policía','Cazadores cósmicos','¡Te contaré sobre los cazadores cósmicos!','Continuar>260','Images/Wixarika/Personajes/Iconos del Rostro/Policia (1)','259');
INSERT INTO `conversaciones` VALUES (260,'Pueblo Nuevo','CULTURA',113,'Masculino','Policía','Cazadores cósmicos','Existen cinco cazadores cósmicos, cada uno representa a un animal y a una dirección del universo:','Continuar>261','Images/Wixarika/Iconos/Número cinco','260');
INSERT INTO `conversaciones` VALUES (261,'Pueblo Nuevo','CULTURA',113,'Masculino','Policía','Cazadores cósmicos','- <i>Utútawi</i>: el <i>maye</i> (puma) cazador del norte.
- <i>Wewetsári</i>: el <i>tuwe</i> (jaguar) cazador del sur.
- <i>Tsipúrawi</i>: el <i>ɨxawe</i> (lobo) cazador del oeste.
- <i>Tutu Háuki</i>: el <i>kapɨwi</i> (lince) cazador del este.
- <i>Tututáka Pitsitéka</i>: el <i>mitsu</i> (gato montés) cazador del centro del universo.','Continuar>-1','Images/Wixarika/Iconos/Número cinco','261,262,263,264,265');
INSERT INTO `conversaciones` VALUES (262,'Durango','CULTURA',114,'Masculino','Policía','Venado azul','<i>Tamatzi Kauyumari</i> (Nuestro hermano mayor Venado Azul), es una deidad mensajera que se convierte en <i>hikuri</i> (peyote) e <i>ikú</i> (maíz).','Continuar>-1','Images/Wixarika/Personajes/Iconos del Rostro/C Venado Azul 3x','266');
INSERT INTO `conversaciones` VALUES (263,'Durango','CULTURA',115,'Masculino','Policía','Facetas del venado azul','¡Te contaré sobre las facetas de <i>Tamatzi Kauyumari</i> (Nuestro hermano mayor Venado Azul)!','Continuar>264','Images/Wixarika/Personajes/Iconos del Rostro/Policia (1)','267');
INSERT INTO `conversaciones` VALUES (264,'Durango','CULTURA',115,'Masculino','Policía','Facetas del venado azul','Esta deidad tiene cinco facetas, puede presentarse como:','Continuar>265','Images/Wixarika/Iconos/Número cinco','268');
INSERT INTO `conversaciones` VALUES (265,'Durango','CULTURA',115,'Masculino','Policía','Facetas del venado azul','- <i>Tamatzi Parietzika</i>: Dios del amanecer y la aurora.
- <i>Tamatzi Iriye</i>: Dios de los arqueros.
- <i>Tamatzi Eaka Teiwari</i>: Dios del viento.
- <i>Tamatzi Wakiri</i>: Dios del tepehuano.
- Y como <i>Tamatzi Kauyumari Maxa Yuavi</i>.','Continuar>-1','Images/Wixarika/Personajes/Iconos del Rostro/C Venado Azul 3x','269,270,271,272,273');
INSERT INTO `conversaciones` VALUES (266,'Canatlán','CULTURA',116,'Masculino','Policía','Watakame','¡Te contaré sobre el viaje en canoa de <i>Watakame</i>!','Continuar>267','Images/Wixarika/Personajes/Iconos del Rostro/Policia (1)','274');
INSERT INTO `conversaciones` VALUES (267,'Canatlán','CULTURA',116,'Masculino','Policía','Watakame','El kawitu (mito) dice que, <i>Watakame</i>, un campesino, fue elegido para sobrevivir al gran diluvio en una canoa.','Continuar>268','Images/Wixarika/Iconos/Cerro gordo','275');
INSERT INTO `conversaciones` VALUES (268,'Canatlán','CULTURA',116,'Masculino','Policía','Watakame','<i>Watakame</i> llevó consigo los cinco colores del ikú (maíz), una brasa y a su tsɨkɨ (perra) negra.','Continuar>269','Images/Wixarika/Iconos/Cerro gordo','276');
INSERT INTO `conversaciones` VALUES (269,'Canatlán','CULTURA',116,'Masculino','Policía','Watakame','El viaje terminó en el sitio sagrado <i>Hawxa Manaka</i>, para iniciar todo de nuevo y dar origen al pueblo <i>Wixárika</i>.','Continuar>-1','Images/Wixarika/Iconos/Cerro gordo','277');
INSERT INTO `conversaciones` VALUES (270,'Huejiquilla','CULTURA',117,'Masculino','Gobernador','Direcciones del universo','¡Te contaré sobre las direcciones del universo!','Continuar>271','Images/Wixarika/Personajes/Iconos del Rostro/Gobernador (1)','278');
INSERT INTO `conversaciones` VALUES (271,'Huejiquilla','CULTURA',117,'Masculino','Gobernador','Direcciones del universo','Existen cinco direcciones del universo, representadas por deidades femeninas:','Continuar>272','Images/Wixarika/Iconos/Direcciones del universo','279');
INSERT INTO `conversaciones` VALUES (272,'Huejiquilla','CULTURA',117,'Masculino','Gobernador','Direcciones del universo','- <i>Tatei Jautze Kupuri</i>: Diosa madre del norte.
- <i>Tatei Rapawiyema</i>: Diosa madre del sur.
- <i>Tatei Sakaimuka</i>: Diosa madre del oeste.
- <i>Tatei Uwiutali</i>: Diosa madre del este.
- <i>Tatei Kiewimuka</i>: Diosa de la lluvia del centro.
','Continuar>-1','Images/Wixarika/Iconos/Direcciones del universo','280,281,282,283,284');
INSERT INTO `conversaciones` VALUES (273,'Mezquitic','CULTURA',118,'Masculino','Gobernador','Nuestro padre el sol','¡Te contaré la historia del <i>táu</i> (sol)!','Continuar>274','Images/Wixarika/Personajes/Iconos del Rostro/Gobernador (1)','285');
INSERT INTO `conversaciones` VALUES (274,'Mezquitic','CULTURA',118,'Masculino','Gobernador','Nuestro padre el sol','La <i>xátsika</i> (leyenda) dice que, antes no había en el mundo más luz que la de la luna, por lo que, los <i>wixaritari</i> le rogaron a la luna que enviará a su hijo, un joven cojo y tuerto. Al principio, la luna se opuso, pero al final accedió.','Continuar>275','Images/Wixarika/Iconos/Sol','286');
INSERT INTO `conversaciones` VALUES (275,'Mezquitic','CULTURA',118,'Masculino','Gobernador','Nuestro padre el sol','Los <i>wixaritari</i> le pusieron un <i>kemari</i> (traje) y le dieron un <i>tɨɨpi</i> (arco) y <i>ɨ’rɨte</i> (flechas), luego lo arrojaron a un horno en <i>Te’akata</i> donde quedó consumido.','Continuar>276','Images/Wixarika/Iconos/A-Arco','287');
INSERT INTO `conversaciones` VALUES (276,'Mezquitic','CULTURA',118,'Masculino','Gobernador','Nuestro padre el sol','A los cinco días, el joven resucitó como <i>Tayau</i> (Nuestro padre el Sol), apareció el sol, irradió su luz sobre la tierra y completó su primer viaje por el cielo.','Continuar>-1','Images/Wixarika/Iconos/Número cinco','288');
INSERT INTO `conversaciones` VALUES (277,'Mezquitic','CULTURA',119,'Masculino','Gobernador','Historia del fuego','¡Te contaré la historia del <i>tái</i> (fuego)!','Continuar>278','Images/Wixarika/Escenario/Objetos/Fogata','289');
INSERT INTO `conversaciones` VALUES (278,'Mezquitic','CULTURA',119,'Masculino','Gobernador','Historia del fuego','La <i>xátsika</i> (leyenda) dice que, antes había seres monstruosos llamados <i>hewiixi</i> (caníbales), quienes no querían compartir el <i>tái</i> (fuego).','Continuar>279','Images/Wixarika/Escenario/Objetos/Fogata','290');
INSERT INTO `conversaciones` VALUES (279,'Mezquitic','CULTURA',119,'Masculino','Gobernador','Historia del fuego','Muchos animales intentaron conseguir el <i>tái</i> (fuego), pero, fue el tlacuache quien lo logró, al tomarlo rápidamente con su cola, por eso, la tiene pelada.','Continuar>280','Images/Wixarika/Escenario/Objetos/Fogata','291');
INSERT INTO `conversaciones` VALUES (280,'Mezquitic','CULTURA',119,'Masculino','Gobernador','Historia del fuego','El tlacuache compartió el <i>tái</i> (fuego) con las comunidades <i>Wixárika</i> de <i>Te’akata</i>, donde ahora habita <i>Tatewari</i> (Nuestro abuelo fuego).','Continuar>-1','Images/Wixarika/Iconos/Tekaata R','292');
INSERT INTO `conversaciones` VALUES (281,'Chapala','CULTURA',120,'Masculino','Gobernador','Diosas del agua','¡Te contaré sobre las diosas del agua!','Continuar>282','Images/Wixarika/Personajes/Iconos del Rostro/Gobernador (1)','293');
INSERT INTO `conversaciones` VALUES (282,'Chapala','CULTURA',120,'Masculino','Gobernador','Diosas del agua','Existen cinco diosas del agua, también llamadas `Madres de la lluvia`:','Continuar>283','Images/Wixarika/Iconos/Número cinco','294');
INSERT INTO `conversaciones` VALUES (283,'Chapala','CULTURA',120,'Masculino','Gobernador','Diosas del agua','- <i>Tatei Niaariwame</i>: lluvia del sur.
- <i>Tatei Yrameka</i> (Nuestra madre del retoño): lluvia del norte.
- <i>Tatei Kiewimuka</i> (Nuestra madre del venado): lluvia del oriente.
- <i>Tatei Matinieri</i>: lluvia del poniente.
- <i>Tatei Aitzarika</i>: lluvia del centro.','Continuar>-1','Images/Wixarika/Personajes/Iconos del Rostro/Gobernador (1)','295,296,297,298,299');
INSERT INTO `conversaciones` VALUES (284,'Miiki','CULTURA',121,'Masculino','Peyotero
','Ceremonia de los muertos','¡Te contaré sobre las ceremonias de los muertos!','Continuar>285','Images/Wixarika/Personajes/Iconos del Rostro/C Marakame 3x','300');
INSERT INTO `conversaciones` VALUES (285,'Miiki','CULTURA',121,'Masculino','Peyotero
','Ceremonia de los muertos','En la cultura <i>Wixárika</i> se honra a los difuntos de diferentes maneras: con el velorio, la despedida física, el camino al lugar de descanso y la despedida del alma.','Continuar>286','Images/Wixarika/Personajes/Iconos del Rostro/C Marakame 3x','301');
INSERT INTO `conversaciones` VALUES (286,'Miiki','CULTURA',121,'Masculino','Peyotero
','Ceremonia de los muertos','En el velorio se reúnen sus familiares, parientes y amigos para despedir al difunto. Luego llevan al difunto a su lugar de descanso y lo entierran hacia el poniente para que esté de frente al sol.','Continuar>287','Images/Wixarika/Personajes/Iconos del Rostro/C Marakame 3x','302');
INSERT INTO `conversaciones` VALUES (287,'Miiki','CULTURA',121,'Masculino','Peyotero
','Ceremonia de los muertos','La despedida del alma, es el <i>Mɨɨkí Kwevíxa</i> (invocar o llamar al muerto), una ceremonia que se realiza cinco días después del fallecimiento para que el difunto pueda despedirse de sus parientes.','Continuar>-1','Images/Wixarika/Personajes/Iconos del Rostro/C Marakame 3x','303');
INSERT INTO `conversaciones` VALUES (288,'Miiki','CULTURA',122,'Masculino','Peyotero
','Muerte','¡Te contaré sobre la <i>mɨɨkí</i> (muerte)!','Continuar>289','Images/Wixarika/Personajes/Iconos del Rostro/C Marakame 3x','304');
INSERT INTO `conversaciones` VALUES (289,'Miiki','CULTURA',122,'Masculino','Peyotero
','Muerte','Los <i>wixaritari</i> consideran que quien muere recorre toda su vida. Después, su espíritu llega a un río donde encuentra un tsɨkɨ (perro), al que tiene que darle tortillas para evitar ser mordido mientras cruza. Al final, encuentra a los animales que dañó en vida, y si estos son sagrados le caerá una roca que lo aplastará.','Continuar>290','Images/Wixarika/Personajes/Iconos del Rostro/C Marakame 3x','305');
INSERT INTO `conversaciones` VALUES (290,'Hewiixi','CULTURA',122,'Masculino','Peyotero
','Muerte','Cuando el difunto pasa las pruebas, se reúne con sus familiares difuntos y antepasados, que lo esperan con alegría y le hacen una fiesta.','Continuar>-1','Images/Wixarika/Personajes/Iconos del Rostro/C Marakame 3x','306');
INSERT INTO `conversaciones` VALUES (291,'Hewiixi','CULTURA',123,'Masculino','Peyotero
','Ceremonia de los muertos','¡Te contaré sobre el <i>Mɨɨkí Kwevíxa</i> (invocar o llamar al muerto)!','Continuar>292','Images/Wixarika/Personajes/Iconos del Rostro/C Marakame 3x','307');
INSERT INTO `conversaciones` VALUES (292,'Hewiixi','CULTURA',123,'Masculino','Peyotero
','Ceremonia de los muertos','Durante este ritual fúnebre, el <i>mara’kame</i> (chamán) con su <i>tunuiya</i> (canto sagrado), llama del inframundo el alma del difunto para que esté presente en la celebración.','Continuar>293','Images/Wixarika/Personajes/Iconos del Rostro/C Marakame 3x','308');
INSERT INTO `conversaciones` VALUES (293,'Hewiixi','CULTURA',123,'Masculino','Peyotero
','Ceremonia de los muertos','La familia del difunto lo espera con todo lo que le gustaba, lo saluda, lo llora y más tarde lo despiden. Ésta será su última partida.','Continuar>294','Images/Wixarika/Personajes/Iconos del Rostro/C Marakame 3x','309');
INSERT INTO `conversaciones` VALUES (294,'Hewiixi','CULTURA',123,'Masculino','Peyotero
','Ceremonia de los muertos','Sin embargo, la familia nunca pierde contacto con el difunto, pueden entrar a su <i>ririki</i> (pequeño santuario) para adorarlo y dirigirse a él.','Continuar>-1','Images/Wixarika/Personajes/Iconos del Rostro/C Marakame 3x','310');
INSERT INTO `conversaciones` VALUES (295,'Hewiixi','CULTURA',124,'Masculino','Peyotero
','Muerte','¡Te contaré sobre la <i>mɨɨkí</i> (muerte) y los tsɨkɨ (perros)!','Continuar>296','Images/Wixarika/Personajes/Iconos del Rostro/C Marakame 3x','311');
INSERT INTO `conversaciones` VALUES (296,'Hewiixi','CULTURA',124,'Masculino','Peyotero
','Muerte','Se dice que si alguien no alimentó o cuidó de su tsɨkɨ (perro) negro, cuando muera el tsɨkɨ (perro) va a estar esperándolo para morderlo.','Continuar>297','Images/Wixarika/Personajes/Iconos del Rostro/C Marakame 3x','312');
INSERT INTO `conversaciones` VALUES (297,'Hewiixi','CULTURA',124,'Masculino','Peyotero
','Muerte','Pero si la persona cuido del tsɨkɨ (perro) entonces todo será diferente, éste le dará agua, comida y buenos deseos.','Continuar>-1','Images/Wixarika/Personajes/Iconos del Rostro/C Marakame 3x','313');
INSERT INTO `conversaciones` VALUES (298,'Kieri','CULTURA',125,'Masculino','Peyotero
','Muerte','¡Te contaré sobre la <i>mɨɨkí</i> (muerte) y las deidades!','Continuar>299','Images/Wixarika/Personajes/Iconos del Rostro/C Marakame 3x','314');
INSERT INTO `conversaciones` VALUES (299,'Kieri','CULTURA',125,'Masculino','Peyotero
','Muerte','En la cultura <i>Wixárika</i>, los antepasados divinizados, junto con las deidades, rigen la sociedad de los vivos.','Continuar>300','Images/Wixarika/Personajes/Iconos del Rostro/C Marakame 3x','315');
INSERT INTO `conversaciones` VALUES (300,'Kieri','CULTURA',125,'Masculino','Peyotero
','Muerte','Si los <i>wixaritari</i> trasgreden las normas de convivencia, pueden ser castigados por las deidades y los parientes muertos, con enfermedades o desgracias, o bien, sus almas pueden quedar prisioneras eternamente en el inframundo.','Continuar>-1','Images/Wixarika/Personajes/Iconos del Rostro/C Marakame 3x','316');
INSERT INTO `conversaciones` VALUES (301,'Kieri','CULTURA',126,'Masculino','Peyotero
','Muerte','¡Te contaré sobre la <i>mɨɨkí</i> (muerte) y los más pequeños!','Continuar>302','Images/Wixarika/Personajes/Iconos del Rostro/C Marakame 3x','317');
INSERT INTO `conversaciones` VALUES (302,'Kieri','CULTURA',126,'Masculino','Peyotero
','Muerte','Cuando muere un bebé el rito funeral es diferente, pues el bebé es enterrado cerca de la casa para que no se pierda. En el lugar donde se entierra se pone comida y juguetes.','Continuar>303','Images/Wixarika/Personajes/Iconos del Rostro/C Marakame 3x','318');
INSERT INTO `conversaciones` VALUES (303,'Kieri','CULTURA',126,'Masculino','Peyotero
','Muerte','El bebé sigue formando parte de la familia, y al llegar un hermano, el bebé se encargará de jugar con él, aun cuando se piense que el niño juega solo.','Continuar>-1','Images/Wixarika/Personajes/Iconos del Rostro/C Marakame 3x','319');
INSERT INTO `conversaciones` VALUES (304,'Kieri','CULTURA',127,'Masculino','Peyotero
','Muerte','¡Te contaré sobre la <i>mɨɨkí</i> (muerte)!','Continuar>305','Images/Wixarika/Personajes/Iconos del Rostro/C Marakame 3x','320');
INSERT INTO `conversaciones` VALUES (305,'Kieri','CULTURA',127,'Masculino','Peyotero
','Muerte','Los <i>wixaritari</i> consideran que con ayuda del <i>hikuri</i> (peyote) y la cuidadosa guía del <i>mara’kame</i> (chamán) pueden comunicarse con los mɨɨkite (muertos).','Continuar>306','Images/Wixarika/Personajes/Iconos del Rostro/C Marakame 3x','321');
INSERT INTO `conversaciones` VALUES (306,'Kieri','CULTURA',127,'Masculino','Peyotero
','Muerte','Para esto, se induce a un sueño en el que puede pasar algo sagrado como convivir, danzar y reír con el ser querido que se ha ido.','Continuar>-1','Images/Wixarika/Personajes/Iconos del Rostro/C Marakame 3x','322');
INSERT INTO `conversaciones` VALUES (307,NULL,'NORMAL',128,'Femenino','Aldeana','Aviso','¡<i>Hamiku</i> (amigo), venciste a las sombras y recuperaste la cultura en esta comunidad!','Continuar>308','Images/Wixarika/Personajes/Iconos del Rostro/C Niña 1 3x','323');
INSERT INTO `conversaciones` VALUES (308,NULL,'NORMAL',128,'Femenino','Aldeana','Aviso','En agradecimiento, te obsequiamos esta <i>mawari</i> (ofrenda) para que la ofrezcas a una deidad.','Continuar>-1','Images/Wixarika/Personajes/Iconos del Rostro/C Niña 1 3x','324');
INSERT INTO `conversaciones` VALUES (309,NULL,'NORMAL',129,'Masculino','Aldeano','Aviso','¡<i>Hamiku</i> (amigo), venciste a las sombras y recuperaste la cultura en esta comunidad!','Continuar>310','Images/Wixarika/Personajes/Iconos del Rostro/C Maestro 3x','325');
INSERT INTO `conversaciones` VALUES (310,NULL,'NORMAL',129,'Masculino','Aldeano','Aviso','En agradecimiento, te obsequiamos esta <i>mawari</i> (ofrenda) para que la ofrezcas a una deidad.','Continuar>-1','Images/Wixarika/Personajes/Iconos del Rostro/C Maestro 3x','326');
INSERT INTO `conversaciones` VALUES (311,NULL,'NORMAL',130,'Femenino','Aldeana','Aviso','<i>Yeikame</i> (viajero) en esta zona se encuentra un enemigo peligroso, defiéndete y véncelo para que no continúe dañando a las comunidades, ¡mucha suerte!','Continuar>-1','Images/Wixarika/Personajes/Iconos del Rostro/C Niña 1 3x','327');
INSERT INTO `conversaciones` VALUES (312,NULL,'NORMAL',131,'Masculino','Aldeano','Aviso','<i>Yeikame</i> (viajero) en esta zona se encuentra un enemigo peligroso, defiéndete y véncelo para que no continúe dañando a las comunidades, ¡mucha suerte!','Continuar>-1','Images/Wixarika/Personajes/Iconos del Rostro/C Maestro 3x','328');
INSERT INTO `conversaciones` VALUES (313,NULL,'NORMAL',132,'Femenino','Aldeana','Aviso','<i>Hamiku</i> (amigo), derrotaste al <i>mara’kame</i> (chamán) corrompido que estaba atacando a las comunidades.','Continuar>314','Images/Wixarika/Personajes/Iconos del Rostro/C Niña 1 3x','329');
INSERT INTO `conversaciones` VALUES (314,NULL,'NORMAL',132,'Femenino','Aldeana','Aviso','En agradecimiento, te obsequiamos esta <i>mawari</i> (ofrenda) para que la ofrezcas a una deidad.','Continuar>-1','Images/Wixarika/Personajes/Iconos del Rostro/C Niña 1 3x','330');
INSERT INTO `conversaciones` VALUES (315,NULL,'NORMAL',133,'Masculino','Aldeano','Aviso','<i>Hamiku</i> (amigo), derrotaste al <i>mara’kame</i> (chamán) corrompido que estaba atacando a las comunidades.','Continuar>316','Images/Wixarika/Personajes/Iconos del Rostro/C Maestro 3x','331');
INSERT INTO `conversaciones` VALUES (316,NULL,'NORMAL',133,'Masculino','Aldeano','Aviso','En agradecimiento, te obsequiamos esta <i>mawari</i> (ofrenda) para que la ofrezcas a una deidad.','Continuar>-1','Images/Wixarika/Personajes/Iconos del Rostro/C Maestro 3x','332');
INSERT INTO `conversaciones` VALUES (317,'Del Nayar','NORMAL',134,'Femenino','Aldeano','Aviso','<i>Yeikame</i> (viajero), para ayudar al pueblo <i>Wixárika</i> debes recorrer las comunidades y aprender sobre su cultura.','Continuar>-1','Images/Wixarika/Personajes/Iconos del Rostro/C Maestro 3x','333');
INSERT INTO `conversaciones` VALUES (318,'Valparaíso','NORMAL',135,'Masculino','Aldeana','Aviso','<i>Hamiku</i> (amigo), para ayudar al pueblo <i>Wixárika</i> debes recorrer las comunidades y aprender sobre su cultura.','Continuar>-1','Images/Wixarika/Personajes/Iconos del Rostro/C Niña 1 3x','334');
INSERT INTO `conversaciones` VALUES (319,NULL,'NORMAL',136,'Masculino','Mara´kame malo','Pelea','<i>Muwieri</i>, no permitiré que sigas salvando a las comunidades <i>Wixárika</i>. Si quieres continuar tu camino, primero tendrás que vencerme.','Continuar>320','Images/Wixarika/Personajes/Iconos del Rostro/C Malo 5 3x','335');
INSERT INTO `conversaciones` VALUES (320,NULL,'NORMAL',136,'Masculino','Mara´kame malo','Pelea','¡Ganaste esta pelea! Pero los otros <i>mara’kate</i> (chamanes) corrompidos y sus sombras te detendrán.','Continuar>-1','Images/Wixarika/Personajes/Iconos del Rostro/C Malo 5 3x','336');
INSERT INTO `conversaciones` VALUES (321,'San blas','DEIDADES',137,'Femenino','Tatei Haramara','Tatei Haramara','¡<i>Ke’aku</i> (hola) <i>Muwieri</i>! Estaba esperándote. He recibido tu <i>mawari</i> (ofrenda), así que escucharé tus peticiones.','Continuar>322','Images/Wixarika/Personajes/Iconos del Rostro/C Tatei Haramara 3x','337');
INSERT INTO `conversaciones` VALUES (322,'San blas','DEIDADES',137,'Masculino','Muwieri','Tatei Haramara','¡<i>Tatei</i> (madre)! He venido porque las comunidades <i>Wixárika</i> están en peligro. ¡Ayúdame a protegerlas! ¿Qué tengo que hacer ahora?','Continuar>323','Images/Wixarika/Personajes/Iconos del Rostro/Repuesta correcta 3x','338');
INSERT INTO `conversaciones` VALUES (323,'San blas','DEIDADES',137,'Femenino','Tatei Haramara','Tatei Haramara','Debes de seguir el rombo <i>Wixárika</i>, dirígete hacia <i>Wirikuta</i> en San Luis Potosí, para vencer al próximo <i>mara’kame</i> (chamán) corrompido y sus sombras.','Continuar>324','Images/Wixarika/Personajes/Iconos del Rostro/C Tatei Haramara 3x','339');
INSERT INTO `conversaciones` VALUES (324,'San blas','DEIDADES',137,'Femenino','Tatei Haramara','Tatei Haramara','Durante el camino deberás protegerte, te obsequio estos <i>iku’ute</i> (maíces) para que mejores tus armas.','Continuar>325','Images/Wixarika/Personajes/Iconos del Rostro/C Tatei Haramara 3x','340');
INSERT INTO `conversaciones` VALUES (325,'San blas','DEIDADES',137,'Masculino','Muwieri','Tatei Haramara','¡Gracias <i>Tatei</i>! Continuaré con valentía.','Continuar>326','Images/Wixarika/Personajes/Iconos del Rostro/Repuesta correcta 3x','341');
INSERT INTO `conversaciones` VALUES (326,'San blas','DEIDADES',137,'Femenino','Tatei Haramara','Tatei Haramara','Recuerda <i>Muwieri</i>, no estás solo, las deidades te ayudaremos en tu camino, ¡buena suerte!','Continuar>-1','Images/Wixarika/Personajes/Iconos del Rostro/C Tatei Haramara 3x','342');
INSERT INTO `conversaciones` VALUES (327,'Santo Domingo','DEIDADES',138,'Femenino','Tatei Kutsaraipa','Tatei Kutsaraipa','¡<i>Ke’aku</i> (hola) <i>Muwieri</i>! Recibí tu <i>mawari</i> (ofrenda), así que escucharé tus peticiones.','Continuar>328','Images/Wixarika/Personajes/Iconos del Rostro/Tatei Kutsaraɨpa 3x (1)','343');
INSERT INTO `conversaciones` VALUES (328,'Santo Domingo','DEIDADES',138,'Masculino','Muwieri','Tatei Kutsaraipa','¡<i>Tatei</i> (madre)! He venido porque las comunidades <i>Wixárika</i> están en peligro. ¡Ayúdame a protegerlas!','Continuar>329','Images/Wixarika/Personajes/C Muwieri 3 3x','344');
INSERT INTO `conversaciones` VALUES (329,'Santo Domingo','DEIDADES',138,'Femenino','Tatei Kutsaraipa','Tatei Kutsaraipa','Te obsequio estos <i>iku’ute</i> (maíces) para que mejores tus armas, pues durante el camino deberás protegerte del <i>mara’kame</i> (chamán) corrompido y sus sombras.','Continuar>330','Images/Wixarika/Personajes/Iconos del Rostro/Tatei Kutsaraɨpa 3x (1)','345');
INSERT INTO `conversaciones` VALUES (330,'Santo Domingo','DEIDADES',138,'Femenino','Tatei Kutsaraipa','Tatei Kutsaraipa','Ahora, dirígete hacia <i>Wirikuta</i> en el desierto de San Luis Potosí.','Continuar>331','Images/Wixarika/Personajes/Iconos del Rostro/Tatei Kutsaraɨpa 3x (1)','346');
INSERT INTO `conversaciones` VALUES (331,'Santo Domingo','DEIDADES',138,'Masculino','Muwieri','Tatei Kutsaraipa','¡Gracias <i>Tatei</i>! Continuaré con valentía.','Continuar>-1','Images/Wixarika/Personajes/Iconos del Rostro/Repuesta correcta 3x','347');
INSERT INTO `conversaciones` VALUES (332,'Real de Catorce','DEIDADES',139,'Masculino','Tututzi Maxa Kwaxi','Tututzi Maxa Kwaxi','¡<i>Ke’aku</i> (hola) <i>Muwieri</i>! Recibí tu <i>mawari</i> (ofrenda), así que escucharé tus peticiones.','Continuar>333','Images/Wixarika/Personajes/Iconos del Rostro/C Venado Azul 3x','348');
INSERT INTO `conversaciones` VALUES (333,'Real de Catorce','DEIDADES',139,'Masculino','Muwieri','Tututzi Maxa Kwaxi','He venido porque las comunidades <i>Wixárika</i> están en peligro, ¡ayúdame a protegerlas!','Continuar>334','Images/Wixarika/Iconos/Neixa','349');
INSERT INTO `conversaciones` VALUES (334,'Real de Catorce','DEIDADES',139,'Masculino','Tututzi Maxa Kwaxi','Tututzi Maxa Kwaxi','Te obsequio estos <i>iku’ute</i> (maíces) para que mejores tus armas, pues durante el camino deberás protegerte del <i>mara’kame</i> (chamán) corrompido y sus sombras.','Continuar>335','Images/Wixarika/Iconos/Maíz 3x','350');
INSERT INTO `conversaciones` VALUES (335,'Real de Catorce','DEIDADES',139,'Masculino','Tututzi Maxa Kwaxi','Tututzi Maxa Kwaxi','Ahora, dirígete hacia <i>Hauxa Manaka</i> en el Cerro Gordo, Durango.','Continuar>336','Images/Wixarika/Personajes/Iconos del Rostro/C Venado Azul 3x','351');
INSERT INTO `conversaciones` VALUES (336,'Real de Catorce','DEIDADES',139,'Masculino','Muwieri','Tututzi Maxa Kwaxi','¡Gracias! Continuaré con valentía.','Continuar>-1','Images/Wixarika/Personajes/Iconos del Rostro/Repuesta correcta 3x','352');
INSERT INTO `conversaciones` VALUES (337,'Canatlán','DEIDADES',140,'Femenino','Tatei Yurienáka','Tatei Yurienáka','¡<i>Ke’aku</i> (hola) <i>Muwieri</i>! Recibí tu <i>mawari</i> (ofrenda), así que escucharé tus peticiones.','Continuar>328','Images/Wixarika/Personajes/Iconos del Rostro/C Tatei Yurienáka 3x','353');
INSERT INTO `conversaciones` VALUES (338,'Canatlán','DEIDADES',140,'Masculino','Muwieri','Tatei Yurienáka','¡<i>Tatei</i> (madre)! He venido porque las comunidades <i>Wixárika</i> están en peligro. ¡Ayúdame a protegerlas!','Continuar>329','Images/Wixarika/Personajes/C Muwieri 3 3x','354');
INSERT INTO `conversaciones` VALUES (339,'Canatlán','DEIDADES',140,'Femenino','Tatei Yurienáka','Tatei Yurienáka','Te obsequio estos <i>iku’ute</i> (maíces) para que mejores tus armas, pues durante el camino deberás protegerte del <i>mara’kame</i> (chamán) corrompido y sus sombras.','Continuar>340','Images/Wixarika/Personajes/Iconos del Rostro/C Tatei Yurienáka 3x','355');
INSERT INTO `conversaciones` VALUES (340,'Canatlán','DEIDADES',140,'Femenino','Tatei Yurienáka','Tatei Yurienáka','Ahora, dirígete hacia <i>Te’akata</i> en Mezquitic, Jalisco.','Continuar>341','Images/Wixarika/Personajes/Iconos del Rostro/C Tatei Yurienáka 3x','356');
INSERT INTO `conversaciones` VALUES (341,'Canatlán','DEIDADES',140,'Masculino','Muwieri','Tatei Yurienáka','¡Gracias <i>Tatei</i>! Continuaré con valentía.','Continuar>-1','Images/Wixarika/Personajes/Iconos del Rostro/Repuesta correcta 3x','357');
INSERT INTO `conversaciones` VALUES (342,'Mezquitic','DEIDADES',141,'Masculino','Tatewari','Tatewari','¡<i>Ke’aku</i> (hola) <i>Muwieri</i>! Recibí tu <i>mawari</i> (ofrenda), así que escucharé tus peticiones.','Continuar>343','Images/Wixarika/Personajes/Iconos del Rostro/C Tatewari 3x','358');
INSERT INTO `conversaciones` VALUES (343,'Mezquitic','DEIDADES',141,'Masculino','Muwieri','Tatewari','He venido porque las comunidades <i>Wixárika</i> están en peligro, ¡ayúdame a protegerlas!','Continuar>344','Images/Wixarika/Personajes/C Muwieri 3 3x','359');
INSERT INTO `conversaciones` VALUES (344,'Mezquitic','DEIDADES',141,'Masculino','Tatewari','Tatewari','Te obsequio estos <i>iku’ute</i> (maíces) para que mejores tus armas, pues durante el camino deberás protegerte del <i>mara’kame</i> (chamán) corrompido y sus sombras.','Continuar>345','Images/Wixarika/Personajes/Iconos del Rostro/C Tatewari 3x','360');
INSERT INTO `conversaciones` VALUES (345,'Mezquitic','DEIDADES',141,'Masculino','Tatewari','Tatewari','Ahora, dirígete hacia <i>Xapawiyemeta</i> en La Laguna de los Alacranes, en Chapala, Jalisco.','Continuar>346','Images/Wixarika/Personajes/Iconos del Rostro/C Tatewari 3x','361');
INSERT INTO `conversaciones` VALUES (346,'Mezquitic','DEIDADES',141,'Masculino','Muwieri','Tatewari','¡Gracias! Continuaré con valentía.','Continuar>-1','Images/Wixarika/Personajes/Iconos del Rostro/Repuesta correcta 3x','362');
INSERT INTO `conversaciones` VALUES (347,'Chapala','DEIDADES',142,'Femenino','Tatei Xapawiyeme','Tatei Xapawiyeme','¡<i>Ke’aku</i> (hola) <i>Muwieri</i>! Estaba esperándote. He recibido tu <i>mawari</i> (ofrenda), así que escucharé tus peticiones.','Continuar>348','Images/Wixarika/Personajes/Iconos del Rostro/C Takútsi Nakawé 3x','363');
INSERT INTO `conversaciones` VALUES (348,'Chapala','DEIDADES',142,'Masculino','Muwieri','Tatei Xapawiyeme','¡<i>Tatei</i> (madre)! He recuperado la cultura en las comunidades <i>Wixárika</i>.','Continuar>349','Images/Wixarika/Personajes/Iconos del Rostro/C Muwieri 3 3x','364');
INSERT INTO `conversaciones` VALUES (349,'Chapala','DEIDADES',142,'Masculino','Muwieri','Tatei Xapawiyeme','Ahora, necesito tu ayuda para vencer al último <i>mara’kame</i> (chamán) corrompido y sus sombras.','Continuar>350','Images/Wixarika/Personajes/Iconos del Rostro/C Muwieri 3 3x','365');
INSERT INTO `conversaciones` VALUES (350,'Chapala','DEIDADES',142,'Femenino','Tatei Xapawiyeme','Tatei Xapawiyeme','Te obsequio estos <i>iku’ute</i> (maíces) para que mejores tus armas.','Continuar>351','Images/Wixarika/Iconos/Maíz 3x','366');
INSERT INTO `conversaciones` VALUES (351,'Chapala','DEIDADES',142,'Masculino','Muwieri','Tatei Xapawiyeme','¡Gracias <i>Tatei</i>! Continuaré con valentía.','Continuar>-1','Images/Wixarika/Personajes/Iconos del Rostro/C Muwieri 3 3x','367');
INSERT INTO `conversaciones` VALUES (352,'Zacatecas','DEIDADES',143,'Masculino','Aldeano','Nairy/Takutsi Nakawé','¡Las deidades escucharán tus plegarias y te ayudarán en el camino!','Continuar>-1','Images/Wixarika/Personajes/Iconos del Rostro/C Maestro 3x','368');
INSERT INTO `conversaciones` VALUES (353,'Real de Catorce','DEIDADES',144,'Femenino','Aldeana','Tayau','¡La deidad escuchará tus plegarias y te ayudará en el camino!','Continuar>-1','Images/Wixarika/Personajes/Iconos del Rostro/C Niña 1 3x','369');
INSERT INTO `conversaciones` VALUES (354,'Todos','SOMBRAS',145,'Masculino','Aldeano','Sombras','Necesitamos de tu ayuda, hay unas sombras que están atacando la comunidad','Continuar>-1','Images/Wixarika/Personajes/Iconos del Rostro/C Maestro 3x','370');
INSERT INTO `conversaciones` VALUES (355,'Todos','SOMBRAS',146,'Masculino','Aldeano','Jefe Final','No podrán detenerme','Continuar>-1','Images/Wixarika/Personajes/Iconos del Rostro/C Malo 5 3x','371');
INSERT INTO `textos` VALUES (1,'INTRO PUEBLO WIXARIKA','<i>Wixárika</i>: el pueblo que sueña');
INSERT INTO `textos` VALUES (2,'INTRO PUEBLO WIXARIKA','El pueblo <i>Wixárika</i> es un grupo étnico originario de México, que habita en la Sierra Madre Occidental, en los estados de Jalisco, Durango, Zacatecas y Nayarit. ');
INSERT INTO `textos` VALUES (3,'INTRO PUEBLO WIXARIKA','Los <i>wixaritari</i> (plural de <i>wixárika</i>) tienen su propia lengua, historia, tradiciones, festividades, medicina tradicional, artesanías, vestimenta, música y gastronomía.');
INSERT INTO `textos` VALUES (4,'INTRO PUEBLO WIXARIKA','Los <i>wixaritari</i> consideran que cada elemento de la naturaleza tiene un dios, por ello, se mantienen en armonía con el medio ambiente y cuidan de los territorios sagrados.');
INSERT INTO `textos` VALUES (5,'INTRO PUEBLO WIXARIKA','Las deidades <i>Wixárika</i> representan a elementos del entorno como el fuego, el agua, la tierra, la lluvia, el maíz, el venado y el sol.');
INSERT INTO `textos` VALUES (6,'INTRO PUEBLO WIXARIKA','El pueblo <i>Wixárika</i> ha conservado su identidad y cultura, gracias a su conexión con la naturaleza, y su compromiso de conservar su historia, lengua, tradiciones y conocimientos.');
INSERT INTO `textos` VALUES (7,'INTRO PUEBLO WIXARIKA','Aprendamos de su fascinante cultura, que tiene un estrecho vínculo con algunas de nuestras tradiciones y costumbres.');
INSERT INTO `textos` VALUES (8,'INTRO PUEBLO WIXARIKA','Para conocer la cultura <i>Wixárika</i> atravesarás una aventura, ¿estás preparado?');
INSERT INTO `textos` VALUES (1,'HISTORIA','La oscuridad quiere destruir al pueblo <i>Wixárika</i>.');
INSERT INTO `textos` VALUES (2,'HISTORIA','Ha corrompido a algunos de nuestros hermanos <i>Wixárika</i>.');
INSERT INTO `textos` VALUES (3,'HISTORIA','Los ha convertido en seres ansiosos de poder...');
INSERT INTO `textos` VALUES (4,'HISTORIA','...que buscan destruir nuestras comunidades para tener el control.');
INSERT INTO `textos` VALUES (5,'HISTORIA','Con la fuerza de la oscuridad invocan sombras que...');
INSERT INTO `textos` VALUES (6,'HISTORIA','…entran a la comunidad y destrozan todo a su paso.');
INSERT INTO `textos` VALUES (7,'HISTORIA','Nuestro pueblo lucha por mantenerse.');
INSERT INTO `textos` VALUES (8,'HISTORIA','Y aunque es complicado vencer a los enemigos...');
INSERT INTO `textos` VALUES (9,'HISTORIA','...nuestras deidades nos protegen de la oscuridad.');
INSERT INTO `textos` VALUES (10,'HISTORIA','<i>Tatei Niwetsika</i>, diosa del maíz, cuida de la comunidad afectada.');
INSERT INTO `textos` VALUES (11,'HISTORIA','Y nos ayuda a encontrar un héroe honesto y valiente…');
INSERT INTO `textos` VALUES (12,'HISTORIA','...que proteja a las otras comunidades.');
INSERT INTO `textos` VALUES (13,'HISTORIA','<i>Muwieri</i>, con ayuda de las deidades <i>Wixárika</i>...');
INSERT INTO `textos` VALUES (14,'HISTORIA','...es el elegido para protegernos de la oscuridad.');
INSERT INTO `textos` VALUES (15,'HISTORIA','<i>Muwieri</i>, deberás visitar los templos de las deidades y pedirles su apoyo para proteger las demás comunidades <i>Wixárika</i>.');
INSERT INTO `textos` VALUES (16,'HISTORIA','<i>Muwieri</i>, ¿estás listo para salvar al pueblo <i>Wixárika</i>?');
INSERT INTO `textos` VALUES (1,'ROMBO','¡<i>Ke’aku</i> (hola) <i>yeikame</i> (viajero)! Te contaré sobre el Rombo <i>Wixárika</i>.');
INSERT INTO `textos` VALUES (2,'ROMBO','Para el pueblo <i>Wixárika</i> existen cinco puntos importantes para la creación del mundo: norte, sur, este, oeste y centro.');
INSERT INTO `textos` VALUES (3,'ROMBO','Estos puntos se representan por lugares sagrados, que al unirlos con una línea se forma un rombo que pasa por los estados de Nayarit, Durango, Jalisco, San Luis Potosí y Zacatecas.');
INSERT INTO `textos` VALUES (4,'ROMBO','En el centro del Rombo <i>Wixárika</i> se encuentra el sitio sagrado <i>Te’akata</i>, ahí inicia tu viaje para proteger a las comunidades <i>Wixárika</i>.');
INSERT INTO `textos` VALUES (5,'ROMBO','Primero, dirígete hacia <i>Haramara</i>, en San Blas, Nayarit.');
INSERT INTO `textos` VALUES (1,'TUTORIAL','<i>¡Ke’aku</i> (hola) <i>hamiku</i> (amigo)! Te mostraré algunos indicadores importantes en el juego:');
INSERT INTO `textos` VALUES (2,'TUTORIAL','En la parte superior podrás revisar tu barra de vida, barra de energía, avance en el nivel, puntos y maíces acumulados.');
INSERT INTO `textos` VALUES (3,'TUTORIAL','En la parte inferior podrás acceder al panel de control, armas, vestimenta, inventario y diario de viaje.');
INSERT INTO `textos` VALUES (4,'TUTORIAL','¡A jugar <i>hamiku</i> (amigo)!');
INSERT INTO `textos` VALUES (1,'EXPLICACION_QUIZ_NORMAL','¡Hola <i>hamiku</i> (amigo)! Bienvenido a la sección de preguntas y respuestas de la cultura <i>Wixárika</i>.');
INSERT INTO `textos` VALUES (2,'EXPLICACION_QUIZ_NORMAL','En esta sección podrás ganar mucho <i>ikú</i> (maíz) y tu pase al siguiente nivel.');
INSERT INTO `textos` VALUES (3,'EXPLICACION_QUIZ_NORMAL','Para jugar debes responder seis preguntas de opción múltiple.');
INSERT INTO `textos` VALUES (4,'EXPLICACION_QUIZ_NORMAL','Para responder cada pregunta tienes dos oportunidades.');
INSERT INTO `textos` VALUES (5,'EXPLICACION_QUIZ_NORMAL','Si aciertas, ¡ganarás <i>ikú</i> (maíz)!');
INSERT INTO `textos` VALUES (6,'EXPLICACION_QUIZ_NORMAL','Si tienes dudas en la respuesta puedes comprar hasta tres pistas.');
INSERT INTO `textos` VALUES (7,'EXPLICACION_QUIZ_NORMAL','Si respondes correctamente todas las preguntas en el primer intento, ¡ganarás un premio especial!');
INSERT INTO `textos` VALUES (8,'EXPLICACION_QUIZ_NORMAL','Para pasar al siguiente nivel debes obtener cuatro aciertos o más, si no debes repetirlo.');
INSERT INTO `textos` VALUES (9,'EXPLICACION_QUIZ_NORMAL','Vamos a jugar, ¡mucha suerte <i>hamiku</i> (amigo)!');
INSERT INTO `textos` VALUES (1,'EXPLICACION_QUIZ_REDUCIDA','Bienvenido nuevamente a la sección de preguntas y respuestas de la cultura <i>Wixárika</i>.');
INSERT INTO `textos` VALUES (2,'EXPLICACION_QUIZ_REDUCIDA','Recuerda que aparecerán seis preguntas de opción múltiple que debes responder correctamente para obtener tu pase al siguiente nivel y ganar <i>ikú</i>.');
INSERT INTO `textos` VALUES (3,'EXPLICACION_QUIZ_REDUCIDA','Para responder cada pregunta tienes dos oportunidades.');
INSERT INTO `textos` VALUES (4,'EXPLICACION_QUIZ_REDUCIDA','Si tienes dudas en la respuesta puedes comprar hasta tres pistas.');
INSERT INTO `textos` VALUES (5,'EXPLICACION_QUIZ_REDUCIDA','¡Mucha suerte <i>hamiku</i> (amigo)!');
INSERT INTO `textos` VALUES (1,'TUTORIAL_QUIZ','¡<i>Hamiku</i> (amigo)! Te mostraré algunos indicadores importantes de la sección de preguntas y respuestas:');
INSERT INTO `textos` VALUES (2,'TUTORIAL_QUIZ','En la parte superior podrás ver el número de pregunta y revisar si respondiste correctamente la pregunta al primera o segunda oportunidad, o si respondiste incorrectamente.');
INSERT INTO `textos` VALUES (3,'TUTORIAL_QUIZ','En la parte lateral podrás comprar pistas para responder las preguntas.');
INSERT INTO `textos` VALUES (4,'TUTORIAL_QUIZ','En la sección de preguntas podrás ver la pregunta y revisar las opciones de respuesta.');
INSERT INTO `textos` VALUES (5,'TUTORIAL_QUIZ','¡Diviértete <i>hamiku</i> (amigo)!');
INSERT INTO `textos` VALUES (1,'ORIGEN PUEBLO WIXARIKA','El origen del pueblo <i>Wixárika</i>');
INSERT INTO `textos` VALUES (2,'ORIGEN PUEBLO WIXARIKA','Los <i>wixaritari salieron del mar para dirigirse a <i>Wirikuta</i> a presenciar el nacimiento del sol.');
INSERT INTO `textos` VALUES (3,'ORIGEN PUEBLO WIXARIKA','Durante su travesía, todos los que se rezagaron en la peregrinación, se transformaron en rocas, cerros, ríos o lagos, seres de la naturaleza que poblaron el mundo.');
INSERT INTO `textos` VALUES (4,'ORIGEN PUEBLO WIXARIKA','Asimismo, durante una época de hambre y sequía, <i>Tamatzi Kauyumari</i>, Nuestro hermano el Venado Azul, llevó a los <i>wixaritari</i> hasta la zona sagrada de <i>Wirikuta</i>.');
INSERT INTO `textos` VALUES (5,'ORIGEN PUEBLO WIXARIKA','Donde se convirtió en <i>ikú</i> (maíz) y <i>hikuri</i> (peyote) para alimentarlos.');
INSERT INTO `textos` VALUES (6,'ORIGEN PUEBLO WIXARIKA','<i>Wirikuta</i> es un territorio sagrado donde habitan los dioses y ancestros espirituales.');
INSERT INTO `textos` VALUES (7,'ORIGEN PUEBLO WIXARIKA','Cada año, las comunidades <i>Wixárika</i> peregrinan hacia allí, para recrear los pasos de los antepasados y pedir lluvia y bienestar.');
INSERT INTO `textos` VALUES (8,'ORIGEN PUEBLO WIXARIKA','<i>Hamiku</i> (amigo), ¡vamos hacia <i>Wirikuta</i>!');
INSERT INTO `textos` VALUES (1,'XAPAWIYEMETA','Dirijete a Xapawiyeme - Xapawiyemeta, lugar donde tocó tierra Watakame después del diluvio. Sitio sagrado ubicado en la Isla de Los Alacranes, en el Lago de Chapala, Jalisco');
INSERT INTO `textos` VALUES (1,'HAUXAMANAKA','Dirijete a Hauxamanaka , sitio sagrado en el que la canoa de Watakame dejó su varado, ubicado en la parte alta del cerro Gordo, en la comunidad Q''dam de San Bemardino Milpillas Chico, Pueblo Nuevo, Durango. ');
INSERT INTO `textos` VALUES (1,'WIRIKUTA','Dirijete a Wirikuta este se encuentra al Oriente, por donde se levanta el Sol: está ubicado en el semi-desierto de San Luis Potosí, en los municipios de Real de Catorce, Villa de la Paz, Matehuala, Villa de Guadalupe, Charcas y Villa de Ramos. ');
INSERT INTO `objetivos` VALUES (1,1,3,'Santa María del Oro','ALIMENTO',-1,1,'RECOLECTA','Recolecta <i>xiete</i> (miel)');
INSERT INTO `objetivos` VALUES (2,1,5,'Santiago Ixcuintla','CONVERSACION',-1,1,'CONVERSA','Aprende sobre los animales mensajeros');
INSERT INTO `objetivos` VALUES (3,1,6,'San Blas','SITIO_IMPORTANTE',-1,1,'VISITA','Visita el sitio sagrado de <i>Haramara</i>');
INSERT INTO `objetivos` VALUES (4,1,1,'Del Nayar','ALIMENTO',29,1,'RECOLECTA','Recolecta <i>ikú</i> (maíz)');
INSERT INTO `objetivos` VALUES (5,1,1,'Del Nayar','ALIMENTO',19,1,'RECOLECTA','Recolecta <i>múme</i> (frijol)');
INSERT INTO `objetivos` VALUES (6,1,1,'Del Nayar','CONVERSACION',62,1,'CONVERSA','Aprende sobre el agua');
INSERT INTO `objetivos` VALUES (7,1,1,'Del Nayar','CONVERSACION',63,1,'CONVERSA','Aprende sobre la tortilla');
INSERT INTO `objetivos` VALUES (8,1,1,'Del Nayar','CONVERSACION',7,1,'CONVERSA','Aprende sobre el <i>mara´kame</i> (chamán)');
INSERT INTO `objetivos` VALUES (9,1,1,'Del Nayar','CONVERSACION',24,1,'CONVERSA','Aprende sobre el <i>muka´etsa</i> (agricultor)');
INSERT INTO `objetivos` VALUES (10,1,1,'Del Nayar','SITIO_IMPORTANTE',1,1,'VISITA','Visita el sitio sagrado de La Mesa del Nayar');
INSERT INTO `objetivos` VALUES (11,1,1,'Del Nayar','SITIO_IMPORTANTE',2,1,'VISITA','Visita el sitio sagrado de Santa Teresa');
INSERT INTO `objetivos` VALUES (12,1,2,'La Yesca','ALIMENTO',29,1,'RECOLECTA','Recolecta <i>ikú</i> (maíz)');
INSERT INTO `objetivos` VALUES (13,1,2,'La Yesca','ALIMENTO',17,1,'RECOLECTA','Recolecta <i>kukúri</i> (chile)');
INSERT INTO `objetivos` VALUES (14,1,2,'La Yesca','CONVERSACION',65,1,'CONVERSA','Aprende sobre el queso');
INSERT INTO `objetivos` VALUES (15,1,2,'La Yesca','CONVERSACION',64,1,'CONVERSA','Aprende sobre la albóndiga de jabalí');
INSERT INTO `objetivos` VALUES (16,1,2,'La Yesca','PLANTA_MEDICINAL',11,1,'RECOLECTA','Recolecta milpa');
INSERT INTO `objetivos` VALUES (17,1,2,'La Yesca','CONVERSACION',25,1,'CONVERSA','Aprende sobre el cazador');
INSERT INTO `objetivos` VALUES (18,1,2,'La Yesca','CONVERSACION',97,1,'CONVERSA','Conoce el camino del corazón');
INSERT INTO `objetivos` VALUES (19,1,2,'La Yesca','SITIO_IMPORTANTE',3,1,'VISITA','Visita el sitio sagrado de Guadalupe Ocotán');
INSERT INTO `objetivos` VALUES (20,1,3,'Santa María del Oro','ALIMENTO',2,1,'RECOLECTA','Recolecta <i>uwá</i> (caña)');
INSERT INTO `objetivos` VALUES (21,1,3,'Santa María del Oro','CONVERSACION',66,1,'CONVERSA','Aprende sobre el piloncillo');
INSERT INTO `objetivos` VALUES (22,1,3,'Santa María del Oro','CONVERSACION',67,1,'CONVERSA','Aprende sobre el pescado');
INSERT INTO `objetivos` VALUES (23,1,3,'Santa María del Oro','CONVERSACION',26,1,'CONVERSA','Aprende sobre los trajes trajes tradicionales');
INSERT INTO `objetivos` VALUES (24,1,3,'Santa María del Oro','CONVERSACION',98,1,'CONVERSA','Aprende sobre la deidad <i>Tamatzi Kauyumari</i> (venado azul)');
INSERT INTO `objetivos` VALUES (25,1,3,'Santa María del Oro','SITIO_IMPORTANTE',4,1,'VISITA','Visita el sitio sagrado de La Laguna de Santa María del Oro');
INSERT INTO `objetivos` VALUES (26,1,4,'Tepic','ALIMENTO',22,1,'RECOLECTA','Recolecta <i>xa’ata</i> (jícama)');
INSERT INTO `objetivos` VALUES (27,1,4,'Tepic','ALIMENTO',3,1,'RECOLECTA','Recolecta <i>kwarɨpa</i> (ciruela)');
INSERT INTO `objetivos` VALUES (28,1,4,'Tepic','CONVERSACION',68,1,'CONVERSA','Aprende sobre el mole de venado');
INSERT INTO `objetivos` VALUES (29,1,4,'Tepic','PLANTA_MEDICINAL',10,1,'RECOLECTA','Recolecta manzanilla');
INSERT INTO `objetivos` VALUES (30,1,4,'Tepic','CONVERSACION',27,1,'CONVERSA','Aprende sobre la música');
INSERT INTO `objetivos` VALUES (31,1,4,'Tepic','CONVERSACION',99,1,'CONVERSA','Aprende sobre los rituales');
INSERT INTO `objetivos` VALUES (32,1,4,'Tepic','SITIO_IMPORTANTE',5,1,'VISITA','Visita el sitio sagrado de Potrero Palmita');
INSERT INTO `objetivos` VALUES (33,1,5,'Santiago Ixcuintla','ALIMENTO',7,1,'RECOLECTA','Recolecta <i>ma’aku</i> (mango)');
INSERT INTO `objetivos` VALUES (34,1,5,'Santiago Ixcuintla','ALIMENTO',1,1,'RECOLECTA','Recolecta <i>tsikwai</i> (arrayan)');
INSERT INTO `objetivos` VALUES (35,1,5,'Santiago Ixcuintla','CONVERSACION',69,1,'CONVERSA','Aprende sobre el caldo de ardilla');
INSERT INTO `objetivos` VALUES (36,1,5,'Santiago Ixcuintla','CONVERSACION',37,1,'CONVERSA','Aprende sobre las festividades');
INSERT INTO `objetivos` VALUES (37,1,5,'Santiago Ixcuintla','CONVERSACION',100,1,'CONVERSA','Aprende sobre los centros ceremoniales');
INSERT INTO `objetivos` VALUES (38,1,5,'Santiago Ixcuintla','SITIO_IMPORTANTE',6,1,'VISITA','Visita el sitio sagrado de la Isla de Mexcaltitán');
INSERT INTO `objetivos` VALUES (39,1,5,'Santiago Ixcuintla','SITIO_IMPORTANTE',7,1,'VISITA','Visita el sitio sagrado de Río Santiago');
INSERT INTO `objetivos` VALUES (40,1,6,'San Blas','ALIMENTO',8,1,'RECOLECTA','Recolecta <i>uwakí</i> (nanchi)');
INSERT INTO `objetivos` VALUES (41,1,6,'San Blas','ALIMENTO',11,1,'RECOLECTA','Recolecta <i>ka’arú</i> (plátano)');
INSERT INTO `objetivos` VALUES (42,1,6,'San Blas','CONVERSACION',70,1,'CONVERSA','Aprende sobre el pipian de iguana');
INSERT INTO `objetivos` VALUES (43,1,6,'San Blas','CONVERSACION',71,1,'CONVERSA','Aprende sobre el plátano frito');
INSERT INTO `objetivos` VALUES (44,1,6,'San Blas','CONVERSACION',46,1,'CONVERSA','Aprende sobre las ofrendas <i>Wixárika</i> (ojos de dios)');
INSERT INTO `objetivos` VALUES (45,1,6,'San Blas','CONVERSACION',14,1,'CONVERSA','Aprende sobre la deidad <i>Tatei Haramara</i> (Nuestra madre del mar)');
INSERT INTO `objetivos` VALUES (46,2,10,'Zacatecas','CONVERSACION',-1,1,'CONVERSA','Aprende sobre la deidad <i>Nairy</i> (dios del fuego primigenio)');
INSERT INTO `objetivos` VALUES (47,2,10,'Zacatecas','CONVERSACION',-1,1,'CONVERSA','Aprende sobre la deidad <i>Takutsi Nakawé</i> (Nuestra abuela tierra)');
INSERT INTO `objetivos` VALUES (48,2,8,'Valparaíso','ALIMENTO',33,1,'RECOLECTA','Recolecta <i>ikú tataɨrawi</i> (maíz morado)');
INSERT INTO `objetivos` VALUES (49,2,8,'Valparaíso','ALIMENTO',34,1,'RECOLECTA','Recolecta <i>ikú yɨwi</i> (maíz negro)');
INSERT INTO `objetivos` VALUES (50,2,8,'Valparaíso','CONVERSACION',72,1,'CONVERSA','Aprende sobre el chicuatol');
INSERT INTO `objetivos` VALUES (51,2,8,'Valparaíso','CONVERSACION',73,1,'CONVERSA','Aprende sobre el pan de elote');
INSERT INTO `objetivos` VALUES (52,2,8,'Valparaíso','CONVERSACION',8,1,'CONVERSA','Aprende sobre el <i>xuku’uri ɨkame</i> (jicarero)');
INSERT INTO `objetivos` VALUES (53,2,8,'Valparaíso','CONVERSACION',15,1,'CONVERSA','Aprende sobre la deidad <i>Tatei Wexica Wimari</i> (Nuestra madre águila)');
INSERT INTO `objetivos` VALUES (54,2,8,'Valparaíso','CONVERSACION',28,1,'CONVERSA','Aprende  sobre los coamiles');
INSERT INTO `objetivos` VALUES (55,2,8,'Valparaíso','CONVERSACION',101,1,'CONVERSA','Aprende sobre el <i>Hikuri</i> (peyote)');
INSERT INTO `objetivos` VALUES (56,2,8,'Valparaíso','CONVERSACION',102,1,'CONVERSA','Aprende sobre la peregrinación a <i>Wirikuta</i>');
INSERT INTO `objetivos` VALUES (57,2,8,'Valparaíso','SITIO_IMPORTANTE',10,1,'VISITA','Visita el sitio sagrado de <i>Tuapurie</i>');
INSERT INTO `objetivos` VALUES (58,2,8,'Valparaíso','SITIO_IMPORTANTE',11,1,'VISITA','Visita el sitio sagrado de <i>Xurahue Muyaca</i>');
INSERT INTO `objetivos` VALUES (59,2,9,'Fresnillo','ALIMENTO',23,1,'RECOLECTA','Recolecta <i>túmati</i> (jitomate)');
INSERT INTO `objetivos` VALUES (60,2,9,'Fresnillo','ALIMENTO',55,1,'RECOLECTA','Recolecta <i>xiete</i> (miel)');
INSERT INTO `objetivos` VALUES (61,2,9,'Fresnillo','CONVERSACION',74,1,'CONVERSA','Aprende sobre el caldo de güilota');
INSERT INTO `objetivos` VALUES (62,2,9,'Fresnillo','PLANTA_MEDICINAL',8,1,'RECOLECTA','Recolecta gordolobo');
INSERT INTO `objetivos` VALUES (63,2,9,'Fresnillo','CONVERSACION',29,1,'CONVERSA','Aprende sobre la forma tradicional de caza');
INSERT INTO `objetivos` VALUES (64,2,9,'Fresnillo','CONVERSACION',52,1,'CONVERSA','Aprende sobre el animal espiritual de la lagartija');
INSERT INTO `objetivos` VALUES (65,2,9,'Fresnillo','CONVERSACION',103,1,'CONVERSA','Aprende sobre el <i>nana’iyari</i> (el costumbre <i>Wixárika</i>).');
INSERT INTO `objetivos` VALUES (66,2,9,'Fresnillo','SITIO_IMPORTANTE',12,1,'VISITA','Aprende sobre el asentamiento de plateros');
INSERT INTO `objetivos` VALUES (67,2,10,'Zacatecas','ALIMENTO',12,1,'RECOLECTA','Recolecta <i>yɨɨna</i> (tuna)');
INSERT INTO `objetivos` VALUES (68,2,10,'Zacatecas','ALIMENTO',15,1,'RECOLECTA','Recolecta <i>ye’eri</i> (camote)');
INSERT INTO `objetivos` VALUES (69,2,10,'Zacatecas','CONVERSACION',75,1,'CONVERSA','Aprende sobre las enchiladas');
INSERT INTO `objetivos` VALUES (70,2,10,'Zacatecas','PLANTA_MEDICINAL',13,1,'RECOLECTA','Recolecta sobre el <i>Hikuri</i> (peyote)');
INSERT INTO `objetivos` VALUES (71,2,10,'Zacatecas','CONVERSACION',105,1,'CONVERSA','Aprende sobre la peregrinación a <i>Wirikuta</i>.');
INSERT INTO `objetivos` VALUES (72,2,10,'Zacatecas','CONVERSACION',47,1,'CONVERSA','Aprende sobre las ofrendas <i>Wixárika</i> (máscaras).');
INSERT INTO `objetivos` VALUES (73,2,10,'Zacatecas','SITIO_IMPORTANTE',13,1,'VISITA','Visita el sitio sagrado de <i>Makuipa</i> (Cerro del Padre)');
INSERT INTO `objetivos` VALUES (74,3,13,'Santo Domingo','ALIMENTO',-1,1,'RECOLECTA','Recolecta guambulos');
INSERT INTO `objetivos` VALUES (75,3,13,'Santo Domingo','ALIMENTO',-1,1,'RECOLECTA','Recolecta habas');
INSERT INTO `objetivos` VALUES (76,3,13,'Santo Domingo','CONVERSACION',-1,1,'CONVERSA','Aprende sobre la deidad <i>Tamatzi Kauyumari</i> (Nuestro hermano mayor Venado Azul)');
INSERT INTO `objetivos` VALUES (77,3,14,'Charcas','ALIMENTO',-1,1,'RECOLECTA','Recolecta chayote');
INSERT INTO `objetivos` VALUES (78,3,15,'Real de Catorce','ALIMENTO',-1,1,'RECOLECTA','Recolecta <i>narakaxi</i> (naranja)');
INSERT INTO `objetivos` VALUES (79,3,15,'Real de Catorce','ALIMENTO',-1,1,'RECOLECTA','Recolecta <i>piní</i> (higo)');
INSERT INTO `objetivos` VALUES (80,3,15,'Real de Catorce','CONVERSACION',-1,1,'CONVERSA','Aprende sobre la deidad <i>Tututzi Maxa Kwaxi</i> (Nuestro bisabuelo Cola de venado)');
INSERT INTO `objetivos` VALUES (81,3,15,'Real de Catorce','CONVERSACION',-1,1,'CONVERSA','Aprende sobre la deidad <i>Tayau</i> (Nuestro padre el Sol)');
INSERT INTO `objetivos` VALUES (82,3,12,'Villa de Ramos','ALIMENTO',31,1,'RECOLECTA','Recolecta <i>ikú yuawime</i> (maíz azul)');
INSERT INTO `objetivos` VALUES (83,3,12,'Villa de Ramos','ALIMENTO',19,1,'RECOLECTA','Recolecta <i>mumé</i> (frijol)');
INSERT INTO `objetivos` VALUES (84,3,12,'Villa de Ramos','CONVERSACION',76,1,'CONVERSA','Aprende sobre los tacos de frijoles');
INSERT INTO `objetivos` VALUES (85,3,12,'Villa de Ramos','CONVERSACION',9,1,'CONVERSA','Aprende sobre el <i>kawiteru</i> (anciano sabio)');
INSERT INTO `objetivos` VALUES (86,3,12,'Villa de Ramos','CONVERSACION',30,1,'CONVERSA','Aprende sobre la vestimenta del pueblo <i>Wixárika</i>');
INSERT INTO `objetivos` VALUES (87,3,12,'Villa de Ramos','CONVERSACION',106,1,'CONVERSA','Aprende sobre los <i>kawitu</i> (mitos <i>Wixárika</i>)');
INSERT INTO `objetivos` VALUES (88,3,12,'Villa de Ramos','SITIO_IMPORTANTE',15,1,'VISITA','Visita el sitio sagrado de <i>Huahuatsari</i>');
INSERT INTO `objetivos` VALUES (89,3,12,'Villa de Ramos','SITIO_IMPORTANTE',16,1,'VISITA','Visita el sitio sagrado de <i>Cuhixu Uheni</i>');
INSERT INTO `objetivos` VALUES (90,3,12,'Villa de Ramos','SITIO_IMPORTANTE',17,1,'VISITA','Visita el sitio sagrado de <i>Tatei Matiniere</i>');
INSERT INTO `objetivos` VALUES (91,3,13,'Santo Domingo','CONVERSACION',77,1,'CONVERSA','Aprende sobre el caldo');
INSERT INTO `objetivos` VALUES (92,3,13,'Santo Domingo','CONVERSACION',31,1,'CONVERSA','Aprende sobre la música <i>Wixárika</i>');
INSERT INTO `objetivos` VALUES (93,3,13,'Santo Domingo','CONVERSACION',107,1,'CONVERSA','Aprende sobre el <i>Hikuri</i> (peyote)');
INSERT INTO `objetivos` VALUES (94,3,13,'Santo Domingo','CONVERSACION',108,1,'CONVERSA','Aprende sobre la peregrinación a <i>Wirikuta</i>');
INSERT INTO `objetivos` VALUES (95,3,13,'Santo Domingo','SITIO_IMPORTANTE',18,1,'VISITA','Visita el sitio sagrado de <i>Maxa Yapa</i>, un lugar sagrado');
INSERT INTO `objetivos` VALUES (96,3,14,'Charcas','ALIMENTO',17,1,'RECOLECTA','Recolecta <i>kukuríte</i> (chiles)');
INSERT INTO `objetivos` VALUES (97,3,14,'Charcas','CONVERSACION',78,1,'CONVERSA','Aprende sobre el elote asado');
INSERT INTO `objetivos` VALUES (98,3,14,'Charcas','PLANTA_MEDICINAL',13,1,'RECOLECTA','Recolecta peyote');
INSERT INTO `objetivos` VALUES (99,3,14,'Charcas','CONVERSACION',53,1,'CONVERSA','Aprende sobre los animales espirituales (águila real)');
INSERT INTO `objetivos` VALUES (100,3,14,'Charcas','CONVERSACION',38,1,'CONVERSA','Aprende sobre las festividades de la cultura <i>Wixárika</i> (sacrificio del toro)');
INSERT INTO `objetivos` VALUES (101,3,14,'Charcas','CONVERSACION',109,1,'CONVERSA','Aprende sobre el <i>Hikuri</i> (peyote)');
INSERT INTO `objetivos` VALUES (102,3,14,'Charcas','CONVERSACION',110,1,'CONVERSA','Aprende sobre la peregrinación a <i>Wirikuta</i>');
INSERT INTO `objetivos` VALUES (103,3,14,'Charcas','SITIO_IMPORTANTE',19,1,'VISITA','Visita el sitio sagrado de <i>Tuy Mayau</i>');
INSERT INTO `objetivos` VALUES (104,3,14,'Charcas','SITIO_IMPORTANTE',20,1,'VISITA','Visita el sitio sagrado de <i>Huacuri Quitenie</i>');
INSERT INTO `objetivos` VALUES (105,3,15,'Real de Catorce','CONVERSACION',80,1,'CONVERSA','Aprende sobre la sopa');
INSERT INTO `objetivos` VALUES (106,3,15,'Real de Catorce','CONVERSACION',111,1,'CONVERSA','Aprende sobre la peregrinación a <i>Wirikuta</i>');
INSERT INTO `objetivos` VALUES (107,3,15,'Real de Catorce','CONVERSACION',48,1,'CONVERSA','Aprende sobre las ofrendas <i>Wixárika</i> (tablillas)');
INSERT INTO `objetivos` VALUES (108,3,15,'Real de Catorce','SITIO_IMPORTANTE',21,1,'VISITA','Visita el sitio sagrado de <i>Wirikuta</i>');
INSERT INTO `objetivos` VALUES (109,4,18,'Pueblo Nuevo','ALIMENTO',-1,1,'RECOLECTA','Recolecta <i>haxi</i> (guaje)');
INSERT INTO `objetivos` VALUES (110,4,20,'Canatlán','ALIMENTO',-1,1,'RECOLECTA','Recolecta <i>xutsi hatsiyari</i> (semillas de calabaza)');
INSERT INTO `objetivos` VALUES (111,4,20,'Canatlán','CONVERSACION',-1,1,'CONVERSA','Aprende sobre la deidad <i>Tatei Yurienáka</i> (Nuestra madre tierra)');
INSERT INTO `objetivos` VALUES (112,4,17,'Mezquital','ALIMENTO',32,1,'RECOLECTA','Recolecta <i>ikú tuuxá</i> (maíz blanco)');
INSERT INTO `objetivos` VALUES (113,4,17,'Mezquital','ALIMENTO',2,1,'RECOLECTA','Recolecta <i>uwá</i> (caña)');
INSERT INTO `objetivos` VALUES (114,4,17,'Mezquital','CONVERSACION',81,1,'CONVERSA','Aprende sobre el pinole');
INSERT INTO `objetivos` VALUES (115,4,17,'Mezquital','CONVERSACION',10,1,'CONVERSA','Aprende sobre quién es el <i>Tupiri</i> (policía).');
INSERT INTO `objetivos` VALUES (116,4,17,'Mezquital','CONVERSACION',32,1,'CONVERSA','Aprende sobre la forma tradicional de agricultura (coamiles).');
INSERT INTO `objetivos` VALUES (117,4,17,'Mezquital','CONVERSACION',112,1,'CONVERSA','Aprende sobre la importancia del número 5 en la cultura <i>Wixárika</i>.');
INSERT INTO `objetivos` VALUES (118,4,17,'Mezquital','SITIO_IMPORTANTE',23,1,'VISITA','Visita San Antonio de Padua, una localidad donde hay comunidades <i>Wixárika</i>.');
INSERT INTO `objetivos` VALUES (119,4,18,'Pueblo Nuevo','ALIMENTO',28,1,'RECOLECTA','Recolecta <i>aɨraxa</i> (verdolaga)');
INSERT INTO `objetivos` VALUES (120,4,18,'Pueblo Nuevo','CONVERSACION',83,1,'CONVERSA','Aprende sobre las quesadillas con verdolagas');
INSERT INTO `objetivos` VALUES (121,4,18,'Pueblo Nuevo','PLANTA_MEDICINAL',12,1,'RECOLECTA','Recolecta orégano');
INSERT INTO `objetivos` VALUES (122,4,18,'Pueblo Nuevo','CONVERSACION',33,1,'CONVERSA','Aprende sobre la forma tradicional de caza (caza ritual)');
INSERT INTO `objetivos` VALUES (123,4,18,'Pueblo Nuevo','CONVERSACION',113,1,'CONVERSA','Aprende sobre los cazadores cósmicos');
INSERT INTO `objetivos` VALUES (124,4,18,'Pueblo Nuevo','SITIO_IMPORTANTE',24,1,'VISITA','Visita San Bernardino de Milpillas, una localidad donde hay asentamientos <i>Wixárika</i>');
INSERT INTO `objetivos` VALUES (125,4,19,'Durango','ALIMENTO',25,1,'RECOLECTA','Recolecta <i>na’akari</i> (nopal)');
INSERT INTO `objetivos` VALUES (126,4,19,'Durango','ALIMENTO',4,1,'RECOLECTA','Recolecta <i>muxu’uri</i> (guamúchil)');
INSERT INTO `objetivos` VALUES (127,4,19,'Durango','CONVERSACION',84,1,'CONVERSA','Aprende sobre los frijoles fritos y cocidos');
INSERT INTO `objetivos` VALUES (128,4,19,'Durango','PLANTA_MEDICINAL',7,1,'RECOLECTA','Recolecta eucalipto');
INSERT INTO `objetivos` VALUES (129,4,19,'Durango','CONVERSACION',54,1,'CONVERSA','Aprende sobre los animales espirituales (serpiente azul)');
INSERT INTO `objetivos` VALUES (130,4,19,'Durango','CONVERSACION',34,1,'CONVERSA','Aprende sobre la vestimenta del pueblo <i>Wixárika</i>');
INSERT INTO `objetivos` VALUES (131,4,19,'Durango','CONVERSACION',115,1,'CONVERSA','Aprende sobre las facetas de <i>Tamatzi Kauyumari</i> (Nuestro hermano mayor Venado Azul)');
INSERT INTO `objetivos` VALUES (132,4,19,'Durango','SITIO_IMPORTANTE',25,1,'VISITA','Visita Cinco de Mayo, una localidad donde hay comunidades <i>Wixárika</i>');
INSERT INTO `objetivos` VALUES (133,4,20,'Canatlán','ALIMENTO',26,1,'RECOLECTA','Recolecta <i>ké’uxa</i> (quelites)');
INSERT INTO `objetivos` VALUES (134,4,20,'Canatlán','CONVERSACION',85,1,'CONVERSA','Aprende sobre el atole');
INSERT INTO `objetivos` VALUES (135,4,20,'Canatlán','CONVERSACION',86,1,'CONVERSA','Aprende sobre el pan');
INSERT INTO `objetivos` VALUES (136,4,20,'Canatlán','CONVERSACION',116,1,'CONVERSA','Aprende sobre la leyenda de <i>Watakame</i>');
INSERT INTO `objetivos` VALUES (137,4,20,'Canatlán','CONVERSACION',49,1,'CONVERSA','Aprende sobre las ofrendas <i>Wixárika</i> (jícaras)');
INSERT INTO `objetivos` VALUES (138,4,20,'Canatlán','SITIO_IMPORTANTE',26,1,'VISITA','Visita el sitio sagrado <i>Hauxa Manaka</i>');
INSERT INTO `objetivos` VALUES (139,5,24,'Bolaños','ALIMENTO',-1,1,'RECOLECTA','Recolecta cebolla');
INSERT INTO `objetivos` VALUES (140,5,22,'Huejuquilla','ALIMENTO',35,1,'RECOLECTA','Recolecta <i>ikú mɨxeta</i> (maíz rojo)');
INSERT INTO `objetivos` VALUES (141,5,22,'Huejuquilla','ALIMENTO',21,1,'RECOLECTA','Recolecta <i>yekwa’ate</i> (hongos)');
INSERT INTO `objetivos` VALUES (142,5,22,'Huejuquilla','CONVERSACION',87,1,'CONVERSA','Aprende sobre las quesadillas');
INSERT INTO `objetivos` VALUES (143,5,22,'Huejuquilla','CONVERSACION',11,1,'CONVERSA','Aprende sobre el <i>Tatuwani</i> (gobernador)');
INSERT INTO `objetivos` VALUES (144,5,22,'Huejuquilla','PLANTA_MEDICINAL',4,1,'RECOLECTA','Recolecta clavo');
INSERT INTO `objetivos` VALUES (145,5,22,'Huejuquilla','CONVERSACION',35,1,'CONVERSA','Aprende sobre la música <i>Wixárika</i>');
INSERT INTO `objetivos` VALUES (146,5,22,'Huejuquilla','CONVERSACION',117,1,'CONVERSA','Aprende sobre las direcciones del universo');
INSERT INTO `objetivos` VALUES (147,5,22,'Huejuquilla','SITIO_IMPORTANTE',28,1,'VISITA','Visita Colonia <i>Hatmasie</i>, una localidad donde hay asentamientos <i>Wixárika</i>');
INSERT INTO `objetivos` VALUES (148,5,23,'Mezquitic','ALIMENTO',10,1,'RECOLECTA','Recolecta <i>ma’ara</i> (pitahaya)');
INSERT INTO `objetivos` VALUES (149,5,23,'Mezquitic','ALIMENTO',5,1,'RECOLECTA','Recolecta <i>ha’yewaxi</i> (guayaba)');
INSERT INTO `objetivos` VALUES (150,5,23,'Mezquitic','CONVERSACION',88,1,'CONVERSA','Aprende sobre las gorditas');
INSERT INTO `objetivos` VALUES (151,5,23,'Mezquitic','CONVERSACION',40,1,'CONVERSA','Aprende sobre festividades de la cultura <i>Wixárika</i>');
INSERT INTO `objetivos` VALUES (152,5,23,'Mezquitic','CONVERSACION',119,1,'CONVERSA','Aprende sobre la historia del fuego');
INSERT INTO `objetivos` VALUES (153,5,23,'Mezquitic','CONVERSACION',118,1,'CONVERSA','Aprende sobre la deidad <i>Tatewari</i> (Nuestro abuelo fuego)');
INSERT INTO `objetivos` VALUES (154,5,23,'Mezquitic','SITIO_IMPORTANTE',29,1,'VISITA','Visita el sitio sagrado <i>Te’akata</i>');
INSERT INTO `objetivos` VALUES (155,5,24,'Bolaños','ALIMENTO',13,1,'RECOLECTA','Recolecta <i>kamaika</i> (jamaica)');
INSERT INTO `objetivos` VALUES (156,5,24,'Bolaños','CONVERSACION',89,1,'CONVERSA','Aprende sobre el pozole');
INSERT INTO `objetivos` VALUES (157,5,24,'Bolaños','PLANTA_MEDICINAL',9,1,'RECOLECTA','Recolecta hierbabuena');
INSERT INTO `objetivos` VALUES (158,5,24,'Bolaños','CONVERSACION',55,1,'CONVERSA','Aprende sobre los animales mensajeros (zorro)');
INSERT INTO `objetivos` VALUES (159,5,24,'Bolaños','CONVERSACION',36,1,'CONVERSA','Aprende sobre la vestimenta del pueblo <i>Wixárika</i>');
INSERT INTO `objetivos` VALUES (160,5,24,'Bolaños','SITIO_IMPORTANTE',32,1,'VISITA','Aprende sobre Tuxpan de Bolaños, una localidad donde hay comunidades <i>Wixárika</i>');
INSERT INTO `objetivos` VALUES (161,5,25,'Chapala','ALIMENTO',14,1,'RECOLECTA','Recolecta <i>xútsi</i> (calabacita)');
INSERT INTO `objetivos` VALUES (162,5,25,'Chapala','ALIMENTO',24,1,'RECOLECTA','Recolecta <i>tsinakari</i> (limón)');
INSERT INTO `objetivos` VALUES (163,5,25,'Chapala','CONVERSACION',91,1,'CONVERSA','Aprende sobre el tejuino');
INSERT INTO `objetivos` VALUES (164,5,25,'Chapala','CONVERSACION',120,1,'CONVERSA','Aprende sobre las diosas de la lluvia');
INSERT INTO `objetivos` VALUES (165,5,25,'Chapala','CONVERSACION',50,1,'CONVERSA','Aprende sobre las ofrendas <i>Wixárika</i> (flechas)');
INSERT INTO `objetivos` VALUES (166,5,25,'Chapala','CONVERSACION',22,1,'CONVERSA','Aprende sobre la deidad <i>Tatei Xapawiyeme</i> (Madre diosa del sur)');
INSERT INTO `objetivos` VALUES (167,5,25,'Chapala','SITIO_IMPORTANTE',33,1,'VISITA','Visita el sitio sagrado <i>Xapawiyeme</i>');
INSERT INTO `objetivos` VALUES (168,6,27,'Miiki','CONVERSACION',92,1,'CONVERSA','Aprende sobre el tamal');
INSERT INTO `objetivos` VALUES (169,6,27,'Miiki','CONVERSACION',93,1,'CONVERSA','Aprende sobre el atole');
INSERT INTO `objetivos` VALUES (170,6,27,'Miiki','CONVERSACION',12,1,'CONVERSA','Aprende sobre el <i>hikuritame</i> (peyotero)');
INSERT INTO `objetivos` VALUES (171,6,27,'Miiki','PLANTA_MEDICINAL',18,1,'RECOLECTA','Recolecta <i>uxa</i>');
INSERT INTO `objetivos` VALUES (172,6,27,'Miiki','CONVERSACION',121,1,'CONVERSA','Aprende sobre el <i>mɨɨkí kwevíxa</i> (ceremonia de los muertos)');
INSERT INTO `objetivos` VALUES (173,6,28,'Hewiixi','CONVERSACION',94,1,'CONVERSA','Aprende sobre el mole');
INSERT INTO `objetivos` VALUES (174,6,28,'Hewiixi','CONVERSACION',56,1,'CONVERSA','Aprende sobre los animales mensajeros (Zopilote)');
INSERT INTO `objetivos` VALUES (175,6,28,'Hewiixi','CONVERSACION',123,1,'CONVERSA','Aprende sobre el <i>mɨɨkí kwevíxa</i> (ceremonia de los muertos)');
INSERT INTO `objetivos` VALUES (176,6,28,'Hewiixi','CONVERSACION',124,1,'CONVERSA','Aprende sobre el viaje de los mɨɨkite (muertos)');
INSERT INTO `objetivos` VALUES (177,6,29,'Kieri','CONVERSACION',95,1,'CONVERSA','Aprende sobre el tejuino');
INSERT INTO `objetivos` VALUES (178,6,29,'Kieri','PLANTA_MEDICINAL',19,1,'RECOLECTA','Recolecta tepehuaje');
INSERT INTO `objetivos` VALUES (179,6,29,'Kieri','CONVERSACION',23,1,'CONVERSA','Aprende sobre la deidad <i>Tukákame</i> (Dios de la muerte)');
INSERT INTO `objetivos` VALUES (180,6,29,'Kieri','CONVERSACION',127,1,'CONVERSA','Aprende sobre la mɨɨkí (muerte)');
INSERT INTO `quiz_pregunta` VALUES (1,'1','Alimentos','¿Cómo se escribe maíz en <i>Wixárika</i>?',25,15,1,1,'','1');
INSERT INTO `quiz_pregunta` VALUES (2,'1','Alimentos','¿Cómo se escribe maíces en <i>Wixárika</i>?',25,15,1,2,'','2');
INSERT INTO `quiz_pregunta` VALUES (3,'1','Alimentos','¿Qué imagen corresponde al <i>ikú</i>?',25,15,5,1,'','3');
INSERT INTO `quiz_pregunta` VALUES (4,'1','Alimentos','¿Qué alimento de importancia espiritual para los <i>wixaritari</i>?',25,15,1,3,'','4');
INSERT INTO `quiz_pregunta` VALUES (5,'1,10','Alimentos','¿Cómo se escribe frijol en <i>Wixárika</i>?',25,15,1,1,'','5');
INSERT INTO `quiz_pregunta` VALUES (6,'1,10','Alimentos','¿Cómo se escribe frijoles en <i>Wixárika</i>?',25,15,1,2,'','6');
INSERT INTO `quiz_pregunta` VALUES (7,'1,10','Alimentos','¿Qué imagen corresponde a los <i>múmete</i>?',25,15,5,1,'','7');
INSERT INTO `quiz_pregunta` VALUES (8,'1','Alimentos','¿Cómo se escribe agua en <i>Wixárika</i>?',25,15,1,2,'','8');
INSERT INTO `quiz_pregunta` VALUES (9,'1','Alimentos','¿Qué imagen corresponde al <i>ha’a</i>?',25,15,5,2,'','9');
INSERT INTO `quiz_pregunta` VALUES (10,'1','Alimentos','¿Cómo se escribe tortilla en <i>Wixárika</i>?',25,15,1,2,'','10');
INSERT INTO `quiz_pregunta` VALUES (11,'1','Alimentos','¿Qué imagen corresponde a la <i>pa’apa</i>?',25,15,5,2,'','11');
INSERT INTO `quiz_pregunta` VALUES (12,'1,2,4,5,7,8,13,14,15,16,17,18,20','Animales','¿Cómo se escribe jabalí en <i>Wixárika</i>?',25,15,1,1,'','12');
INSERT INTO `quiz_pregunta` VALUES (13,'1,2,3,4,5,7,8,10,11,12,13,14,15,16,17,18,19,20','Animales','¿Cómo se escribe venado en <i>Wixárika</i>?',25,15,1,1,'','13');
INSERT INTO `quiz_pregunta` VALUES (14,'1,4,7,15,16','Animales','¿Cómo se escribe puma en <i>Wixárika</i>?',25,15,1,1,'','14');
INSERT INTO `quiz_pregunta` VALUES (15,'1,2,10,11,12,19,22,23,24','Animales','¿Cómo se escribe araña en <i>Wixárika</i>?',25,15,1,1,'','15');
INSERT INTO `quiz_pregunta` VALUES (16,'1','Armas','¿Cómo se escribe palo en <i>Wixárika</i>?',25,15,1,2,'','16');
INSERT INTO `quiz_pregunta` VALUES (17,'1','Sitio sagrado','¿Cuál de los siguientes es un sitio sagrado para los <i>wixaritari</i>?',25,15,1,2,'','17');
INSERT INTO `quiz_pregunta` VALUES (18,'1','Sitio sagrado','¿Cuál de los siguientes es un sitio sagrado para los <i>wixaritari</i>i?',25,15,1,2,'','18');
INSERT INTO `quiz_pregunta` VALUES (19,'1','Autoridades','¿Cómo se escribe chamán en <i>Wixárika</i>?',25,15,1,2,'','19');
INSERT INTO `quiz_pregunta` VALUES (20,'1','Autoridades','¿Quién se encarga de curar a los <i>wixaritari</i>, dirige las ceremonias y es una autoridad en las comunidades?',25,15,1,3,'','20');
INSERT INTO `quiz_pregunta` VALUES (21,'1,7,14','Agricultura','¿Cómo se dice agricultor en <i>Wixárika</i>?',25,15,1,2,'','21');
INSERT INTO `quiz_pregunta` VALUES (22,'1,7','Agricultura','¿Dónde siembran sus cultivos los <i>wixaritari</i>?',25,15,1,3,'','22');
INSERT INTO `quiz_pregunta` VALUES (23,'1,7','Agricultura','¿Qué se siembra en los coamiles?',25,15,1,3,'','23');
INSERT INTO `quiz_pregunta` VALUES (24,'2','Alimentos','¿Cómo se escribe maíz amarillo en <i>Wixárika</i>?',25,15,1,1,'','24');
INSERT INTO `quiz_pregunta` VALUES (25,'2','Alimentos','¿Qué tipo de maíz se utiliza como alimento para los animales?',25,15,1,2,'','25');
INSERT INTO `quiz_pregunta` VALUES (26,'2,12','Alimentos','¿Cómo se escribe chile en <i>Wixárika</i>?',25,15,1,1,'','26');
INSERT INTO `quiz_pregunta` VALUES (27,'2,12','Alimentos','¿Cómo se escribe chiles en <i>Wixárika</i>?',25,15,1,2,'','27');
INSERT INTO `quiz_pregunta` VALUES (28,'2,12','Alimentos','¿Qué imagen corresponde al <i>kukúri</i>?',25,15,5,1,'','28');
INSERT INTO `quiz_pregunta` VALUES (29,'2','Alimentos','¿Cómo se escribe albóndiga de jabalí en <i>Wixárika</i>?',25,15,1,2,'','29');
INSERT INTO `quiz_pregunta` VALUES (30,'2','Alimentos','¿Cómo se escribe queso en <i>Wixárika</i>?',25,15,1,2,'','30');
INSERT INTO `quiz_pregunta` VALUES (31,'2','Alimentos','¿Cómo se escribe quesos en <i>Wixárika</i>?',25,15,1,2,'','31');
INSERT INTO `quiz_pregunta` VALUES (32,'2,4,6,10,11,12,13,17,18,20','Animales','¿Cómo se escribe serpiente en <i>Wixárika</i>?',25,15,1,1,'','32');
INSERT INTO `quiz_pregunta` VALUES (33,'2','Plantas','¿Cómo se escribe milpa en <i>Wixárika</i>?',25,15,1,2,'','33');
INSERT INTO `quiz_pregunta` VALUES (34,'2','Armas','¿Cómo se escribe machete en <i>Wixárika</i>?',25,15,1,2,'','34');
INSERT INTO `quiz_pregunta` VALUES (35,'2','Sitio sagrado','¿Cuál de los siguientes es un sitio sagrado para los <i>wixaritari</i>?',25,15,1,2,'','35');
INSERT INTO `quiz_pregunta` VALUES (36,'2,8,15','Cacería','¿Cómo se escribe cazador en <i>Wixárika</i>?',25,15,1,2,'','36');
INSERT INTO `quiz_pregunta` VALUES (37,'2,14','Cacería','¿Principalmente, para qué cazan los <i>wixaritari</i>?',25,15,1,3,'','37');
INSERT INTO `quiz_pregunta` VALUES (38,'2,8','Cultura','¿Qué es el <i>nana’iyari</i>?',25,15,1,3,'','38');
INSERT INTO `quiz_pregunta` VALUES (39,'2','Cultura','¿Cómo se vive y reproduce el <i>nana’iyari</i>?',25,15,1,3,'','39');
INSERT INTO `quiz_pregunta` VALUES (40,'3,14','Alimentos','¿Cómo se escribe caña en <i>Wixárika</i>?',25,15,1,1,'','40');
INSERT INTO `quiz_pregunta` VALUES (41,'3,14','Alimentos','¿Qué imagen corresponde a la <i>uwá</i>?',25,15,5,1,'','41');
INSERT INTO `quiz_pregunta` VALUES (42,'3,8','Alimentos','¿Cómo se escribe miel en <i>Wixárika</i>?',25,15,1,1,'','42');
INSERT INTO `quiz_pregunta` VALUES (43,'3,8','Alimentos','¿Qué imagen corresponde a la <i>xiete</i>?',25,15,5,1,'','43');
INSERT INTO `quiz_pregunta` VALUES (44,'3','Alimentos','¿Cómo se escribe piloncillo en <i>Wixárika</i>?',25,15,1,2,'','44');
INSERT INTO `quiz_pregunta` VALUES (45,'3','Alimentos','¿Cómo se escribe pescado sarandeado en <i>Wixárika</i>?',25,15,1,2,'','45');
INSERT INTO `quiz_pregunta` VALUES (46,'3,21','Animales','¿Cómo se escribe pescado en <i>Wixárika</i>?',25,15,1,1,'','46');
INSERT INTO `quiz_pregunta` VALUES (47,'3,5,6,7,9,13,14,15,17,19,21','Animales','¿Cómo se escribe coyote en <i>Wixárika</i>?',25,15,1,1,'','47');
INSERT INTO `quiz_pregunta` VALUES (48,'3,8','Animales','¿Cómo se escribe abeja en <i>Wixárika</i>?',25,15,1,1,'','48');
INSERT INTO `quiz_pregunta` VALUES (49,'3,8,9,13,16,20,21','Animales','¿Cómo se escribe zorro en <i>Wixárika</i> ?',25,15,1,1,'','49');
INSERT INTO `quiz_pregunta` VALUES (50,'3','Armas','¿Cómo se escribe hacha en <i>Wixárika</i> ?',25,15,1,2,'','50');
INSERT INTO `quiz_pregunta` VALUES (51,'3','Sitio sagrado','¿Cuál de los siguientes es un sitio sagrado para los <i>wixaritari</i>?',25,15,1,2,'','51');
INSERT INTO `quiz_pregunta` VALUES (52,'3,10,16,20','Vestimenta','¿Cómo se escribe artesano en <i>Wixárika</i> ?',25,15,1,2,'','52');
INSERT INTO `quiz_pregunta` VALUES (53,'3,16,20','Vestimenta','¿Cómo se escribe trajes tradicionales en <i>Wixárika</i> ?',25,15,1,3,'','53');
INSERT INTO `quiz_pregunta` VALUES (54,'3','Vestimenta','¿Cómo es un kemari (traje tradicional <i>Wixárika</i>)?',25,15,1,3,'','54');
INSERT INTO `quiz_pregunta` VALUES (55,'3,16','Deidad','¿Quién es <i>Tamatzi Kauyumari</i>?',25,15,1,2,'','55');
INSERT INTO `quiz_pregunta` VALUES (56,'3,16','Deidad','¿En qué se convierte <i>Tamatzi Kauyumari</i> (Nuestro hermano mayor Venado Azul)?',25,15,1,3,'','56');
INSERT INTO `quiz_pregunta` VALUES (57,'3','Deidad','¿A dónde guio <i>Tamatzi Kauyumari</i> (Nuestro hermano mayor Venado Azul) a los wixaritari?',25,15,1,3,'','57');
INSERT INTO `quiz_pregunta` VALUES (58,'4','Alimentos','¿Cómo se escribe jícama en <i>Wixárika</i>?',25,15,1,1,'','58');
INSERT INTO `quiz_pregunta` VALUES (59,'4','Alimentos','¿Qué imagen corresponde a la <i>xa’ata</i>?',25,15,5,1,'','59');
INSERT INTO `quiz_pregunta` VALUES (60,'4','Alimentos','¿Cómo se escribe ciruela en <i>Wixárika</i>?',25,15,1,1,'','60');
INSERT INTO `quiz_pregunta` VALUES (61,'4','Alimentos','¿Qué imagen corresponde a la <i>kwarɨpa</i>?',25,15,5,1,'','61');
INSERT INTO `quiz_pregunta` VALUES (62,'4','Alimentos','¿Cómo se escribe mole de venado en <i>Wixárika</i>?',25,15,1,2,'','62');
INSERT INTO `quiz_pregunta` VALUES (63,'4','Plantas','¿Cómo se escribe manzanilla en <i>Wixárika</i>?',25,15,1,2,'','63');
INSERT INTO `quiz_pregunta` VALUES (64,'4','Sitio sagrado','¿Cuál de los siguientes es un sitio sagrado para los <i>wixaritari</i>?',25,15,1,2,'','64');
INSERT INTO `quiz_pregunta` VALUES (65,'4,11,18','Música','¿Cómo se escribe músico en <i>Wixárika</i>?',25,15,1,2,'','65');
INSERT INTO `quiz_pregunta` VALUES (66,'4','Música','¿Cómo se escribe canción en <i>Wixárika</i>?',25,15,1,3,'','66');
INSERT INTO `quiz_pregunta` VALUES (67,'4','Música','¿Cuáles son los géneros de la música tradicional <i>Wixárika</i>?',25,15,1,3,'','67');
INSERT INTO `quiz_pregunta` VALUES (68,'4','Cultura','¿Qué recrean los rituales <i>Wixárika</i>?',25,15,1,3,'','68');
INSERT INTO `quiz_pregunta` VALUES (69,'5','Alimentos','¿Cómo se escribe mango en <i>Wixárika</i>?',25,15,1,1,'','69');
INSERT INTO `quiz_pregunta` VALUES (70,'5','Alimentos','¿Qué imagen corresponde al <i>ma’aku</i>?',25,15,5,1,'','70');
INSERT INTO `quiz_pregunta` VALUES (71,'5','Alimentos','¿Cómo se escribe arrayan en <i>Wixárika</i>?',25,15,1,1,'','71');
INSERT INTO `quiz_pregunta` VALUES (72,'5','Alimentos','¿Qué imagen corresponde al <i>tsikwai</i>?',25,15,5,1,'','72');
INSERT INTO `quiz_pregunta` VALUES (73,'5','Alimentos','¿Cómo se escribe caldo de ardilla en <i>Wixárika</i>?',25,15,1,2,NULL,'73');
INSERT INTO `quiz_pregunta` VALUES (74,'5,14','Animales','¿Cómo se escribe ardilla en <i>Wixárika</i>?',25,15,1,1,'','74');
INSERT INTO `quiz_pregunta` VALUES (75,'5,18,19,20,21','Animales','¿Cómo se escribe alacrán en <i>Wixárika</i>?',25,15,1,1,'','75');
INSERT INTO `quiz_pregunta` VALUES (76,'5','Sitio sagrado','¿Cuál de los siguientes es un sitio sagrado para los <i>wixaritari</i>?',25,15,1,2,'','76');
INSERT INTO `quiz_pregunta` VALUES (77,'5','Sitio sagrado','¿Cuál de los siguientes es un sitio sagrado para los <i>wixaritari</i>?',25,15,1,2,'','77');
INSERT INTO `quiz_pregunta` VALUES (78,'5','Animales especiales','¿Cómo se escribe lechuza en <i>Wixárika</i>?',25,15,1,2,'','78');
INSERT INTO `quiz_pregunta` VALUES (79,'5','Animales especiales','¿Cuál de los siguientes es un animal mensajero?',25,15,1,3,'','79');
INSERT INTO `quiz_pregunta` VALUES (80,'5,20','Animales especiales','¿Dónde aparecen los animales mensajeros?',25,15,1,3,NULL,'80');
INSERT INTO `quiz_pregunta` VALUES (81,'5','Cultura','¿Cómo se escribe fiesta ceremonial en <i>Wixárika</i>?',25,15,1,3,NULL,'81');
INSERT INTO `quiz_pregunta` VALUES (82,'5','Cultura','¿Qué celebran las principales <i>neixa</i> (fiestas ceremoniales)?',25,15,1,3,NULL,'82');
INSERT INTO `quiz_pregunta` VALUES (83,'5','Cultura','¿Dónde se realizan las <i>neixa</i> (fiestas ceremoniales)?',25,15,1,3,NULL,'83');
INSERT INTO `quiz_pregunta` VALUES (84,'5','Cultura','¿Cómo se escribe centro ceremonial en <i>Wixárika</i>?',25,15,1,3,NULL,'84');
INSERT INTO `quiz_pregunta` VALUES (85,'5','Cultura','¿Qué es el <i>tukipa</i> (centro ceremonial)?',25,15,1,3,NULL,'85');
INSERT INTO `quiz_pregunta` VALUES (86,'5','Cultura','¿Qué templos integran el <i>tukipa</i> (centro ceremonial)?',25,15,1,3,NULL,'86');
INSERT INTO `quiz_pregunta` VALUES (87,'6','Alimentos','¿Cómo se escribe nanchi en <i>Wixárika</i>?',25,15,1,1,NULL,'87');
INSERT INTO `quiz_pregunta` VALUES (88,'6','Alimentos','¿Qué imagen corresponde al <i>uwakí</i>?',25,15,5,1,NULL,'88');
INSERT INTO `quiz_pregunta` VALUES (89,'6','Alimentos','¿Cómo se escribe plátano en <i>Wixárika</i>?',25,15,1,1,NULL,'89');
INSERT INTO `quiz_pregunta` VALUES (90,'6','Alimentos','¿Qué imagen corresponde al <i>ka’arú</i>?',25,15,5,1,NULL,'90');
INSERT INTO `quiz_pregunta` VALUES (91,'6','Alimentos','¿Cómo se escribe pipián de iguana en <i>Wixárika</i>?',25,15,1,2,NULL,'91');
INSERT INTO `quiz_pregunta` VALUES (92,'6','Alimentos','¿Cómo se escribe plátano frito en <i>Wixárika</i>?',25,15,1,2,NULL,'92');
INSERT INTO `quiz_pregunta` VALUES (93,'6,19','Animales','¿Cómo se escribe iguana en <i>Wixárika</i>?',25,15,1,1,NULL,'93');
INSERT INTO `quiz_pregunta` VALUES (94,'6','Animales','¿Cómo se escribe cocodrilo en <i>Wixárika</i>?',25,15,1,1,NULL,'94');
INSERT INTO `quiz_pregunta` VALUES (95,'6','Ofrendas','¿Qué es un <i>tsikɨri</i> (ojos de dios)?',25,15,1,2,NULL,'95');
INSERT INTO `quiz_pregunta` VALUES (96,'6','Ofrendas','¿Qué simboliza un <i>tsikɨri</i> (ojos de dios)?',25,15,1,3,NULL,'96');
INSERT INTO `quiz_pregunta` VALUES (97,'6','Sitio sagrado','¿Cuál de los siguientes es un sitio sagrado para los <i>wixaritari</i>?',25,15,1,2,NULL,'97');
INSERT INTO `quiz_pregunta` VALUES (98,'6','Sitio sagrado','¿Dónde está el sitio sagrado <i>Haramara</i>?',25,15,1,2,NULL,'98');
INSERT INTO `quiz_pregunta` VALUES (99,'6','Deidad','¿Quién es <i>Tatei Haramara</i>?',25,15,1,2,NULL,'99');
INSERT INTO `quiz_pregunta` VALUES (100,'6','Deidad','¿A qué da origen <i>Tatei Haramara</i> al convertirse en vapor?',25,15,1,3,NULL,'100');
INSERT INTO `quiz_pregunta` VALUES (101,'6,9,13','Ofrendas','¿Cómo se escribe ofrenda en <i>Wixárika</i>?',25,15,1,2,NULL,'101');
INSERT INTO `quiz_pregunta` VALUES (102,'6,9,13','Ofrendas','¿Qué es una ofrenda <i>Wixárika</i>?',25,15,1,3,NULL,'102');
INSERT INTO `quiz_pregunta` VALUES (103,'6,9,13','Ofrendas','¿Para qué sirven las ofrendas?',25,15,1,3,NULL,'103');
INSERT INTO `quiz_pregunta` VALUES (104,'7','Alimentos','¿Cómo se escribe maíz morado en <i>Wixárika</i>?',25,15,1,1,NULL,'104');
INSERT INTO `quiz_pregunta` VALUES (105,'7','Alimentos','¿Qué tipo de maíz se utiliza para preparar tortillas y dar color a los alimentos?',25,15,1,2,NULL,'105');
INSERT INTO `quiz_pregunta` VALUES (106,'7','Alimentos','¿Cómo se escribe maíz negro en <i>Wixárika</i>?',25,15,1,1,NULL,'106');
INSERT INTO `quiz_pregunta` VALUES (107,'7','Alimentos','¿Cómo se escribe chicuatol en <i>Wixárika</i>?',25,15,1,2,NULL,'107');
INSERT INTO `quiz_pregunta` VALUES (108,'7','Alimentos','¿Cómo se escribe pan de elote en <i>Wixárika</i>?',25,15,1,2,NULL,'108');
INSERT INTO `quiz_pregunta` VALUES (109,'7','Armas','¿Cómo se escribe azadón en <i>Wixárika</i>?',25,15,1,2,NULL,'109');
INSERT INTO `quiz_pregunta` VALUES (110,'7','Sitio sagrado','¿Cuál de los siguientes es un sitio sagrado para los <i>wixaritari</i>?',25,15,1,2,NULL,'110');
INSERT INTO `quiz_pregunta` VALUES (111,'7','Sitio sagrado','¿Cuál de los siguientes es un sitio sagrado para los <i>wixaritari</i>?',25,15,1,2,NULL,'111');
INSERT INTO `quiz_pregunta` VALUES (112,'7','Deidad','¿Quién es <i>Tatei Wexica Wimari</i>?',25,15,1,2,NULL,'112');
INSERT INTO `quiz_pregunta` VALUES (113,'7','Autoridades','¿Cómo se escribe jicarero en <i>Wixárika</i>?',25,15,1,2,NULL,'113');
INSERT INTO `quiz_pregunta` VALUES (114,'7','Autoridades','¿Quién se encarga de cuidar el <i>tukipa</i> (centro ceremonial)?',25,15,1,3,NULL,'114');
INSERT INTO `quiz_pregunta` VALUES (115,'7,9,11,12','Plantas','¿Cómo se escribe peyote en <i>Wixárika</i>?',25,15,1,2,NULL,'115');
INSERT INTO `quiz_pregunta` VALUES (116,'7','Plantas','¿Qué dio origen al <i>peyote?',25,15,1,3,NULL,'116');
INSERT INTO `quiz_pregunta` VALUES (117,'7','Cultura','¿Qué se recrea durante las peregrinaciones?',25,15,1,3,NULL,'117');
INSERT INTO `quiz_pregunta` VALUES (118,'7','Cultura','¿Qué lugares se visitan durante las peregrinaciones?',25,15,1,3,NULL,'118');
INSERT INTO `quiz_pregunta` VALUES (119,'7','Cultura','¿A dónde se realiza la peregrinación más importante?',25,15,1,3,NULL,'119');
INSERT INTO `quiz_pregunta` VALUES (120,'8','Alimentos','¿Cómo se escribe jitomate en <i>Wixárika</i>?',25,15,1,1,NULL,'120');
INSERT INTO `quiz_pregunta` VALUES (121,'8','Alimentos','¿Qué imagen corresponde al <i>túmati</i>?',25,15,5,1,NULL,'121');
INSERT INTO `quiz_pregunta` VALUES (122,'8','Alimentos','¿Cómo se escribe caldo de güilota en <i>Wixárika</i>?',25,15,1,2,NULL,'122');
INSERT INTO `quiz_pregunta` VALUES (123,'8,20','Animales','¿Cómo se escribe güilota en <i>Wixárika</i>?',25,15,1,1,NULL,'123');
INSERT INTO `quiz_pregunta` VALUES (124,'8','Plantas','¿Cómo se escribe gordolobo en <i>Wixárika</i>?',25,15,1,2,NULL,'124');
INSERT INTO `quiz_pregunta` VALUES (125,'8','Sitio sagrado','¿Cuál de los siguientes es un sitio sagrado para los <i>wixaritari</i>?',25,15,1,2,NULL,'125');
INSERT INTO `quiz_pregunta` VALUES (126,'8','Animales especiales','¿Cómo se escribe lagartija en <i>Wixárika</i>?',25,15,1,2,NULL,'126');
INSERT INTO `quiz_pregunta` VALUES (127,'8','Animales especiales','¿Cuál de los siguientes es un animal espiritual?',25,15,1,3,NULL,'127');
INSERT INTO `quiz_pregunta` VALUES (128,'8','Cacería','¿Qué animal se caza durante la peregrinación a <i>Wirikuta</i>?',25,15,1,3,NULL,'128');
INSERT INTO `quiz_pregunta` VALUES (129,'8','Cultura','¿Qué significa <i>’iyari</i>?',25,15,1,2,NULL,'129');
INSERT INTO `quiz_pregunta` VALUES (130,'8','Cultura','¿Qué elementos y características constituyen el <i>nana’iyari</i>?',25,15,1,3,NULL,'130');
INSERT INTO `quiz_pregunta` VALUES (131,'8','Cultura','¿Qué deidad tuvo que hacer el <i>nana’iyari</i>?',25,15,1,3,NULL,'131');
INSERT INTO `quiz_pregunta` VALUES (132,'9','Alimentos','¿Cómo se escribe tuna en <i>Wixárika</i>?',25,15,1,1,NULL,'132');
INSERT INTO `quiz_pregunta` VALUES (133,'9','Alimentos','¿Qué imagen corresponde a <i>yɨɨna</i>?',25,15,5,1,NULL,'133');
INSERT INTO `quiz_pregunta` VALUES (134,'9','Alimentos','¿Cómo se escribe camote en <i>Wixárika</i>?',25,15,1,1,NULL,'134');
INSERT INTO `quiz_pregunta` VALUES (135,'9','Alimentos','¿Qué imagen corresponde a <i>ye’eri</i>?',25,15,5,1,NULL,'135');
INSERT INTO `quiz_pregunta` VALUES (136,'9','Alimentos','¿Cómo se escribe enchilada de pollo en <i>Wixárika</i>?',25,15,1,2,NULL,'136');
INSERT INTO `quiz_pregunta` VALUES (137,'9,16','Animales','¿Cómo se escribe pollo en <i>Wixárika</i>?',25,15,1,1,NULL,'137');
INSERT INTO `quiz_pregunta` VALUES (138,'9','Ofrendas','¿Qué es una <i>tsikwaki nierikaya</i> (máscara)?',25,15,1,2,NULL,'138');
INSERT INTO `quiz_pregunta` VALUES (139,'9','Ofrendas','¿Qué representa la <i>tsikwaki nierikaya</i> (máscara)?',25,15,1,3,NULL,'139');
INSERT INTO `quiz_pregunta` VALUES (140,'9','Sitio sagrado','¿Cuál de los siguientes es un sitio sagrado para los <i>wixaritari</i>?',25,15,1,2,NULL,'140');
INSERT INTO `quiz_pregunta` VALUES (141,'9','Deidad','¿Quién es <i>Naɨrɨ</i>?',25,15,1,2,NULL,'141');
INSERT INTO `quiz_pregunta` VALUES (142,'9','Deidad','¿Quién es <i>Takutsi Nakawé</i>?',25,15,1,2,NULL,'142');
INSERT INTO `quiz_pregunta` VALUES (143,'9','Cultura','¿Qué es la <i>Namawita neixa</i>?',25,15,1,3,NULL,'143');
INSERT INTO `quiz_pregunta` VALUES (144,'9','Ofrendas','¿Qué ofrendas elaboran los <i>wixaritari</i>?',25,15,1,3,NULL,'144');
INSERT INTO `quiz_pregunta` VALUES (145,'9','Plantas','¿Qué es el <i>peyote</i>?',25,15,1,2,NULL,'145');
INSERT INTO `quiz_pregunta` VALUES (146,'9','Plantas','¿Dónde crece el <i>peyote</i>?',25,15,1,2,NULL,'146');
INSERT INTO `quiz_pregunta` VALUES (147,'9','Plantas','¿Cuánto tarda en crecer el <i>peyote</i>?',25,15,1,2,NULL,'147');
INSERT INTO `quiz_pregunta` VALUES (148,'9','Plantas','¿Quiénes pueden recolectar el <i>peyote</i>?',25,15,1,2,NULL,'148');
INSERT INTO `quiz_pregunta` VALUES (149,'9','Cultura','¿Qué se hace durante la peregrinación a <i>Wirikuta</i>?',25,15,1,3,NULL,'149');
INSERT INTO `quiz_pregunta` VALUES (150,'9','Cultura','¿Quiénes transmiten conocimiento por medio de visiones durante la peregrinación a <i>Wirikuta</i>?',25,15,1,3,NULL,'150');
INSERT INTO `quiz_pregunta` VALUES (151,'9','Cultura','¿Para qué se transporta agua del mar hacia el desierto y viceversa?',25,15,1,3,NULL,'151');
INSERT INTO `quiz_pregunta` VALUES (152,'10','Alimentos','¿Cómo se escribe maíz azul en <i>Wixárika</i>?',25,15,1,1,NULL,'152');
INSERT INTO `quiz_pregunta` VALUES (153,'10','Alimentos','¿Qué tipo de maíz se utiliza para preparar tortillas, atole y pinole?',25,15,1,2,NULL,'153');
INSERT INTO `quiz_pregunta` VALUES (154,'10','Alimentos','¿Cómo se escribe taco de frijoles en <i>Wixárika</i>?',25,15,1,2,NULL,'154');
INSERT INTO `quiz_pregunta` VALUES (155,'10,17','Animales','¿Cómo se escribe cerdo en <i>Wixárika</i>?',25,15,1,2,NULL,'155');
INSERT INTO `quiz_pregunta` VALUES (156,'10','Armas','¿Cómo se escribe cuchillo en <i>Wixárika</i>?',25,15,1,2,NULL,'156');
INSERT INTO `quiz_pregunta` VALUES (157,'10','Sitio sagrado','¿Cuál de los siguientes es un sitio sagrado para los <i>wixaritari</i>?',25,15,1,2,NULL,'157');
INSERT INTO `quiz_pregunta` VALUES (158,'10','Sitio sagrado','¿Cuál de los siguientes es un sitio sagrado para los <i>wixaritari</i>?',25,15,1,2,NULL,'158');
INSERT INTO `quiz_pregunta` VALUES (159,'10','Sitio sagrado','¿Cuál de los siguientes es un sitio sagrado para los <i>wixaritari</i>?',25,15,1,2,NULL,'159');
INSERT INTO `quiz_pregunta` VALUES (160,'10','Autoridades','¿Cómo se escribe anciano sabio en <i>Wixárika</i>?',25,15,1,2,NULL,'160');
INSERT INTO `quiz_pregunta` VALUES (161,'10','Autoridades','¿Quién es la autoridad máxima en las comunidades?',25,15,1,3,NULL,'161');
INSERT INTO `quiz_pregunta` VALUES (162,'10','Autoridades','¿Quién sueña a los nuevos funcionarios del gobierno comunal?',25,15,1,3,NULL,'162');
INSERT INTO `quiz_pregunta` VALUES (163,'10','Vestimenta','¿Qué artesanías de chaquira elaboran los <i>wixaritari</i>?',25,15,1,3,NULL,'163');
INSERT INTO `quiz_pregunta` VALUES (164,'10','Vestimenta','¿Qué artesanías de estambre elaboran los <i>wixaritari</i>?',25,15,1,3,NULL,'164');
INSERT INTO `quiz_pregunta` VALUES (165,'10','Vestimenta','¿Cómo se escribe collares en <i>Wixárika</i>?',25,15,1,2,NULL,'165');
INSERT INTO `quiz_pregunta` VALUES (166,'10','Vestimenta','¿Cómo se escribe pulseras en <i>Wixárika</i>?',25,15,1,2,NULL,'166');
INSERT INTO `quiz_pregunta` VALUES (167,'10','Vestimenta','¿Cómo se escribe aretes en <i>Wixárika</i>?',25,15,1,2,NULL,'167');
INSERT INTO `quiz_pregunta` VALUES (168,'10','Vestimenta','¿Cómo se escribe morrales en <i>Wixárika</i>?',25,15,1,2,NULL,'168');
INSERT INTO `quiz_pregunta` VALUES (169,'10','Cultura','¿De qué colores hay maíz?',25,15,1,3,NULL,'169');
INSERT INTO `quiz_pregunta` VALUES (170,'10','Cultura','¿Quién le dio el maíz a los <i>wixaritari</i>?',25,15,1,3,NULL,'170');
INSERT INTO `quiz_pregunta` VALUES (171,'11','Alimentos','¿Cómo se escribe gualumbos en <i>Wixárika</i>?',25,15,1,1,NULL,'171');
INSERT INTO `quiz_pregunta` VALUES (172,'11','Alimentos','¿Qué imagen corresponde a <i>tsíweri</i>?',25,15,5,1,NULL,'172');
INSERT INTO `quiz_pregunta` VALUES (173,'11','Alimentos','¿Cómo se escribe habas en <i>Wixárika</i>?',25,15,1,1,NULL,'173');
INSERT INTO `quiz_pregunta` VALUES (174,'11','Alimentos','¿Qué imagen corresponde a <i>kweetsi</i>?',25,15,5,1,NULL,'174');
INSERT INTO `quiz_pregunta` VALUES (175,'11','Alimentos','¿Cómo se escribe caldo de conejo en <i>Wixárika</i>?',25,15,1,2,NULL,'175');
INSERT INTO `quiz_pregunta` VALUES (176,'11,15','Animales','¿Cómo se escribe conejo en <i>Wixárika</i>?',25,15,1,2,NULL,'176');
INSERT INTO `quiz_pregunta` VALUES (177,'11','Sitio sagrado','¿Cuál de los siguientes es un sitio sagrado para los <i>wixaritari</i>?',25,15,1,2,NULL,'177');
INSERT INTO `quiz_pregunta` VALUES (178,'11','Deidad','¿Quién es <i>Tatei Kutsaraɨpa</i>?',25,15,1,2,NULL,'178');
INSERT INTO `quiz_pregunta` VALUES (179,'11','Cultura','¿En qué lugar mítico se reunieron los antepasados?',25,15,1,3,NULL,'179');
INSERT INTO `quiz_pregunta` VALUES (180,'11','Música','¿Qué instrumentos se utilizan para interpretar la música tradicional <i>Wixárika</i>?',25,15,1,3,NULL,'180');
INSERT INTO `quiz_pregunta` VALUES (181,'11','Música','¿Cuál es el conjunto <i>Wixárika</i> más famoso?',25,15,1,3,NULL,'181');
INSERT INTO `quiz_pregunta` VALUES (182,'11','Plantas','¿Para qué se utiliza el <i>peyote</i>?',25,15,1,3,NULL,'182');
INSERT INTO `quiz_pregunta` VALUES (183,'11','Plantas','¿Cuál es la medicina más potente para ahuyentar el mal o las influencias sobrenaturales?',25,15,1,3,NULL,'183');
INSERT INTO `quiz_pregunta` VALUES (184,'11','Cultura','¿Qué se necesita para ir a <i>Wirikuta</i>?',25,15,1,3,NULL,'184');
INSERT INTO `quiz_pregunta` VALUES (185,'11','Cultura','¿Qué se debe hacer durante la peregrinación a <i>Wirikuta</i>?',25,15,1,3,NULL,'185');
INSERT INTO `quiz_pregunta` VALUES (186,'12','Alimentos','¿Cómo se escribe pochote en <i>Wixárika</i>?',25,15,1,1,NULL,'186');
INSERT INTO `quiz_pregunta` VALUES (187,'12','Alimentos','¿Qué imagen corresponde a <i>karimutsi</i>?',25,15,5,1,NULL,'187');
INSERT INTO `quiz_pregunta` VALUES (188,'12','Alimentos','¿Cómo se escribe elote asado en <i>Wixárika</i>?',25,15,1,2,NULL,'188');
INSERT INTO `quiz_pregunta` VALUES (189,'12','Animales','¿Cómo se escribe vaca en <i>Wixárika</i>?',25,15,1,1,NULL,'189');
INSERT INTO `quiz_pregunta` VALUES (190,'12','Sitio sagrado','¿Cuál de los siguientes es un sitio sagrado para los <i>wixaritari</i>?',25,15,1,2,NULL,'190');
INSERT INTO `quiz_pregunta` VALUES (191,'12','Sitio sagrado','¿Cuál de los siguientes es un sitio sagrado para los <i>wixaritari</i>?',25,15,1,2,NULL,'191');
INSERT INTO `quiz_pregunta` VALUES (192,'12','Animales especiales','¿Cómo se escribe águila real en <i>Wixárika</i>?',25,15,1,2,NULL,'192');
INSERT INTO `quiz_pregunta` VALUES (193,'12','Animales especiales','¿Cuál de los siguientes es un animal espiritual?',25,15,1,3,NULL,'193');
INSERT INTO `quiz_pregunta` VALUES (194,'12','Animales especiales','¿Qué utilizan para decorar flechas y sombreros?',25,15,1,3,NULL,'194');
INSERT INTO `quiz_pregunta` VALUES (195,'12','Animales especiales','¿Qué bordan en sus trajes los <i>wixaritari</i>?',25,15,1,3,NULL,'195');
INSERT INTO `quiz_pregunta` VALUES (196,'12','Cultura','¿Quién establece contacto con las deidades por medio del canto chamánico?',25,15,1,3,NULL,'196');
INSERT INTO `quiz_pregunta` VALUES (197,'12','Cultura','¿Qué le pide el <i>mara’kame</i> a las deidades durante las fiestas ceremoniales?',25,15,1,3,NULL,'197');
INSERT INTO `quiz_pregunta` VALUES (198,'12','Plantas','¿Con qué planta se relacionan las festividades, peregrinaciones y ofrendas?',25,15,1,3,NULL,'198');
INSERT INTO `quiz_pregunta` VALUES (199,'12','Plantas','¿Qué se recolecta en <i>Wirikuta</i>?',25,15,1,3,NULL,'199');
INSERT INTO `quiz_pregunta` VALUES (200,'12','Plantas','¿Quiénes son los guardianes del <i>peyote</i>?',25,15,1,3,NULL,'200');
INSERT INTO `quiz_pregunta` VALUES (201,'12','Cultura','¿Cuál es la celebración <i>Wixárika</i> más importante?',25,15,1,3,NULL,'201');
INSERT INTO `quiz_pregunta` VALUES (202,'12','Cultura','¿Qué significa <i>matewáme</i>?',25,15,1,3,NULL,'202');
INSERT INTO `quiz_pregunta` VALUES (203,'12','Cultura','¿Quiénes son nombrados <i>matewáme</i> (el que no sabe y va a saber)?',25,15,1,3,NULL,'203');
INSERT INTO `quiz_pregunta` VALUES (204,'12','Deidad','¿Quién es <i>Tatéi Matiniéri</i>?',25,15,1,3,NULL,'204');
INSERT INTO `quiz_pregunta` VALUES (205,'13','Alimentos','¿Cómo se escribe naranja en <i>Wixárika</i>?',25,15,1,1,NULL,'205');
INSERT INTO `quiz_pregunta` VALUES (206,'13','Alimentos','¿Qué imagen corresponde a <i>narakaxi</i>?',25,15,5,1,NULL,'206');
INSERT INTO `quiz_pregunta` VALUES (207,'13','Alimentos','¿Cómo se escribe higo en <i>Wixárika</i>?',25,15,1,1,NULL,'207');
INSERT INTO `quiz_pregunta` VALUES (208,'13','Alimentos','¿Qué imagen corresponde a <i>piní</i>?',25,15,5,1,NULL,'208');
INSERT INTO `quiz_pregunta` VALUES (209,'13','Alimentos','¿Cómo se escribe jugo de naranja en <i>Wixárika</i>?',25,15,1,2,NULL,'209');
INSERT INTO `quiz_pregunta` VALUES (210,'13','Alimentos','¿Cómo se escribe sopa en <i>Wixárika</i>?',25,15,1,2,NULL,'210');
INSERT INTO `quiz_pregunta` VALUES (211,'13','Ofrendas','¿Qué es una <i>nierika</i> (tablilla de estambre)?',25,15,1,2,NULL,'211');
INSERT INTO `quiz_pregunta` VALUES (212,'13','Ofrendas','¿Qué representa la <i>nierika</i> (tablilla de estambre)?',25,15,1,3,NULL,'212');
INSERT INTO `quiz_pregunta` VALUES (213,'13','Ofrendas','¿Para qué se utiliza la <i>nierika</i> (tablilla de estambre)?',25,15,1,3,NULL,'213');
INSERT INTO `quiz_pregunta` VALUES (214,'13','Sitio sagrado','¿Cuál de los siguientes es un sitio sagrado para los <i>wixaritari</i>?',25,15,1,2,NULL,'214');
INSERT INTO `quiz_pregunta` VALUES (215,'13','Deidad','¿Quién es <i>Tututzi Maxa Kwaxi</i>?',25,15,1,2,NULL,'215');
INSERT INTO `quiz_pregunta` VALUES (216,'13','Deidad','¿Qué representa <i>Tututzi Maxa Kwaxi</i>?',25,15,1,3,NULL,'216');
INSERT INTO `quiz_pregunta` VALUES (217,'13','Deidad','¿Quién es <i>Tayau</i>?',25,15,1,2,NULL,'217');
INSERT INTO `quiz_pregunta` VALUES (218,'13','Deidad','¿En dónde nace <i>Tayau</i>?',25,15,1,3,NULL,'218');
INSERT INTO `quiz_pregunta` VALUES (219,'13','Deidad','¿En dónde muere <i>Tayau</i>?',25,15,1,3,NULL,'219');
INSERT INTO `quiz_pregunta` VALUES (220,'13','Ofrendas','¿Con qué se plasman los agradecimientos o plegarias en las ofrendas?',25,15,1,3,NULL,'220');
INSERT INTO `quiz_pregunta` VALUES (221,'13','Cultura','¿Dónde finaliza la peregrinación a <i>Wirikuta</i>?',25,15,1,3,NULL,'221');
INSERT INTO `quiz_pregunta` VALUES (222,'13','Cultura','¿Dónde dejan ofrendas los peregrinos en <i>Reunari</i> (Cerro Quemado)?',25,15,1,3,NULL,'222');
INSERT INTO `quiz_pregunta` VALUES (223,'13','Cultura','¿Qué se hace después de llegar a <i>Wirikuta</i>?',25,15,1,3,NULL,'223');
INSERT INTO `quiz_pregunta` VALUES (224,'13','Cultura','¿A dónde van los peregrinos después de <i>Wirikuta</i>?',25,15,1,3,NULL,'224');
INSERT INTO `quiz_pregunta` VALUES (225,'14','Alimentos','¿Cómo se escribe maíz blanco en <i>Wixárika</i>?',25,15,1,1,NULL,'225');
INSERT INTO `quiz_pregunta` VALUES (226,'14','Alimentos','¿Qué tipo de maíz se utiliza en las ceremonias religiosas?',25,15,1,2,NULL,'226');
INSERT INTO `quiz_pregunta` VALUES (227,'14','Alimentos','¿Cómo se escribe pinole en <i>Wixárika</i>?',25,15,1,2,NULL,'227');
INSERT INTO `quiz_pregunta` VALUES (228,'14','Alimentos','¿Cómo se escribe jugo de caña en <i>Wixárika</i>?',25,15,1,2,NULL,'228');
INSERT INTO `quiz_pregunta` VALUES (229,'14','Sitio sagrado','¿Cuál de los siguientes es un sitio sagrado para los <i>wixaritari</i>?',25,15,1,2,NULL,'229');
INSERT INTO `quiz_pregunta` VALUES (230,'14','Autoridades','¿Cómo se escribe policía en <i>Wixárika</i>?',25,15,1,2,NULL,'230');
INSERT INTO `quiz_pregunta` VALUES (231,'14','Autoridades','¿Quiénes son los mensajeros de las comunidades?',25,15,1,3,NULL,'231');
INSERT INTO `quiz_pregunta` VALUES (232,'14','Agricultura','¿Por qué se siembran flores de cempasúchil en el <i>coamil</i>?',25,15,1,3,NULL,'232');
INSERT INTO `quiz_pregunta` VALUES (233,'14','Agricultura','¿Para qué se siembran frijoles en el <i>coamil</i>?',25,15,1,3,NULL,'233');
INSERT INTO `quiz_pregunta` VALUES (234,'14','Agricultura','¿Para qué se siembran calabacitas en el <i>coamil</i>?',25,15,1,3,NULL,'234');
INSERT INTO `quiz_pregunta` VALUES (235,'14','Cultura','¿Qué número es importante en la cosmogonía<i>Wixárika</i>?',25,15,1,3,NULL,'235');
INSERT INTO `quiz_pregunta` VALUES (236,'15','Alimentos','¿Cómo se escribe verdolaga en <i>Wixárika</i>?',25,15,1,1,NULL,'236');
INSERT INTO `quiz_pregunta` VALUES (237,'15','Alimentos','¿Cómo se escribe verdolagas en <i>Wixárika</i>?',25,15,1,2,NULL,'237');
INSERT INTO `quiz_pregunta` VALUES (238,'15','Alimentos','¿Qué imagen corresponde a <i>aɨraxa</i>?',25,15,5,1,NULL,'238');
INSERT INTO `quiz_pregunta` VALUES (239,'15','Alimentos','¿Cómo se escribe guaje en <i>Wixárika</i>?',25,15,1,1,NULL,'239');
INSERT INTO `quiz_pregunta` VALUES (240,'15','Alimentos','¿Qué imagen corresponde a <i>haxi</i>?',25,15,5,1,NULL,'240');
INSERT INTO `quiz_pregunta` VALUES (241,'15','Alimentos','¿Cómo se escribe quesadilla con verdolagas en <i>Wixárika</i>?',25,15,1,2,NULL,'241');
INSERT INTO `quiz_pregunta` VALUES (242,'15','Plantas','¿Cómo se escribe orégano en <i>Wixárika</i>?',25,15,1,2,NULL,'242');
INSERT INTO `quiz_pregunta` VALUES (243,'15','Armas','¿Cómo se escribe arco en <i>Wixárika</i>?',25,15,1,2,NULL,'243');
INSERT INTO `quiz_pregunta` VALUES (244,'15,21','Armas','¿Cómo se escribe flecha en <i>Wixárika</i>?',25,15,1,2,NULL,'244');
INSERT INTO `quiz_pregunta` VALUES (245,'15,21','Armas','¿Cómo se escribe flechas en <i>Wixárika</i>?',25,15,1,2,NULL,'245');
INSERT INTO `quiz_pregunta` VALUES (246,'15','Armas','¿Para que se utilizan el arco y las flechas?',25,15,1,3,NULL,'246');
INSERT INTO `quiz_pregunta` VALUES (247,'15','Sitio sagrado','¿Cuál de los siguientes es un sitio sagrado para los <i>wixaritari</i>?',25,15,1,2,NULL,'247');
INSERT INTO `quiz_pregunta` VALUES (248,'15','Cacería','¿Qué tradición es esencial para la cultura <i>Wixárika</i>?',25,15,1,3,NULL,'248');
INSERT INTO `quiz_pregunta` VALUES (249,'15','Cultura','¿Cuántos cazadores cósmicos son?',25,15,1,3,NULL,'249');
INSERT INTO `quiz_pregunta` VALUES (250,'15','Cultura','¿Qué representan los cazadores cósmicos?',25,15,1,3,NULL,'250');
INSERT INTO `quiz_pregunta` VALUES (251,'16','Alimentos','¿Cómo se escribe nopal en <i>Wixárika</i>?',25,15,1,1,NULL,'251');
INSERT INTO `quiz_pregunta` VALUES (252,'16','Alimentos','¿Qué imagen corresponde a <i>na’akari</i>?',25,15,5,1,NULL,'252');
INSERT INTO `quiz_pregunta` VALUES (253,'16','Alimentos','¿Cómo se escribe guamúchil en <i>Wixárika</i>?',25,15,1,1,NULL,'253');
INSERT INTO `quiz_pregunta` VALUES (254,'16','Alimentos','¿Qué imagen corresponde al <i>muxu’uri</i>?',25,15,5,1,NULL,'254');
INSERT INTO `quiz_pregunta` VALUES (255,'16','Alimentos','¿Cómo se escribe frijoles cocidos en<i>Wixárika</i>?',25,15,1,2,NULL,'255');
INSERT INTO `quiz_pregunta` VALUES (256,'16','Alimentos','¿Cómo se escribe frijoles fritos en <i>Wixárika</i>?',25,15,1,2,NULL,'256');
INSERT INTO `quiz_pregunta` VALUES (257,'16','Plantas','¿Cómo se escribe eucalipto en <i>Wixárika</i>?',25,15,1,2,NULL,'257');
INSERT INTO `quiz_pregunta` VALUES (258,'16','Sitio sagrado','¿Cuál de los siguientes es un sitio sagrado para los <i>wixaritari</i>?',25,15,1,2,NULL,'258');
INSERT INTO `quiz_pregunta` VALUES (259,'16','Animales especiales','¿Cómo se escribe serpiente azul en <i>Wixárika</i>?',25,15,1,2,NULL,'259');
INSERT INTO `quiz_pregunta` VALUES (260,'16','Animales especiales','¿Cuál de los siguientes es un animal espiritual?',25,15,1,3,NULL,'260');
INSERT INTO `quiz_pregunta` VALUES (261,'19','Deidades','¿Quién elabora los trajes tradicionales <i>Wixárika</i>?',25,15,1,3,NULL,'261');
INSERT INTO `quiz_pregunta` VALUES (262,'16','Vestimenta','¿Qué visten los hombres <i>Wixárika</i>?',25,15,1,3,NULL,'262');
INSERT INTO `quiz_pregunta` VALUES (263,'16','Vestimenta','¿Qué accesorios usan los hombres <i>Wixárika</i>?',25,15,1,3,NULL,'263');
INSERT INTO `quiz_pregunta` VALUES (264,'16','Cultura','¿Cuántas facetas tiene <i>Tamatzi Kauyumárie</i> (Nuestro hermano mayor Venado Azul)?',25,15,1,3,NULL,'264');
INSERT INTO `quiz_pregunta` VALUES (265,'16','Cultura','¿Cuál es la deidad mensajera?',25,15,1,3,NULL,'265');
INSERT INTO `quiz_pregunta` VALUES (266,'17','Alimentos','¿Cómo se escribe quelites en <i>Wixárika</i>?',25,15,1,1,NULL,'266');
INSERT INTO `quiz_pregunta` VALUES (267,'17','Alimentos','¿Qué imagen corresponde a <i>ké’uxate</i>?',25,15,5,1,NULL,'267');
INSERT INTO `quiz_pregunta` VALUES (268,'17','Alimentos','¿Cómo se escribe semillas de calabaza en <i>Wixárika</i>?',25,15,1,1,NULL,'268');
INSERT INTO `quiz_pregunta` VALUES (269,'17','Alimentos','¿Qué imagen corresponde al <i>xutsi hatsiyarite</i>?',25,15,5,1,NULL,'269');
INSERT INTO `quiz_pregunta` VALUES (270,'17,22','Alimentos','¿Cómo se escribe atole en <i>Wixárika</i>?',25,15,1,2,NULL,'270');
INSERT INTO `quiz_pregunta` VALUES (271,'17','Alimentos','¿Cómo se escribe pan en <i>Wixárika</i>?',25,15,1,2,NULL,'271');
INSERT INTO `quiz_pregunta` VALUES (272,'17','Ofrendas','¿Qué es una <i>xukúri</i> (jícara)?',25,15,1,2,NULL,'272');
INSERT INTO `quiz_pregunta` VALUES (273,'17','Ofrendas','¿Qué representa la <i>xukúri</i>(jícara)?',25,15,1,3,NULL,'273');
INSERT INTO `quiz_pregunta` VALUES (274,'17','Ofrendas','¿Qué simboliza la <i>xukúri</i> (jícara)?',25,15,1,3,NULL,'274');
INSERT INTO `quiz_pregunta` VALUES (275,'17','Sitio sagrado','¿Cuál de los siguientes es un sitio sagrado para los <i>wixaritari</i>?',25,15,1,2,NULL,'275');
INSERT INTO `quiz_pregunta` VALUES (276,'17','Deidad','¿Quién es <i>Tatei Yurienáka</i>?',25,15,1,3,NULL,'276');
INSERT INTO `quiz_pregunta` VALUES (277,'17','Deidad','¿De qué es responsable <i>Tatei Yurienáka</i>?',25,15,1,3,NULL,'277');
INSERT INTO `quiz_pregunta` VALUES (278,'17','Deidad','¿Cómo se representa a <i>Tatei Yurienáka</i>?',25,15,1,3,NULL,'278');
INSERT INTO `quiz_pregunta` VALUES (279,'17','Cultura','¿Qué celebra la fiesta ceremonial “La danza de nuestras madres”?',25,15,1,3,NULL,'279');
INSERT INTO `quiz_pregunta` VALUES (280,'17','Cultura','¿Qué alimentos se preparan en “La fiesta del tambor”?',25,15,1,3,NULL,'280');
INSERT INTO `quiz_pregunta` VALUES (281,'17','Cultura','¿Quiénes reciben los primeros frutos de las cosechas?',25,15,1,3,NULL,'281');
INSERT INTO `quiz_pregunta` VALUES (282,'17','Cultura','¿Quién sobrevivió al gran diluvio y dio origen al pueblo <i>Wixárika</i>?',25,15,1,3,NULL,'282');
INSERT INTO `quiz_pregunta` VALUES (283,'17','Cultura','¿Qué llevó consigo <i>Watakame</i> en su canoa?',25,15,1,3,NULL,'283');
INSERT INTO `quiz_pregunta` VALUES (284,'17','Cultura','¿En qué sobrevivió al gran diluvio <i>Watakame</i>?',25,15,1,3,NULL,'284');
INSERT INTO `quiz_pregunta` VALUES (285,'18','Alimentos','¿Cómo se escribe maíz rojo en <i>Wixárika</i>?',25,15,1,1,NULL,'285');
INSERT INTO `quiz_pregunta` VALUES (286,'18','Alimentos','¿Cómo se escribe hongo en <i>Wixárika</i>?',25,15,1,1,NULL,'286');
INSERT INTO `quiz_pregunta` VALUES (287,'18','Alimentos','¿Cómo se escribe hongos en <i>Wixárika</i>?',25,15,1,1,NULL,'287');
INSERT INTO `quiz_pregunta` VALUES (288,'18','Alimentos','¿Qué imagen corresponde a <i>yekwa’ate</i>?',25,15,5,1,NULL,'288');
INSERT INTO `quiz_pregunta` VALUES (289,'18','Alimentos','¿Cómo se escribe quesadilla con hongos en <i>Wixárika</i>?',25,15,1,2,NULL,'289');
INSERT INTO `quiz_pregunta` VALUES (290,'18','Plantas','¿Cómo se escribe clavo en <i>Wixárika</i>?',25,15,1,2,NULL,'290');
INSERT INTO `quiz_pregunta` VALUES (291,'18','Armas','¿Cómo se escribe cuña en<i>Wixárika</i>?',25,15,1,2,NULL,'291');
INSERT INTO `quiz_pregunta` VALUES (292,'18','Sitio sagrado','¿Cuál de los siguientes es un sitio sagrado para los <i>wixaritari</i>?',25,15,1,2,NULL,'292');
INSERT INTO `quiz_pregunta` VALUES (293,'18','Autoridades','¿Cómo se escribe gobernador en <i>Wixárika</i>?',25,15,1,2,NULL,'293');
INSERT INTO `quiz_pregunta` VALUES (294,'18','Autoridades','¿Quién elige a los gobernadores?',25,15,1,3,NULL,'294');
INSERT INTO `quiz_pregunta` VALUES (295,'18','Música','¿Cuáles son los instrumentos tradicionales<i>Wixárika</i>?',25,15,1,3,NULL,'295');
INSERT INTO `quiz_pregunta` VALUES (296,'18','Cultura','¿Cuántas son las direcciones del universo?',25,15,1,3,NULL,'296');
INSERT INTO `quiz_pregunta` VALUES (297,'18','Cultura','¿Quiénes representan las direcciones del universo?',25,15,1,3,NULL,'297');
INSERT INTO `quiz_pregunta` VALUES (298,'19','Alimentos','¿Cómo se escribe pitahaya en <i>Wixárika</i>?',25,15,1,1,NULL,'298');
INSERT INTO `quiz_pregunta` VALUES (299,'19','Alimentos','¿Qué imagen corresponde a <i>ma’ara</i>?',25,15,5,1,NULL,'299');
INSERT INTO `quiz_pregunta` VALUES (300,'19','Alimentos','¿Cómo se escribe guayaba en <i>Wixárika</i>?',25,15,1,1,NULL,'300');
INSERT INTO `quiz_pregunta` VALUES (301,'19','Alimentos','¿Qué imagen corresponde a <i>ha’yewaxi</i>?',25,15,5,1,NULL,'301');
INSERT INTO `quiz_pregunta` VALUES (302,'19','Alimentos','¿Cómo se escribe gordita en <i>Wixárika</i>?',25,15,1,2,NULL,'302');
INSERT INTO `quiz_pregunta` VALUES (303,'19','Sitio sagrado','¿Cuál de los siguientes es un sitio sagrado para los <i>wixaritari</i>?',25,15,1,2,NULL,'303');
INSERT INTO `quiz_pregunta` VALUES (304,'19','Sitio sagrado','¿Cuál de los siguientes es un sitio sagrado para los <i>wixaritari</i>?',25,15,1,2,NULL,'304');
INSERT INTO `quiz_pregunta` VALUES (305,'19','Sitio sagrado','¿Cuál de los siguientes es un sitio sagrado para los <i>wixaritari</i>?',25,15,1,2,NULL,'305');
INSERT INTO `quiz_pregunta` VALUES (306,'19','Deidad','¿Quién es <i>Tatewari</i>?',25,15,1,3,NULL,'306');
INSERT INTO `quiz_pregunta` VALUES (307,'19','Deidad','¿Quién es la deidad tutelar de los <i>mara’kate</i> (chamanes)?',25,15,1,3,NULL,'307');
INSERT INTO `quiz_pregunta` VALUES (308,'19','Deidad','¿Quién es considerado “el gran transformador”?',25,15,1,3,NULL,'308');
INSERT INTO `quiz_pregunta` VALUES (309,'19','Cultura','¿En qué celebración se hace una peregrinación imaginaria a <i>Wirikuta</i>?',25,15,1,3,NULL,'309');
INSERT INTO `quiz_pregunta` VALUES (310,'19','Cultura','¿Por qué se hace una peregrinación imaginaria a <i>Wirikuta</i>?',25,15,1,3,NULL,'310');
INSERT INTO `quiz_pregunta` VALUES (311,'19','Cultura','¿En qué son convertidos los niños durante la fiesta del tambor?',25,15,1,3,NULL,'311');
INSERT INTO `quiz_pregunta` VALUES (312,'19','Cultura','¿Qué ganan los niños durante la fiesta del tambor?',25,15,1,3,NULL,'312');
INSERT INTO `quiz_pregunta` VALUES (313,'19','Cultura','¿Quién era la madre del joven cojo y tuerto?',25,15,1,3,NULL,'313');
INSERT INTO `quiz_pregunta` VALUES (314,'19','Cultura','¿En dónde arrojaron al joven cojo y tuerto?',25,15,1,3,NULL,'314');
INSERT INTO `quiz_pregunta` VALUES (315,'19','Cultura','¿En qué se transformó el joven cojo y tuerto?',25,15,1,3,NULL,'315');
INSERT INTO `quiz_pregunta` VALUES (316,'19','Cultura','¿Cómo se escribe sol en <i>Wixárika</i>?',25,15,1,3,NULL,'316');
INSERT INTO `quiz_pregunta` VALUES (317,'19','Cultura','¿Cómo se escribe fuego en <i>Wixárika</i>?',25,15,1,3,NULL,'317');
INSERT INTO `quiz_pregunta` VALUES (318,'19','Cultura','¿Quiénes no querían compartir el fuego?',25,15,1,3,NULL,'318');
INSERT INTO `quiz_pregunta` VALUES (319,'19','Cultura','¿Quién logró robar el fuego?',25,15,1,3,NULL,'319');
INSERT INTO `quiz_pregunta` VALUES (320,'19','Cultura','¿Con quiénes compartió el fuego el tlacuache?',25,15,1,3,NULL,'320');
INSERT INTO `quiz_pregunta` VALUES (321,'19','Cultura','¿Dónde es el santuario de <i>Tatewari</i> (Nuestro abuelo fuego)?',25,15,1,3,NULL,'321');
INSERT INTO `quiz_pregunta` VALUES (322,'20','Alimentos','¿Cómo se escribe jamaica en <i>Wixárika</i>?',25,15,1,1,NULL,'322');
INSERT INTO `quiz_pregunta` VALUES (323,'20','Alimentos','¿Qué imagen corresponde a <i>kamaika</i>?',25,15,5,1,NULL,'323');
INSERT INTO `quiz_pregunta` VALUES (324,'20','Alimentos','¿Cómo se escribe cebolla en <i>Wixárika</i>?',25,15,1,1,NULL,'324');
INSERT INTO `quiz_pregunta` VALUES (325,'20','Alimentos','¿Qué imagen corresponde a <i>uyuri</i>?',25,15,5,1,NULL,'325');
INSERT INTO `quiz_pregunta` VALUES (326,'20','Alimentos','¿Cómo se escribe pozole en <i>Wixárika</i>?',25,15,1,2,NULL,'326');
INSERT INTO `quiz_pregunta` VALUES (327,'20','Alimentos','¿Cómo se escribe agua de jamaica en <i>Wixárika</i>?',25,15,1,2,NULL,'327');
INSERT INTO `quiz_pregunta` VALUES (328,'20','Plantas','¿Cómo se escribe hierbabuena en <i>Wixárika</i>?',25,15,1,2,NULL,'328');
INSERT INTO `quiz_pregunta` VALUES (329,'20','Sitio sagrado','¿Cuál de los siguientes es un sitio sagrado para los <i>wixaritari</i>?',25,15,1,2,NULL,'329');
INSERT INTO `quiz_pregunta` VALUES (330,'20','Animales especiales','¿Cuál de los siguientes es un animal mensajero?',25,15,1,2,NULL,'330');
INSERT INTO `quiz_pregunta` VALUES (331,'20','Vestimenta','¿Qué visten las mujeres <i>Wixárika</i>?',25,15,1,3,NULL,'331');
INSERT INTO `quiz_pregunta` VALUES (332,'20','Vestimenta','¿Qué accesorios de chaquira usan las mujeres <i>Wixárika</i>?',25,15,1,3,NULL,'332');
INSERT INTO `quiz_pregunta` VALUES (333,'20','Vestimenta','¿Qué accesorios de estambre usan las mujeres <i>Wixárika</i>?',25,15,1,3,NULL,'333');
INSERT INTO `quiz_pregunta` VALUES (334,'21','Alimentos','¿Cómo se escribe calabacita en <i>Wixárika</i>?',25,15,1,1,NULL,'334');
INSERT INTO `quiz_pregunta` VALUES (335,'21','Alimentos','¿Qué imagen corresponde a <i>xútsi</i>?',25,15,5,1,NULL,'335');
INSERT INTO `quiz_pregunta` VALUES (336,'21','Alimentos','¿Cómo se escribe limón en <i>Wixárika</i>?',25,15,1,1,NULL,'336');
INSERT INTO `quiz_pregunta` VALUES (337,'21','Alimentos','¿Qué imagen corresponde a <i>tsinakari</i>?',25,15,5,1,NULL,'337');
INSERT INTO `quiz_pregunta` VALUES (338,'21,24','Alimentos','¿Cómo se escribe tejuino en <i>Wixárika</i>?',25,15,1,2,NULL,'338');
INSERT INTO `quiz_pregunta` VALUES (339,'21','Ofrendas','¿Qué es una <i>ɨ’rɨ</i> (flecha)?',25,15,1,3,NULL,'339');
INSERT INTO `quiz_pregunta` VALUES (340,'21','Ofrendas','¿Qué representa la <i>ɨ’rɨ</i> (flecha)?',25,15,1,3,NULL,'340');
INSERT INTO `quiz_pregunta` VALUES (341,'21','Ofrendas','¿Para que utilizan las deidades las <i>ɨ ’rɨte</i> (flechas)?',25,15,1,3,NULL,'341');
INSERT INTO `quiz_pregunta` VALUES (342,'21','Ofrendas','¿Para qué se utilizan las <i>ɨ ’rɨte</i> (flechas)?',25,15,1,3,NULL,'342');
INSERT INTO `quiz_pregunta` VALUES (343,'21','Sitio sagrado','¿Cuál de los siguientes es un sitio sagrado para los <i>wixaritari</i>?',25,15,1,2,NULL,'343');
INSERT INTO `quiz_pregunta` VALUES (344,'21','Deidad','¿Quién es <i>Tatei Xapawiyeme</i>?',25,15,1,3,NULL,'344');
INSERT INTO `quiz_pregunta` VALUES (345,'21','Cultura','¿Qué elaboran las madres de los niños en la fiesta del tambor?',25,15,1,3,NULL,'345');
INSERT INTO `quiz_pregunta` VALUES (346,'21','Cultura','¿Dónde deben realizar las familias la fiesta del tambor?',25,15,1,3,NULL,'346');
INSERT INTO `quiz_pregunta` VALUES (347,'21','Cultura','¿Por qué se llama la fiesta del tambor?',25,15,1,3,NULL,'347');
INSERT INTO `quiz_pregunta` VALUES (348,'21','Cultura','¿Cuántas son las diosas del agua?',25,15,1,3,NULL,'348');
INSERT INTO `quiz_pregunta` VALUES (349,'22','Alimentos','¿Cómo se escribe tamal en <i>Wixárika</i>?',25,15,1,1,NULL,'349');
INSERT INTO `quiz_pregunta` VALUES (350,'22,23,24','Animales','¿Cómo se escribe lobo en<i>Wixárika</i>?',25,15,1,1,NULL,'350');
INSERT INTO `quiz_pregunta` VALUES (351,'22','Plantas','¿Cómo se escribe planta para pintar el rostro en <i>Wixárika</i>?',25,15,1,1,NULL,'351');
INSERT INTO `quiz_pregunta` VALUES (352,'22','Armas','¿Cómo se escribe antorcha en <i>Wixárika</i>?',25,15,1,2,NULL,'352');
INSERT INTO `quiz_pregunta` VALUES (353,'22','Autoridades','¿Cómo se escribe peyotero en <i>Wixárika</i>?',25,15,1,2,NULL,'353');
INSERT INTO `quiz_pregunta` VALUES (354,'22','Autoridades','¿Quién se encarga de recolectar el peyote durante la peregrinación a <i>Wirikuta</i>?',25,15,1,3,NULL,'354');
INSERT INTO `quiz_pregunta` VALUES (355,'22','Cultura','¿Cómo se honra a los difuntos en la cultura <i>Wixárika</i>?',25,15,1,3,NULL,'355');
INSERT INTO `quiz_pregunta` VALUES (356,'22','Cultura','¿Cuál es la ceremonia para despedir el alma?',25,15,1,3,NULL,'356');
INSERT INTO `quiz_pregunta` VALUES (357,'22','Cultura','¿Qué significa <i>Mɨɨkí Kwevíxa</i>?',25,15,1,3,NULL,'357');
INSERT INTO `quiz_pregunta` VALUES (358,'22','Cultura','¿Qué es el <i>Mɨɨkí Kwevíxa</i> (invocar o llamar al muerto)?',25,15,1,3,NULL,'358');
INSERT INTO `quiz_pregunta` VALUES (359,'22','Cultura','¿Cómo se escribe muerte en <i>Wixárika</i>?',25,15,1,3,NULL,'359');
INSERT INTO `quiz_pregunta` VALUES (360,'22','Cultura','¿Qué pasa cuando alguien muere?',25,15,1,3,NULL,'360');
INSERT INTO `quiz_pregunta` VALUES (361,'22','Cultura','¿Qué debe darle el difunto al perro para evitar ser mordido?',25,15,1,3,NULL,'361');
INSERT INTO `quiz_pregunta` VALUES (362,'22','Cultura','¿Qué les pasa a los difuntos que dañaron a los animales en vida?',25,15,1,3,NULL,'362');
INSERT INTO `quiz_pregunta` VALUES (363,'23','Alimentos','¿Cómo se escribe mole en <i>Wixárika</i>?',25,15,1,1,NULL,'363');
INSERT INTO `quiz_pregunta` VALUES (364,'23','Animales especiales','¿Cómo se escribe zopilote en <i>Wixárika</i>?',25,15,1,2,NULL,'364');
INSERT INTO `quiz_pregunta` VALUES (365,'23','Animales especiales','¿Cuál de los siguientes es un animal espiritual?',25,15,1,3,NULL,'365');
INSERT INTO `quiz_pregunta` VALUES (366,'23','Cultura','¿Qué hace el <i>mara’kame</i> (chamán) durante el <i>Mɨɨkí Kwevíxa</i> ?',25,15,1,3,NULL,'366');
INSERT INTO `quiz_pregunta` VALUES (367,'23','Cultura','¿Qué hace la familia del difunto durante el <i>Mɨɨkí Kwevíxa</i>?',25,15,1,3,NULL,'367');
INSERT INTO `quiz_pregunta` VALUES (368,'23','Cultura','¿Qué es el <i>ririki</i>?',25,15,1,3,NULL,'368');
INSERT INTO `quiz_pregunta` VALUES (369,'23','Cultura','¿Cómo se escribe perro en <i>Wixárika</i>?',25,15,1,3,NULL,'369');
INSERT INTO `quiz_pregunta` VALUES (370,'23','Cultura','¿Qué pasa si una persona no cuido a su perro negro?',25,15,1,3,NULL,'370');
INSERT INTO `quiz_pregunta` VALUES (371,'23','Cultura','¿Qué pasa si una persona cuido a su perro negro?',25,15,1,3,NULL,'371');
INSERT INTO `quiz_pregunta` VALUES (372,'24','Plantas','¿Cómo se escribe tepehuaje en <i>Wixárika</i>?',25,15,1,2,NULL,'372');
INSERT INTO `quiz_pregunta` VALUES (373,'24','Deidad','¿Quién es <i>Tukákame</i>?',25,15,1,3,NULL,'373');
INSERT INTO `quiz_pregunta` VALUES (374,'24','Deidad','¿Qué representa <i>Tukákame</i>?',25,15,1,3,NULL,'374');
INSERT INTO `quiz_pregunta` VALUES (375,'24','Deidad','¿Qué hace <i>Tukákame</i>?',25,15,1,3,NULL,'375');
INSERT INTO `quiz_pregunta` VALUES (376,'24','Deidad','¿Cómo se presenta <i>Tukákame</i>?',25,15,1,3,NULL,'376');
INSERT INTO `quiz_pregunta` VALUES (377,'24','Cultura','¿Quiénes rigen la sociedad de los vivos?',25,15,1,3,NULL,'377');
INSERT INTO `quiz_pregunta` VALUES (378,'24','Cultura','¿Por qué pueden ser castigados los <i>wixaritari</i>?',25,15,1,3,NULL,'378');
INSERT INTO `quiz_pregunta` VALUES (379,'24','Cultura','¿Qué pasa si los <i>wixaritari</i> trasgreden las normas de convivencia?',25,15,1,3,NULL,'379');
INSERT INTO `quiz_pregunta` VALUES (380,'24','Cultura','¿Cómo son castigados los <i>wixaritari</i>?',25,15,1,3,NULL,'380');
INSERT INTO `quiz_pregunta` VALUES (381,'24','Cultura','¿Cuál es el peor castigo para los <i>wixaritari</i>?',25,15,1,3,NULL,'381');
INSERT INTO `quiz_pregunta` VALUES (382,'24','Cultura','¿Dónde son enterrados los bebés?',25,15,1,3,NULL,'382');
INSERT INTO `quiz_pregunta` VALUES (383,'24','Cultura','¿Qué se ofrece a los bebés difuntos?',25,15,1,3,NULL,'383');
INSERT INTO `quiz_pregunta` VALUES (384,'24','Cultura','¿Qué pasa con los bebés cuando mueren?',25,15,1,3,NULL,'384');
INSERT INTO `quiz_pregunta` VALUES (385,'24','Cultura','¿Con qué se comunica el <i>mara’kame</i> (chamán) con los muertos?',25,15,1,3,NULL,'385');
INSERT INTO `quiz_pregunta` VALUES (386,'24','Cultura','¿Cómo se comunica el <i>mara’kame</i> (chamán) con los muertos?',25,15,1,3,NULL,'386');
INSERT INTO `quiz_pregunta` VALUES (387,'24','Cultura','¿Qué pasa en el sueño sagrado?',25,15,1,3,NULL,'387');
INSERT INTO `quiz_pregunta` VALUES (388,'1,2,4,5,7,8,13,14,15,16,17,18,20','Animales','¿Qué imagen corresponde a <i>tuixuyeutanaka</i>?',25,15,5,1,NULL,'388');
INSERT INTO `quiz_pregunta` VALUES (389,'1,2,3,4,5,7,8,10,11,12,13,14,15,16,17,18,19,20','Animales','¿Qué imagen corresponde a <i>maxa</i>?',25,15,5,1,NULL,'389');
INSERT INTO `quiz_pregunta` VALUES (390,'1,4,7,15,16','Animales','¿Qué imagen corresponde a <i>máye</i>?',25,15,5,1,NULL,'390');
INSERT INTO `quiz_pregunta` VALUES (391,'1,2,10,11,12,19,22,23,24','Animales','¿Qué imagen corresponde a <i>tuuká</i>?',25,15,5,1,NULL,'391');
INSERT INTO `quiz_pregunta` VALUES (392,'2,4,6,10,11,12,13,17,18,20','Animales','¿Qué imagen corresponde a <i>kúu</i>?',25,15,5,1,NULL,'392');
INSERT INTO `quiz_pregunta` VALUES (393,'3,21','Animales','¿Qué imagen corresponde a <i>ketsɨ</i>?',25,15,5,1,NULL,'393');
INSERT INTO `quiz_pregunta` VALUES (394,'3,5,6,7,9,13,14,15,17,19,21','Animales','¿Qué imagen corresponde a <i>yáavi</i>?',25,15,5,1,NULL,'394');
INSERT INTO `quiz_pregunta` VALUES (395,'3,8','Animales','¿Qué imagen corresponde a <i>xiete</i>?',25,15,5,1,NULL,'395');
INSERT INTO `quiz_pregunta` VALUES (396,'3,8,9,13,16,20,21','Animales','¿Qué imagen corresponde a <i>kauxai</i>?',25,15,5,1,NULL,'396');
INSERT INTO `quiz_pregunta` VALUES (397,'5,14','Animales','¿Qué imagen corresponde a <i>tekɨ</i>?',25,15,5,1,NULL,'397');
INSERT INTO `quiz_pregunta` VALUES (398,'5,18,19,20,21','Animales','¿Qué imagen corresponde a <i>teruka</i>?',25,15,5,1,NULL,'398');
INSERT INTO `quiz_pregunta` VALUES (399,'5','Animales','¿Qué imagen corresponde a <i>mikɨri</i>?',25,15,5,1,NULL,'399');
INSERT INTO `quiz_pregunta` VALUES (400,'6,19','Animales','¿Qué imagen corresponde a <i>ke’etsé</i>?',25,15,5,1,NULL,'400');
INSERT INTO `quiz_pregunta` VALUES (401,'6','Animales','¿Qué imagen corresponde a <i>ha’axi</i>?',25,15,5,1,NULL,'401');
INSERT INTO `quiz_pregunta` VALUES (402,'8,20','Animales','¿Qué imagen corresponde a <i>weurai</i>?',25,15,5,1,NULL,'402');
INSERT INTO `quiz_pregunta` VALUES (403,'8','Animales','¿Qué imagen corresponde a <i>ɨkwi</i>?',25,15,5,1,NULL,'403');
INSERT INTO `quiz_pregunta` VALUES (404,'9,16','Animales','¿Qué imagen corresponde a <i>wakana</i>?',25,15,5,1,NULL,'404');
INSERT INTO `quiz_pregunta` VALUES (405,'10,17','Animales','¿Qué imagen corresponde a <i>tuixu</i>?',25,15,5,1,NULL,'405');
INSERT INTO `quiz_pregunta` VALUES (406,'11,15','Animales','¿Qué imagen corresponde a <i>tátsiu</i>?',25,15,5,1,NULL,'406');
INSERT INTO `quiz_pregunta` VALUES (407,'12','Animales','¿Qué imagen corresponde a <i>wakaxi</i>?',25,15,5,1,NULL,'407');
INSERT INTO `quiz_pregunta` VALUES (408,'12','Animales','¿Qué imagen corresponde a <i>werika</i>?',25,15,5,1,NULL,'408');
INSERT INTO `quiz_pregunta` VALUES (409,'16','Animales','¿Qué imagen corresponde a <i>haikɨ</i>?',25,15,5,1,NULL,'409');
INSERT INTO `quiz_pregunta` VALUES (410,'22,23,24','Animales','¿Qué imagen corresponde a <i>ɨxawe</i>?',25,15,5,1,NULL,'410');
INSERT INTO `quiz_pregunta` VALUES (411,'23','Animales','¿Qué imagen corresponde a <i>wirɨkɨ</i>?',25,15,5,1,NULL,'411');
INSERT INTO `quiz_pregunta` VALUES (412,'6','Sitio sagrado','¿Dónde está el sitio sagrado <i>Haramara</i>?',25,15,1,3,NULL,'412');
INSERT INTO `quiz_pregunta` VALUES (413,'6','Sitio sagrado','¿Cómo se llama la roca blanca que está en <i>Haramara</i>?',25,15,1,3,NULL,'413');
INSERT INTO `quiz_pregunta` VALUES (414,'13','Sitio sagrado','¿Dónde está el sitio sagrado <i>Wirikuta</i>?',25,15,1,3,NULL,'414');
INSERT INTO `quiz_pregunta` VALUES (415,'13','Sitio sagrado','¿Dónde ocurrió la creación del mundo?',25,15,1,3,NULL,'415');
INSERT INTO `quiz_pregunta` VALUES (416,'13','Sitio sagrado','¿Por dónde se levanta el sol?',25,15,1,3,NULL,'416');
INSERT INTO `quiz_pregunta` VALUES (417,'17','Sitio sagrado','¿Dónde está el sitio sagrado <i>Hauxa Manaka</i>?',25,15,1,3,NULL,'417');
INSERT INTO `quiz_pregunta` VALUES (418,'17','Sitio sagrado','¿Dónde es la casa de <i>Tututzi Maxa Kwaxi</i> (Nuestro Bisabuelo Cola de Venado)?',25,15,1,3,NULL,'418');
INSERT INTO `quiz_pregunta` VALUES (419,'19','Sitio sagrado','¿Dónde está el sitio sagrado <i>Te’akata</i>?',25,15,1,3,NULL,'419');
INSERT INTO `quiz_pregunta` VALUES (420,'19','Sitio sagrado','¿Dónde es el santuario de <i>Tatewari</i> (abuelo fuego)?',25,15,1,3,NULL,'420');
INSERT INTO `quiz_pregunta` VALUES (421,'19','Sitio sagrado','¿Dónde tuvo lugar la gesta universal?',25,15,1,3,NULL,'421');
INSERT INTO `quiz_pregunta` VALUES (422,'19','Sitio sagrado','¿Dónde es el centro del universo?',25,15,1,3,NULL,'422');
INSERT INTO `quiz_pregunta` VALUES (423,'19','Sitio sagrado','¿Dónde arrojaron a un niño enfermo al fuego para que se transformara en <i>Tayau</i> (Nuestro padre sol)?',25,15,1,3,NULL,'423');
INSERT INTO `quiz_pregunta` VALUES (424,'21','Sitio sagrado','¿Dónde está el sitio sagrado <i>Xapawiyemeta</i>?',25,15,1,3,NULL,'424');
INSERT INTO `quiz_pregunta` VALUES (425,'21','Sitio sagrado','¿Qué representa <i>Xapawiyemeta</i>?',25,15,1,3,NULL,'425');
INSERT INTO `quiz_pregunta` VALUES (426,'21','Sitio sagrado','¿Quién luchó contra seres inframundanos en <i>Xapawiyemeta</i>?',25,15,1,3,NULL,'426');
INSERT INTO `quiz_pregunta` VALUES (427,'21','Sitio sagrado','¿Por dónde salió <i>Tamatzi Paritsika</i> convertido en un gran sol?',25,15,1,3,NULL,'427');
INSERT INTO `quiz_pregunta` VALUES (428,'13','Cultura','¿Por qué deben realizarse las peregrinaciones?',25,15,1,3,NULL,'428');
INSERT INTO `quiz_pregunta` VALUES (429,'16','Deidad','¿Qué deidad <i>Wixárika</i> tiene 5 facetas?',25,15,1,3,NULL,'429');
INSERT INTO `quiz_pregunta` VALUES (430,'21','Sitio sagrado','¿Cuáles son los sitios sagrados <i>Wixárika</i>?',25,15,1,3,NULL,'430');
INSERT INTO `quiz_pregunta` VALUES (431,'18','Sitio sagrado','¿Cuáles son las direcciones del universo <i>Wixárika</i>?',25,15,1,3,NULL,'431');
INSERT INTO `quiz_pregunta` VALUES (432,'15','Deidad','¿Cuáles son los cazadores cósmicos <i>Wixárika</i>?',25,15,1,3,NULL,'432');
INSERT INTO `quiz_pregunta` VALUES (433,'17','Ofrendas','¿En qué se deposita la sangre de los animales sacrificados?',25,15,1,3,NULL,'433');
INSERT INTO `quiz_pregunta` VALUES (434,'18','Música','¿Cómo se dice instrumentos en <i>Wixárika</i>?',25,15,1,3,NULL,'434');
INSERT INTO `quiz_pregunta` VALUES (435,'18','Música','¿Cómo se dice violín en <i>Wixárika</i>?',25,15,1,3,NULL,'435');
INSERT INTO `quiz_pregunta` VALUES (436,'18','Música','¿Cómo se dice guitarra en <i>Wixárika</i>?',25,15,1,3,NULL,'436');
INSERT INTO `quiz_pregunta` VALUES (437,'18','Música','¿Cómo se dice maracas en <i>Wixárika</i>?',25,15,1,3,NULL,'437');
INSERT INTO `quiz_pregunta` VALUES (438,'5','Cultura','¿Qué es un <i>tukipa</i>?',25,15,1,3,NULL,'438');
INSERT INTO `quiz_pregunta` VALUES (439,'5','Cultura','¿Cuáles templos integran el <i>tukipa</i>?',25,15,1,3,NULL,'439');
INSERT INTO `quiz_pregunta` VALUES (440,'16','Animales especiales','¿De quiénes dependen las serpientes?',25,15,1,3,NULL,'440');
INSERT INTO `quiz_pregunta` VALUES (441,'16','Animales especiales','¿Para qué frotan una vaca con una serpiente?',25,15,1,3,NULL,'441');
INSERT INTO `quiz_pregunta` VALUES (442,'16','Animales especiales','¿Qué habilidad conceden las serpientes a las mujeres?',25,15,1,3,NULL,'442');
INSERT INTO `quiz_pregunta` VALUES (443,NULL,NULL,'¿Para qué se utiliza la sangre de los pescados?',25,15,1,3,NULL,'443');
INSERT INTO `diarios_viajes` VALUES (1,16,1,'RECOLECTA','ALIMENTO','FRUTA','Santiago Ixcuintla','Arrayan','Tsikwai','Arrayanes','Tsikwaita','El <i>tsikwai</i> (arrayan) se utiliza para hacer atoles.
Plural: <i>Tsikwaita</i> (arrayanes).','Images/Wixarika/Alimentos/Arrayanes','1,2');
INSERT INTO `diarios_viajes` VALUES (2,10,2,'RECOLECTA','ALIMENTO','FRUTA','SAMAO','Caña','Uwá','Cañas','Uwa’ate','<i>Uwá</i> (caña).
Plural: <i>Uwa’ate</i> (cañas).','Images/Wixarika/Alimentos/Caña','3,4');
INSERT INTO `diarios_viajes` VALUES (3,14,3,'RECOLECTA','ALIMENTO','FRUTA','Tepic','Ciruela','Kwarɨpa','Ciruelas','Kwarɨpate','<i>Kwarɨpa</i> (ciruela).
Plural: <i>Kwarɨpate</i> (ciruelas).','Images/Wixarika/Alimentos/Ciruela','5,6');
INSERT INTO `diarios_viajes` VALUES (4,47,4,'RECOLECTA','ALIMENTO','FRUTA','Durango','Guamúchil','Muxu’uri','Guamúchiles','Muxu’urite','<i>Muxu’uri</i> (<i>guamúchil</i>).
Plural: <i>Muxu’urite</i> (guamúchiles).','Images/Wixarika/Alimentos/Guamúchil','7,8');
INSERT INTO `diarios_viajes` VALUES (5,58,5,'RECOLECTA','ALIMENTO','FRUTA','Mezquitic','Guayaba','Ha’yewaxi','Guayabas','Ha’yewaxite','<i>Ha’yewaxi</i> (guayaba).
Plural: <i>Ha’yewaxite</i> (guayabas).','Images/Wixarika/Alimentos/Guayaba 1','9,10');
INSERT INTO `diarios_viajes` VALUES (6,37,6,'RECOLECTA','ALIMENTO','FRUTA','Real de Catorce','Higo','Piní','Higos','Piní','<i>Piní</i> (higo).','Images/Wixarika/Alimentos/Higo','11');
INSERT INTO `diarios_viajes` VALUES (7,17,7,'RECOLECTA','ALIMENTO','FRUTA','Santiago Ixcuintla','Mango','Ma’aku','Mangos','Ma’akute','<i>Máacu</i> (mango).
Plural: <i>Ma’akute</i> (mangos).','Images/Wixarika/Alimentos/Mango','12,13');
INSERT INTO `diarios_viajes` VALUES (8,19,8,'RECOLECTA','ALIMENTO','FRUTA','San Blas','Nanchi','Uwakí','Nanchis','Uwakiite','<i>Uwakí</i> (nanchi).
Plural: <i>Uwakiite</i> (nanchi).','Images/Wixarika/Alimentos/Nanchi','14,15');
INSERT INTO `diarios_viajes` VALUES (9,38,9,'RECOLECTA','ALIMENTO','FRUTA','Real de Catorce','Naranja','Narakaxi','Naranjas','Narakaxite','<i>Narakaxi</i> (naranja).
Plural: <i>Narakaxite</i> (naranjas).','Images/Wixarika/Alimentos/Naranja','16,17');
INSERT INTO `diarios_viajes` VALUES (10,59,10,'RECOLECTA','ALIMENTO','FRUTA','Mezquitic','Pitahaya','Ma’ara','Pitahayas','Ma’arate','<i>Ma’ara</i> (pitahaya).
Plural: <i>Ma’arate</i> (pitahaya).','Images/Wixarika/Alimentos/Pitahaya','18,19');
INSERT INTO `diarios_viajes` VALUES (11,20,11,'RECOLECTA','ALIMENTO','FRUTA','San Blas','Plátano','Ka’arú','Plátanos','Ka’arute','<i>Kaárú</i> (plátano).
Plural: <i>Ka’arute</i> (plátanos).','Images/Wixarika/Alimentos/Plátano','20,21');
INSERT INTO `diarios_viajes` VALUES (12,27,12,'RECOLECTA','ALIMENTO','FRUTA','Zacatecas','Tuna','Yɨɨna','Tunas','Yɨna’ate','<i>Yɨɨna</i> (tuna).
Plural: <i>Yɨna’ate</i> (tunas).','Images/Wixarika/Alimentos/Tuna','22,23');
INSERT INTO `diarios_viajes` VALUES (13,61,13,'RECOLECTA','ALIMENTO','FRUTA','Bolaños','Jamaica','Jamaicas','Kamaika','Kamaikas','La <i>kamaika</i> (jamaica) se utiliza para preparar bebidas y mermeladas.','Images/Wixarika/Alimentos/Jamaica','24');
INSERT INTO `diarios_viajes` VALUES (14,65,14,'RECOLECTA','ALIMENTO','VERDURA','Chapala','Calabacita','Xútsi','Calabacitas','Xutsíte','La <i>xútsi</i> (calabacita) es un alimento importante de la gastronomía <i>Wixárika</i>, se cultiva en los coamiles.
Plural: <i>Xutsíte</i> (calabacitas).','Images/Wixarika/Alimentos/Calabacita','25,26');
INSERT INTO `diarios_viajes` VALUES (15,28,15,'RECOLECTA','ALIMENTO','VERDURA','Zacatecas','Camote','Ye’eri','Camotes','Ye’erite','<i>Ye’eri</i> (camote).
Plural: <i>Ye’erite</i> (camote).','Images/Wixarika/Alimentos/Camote','27,28');
INSERT INTO `diarios_viajes` VALUES (16,62,-1,'RECOLECTA','ALIMENTO','VERDURA','Bolaños','Cebolla','Uyuri','Cebollas','Uyurite','<i>Uyuri</i> (cebolla).
Plural: <i>Uyurite</i> (cebollas).
','Images/Wixarika/Alimentos/Cebolla','29,30');
INSERT INTO `diarios_viajes` VALUES (17,70,16,'RECOLECTA','ALIMENTO','VERDURA',NULL,'Champiñon','U tuxa yekwa','Champiñones','U tuxa yekwa’ate','<i>U tuxa yekwa’ate</i> (champiñones).
Singular: <i>U tuxa yekwa</i> (champiñón).','Images/Wixarika/Alimentos/Hongo','31,32');
INSERT INTO `diarios_viajes` VALUES (18,6,17,'RECOLECTA','ALIMENTO','VERDURA','La Yesca','Chile','Kukúri','Chiles','Kukuríte','El <i>kukúri</i> (chile) se utiliza para hacer salsas y se cultiva en los coamiles.
Plural: <i>Kukuríte</i> (chiles).','Images/Wixarika/Alimentos/Chile','33,34');
INSERT INTO `diarios_viajes` VALUES (19,71,18,'RECOLECTA','ALIMENTO','VERDURA',NULL,'Elote','Ikɨri','Elotes','Ikɨríte','<i>Ikɨri</i> (elote).
Plural: <i>Ikɨríte</i> (elotes).','Images/Wixarika/Alimentos/Elote','35,36');
INSERT INTO `diarios_viajes` VALUES (20,2,19,'RECOLECTA','ALIMENTO','VERDURA','Del Nayar','Frijol','Múme','Frijoles','Múmete','El <i>múme</i> (frijol) es uno de los principales alimentos de la gastronomía <i>Wixárika</i>, se cultiva en los coamiles.
Plural: <i>Múmete</i> (frijoles).','Images/Wixarika/Alimentos/Frijol','37,38');
INSERT INTO `diarios_viajes` VALUES (21,44,20,'RECOLECTA','ALIMENTO','VERDURA','Pueblo Nuevo','Guaje','Haxi','Guajes','Haxite','<i>Haxi</i> (guaje).
Plural: <i>Haxite</i> (guaje).','Images/Wixarika/Alimentos/Guaje rojo','39,40');
INSERT INTO `diarios_viajes` VALUES (22,32,-1,'RECOLECTA','ALIMENTO','VERDURA','Santo Domingo',NULL,NULL,'Gualumbos','Tsíweri','<i>Tsíweri</i> (Gualumbos).','Images/Wixarika/Alimentos/Gualumbo','41');
INSERT INTO `diarios_viajes` VALUES (23,33,-1,'RECOLECTA','ALIMENTO','VERDURA','Santo Domingo',NULL,NULL,'Habas','Kweetsi','<i>Kweetsi</i> (Habas).','Images/Wixarika/Alimentos/Habas','42');
INSERT INTO `diarios_viajes` VALUES (24,56,21,'RECOLECTA','ALIMENTO','VERDURA','Huejiquilla','Hongo','Yekwa','Hongos','Yekwa’ate','Los <i>yekwa’ate</i> (hongos) son recolectados en los caminos durante la temporada de lluvias.
Singular: <i>Yekwa</i> (hongo).','Images/Wixarika/Alimentos/Hongo','43,44');
INSERT INTO `diarios_viajes` VALUES (25,13,22,'RECOLECTA','ALIMENTO','VERDURA','Tepic','Jícama','Xa’ata','Jícamas','Xata’ate','<i>Xa´ata</i> (jicama).
Plural: <i>Xata’ate</i> (jícamas).','Images/Wixarika/Alimentos/Jícama','45,46');
INSERT INTO `diarios_viajes` VALUES (26,27,23,'RECOLECTA','ALIMENTO','VERDURA','Fresnillo','Jitomate','Túmati','Jítomates','Túmatite','<i>Túmati</i> (jitomate)!.
Plural: <i>Túmatite</i> (jitomates).','Images/Wixarika/Alimentos/Jitomate','47,48');
INSERT INTO `diarios_viajes` VALUES (27,66,24,'RECOLECTA','ALIMENTO','VERDURA','Chapala','Limón','Tsinakari','Limones','Tsinakarite','<i>Tsinakari</i> (limón).
Plural: <i>Tsinakarite</i> (limones).','Images/Wixarika/Alimentos/Limón','49,50');
INSERT INTO `diarios_viajes` VALUES (28,48,25,'RECOLECTA','ALIMENTO','VERDURA','Durango','Nopal','Na’akari','Nopales','Na’akarite','<i>Na’akari</i> (nopal).
Plural: <i>Na’akarite</i> (nopales).','Images/Wixarika/Alimentos/Nopal','51,52');
INSERT INTO `diarios_viajes` VALUES (29,35,-1,'RECOLECTA','ALIMENTO','VERDURA','Charcas','Pochote','Karimutsi',NULL,NULL,'<i>Karimutsi</i> (Pochote).','Images/Wixarika/Alimentos/Pochote','53,54');
INSERT INTO `diarios_viajes` VALUES (30,51,26,'RECOLECTA','ALIMENTO','VERDURA','Canatlán','Quelite','Ké’uxa','Quelites','Ké’uxate','Los <i>ké’uxate</i> (quelites) se guisan y siven para rellenar quesadillas.
Singular: <i>Ké’uxa</i> (quelite).','Images/Wixarika/Alimentos/Quelites','55,56');
INSERT INTO `diarios_viajes` VALUES (31,52,27,'RECOLECTA','ALIMENTO','VERDURA','Canatlán','Semilla de calabaza','Xutsi hatsiyari','Semillas de calabaza','Xutsi hatsiyarite','<i>Xutsi hatsiyarite</i> (semillas de calabaza).
Singular: <i>Xutsi hatsiyari</i> (semilla de calabaza).','Images/Wixarika/Alimentos/Semilla de calabaza','57,58');
INSERT INTO `diarios_viajes` VALUES (32,45,28,'RECOLECTA','ALIMENTO','VERDURA','Pueblo Nuevo','Verdolaga','Aɨraxa','Verdolagas','Aɨraxate','Las <i>aɨraxate</i> (verdolagas) se guisan y siven para rellenar quesadillas.
Singular: <i>Aɨraxa</i> (verdolagas).','Images/Wixarika/Alimentos/Verdolaga','59,60');
INSERT INTO `diarios_viajes` VALUES (33,1,29,'RECOLECTA','ALIMENTO','MAIZ','Del Nayar','Maíz','Ikú','Maíces','Iku’ute','El <i>ikú</i> (maíz) es el alimento más importante para los <i>wixaritari</i>, es su fuente principal de comida.
Plural: <i>Iku’ute</i> (maíces).','Images/Wixarika/Alimentos/Maíz','61,62');
INSERT INTO `diarios_viajes` VALUES (34,5,30,'RECOLECTA','ALIMENTO','MAIZ','La Yesca','Maíz amarillo','Ikú taxawime','Maíces amarillos','Iku’ute taxawime','El <i>ikú taxawime</i> (maíz amarillo) se utiliza como alimento para los animales.
Plural: <i>Iku’ute taxawime</i> (maíces amarillos).','Images/Wixarika/Alimentos/Maíz amarillo','63,64');
INSERT INTO `diarios_viajes` VALUES (35,30,31,'RECOLECTA','ALIMENTO','MAIZ','Villa de Ramos','Maíz azul','Ikú yuawime','Maíces azules','Iku’ute yuawime','El <i>ikú yuawime</i> (maíz azul) se utiliza para la preparación de tortillas, atole, pinole y para dar color a los alimentos.
Plural: <i>Iku’ute yuawime</i> (maíces azules).','Images/Wixarika/Alimentos/Maíz morado','65,66');
INSERT INTO `diarios_viajes` VALUES (36,41,32,'RECOLECTA','ALIMENTO','MAIZ','Mezquital','Maíz blanco','Ikú tuuxá','Maíces blancos','Iku’ute tuxame','El <i>iIú tuuxá</i> (maíz blanco) se utiliza en las ceremonias religiosas.
Plural: <i>Iku’ute tuxame</i> (maíces blancos).','Images/Wixarika/Alimentos/Maíz blanco','67,68');
INSERT INTO `diarios_viajes` VALUES (37,23,33,'RECOLECTA','ALIMENTO','MAIZ','Valparíso','Maíz morado','Ikú tataɨrawi','Maíces morados','Iku’ute taɨrawime','El <i>ikú tataɨrawi</i> (maíz morado) se utiliza para la preparación de tortillas y para dar color a los alimentos.
Plural: <i>Iku’ute taɨrawime</i> (maíces morados).','Images/Wixarika/Alimentos/Maíz morado','69,70');
INSERT INTO `diarios_viajes` VALUES (38,24,34,'RECOLECTA','ALIMENTO','MAIZ','Valparíso','Maíz negro','Ikú yɨwi','Maíces negros','Iku’ute yɨyɨwi','<i>Ikú yɨwi</i> (maíz negro).
Plural: <i>Iku’ute yɨyɨwi</i> (maíces negros).','Images/Wixarika/Alimentos/Maíz morado','71,72');
INSERT INTO `diarios_viajes` VALUES (39,55,35,'RECOLECTA','ALIMENTO','MAIZ','Huejiquilla','Maíz rojo','Ikú mɨxeta','Maíces rojos','Iku’ute xetame','<i>Ikú mɨxeta</i> (maíz rojo).
Plural: <i>Iku’ute xetame</i> (maíces rojos).','Images/Wixarika/Alimentos/Maíz rojo','73,74');
INSERT INTO `diarios_viajes` VALUES (40,72,36,'RECOLECTA','ALIMENTO','PROTEINA',NULL,'Carne de ardilla','Tekɨ','Carne de ardillas','Tekɨri','Carne de <i>tekɨ</i> (ardilla).','Images/Wixarika/Alimentos/Carne ardilla','75');
INSERT INTO `diarios_viajes` VALUES (41,73,37,'RECOLECTA','ALIMENTO','PROTEINA',NULL,'Carne de cerdo','Tuixu','Carne de cerdos','Tuixuri','Carne de <i>tuixu</i> (cerdo).','Images/Wixarika/Alimentos/Carne cerdo','76');
INSERT INTO `diarios_viajes` VALUES (42,74,38,'RECOLECTA','ALIMENTO','PROTEINA',NULL,'Carne de conejo','Tátsiu','Carne de conejos','Tatsiurixi','Carne de <i>tátsiu</i> (conejo).','Images/Wixarika/Alimentos/Carne conejo','77');
INSERT INTO `diarios_viajes` VALUES (43,75,39,'RECOLECTA','ALIMENTO','PROTEINA',NULL,'Carne de güilota','Weurai','Carne de güilotas','Weuraixi','Carne de <i>weurai</i> (güilota).','Images/Wixarika/Alimentos/Carne pollo','78');
INSERT INTO `diarios_viajes` VALUES (44,76,40,'RECOLECTA','ALIMENTO','PROTEINA',NULL,'Carne de iguana','Keetse','Carne de iguanas','Ketse’ete','Carne de <i>ke’etsé</i> (iguana).
La <i>ke’etsé</i> (iguana) es consumida tradicionalmente en pipián.','Images/Wixarika/Alimentos/Carne iguana','79,80');
INSERT INTO `diarios_viajes` VALUES (45,77,41,'RECOLECTA','ALIMENTO','PROTEINA',NULL,'Carne de jabalí','Tuixuyeutanaka','Carne de jabalís','Tuixuriyeutari','Carne de <i>tuixuyeutanaka</i> (jabalí).
El <i>tuixuyeutanaka</i> (jabalí) es consumido tradicionalmente en albóndigas.','Images/Wixarika/Alimentos/Carne jabalí','81,82');
INSERT INTO `diarios_viajes` VALUES (46,78,42,'RECOLECTA','ALIMENTO','PROTEINA',NULL,'Carne de pescado','Ketsɨ','Carne de pescados','Ketsɨte','<i>Ketsí</i> (pescado).
La sangre de <i>ketsí</i> (pescado) se utiliza en los rituales sagrados cuando no se caza un venado.','Images/Wixarika/Alimentos/Pescado','83,84');
INSERT INTO `diarios_viajes` VALUES (47,79,43,'RECOLECTA','ALIMENTO','PROTEINA',NULL,'Carne de pollo','Wakana','Carne de pollos','Wakana','Carne de <i>wakana</i> (pollo).','Images/Wixarika/Alimentos/Carne pollo','85');
INSERT INTO `diarios_viajes` VALUES (48,80,44,'RECOLECTA','ALIMENTO','PROTEINA',NULL,'Carne de venado','Maxa','Carnes de venados','Maxatsi','Carne de <i>maxa</i> (venado).
El <i>maxa</i> (venado) es consumido tradicionalmente en mole.','Images/Wixarika/Alimentos/Carne venado','86,87');
INSERT INTO `diarios_viajes` VALUES (49,3,45,'RECOLECTA','ALIMENTO','BEBIDA','Del Nayar','Agua','Ha’a',NULL,NULL,'<i>Ha’a</i> (agua).','Images/Wixarika/Alimentos/Agua','88');
INSERT INTO `diarios_viajes` VALUES (50,63,46,'RECOLECTA','ALIMENTO','BEBIDA','Bolaños','Agua de jamaica','Kamaika hayaári',NULL,NULL,'<i>Kamaika hayaári</i> (agua de jamaica).','Images/Wixarika/Alimentos/Agua de jamaica','89');
INSERT INTO `diarios_viajes` VALUES (51,42,50,'RECOLECTA','ALIMENTO','BEBIDA','Mezquital','Jugo de caña','Uwá hayaári',NULL,NULL,'<i>Uwá hayaári</i> (jugo de caña).','Images/Wixarika/Alimentos/Jugo de caña','90');
INSERT INTO `diarios_viajes` VALUES (52,39,51,'RECOLECTA','ALIMENTO','BEBIDA','Real de Catorce','Jugo de naranja','Narakaxi hayaári',NULL,NULL,'<i>Narakaxi hayaári</i> (jugo de naranja).','Images/Wixarika/Alimentos/Jugo de naranja','91');
INSERT INTO `diarios_viajes` VALUES (53,53,47,'RECOLECTA','ALIMENTO','BEBIDA','Canatlán','Atole','Hamuitsi','Atoles','Hamuitsite','El <i>hamuitsi</i> (atole) es una bebida típica <i>Wixárika</i>. 
Plural: <i>Hamuitsite</i> (atoles).
','Images/Wixarika/Alimentos/Atole','92,93');
INSERT INTO `diarios_viajes` VALUES (54,25,48,'RECOLECTA','ALIMENTO','BEBIDA','Valparíso','Chicuatol','Tsinari','Chicuatoles','Tsinarite','El <i>tsinari</i> (chicuatol) es una bebida típica <i>Wixárika</i>.
Plural: <i>Tsinarite</i> (chicuatoles).','Images/Wixarika/Alimentos/Chicuatol','94,95');
INSERT INTO `diarios_viajes` VALUES (55,67,53,'RECOLECTA','ALIMENTO','BEBIDA','Chapala','Tejuino','Nawá','Tejuinos','Nawa’ate','El <i>nawá</i> (tejuino) es una bebida típica <i>Wixárika</i>. 
Plural: <i>Nawa’ate</i> (tejuinos).
','Images/Wixarika/Alimentos/Tejuino','96,97');
INSERT INTO `diarios_viajes` VALUES (56,9,55,'RECOLECTA','ALIMENTO','POSTRE','SAMAO','Miel','Xiete',NULL,NULL,'La <i>xiete</i> (miel) se utiliza para endulzar postres y bebidas tradicionales.','Images/Wixarika/Alimentos/Miel','98');
INSERT INTO `diarios_viajes` VALUES (57,54,56,'RECOLECTA','ALIMENTO','POSTRE','Canatlán','Pan','Paní','Panes','Panite','<i>Paní</i> (pan).
Plural: <i>Panite</i> (panes).
','Images/Wixarika/Alimentos/Pan','99,100');
INSERT INTO `diarios_viajes` VALUES (58,26,57,'RECOLECTA','ALIMENTO','POSTRE','Valparíso','Pan de elote','Ikɨri paniyari',NULL,NULL,'<i>Ikɨri paniyari</i> (pan de elote).','Images/Wixarika/Alimentos/Pan de elote','101');
INSERT INTO `diarios_viajes` VALUES (59,11,59,'RECOLECTA','ALIMENTO','POSTRE','SAMAO','Piloncillo','Tsakaka','Piloncillos','Tsakakate','El <i>tsakaka</i> (piloncillo) se utiliza para endulzar bebidas y postres tradicionales.
Plural: <i>Tsakakate</i> (piloncillos).
','Images/Wixarika/Alimentos/Piloncillo','102,103');
INSERT INTO `diarios_viajes` VALUES (60,43,60,'RECOLECTA','ALIMENTO','POSTRE','Mezquital','Pinole','Pexúri','Pinoles','Pexúrite','El <i>pexúri</i> (pinole) es un alimento tradicional <i>Wixárika</i>. 
Plural: <i>Pexúrite</i> (pinoles).
','Images/Wixarika/Alimentos/Pinole','104,105');
INSERT INTO `diarios_viajes` VALUES (61,21,58,'RECOLECTA','ALIMENTO','POSTRE','San Blas','Plátano frito','Ka’arú wiyamatɨ','Plátanos fritos','Ka’arúte wiyamatika','<i>Ka’arú wiyamatɨ</i> (plátano frito).
Plural: <i>Ka’arúte wiyamatika</i> (plátano frito).
','Images/Wixarika/Alimentos/Plátano frito','106,107');
INSERT INTO `diarios_viajes` VALUES (62,8,84,'RECOLECTA','ALIMENTO','PLATILLO','La Yesca','Albóndiga de jabalí','Albóndiga de tuixuyeutanaka','Albóndigas de jabalí','Albóndigas de tuixuyeutanaka','La albóndiga de <i>tuixuyeutanaka</i> (jabalí) es un platillo tradicional <i>Wixárika</i>.
Plural: Albóndigas de <i>tuixuyeutanaka</i> (jabalí).','Images/Wixarika/Alimentos/Albóndiga de jabalí','108,109');
INSERT INTO `diarios_viajes` VALUES (63,18,89,'RECOLECTA','ALIMENTO','PLATILLO','Santiago Ixcuintla','Caldo de ardilla','Tekɨ itsari','Caldos de ardilla','Tekɨ itsarite','<i>Tekɨ itsari</i> (caldo de ardilla).
Plural: <i>Tekɨ itsarite</i> (caldos de ardilla).
','Images/Wixarika/Alimentos/Caldo de ardilla','110,111');
INSERT INTO `diarios_viajes` VALUES (64,34,107,'RECOLECTA','ALIMENTO','PLATILLO','Santo Domingo','Caldo de conejo','Tátsiu itsari','Caldos de conejo','Tátsiu itsárite','<i>Tátsiu itsari</i> (caldo de conejo).
Plural: <i>Tátsiu itsárite</i> (caldos de conejo).
','Images/Wixarika/Alimentos/Caldo de conejo','112,113');
INSERT INTO `diarios_viajes` VALUES (65,28,95,'RECOLECTA','ALIMENTO','PLATILLO','Fresnillo','Caldo de güilota','Weurai itsari','Caldos de güilota','Weuraixi wa’itsari','<i>Weurai itsari</i> (caldo de güilota).
Plural: <i>Weuraixi wa’itsari</i> (caldos de güilota).
','Images/Wixarika/Alimentos/Caldo de güilota','114,115');
INSERT INTO `diarios_viajes` VALUES (66,36,62,'RECOLECTA','ALIMENTO','PLATILLO','Charcas','Elote asado','Ikɨri warikietɨ','Elotes asados','Ikɨri wawarikitɨka','El <i>ikɨri warikietɨ</i> (elote asado) es un alimento tradicional <i>Wixárika</i>.
Plural: <i>Ikɨri wawarikitɨka</i> (elotes asados).
','Images/Wixarika/Alimentos/Elote asado','116,117');
INSERT INTO `diarios_viajes` VALUES (67,29,99,'RECOLECTA','ALIMENTO','PLATILLO','Zacatecas','Enchilada de pollo','Enchilada de wakana','Enchiladas de pollo','Enchiladas de wakanari','Enchilada de <i>wakana</i> (pollo).
Plural: Enchiladas de <i>wakana</i> (pollo).
','Images/Wixarika/Alimentos/Enchilada de pollo','118,119');
INSERT INTO `diarios_viajes` VALUES (68,49,72,'RECOLECTA','ALIMENTO','PLATILLO','Durango','Frijol cocido','Múme kwakwaxitɨ','Frijoles cocidos','Múme kwakwaxitɨ','Los <i>múme kwakwaxitɨ</i> (frijoles cocidos) son alimentos tradicionales <i>Wixárika</i>.','Images/Wixarika/Alimentos/Frijol cocido','120');
INSERT INTO `diarios_viajes` VALUES (69,50,73,'RECOLECTA','ALIMENTO','PLATILLO','Durango','Frijol frito','Múme wiyamari','Frijoles fritos','Múme wiyamarietɨka','Los <i>múme wiyamarietɨka</i> (frijoles fritos) son alimentos tradicionales <i>Wixárika</i>.','Images/Wixarika/Alimentos/Frijol frito','121');
INSERT INTO `diarios_viajes` VALUES (70,60,64,'RECOLECTA','ALIMENTO','PLATILLO','Mezquitic','Gordita','Tsuirá','Gorditas','Tsuiráte','La <i>tsuirá</i> (gordita) es un alimento popular <i>Wixárika</i>.
Plural: <i>Tsuiráte</i> (gordita).
','Images/Wixarika/Alimentos/Gordita','122,123');
INSERT INTO `diarios_viajes` VALUES (71,69,80,'RECOLECTA','ALIMENTO','PLATILLO','Hewiixi','Mole','Pexuri',NULL,NULL,'El <i>pexuri</i> (mole) es un alimento tradicional <i>Wixárika</i>.','Images/Wixarika/Alimentos/Mole de venado','124');
INSERT INTO `diarios_viajes` VALUES (72,15,88,'RECOLECTA','ALIMENTO','PLATILLO','Tepic','Mole de venado','Maxa ikwaiyári',NULL,NULL,'El <i>maxa ikwaiyári</i> (mole de venado) es un platillo tradicional <i>Wixárika</i>.','Images/Wixarika/Alimentos/Mole de venado','125');
INSERT INTO `diarios_viajes` VALUES (73,12,94,'RECOLECTA','ALIMENTO','PLATILLO','SAMAO','Pescado sarandeado','Ketsɨ warikietɨ','Pescados sarandeados','Ketsɨte me warikietɨkaitɨ','<i>Ketsɨ warikietɨ</i> (pescado sarandeado).
Plural: <i>Ketsɨte me warikietɨkaitɨ</i> (pescados sarandeados).
','Images/Wixarika/Alimentos/Pescado sarandeado','126,127');
INSERT INTO `diarios_viajes` VALUES (74,22,91,'RECOLECTA','ALIMENTO','PLATILLO','San Blas','Pipián de iguana','Pipián de ke’etse',NULL,NULL,'El pipián de <i>ke’etse</i> (iguana) es un platillo tradicional <i>Wixárika</i>.','Images/Wixarika/Alimentos/Pipián de iguana','128');
INSERT INTO `diarios_viajes` VALUES (75,64,81,'RECOLECTA','ALIMENTO','PLATILLO','Bolaños','Pozole','kwitsari','Pozoles','Kwitsarite','<i>Kwitsari</i> (pozole).
Plural: <i>Kwitsarite</i> (pozoles).
','Images/Wixarika/Alimentos/Pozole de cerdo','129,130');
INSERT INTO `diarios_viajes` VALUES (76,57,-1,'RECOLECTA','ALIMENTO','PLATILLO','Huejiquilla','Quesadilla con hongos','Quesadilla con <i>yekwa’ate</i>','Quesadillas con hongos','Quesadillas con <i>yekwa’ate</i>','La quesadilla con <i>yekwa’ate</i> (hongos) es un alimento popular <i>Wixárika</i>.
Plural: Quesadillas con <i>yekwa’ate</i> (hongos).','Images/Wixarika/Alimentos/Quesadilla','131,132');
INSERT INTO `diarios_viajes` VALUES (77,46,69,'RECOLECTA','ALIMENTO','PLATILLO','Pueblo Nuevo','Quesadilla con verdolagas','Quesadilla con aɨraxate','Quesadillas con verdolagas','Quesadillas con aɨraxate','La quesadilla con <i>aɨraxate</i> (verdolagas) es un alimento popular <i>Wixárika</i>.
Plural: quesadillas con <i>aɨraxate</i> (verdolagas).
','Images/Wixarika/Alimentos/Quesadilla','133,134');
INSERT INTO `diarios_viajes` VALUES (78,7,82,'RECOLECTA','ALIMENTO','PLATILLO','La Yesca','Queso','Kexiu','Quesos','Kexiute','Los <i>wixaritari</i> elaboran <i>kexiu</i> (queso).
Plural: <i>Kexiute</i> (quesos).','Images/Wixarika/Alimentos/Queso','135,136');
INSERT INTO `diarios_viajes` VALUES (79,40,83,'RECOLECTA','ALIMENTO','PLATILLO','Real de Catorce','Sopa','Xupaxi','Sopas','Xupaxite','<i>Xupaxi</i> (sopa).
Plural: <i>Xupaxite</i> (sopa).','Images/Wixarika/Alimentos/Sopa aguada','137,138');
INSERT INTO `diarios_viajes` VALUES (80,31,74,'RECOLECTA','ALIMENTO','PLATILLO','Villa de Ramos','Taco de frijoles','Múmete takuyari','Tacos de frijoles','Múmete takuxiyari','El <i>múmete takuyari</i> (taco de frijoles) es un alimento principal en la gastronomía <i>Wixárika</i>.
Plural: <i>Múmete takuxiyari</i> (tacos de frijoles).
','Images/Wixarika/Alimentos/Taco de frijol','139,140');
INSERT INTO `diarios_viajes` VALUES (81,68,70,'RECOLECTA','ALIMENTO','PLATILLO','Miiki','Tamal','Tétsu','Tamales','Tétsute','El <i>tétsu</i> (tamal) es un alimento tradicional <i>Wixárika</i>. 
Plural: <i>Tétsute</i> (tamales).
','Images/Wixarika/Alimentos/Tamal','141,142');
INSERT INTO `diarios_viajes` VALUES (82,4,66,'RECOLECTA','ALIMENTO','PLATILLO','Del Nayar','Tortilla','Pa’apa','Tortillas','Papa’ate','<i>Pa’apa</i> (tortilla).
Plural: <i>Papa’ate</i> (tortillas).
','Images/Wixarika/Alimentos/Tortilla','143,144');
INSERT INTO `diarios_viajes` VALUES (83,1,1,'RECOLECTA','PLANTA_MEDICINAL','CURATIVA',NULL,'Agave','Mai','Agaves','Maite','El <i>mai</i> (agave) es una planta medicinal que se utiliza como digestivo.','Images/Wixarika/Planta_Medicinal/Agave','145');
INSERT INTO `diarios_viajes` VALUES (84,2,2,'RECOLECTA','PLANTA_MEDICINAL','CURATIVA',NULL,'Aloe vera',NULL,'Aloes vera',NULL,'El aloe vera es una planta medicinal que se utiliza como desintoxicante.','Images/Wixarika/Planta_Medicinal/Aloe vera','146');
INSERT INTO `diarios_viajes` VALUES (85,3,3,'RECOLECTA','PLANTA_MEDICINAL','CURATIVA',NULL,'Cebolla','Uyuri','Cebollas','Uyurite','La <i>uyuri</i> (cebolla) es una planta medicinal que se utiliza para las cortadas.
Plural: <i>Uyurite</i> (cebollas).','Images/Wixarika/Alimentos/Cebolla','147,148');
INSERT INTO `diarios_viajes` VALUES (86,4,4,'RECOLECTA','PLANTA_MEDICINAL','CURATIVA','Huejiquilla','Clavo','Kɨrapu','Clavos','Kɨrapuxi','El <i>kɨrapu</i> (clavo) es una planta medicinal que se utiliza para el dolor de muelas.
Plural: <i>Kɨrapuxi</i> (clavo).','Images/Wixarika/Planta_Medicinal/Clavo','149,150');
INSERT INTO `diarios_viajes` VALUES (87,5,5,'RECOLECTA','PLANTA_MEDICINAL','CURATIVA',NULL,'Cola de caballo',NULL,NULL,NULL,'La cola de caballo es una planta medicinal que se utiliza para la caída de cabello .','Images/Wixarika/Planta_Medicinal/Cola de caballo','151');
INSERT INTO `diarios_viajes` VALUES (88,6,6,'RECOLECTA','PLANTA_MEDICINAL','CURATIVA',NULL,'Estafiate',NULL,NULL,NULL,'El estafiate es una planta medicinal que se utiliza para el dolor de estómago.','Images/Wixarika/Planta_Medicinal/Estafiate','152');
INSERT INTO `diarios_viajes` VALUES (89,7,7,'RECOLECTA','PLANTA_MEDICINAL','CURATIVA','Durango','Eucalipto','Eɨkariti','Eucaliptos',NULL,'El <i>eɨkariti</i> (eucalipto) es una planta medicinal que se utiliza para el resfriado.','Images/Wixarika/Planta_Medicinal/Eucalipto','153');
INSERT INTO `diarios_viajes` VALUES (90,8,8,'RECOLECTA','PLANTA_MEDICINAL','CURATIVA','Fresnillo','Gordolobo','Ɨrawe emɨtimariwe','Gordolobos','Ɨrawetsixi ememɨtemamariwawe','El <i>ɨrawe emɨtimariwe</i> (gordolobo) es una planta medicinal que se utiliza para la tos.
Plural: <i>Ɨrawetsixi ememɨtemamariwawe</i> (gordolobos).','Images/Wixarika/Planta_Medicinal/Gordolobo','154,155');
INSERT INTO `diarios_viajes` VALUES (91,9,9,'RECOLECTA','PLANTA_MEDICINAL','CURATIVA','Bolaños','Hierbabuena','Yervawena','Hierbabuenas',NULL,'La <i>yervawena</i> (hierbabuena) es una planta medicinal que se utiliza para los problemas estomacales.','Images/Wixarika/Planta_Medicinal/Hierbabuena','156');
INSERT INTO `diarios_viajes` VALUES (92,10,10,'RECOLECTA','PLANTA_MEDICINAL','CURATIVA','Tepic','Manzanilla','Mantsaniya','Manzanillas',NULL,'La <i>mantsaniya</i> (mazanilla) es una planta medicinal que se utiliza como antiinflamatorio.','Images/Wixarika/Planta_Medicinal/Manzanilla','157');
INSERT INTO `diarios_viajes` VALUES (93,11,11,'RECOLECTA','PLANTA_MEDICINAL','CURATIVA','La Yesca','Milpa','Wáxa','Milpas','Waxate','La <i>wáxa</i> (milpa) es una planta medicinal que se utiliza como analgésico.
Plural: <i>Waxate</i> (milpas).','Images/Wixarika/Planta_Medicinal/Milpa','158,159');
INSERT INTO `diarios_viajes` VALUES (94,12,12,'RECOLECTA','PLANTA_MEDICINAL','CURATIVA','Pueblo Nuevo','Orégano','Orekanu','Oréganos',NULL,'El <i>orekanu</i> (orégano) es una planta medicinal que se utiliza para el dolor muscular y de oído.','Images/Wixarika/Planta_Medicinal/Óregano','160');
INSERT INTO `diarios_viajes` VALUES (95,13,13,'RECOLECTA','PLANTA_MEDICINAL','CURATIVA','Charcas','Peyote','Hikuri','Peyotes','Hikurite','El <i>hikuri</i> (peyote) es una planta medicinal que se utiliza para el dolor de huesos.
Plural: <i>Hikurite</i> (peyote).','Images/Wixarika/Planta_Medicinal/Peyote','161,162');
INSERT INTO `diarios_viajes` VALUES (96,14,14,'RECOLECTA','PLANTA_MEDICINAL','CURATIVA',NULL,'Planta para dolor de cabeza o cuerpo cortado','Tɨpina huaki','Planta para dolor de cabeza o cuerpo cortado','Tɨpina huaki','El <i>tɨpina huaki</i> es una planta medicinal que se utiliza para el dolor de cabeza.
Plural: <i>Tɨpina huaki</i>.','Images/Wixarika/Planta_Medicinal/Planta para dolor de cabeza o cuerpo cortado','163,164');
INSERT INTO `diarios_viajes` VALUES (97,15,15,'RECOLECTA','PLANTA_MEDICINAL','CURATIVA',NULL,'Planta para dolor de estómago o empacho','Kɨpaixa (Xuriya kwitayari)','Planta para dolor de estómago o empacho','Yuriepakwiniya','La <i>kɨpaixa</i> es una planta medicinal que se utiliza para el empacho y el dolor de estómago.
Plural: <i>Yuriepakwiniya</i>.','Images/Wixarika/Planta_Medicinal/Planta para dolor de estómago o empacho','165,166');
INSERT INTO `diarios_viajes` VALUES (98,16,16,'RECOLECTA','PLANTA_MEDICINAL','CURATIVA',NULL,'Planta para dolor de estómago o empacho','Mutsirixa','Planta para dolor de estómago o empacho','Mutsirixate','La <i>mutsirixa</i> es una planta medicinal que se utiliza para el empacho y el dolor de estómago.
Plural: <i>Mutsirixate</i>.','Images/Wixarika/Planta_Medicinal/Planta para dolor de estómago o empacho','167,168');
INSERT INTO `diarios_viajes` VALUES (99,17,17,'RECOLECTA','PLANTA_MEDICINAL','CURATIVA',NULL,'Planta para huesos rotos','Hapani','Planta para huesos rotos','Hapanite','El <i>hapani</i> es una planta medicinal que se utiliza para los huesos rotos.
Plural: <i>Hapanite</i>.','Images/Wixarika/Planta_Medicinal/Planta para huesos rotos','169,170');
INSERT INTO `diarios_viajes` VALUES (100,18,18,'RECOLECTA','PLANTA_MEDICINAL','PINTAR','Miiki','Planta para pintar el rostro','Uxa','Plantas para pintar el rostro','Uxáte','La <i>uxa</i> es una planta que se utiliza para pintar el rostro.
Plural: <i>Uxáte</i>.','Images/Wixarika/Planta_Medicinal/Uxa','171,172');
INSERT INTO `diarios_viajes` VALUES (101,19,19,'RECOLECTA','PLANTA_MEDICINAL','CURATIVA','Kieri','Tepehuaje','Ɨpá','Tepehuajes','Haxi wexu','El <i>ɨpá</i> (tepehuaje) es una planta medicinal que se utiliza para el dolor de oído.
Plural: <i>Haxi wexu</i> (tepehuajes).','Images/Wixarika/Planta_Medicinal/Tepehuaje','173,174');
INSERT INTO `diarios_viajes` VALUES (102,20,20,'RECOLECTA','PLANTA_MEDICINAL','CURATIVA',NULL,'Tomillo',NULL,'Tomillos',NULL,'El tomillo es una planta medicinal que se utiliza como antiséptico.','Images/Wixarika/Planta_Medicinal/Tomillo','175');
INSERT INTO `diarios_viajes` VALUES (103,1,1,'CAZA','ANIMAL','SILVESTRE',NULL,'Abeja','Xiete','Abejas','Xietexi','<i>Xiete</i> (abeja).
Plural: <i>Xietexi</i> (abejas).','Images/Wixarika/Enfermedades/Piquete de abeja','176,177');
INSERT INTO `diarios_viajes` VALUES (104,2,2,'CAZA','ANIMAL','ESPIRITUAL',NULL,'Águila','Kwixɨ','Águilas','Kwixɨri','El <i>kwixɨ</i> (águila) es considerada como un animal espiritual.
Plural: <i>Kwixɨri</i> (águilas).','Images/Wixarika/Diario_Viaje/Águila','178,179');
INSERT INTO `diarios_viajes` VALUES (105,3,3,'CAZA','ANIMAL','ESPIRITUAL','Charcas','Águila real','Werika','Águilas reales','Werikaxi','El <i>werika</i> (águila real) es considerada como un animal espiritual.
Plural: <i>Werikaxi</i> (águilas reales).
','Images/Wixarika/Diario_Viaje/Águila','180,181');
INSERT INTO `diarios_viajes` VALUES (106,4,4,'CAZA','ANIMAL','SILVESTRE',NULL,'Alacrán','Teruka',NULL,NULL,'<i>Teruka</i> (alacrán).','Images/Wixarika/Animales/Alacran/Alacran 1','182');
INSERT INTO `diarios_viajes` VALUES (107,5,5,'CAZA','ANIMAL','SILVESTRE',NULL,'Araña','Tuuká','Arañas','Tuukatsi','<i>Tuuká</i> (araña).
Plural: <i>Tuukatsi</i> (arañas).
','Images/Wixarika/Animales/Araña/Araña f3','183,184');
INSERT INTO `diarios_viajes` VALUES (108,6,6,'CAZA','ANIMAL','SILVESTRE',NULL,'Ardilla','Tekɨ','Ardillas','Tekɨri','<i>Tekɨ</i> (ardilla).
Plural: <i>Tekɨri</i> (ardillas).
','Images/Wixarika/Animales/Ardilla/Ardilla 1','185,186');
INSERT INTO `diarios_viajes` VALUES (109,7,7,'CAZA','ANIMAL','SILVESTRE',NULL,'Armadillo','Xɨye','Armadillos','Xɨyetsi','<i>Xɨye</i> (armadillo).
Plural: <i>Xɨyetsi</i> (armadillos).
','Images/Wixarika/Diario_Viaje/Hoja dorada','187,188');
INSERT INTO `diarios_viajes` VALUES (110,8,8,'CAZA','ANIMAL','RELIGIOSO',NULL,'Bagre','Ketsɨ','Bagres','Ketsɨte','La sangre de <i>ketsɨ</i> (bagre) se utiliza en los rituales sagrados.
Plural: <i>Ketsɨte</i> (bagres).
','Images/Wixarika/Animales/Pez/Pez 9','189,190');
INSERT INTO `diarios_viajes` VALUES (111,10,10,'CAZA','ANIMAL','COSTUMBRE',NULL,'Camaleón cornudo','Téka','Camaleónes cornudos','Tekaxi','<i>Téka</i> (camaleón cornudo).
Plural: <i>Tekaxi</i> (camaleones cornudos).
','Images/Wixarika/Diario_Viaje/Hoja dorada','191,192');
INSERT INTO `diarios_viajes` VALUES (112,11,11,'CAZA','ANIMAL','SILVESTRE',NULL,'Cangrejo','Rharhapai','Cangrejos',NULL,'<i>Rharhapai</i> (cangrejo).','Images/Wixarika/Diario_Viaje/Hoja dorada','193');
INSERT INTO `diarios_viajes` VALUES (113,12,12,'CAZA','ANIMAL','SILVESTRE',NULL,'Caracol','Curupo','Caracoles',NULL,'<i>Curupo</i> (caracol).','Images/Wixarika/Vegetacion/Plantas de Agua/Caracol 1x','194');
INSERT INTO `diarios_viajes` VALUES (114,13,13,'CAZA','ANIMAL','DOMESTICA',NULL,'Cerdo','Tuixu','Cerdos','Tuixuri','<i>Tuixu</i> (cerdo).
Plural: <i>Tuixuri</i> (cerdos).
','Images/Wixarika/Animales/Cerdo/Cerdo 1','195,196');
INSERT INTO `diarios_viajes` VALUES (115,14,14,'CAZA','ANIMAL','SILVESTRE',NULL,'Cocodrilo','Ha’axi','Cocodrilos','Haxitsi','<i>Ha’axi</i> (cocodrilo).
Plural: <i>Haxitsi</i> (cocodrilos).
','Images/Wixarika/Animales/Cocodrilo/Cocodrilo 1 x1','197,198');
INSERT INTO `diarios_viajes` VALUES (116,15,15,'CAZA','ANIMAL','SILVESTRE',NULL,'Conejo','Tátsiu','Conejos','Tatsiurixi','<i>Tátsiu</i> (conejo).
Plural: <i>Tatsiurixi</i> (conejos).
','Images/Wixarika/Diario_Viaje/Conejo','199,200');
INSERT INTO `diarios_viajes` VALUES (117,16,16,'CAZA','ANIMAL','SILVESTRE',NULL,'Coyote','Yáavi','Coyotes','Yáavixi','<i>Yáavi</i> (coyote).
Plural: <i>Yáavixi</i> (coyotes).
','Images/Wixarika/Animales/Coyote/Coyote 1','201,202');
INSERT INTO `diarios_viajes` VALUES (118,17,17,'CAZA','ANIMAL','SILVESTRE',NULL,'Cuervo','Kwatsa','Cuervos','Kwatsári','<i>Kwatsa</i> (cuervo).
Plural: <i>Kwatsári</i> (cuervos).
','Images/Wixarika/Diario_Viaje/Hoja dorada','203,204');
INSERT INTO `diarios_viajes` VALUES (119,18,18,'CAZA','ANIMAL','DOMESTICA',NULL,'Gato','Mitsu','Gatos','Mitsúri','<i>Mitsu</i> (gato).
Plural: <i>Mitsúri</i> (gatos).','Images/Wixarika/Diario_Viaje/Hoja dorada','205,206');
INSERT INTO `diarios_viajes` VALUES (120,19,19,'CAZA','ANIMAL','SILVESTRE',NULL,'Gato montés','Mitsu yeutanaka','Gatos montés','Mitsuri yeutari','<i>Mitsu yeutanaka</i> (gato montés).
Plural: <i>Mitsúri yeutari</i> (gatos monteses).
','Images/Wixarika/Diario_Viaje/Hoja dorada','207,208');
INSERT INTO `diarios_viajes` VALUES (121,20,20,'CAZA','ANIMAL','DOMESTICA',NULL,'Güilota','Weurai','Güilotas','Weuraixi','<i>Weurai</i> (güilota).
Plural: <i>Weuraixi</i> (güilotas).
','Images/Wixarika/Animales/Güilota/Guilota 6','209,210');
INSERT INTO `diarios_viajes` VALUES (122,21,21,'CAZA','ANIMAL','ESPIRITUAL',NULL,'Halcón','Witse','Halcónes','Witseri','El <i>witse</i> (halcón) es considerado como un animal espiritual.
Plural: <i>Witseri</i> (halcones).
','Images/Wixarika/Diario_Viaje/Hoja dorada','211,212');
INSERT INTO `diarios_viajes` VALUES (123,22,22,'CAZA','ANIMAL','SILVESTRE',NULL,'Iguana','Ke’etsé','Iguanas','Ketse’ete','<i>Ke’etsé</i> (iguana).
Plural: <i>Ketse’ete</i> (iguanas).
','Images/Wixarika/Iconos/Lagartija','213,214');
INSERT INTO `diarios_viajes` VALUES (124,23,23,'CAZA','ANIMAL','SILVESTRE',NULL,'Jabalí','Tuixuyeutanaka','Jabalís','Tuixuriyeutari','<i>Tuixuyeutanaka</i> (jabalí).
Plural: <i>Tuixuriyeutari</i> (jabalís).
','Images/Wixarika/Animales/Jabali/Jabalí 1 x1','215,216');
INSERT INTO `diarios_viajes` VALUES (125,24,24,'CAZA','ANIMAL','SILVESTRE',NULL,'Jaguar','Tɨwe','Jaguares','Tɨwexi','<i>Tɨwe</i> (jaguar).
Plural: <i>Tɨwexi</i> (jaguares).
','Images/Wixarika/Animales/Lince/Puma 1','217,218');
INSERT INTO `diarios_viajes` VALUES (126,25,25,'CAZA','ANIMAL','ESPIRITUAL','Fresnillo','Lagartija','Ɨkwi','Lagartijas','Ɨkwixi','La <i>Ɨkwi</i> (lagartija) es considerada como un animal espiritual.
Plural: <i>Ɨkwixi</i> (lagartija).
','Images/Wixarika/Animales/Lagartija/Lagartija 1','219,220');
INSERT INTO `diarios_viajes` VALUES (127,26,26,'CAZA','ANIMAL','MENSAJERO','Santiago Ixcuintla','Lechuza','Miikɨiri','Lechuzas','Mikɨri','La <i>miikɨiri</i> (lechuza) es considerada como un animal mensajero.
Plural: <i>Mikɨri</i> (lechuzas).
','Images/Wixarika/Animales/Buho/Lechuza 1','221,222');
INSERT INTO `diarios_viajes` VALUES (128,27,27,'CAZA','ANIMAL','SILVESTRE',NULL,'Lince','Untsa','Linces','Untsari','<i>Untsa</i> (lince).
Plural: <i>Untsari</i> (linces).','Images/Wixarika/Animales/Lince/Puma 1','223,224');
INSERT INTO `diarios_viajes` VALUES (129,28,28,'CAZA','ANIMAL','SILVESTRE',NULL,'Lobo','Ɨrawe','Lobos','Ɨrawetsixi','<i>Ɨrawe</i> (lobo).
Plural: <i>Ɨrawetsixi</i> (lobos).
','Images/Wixarika/Animales/Lobo/Lobo 1','225,226');
INSERT INTO `diarios_viajes` VALUES (130,39,39,'CAZA','ANIMAL','MENSAJERO',NULL,'Tecolote enano','Peexá','Tecolotes enanos','Peexátsi','<i>Peexá</i> (Tecolote enano).
Plural: <i>Peexátsi</i> (Tecolotes enanos).','Images/Wixarika/Diario_Viaje/Hoja dorada','227,228');
INSERT INTO `diarios_viajes` VALUES (131,31,31,'CAZA','ANIMAL','ESPIRITUAL',NULL,'Perro','Tsɨkɨ','Perros','Tsɨikɨri','<i>Tsɨkɨ</i> (perro).
Plural: <i>Tsɨikɨri</i> (perros).
','Images/Wixarika/Diario_Viaje/Hoja dorada','229,230');
INSERT INTO `diarios_viajes` VALUES (132,32,32,'CAZA','ANIMAL','SILVESTRE',NULL,'Pez','Ketsɨ','Peces','Ketsíte','<i>Ketsɨ</i> (pez).
Plural: <i>Ketsíte</i> (peces).
','Images/Wixarika/Animales/Pez/Pez 9','231,232');
INSERT INTO `diarios_viajes` VALUES (133,33,33,'CAZA','ANIMAL','DOMESTICA',NULL,'Pollo','Wakana','Pollos','Wakanari','<i>Wakana</i> (pollo).
Plural: <i>Wakanari</i> (pollos).
','Images/Wixarika/Animales/Gallina/Gallina 1 x1','233,234');
INSERT INTO `diarios_viajes` VALUES (134,34,34,'CAZA','ANIMAL','SILVESTRE',NULL,'Puma','Máye','Pumas','Mayetsi','<i>Máye</i> (puma).
Plural: <i>Mayetsi</i> (pumas).
','Images/Wixarika/Animales/Lince/Puma 1','235,236');
INSERT INTO `diarios_viajes` VALUES (135,35,35,'CAZA','ANIMAL','ESPIRITUAL',NULL,'Salamandra','Imukwi','Salamandras','Imukwixi','<i>Imukwi</i> (salamandra).
Plural: <i>Imukwixi</i> (salamandras).
','Images/Wixarika/Diario_Viaje/Hoja dorada','237,238');
INSERT INTO `diarios_viajes` VALUES (136,36,36,'CAZA','ANIMAL','SILVESTRE',NULL,'Serpiente','Kúu','Serpientes','Kuterixi','<i>Kúu</i> (serpiente).
Plural: <i>Kuterixi</i> (serpientes).
','Images/Wixarika/Animales/Serpiente/Serpiente 1','239,240');
INSERT INTO `diarios_viajes` VALUES (137,37,37,'CAZA','ANIMAL','ESPIRITUAL','Durango','Serpiente azul','Haikɨ','Serpientes azules','Haikɨxi','La <i>Haikɨ</i> (serpiente azul) es considerada como un animal espiritual.
Plural: <i>Haikɨxi</i> (serpientes azules).
','Images/Wixarika/Animales/Serpiente/Serpiente 1','241,242');
INSERT INTO `diarios_viajes` VALUES (138,38,38,'CAZA','ANIMAL','ESPIRITUAL',NULL,'Serpiente cascabel','Xáye','Serpientes de cascabel','Xayetsi','<i>Xáye</i> (serpiente de cascabel).
Plural: <i>Xayetsi</i> (serpientes de cascabel).
','Images/Wixarika/Animales/Serpiente/Serpiente 1','243,244');
INSERT INTO `diarios_viajes` VALUES (139,39,39,'CAZA','ANIMAL','MENSAJERO',NULL,'Tecolote','Mikɨri','Tecolotes','Mikɨrixi','El <i>Mikɨri</i> (tecolote) es considerado como un animal mensajero.
Plural: <i>Mikɨrixi</i> (tecolotes).
','Images/Wixarika/Diario_Viaje/Hoja dorada','245,246');
INSERT INTO `diarios_viajes` VALUES (140,40,40,'CAZA','ANIMAL','DOMESTICA',NULL,'Vaca','Wakaxi','Vacas','Wakaitsixi','<i>Wakaxi</i> (vaca).
Plural: <i>Wakaitsixi</i> (vacas).','Images/Wixarika/Animales/Vaca/Vaca 5 x1','247,248');
INSERT INTO `diarios_viajes` VALUES (141,41,41,'CAZA','ANIMAL','DIVINA',NULL,'Venado','Maxa','Venados','Maxatsi','<i>Maxa</i> (venado).
Plural: <i>Maxatsi</i> (venados).
','Images/Wixarika/Animales/Venado/Venado 1','249,250');
INSERT INTO `diarios_viajes` VALUES (142,42,42,'CAZA','ANIMAL','ESPIRITUAL','Hewiiki','Zopilote','Wirɨkɨ','Zopilotes','Wirɨkɨxɨ','El <i>Wirɨkɨ</i> (zopilote) es considerado como un animal espiritual.
Plural: <i>Wirɨkɨxɨ</i> (zopilotes).
','Images/Wixarika/Diario_Viaje/Hoja dorada','251,252');
INSERT INTO `diarios_viajes` VALUES (143,43,43,'CAZA','ANIMAL','MENSAJERO','Bolaños','Zorro','Kauxai','Zorros','Kauxaitsi','El <i>Kauxai</i> (zorro) es considerada como un animal mensajero.
Plural: <i>Kauxaitsi</i> (zorros).
','Images/Wixarika/Diario_Viaje/Hoja dorada','253,254');
INSERT INTO `diarios_viajes` VALUES (144,5,41,'ARMA','ARMA',NULL,NULL,'Antorcha','Utsí','Antorchas','Utsi taiyari','<i>Utsí</i> (antorcha).
Plural: <i>Utsi taiyari</i> (antorchas).
','Images/Wixarika/Escenario/Objetos/Antorcha/Antorcha 1 3x (1)','255,256');
INSERT INTO `diarios_viajes` VALUES (145,4,23,'ARMA','ARMA',NULL,NULL,'Arco','Tɨɨpi','Arcos','Tɨɨpite','El <i>tɨɨpi</i> (arco) se utiliza para la caza del venado cola blanca.
Plural: <i>Tɨɨpite</i> (arcos).
','Images/Wixarika/Armas/Arco','257,258');
INSERT INTO `diarios_viajes` VALUES (146,8,42,'ARMA','ARMA',NULL,NULL,'Azadón','Hatsaruni','Azadones','Hatsarunite','<i>Hatsaruni</i> (azadón).
Plural: <i>Hatsarunite</i> (azadones).
','Images/Wixarika/Armas/Azadón D1','259,260');
INSERT INTO `diarios_viajes` VALUES (147,3,26,'ARMA','ARMA',NULL,NULL,'Cuchillo','Nawaxa','Cuchillos','Nawaxate','<i>Nawaxa</i> (cuchillo)!
Plural: <i>Nawaxate</i> (cuchillos).
','Images/Wixarika/Armas/Cuchillo C1','261,262');
INSERT INTO `diarios_viajes` VALUES (148,2,29,'ARMA','ARMA',NULL,NULL,'Cuña','Mɨtsɨtɨari','Cuñas','Mɨtsɨtɨarite','<i>Mɨtsɨtɨari</i> (cuña)!
Plural: <i>Mɨtsɨtɨarite</i> (cuñas).
','Images/Wixarika/Armas/Cuña 1','263,264');
INSERT INTO `diarios_viajes` VALUES (149,10,43,'ARMA','ARMA',NULL,NULL,'Escalera','Imúmui','Escaleras','Imúmuite','<i>Imúmui</i> (escalera)!
Plural: <i>Imúmuite</i> (escaleras).
','Images/Wixarika/Escenario/Objetos/Escalera 1 3x','265,266');
INSERT INTO `diarios_viajes` VALUES (150,9,20,'ARMA','ARMA',NULL,NULL,'Flecha','Ɨ’rɨ','Flechas','Ɨ’rɨte','Las <i>ɨ’rɨte</i> (flechas) se utilizan para la cacería del venado cola blanca y para luchar contra las fuerzas de la oscuridad.
Plural: <i>Ɨ’rɨte</i> (flechas).','Images/Wixarika/Armas/Flechas pack','267,268');
INSERT INTO `diarios_viajes` VALUES (151,6,4,'ARMA','ARMA',NULL,NULL,'Hacha','Ha’tsa','Hachas','Ha’tsate','<i>Ha’tsa</i> (hacha).
Plural: <i>Ha’tsate</i> (hachas).
','Images/Wixarika/Armas/Hacha 2','269,270');
INSERT INTO `diarios_viajes` VALUES (152,7,5,'ARMA','ARMA',NULL,NULL,'Machete','Kutsira','Machetes','Kutsirate','<i>Kutsira</i> (machete).
Plural: <i>Kutsirate</i> (machetes).
','Images/Wixarika/Armas/Machete 2','271,272');
INSERT INTO `diarios_viajes` VALUES (153,11,6,'ARMA','ARMA',NULL,NULL,'Malla de cacería','Winiyari','Mallas de cacería','Winiyarite','<i>Winiyari</i> (malla de cacería).
Plural: <i>Winiyarite</i> (mallas de cacería).
','Images/Wixarika/Armas/Malla de cacería','273,274');
INSERT INTO `diarios_viajes` VALUES (154,1,7,'ARMA','ARMA',NULL,NULL,'Palo','Kɨyé','Palos','Kɨyéxi','<i>Kɨyé</i> (palo).
Plural: <i>Kɨyéxi</i> (palos).
','Images/Wixarika/Armas/Palo','275,276');
INSERT INTO `diarios_viajes` VALUES (155,12,44,'ARMA','ARMA',NULL,NULL,'Petaca','Kiriwa','Petacas','Kiriwate','<i>Kiriwa</i> (petaca).
Plural: <i>Kiriwate</i> (petacas).','Images/Wixarika/Diario_Viaje/Hoja dorada','277,278');
INSERT INTO `diarios_viajes` VALUES (156,13,45,'ARMA','ARMA',NULL,NULL,'Red de pesca','Wipí','Redes de pesca','Wipiite','<i>Wipí</i> (red de pesca).
Plural: <i>Wipiite</i> (redes de pesca).
','Images/Wixarika/Armas/Red 1 1x','279,280');
INSERT INTO `diarios_viajes` VALUES (157,14,46,'ARMA','ARMA',NULL,NULL,'Roca','Ha’úte','Rocas','Ha’utete','<i>Ha’úte</i> (roca).
Plural: <i>Ha’utete</i> (rocas).
','Images/Wixarika/Vegetacion/Roca 4','281,282');
INSERT INTO `diarios_viajes` VALUES (158,15,47,'ARMA','ARMA',NULL,NULL,'Soga/Cuerda','Kaunari','Sogas/Cuerdas','Kaunarite','<i>Kaunari</i> (soga).
Plural: <i>Kaunarite</i> (sogas).
','Images/Wixarika/Escenario/Puentes/Puente soga t2 3x','283,284');
INSERT INTO `diarios_viajes` VALUES (159,16,48,'ARMA','ARMA',NULL,NULL,'Tijera','Tixikame','Tijeras','Tixikamete','<i>Tixikamete</i> (tijeras).
Plural: <i>Tixikame</i> (tijera).
','Images/Wixarika/Diario_Viaje/Hoja dorada','285,286');
INSERT INTO `diarios_viajes` VALUES (160,1,1,'VISITA','SITIO_IMPORTANTE','SITIO_SAGRADO','Del Nayar','Mesa del Nayar',NULL,NULL,NULL,'Mesa del Nayar es una comunidad serrana, considerada como un lugar sagrado donde se colocan ofrendas para las deidades.
Aquí habitan comunidades <i>Wixárika</i>.','Images/Wixarika/Diario_Viaje/Mesa del Nayar','287,288');
INSERT INTO `diarios_viajes` VALUES (161,2,2,'VISITA','SITIO_IMPORTANTE','SITIO_SAGRADO','Del Nayar','Santa Teresa',NULL,NULL,NULL,'Santa Teresa es una comunidad serrana, considerada como un lugar sagrado donde se dejan ofrendas para las deidades.
Aquí habitan comunidades <i>Wixárika</i>.','Images/Wixarika/Diario_Viaje/Santa Teresa','289,290');
INSERT INTO `diarios_viajes` VALUES (162,3,3,'VISITA','SITIO_IMPORTANTE','SITIO_SAGRADO','La Yesca','Laguna de Guadalupe Ocotán',NULL,NULL,NULL,'La Laguna de Guadalupe Ocotán es considerada como un lugar sagrado donde se colocan ofrendas para las deidades.','Images/Wixarika/Diario_Viaje/Guadalupe Ocotán','291');
INSERT INTO `diarios_viajes` VALUES (163,4,4,'VISITA','SITIO_IMPORTANTE','SITIO_SAGRADO','SAMAO','Laguna de Santa María del Oro',NULL,NULL,NULL,'La Laguna de Santa María del Oro es considerada como un lugar especial donde se dejan ofrendas para las deidades.
Está ubicada dentro de un cráter volcánico, rodeada por cerros y bosques.','Images/Wixarika/Diario_Viaje/SAMAO','292,293');
INSERT INTO `diarios_viajes` VALUES (164,5,5,'VISITA','SITIO_IMPORTANTE','SITIO_SAGRADO','Tepic','Potrero de Palmita',NULL,NULL,NULL,'Potrero de Palmita es una comunidad <i>Wixárika</i> ubicada a la orilla de la presa de Agua Milpa, es considerada como un lugar sagrado donde se colocan ofrendas para las deidades.','Images/Wixarika/Diario_Viaje/Potrero de palmita','294');
INSERT INTO `diarios_viajes` VALUES (165,6,6,'VISITA','SITIO_IMPORTANTE','SITIO_SAGRADO','Santiago Ixcuintla','Isla de Mexcaltitán',NULL,NULL,NULL,'La Isla de Mexcaltitán es un sitio turístico, considerado como un lugar especial donde se dejan ofrendas para las deidades.','Images/Wixarika/Diario_Viaje/Isla de Mexcaltitan','295,296');
INSERT INTO `diarios_viajes` VALUES (166,7,7,'VISITA','SITIO_IMPORTANTE','SITIO_SAGRADO','Santiago Ixcuintla','Río Santiago',NULL,NULL,NULL,'El Río Santiago es considerado como un lugar especial donde se colocan ofrendas para las deidades.
Nace en Ocotlán, Jalisco y desemboca en el mar de San Blas, Nayarit.','Images/Wixarika/Diario_Viaje/Río Santiago','297,298');
INSERT INTO `diarios_viajes` VALUES (167,8,8,'VISITA','SITIO_IMPORTANTE','SITIO_SAGRADO','San Blas','Haramara',NULL,NULL,NULL,'<i>Haramara</i> es uno de los sitios sagrados más importantes de la cosmogonía <i>Wixárika</i>.
La roca blanca <i>Waxiewe</i> (blanco vapor) dentro del mar, representa la forma física de <i>Tatei Haramara</i> (diosa madre del mar), y es el lugar donde habita. La piedra más pequeña es llamada <i>Cuca Wima</i>.','Images/Wixarika/Diario_Viaje/Haramara','299,300');
INSERT INTO `diarios_viajes` VALUES (168,10,10,'VISITA','SITIO_IMPORTANTE','SITIO_SAGRADO','Valparaíso','Tuapurie',NULL,NULL,NULL,'<i>Tuapurie</i> es un lugar sagrado donde se dejan ofrendas para las deidades.','Images/Wixarika/Diario_Viaje/Sitios sagrados','301');
INSERT INTO `diarios_viajes` VALUES (169,11,11,'VISITA','SITIO_IMPORTANTE','SITIO_SAGRADO','Valparaíso','Xurahue Muyaca',NULL,NULL,NULL,'<i>Xurahue Muyaca</i> es un lugar especial donde se colocan ofrendas para las deidades.','Images/Wixarika/Diario_Viaje/Sitios sagrados','302');
INSERT INTO `diarios_viajes` VALUES (170,12,12,'VISITA','SITIO_IMPORTANTE','COMUNIDAD','Fresnillo','Plateros',NULL,NULL,NULL,'Plateros es una localidad donde descansan los <i>wixaritari</i> durante el camino de su peregrinación a <i>Wirikuta</i>.','Images/Wixarika/Diario_Viaje/Sitios sagrados','303');
INSERT INTO `diarios_viajes` VALUES (171,13,13,'VISITA','SITIO_IMPORTANTE','SITIO_SAGRADO','Zacatecas','Makuipa (Cerro del Padre)',NULL,NULL,NULL,'<i>Makuipa</i> (Cerro del Padre) es un lugar sagrado donde se dejan ofrendas para las deidades.
Aquí durmió una noche <i>Tamatzi Kauyumari</i> (Nuestro hermano mayor Venado Azul), durante su peregrinación a <i>Wirikuta</i>.','Images/Wixarika/Diario_Viaje/Sitios sagrados','304,305');
INSERT INTO `diarios_viajes` VALUES (172,15,15,'VISITA','SITIO_IMPORTANTE','SITIO_SAGRADO','Villa de Ramos','Huahuatsari',NULL,NULL,NULL,'<i>Huahuatsari</i> es un lugar especial donde se dejan ofrendas para las deidades.','Images/Wixarika/Diario_Viaje/Sitios sagrados','306');
INSERT INTO `diarios_viajes` VALUES (173,16,16,'VISITA','SITIO_IMPORTANTE','SITIO_SAGRADO','Villa de Ramos','Cuhixu Uheni',NULL,NULL,NULL,'<i>Cuhixu Uheni</i> es un lugar sagrado donde se dejan ofrendas para las deidades.','Images/Wixarika/Diario_Viaje/Sitios sagrados','307');
INSERT INTO `diarios_viajes` VALUES (174,17,17,'VISITA','SITIO_IMPORTANTE','SITIO_SAGRADO','Villa de Ramos','Tatei Matiniere',NULL,NULL,NULL,'<i>Tatei Matiniere</i> es un lugar especial donde se dejan ofrendas para las deidades.','Images/Wixarika/Diario_Viaje/Sitios sagrados','308');
INSERT INTO `diarios_viajes` VALUES (175,18,18,'VISITA','SITIO_IMPORTANTE','SITIO_SAGRADO','Santo Domingo','Maxa Yapa',NULL,NULL,NULL,'<i>Maxa Yapa</i> es un lugar sagrado donde se dejan ofrendas para las deidades.','Images/Wixarika/Diario_Viaje/Sitios sagrados','309');
INSERT INTO `diarios_viajes` VALUES (176,19,19,'VISITA','SITIO_IMPORTANTE','SITIO_SAGRADO','Charcas','Tuy Mayau',NULL,NULL,NULL,'<i>Tuy Mayau</i> es un lugar especial donde se dejan ofrendas para las deidades.','Images/Wixarika/Diario_Viaje/Sitios sagrados','310');
INSERT INTO `diarios_viajes` VALUES (177,20,20,'VISITA','SITIO_IMPORTANTE','SITIO_SAGRADO','Charcas','Huacuri Quitenie',NULL,NULL,NULL,'<i>Huacuri Quitenie</i> es un lugar sagrado donde se dejan ofrendas para las deidades.','Images/Wixarika/Diario_Viaje/Sitios sagrados','311');
INSERT INTO `diarios_viajes` VALUES (178,21,21,'VISITA','SITIO_IMPORTANTE','SITIO_SAGRADO','Real de Catorce','Wirikuta',NULL,NULL,NULL,'<i>Wirikuta</i> es uno de los sitios sagrados más importantes de la cosmogonía <i>Wixárika</i>, aquí es donde ocurrió la creación del mundo y por donde se levanta el sol.','Images/Wixarika/Diario_Viaje/Wirikuta','312');
INSERT INTO `diarios_viajes` VALUES (179,23,23,'VISITA','SITIO_IMPORTANTE','COMUNIDAD','Mezquital','San Antonio de Padua',NULL,NULL,NULL,'San Antonio de Padua es una localidad donde viven pobladores <i>Wixárika</i>.','Images/Wixarika/Diario_Viaje/Sitios sagrados','313');
INSERT INTO `diarios_viajes` VALUES (180,24,24,'VISITA','SITIO_IMPORTANTE','COMUNIDAD','Pueblo Nuevo','San Bernardino de Milpillas',NULL,NULL,NULL,'San Bernardino de Milpillas es una localidad donde residen pobladores <i>Wixárika</i>.','Images/Wixarika/Diario_Viaje/Sitios sagrados','314');
INSERT INTO `diarios_viajes` VALUES (181,25,25,'VISITA','SITIO_IMPORTANTE','COMUNIDAD','Durango','Cinco de Mayo',NULL,NULL,NULL,'Cinco de Mayo es una localidad donde se encuentran asentamientos <i>Wixárika</i>.','Images/Wixarika/Diario_Viaje/Sitios sagrados','315');
INSERT INTO `diarios_viajes` VALUES (182,26,26,'VISITA','SITIO_IMPORTANTE','SITIO_SAGRADO','Canatlán','Hauxa Manaka',NULL,NULL,NULL,'<i>Hauxa Manaka</i> es uno de los sitios sagrados más importantes de la cosmogonía <i>Wixárika</i>, es la casa de Tututzi Maxa Kwaxi (Nuestro Bisabuelo Cola de Venado). Y es aquí donde Tayau (Nuestro padre sol) regresa a su lugar de origen dando inicio nuevamente a la oscuridad (la noche).','Images/Wixarika/Diario_Viaje/Hauxa Manaka','316');
INSERT INTO `diarios_viajes` VALUES (183,28,28,'VISITA','SITIO_IMPORTANTE','COMUNIDAD','Huejiquilla','Colonia Hatmasie',NULL,NULL,NULL,'Colonia <i>Hatmasie</i> es una localidad donde habitan comunidades <i>Wixárika</i>.','Images/Wixarika/Diario_Viaje/Sitios sagrados','317');
INSERT INTO `diarios_viajes` VALUES (184,29,29,'VISITA','SITIO_IMPORTANTE','SITIO_SAGRADO','Mezquitic','Te’akata',NULL,NULL,NULL,'<i>Te’akata</i> es uno de los sitios sagrados más importantes de la cosmogonía <i>Wixárika</i>, es el centro del universo, donde tuvo lugar la gesta universal, pues ahí los antepasados arrojaron un niño enfermo al fuego para que se transformara en Tayau (Nuestro padre sol).','Images/Wixarika/Diario_Viaje/Teakata','318');
INSERT INTO `diarios_viajes` VALUES (185,30,30,'VISITA','SITIO_IMPORTANTE','COMUNIDAD','Mezquitic','Santa Catarina Cuexcomatitlán',NULL,NULL,NULL,'Santa Catarina Cuexcomatitlán es una localidad donde se encuentrancomunidades <i>Wixárika</i>.','Images/Wixarika/Diario_Viaje/Sitios sagrados','319');
INSERT INTO `diarios_viajes` VALUES (186,31,31,'VISITA','SITIO_IMPORTANTE','COMUNIDAD','Mezquitic','San Sebastián Teponahuaxtlán',NULL,NULL,NULL,'San Sebastián Teponahuaxtlán es una localidad donde habitan comunidades <i>Wixárika</i>.','Images/Wixarika/Diario_Viaje/Sitios sagrados','320');
INSERT INTO `diarios_viajes` VALUES (187,32,32,'VISITA','SITIO_IMPORTANTE','COMUNIDAD','Bolaños','Tuxpan de Bolaños',NULL,NULL,NULL,'Tuxpan de Bolaños es una localidad donde se encuentran comunidades <i>Wixárika</i>.','Images/Wixarika/Diario_Viaje/Sitios sagrados','321');
INSERT INTO `diarios_viajes` VALUES (188,33,33,'VISITA','SITIO_IMPORTANTE','SITIO_SAGRADO','Chapala','Xapawiyemeta',NULL,NULL,NULL,'<i>Xapawiyemeta</i> es uno de los sitios sagrados más importantes de la cosmogonía <i>Wixárika</i>, representa el paso de una absoluta oscuridad a un amanecer.','Images/Wixarika/Diario_Viaje/Xapawiyemeta','322');
INSERT INTO `diarios_viajes` VALUES (189,1,7,'CONVERSA','AUTORIDAD',NULL,'Del Nayar','Chamán','Chamanes','Mara´kame','Mara’kate','El <i>mara’kame</i> (chamán) se encarga de sanar el cuerpo, mente y corazón de los <i>wixaritari</i>, dirige las ceremonias sagradas y es una autoridad en las comunidades. Tiene la misión de conservar y mantener vivas las tradiciones <i>Wixárika</i>.
Plural: <i>Mara’kate</i> (chamanes).','Images/Wixarika/Diario_Viaje/Marakame','323,324');
INSERT INTO `diarios_viajes` VALUES (190,2,8,'CONVERSA','AUTORIDAD',NULL,'Valparaíso','Jicarero','Jicareros','Xuku’uri ɨkame','Xuku’uri ɨkate','El <i>xuku’uri ɨkame</i> (jicarero) se encarga de cuidar el <i>tukipa</i> (centro ceremonial), de realizar las <i>neixa</i> (fiestas ceremoniales) y las peregrinaciones. El cargo de <i>xuku’uri ɨkame</i> (jicarero) se hereda de los abuelos a nietos.
Plural: <i>Xuku’uri ɨkate</i> (jicareros).','Images/Wixarika/Diario_Viaje/Jicarero','325,326');
INSERT INTO `diarios_viajes` VALUES (191,3,9,'CONVERSA','AUTORIDAD',NULL,'Villa de Ramos','Anciano sabio','Ancianos sabios','Kawiteru','Kawiterutsixi','El <i>kawiteru</i> (anciano sabio) se encarga de guiar el buen camino de las comunidades <i>Wixárika</i>, de soñar y elegir a los nuevos funcionarios del gobierno comunal. Es la autoridad suprema en las comunidades.
Plural: <i>Kawiterutsixi</i> (ancianos sabios).','Images/Wixarika/Diario_Viaje/Anciano','327,328');
INSERT INTO `diarios_viajes` VALUES (192,4,10,'CONVERSA','AUTORIDAD',NULL,'Mezquital','Policía','Policías','Tupiri','Tupiritsixi','El <i>tupiri</i> (policía) se encarga de hacer cumplir las leyes comunales y de ser el mensajero de la comunidad.
Plural: <i>Tupiritsixi</i> (policías).','Images/Wixarika/Diario_Viaje/Policia','329,330');
INSERT INTO `diarios_viajes` VALUES (193,5,11,'CONVERSA','AUTORIDAD',NULL,'Huejiquilla','Gobernador','Gobernadores','Tatuwani','Tatuwanitsixi','El <i>tatuwani</i> (gobernador) se encarga de dirigir las comunidades y es parte del gobierno comunal. Es elegido por los <i>kawiterutsixi</i> (ancianos sabios).
Plural: <i>Tatuwanitsixi</i> (gobernadores).','Images/Wixarika/Diario_Viaje/Gobernador','331,332');
INSERT INTO `diarios_viajes` VALUES (194,6,12,'CONVERSA','AUTORIDAD',NULL,'Miiki','Peyotero','Peyoteros','Hikuritame','Hikuritamete','El <i>hikuritame</i> (peyotero) se encarga de recolectar el peyote durante la peregrinación a <i>Wirikuta</i>.
Plural: <i>Hikuritamete</i> (peyoteros).','Images/Wixarika/Diario_Viaje/Jicarero','333,334');
INSERT INTO `diarios_viajes` VALUES (195,1,13,'CONVERSA','DEIDAD',NULL,'SAMAO','Nuestro hermano mayor Venado Azul','Tamatzi Kauyumari',NULL,NULL,'<i>Tamatzi Kauyumari</i> (Nuestro hermano mayor Venado Azul), marca el camino sagrado durante a la peregrinación a <i>Wirikuta</i> y se sacrifica para renacer en <i>hikuri</i> (peyote) que alimenta tu alma e <i>ikú</i> (maíz) que alimenta tu cuerpo.','Images/Wixarika/Diario_Viaje/Venado azul','335');
INSERT INTO `diarios_viajes` VALUES (196,2,14,'CONVERSA','DEIDAD',NULL,'San Blas','Nuestra madre el mar','Tatei Haramara',NULL,NULL,'<i>Tatei Haramara</i> (Nuestra madre el mar), da origen a las nubes y a la lluvia al chocar contra la roca blanca <i>Waxiewe</i> (blanco vapor) para convertirse en vapor.
El cielo es su cabello adornado de nubes y pájaros, el mar su vestido azul y la espuma de las olas es el encaje que lo adorna.','Images/Wixarika/Diario_Viaje/Tatei Haramara','336,337');
INSERT INTO `diarios_viajes` VALUES (197,3,15,'CONVERSA','DEIDAD',NULL,'Valparaíso','Nuestra madre águila','Tatei Wexica Wimari',NULL,NULL,'<i>Tatei Wexica Wimari</i> (Nuestra madre águila), es la hija de Tatewari (Nuestro abuelo fuego) y Tukutzi (Diosa de la tierra).','Images/Wixarika/Diario_Viaje/Águila','338');
INSERT INTO `diarios_viajes` VALUES (198,4,16,'CONVERSA','DEIDAD',NULL,'Zacatecas','Dios del fuego primigenio','Naɨrɨ',NULL,NULL,'<i>Naɨrɨ</i> (Dios del fuego primigenio), es una deidad divina destronada, que retorna momentáneamente al poder durante <i>namawita neixa</i> (fiesta del solsticio de verano), noche en la que se celebra el derrumbe de los pilares cósmicos y el retorno al caos original.','Images/Wixarika/Diario_Viaje/Abuelo fuego','339');
INSERT INTO `diarios_viajes` VALUES (199,5,-1,'CONVERSA','DEIDAD',NULL,'Zacatecas','Diosa de la tierra','Takutsi Nakawé',NULL,NULL,'<i>Takutsi Nakawé</i> (Diosa de la tierra), es una deidad divina destronada, que retorna momentáneamente al poder durante <i>namawita neixa</i> (fiesta del solsticio de verano), noche en la que se celebra el derrumbe de los pilares cósmicos y el retorno al caos original.','Images/Wixarika/Diario_Viaje/Hoja dorada','340');
INSERT INTO `diarios_viajes` VALUES (200,6,17,'CONVERSA','DEIDAD',NULL,'Santo Domingo','Nuestra madre agua sagrada','Tatei Kutsaraɨpa',NULL,NULL,'<i>Tatei Kutsaraɨpa</i> (Nuestra madre agua sagrada), es la deidad del agua, se encuentra en Yoliátl, cerca de Salinas, San Luis Potosí. <i>Kutsaraɨpa</i> significa lugar mítico donde se reunieron los antepasados.','Images/Wixarika/Diario_Viaje/Hoja dorada','341');
INSERT INTO `diarios_viajes` VALUES (201,7,18,'CONVERSA','DEIDAD',NULL,'Real de Catorce','Nuestro bisabuelo Cola de venado','Tututzi Maxa Kwaxi',NULL,NULL,'<i>Tututzi Maxa Kwaxi</i> (Nuestro bisabuelo Cola de venado), es la deidad suprema, representa al universo mismo, es la esencia que se encuentra en cualquier substancia material o inmaterial.','Images/Wixarika/Diario_Viaje/Hoja dorada','342');
INSERT INTO `diarios_viajes` VALUES (202,8,19,'CONVERSA','DEIDAD',NULL,'Real de Catorce','Nuestro padre el Sol','Tayau',NULL,NULL,'<i>Tayau</i> (Nuestro padre el Sol), es la divinidad del sol que nace en <i>Wirikuta</i> y muere en <i>Hauxa Manaka</i>. Durante el día viaja por el cielo, se sienta en su silla de oro al medio día.','Images/Wixarika/Diario_Viaje/Tayau','343');
INSERT INTO `diarios_viajes` VALUES (203,9,20,'CONVERSA','DEIDAD',NULL,'Canatlán','Nuestra madre tierra','Tatei Yurienáka',NULL,NULL,'<i>Tatei Yurienáka</i> (Nuestra madre tierra), es la “madre tierra”, responsable de la fertilidad del suelo. Es representada con un cántaro de barro.','Images/Wixarika/Deidades/Tatei Yurienáka 3x','344');
INSERT INTO `diarios_viajes` VALUES (204,10,21,'CONVERSA','DEIDAD',NULL,'Mezquitic','Nuestro abuelo fuego','Tatewari',NULL,NULL,'<i>Tatewari</i> (Nuestro abuelo fuego), es un antepasado divinizado, una de las deidades más antiguas. Se le considera como el primer <i>mara’kame</i> (chamán) y como “el gran transformador”.','Images/Wixarika/Deidades/Tatewari 3x','345');
INSERT INTO `diarios_viajes` VALUES (205,11,22,'CONVERSA','DEIDAD',NULL,'Chapala','Diosa madre del sur','Tatei Xapawiyeme',NULL,NULL,'<i>Tatei Xapawiyeme</i> (Diosa madre del sur), es la “madre lluvia”, que se transforma en nubes, lluvia, manantiales, truenos, rayos y hasta en cristales de roca.','Images/Wixarika/Diario_Viaje/Hoja dorada','346');
INSERT INTO `diarios_viajes` VALUES (206,12,23,'CONVERSA','DEIDAD',NULL,'Kieri','Chupa sangre','T’kákame',NULL,NULL,'<i>Tukákame</i> (Chupa sangre), es un murciélago que representa a la muerte. Se adorna con los huesos de difuntos y le gusta hacer que las personas se pierdan para quitarles la vida.','Images/Wixarika/Diario_Viaje/Hoja dorada','347');
INSERT INTO `diarios_viajes` VALUES (207,1,24,'CONVERSA','OCUPACION',NULL,'Del Nayar','Agricultor','Muka’etsa','Agricultores','Memuka’etsa','El <i>muka’etsa</i> (agricultor) se encarga de cultivar los coamiles, un área de tierra donde se siembra maíz, frijoles, chiles, calabacitas y flores de cempasúchil.','Images/Wixarika/Diario_Viaje/Agricultor','348');
INSERT INTO `diarios_viajes` VALUES (208,2,25,'CONVERSA','OCUPACION',NULL,'La Yesca','Cazador','Tiweweiyame','Cazadores','Teweweiyamete','El <i>tiweweiyame</i> (cazador) se encarga de cazar animales, principalmente, para los rituales sagrados, y, en menor medida para la subsistencia alimenticia.','Images/Wixarika/Diario_Viaje/Cazador','349');
INSERT INTO `diarios_viajes` VALUES (209,3,26,'CONVERSA','OCUPACION',NULL,'SAMAO','Artesana','Titsatsaweme','Artesanos','Tetsatsawemete','El <i>titsatsaweme</i> (artesana) se encarga de elaborar los <i>kemarite</i> (trajes tradicionales Wixárika), artesanías de chaquira como <i>kuka tiwameté</i> (collares), <i>matsiwate</i> (pulseras) y <i>nakɨtsate</i> (aretes), y kɨtsiɨrite (morrales) de estambre.','Images/Wixarika/Diario_Viaje/Artesano','350');
INSERT INTO `diarios_viajes` VALUES (210,4,27,'CONVERSA','OCUPACION',NULL,'Tepic','Músico','Tiyuitɨwame','Músicos','Teyuitɨwamate','El <i>tiyuitɨwame</i> (músico) se encarga de interpretar la música tradicional <i>Wixárika</i>, que tiene tres géneros: <i>wawi</i> (canto sagrado), <i>xaweri</i> (música tradicional) y <i>teiwari</i> (música regional).','Images/Wixarika/Diario_Viaje/Música','351');
INSERT INTO `diarios_viajes` VALUES (211,5,43,'CONVERSA','OFRENDA',NULL,'San Blas','Ofrendas','Mawari','Ofrendas','Mawarite','Las <i>mawarite</i> (ofrendas) son objetos espirituales que dejamos en los <i>tukipa</i> (centros ceremoniales) y sitios sagrados para agradecer o hacer peticiones a nuestras deidades.','Images/Wixarika/Diario_Viaje/Ofrenda','352');
INSERT INTO `diarios_viajes` VALUES (212,6,43,'CONVERSA','OFRENDA',NULL,'San Blas','Ojo de dios','Tsikɨri','Ojos de dios','Tsik ɨrite','El <i>tsikɨri</i> (ojo de dios) es un objeto espiritual que simboliza los cinco puntos cardinales del <i>Wixárika</i>: norte, sur, este, oeste y centro. Se ofrenda a las deidades para pedir por el buen crecimiento de los niños y se considera como un escudo protector para los peregrinos.','Images/Wixarika/Diario_Viaje/Ofrenda','353');
INSERT INTO `diarios_viajes` VALUES (213,7,44,'CONVERSA','OFRENDA',NULL,'Zacatecas','Máscara','Tsikwaki nierikaya','Máscaras','Tsikwaki nierikaya','La <i>tsikwaki nierikaya</i> (máscara) es un objeto espiritual que representa un momento imprescindible en la cultura <i>Wixárika</i>: el contacto con el mestizo. Personifica a dioses asociados con los mestizos.','Images/Wixarika/Objetos_Espirituales/Máscara','354');
INSERT INTO `diarios_viajes` VALUES (214,8,45,'CONVERSA','OFRENDA',NULL,'Real de Catorce','Tablilla','Nierika','Tablillas de estambre','Nierikate','La <i>nierika</i> (tablilla de estambre) es un objeto espiritual que representa el mundo de las deidades y la cosmovisión del pueblo <i>Wixárika</i>, están inspiradas en sueños o visiones. Se utiliza para conocer el estado oculto o auténtico de las cosas.','Images/Wixarika/Objetos_Espirituales/Tablilla','355');
INSERT INTO `diarios_viajes` VALUES (215,9,-1,'CONVERSA','OFRENDA',NULL,'Canatlán','Jícara','Xuku’uri','Jícaras','Xukúrite','La <i>xukúri</i> (jícara) es un objeto espiritual que representa a la <i>’uka</i> (mujer), a lo femenino, es símbolo de depósito de vida pues en ella se transportan agua y semillas.','Images/Wixarika/Objetos_Espirituales/Jícara','356');
INSERT INTO `diarios_viajes` VALUES (216,10,-1,'CONVERSA','OFRENDA',NULL,'Chapala','Flecha','Ɨ’rɨ','Flechas','Ɨ’rɨte','La <i>ɨ’rɨ</i> (flecha) es un objeto espiritual que simboliza lo masculino, por ser un elemento empleado para la caza. Se cree que, las <i>ɨ ’rɨte</i> (flechas) son utilizadas por las deidades para cazar al <i>maxa</i> (venado) y luchar contra las fuerzas de la oscuridad.','Images/Wixarika/Objetos_Espirituales/Flecha','357');
INSERT INTO `diarios_viajes` VALUES (217,1,37,'CONVERSA','FESTIVIDAD',NULL,'Santiago Ixcuintla',NULL,NULL,'Fiestas ceremoniales','Neixa','Las <i>neixa</i> (fiestas ceremoniales) se realizan en los <i>tukipa</i> (centros ceremoniales) para celebrar el ciclo del cultivo de maíz.','Images/Wixarika/Diario_Viaje/Hoja dorada','358');
INSERT INTO `diarios_viajes` VALUES (218,3,40,'CONVERSA','FESTIVIDAD',NULL,'Canatlán','La danza de nuestras madres','Tatei neixa',NULL,NULL,'<i>Tatei neixa</i> (La danza de nuestras madres), celebra la cosecha de los primeros frutos, es para despedir a las lluvias, una vez que las milpas han crecido y los elotes están tiernos.','Images/Wixarika/Diario_Viaje/Hoja dorada','359');
INSERT INTO `diarios_viajes` VALUES (219,2,41,'CONVERSA','FESTIVIDAD',NULL,'Mezquitic','Fiesta del tambor','Fiesta del tepu',NULL,NULL,'La fiesta del <i>tepu</i> (tambor) es una celebración para los niños y niñas, en la que se hace una peregrinación imaginaria a <i>Wirikuta</i> con ayuda del <i>mara’kame</i> (chamán), para que el <i>kupuri</i> (aliento) se asiente en el cuerpo de los infantes.','Images/Wixarika/Diario_Viaje/Hoja dorada','360');
INSERT INTO `diarios_viajes` VALUES (220,1,96,'CONVERSA','CULTURA',NULL,'Del Nayar','Tradición oral',NULL,NULL,NULL,'La tradición oral son las diversas historias del pueblo <i>Wixárika</i>, donde se narra el origen de los <i>wixaritari</i>, las deidades y los elementos de la naturaleza.','Images/Wixarika/Diario_Viaje/Hoja dorada','361');
INSERT INTO `diarios_viajes` VALUES (221,1,97,'CONVERSA','CULTURA',NULL,'La Yesca','El camino del corazón','Nana’iyar',NULL,NULL,'El <i>nana’iyari</i> (el camino del corazón) son los lazos que vinculan a las diferentes comunidades del pueblo <i>Wixárika</i>. Debe ser vivido y reproducido cotidianamente a través de las costumbres.','Images/Wixarika/Diario_Viaje/Camino corazón','362');
INSERT INTO `diarios_viajes` VALUES (222,3,98,'CONVERSA','CULTURA',NULL,'SAMAO','La leyenda del Venado Azul',NULL,NULL,NULL,'La leyenda de <i>Tamatzi Kauyumari</i> (Nuestro hermano mayor Venado Azul) dice que, durante una época de sequía y hambre, los abuelos enviaron a cuatro jóvenes cazadores en busca de alimento. En el camino encontraron un <i>maxa</i> (venado) que los guio hasta la zona sagrada de <i>Wirikuta</i>, donde hallaron el <i>hikuri</i> (peyote), un alimento espiritual que les regresó la lluvia, comida y salud. Desde entonces, los <i>wixaritari</i> adoran al <i>maxa</i> (venado) que, al mismo tiempo es <i>hikuri</i> (peyote) y <i>ikú</i> (maíz).','Images/Wixarika/Diario_Viaje/Venado azul','363');
INSERT INTO `diarios_viajes` VALUES (223,4,99,'CONVERSA','CULTURA',NULL,'Tepic','Rituales Wixárika',NULL,NULL,NULL,'Los rituales <i>Wixárika</i> recrean las historias sobre los orígenes de los <i>wixaritari</i>, y deben realizarse permanentemente para garantizar la continuidad del pueblo <i>Wixárika</i>. Las peregrinaciones, las abstinencias y el consumo de <i>hikuri</i> (peyote) son parte esencial de los rituales que guían la vida de los <i>wixaritari</i>.','Images/Wixarika/Diario_Viaje/Ritual Wixárika','364');
INSERT INTO `diarios_viajes` VALUES (224,5,100,'CONVERSA','CULTURA',NULL,'Santiago Ixcuintla','Tukipa',NULL,NULL,NULL,'El <i>tukipa</i> (centro ceremonial) es un espacio dedicado al culto de las deidades, se compone de un gran templo llamado <i>tuki</i> y otros templos de menor tamaño conocidos como <i>xiriki</i>. En el <i>tukipa</i> (centro ceremonial) se realizan las <i>neixa</i> (fiestas ceremoniales).','Images/Wixarika/Diario_Viaje/Tukipa','365');
INSERT INTO `diarios_viajes` VALUES (225,6,101,'CONVERSA','CULTURA',NULL,'Valparaíso','Origen del peyote',NULL,NULL,NULL,'La leyenda del <i>hikuri</i> (peyote) dice que, <i>Tamatzi Kauyumari</i> (Nuestro hermano mayor Venado Azul), en su esfuerzo por levantar a <i>Tayau</i> (Nuestro padre el Sol) al cielo, perdió parte de sus cuernos, los cuales cayeron a la tierra y germinaron dando origen al <i>hikuri</i> (peyote). Desde entonces, el <i>hikuri</i> (peyote) quedó divinizado en la cultura <i>Wixárika</i>.','Images/Wixarika/Vegetacion/Peyote 1','366');
INSERT INTO `diarios_viajes` VALUES (226,7,102,'CONVERSA','CULTURA',NULL,'Valparaíso','Peregrinación a Wirikuta',NULL,NULL,NULL,'El pueblo <i>Wixárika</i> recrea las historias sobre su origen, según las cuales, los hombres surgieron del mar y emprendieron el viaje hacia <i>Wirikuta</i> para asistir al nacimiento del sol. Por lo cual, anualmente, realizan una peregrinación a <i>Wirikuta</i>, donde los peregrinos dejan <i>mawarite</i> (ofrendas) para las deidades.','Images/Wixarika/Diario_Viaje/Rombo Wixárika','367');
INSERT INTO `diarios_viajes` VALUES (227,8,103,'CONVERSA','CULTURA',NULL,'Fresnillo','El costumbre',NULL,NULL,NULL,'El <i>nana’iyari</i> (el costumbre) se constituye de diferentes elementos y características de la cultura <i>Wixárika</i>, así como de las peregrinaciones, por ello, es necesario vivirlo.','Images/Wixarika/Diario_Viaje/Hoja dorada','368');
INSERT INTO `diarios_viajes` VALUES (228,9,104,'CONVERSA','CULTURA',NULL,'Zacatecas','Peyote',NULL,NULL,NULL,'El <i>hikuri</i> (peyote) es un cactus sin espinas endémico de los desiertos del norte de México, que comúnmente crece debajo de matorrales. Sus flores son de color rosa pálido y tarda de 15 a 20 años en crecer, debido a este lento crecimiento y a la sobre-recolección se encuentra en peligro de extinción. Solamente los <i>wixaritari</i> pueden recolectarlo.','Images/Wixarika/Diario_Viaje/Wirikuta','369');
INSERT INTO `diarios_viajes` VALUES (229,10,105,'CONVERSA','CULTURA',NULL,'Zacatecas','Peregrinación a Wirikuta','',NULL,NULL,'Durante la peregrinación a <i>Wirikuta</i> se realiza la búsqueda de los antepasados, quienes transmiten el conocimiento por medio de visiones para que el mundo siga en equilibrio. También, se caza el <i>maxa</i> (venado), se recolecta el <i>hikuri</i> (peyote) y se transporta agua del mar hacia el desierto y viceversa, para invocar a la lluvia.','Images/Wixarika/Diario_Viaje/Rombo Wixárika','370');
INSERT INTO `diarios_viajes` VALUES (230,11,106,'CONVERSA','CULTURA',NULL,'Villa de Ramos','Los colores del maíz',NULL,NULL,NULL,'La leyenda de los colores del <i>ikú</i> (maíz) dice que, el pueblo <i>Wixárika</i> estaba cansado por la monotonía de su comida, por lo que, un joven decidió partir en búsqueda de un nuevo alimento. En su camino, encontró dificultades, así que se entristeció. Entonces, apareció la Madre del <i>ikú</i> (maíz), quien lo guio hasta donde había <i>ikú</i> (maíz) blanco, amarillo, rojo, morado y azul en abundancia. Tomó los cinco tipos de <i>ikú</i> (maíz) de colores y regreso a la comunidad para sembrarlos y cosecharlos.','Images/Wixarika/Diario_Viaje/Hoja dorada','371');
INSERT INTO `diarios_viajes` VALUES (231,12,107,'CONVERSA','CULTURA',NULL,'Santo Domingo','Peyote',NULL,NULL,NULL,'El <i>hikuri</i> (peyote) tiene una larga tradición de uso dentro de las comunidades <i>Wixárika</i>, esencialmente, con fines medicinales y rituales. Además, de su uso terapéutico, por sus propiedades alucinógenas, el <i>hikuri</i> (peyote) se convirtió en la medicina más potente para ahuyentar el mal o las influencias sobrenaturales.','Images/Wixarika/Diario_Viaje/Wirikuta','372');
INSERT INTO `diarios_viajes` VALUES (232,13,108,'CONVERSA','CULTURA',NULL,'Santo Domingo','Peregrinación a Wirikuta',NULL,NULL,NULL,'Antes de iniciar el viaje a <i>Wirikuta</i>, los peregrinos deben realizar una ceremonia de purificación, en la cual confiesan sus faltas cometidas. También, deben realizar abstinencias para estar en condiciones espirituales.','Images/Wixarika/Diario_Viaje/Rombo Wixárika','373');
INSERT INTO `diarios_viajes` VALUES (233,14,109,'CONVERSA','CULTURA',NULL,'Charcas','Peyote',NULL,NULL,NULL,'La vida del pueblo <i>Wixárika</i>gira en torno a un calendario de festividades, peregrinaciones, y ofrendas relacionadas con el <i>hikuri</i> (peyote). La celebración más importante es la peregrinación a <i Wirikuta</i>, donde se recolecta el <i>hikuri</i> (peyote) para todas las demás ceremonias.','Images/Wixarika/Diario_Viaje/Wirikuta','374');
INSERT INTO `diarios_viajes` VALUES (234,15,110,'CONVERSA','CULTURA',NULL,'Charcas','Peregrinación a Wirikuta',NULL,NULL,NULL,'Quienes hacen por primera vez la peregrinación a <i>Wirikuta</i> son nombrados <i>matewáme</i> (el que no sabe y va a saber). Deben llevar los ojos vendados durante el trayecto, una vez en <i>Wirikuta</i>, realizan varios rituales y duras pruebas físicas para acceder al <i>hikuri</i> (peyote) y estar en condiciones espirituales.','Images/Wixarika/Diario_Viaje/Rombo Wixárika','375');
INSERT INTO `diarios_viajes` VALUES (235,16,111,'CONVERSA','CULTURA',NULL,'Real de Catorce','Peregrinación a Wirikuta',NULL,NULL,NULL,'La peregrinación finaliza en el desierto de <i>Wirikuta</i>, en <i>Reunari</i> (Cerro Quemado), donde surgió <i>Tayau</i> (Nuestro padre el Sol). Ahí los peregrinos dejan <i>mawarite</i> (ofrendas), y se hace una ceremonia de representación del nacimiento de los ancestros. Por último, los peregrinos se dividen en grupos para viajar a los otros sitios sagrados.','Images/Wixarika/Diario_Viaje/Rombo Wixárika','376');
INSERT INTO `diarios_viajes` VALUES (236,17,112,'CONVERSA','CULTURA',NULL,'Mezquital','Número 5',NULL,NULL,NULL,'El número 5 es importante en la cosmogonía <i>Wixárika</i>, “todo se hace siempre cinco veces”. Son 5 los lugares sagrados, 5 las direcciones del universo, 5 las facetas de <i>Tamatzi Kauyumari</i> (Nuestro hermano mayor Venado Azul), 5 las diosas del maíz y 5 cazadores cósmicos.','Images/Wixarika/Diario_Viaje/Número cinco','377');
INSERT INTO `diarios_viajes` VALUES (237,18,113,'CONVERSA','CULTURA',NULL,'Pueblo Nuevo','Cazadores cósmicos',NULL,NULL,NULL,'Los cinco cazadores cósmicos son: <i>Utútawi</i>: el <i>maye</i> (puma) cazador del norte, <i>Wewetsári</i>: el <i>tuwe</i> (jaguar) cazador del sur, <i>Tsipúrawi</i>: el <i>ɨxawe</i> (lobo) cazador del oeste, <i>Tutu Háuki</i>: el <i>kapɨwi</i> (lince) cazador del este, y <i>Tututáka Pitsitéka</i>: el <i>mitsu</i> (gato montés) cazador del centro del universo.','Images/Wixarika/Diario_Viaje/Cazadores','378');
INSERT INTO `diarios_viajes` VALUES (238,19,115,'CONVERSA','CULTURA',NULL,'Durango','Facetas del venado azul',NULL,NULL,NULL,'Las cinco facetas de <i>Tamatzi Kauyumari</i> (Nuestro hermano mayor Venado Azul) son: <i>Tamatzi Parietzika</i>: Dios del amanecer y la aurora, <i>Tamatzi Iriye</i>: Dios de los arqueros, <i>Tamatzi Eaka Teiwari</i>: Dios del viento, <i>Tamatzi Wakiri</i>: Dios del tepehuano, y <i>Tamatzi Kauyumari Maxa Yuavi</i>.','Images/Wixarika/Diario_Viaje/Hoja dorada','379');
INSERT INTO `diarios_viajes` VALUES (239,20,116,'CONVERSA','CULTURA',NULL,'Canatlán','Watakame',NULL,NULL,NULL,'El mito del viaje en canoa de <i>Watakame</i> dice que, un campesino, fue elegido para sobrevivir al gran diluvio en una canoa. <i>Watakame</i> llevó consigo los cinco colores del ikú (maíz), una brasa y a su tsɨkɨ (perra) negra. El viaje terminó en el sitio sagrado <i>Hawxa Manaka</i>, para iniciar todo de nuevo y dar origen al pueblo <i>Wixárika</i>.','Images/Wixarika/Deidades/Watakame 3x','380');
INSERT INTO `diarios_viajes` VALUES (240,21,117,'CONVERSA','CULTURA',NULL,'Huejiquilla','Direcciones del universo',NULL,NULL,NULL,'Las cinco direcciones del universo son representadas por deidades femeninas: <i>Tatei Jautze Kupuri</i>: Diosa madre del norte, <i>Tatei Rapawiyema</i>: Diosa madre del sur, <i>Tatei Sakaimuka</i>: Diosa madre del oeste, <i>Tatei Uwiutali</i>: Diosa madre del este, y <i>Tatei Kiewimuka</i>: Diosa de la lluvia del centro.','Images/Wixarika/Diario_Viaje/Direcciones del universo','381');
INSERT INTO `diarios_viajes` VALUES (241,22,118,'CONVERSA','CULTURA',NULL,'Mezquitic','Nuestro padre el sol',NULL,NULL,NULL,'La leyenda del <i>táu</i> (sol) dice que, antes no había en el mundo más luz que la de la luna, por lo que, los <i>wixaritari</i> le rogaron a la luna que enviará a su hijo, un joven cojo y tuerto. Al principio, la luna se opuso, pero al final accedió. Los <i>wixaritari</i> le pusieron un <i>kemari</i> (traje) y le dieron un <i>tɨɨpi</i> (arco) y <i>ɨ’rɨte</i> (flechas), luego lo arrojaron a un horno en <i>Te’akata</i> donde quedó consumido. A los cinco días, el joven resucitó como <i>Tayau</i> (Nuestro padre el Sol), apareció el sol, irradió su luz sobre la tierra y completó su primer viaje por el cielo.','Images/Wixarika/Escenario/Sol/sol','382');
INSERT INTO `diarios_viajes` VALUES (242,23,119,'CONVERSA','CULTURA',NULL,'Mezquitic','Historia del fuego',NULL,NULL,NULL,'La leyenda del <i>tái</i> (fuego) dice que, antes había seres monstruosos llamados <i>hewiixi</i> (caníbales), quienes no querían compartir el <i>tái</i> (fuego). Muchos animales intentaron conseguir el <i>tái</i> (fuego), pero, fue el tlacuache quien lo logró, al tomarlo rápidamente con su cola, por eso, la tiene pelada. El tlacuache compartió el <i>tái</i> (fuego) con las comunidades <i>Wixárika</i> de <i>Te’akata</i>, donde ahora habita <i>Tatewari</i> (Nuestro abuelo fuego).','Images/Wixarika/Diario_Viaje/Hoja dorada','383');
INSERT INTO `diarios_viajes` VALUES (243,24,120,'CONVERSA','CULTURA',NULL,'Chapala','Diosas del agua',NULL,NULL,NULL,'Las cinco diosas del agua, también llamadas `Madres de la lluvia` son: <i>Tatei Niaariwame</i>: lluvia del sur, <i>Tatei Yrameka</i> (Nuestra madre del retoño): lluvia del norte, <i>Tatei Kiewimuka</i> (Nuestra madre del venado): lluvia del oriente, <i>Tatei Matinieri</i>: lluvia del poniente, y <i>Tatei Aitzarika</i>: lluvia del centro.','Images/Wixarika/Diario_Viaje/Hoja dorada','384');
INSERT INTO `diarios_viajes` VALUES (244,25,121,'CONVERSA','CULTURA',NULL,'Miiki','Ceremonia de los muertos',NULL,NULL,NULL,'En la cultura <i>Wixárika</i> se honra a los difuntos de diferentes maneras: con el velorio, la despedida física, el camino al lugar de descanso y la despedida del alma. En el velorio se reúnen sus familiares, parientes y amigos para despedir al difunto. Luego llevan al difunto a su lugar de descanso y lo entierran hacia el poniente para que esté de frente al sol.','Images/Wixarika/Diario_Viaje/Hoja dorada','385');
INSERT INTO `diarios_viajes` VALUES (245,26,122,'CONVERSA','CULTURA',NULL,'Miiki','Muerte',NULL,NULL,NULL,'Los <i>wixaritari</i> consideran que quien muere recorre toda su vida. Después, su espíritu llega a un río donde encuentra un tsɨkɨ (perro), al que tiene que darle tortillas para evitar ser mordido mientras cruza. Al final, encuentra a los animales que dañó en vida, y si estos son sagrados le caerá una roca que lo aplastará. Cuando el difunto pasa las pruebas, se reúne con sus familiares difuntos y antepasados, que lo esperan con alegría y le hacen una fiesta.','Images/Wixarika/Diario_Viaje/Hoja dorada','386');
INSERT INTO `diarios_viajes` VALUES (246,27,123,'CONVERSA','CULTURA',NULL,'Hewiixi','Invocar o llamar al muerto','Mɨɨkí Kwevíxa',NULL,NULL,'La despedida del alma, es el <i>Mɨɨkí Kwevíxa</i> (invocar o llamar al muerto), una ceremonia que se realiza cinco días después del fallecimiento para que el difunto pueda despedirse de sus parientes. Ésta será su última partida. Sin embargo, la familia nunca pierde contacto con el difunto, pueden entrar a su <i>ririki</i> (pequeño santuario) para adorarlo y dirigirse a él.','Images/Wixarika/Diario_Viaje/Hoja dorada','387');
INSERT INTO `diarios_viajes` VALUES (247,28,124,'CONVERSA','CULTURA',NULL,'Hewiixi','Muerte',NULL,NULL,NULL,'Se dice que si alguien no alimentó o cuidó de su tsɨkɨ (perro) negro, cuando muera el tsɨkɨ (perro) va a estar esperándolo para morderlo. Pero si la persona cuido del tsɨkɨ (perro) entonces todo será diferente, éste le dará agua, comida y buenos deseos.','Images/Wixarika/Diario_Viaje/Hoja dorada','388');
INSERT INTO `diarios_viajes` VALUES (248,29,125,'CONVERSA','CULTURA',NULL,'Kieri','Ceremonia de los muertos',NULL,NULL,NULL,'En la cultura <i>Wixárika</i>, los antepasados divinizados, junto con las deidades, rigen la sociedad de los vivos. Si los <i>wixaritari</i> trasgreden las normas de convivencia, pueden ser castigados por las deidades y los parientes muertos, con enfermedades o desgracias, o bien, sus almas pueden quedar prisioneras eternamente en el inframundo.','Images/Wixarika/Diario_Viaje/Fiesta ceremonial','389');
INSERT INTO `diarios_viajes` VALUES (249,30,126,'CONVERSA','CULTURA',NULL,'Kieri','Muerte',NULL,NULL,NULL,'Cuando muere un bebé el rito funeral es diferente, pues el bebé es enterrado cerca de la casa para que no se pierda. En el lugar donde se entierra se pone comida y juguetes. El bebé sigue formando parte de la familia, y al llegar un hermano, el bebé se encargará de jugar con él, aun cuando se piense que el niño juega solo.','Images/Wixarika/Diario_Viaje/Hoja dorada','390');
INSERT INTO `diarios_viajes` VALUES (250,31,127,'CONVERSA','CULTURA','','Kieri','Muerte',NULL,NULL,NULL,'Los <i>wixaritari</i> consideran que con ayuda del <i>hikuri</i> (peyote) y la cuidadosa guía del <i>mara’kame</i> (chamán) pueden comunicarse con los mɨɨkite (muertos). Para esto, se induce a un sueño en el que puede pasar algo sagrado como convivir, danzar y reír con el ser querido que se ha ido.','Images/Wixarika/Diario_Viaje/Hoja dorada','391');
CREATE TRIGGER borrar_usuario after delete on usuarios begin
	delete from inventario_usuarios where usuario_id=old.usuario_id; 
end;
CREATE TRIGGER nuevo_usuario after insert on usuarios begin
	insert into extras_usuarios values(new.usuario_id,1,0);
	insert into extras_usuarios values(new.usuario_id,2,0);
	insert into extras_usuarios values(new.usuario_id,3,0);
	insert into extras_usuarios values(new.usuario_id,4,0);
	insert into extras_usuarios values(new.usuario_id,5,0);
	insert into extras_usuarios values(new.usuario_id,6,0);
	insert into extras_usuarios values(new.usuario_id,7,0);
	insert into extras_usuarios values(new.usuario_id,8,0);
	insert into extras_usuarios values(new.usuario_id,9,0);
	insert into extras_usuarios values(new.usuario_id,10,0);
	insert into extras_usuarios values(new.usuario_id,11,0);
	insert into extras_usuarios values(new.usuario_id,12,0);
	insert into extras_usuarios values(new.usuario_id,13,0);
	insert into extras_usuarios values(new.usuario_id,14,0);
	insert into extras_usuarios values(new.usuario_id,15,0);
	insert into extras_usuarios values(new.usuario_id,16,0);
	
	insert into registro_sesion(usuario_id,sesion_id,fecha_termino) values(new.usuario_id,-1,datetime('now','localtime'));
	
	insert into estados_desbloqueados values(new.usuario_id,1,1,1,0,0,0);
	insert into estados_desbloqueados values(new.usuario_id,1,2,0,0,0,0);
	insert into estados_desbloqueados values(new.usuario_id,1,3,0,0,0,0);
	insert into estados_desbloqueados values(new.usuario_id,1,4,0,0,0,0);
	insert into estados_desbloqueados values(new.usuario_id,1,5,0,0,0,0);
	insert into estados_desbloqueados values(new.usuario_id,1,6,0,0,0,0);
	insert into estados_desbloqueados values(new.usuario_id,1,7,0,0,0,0);
	
	insert into estados_desbloqueados values(new.usuario_id,2,8,0,0,0,0);
	insert into estados_desbloqueados values(new.usuario_id,2,9,0,0,0,0);
	insert into estados_desbloqueados values(new.usuario_id,2,10,0,0,0,0);
	insert into estados_desbloqueados values(new.usuario_id,2,11,0,0,0,0);
	
	insert into estados_desbloqueados values(new.usuario_id,3,12,0,0,0,0);
	insert into estados_desbloqueados values(new.usuario_id,3,13,0,0,0,0);
	insert into estados_desbloqueados values(new.usuario_id,3,14,0,0,0,0);
	insert into estados_desbloqueados values(new.usuario_id,3,15,0,0,0,0);
	insert into estados_desbloqueados values(new.usuario_id,3,16,0,0,0,0);
	
	insert into estados_desbloqueados values(new.usuario_id,4,17,0,0,0,0);
	insert into estados_desbloqueados values(new.usuario_id,4,18,0,0,0,0);
	insert into estados_desbloqueados values(new.usuario_id,4,19,0,0,0,0);
	insert into estados_desbloqueados values(new.usuario_id,4,20,0,0,0,0);
	insert into estados_desbloqueados values(new.usuario_id,4,21,0,0,0,0);
	
	insert into estados_desbloqueados values(new.usuario_id,5,22,0,0,0,0);
	insert into estados_desbloqueados values(new.usuario_id,5,23,0,0,0,0);
	insert into estados_desbloqueados values(new.usuario_id,5,24,0,0,0,0);
	insert into estados_desbloqueados values(new.usuario_id,5,25,0,0,0,0);
	insert into estados_desbloqueados values(new.usuario_id,5,26,0,0,0,0);
	
	insert into estados_desbloqueados values(new.usuario_id,6,27,0,0,0,0);
	insert into estados_desbloqueados values(new.usuario_id,6,28,0,0,0,0);
	insert into estados_desbloqueados values(new.usuario_id,6,29,0,0,0,0);
	insert into estados_desbloqueados values(new.usuario_id,6,30,0,0,0,0);
	
	insert into estrellas values(1,1,'JUEGO',new.usuario_id,0);
	insert into estrellas values(1,2,'JUEGO',new.usuario_id,0);
	insert into estrellas values(1,3,'JUEGO',new.usuario_id,0);
	insert into estrellas values(1,4,'JUEGO',new.usuario_id,0);
	insert into estrellas values(1,5,'JUEGO',new.usuario_id,0);
	insert into estrellas values(1,6,'JUEGO',new.usuario_id,0);
	insert into estrellas values(1,7,'JEFE',new.usuario_id,0);
	
	insert into estrellas values(2,8,'JUEGO',new.usuario_id,0);
	insert into estrellas values(2,9,'JUEGO',new.usuario_id,0);
	insert into estrellas values(2,10,'JUEGO',new.usuario_id,0);
	insert into estrellas values(2,11,'JEFE',new.usuario_id,0);
	
	insert into estrellas values(3,12,'JUEGO',new.usuario_id,0);
	insert into estrellas values(3,13,'JUEGO',new.usuario_id,0);
	insert into estrellas values(3,14,'JUEGO',new.usuario_id,0);
	insert into estrellas values(3,15,'JUEGO',new.usuario_id,0);
	insert into estrellas values(3,16,'JEFE',new.usuario_id,0);
	
	insert into estrellas values(4,17,'JUEGO',new.usuario_id,0);
	insert into estrellas values(4,18,'JUEGO',new.usuario_id,0);
	insert into estrellas values(4,19,'JUEGO',new.usuario_id,0);
	insert into estrellas values(4,20,'JUEGO',new.usuario_id,0);
	insert into estrellas values(4,21,'JEFE',new.usuario_id,0);
	
	insert into estrellas values(5,22,'JUEGO',new.usuario_id,0);
	insert into estrellas values(5,23,'JUEGO',new.usuario_id,0);
	insert into estrellas values(5,24,'JUEGO',new.usuario_id,0);
	insert into estrellas values(5,25,'JUEGO',new.usuario_id,0);
	insert into estrellas values(5,26,'JEFE',new.usuario_id,0);
	
	insert into estrellas values(6,27,'JUEGO',new.usuario_id,0);
	insert into estrellas values(6,28,'JUEGO',new.usuario_id,0);
	insert into estrellas values(6,29,'JUEGO',new.usuario_id,0);
	insert into estrellas values(6,30,'JEFE',new.usuario_id,0);

    insert into estrellas values(1,1,'QUIZ',new.usuario_id,0);
	insert into estrellas values(1,2,'QUIZ',new.usuario_id,0);
	insert into estrellas values(1,3,'QUIZ',new.usuario_id,0);
	insert into estrellas values(1,4,'QUIZ',new.usuario_id,0);
	insert into estrellas values(1,5,'QUIZ',new.usuario_id,0);
	insert into estrellas values(1,6,'QUIZ',new.usuario_id,0);
	insert into estrellas values(1,7,'JEFE',new.usuario_id,0);
	
	insert into estrellas values(2,8,'QUIZ',new.usuario_id,0);
	insert into estrellas values(2,9,'QUIZ',new.usuario_id,0);
	insert into estrellas values(2,10,'QUIZ',new.usuario_id,0);
	insert into estrellas values(2,11,'JEFE',new.usuario_id,0);
	
	insert into estrellas values(3,12,'QUIZ',new.usuario_id,0);
	insert into estrellas values(3,13,'QUIZ',new.usuario_id,0);
	insert into estrellas values(3,14,'QUIZ',new.usuario_id,0);
	insert into estrellas values(3,15,'QUIZ',new.usuario_id,0);
	insert into estrellas values(3,16,'JEFE',new.usuario_id,0);
	
	insert into estrellas values(4,17,'QUIZ',new.usuario_id,0);
	insert into estrellas values(4,18,'QUIZ',new.usuario_id,0);
	insert into estrellas values(4,19,'QUIZ',new.usuario_id,0);
	insert into estrellas values(4,20,'QUIZ',new.usuario_id,0);
	insert into estrellas values(4,21,'JEFE',new.usuario_id,0);
	
	insert into estrellas values(5,22,'QUIZ',new.usuario_id,0);
	insert into estrellas values(5,23,'QUIZ',new.usuario_id,0);
	insert into estrellas values(5,24,'QUIZ',new.usuario_id,0);
	insert into estrellas values(5,25,'QUIZ',new.usuario_id,0);
	insert into estrellas values(5,26,'JEFE',new.usuario_id,0);
	
	insert into estrellas values(6,27,'QUIZ',new.usuario_id,0);
	insert into estrellas values(6,28,'QUIZ',new.usuario_id,0);
	insert into estrellas values(6,29,'QUIZ',new.usuario_id,0);
	insert into estrellas values(6,30,'JEFE',new.usuario_id,0);
	
	insert into puntajes values(new.usuario_id,1,1,0);
	insert into puntajes values(new.usuario_id,1,2,0);
	insert into puntajes values(new.usuario_id,1,3,0);
	insert into puntajes values(new.usuario_id,1,4,0);
	insert into puntajes values(new.usuario_id,1,5,0);
	insert into puntajes values(new.usuario_id,1,6,0);
	insert into puntajes values(new.usuario_id,1,7,0);
	
	insert into puntajes values(new.usuario_id,2,8,0);
	insert into puntajes values(new.usuario_id,2,9,0);
	insert into puntajes values(new.usuario_id,2,10,0);
	insert into puntajes values(new.usuario_id,2,11,0);
	
	insert into puntajes values(new.usuario_id,3,12,0);
	insert into puntajes values(new.usuario_id,3,13,0);
	insert into puntajes values(new.usuario_id,3,14,0);
	insert into puntajes values(new.usuario_id,3,15,0);
	insert into puntajes values(new.usuario_id,3,16,0);
	
	insert into puntajes values(new.usuario_id,4,17,0);
	insert into puntajes values(new.usuario_id,4,18,0);
	insert into puntajes values(new.usuario_id,4,19,0);
	insert into puntajes values(new.usuario_id,4,20,0);
	insert into puntajes values(new.usuario_id,4,21,0);
	
	insert into puntajes values(new.usuario_id,5,22,0);
	insert into puntajes values(new.usuario_id,5,23,0);
	insert into puntajes values(new.usuario_id,5,24,0);
	insert into puntajes values(new.usuario_id,5,25,0);
	insert into puntajes values(new.usuario_id,5,26,0);
	
	insert into puntajes values(new.usuario_id,6,27,0);
	insert into puntajes values(new.usuario_id,6,28,0);
	insert into puntajes values(new.usuario_id,6,29,0);
	insert into puntajes values(new.usuario_id,6,30,0);
	
end;
CREATE TRIGGER poner_fecha_sesion after insert on registro_sesion begin
	update registro_sesion set fecha_inicio=datetime('now','localtime') where registro_sesion_id=new.registro_sesion_id; 
end;
CREATE VIEW vista_posicion_global as
	select u.usuario_id,nombres,apellido_paterno,apellido_materno,fecha_nacimiento,sexo,pais,estado,municipio,colonia,calles,escuela,grado,grupo,activo,ruta_imagen,puntos,estado_id,municipio_id,
		(SELECT COUNT(*) FROM (select usuario_id,estado_id,municipio_id,sum(puntos) as puntos from Puntajes group by usuario_id order by puntos desc) AS t2
		WHERE case when t2.puntos=t1.puntos then t1.usuario_id>=t2.usuario_id else t2.puntos >= t1.puntos end ) AS Posicion
		FROM (select usuario_id,estado_id,municipio_id,sum(puntos) as puntos from Puntajes group by usuario_id order by puntos desc) AS t1,usuarios as u where t1.usuario_id=u.usuario_id  order by puntos DESC;
COMMIT;

        ";

	public static string usuarios_default = @"
	insert into usuarios(usuario_id,usuario_server_id,usuario_local_id,contraseña,nombres,pais,estado,municipio,colonia,calles,escuela,grado,activo)
	values 
	(2,2,2,'2','Usuario 2','México','Nayarit','Tepic','Centro','Centro','Centro','1',1),
	(3,3,3,'3','Usuario 3','México','Nayarit','Tepic','Centro','Centro','Escuela 2','1',1),
	(4,4,4,'4','Usuario 4','México','Nayarit','Tepic','Centro','Centro','Centro','1',1),
	(5,5,5,'5','Usuario 5','México','Nayarit','Tepic','Centro','Centro','Escuela 2','2',1),
	(6,6,6,'6','Usuario 6','México','Nayarit','Tepic','Centro','Centro','Escuela 3','2',1),
	(7,7,7,'7','Usuario 7','México','Nayarit','Tepic','Valle','Centro','Escuela 3','3',1),
	(8,8,8,'8','Usuario 8','México','Nayarit','Tepic','Valle','Centro','Escuela 3','3',1),
	(9,9,9,'9','Usuario 9','México','Nayarit','Tepic','Valle','Centro','Escuela 2','1',1),
	(10,10,10,'10','Usuario 10','México','Nayarit','Tepic','Valle','Centro','Escuela 2','2',1),
	(11,11,11,'11','Usuario 11','México','Nayarit','Tepic','Valle','Centro','Escuela 6','3',1),
	(12,12,12,'12','Usuario 12','México','Nayarit','Tepic','Local','Centro','Escuela 2','2',1),
	(13,13,13,'13','Usuario 13','México','Nayarit','Tepic','Local','Centro','Centro','2',1),
	(14,14,14,'14','Usuario 14','México','Nayarit','Tepic','Local','Centro','Escuela 5','1',1),
	(15,15,15,'15','Usuario 15','México','Nayarit','Tepic','Local','Centro','Centro','2',1),
	(16,16,16,'16','Usuario 16','México','Nayarit','Tepic','Local','Centro','Escuela 2','3',1),
	(17,17,17,'17','Usuario 17','México','Nayarit','Tepic','Roble','Centro','Centro','2',1),
	(18,18,18,'18','Usuario 18','México','Nayarit','Tepic','Roble','Centro','Centro','1',1),
	(19,19,19,'19','Usuario 19','México','Nayarit','Tepic','Roble','Centro','Centro','4',1);

	update puntajes set puntos=abs(random()/100000000000000) where usuario_id!=1;
";

	public static string img_niño = "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";

	public static string img_niña = "iVBORw0KGgoAAAANSUhEUgAAA2oAAAPcCAMAAAAHZrhRAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAMAUExURQAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAALMw9IgAAAD/dFJOUwABAgMEBQYHCAkKCwwNDg8QERITFBUWFxgZGhscHR4fICEiIyQlJicoKSorLC0uLzAxMjM0NTY3ODk6Ozw9Pj9AQUJDREVGR0hJSktMTU5PUFFSU1RVVldYWVpbXF1eX2BhYmNkZWZnaGlqa2xtbm9wcXJzdHV2d3h5ent8fX5/gIGCg4SFhoeIiYqLjI2Oj5CRkpOUlZaXmJmam5ydnp+goaKjpKWmp6ipqqusra6vsLGys7S1tre4ubq7vL2+v8DBwsPExcbHyMnKy8zNzs/Q0dLT1NXW19jZ2tvc3d7f4OHi4+Tl5ufo6err7O3u7/Dx8vP09fb3+Pn6+/z9/usI2TUAAAAJcEhZcwAAFxEAABcRAcom8z8AAD41SURBVHhe7d11YBRHGwbwvbgbBAokaCC4BncProVi/dpCoVhxaYFipWiLW3GKBStQCO6EoqFI8ASS4EmIu/BdkrctRZPc7qzM8/uH20tyO3v3Puze7uyMAEpl7F7Do0r1Rl+N+27s2DHjZ+04ceRDTuz/acRYvfFDW9Xy8KhexYFeBQDeYGaXy7VU5Yq1OnTu9Fm/EcNHjp+7bMnipat2HDp48OCBw6duBNz/kIC7J7z1v3jw8N51yxYvXrp4yujhI4b06daxs2dNj4ql8zk6mdBqALhkbGJmbmltY2tvm6eGZ4e+436Zteq8r++t6FeieHn7ou+BX+fPnNCrTXuP/E72drbWVhbmZiZGtHYAbrhUatRxwI9zVh44c+Hazdv+wU+fhMTFxydSVAyWGBcf+eLZk+B7t25fvXjh6NZl00f16diwYgFaO4DGmdkVKlulVtMun38zacHanSdvPwilaEgs4fndS0d3rpk/sd/nnzapW7Ns0fy5zKlJAJqi0xNM8pVr/O0va/+4kkwJkEPSpUO/zx31VfuKpoKRUUazADTD2MWjVe/vl+89dOLczcBnLxOo6uURHxEWdOfapVN/bF65YESHuuVdqZEAKmbhmK9oaY8G3UbM2XjslmhfxMSSHHFl25KfRrStU7d29YruLo6m1GoAtbGv2HbQ7C2+sYkJiUnJqWlU4YqRlpaSlJiYEB/1IvDakZWj25c0o3YDqIZliUZdBs9cu+f45XvPk6iylSwlKvjaiR2rf10wdcT/mpdxpK0AUDBTW2eXomXbj1mx/1Yq1bGaxDw4vX5Cj/oVShTOn8vamLYJQIGKtxm54szT0PDoeDXszd6SlpwQExH6PPDK7rkDm+SnbQJQFOsitT8b8tOqvReD5TyZL5aI+2d/Xzpp2BfNyuel7QOQnc7c1tHJrenApRfiqFC1IvHW1indKxVwsrNCV0pQAPPGI1edvHE/ODSeKlQ7EiOePbx7dvOULiVpWwFkYVOibruefcZuvPCcSlObIm/snT+0a9MKOJYE5nRGJqamFvnqj91+M4rqUetCj87pWtTa1MQIfbmAIduy7b5ftf/EJf8wxXUCkUxU8A2f3fN7lsWVbmBCZ1ewYt12A2ZsvR5LJciV594ze7eoUhA3BoDUTF2bjNh8MzY5VXkdrZhIS01LfrRveAWckQQJmZfrNnrRrpPXn6VQ3fHq5fXDq8e0x0lJkICJo2txj45T999XZR8QKdzbMb6heyEben8ARFK068x9t8LiElP4PGx8h5SE6LAL8xvZ0hsEYDj7Ol9NXHvsTgTVGPwjxX/vwrHN0FUSRGDukKdMxyXXqbTgbVFbvvJwscdZEjCMWbURO689iuDn2ln2pUU9vf/7twXpDQPIgYJ1e05Yf56XziCGiDo7f0jHohhlEnLA2NzUrd+m+1RK8FHJvkPK2ZjhOBKyq1jPVefvh+HMftbFP7ywc2Sl9PcOfSQhiywK1f3fdG+ccMy21Evzu9bEtzbIKovKIw7jC1oOJR0fZIfdGnxERoWU/3rpEVxCy7kov71zPkXW4COcirecdobLDvtiitj9aXl3C3pLAd7B8dPtT+I57bEvorTk+HtrM06QALxD7uZj1pwMoWoBAz3+46fP0WEL3maep1TPDciZmFLOfONewJLeXwBSefzZ5zFqHI1YwRIiri2pS+8vQDrrqv1XX+X9bk9JPNw+urULvcvAOxMLx2Zzg6g0QGzJ+/sWssdsUqBXrO9uv1Ds0iQT/ejPaXXovQZ+mRVp8eMp3CEjsVtr+9fKTe848Mmy0pjzuGDNQLJXR2t01+JYrWlHApE0JsKubP8S50c4ZVu6/RJ/KgRg4MSohgUwNyKHLJouCEhGFyyWYn3HFkHWeGNSfeaRQKoAYCXh7raBpekTAC5YFmkxN4w+fmDqwuRa+TG7BjdMGqy+l4ROWLJIjrk/sTx9DqBxFjVG7X5EHzzI4OaavqUxtpb2Gds0nIegyezmpGq5EDYtS7+EWmja+XAcO8osMerCqCKZnwloVOF2s2/hBL8SXJzp6az/QNCBRJN0guug0/RJg9wSznYtgPFZNcq86+Y78fRBg+ziAta2x2l/LbKv9NW+aPqUQRHCt/2vJKbU1hwjz5W4aK00qU9XVMLgIxpTbPSh5/T5goI8PzamIn1EoAX2lcbfoM8WFObGNA9r+phA9Ww9TydhRAOlenmsPX1OoHaVxxzDiUcFiz4xvpIZLrGpn3nFGXfpMwWFCpte154+LlCvnkdfoH+I0sUeHWhLnxeokP6IxKjMiONx9HGCgsWdHlWOPjZQI6sKk2LoswSFe/xzYfQdUS3H/+3DZWu1SA050QoDj6iUe/9DCfQ5ghrs+rwwTkOqkK7gnHv0EYJK3B9clD49UA+Llmv9cQuoyqTeXdmOPj9QizydNkXS5wdqkfbqVeiWxrjCpiq5Ol+ljw/UJeb85/i+piIlxl5Ioo8OVCbhxPB89DGC0llWHIN+/Crm168ozvqrQ/sdmD5ezZLD52POGjVw7LT9ZcY3bFCtgF/qWNHHCUqlc+h8HH2xVC9+UQNkTeFMx/+FpGlA/L4W6OqvZLqSg33pswJ1i97W1o4+VVAeM7fJ6F6sGVta4hhSsdzXPqaPCdQv9kBLawyDoEjWNafiJL+WxG3sid7HSmRVb30ifUagETd+zE2fLiiE/ijDsb33S/qAQCtSb4wriWNIhbHt4EUfD2jJjdHoEKksdm0O0mcD2uL3E7KmGPrjC/uO+6PQGUuTUm58i3MjyuHcSX/0iKBp1LmBuL6mFK4/+OLuNA073MKGPmmQlUXLeVdTsFPTsPCd6KOlACYO9XzoEwHN2oQ+WvKrPdsPs8xoXvz+FhjcR1YmJbr/GkCfBmiW/rtBrFczI/rQQQaWpWcHY6hHTiwuixH9ZePx3ZEnOBnCi6eritPnDmwZ5W/283X6FIAHz8eXos8emCo4+BbmTeNLwjj67IGhT7qs8MM88ry5NDQXff7AiE2Jr/dhNif+pN1oh24j7OgEwbrxrqe4lsaj2INdcPcaQ9VG/hGuf9tx7pFD0RvrWlAZgMSMTMrNf0jvO/DnycoKVAkgsTxDTjzHVWuOvRjrRqUAUnJoMOUSgsYx/beGi0NxakR6lm0201sO/PKpi6xJrezcc+nnQ4BvCRdaUkGANJzqzXmkf6Nx4hE2NXeiogAJOLW5QG80cO+vNo5UFiC6GlMvYOhiIMkXptSgwgBx2daehztA4W/6LxEB82pj+jUp9D4bSe8yQIbIs72pOEAcOkEwqTTlHLo8whsSzowtQUUC4rCtMQNBg3d4OracNRUJiMD+s0NIGrxLauyeFlbo6S+WUgMP4GZreI/ond+UpEIBw5i6/XiD3lWAd/CbiLkOReG5/WEyvacA75D8wKsuFQvknFPnLTH0jgK8R+y6lug6YiD7Vjh4hCzwa4XJMwxSZuJVHDxCFiSeHVuWigayz7r6lDv0TgJ8xLUpVTDkSE613IyuWJBl4cvrU+FA9jh23hpCbyJAFjze1NmSigeyIV83b+zTIOvSXr2K8m6Drv7ZZjH1DnqIQDbF3Z6C72vZY1Rm+F/07gFkw9XhmBYqO8zLTQ7C+HOQA2lBowtjusOsK+0VistpkCPJLzaXpjKCj6o1L4zeN4BsC5tXC/fUZIlFycX0ngHkyCJ3nBz5GP3/Rs6dD7ykdwwgR55ubW6L20U/xrH7Hnq/AHIqbkMnDMj6EY6djtG7BWCAY51wV8376Xf4ubudxIVrEEHcyW65cQz5fg7ddmFAfhCBvoh2dXOgsoK3OHU6Te8UgMFO4/va++TufgRHjyCauCPdMbzPOzl02YOjRxCNvpT2dMEx5Dvk6uyDnIGo0nw656Lygn/k6rEfEzqByBL390DW3uDQ2ZveHQAReXfGMeR/5O5yFvs0kEDi2S44N/Iapx57cHsaSCJ1Tw+c8/+HY6cj9L4AiO5IJxxDkjyfncb1NJBM/Km2GPk4g0nn3bieBpLRl9Z6TxMqNq4Zu+2k9wRAGsm/FUfWBKHGmhf0hgBI5MmaGlRu/DKvNCuF3g4AyaTMrsD5DTUmJTF7GrAQt9qKao5T7tMe0VsBIKmA4YWo6HhkZDcgEIePwERq8ACOT/k7TfdD0oCRlJs/mvA6AkKurlfpXQBgwLc1r/u1tt7R9B4AMBDt3ZZKjy8mjdYjacBU7IpqxlR+HDGq8Gsw+mMBQ/pi8/+lJNUfR8qMxxQYwFzIt4WpALlhPeImbTwAQ38NsaES5IRjh+O06QBMHWvL191rdW/jWxrIIv5cTSpC7dMJQqP1SBrIJHZpHX4G83danUCbDcBc6AJuDiFdR/vRRgPIwLffJ1SKGmfX4xJ2aiCj2FMduZjA17zFZtpiAJmsqG1G5ahlxTeG0/YCyCRsdV4qRw0rPjKQNhdANg/6u1BBapZRn3tJtLUAskm6150qUqtMPb0wYjgoQNrGxqZUlNpUbHkQbSqArIKXa7rjcYGvkDRQiIedHKkstaibD8YSAYVIPvYplaX2GBVeS1sJoABrXI2oNLXG5VccPoKC3J2dj0pTY+w/u0+bCKAEKde7aLPjcYsNuKIGipK8sQUVp6ZYz42gDQRQiIi5tlSeGpL7i0u0eQCKcb6b9s74N7iGw0dQnOhTtalANcN9Cqa7BgWKnOROJaoRloPO0qYBKMrZQZZUpJqgc/OmDQNQGG83zYzpo98Q95nBtF0AChM8sxRVqgYYfxGAvo+gUCkBPalO1c+82UbaKgAFml/ahEpV7RwW4fARFOz2NHsqVZUzqn6DNglAkfyqauPMSI2lobRFAIr0YmZFKlZVsxj2EOdEQNGSbn2jgTvXLGtupe0BUKx1pdQ/BqvzEtwPCop35ycnKljVMmmC2UFB+ZIv1lH7mZGa86JoYwAULHJKeSpZtRodiCFWQQXS7g2nklUnk+LbaEsAFG5rEWMqWzUq+AMmLQSV8B2i5vGz6vni1mtQiaijKp6PPve3tBUAyhc1wJkKV30+24t+IqAayXu7UeGqTr7lL9NoKwCUL3yhSqfIztXuCm0CgCr4VFRn96wqm0JoCwBU4fkadfbw7xGMw0dQlbTHahz7wLjkHGo/gGrMKam+sQ9Mh56n1gOoxvmh6pshu8COeGo9gGrE73ChAlYNh1a4eQZU6GYrtU25Vn1FGLUdQEXCVlanElaLPg+Tqe0AKpL8sA+VsDqYFF1ALQdQmQVF1XQS0qY/Jp4BlTrb34bKWA2K7MYwB6BSUbuLUBmrgE1rf2o2gOr4t1XPBNnV50dTqwFUJ3Kqekb0GXgXN1+DasUd7UqFrHimv1CbAdQoeYZKemdZ1d5LTQZQJe86VlTMylZ4AU6KgKr5LyhMxaxsVTAgHahb0kVV3CJq2SORGgygUqGfWVM5K1njlbh7BlQufmVjKmcl+/4BRqQDlUu5NYLKWcGst1BrAVRspeLHqXNq40NtBVCxY/WV3jurxC8Pqa0AKub/oxuVtFLVPxtLbQVQsdg/61FJK5POvDc6GoMmRCt7AH/jaoupoQAqN91dyVMbmo+8hBGNQRPSTvRT8gD+Rmtx+Ro0ImalEZW1EhU5Sc0EUL1TCh74wPXrO9RKANW709eVClt5am4IpVYCqN6zXz2osJWnKwY6AO2Iu9SeCltpdOZjqY0AmjDGTEfFrSym5ZZRCwE0YXk5ZZ7vt/nqKLUQQBOOfKnMgY4dN0dQCwE04ekKJypuZXH3pQYCaMQZRQ7nY98Wt8+AxvjXV+KXtSqzQqh9ABoRMk2JQ2d1PoU71UBjYk93pvJWkqHh6NQPGpMWPpTKW0FsfqbWAWjIHMWd7resuY3aBqAh22pYUokrRd4xONUPGnR5dB4qcaUotCOM2gagIaHblXZprQLmxABN8itHJa4QFu2eU8sANOWZwkbvd/8eg9KBJkUOKEBFrgytveKoZQCaErOyERW5Mgy5nUwtA9CUJL/BVOSKYDwXIx2ARqX8TFWuBEYuuH4NmrVBQR1G7Jrg/mvQrMP1lZM1lxFXqVUAmnNlqHLOQZbd/IxaBaA5gUvdqdDlV+0Gpr8GzUq8XJUKXX4tI6lRABoU5UmFLjfdJ8MSqU0AGpT4ta0yhl41qrkAx4+gYSlzqihj/ifjHt6IGmhYyr4eyphA1HhaIEYVAQ1LvTlOGVEz30gtAtCktIQVyviulvcItQhAo/abU7HLyrbhBWoPgEb5VFTCKMeFBt2i9gBo1LWe+ajc5VRxyWNqD4BGPfipNJW7nBodj6L2AGhU2M7aVO5yav8Ip/pB45JutaJyl1MPag2AhnWncpePznIUtQVAw0ZbyX1pzbTSImoLgIYtqGBKJS8Xiy57qC0AGralpdxXsW2+v0JtAdCw04OsqOTlYr/iBbUFQMMe/GJHJS8X+/3UFAAti/OSO2ou56kpAJp2UuZhs4yr3qCWAGjaDQ9578TO1fEetQRA0+53zEVFL48So4KpJQCaFjTMjYpeHnVWh1JLADTt+TwPKnp5tDmKKQyBC1HeLano5dHrLmZ7Ai4k3etFRS+PQQnUEACNS5B3SsMJ1AwAzRtLRS8H4zxzqRUAmjdNxqF8LDzWUysANG+Zm3z30dh23EetANC831tYU+Gz5zTkHLUCQPP+/NaJCp+9vD/fpVYAaN6NqXmo8NkrsOE5tQJA8x4skm/c1ULesdQKAM0L2exChc9e4evUCAAOXC5Mhc9eBQwhDhx5UpYKnznrdk+pDQAceNxArrEg8/XGED7Akaed5BpfpOR3YdQGAA6EDJbry1rdpZHUBgAORC6pQ6XPWpsdMdQGAA5Eb/ak0mfts2Nx1AYADsTsakulz9rXfyVSGwA4kHi5N5U+a6OfpVAbADiQ8mgElT5rM6gFAFxIi/2BSp+1n6kFAJyYRaXPmPlyWj8AJ5bJMpi4ZdVdtH4ATmwpLseYB1Y1dtP6ATixs7IFlT9Ltk0wtRpwxruRLZU/S/mG+9L6AThxtrczlT9LBb6/RusH4ITvcDnGPHCdiFkMgTPXJ8gx5kHJbbhbDTjjP68IlT9LZQ/hFhrgzNON7lT+LJU9gKgBZyIPyDG8iMdNWj0AL2JOlKfyZ6lGCK0egBdp16tQ+bNUC8ePwJ2IWlT+LDWIorUDcCO6BpU/Q7oOmHAeuBPViOqfIZehGK8fuBPzpT0FgJ3CoxA14E7skAIUAHaKjkHUgDuxI9mPulplVjytHYAbscNcKQDsNFmVQGsH4Eb8bA8KADsdtmMQSOBO4vYOFAB22m9F1IA7iTs6UgDY6foHogbcSfRqRwFgp59PMq0dgBuJ29pTANgZewejiAN3kn2HUADYmRycSmsH4EbqoykUAHYmBiJqwJ3UwIkUAHbG++MAEriT8oD9DBnTnmGvBtyR4wByI60bgCNpsfMpAOzsoHUDcGUtBYCdrbRmAK5sowCwg6gBl9hHzYvWDMCVrRQAZnTbaM0AXGEdNfPyR2jNAFzZYU4ZYMShuQ+tGYAr3oV0FAI27JueoTUDcOV0IzsKARuIGnDKp7kDhYANRA04daYp25EgETXgFOuo2TVB1IBLzPdqzRA14BLrqDm1O0drBuDKmSZsz0C6DLhGawbgytkWbM9Auk/3pzUDcOViV2cKARvuMxA14NLt74pSCNjAXg04FTCzJIWADezVgFP+M9wpBGwgasApRA2ACUQNgAlEDYAJRA2ACUQNgAlEDYAJRA2ACUQNgAlEDYAJRA2ACUQNgAlEDYAJRA2ACUQNgAlEDYAJRA2ACUQNgAlEDYAJRA2ACUQNgAnWUSs5E1EDLvkzHpwOUQNOIWoATCBqAEwgagBMIGoATCBqAEwgagBMIGoATCBqAEwgagBMIGoATCBqAEwgagBMIGoATCBqAEwEMI9aAK0ZgCsBsxA1AAaY34U9G1EDLvl+kY9CwIbbxLu0ZgCunG5sRyFgI2+vy7RmAK74NLOnELDh2PpPWjMAV3yaO1AI2LBvdobWDMAV9lHzoTUDcIV51JojasAlRA2ACUQNgAnWUXNA1IBPPp6IGgADPs0ZX1dreZbWDMCVA27GFAI2rGufoDUDcGWHJWWAldx7aM0AXNlKCWDGZDutGYArzKMmbKU1A3AFUQNgAlEDYAJRA2CCfdS20ZoBuLKDAsDOFlozAFdWUgDYWUtrBuBIavBkCgA7s1+m0toBuJEaOIkCwM7EQEQNuCPHXm1yMKIG3EHUAJiQI2pTEDXgT+qjKRQAdqY8QtSAO6mPp1IA2PnhQQqtHYAbyRcGUgDYGX4NUQPuJO7oSAFgp/v+JFo7ADcSf+9EAWCn445EWjsANxJ3daYAsNN5F6IG3EHUAJhI3NOFAsBOl92IGnAn0astBYCdTxE14E/8ojoUAHbqLomntQNwI25yGQoAO6UnxtHaAbgRO7ooBYAdt+8QNeBO7NhiFAB2io2JpbUDcEOOqGGvBhyK+86NAsBO8e8RNeBO3LjiFAB28g3CASRwJ6aXNQWAHdMu0bR2AG5E1qT6Z6lhFK0dgBuyRK1mJK0dgBuRtan8WaqNqAF35IkaDiCBO7Hsu0AKQq1wWjsANx5Xp/JnqcZTWjsAL2KOl6PyZ6nS5WRaPwAnIg+WpfJnqewhnBcBzkQfkWOvVu4IzosAZ+SKGrqLAGfk+a5W7iiiBpyR57ta+RMxtH4ATgQuZ38PjSC4b8LZfuDM7Z8KUfmz5DrJj9YPwAm/Sa5U/iy5/HCD1g/ACXmihr0acOfmFEQNgAG/yXJEreCPt2j9AJy49O0nVP4sFRh7ldYPwIkTXZ2o/Fly7HSc1g/ACe9GtlT+LNk22U/rB+DE3no2VP4s2dTfS+sH4MS+BvJEbR+tH4AT+xrKETXbRt60fgBO/FGb/Yir+r1aPRxAAme2lTGn8mfJtPgWWj8AJ5aaUPWzZbOC1g/AiblU+6zNp/UDcGI+lT5r82j9AJyQK2rYqwFnEDUAJuSK2kJaPwAnZlLps/YzrR+AC2lRE6j0WRsXkUptAOBAcuAwKn3WhgenUBsAOJDs/y2VPmtDAjBBBnAk+cEQKn3WEDXgioxRe4CoAUcQNQAmkgPkitrQQEQNOJJ0ewCVPmsDbydRGwA4EO/Ti0qftV4+8dQGAA5Ee7Wk0met3e5YagMAB6I3NafSZ81zEyYzBI5Eb2lBpc9aiy2IGnAEUQNgAlEDYAJRA2ACUQNgImZrKyp91hA14Er0Jk8qfdZabY2hNgBw4OW0SlT6rDVeE0VtAOBAyNCiVPqsVZkRTm0A4MCLAQWp9FlzGxFKbQDgwIv+ckw6n86l3wtqAwAH5NurFRyAqAFHEDUAJhA1ACYQNQAm5IxaCLUBgAMh8kVtIKIGHAkZiKgBMICoATCBqAEwgagBMIGoATCBqAEwgagBMIGoATCBqAEwgagBMIGoATCBqAEwIV/UXPvjJhrgiHxRK4S9GvBEvqiV/SGM2gDAgRffyDViVu1FkdQGAA6EjnCj0mcNY/YDV8JneVDps4Yx+4ErUWsaU+mz1tILezXgSMw2uSZ9QtSAK9FeLan0WfPcjKgBR+SLWuvt+K4GHJFvgt7uJ+KpDQAciN7YnEqftX43EqkNAByIP9mDSp+1YYHJ1AYADqQED6fSZ23oQ0QNOJISNIxKn7XhwSnUBgAOJAcModJn7buwVGoDAAeSA4dS6bM2k1oAwImfqPRZm0/rB+DEQip91ubR+gE4MY9Kny2TIhtp/QCcWGGdWfy6zH8YsW3sTesH4MSW4qaZ1W9k51Isr1XmY8nZ1N9H6wfgxB91aLdm226lz/R6mY8ll6vbSVo/ACeOdXLMrH7nqbfSbm/7roZ+xyb9wWSB767S+gE4cf6bvJnVX+j3CP1i4uIGdpnLkio8+27m6gF4cW2cS2b1l72TsRx1akShzCekVGLD04y1AXAjaGXmmFnW7UMzn0i78KVTxjNSKn8yNnNtALyIPVE+s/gnv6RnXu3tKvmJyErX0mhlAJxIvVY5o/g9N/0z1kfCfg+6ACCZWsG0LgBuBGWOBNn32r93RUf8mLmnk07LKFoVADcia2UU/wxaTJf6fKi05/tz9cVXNeBO7BfpZ/ftV9Fips11LTJDIY3KMzGGD3AnbnpFQbCu/d+OUrenf0KpkESnnRjDB7iTsK65ILiMu06LxLccpUISA85jYBHgTvLprwXB3es5LZIXX1AnEklMCsJoB8Cd1KBJglD18hu7mehVjSgWUlhBawHgSFrCfEFo+8/1a5J0ayDFQgLOu2gtAFzZ7Fxg+FunKVLmGFEwRGdb/xStBIArx+p3/DWBHv9rs4tUWXMd+MY5GAA+XB48+UASPf7X8XYOFA2xVVyPbv3ApXvL9wW8fUbw1lSpbqZpehcDGwOXnp1640x/hvB9Ul1a+x+6igCfEl6+84JyUDWKhriMi0yhFQBAugRppl4z67STVgAA6eK+kmSUEdNp92kFAJAufkIJSoeonH7HDdgAr0tYWJvSISaLmpfo9QEgQ+J6T4qHKOhm05ITMNYBwH8kn+4r4r3Y5vmK5E/vftLGJ4ZeHwAypD6YJGLUXPqtn2Wu/3cg7lQD+K+Ue+NEjFqxqQFXKgtGpZbRqwMASYucZ0DU3vzTQuMCQ0e7Wo++TK8OAP/YmeOo6cys3rgvoNC4+6nHWxU+gEFFAN7ye06jZlbasy1NcPO3wuNvvwqf3SuQXhoA/rWdYpJtDiOPnKpAj0nhcTdfvbp+CEOtArxtB8Uk25yXPA/91IwWMuXv7/vqVWwYbp8BeNvhUiYUlGzKuyEu+ccytJApV2cfelUAeMPZpvYUlGzKuy485WAPWshk2+AIvSoAvOFMwxz27c+7LuTVi6m0kEmXfzu9KgC84UxDWwpKNuVd9/zVq03pnUP+5eRFrwoAbzAsaifq2tBiOvuOZ+hVAeANBkTt2atXN4e/Ng6QSaOdL+hVAeANhu3Vnm3MnI80nUmVX+g1AeAthkUt1b8xLQqC26/vGpQLADLkPGq/pR8tJnWjRTOPiUGZrwgA72DYXu1V2mjnjD7HppXnY+RHgA/IcdRyTfNL//sV9dJ7mzi124aBwwE+JMdRs//2bPrfnx5oJgjW7b3enhAAAF6T46jZ9TuZ/vcvllgKFh33ZLwWALxXzvdq/TNnUNtlUmTkwTD9t7aMJQB4txxHzbbXoYwXuFBxNIYyBvioHEfNvPKqjBd4sdsPOzSAj8px1ExLLcl4gaSXOCMC8HGn/9NhOBv+jhoAZMXZZjm8NdQoz0x6CQD4uNujClB2sks3jl4CAD4u/EiXHO7WhCH0EgCQFdvb//dW6izrHUevAABZEL7RPWfDrn6D+WYAsiN4QikKT3boHKbS3wNA1sQPo/hkh9P3mAUDIJsuDjWlAGWZbavr9McAkGV/VacEZVnrvRiaHyDbIjb+O0ZIVuhc50TSnwJANoTMdDWmGGVFnr7n6A8BIFvO93WmGGVF/cuY7hogR1IvVaIYfYxOEKovxMSgADmUMOONiQnfz2h6aCr9FQBkU2rY9Cx2GsnV5TT9DQDkwFHPrHU8rrQ9jP4CAHLg2epyFKYP6xJOfwAAORLelcL0Qa29MJYIgGG8WlOcPiD3shD6bQDIoZBluSlQ7+XY6hL9MgDk2KVWThSp9/HY+JJ+FwByLHTVxy5k90HSAAyXGtyTIvVu5g3W0W8CgEEWVzajWL2L5fR79HsAYBC/HywoVu/icoZ+DQAMdMaFYvUOBb9+SL8FAAZ6+HVBCtbbPPdG028BgIGi97agYL1tOOa7BhBN/HAK1pssa2yiXwEAESwpmD6T/Nucx1+l3wAAEZz80oHC9V+F/8DQPQAiithTmML1H7oGd+kXAEAU96pRuv7DbRRGfgQQVUS//BSv1/U4lkA/BwBRJBzpRvF63eSXGLsHQFSp4VMoXv/S5UVHYwDRrcv75uhZTp1O0s8AQDSnPs1FEfub27Ig+hkAiOb+nKIUsb/VvIXBewBEF3XkzbuxO0bQjwBARM+aUMRIke8xHwaABFL6O1LIMug6bEuinwCAiJJWNfnPOcgxfrioBiCBlHMDKWQZTJbF46wIgATSXs57/U4ap730PACI7I/XBl91aH6BngUAkf1Z14aCJgjuU/zpWQAQ2c2h/961Vn0rpsQAkMjjFf9exW7yF+6fAZBI9Kn6FDRB6ITJCwGkkhbUloKmMx2IM/0Akkn+nKJm4j6LngIACYx3NsqImnmHHfQMAEhgbaPMSWksB2NSDAAJHfg8c1Ia62l36BkAkMDVcVYZUbNdFUbPAIAEwlbaZkTN+gA9AQCS+COza1b+y7QMAJI4kyc9aTZN/GgZACTxV2VjfdRyd8ZZEQBJ3Wppr4+aax906weQlH8fV33Uqi58RssAIIknMyroo9ZkFzobA0jqxbIa+qi1ORlDywAgidDf6uqj1uU6blYDkFTcxY76qPV8gNFWASSV7N9DH7UvnmAISABJJd3tro/aICQNQGIJ/fVRG0ELACCZkfqoDaPHACCZ4YJgOoEeA4BkhgiC28/0GAAkM1YQGqykxwAgmWU1Bc+19BgAJLOuldB0NT0GAMmsboqoATCAqAEwgagBMKGPWhNEDUBy+qi1wBlIAMnpo9ZlMz0GAMnoozbCmx4DgGT0URuDsY0BJKePWuv19BgAJKOPWqNV9BgAJKOPGq6rAUgPUQNgAlEDYAJRA2ACUQNgYq0nbg0FYGB5baH5GnoMAJKZVVBwnEqPAUAy3wuCMIQeA4BkhumjNpweA4BkRmB0YwAWBuujNhBzPgFILOJrfdR6BSJrAJJKfthLH7Ue95NoGQAkkXjzM33Uut1JpGUAkETmBL1NdkfQMgBIInxnY33Uaq54QcsAIInnS6rro1Z1wTNaBgBJPJpaTh+14iMCaRkAJPFgcFF91Bzb3qZlAJDE7XZO+qgJJX1pGQAkcaVUetKEPH/SMgBI4oRzRtTsDtMyAEhit21m1DbF0hMAIIGYjZlRs555n54BAAn4z7LJiJpl/5P0DABI4GR/y4yombfdSs8AgAS2tTPPiJpJhfn0DABIYFZJk4yoGTmMpmcAQAL9MoKWrkcMPQUAoovtSkEThJZBafQkAIgsxb8lBU0QGp3DlTUAiUQcrENBE4SKyx/RswAgsqAF6XfQZCrw9XV6FgBEdr2vCwVNECw8TtGzACCy0x4WFDQ9p130LACIbFvGvWp/W56Ec5AAEkiLmEchy6Ab45dCPwEAEaXcGK2jmKXTdf0DQxwDSCB5e4f/RM3jZ4y7CiCBhAklXo+aIHSMpp8AgIii21LE/lblDv0EAER014Mi9reyO0LoRwAgmpAdZSlif3Mdd4N+BgCiuTHOlSL2N5v6B+hnACCaA/UzhxV53RL6GQCIZgnF63XjwlLppwAgitSX4yler+txLIF+DgCiiD/SjeL1ugpTI+nnACCKiBHFKF6vM2/1mH4OAKJ43JDS9V/FLtHPAUAUF9+1UxOE/MsxpyGAiO7+/AmF67/sPztCvwEAItjd7O2LaumM8y2i3wAAEcwwo2y9ZUAg7loDEEly0L+jGr+pkRfO9wOIJOK3BhSstxUeEEy/BQAGevhZ5rS876KrcIZ+CwAMdMadcvUuNsue0q8BgEGCFrz79GMmq95H6fcAwCCbmmTOX/huxq4/0e8BgEHG2vx3+J43dXiMsVcBDJb2uBNF6n0qLcW3NQCDPV1amSL1PvZN0ekYwGCXmtpRpN7LZCOOIAEMlLYxc6L5D9EN8KHfBoAcOvT5h8+JpDMq9SNmygAwSPSYgkYUqA9p+ox+HwByIvFKPQrThxWeHkB/AQA5cO+HghSmD7OotI/+gqmUBAyOByJLTZDl29AfVSwpTB9hNDtShrOQEYHx9AhAJPGBEfSIodQnU7LyRS1D/RUy3CIacjOGHgGIJOamDLO+pCyuRUH6OJ2nDOPUPb+GKd5AZNFXn9MjhoI9s7xTEwT3bez/M3jhh70aiCz6OvuoPVrlRjHKCrtOx+jv2Ll/IJQeAYgk4s9H9Igd75bWFKMsMZnG/NvapSUYbQFEFrL3Lj1iJS1yoimFKIsar2N9OHd5Ofv/gEDjnv52jR6xErO6EUUoqxw63Ke/ZeXcXAyuDCLz/+EEPWLldgdHilCWffJ7FP0xI0fH3KNHACK52mkbPWLkxW95KEBZZ9ZsO/01I7t6YTZuENnVDlvpESNrPYwpQNlgMuAJ0z4j2zpdpUcAIrnWhekOI8W/J6Une2quYHpxbfunrL/BguYxjlrQdx8a+vH9rOoeY7lbW1H5PD0CEImPG9MpXw6Vz8HhoyDoBGHUFXoJFmY7sT5ZBJp3WJhMj1jwHZ2jpKWzH0GvwcI0U/Y9VEDjjhlPpUcMxA5x+PgoB+9TdWs4vYz0fjT6nR4BiMTLeBo9kl7Ybx8bju5D7Bofp9eR3k9Gq3HDGogqfLGO3XDdx5t8dDi6D7EZe51eSHI/Gf2Krv0gqujlRsyidn3sh2bDyIIyk1kdQk7XzUQnSBDVwx+F6fRQauGTy1BkcqzcCUb7mpnCMJYnPIEDp3oLs+ihxGJOlKPA5JxF/cP0ahKbIXxxih4CiGJPZ8v59FBi++pbUGAMMfgWk1GHFtm1Z9zrErRuXevia+mhpJKvvH+C+ewo0JvJPZvbatT9hR4CiGJOo1Z76aGkgnvnp7AYqMSSoFevJO+j5d3UsTc9BBBF79wtD9JDyeiDEbwkZ10f32beaH0cva6EDjUTPDFbAIgosbnQksGZhui1jT40FW/2tN0r/WnIA02E5mH0GEAEQY2Mupykx9KJ3NSMYiIGh/ZnJB/W5+Igy7q3ZRjqFbQq7mJ1m1F/0YJkUk63MPDa9X+ZjLoo9Ze1+3NzVzgcSQsABgvZXtp5nj8tSCXp/OhsjpD1MWb9pB678s5M5yKLMT4diOb+lHz5l0k8NlTClR7ifU9LpxOEIlMl/v/h6YbC9v1wIzaI5kovq2I7JR7H9/bwfBnxEJXTslBJZ2VKPF9OqCn9l1jgxsmaQrnzibQgidTQ5bkoHmLSFftO0p7Hyb6VBJsdtABgsJ22gsdteiyN8O/dsjETRja4L5R0OtEAD0FYlsp0lC7QrrS05YJQ7QktSSJgYUmKhugc50VJeAwZXUcQJj3E1KEgitSHkwShjoTjBqdGzc/2QMZZV2rcC1qPBOJ6GQu9T+HKGogiaX83we4bCbs5vRhXmmIhiSK/PqQViS/+BzehzjJJv8YCPxLmVBQ85ibQkvge/lqEQiENo1zTJeuilbCigWDTJ5aWAAwS18dKaLtNsv+5Y6bnkuaMyL/KTZRq8t7kY18KQlMZ5i4GLXrcRBC+lqw/4eOJht91/VH5VwZJ0/4U//GCUPkGTkGCCBLOVhCECQHSnGVLDFwl0g1qH2RSYKS/JBuQFvmLIJTykmGecNCewOVugrAkWZL/uJP++tbFhOIgrSIDz0mzX1tvKnwy4BItABjg3Fe5BXsJJlfTZzfFp7crRUF6Iy5IMjrqgUKCaRnGc8+BNm0ra2xSWZIBg+MujKAYsGDTfp8UJyKvfPGJoJtHCwA5lzxLJ+TrLcXNauGbW9pSDKSnEwTbLlskOBEZtLCsIAwLxrAHYKAkv28EodxSCe7Jeri6lZX4ffk/qMFi8c9fRByqKQhtdmA8cTBQ1JrGglDvT/Ev0j6b7kEBYMeqwqBztHrxpF8Lce3/kpYAcuhl3/yC0FT8ncH5gaXNKAAsfTLohNjjE6T0NRaEmvdpCSCH7tQSBOOvxf4mEnZ80CdU/IwZVz8m9onIlfVNhMKHGAyFB1oWvrOgYFJ/JS2JJXxPVTZX097Bovb0G9QMkZwdbCbkGif5KEegbT79HQSzQT60JJIbP1YTdxiR7HGfdD6aWiKK50tsBIvqv9ESQI78WtJMsFkqanfaqAuTxBrCOIccm4ubi0Mu+hcdhftDwQCpI/VFVEDUeY0S1jSX8D7QLNAJgl29qVdE/Pp5vV0uQWjFak430KLYk20EIW+3u7QogtTLE+rYMb6a9i5Wo89HiNar8+mCyoJQYhTGg4QcCxxeXBBqbxDt+DE19NwYayp2mVnVXyXa1LpJtzsKgkl1ZjNwg/b8WUxflJ89Fu1byK0J1Wxl36ER04bjvMUauC7lOyudkMdL1JMtwJPHC8wFndV4WjLY8x0DDZ7lWkxmDfc9F+nGms2NLQSrgadpCSCbtraxECwab6YlA6UEb2Zwu3W2WLv3FGkStocL7ATjAj/SEkD2JA820Ql2C8QZbirutx5uIs9+IQJbzyWXRJkf7XwF/au1u5FEiwDZkHCpob58KpynRYOEXVriaZdR3Aqjs2y3S4yewk8GFBaE4uPQExJy4P644oJQeIAYoxq/3NXOUimnQ97kUOMbL8NPj8QdaS8IRiUO0CJANuwqYaw/Jjpq+JeZcK9vajhQYSuRbYsVlw0+ioydlN6rc9IDWgTIsptj9KVjOdXgG9WeX1jRgt3N1jmh01m2X2/wlFbba1sJuqrzMUwdZFPavKo6wa6NNy3mWMjCJlY6pR48/iN3jR5LDbwC/Xi5k/6F2txGV0jIlqRrbfT5cF5v2MwSiefm96ikyNMhbys/2eepQb0Yn7R1cBbKLpNufgDQpFtTiwp2Nq0N6rsUE3RwqBsVsvKZOBXvu9+Q/h7JAXt++Kx+g920CJAlK5xda3y23M+Q60TR+/sWczCmQlYFl/bfe902YBqQmKtHNiy8gm9rkHWpz3eP+XnzIQOGcov9a9O49uyGUxWNx8yTj2IMu78mCVmDLEu+Z9CRY0rM4yPfl6fiVRdLlyLNF96j7ciZLCQNYeTExz/o1HiDehjdW+hZ+BM5hzQwiK72sFWH70s7Kk/cfbHH7QLFibj38SIy5D/dqLuHVw2rrfiz+x9mXn/GRUnvqX6y7Hg8xkPWtJT4E8sknT8+7vLM+qrdn73Gqe583xjpjvPu9/PsvfMZLYAGPdvxVftR0l34SUt64TOlakapqnq3ZpavfNVWCy9JGLWHw1zMOs496o+ZfTUp1v/o3I6mbhOlGwIjLen5uUWfV6tWzJJqVqXcBx0NipT0RGLE7qb6w1S3cX/irhsNSvpzvJv+0K7pngh6QgppaanRgRenV1bxMWS94bN3/BVF2yOV5Ac99KvSFfccvA0dlLUl7v72wZ7F04/qugdINan1PxLu7Vs8sk16F2PVHUbmKVF3yT0We5rUYZkrNG79y8E7YbE4RaIV0dfn6w9YMoxg0h02LWDrp4UKqG7fZvfd8YfRKVIeOf5jhoNRxiotrB06rrgs5ZEGsHR3cYmMz1UwLbaInpJYStzj03PSBwFQh/Tdr5nnoNm+kszh+y7bGltlrlqftna7MW29VoScnfdNxgydVq3Z9YZNurtxaCuZ5pvJNp2tc4PtT6nlLPh+l37PjX7FlnkKtt4r6kjtIK+Y3a0dnAWncVdomYmYPb1yWck250x2WPffdieS5a1mj1fmyVxx291XH0ZJ/v0ZGIoKvrS5rstutsOCxjw8OCB92FaFc24w7qTUJx3fkHaqiCAYl+o5fhcmZtOglxtmBNJDZlJ9ZnfKRxWtVPk7HaXWMhRcW2dTfrKkXXdAiVITJLtkm3rzy9yKPhfZboWvdPv6tPddOggZPdLr7CMMj8CdiJuiTRbxlsRb63vmVuxFtjyttkp5UiLh2p13H5rGHvXFNzT+xBxfdVTKKYuivQeWpMpWGJ3tgPuSXrJ+uXTRccZfA0G5Ys60d9st7Q1UT1fkouJWFF3pGVelPYZ7uahDvSm+tACc853WxNz5d4lPTQatakLlrSCW1SYaMKhDloQvqyyUGOmDC2eQFuIzspQg5Nsl9VWApA0NlTbyqkW93wwYtCdrwpfV0a+oxHLDRvkDDXixvISFkSDklzxqr1J89UWnJLaNN0l/d+bLhbXS11Vl4Jogega45L/iq8zZBfN5iTEfy4cl7fk0s4utQtRexKDHY8ikzFnljKrP93uCq9X8uvxt0YxCEJznB9BTUtpbVUEX2KynhzDow/+4a2ZfR8HcquSwM/Qk8Cflyea2GYVg1+cYPSWliP36r4UK4TyMSXfQ+x4Za0u/qFh6CaZf45b+f/UX+wand1I0r7yGnpNUxETFXF9ryqSPWsqR/LQ+8xrTxZrlHlQq7rv8FoJgMYEWpeU/Rb8uBTCqs1jyk4/pTvdLnx1Ev0+z7bU7lMk9p6BgITub6euh8yUmt0UeqaaAMX70pT/2FjVIWnMKm2Ss7pPu+yUdWBLUIXFLO2uh2ASpr+ZmCJ5KN4TLy3wLtUdSSf7dMldnOsAHuzRIt6OplVDmFC1IKulW68zqk1WRIUx2akFDC2eszmXA6UR6CjgXfailtXV/n4wTJRJLGm4jfx//et4sBs4JW5eZNKGlD5IGJHVDHSH/aCaHkBsay39xrflFFleT1zU2TV+ZsetkHD3Cv6YJQoUlLLIWMEf+vpAdGOzUIo73KfFJ/vQbGr73o6cA9C58U7RAmy3PpP//9+U6+8x6l9G31BYpPd02qmffgf+rWqDaRXoGIF3c9cldeq3zl/4O/KTfHang5WLefBO1RUopcRFhL1++fHHrMtuhk0Dp0pIiQ8NiWNyDf17uu0QtuuyhpjCBL2ogkyu5qeTlYtF+BzUFQMsuyb5XQ9SAC/JHrcNOagqAll2W/QCy3XZqCoCWyb5XM2+6gZoCoGWyn4E0LvITNQVAyw7SEAAy6o4+iaB9oavl7y3S8g4G8QbNuzVV/j6Q9bwx+ABo3u5P5R/zoOIiJjcxAMjpe5pwXU42dU9SawC06klXKncZ6QRhVQq1B0Cboo41oHqXldGMR+gFDJr2dEVlqnZZGXVAhxHQtj9byn23Wgad82AmY/EByGUt1brsGvnh2xpoV9qz2fIPl5Wp/AEWg2YByCN1X3djKnW5lVj0gBoFoD2JIwooZa9mU/8ANQpAe0JaUaErgMViahSA5iT6Zs42pgwTnuLECGjUkw2Z0wErQ79LSdQuAI25OsKFylwJ2u/E3NSgUQfr2lCZK0G1ORgLFTRqjfx9+l9T5odIaheAxiynIlcGRA00ayUVuTJ4zIqidgFozBqlXL/O0GoLTouARv1RXv5ZDP+Fk/2gWVe+LUBlrgTjHuMSNmhU0PKSVOZKMC0aN2KDRr3cV5HKXAlmJSJqoFExp5XUB3IZtQpAcxIuVqcyl58u/zZqFYDmpN2pTYUuP4fWJ6hVANoT3ogKXX55e1+gRgFoT3wTKnT55R/oS40C0J7EplTo8ss34DI1CkB7EhA1ABYQNQAmFBS1/IPwXQ20S0Hf1fJ+eZ4aBaA9MY2p0OVn1/QoNQpAe54rYsanTI6bqVEA2vOwLtW5EmDIVdCs5Gs1qcyVYEY8evaDRkUerUJlrgS4NRQ0S1m3hn5+MpHaBaAx57/KS2WuBKV/wJCroFHr81CVK4Jxi8fULgBtifyRilwhihzAbg20KP5wd6pxhbBp401NA9CSx5/aUo0rhM5u/AtqG4B2BC93pRJXjmZe4dQ6AM1YX9WSClw5LOocTqDmAWhD9MFuZlTfyqETdP87hOvYoCVJF/so7/AxQ5d7GLkfNOTOjFxU2oqiEwTnz69RGwHUL2RKMSpu5bFYEkqtBFC7Z+srZ+xBFMm0zRZqJoDaHXanslYku4Gx1E4AdUvdrKQZDN/W5Dk1FEDdIldYUFErU5OH1FAAdXv0i7Kj1tQft2ODJgTOUnjUAhA10ISg2dirATCg9Kg1C6KGAqib0g8gW+AMJGiD0vdqLcOooQDqFr1aebfPvK4FbhAFbUjerOyoNX1ADQVQt9i1yo5aw+u4jwY04el8ZX9Xa/AXxl4FLUi7PVnZfSCbPsB1NdCCtMujlB21ehfiqKkAapZ26AvlDSvyuspeOAUJWpC2tokJFbUylV4YSE0FULVJRYyoqJUpf58r1FIANYv/0kipgx1kMitzgJoKoGbPPKmkFctyEzUVQMXSAppQRSvXYpztB/WLOFCdClq5JgZhjGNQvZujC1NBK1et+biyBqp3qJKyu2WlM2obQq0FUKvkpcq+qJbJ4zY1F0ClUi4NompWNOfBl6nBAOoUM7IIVbOyWcxJpRYDqFHyeUWPIf6aLocxBz2o2JmejlTKSufS5x61GUB1UoOnfGJMpax4eTbhLCSo1fPl9aiOVcCo9BJqNoDKxB+uoZp9WroGXhilDtQo6cD/TKmIVaLgXvTPAvWJ+qsLVbBq6KovxSl/UJvIfa3tlTsr7/vU3INjSFCXyL091Rc0vYLbaQMAVCHyeDOqXbWpuxSd/EE9kg62slHlTk2vymnMQg9qkXJhgFqDJgiO7Y/RZgAoXdyXVlS3auS4hjYDQOluK3+Igw+ZTZsBoHAhO8tR0arTiIBk2hIARbs9VfmjiXxIt8MJtCUAinaycy4qWnWquwS3roEqbHVX9nQYH+M6MJS2BEDRVqmsl/GbjJo+oS0BULKUGVSyqlX6Dm0KgIIlXfmGKla18i/DLFCgfDG/qPuqmp5tm320MQDK9bijDVWseln/TBsDoFip50pTvarZFzeTaHsAFOrqKCcqVzWr9BNOQoLCLSmq8lP9mUqdpe0BUKTEU59RraqccZ/jtEkACpTm28eValX1uj9Ap2NQqlS/adZUqOrn2PpP2iwApbk+MC/VqSb09Q7CHNmgQKFHRhdS7zgH71JmcRBtG4BSpL1KWN9SDfODZodJue6LbtEGAijDo02fV1bL/E5Zpd9BG1WcvO+GfyhOkIASpEYG+R1b6En1qTHGFvlqDtrsT5sKIKfH3uOb5LPR2sHjv8yL1O/e/4fle89df4KBEEAmT/evnDakV/NStlSV2qUr23no5FUHfDHdIcjiz27l7KgWtc7MxiGXcwH35bTlAEztstPUyf2Pc1tFWw7A1DaqQF44/A89I0EWvxtRDXLCvvsR2nIApnZQCfIi17ALtOUATB0qzNdXtQLL0VMLZOHbXVP9iz/KddNz2nIApvy+LURFyIeCW3BdDWRxa7i6J8LIroKbETWQxc2hfEWt8P4Y2nIAph4trUBFyAe3c7hXFGQRfqAOFSEfSlynDQdgK/pkfSpCPpS4SRsOwFb06QZUhHwo4UcbDsBW9CnO9mqIGsiDu6jdoA0HYCvqBPZqAAzw9l3NHdOJgjxSA1pREfKhZABtOABjLztQEfKh1CPabgDGIjpSEfIBUQO5RHIWtae03QCMxXSiIuRDadyuBjKJ70xFyAeXVYd3AbC3Z9fKqlSEfHDs+W0/APb6f9U2PxUhO4Lwf8HSkSu5VjdSAAAAAElFTkSuQmCC";
}
