DROP DATABASE IF EXISTS T7_BBDDJuego;
CREATE DATABASE T7_BBDDJuego; 

USE T7_BBDDJuego;

create table jugador(
	id int,
	nombre varchar(20),
	contraseña varchar(20),
	PRIMARY KEY (id));

create table partida(
	id int,
	jugadores int,
	servidor varchar(5),
	PRIMARY KEY (id));

create table historial(
	idp int,
	idj int,
	ganador varchar(2),
	puntos int,
	tiempo int,
	fecha varchar(11));

insert into jugador values (1,'Kike','1234');
insert into jugador values (2,'Aran','1234');
insert into jugador values (3,'Janna','1234');
insert into jugador values (4,'Juan','1234');

insert into partida values (1,2,'EUW');
insert into partida values (2,2,'EUW');
insert into partida values (3,4,'EUW');
insert into partida values (4,2,'EUW');

insert into historial values (1,1,'no',5,5,'07/10/2022');
insert into historial values (1,2,'si',16,5,'07/10/2022');
insert into historial values (2,3,'si',16,3,'06/10/2022');
insert into historial values (2,1,'no',7,3,'06/10/2022');
insert into historial values (3,3,'si',16,1,'06/10/2022');
insert into historial values (3,2,'si',15,1,'06/10/2022');
insert into historial values (3,4,'no',6,1,'06/10/2022');
insert into historial values (3,1,'no',6,1,'06/10/2022');
insert into historial values (4,3,'si',16,2,'06/10/2022');
insert into historial values (4,4,'no',9,2,'06/10/2022');
insert into historial values (5,3,'si',16,2,'06/10/2022');
insert into historial values (5,2,'no',9,2,'06/10/2022');
insert into historial values (6,3,'no',9,2,'07/10/2022');
insert into historial values (6,1,'si',16,2,'07/10/2022');











