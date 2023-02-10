
drop view if exists vista_posicion_global;

drop view if exists vista_posicion_estado_1_global;
drop view if exists vista_posicion_estado_2_global;
drop view if exists vista_posicion_estado_3_global;
drop view if exists vista_posicion_estado_4_global;
drop view if exists vista_posicion_estado_5_global;
drop view if exists vista_posicion_estado_6_global;

drop view if exists vista_posicion_estado_1_municipio_1;
drop view if exists vista_posicion_estado_2_municipio_1;
drop view if exists vista_posicion_estado_3_municipio_1;
drop view if exists vista_posicion_estado_4_municipio_1;
drop view if exists vista_posicion_estado_5_municipio_1;
drop view if exists vista_posicion_estado_6_municipio_1;

drop view if exists vista_posicion_estado_1_municipio_2;
drop view if exists vista_posicion_estado_2_municipio_2;
drop view if exists vista_posicion_estado_3_municipio_2;
drop view if exists vista_posicion_estado_4_municipio_2;
drop view if exists vista_posicion_estado_5_municipio_2;
drop view if exists vista_posicion_estado_6_municipio_2;

drop view if exists vista_posicion_estado_1_municipio_3;
drop view if exists vista_posicion_estado_2_municipio_3;
drop view if exists vista_posicion_estado_3_municipio_3;
drop view if exists vista_posicion_estado_4_municipio_3;
drop view if exists vista_posicion_estado_5_municipio_3;
drop view if exists vista_posicion_estado_6_municipio_3;

drop view if exists vista_posicion_estado_1_municipio_4;
drop view if exists vista_posicion_estado_2_municipio_4;
drop view if exists vista_posicion_estado_3_municipio_4;
drop view if exists vista_posicion_estado_4_municipio_4;
drop view if exists vista_posicion_estado_5_municipio_4;
drop view if exists vista_posicion_estado_6_municipio_4;

drop view if exists vista_posicion_estado_1_municipio_5;
drop view if exists vista_posicion_estado_2_municipio_5;
drop view if exists vista_posicion_estado_3_municipio_5;
drop view if exists vista_posicion_estado_4_municipio_5;
drop view if exists vista_posicion_estado_5_municipio_5;

drop view if exists vista_posicion_estado_1_municipio_6;
drop view if exists vista_posicion_estado_2_municipio_6;
drop view if exists vista_posicion_estado_3_municipio_6;
drop view if exists vista_posicion_estado_4_municipio_6;
drop view if exists vista_posicion_estado_5_municipio_6;

drop view if exists vista_posicion_estado_1_municipio_7;
drop view if exists vista_posicion_estado_4_municipio_7;


/*Global*/
CREATE VIEW vista_posicion_global as
	select u.usuario_id,nombres,apellido_paterno,apellido_materno,fecha_nacimiento,sexo,pais,estado,municipio,colonia,calles,escuela,grado,grupo,activo,ruta_imagen,puntos,estado_id,municipio_id,
		(SELECT COUNT(*) FROM (select usuario_id,estado_id,municipio_id,sum(puntos) as puntos from Puntajes group by usuario_id order by puntos desc) AS t2
		WHERE case when t2.puntos=t1.puntos then t1.usuario_id>=t2.usuario_id else t2.puntos >= t1.puntos end ) AS Posicion
		FROM (select usuario_id,estado_id,municipio_id,sum(puntos) as puntos from Puntajes group by usuario_id order by puntos desc) AS t1,usuarios as u where t1.usuario_id=u.usuario_id  order by puntos DESC;
		
CREATE VIEW vista_posicion_estado_1_global as
	select u.usuario_id,nombres,apellido_paterno,apellido_materno,fecha_nacimiento,sexo,pais,estado,municipio,colonia,calles,escuela,grado,grupo,activo,ruta_imagen,puntos,estado_id,municipio_id,
		(SELECT COUNT(*) FROM (select usuario_id,estado_id,municipio_id,sum(puntos) as puntos from Puntajes where estado_id=1 group by usuario_id order by puntos desc) AS t2
		WHERE case when t2.puntos=t1.puntos then t1.usuario_id>=t2.usuario_id else t2.puntos >= t1.puntos end ) AS Posicion
		FROM (select usuario_id,estado_id,municipio_id,sum(puntos) as puntos from Puntajes where estado_id=1 group by usuario_id order by puntos desc) AS t1,usuarios as u where t1.usuario_id=u.usuario_id  order by puntos DESC;

CREATE VIEW vista_posicion_estado_2_global as
	select u.usuario_id,nombres,apellido_paterno,apellido_materno,fecha_nacimiento,sexo,pais,estado,municipio,colonia,calles,escuela,grado,grupo,activo,ruta_imagen,puntos,estado_id,municipio_id,
		(SELECT COUNT(*) FROM (select usuario_id,estado_id,municipio_id,sum(puntos) as puntos from Puntajes where estado_id=2 group by usuario_id order by puntos desc) AS t2
		WHERE case when t2.puntos=t1.puntos then t1.usuario_id>=t2.usuario_id else t2.puntos >= t1.puntos end ) AS Posicion
		FROM (select usuario_id,estado_id,municipio_id,sum(puntos) as puntos from Puntajes where estado_id=2 group by usuario_id order by puntos desc) AS t1,usuarios as u where t1.usuario_id=u.usuario_id  order by puntos DESC;

CREATE VIEW vista_posicion_estado_3_global as
	select u.usuario_id,nombres,apellido_paterno,apellido_materno,fecha_nacimiento,sexo,pais,estado,municipio,colonia,calles,escuela,grado,grupo,activo,ruta_imagen,puntos,estado_id,municipio_id,
		(SELECT COUNT(*) FROM (select usuario_id,estado_id,municipio_id,sum(puntos) as puntos from Puntajes where estado_id=3 group by usuario_id order by puntos desc) AS t2
		WHERE case when t2.puntos=t1.puntos then t1.usuario_id>=t2.usuario_id else t2.puntos >= t1.puntos end ) AS Posicion
		FROM (select usuario_id,estado_id,municipio_id,sum(puntos) as puntos from Puntajes where estado_id=3 group by usuario_id order by puntos desc) AS t1,usuarios as u where t1.usuario_id=u.usuario_id  order by puntos DESC;
CREATE VIEW vista_posicion_estado_4_global as
	select u.usuario_id,nombres,apellido_paterno,apellido_materno,fecha_nacimiento,sexo,pais,estado,municipio,colonia,calles,escuela,grado,grupo,activo,ruta_imagen,puntos,estado_id,municipio_id,
		(SELECT COUNT(*) FROM (select usuario_id,estado_id,municipio_id,sum(puntos) as puntos from Puntajes where estado_id=4 group by usuario_id order by puntos desc) AS t2
		WHERE case when t2.puntos=t1.puntos then t1.usuario_id>=t2.usuario_id else t2.puntos >= t1.puntos end ) AS Posicion
		FROM (select usuario_id,estado_id,municipio_id,sum(puntos) as puntos from Puntajes where estado_id=4 group by usuario_id order by puntos desc) AS t1,usuarios as u where t1.usuario_id=u.usuario_id  order by puntos DESC;
CREATE VIEW vista_posicion_estado_5_global as
	select u.usuario_id,nombres,apellido_paterno,apellido_materno,fecha_nacimiento,sexo,pais,estado,municipio,colonia,calles,escuela,grado,grupo,activo,ruta_imagen,puntos,estado_id,municipio_id,
		(SELECT COUNT(*) FROM (select usuario_id,estado_id,municipio_id,sum(puntos) as puntos from Puntajes where estado_id=5 group by usuario_id order by puntos desc) AS t2
		WHERE case when t2.puntos=t1.puntos then t1.usuario_id>=t2.usuario_id else t2.puntos >= t1.puntos end ) AS Posicion
		FROM (select usuario_id,estado_id,municipio_id,sum(puntos) as puntos from Puntajes where estado_id=5 group by usuario_id order by puntos desc) AS t1,usuarios as u where t1.usuario_id=u.usuario_id  order by puntos DESC;
CREATE VIEW vista_posicion_estado_6_global as
	select u.usuario_id,nombres,apellido_paterno,apellido_materno,fecha_nacimiento,sexo,pais,estado,municipio,colonia,calles,escuela,grado,grupo,activo,ruta_imagen,puntos,estado_id,municipio_id,
		(SELECT COUNT(*) FROM (select usuario_id,estado_id,municipio_id,sum(puntos) as puntos from Puntajes where estado_id=6 group by usuario_id order by puntos desc) AS t2
		WHERE case when t2.puntos=t1.puntos then t1.usuario_id>=t2.usuario_id else t2.puntos >= t1.puntos end ) AS Posicion
		FROM (select usuario_id,estado_id,municipio_id,sum(puntos) as puntos from Puntajes where estado_id=6 group by usuario_id order by puntos desc) AS t1,usuarios as u where t1.usuario_id=u.usuario_id  order by puntos DESC;

CREATE VIEW vista_posicion_estado_1_municipio_1 as
	select u.usuario_id,nombres,apellido_paterno,apellido_materno,fecha_nacimiento,sexo,pais,estado,municipio,colonia,calles,escuela,grado,grupo,activo,ruta_imagen,puntos,estado_id,municipio_id,
		(SELECT COUNT(*) FROM (select * from Puntajes where estado_id=1 and municipio_id=1 order by puntos desc) AS t2
		WHERE case when t2.puntos=t1.puntos then t1.usuario_id>=t2.usuario_id else t2.puntos >= t1.puntos end ) AS Posicion
		FROM (select * from Puntajes  where estado_id=1 and municipio_id=1 order by puntos desc) AS t1,usuarios as u where t1.usuario_id=u.usuario_id  order by puntos DESC;

CREATE VIEW vista_posicion_estado_1_municipio_2 as
	select u.usuario_id,nombres,apellido_paterno,apellido_materno,fecha_nacimiento,sexo,pais,estado,municipio,colonia,calles,escuela,grado,grupo,activo,ruta_imagen,puntos,estado_id,municipio_id,
		(SELECT COUNT(*) FROM (select * from Puntajes where estado_id=1 and municipio_id=2 order by puntos desc) AS t2
		WHERE case when t2.puntos=t1.puntos then t1.usuario_id>=t2.usuario_id else t2.puntos >= t1.puntos end ) AS Posicion
		FROM (select * from Puntajes  where estado_id=1 and municipio_id=2 order by puntos desc) AS t1,usuarios as u where t1.usuario_id=u.usuario_id  order by puntos DESC;
CREATE VIEW vista_posicion_estado_1_municipio_3 as
	select u.usuario_id,nombres,apellido_paterno,apellido_materno,fecha_nacimiento,sexo,pais,estado,municipio,colonia,calles,escuela,grado,grupo,activo,ruta_imagen,puntos,estado_id,municipio_id,
		(SELECT COUNT(*) FROM (select * from Puntajes where estado_id=1 and municipio_id=3 order by puntos desc) AS t2
		WHERE case when t2.puntos=t1.puntos then t1.usuario_id>=t2.usuario_id else t2.puntos >= t1.puntos end ) AS Posicion
		FROM (select * from Puntajes  where estado_id=1 and municipio_id=3 order by puntos desc) AS t1,usuarios as u where t1.usuario_id=u.usuario_id  order by puntos DESC;
CREATE VIEW vista_posicion_estado_1_municipio_4 as
	select u.usuario_id,nombres,apellido_paterno,apellido_materno,fecha_nacimiento,sexo,pais,estado,municipio,colonia,calles,escuela,grado,grupo,activo,ruta_imagen,puntos,estado_id,municipio_id,
		(SELECT COUNT(*) FROM (select * from Puntajes where estado_id=1 and municipio_id=4 order by puntos desc) AS t2
		WHERE case when t2.puntos=t1.puntos then t1.usuario_id>=t2.usuario_id else t2.puntos >= t1.puntos end ) AS Posicion
		FROM (select * from Puntajes  where estado_id=1 and municipio_id=4 order by puntos desc) AS t1,usuarios as u where t1.usuario_id=u.usuario_id  order by puntos DESC;
CREATE VIEW vista_posicion_estado_1_municipio_5 as
	select u.usuario_id,nombres,apellido_paterno,apellido_materno,fecha_nacimiento,sexo,pais,estado,municipio,colonia,calles,escuela,grado,grupo,activo,ruta_imagen,puntos,estado_id,municipio_id,
		(SELECT COUNT(*) FROM (select * from Puntajes where estado_id=1 and municipio_id=5 order by puntos desc) AS t2
		WHERE case when t2.puntos=t1.puntos then t1.usuario_id>=t2.usuario_id else t2.puntos >= t1.puntos end ) AS Posicion
		FROM (select * from Puntajes  where estado_id=1 and municipio_id=5 order by puntos desc) AS t1,usuarios as u where t1.usuario_id=u.usuario_id  order by puntos DESC;
CREATE VIEW vista_posicion_estado_1_municipio_6 as
	select u.usuario_id,nombres,apellido_paterno,apellido_materno,fecha_nacimiento,sexo,pais,estado,municipio,colonia,calles,escuela,grado,grupo,activo,ruta_imagen,puntos,estado_id,municipio_id,
		(SELECT COUNT(*) FROM (select * from Puntajes where estado_id=1 and municipio_id=6 order by puntos desc) AS t2
		WHERE case when t2.puntos=t1.puntos then t1.usuario_id>=t2.usuario_id else t2.puntos >= t1.puntos end ) AS Posicion
		FROM (select * from Puntajes  where estado_id=1 and municipio_id=6 order by puntos desc) AS t1,usuarios as u where t1.usuario_id=u.usuario_id  order by puntos DESC;
CREATE VIEW vista_posicion_estado_1_municipio_7 as
	select u.usuario_id,nombres,apellido_paterno,apellido_materno,fecha_nacimiento,sexo,pais,estado,municipio,colonia,calles,escuela,grado,grupo,activo,ruta_imagen,puntos,estado_id,municipio_id,
		(SELECT COUNT(*) FROM (select * from Puntajes where estado_id=1 and municipio_id=7 order by puntos desc) AS t2
		WHERE case when t2.puntos=t1.puntos then t1.usuario_id>=t2.usuario_id else t2.puntos >= t1.puntos end ) AS Posicion
		FROM (select * from Puntajes  where estado_id=1 and municipio_id=7 order by puntos desc) AS t1,usuarios as u where t1.usuario_id=u.usuario_id  order by puntos DESC;

		
CREATE VIEW vista_posicion_estado_2_municipio_1 as
	select u.usuario_id,nombres,apellido_paterno,apellido_materno,fecha_nacimiento,sexo,pais,estado,municipio,colonia,calles,escuela,grado,grupo,activo,ruta_imagen,puntos,estado_id,municipio_id,
		(SELECT COUNT(*) FROM (select * from Puntajes where estado_id=2 and municipio_id=8 order by puntos desc) AS t2
		WHERE case when t2.puntos=t1.puntos then t1.usuario_id>=t2.usuario_id else t2.puntos >= t1.puntos end ) AS Posicion
		FROM (select * from Puntajes  where estado_id=2 and municipio_id=8 order by puntos desc) AS t1,usuarios as u where t1.usuario_id=u.usuario_id  order by puntos DESC;
CREATE VIEW vista_posicion_estado_2_municipio_2 as
	select u.usuario_id,nombres,apellido_paterno,apellido_materno,fecha_nacimiento,sexo,pais,estado,municipio,colonia,calles,escuela,grado,grupo,activo,ruta_imagen,puntos,estado_id,municipio_id,
		(SELECT COUNT(*) FROM (select * from Puntajes where estado_id=2 and municipio_id=9 order by puntos desc) AS t2
		WHERE case when t2.puntos=t1.puntos then t1.usuario_id>=t2.usuario_id else t2.puntos >= t1.puntos end ) AS Posicion
		FROM (select * from Puntajes  where estado_id=2 and municipio_id=9 order by puntos desc) AS t1,usuarios as u where t1.usuario_id=u.usuario_id  order by puntos DESC;
CREATE VIEW vista_posicion_estado_2_municipio_3 as
	select u.usuario_id,nombres,apellido_paterno,apellido_materno,fecha_nacimiento,sexo,pais,estado,municipio,colonia,calles,escuela,grado,grupo,activo,ruta_imagen,puntos,estado_id,municipio_id,
		(SELECT COUNT(*) FROM (select * from Puntajes where estado_id=2 and municipio_id=10 order by puntos desc) AS t2
		WHERE case when t2.puntos=t1.puntos then t1.usuario_id>=t2.usuario_id else t2.puntos >= t1.puntos end ) AS Posicion
		FROM (select * from Puntajes  where estado_id=2 and municipio_id=10 order by puntos desc) AS t1,usuarios as u where t1.usuario_id=u.usuario_id  order by puntos DESC;
CREATE VIEW vista_posicion_estado_2_municipio_4 as
	select u.usuario_id,nombres,apellido_paterno,apellido_materno,fecha_nacimiento,sexo,pais,estado,municipio,colonia,calles,escuela,grado,grupo,activo,ruta_imagen,puntos,estado_id,municipio_id,
		(SELECT COUNT(*) FROM (select * from Puntajes where estado_id=2 and municipio_id=11 order by puntos desc) AS t2
		WHERE case when t2.puntos=t1.puntos then t1.usuario_id>=t2.usuario_id else t2.puntos >= t1.puntos end ) AS Posicion
		FROM (select * from Puntajes  where estado_id=2 and municipio_id=11 order by puntos desc) AS t1,usuarios as u where t1.usuario_id=u.usuario_id  order by puntos DESC;
CREATE VIEW vista_posicion_estado_2_municipio_5 as
	select u.usuario_id,nombres,apellido_paterno,apellido_materno,fecha_nacimiento,sexo,pais,estado,municipio,colonia,calles,escuela,grado,grupo,activo,ruta_imagen,puntos,estado_id,municipio_id,
		(SELECT COUNT(*) FROM (select * from Puntajes where estado_id=2 and municipio_id=12 order by puntos desc) AS t2
		WHERE case when t2.puntos=t1.puntos then t1.usuario_id>=t2.usuario_id else t2.puntos >= t1.puntos end ) AS Posicion
		FROM (select * from Puntajes  where estado_id=2 and municipio_id=12 order by puntos desc) AS t1,usuarios as u where t1.usuario_id=u.usuario_id  order by puntos DESC;
CREATE VIEW vista_posicion_estado_2_municipio_6 as
	select u.usuario_id,nombres,apellido_paterno,apellido_materno,fecha_nacimiento,sexo,pais,estado,municipio,colonia,calles,escuela,grado,grupo,activo,ruta_imagen,puntos,estado_id,municipio_id,
		(SELECT COUNT(*) FROM (select * from Puntajes where estado_id=2 and municipio_id=13 order by puntos desc) AS t2
		WHERE case when t2.puntos=t1.puntos then t1.usuario_id>=t2.usuario_id else t2.puntos >= t1.puntos end ) AS Posicion
		FROM (select * from Puntajes  where estado_id=2 and municipio_id=13 order by puntos desc) AS t1,usuarios as u where t1.usuario_id=u.usuario_id  order by puntos DESC;

		
CREATE VIEW vista_posicion_estado_3_municipio_1 as
	select u.usuario_id,nombres,apellido_paterno,apellido_materno,fecha_nacimiento,sexo,pais,estado,municipio,colonia,calles,escuela,grado,grupo,activo,ruta_imagen,puntos,estado_id,municipio_id,
		(SELECT COUNT(*) FROM (select * from Puntajes where estado_id=3 and municipio_id=14 order by puntos desc) AS t2
		WHERE case when t2.puntos=t1.puntos then t1.usuario_id>=t2.usuario_id else t2.puntos >= t1.puntos end ) AS Posicion
		FROM (select * from Puntajes  where estado_id=3 and municipio_id=14 order by puntos desc) AS t1,usuarios as u where t1.usuario_id=u.usuario_id  order by puntos DESC;
CREATE VIEW vista_posicion_estado_3_municipio_2 as
	select u.usuario_id,nombres,apellido_paterno,apellido_materno,fecha_nacimiento,sexo,pais,estado,municipio,colonia,calles,escuela,grado,grupo,activo,ruta_imagen,puntos,estado_id,municipio_id,
		(SELECT COUNT(*) FROM (select * from Puntajes where estado_id=3 and municipio_id=15 order by puntos desc) AS t2
		WHERE case when t2.puntos=t1.puntos then t1.usuario_id>=t2.usuario_id else t2.puntos >= t1.puntos end ) AS Posicion
		FROM (select * from Puntajes  where estado_id=3 and municipio_id=15 order by puntos desc) AS t1,usuarios as u where t1.usuario_id=u.usuario_id  order by puntos DESC;
CREATE VIEW vista_posicion_estado_3_municipio_3 as
	select u.usuario_id,nombres,apellido_paterno,apellido_materno,fecha_nacimiento,sexo,pais,estado,municipio,colonia,calles,escuela,grado,grupo,activo,ruta_imagen,puntos,estado_id,municipio_id,
		(SELECT COUNT(*) FROM (select * from Puntajes where estado_id=3 and municipio_id=16 order by puntos desc) AS t2
		WHERE case when t2.puntos=t1.puntos then t1.usuario_id>=t2.usuario_id else t2.puntos >= t1.puntos end ) AS Posicion
		FROM (select * from Puntajes  where estado_id=3 and municipio_id=16 order by puntos desc) AS t1,usuarios as u where t1.usuario_id=u.usuario_id  order by puntos DESC;
CREATE VIEW vista_posicion_estado_3_municipio_4 as
	select u.usuario_id,nombres,apellido_paterno,apellido_materno,fecha_nacimiento,sexo,pais,estado,municipio,colonia,calles,escuela,grado,grupo,activo,ruta_imagen,puntos,estado_id,municipio_id,
		(SELECT COUNT(*) FROM (select * from Puntajes where estado_id=3 and municipio_id=17 order by puntos desc) AS t2
		WHERE case when t2.puntos=t1.puntos then t1.usuario_id>=t2.usuario_id else t2.puntos >= t1.puntos end ) AS Posicion
		FROM (select * from Puntajes  where estado_id=3 and municipio_id=17 order by puntos desc) AS t1,usuarios as u where t1.usuario_id=u.usuario_id  order by puntos DESC;
CREATE VIEW vista_posicion_estado_3_municipio_5 as
	select u.usuario_id,nombres,apellido_paterno,apellido_materno,fecha_nacimiento,sexo,pais,estado,municipio,colonia,calles,escuela,grado,grupo,activo,ruta_imagen,puntos,estado_id,municipio_id,
		(SELECT COUNT(*) FROM (select * from Puntajes where estado_id=3 and municipio_id=18 order by puntos desc) AS t2
		WHERE case when t2.puntos=t1.puntos then t1.usuario_id>=t2.usuario_id else t2.puntos >= t1.puntos end ) AS Posicion
		FROM (select * from Puntajes  where estado_id=3 and municipio_id=18 order by puntos desc) AS t1,usuarios as u where t1.usuario_id=u.usuario_id  order by puntos DESC;
CREATE VIEW vista_posicion_estado_3_municipio_6 as
	select u.usuario_id,nombres,apellido_paterno,apellido_materno,fecha_nacimiento,sexo,pais,estado,municipio,colonia,calles,escuela,grado,grupo,activo,ruta_imagen,puntos,estado_id,municipio_id,
		(SELECT COUNT(*) FROM (select * from Puntajes where estado_id=3 and municipio_id=19 order by puntos desc) AS t2
		WHERE case when t2.puntos=t1.puntos then t1.usuario_id>=t2.usuario_id else t2.puntos >= t1.puntos end ) AS Posicion
		FROM (select * from Puntajes  where estado_id=3 and municipio_id=19 order by puntos desc) AS t1,usuarios as u where t1.usuario_id=u.usuario_id  order by puntos DESC;

		
CREATE VIEW vista_posicion_estado_4_municipio_1 as
	select u.usuario_id,nombres,apellido_paterno,apellido_materno,fecha_nacimiento,sexo,pais,estado,municipio,colonia,calles,escuela,grado,grupo,activo,ruta_imagen,puntos,estado_id,municipio_id,
		(SELECT COUNT(*) FROM (select * from Puntajes where estado_id=4 and municipio_id=20 order by puntos desc) AS t2
		WHERE case when t2.puntos=t1.puntos then t1.usuario_id>=t2.usuario_id else t2.puntos >= t1.puntos end ) AS Posicion
		FROM (select * from Puntajes  where estado_id=4 and municipio_id=20 order by puntos desc) AS t1,usuarios as u where t1.usuario_id=u.usuario_id  order by puntos DESC;
CREATE VIEW vista_posicion_estado_4_municipio_2 as
	select u.usuario_id,nombres,apellido_paterno,apellido_materno,fecha_nacimiento,sexo,pais,estado,municipio,colonia,calles,escuela,grado,grupo,activo,ruta_imagen,puntos,estado_id,municipio_id,
		(SELECT COUNT(*) FROM (select * from Puntajes where estado_id=4 and municipio_id=21 order by puntos desc) AS t2
		WHERE case when t2.puntos=t1.puntos then t1.usuario_id>=t2.usuario_id else t2.puntos >= t1.puntos end ) AS Posicion
		FROM (select * from Puntajes  where estado_id=4 and municipio_id=21 order by puntos desc) AS t1,usuarios as u where t1.usuario_id=u.usuario_id  order by puntos DESC;
CREATE VIEW vista_posicion_estado_4_municipio_3 as
	select u.usuario_id,nombres,apellido_paterno,apellido_materno,fecha_nacimiento,sexo,pais,estado,municipio,colonia,calles,escuela,grado,grupo,activo,ruta_imagen,puntos,estado_id,municipio_id,
		(SELECT COUNT(*) FROM (select * from Puntajes where estado_id=4 and municipio_id=22 order by puntos desc) AS t2
		WHERE case when t2.puntos=t1.puntos then t1.usuario_id>=t2.usuario_id else t2.puntos >= t1.puntos end ) AS Posicion
		FROM (select * from Puntajes  where estado_id=4 and municipio_id=22 order by puntos desc) AS t1,usuarios as u where t1.usuario_id=u.usuario_id  order by puntos DESC;
CREATE VIEW vista_posicion_estado_4_municipio_4 as
	select u.usuario_id,nombres,apellido_paterno,apellido_materno,fecha_nacimiento,sexo,pais,estado,municipio,colonia,calles,escuela,grado,grupo,activo,ruta_imagen,puntos,estado_id,municipio_id,
		(SELECT COUNT(*) FROM (select * from Puntajes where estado_id=4 and municipio_id=23 order by puntos desc) AS t2
		WHERE case when t2.puntos=t1.puntos then t1.usuario_id>=t2.usuario_id else t2.puntos >= t1.puntos end ) AS Posicion
		FROM (select * from Puntajes  where estado_id=4 and municipio_id=23 order by puntos desc) AS t1,usuarios as u where t1.usuario_id=u.usuario_id  order by puntos DESC;
CREATE VIEW vista_posicion_estado_4_municipio_5 as
	select u.usuario_id,nombres,apellido_paterno,apellido_materno,fecha_nacimiento,sexo,pais,estado,municipio,colonia,calles,escuela,grado,grupo,activo,ruta_imagen,puntos,estado_id,municipio_id,
		(SELECT COUNT(*) FROM (select * from Puntajes where estado_id=4 and municipio_id=24 order by puntos desc) AS t2
		WHERE case when t2.puntos=t1.puntos then t1.usuario_id>=t2.usuario_id else t2.puntos >= t1.puntos end ) AS Posicion
		FROM (select * from Puntajes  where estado_id=4 and municipio_id=24 order by puntos desc) AS t1,usuarios as u where t1.usuario_id=u.usuario_id  order by puntos DESC;
CREATE VIEW vista_posicion_estado_4_municipio_6 as
	select u.usuario_id,nombres,apellido_paterno,apellido_materno,fecha_nacimiento,sexo,pais,estado,municipio,colonia,calles,escuela,grado,grupo,activo,ruta_imagen,puntos,estado_id,municipio_id,
		(SELECT COUNT(*) FROM (select * from Puntajes where estado_id=4 and municipio_id=25 order by puntos desc) AS t2
		WHERE case when t2.puntos=t1.puntos then t1.usuario_id>=t2.usuario_id else t2.puntos >= t1.puntos end ) AS Posicion
		FROM (select * from Puntajes  where estado_id=4 and municipio_id=25 order by puntos desc) AS t1,usuarios as u where t1.usuario_id=u.usuario_id  order by puntos DESC;
CREATE VIEW vista_posicion_estado_4_municipio_7 as
	select u.usuario_id,nombres,apellido_paterno,apellido_materno,fecha_nacimiento,sexo,pais,estado,municipio,colonia,calles,escuela,grado,grupo,activo,ruta_imagen,puntos,estado_id,municipio_id,
		(SELECT COUNT(*) FROM (select * from Puntajes where estado_id=4 and municipio_id=26 order by puntos desc) AS t2
		WHERE case when t2.puntos=t1.puntos then t1.usuario_id>=t2.usuario_id else t2.puntos >= t1.puntos end ) AS Posicion
		FROM (select * from Puntajes  where estado_id=4 and municipio_id=26 order by puntos desc) AS t1,usuarios as u where t1.usuario_id=u.usuario_id  order by puntos DESC;

		
	
CREATE VIEW vista_posicion_estado_5_municipio_1 as
	select u.usuario_id,nombres,apellido_paterno,apellido_materno,fecha_nacimiento,sexo,pais,estado,municipio,colonia,calles,escuela,grado,grupo,activo,ruta_imagen,puntos,estado_id,municipio_id,
		(SELECT COUNT(*) FROM (select * from Puntajes where estado_id=5 and municipio_id=27 order by puntos desc) AS t2
		WHERE case when t2.puntos=t1.puntos then t1.usuario_id>=t2.usuario_id else t2.puntos >= t1.puntos end ) AS Posicion
		FROM (select * from Puntajes  where estado_id=5 and municipio_id=27 order by puntos desc) AS t1,usuarios as u where t1.usuario_id=u.usuario_id  order by puntos DESC;
CREATE VIEW vista_posicion_estado_5_municipio_2 as
	select u.usuario_id,nombres,apellido_paterno,apellido_materno,fecha_nacimiento,sexo,pais,estado,municipio,colonia,calles,escuela,grado,grupo,activo,ruta_imagen,puntos,estado_id,municipio_id,
		(SELECT COUNT(*) FROM (select * from Puntajes where estado_id=5 and municipio_id=28 order by puntos desc) AS t2
		WHERE case when t2.puntos=t1.puntos then t1.usuario_id>=t2.usuario_id else t2.puntos >= t1.puntos end ) AS Posicion
		FROM (select * from Puntajes  where estado_id=5 and municipio_id=28 order by puntos desc) AS t1,usuarios as u where t1.usuario_id=u.usuario_id  order by puntos DESC;
CREATE VIEW vista_posicion_estado_5_municipio_3 as
	select u.usuario_id,nombres,apellido_paterno,apellido_materno,fecha_nacimiento,sexo,pais,estado,municipio,colonia,calles,escuela,grado,grupo,activo,ruta_imagen,puntos,estado_id,municipio_id,
		(SELECT COUNT(*) FROM (select * from Puntajes where estado_id=5 and municipio_id=29 order by puntos desc) AS t2
		WHERE case when t2.puntos=t1.puntos then t1.usuario_id>=t2.usuario_id else t2.puntos >= t1.puntos end ) AS Posicion
		FROM (select * from Puntajes  where estado_id=5 and municipio_id=29 order by puntos desc) AS t1,usuarios as u where t1.usuario_id=u.usuario_id  order by puntos DESC;
CREATE VIEW vista_posicion_estado_5_municipio_4 as
	select u.usuario_id,nombres,apellido_paterno,apellido_materno,fecha_nacimiento,sexo,pais,estado,municipio,colonia,calles,escuela,grado,grupo,activo,ruta_imagen,puntos,estado_id,municipio_id,
		(SELECT COUNT(*) FROM (select * from Puntajes where estado_id=5 and municipio_id=30 order by puntos desc) AS t2
		WHERE case when t2.puntos=t1.puntos then t1.usuario_id>=t2.usuario_id else t2.puntos >= t1.puntos end ) AS Posicion
		FROM (select * from Puntajes  where estado_id=5 and municipio_id=30 order by puntos desc) AS t1,usuarios as u where t1.usuario_id=u.usuario_id  order by puntos DESC;
CREATE VIEW vista_posicion_estado_5_municipio_5 as
	select u.usuario_id,nombres,apellido_paterno,apellido_materno,fecha_nacimiento,sexo,pais,estado,municipio,colonia,calles,escuela,grado,grupo,activo,ruta_imagen,puntos,estado_id,municipio_id,
		(SELECT COUNT(*) FROM (select * from Puntajes where estado_id=5 and municipio_id=31 order by puntos desc) AS t2
		WHERE case when t2.puntos=t1.puntos then t1.usuario_id>=t2.usuario_id else t2.puntos >= t1.puntos end ) AS Posicion
		FROM (select * from Puntajes  where estado_id=5 and municipio_id=31 order by puntos desc) AS t1,usuarios as u where t1.usuario_id=u.usuario_id  order by puntos DESC;
CREATE VIEW vista_posicion_estado_5_municipio_6 as
	select u.usuario_id,nombres,apellido_paterno,apellido_materno,fecha_nacimiento,sexo,pais,estado,municipio,colonia,calles,escuela,grado,grupo,activo,ruta_imagen,puntos,estado_id,municipio_id,
		(SELECT COUNT(*) FROM (select * from Puntajes where estado_id=5 and municipio_id=32 order by puntos desc) AS t2
		WHERE case when t2.puntos=t1.puntos then t1.usuario_id>=t2.usuario_id else t2.puntos >= t1.puntos end ) AS Posicion
		FROM (select * from Puntajes  where estado_id=5 and municipio_id=32 order by puntos desc) AS t1,usuarios as u where t1.usuario_id=u.usuario_id  order by puntos DESC;

		
		
CREATE VIEW vista_posicion_estado_6_municipio_1 as
	select u.usuario_id,nombres,apellido_paterno,apellido_materno,fecha_nacimiento,sexo,pais,estado,municipio,colonia,calles,escuela,grado,grupo,activo,ruta_imagen,puntos,estado_id,municipio_id,
		(SELECT COUNT(*) FROM (select * from Puntajes where estado_id=6 and municipio_id=33 order by puntos desc) AS t2
		WHERE case when t2.puntos=t1.puntos then t1.usuario_id>=t2.usuario_id else t2.puntos >= t1.puntos end ) AS Posicion
		FROM (select * from Puntajes  where estado_id=6 and municipio_id=33 order by puntos desc) AS t1,usuarios as u where t1.usuario_id=u.usuario_id  order by puntos DESC;
CREATE VIEW vista_posicion_estado_6_municipio_2 as
	select u.usuario_id,nombres,apellido_paterno,apellido_materno,fecha_nacimiento,sexo,pais,estado,municipio,colonia,calles,escuela,grado,grupo,activo,ruta_imagen,puntos,estado_id,municipio_id,
		(SELECT COUNT(*) FROM (select * from Puntajes where estado_id=6 and municipio_id=34 order by puntos desc) AS t2
		WHERE case when t2.puntos=t1.puntos then t1.usuario_id>=t2.usuario_id else t2.puntos >= t1.puntos end ) AS Posicion
		FROM (select * from Puntajes  where estado_id=6 and municipio_id=34 order by puntos desc) AS t1,usuarios as u where t1.usuario_id=u.usuario_id  order by puntos DESC;
CREATE VIEW vista_posicion_estado_6_municipio_3 as
	select u.usuario_id,nombres,apellido_paterno,apellido_materno,fecha_nacimiento,sexo,pais,estado,municipio,colonia,calles,escuela,grado,grupo,activo,ruta_imagen,puntos,estado_id,municipio_id,
		(SELECT COUNT(*) FROM (select * from Puntajes where estado_id=6 and municipio_id=35 order by puntos desc) AS t2
		WHERE case when t2.puntos=t1.puntos then t1.usuario_id>=t2.usuario_id else t2.puntos >= t1.puntos end ) AS Posicion
		FROM (select * from Puntajes  where estado_id=6 and municipio_id=35 order by puntos desc) AS t1,usuarios as u where t1.usuario_id=u.usuario_id  order by puntos DESC;
CREATE VIEW vista_posicion_estado_6_municipio_4 as
	select u.usuario_id,nombres,apellido_paterno,apellido_materno,fecha_nacimiento,sexo,pais,estado,municipio,colonia,calles,escuela,grado,grupo,activo,ruta_imagen,puntos,estado_id,municipio_id,
		(SELECT COUNT(*) FROM (select * from Puntajes where estado_id=6 and municipio_id=36 order by puntos desc) AS t2
		WHERE case when t2.puntos=t1.puntos then t1.usuario_id>=t2.usuario_id else t2.puntos >= t1.puntos end ) AS Posicion
		FROM (select * from Puntajes  where estado_id=6 and municipio_id=36 order by puntos desc) AS t1,usuarios as u where t1.usuario_id=u.usuario_id  order by puntos DESC;
