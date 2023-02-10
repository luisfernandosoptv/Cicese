BEGIN TRANSACTION;
CREATE TABLE IF NOT EXISTS "estados" (
	"estado_id"	INTEGER NOT NULL UNIQUE,
	"nombre"	TEXT,
	PRIMARY KEY("estado_id" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "usuarios" (
	"usuario_id"	INTEGER UNIQUE,
	"nombre"	TEXT,
	"apellidoP"	TEXT,
	"apellidoM"	TEXT,
	"edad"	INTEGER,
	"estado"	TEXT,
	"municipio"	TEXT,
	"localidad"	TEXT,
	FOREIGN KEY("estado") REFERENCES "estados"("nombre"),
	PRIMARY KEY("usuario_id" AUTOINCREMENT)
);
INSERT INTO "estados" VALUES (1,'Aguascalientes');
INSERT INTO "estados" VALUES (2,'Baja California');
INSERT INTO "estados" VALUES (3,'Baja California Sur');
INSERT INTO "estados" VALUES (4,'Campeche');
INSERT INTO "estados" VALUES (5,'Chiapas');
INSERT INTO "estados" VALUES (6,'Chihuahua');
INSERT INTO "estados" VALUES (7,'Coahuila');
INSERT INTO "estados" VALUES (8,'Colima');
INSERT INTO "estados" VALUES (9,'Distrito Federal');
INSERT INTO "estados" VALUES (10,'Durango');
INSERT INTO "estados" VALUES (11,'Guanajuato');
INSERT INTO "estados" VALUES (12,'Guerrero');
INSERT INTO "estados" VALUES (13,'Hidalgo');
INSERT INTO "estados" VALUES (14,'Jalisco');
INSERT INTO "estados" VALUES (15,'México');
INSERT INTO "estados" VALUES (16,'Michoacán');
INSERT INTO "estados" VALUES (17,'Morelos');
INSERT INTO "estados" VALUES (18,'Nayarit');
INSERT INTO "estados" VALUES (19,'Nuevo León');
INSERT INTO "estados" VALUES (20,'Oaxaca');
INSERT INTO "estados" VALUES (21,'Puebla');
INSERT INTO "estados" VALUES (22,'Querétaro');
INSERT INTO "estados" VALUES (23,'Quintana Roo');
INSERT INTO "estados" VALUES (24,'San Luis Potosí');
INSERT INTO "estados" VALUES (25,'Sinaloa');
INSERT INTO "estados" VALUES (26,'Sonora');
INSERT INTO "estados" VALUES (27,'Tabasco');
INSERT INTO "estados" VALUES (28,'Tamaulipas');
INSERT INTO "estados" VALUES (29,'Tlaxcala');
INSERT INTO "estados" VALUES (30,'Veracruz');
INSERT INTO "estados" VALUES (31,'Yucatán');
INSERT INTO "estados" VALUES (32,'Zacatecas');
COMMIT;
