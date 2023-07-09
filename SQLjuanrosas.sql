create table PERFILESjuanrosas(
	Rut varchar(10) primary key not null,
	Nombre varchar(30) not null,
	ApPat varchar(30) not null,
	ApMat varchar(30) not null,
	Clave varchar(13) not null
);

create table ACCIONESjuanrosas(
	Num int identity(1,1) primary key not null,
	Clave varchar(13) not null,
	InicioSesion varchar(50) not null,
	FinSesion varchar(50) not null,
	Accion varchar(500) not null,
	AccionF varchar(50) not null
);


alter table PERFILESjuanrosas 
ADD Nivel numeric(1,0) not null;

insert into PERFILESjuanrosas(Rut, Nombre, ApPat, ApMat, Clave, Nivel) 
values('11111111-1', 'juan', 'rosas', 'perez', 'jrp11111111-1', 1);

insert into PERFILESjuanrosas(Rut, Nombre, ApPat, ApMat, Clave, Nivel) 
values('2222222-2', 'jose', 'perez', 'cotapos', 'jpc2222222-2', 2);

select * from PERFILESjuanrosas where ApPat='rosas';

update PERFILESjuanrosas set Clave = 'jrp1111111-2';

delete from PERFILESjuanrosas where Nombre='juan';

select * from PERFILESjuanrosas;
