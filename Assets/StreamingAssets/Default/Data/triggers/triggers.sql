
drop trigger if exists borrar_usuario;
drop trigger if exists nuevo_usuario;
drop trigger if exists poner_fecha_sesion;

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
