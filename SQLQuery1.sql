CREATE DATABASE DBSistema_inventario
go
use DBSistema_inventario
go


CREATE TABLE Rol(
idRol int primary key identity(1,1),
descripcion varchar (50) Not null,
fechaRegistro datetime default getdate()
);

go


CREATE TABLE Permiso(
idPermiso int primary key identity,
idRol int references Rol(idRol),
nombreMenu varchar(100),
fechaRegistro datetime default getdate()
);

go

CREATE TABLE Proovedor(
idProovedor int primary key identity(1,1),
documento varchar(50),
razonSocial varchar(50),
correo varchar(50),
telefono varchar(50),
estado bit,
fechaRegistro datetime default getdate()
);

go

CREATE TABLE Cliente(
idCliente int primary key identity(1,1),
documento varchar(50),
nombreCompleto varchar(50),
correo varchar(50),
telefono varchar(50),
estado bit,
fechaRegistro datetime default getdate()
);

go

CREATE TABLE Usuario(
idUsuario int primary key identity(1,1),
documento varchar(50),
nombreCompleto varchar(50),
correo varchar(50),
clave varchar(50),
idRol int references Rol(idRol),
estado bit,
fechaRegistro datetime default getdate()
);

go

CREATE TABLE Categoria(
idCategoria int primary key identity(1,1),
descripcion varchar(100),
estado bit,
fechaRegistro datetime default getdate()
);

go

CREATE TABLE Producto(
idProducto int primary key identity(1,1),
codigo varchar(100),
nombre varchar(100),
descripcion varchar(100),
idCategoria int references Categoria(idCategoria),
stock int not null default 9,
precioCompra decimal(10,2)default 0,
precioVenta decimal(10,2)default 0,
estado bit,
fechaRegistro datetime default getdate()
);

go

CREATE TABLE Compra(
idCompra int primary key identity(1,1),
idUsuario int references Usuario(idUsuario),
idProovedor int references Proovedor(idProovedor),
tipoDocumento varchar(50),
numeroDocumento varchar(50),
montoTotal decimal(10,2),
fechaRegistro datetime default getdate()
);

go

CREATE TABLE Detalle_Compra(
idDetalleComprar   int primary key identity(1,1),
idCompra int references Compra(idCompra),
idProducto int references Producto(idProducto),
precioCompra decimal(10,2)default 0,
precioVenta decimal(10,2)default 0,
cantidad int,
montoTotal decimal(10,2),
fechaRegistro datetime default getdate()
);

go

CREATE TABLE Venta(
idVenta int primary key identity(1,1),
idUsuario int references Usuario(idUsuario),
tipoDocumento varchar(50),
numeroDocumento varchar(50),
DocumentoCliente varchar(50),
nombreCliente varchar(100),
montoPago decimal(10,2),
montoCambio decimal(10,2),
montoTotal decimal(10,2),
fechaRegistro datetime default getdate()
);

go

CREATE TABLE Detalle_Venta(
idDetalleVenta int primary key identity(1,1),
idVenta int references Venta(idVenta),
idProducto int references Producto(idProducto),
precioVenta decimal(10,2)default 0,
cantidad int,
subTotal decimal(10,2),
fechaRegistro datetime default getdate()
);

go

create table Negocio(
idNegocio int primary key,
nombre varchar(60),
RUC varchar(60),
direccion varchar(60),
logo varbinary(max) NULL
)

go
--Drop table NEGOCIO
--DROP TABLE Rol
--DROP TABLE Permiso
--DROP TABLE Proovedor
--DROP TABLE Cliente
--DROP TABLE Usuario
--DROP TABLE Categoria
--DROP TABLE Producto
--DROP TABLE Compra
--DROP TABLE Detalle_Compra
--DROP TABLE Venta
--DROP TABLE Detalle_Venta

Select * from Rol
Select * from Permiso
Select * from Negocio
Select * from Usuario
Select * from Categoria
select * from Proovedor
Select * from Producto
Select * from Compra
Select * from Detalle_Compra
Select * from Venta
Select * from Detalle_Venta

select u.idUsuario,u.Documento,u.nombreCompleto,u.correo,u.clave,u.estado,r.idRol,r.descripcion from Usuario u
inner join Rol r on r.idRol= u.idRol

update usuario set estado = 0 where idUsuario = 2

ALTER TABLE Usuario ALTER COLUMN clave VARCHAR(60) NOT NULL;
ALTER TABLE Usuario ADD debeCambiarClave BIT NOT NULL DEFAULT(0);

--insert into Negocio(idNegocio,nombre,RUC,direccion) values
--(1,'Milys Garden','345678','8av, norte')

--insert into Rol(descripcion) values
--('Administrador'),
--('Empleado')

--insert into Usuario(documento,nombreCompleto,correo,clave,idRol,estado) values
--('750394', 'Neton vega','Neton_Vega@gmail.com','123456',1,1),
--('380495', 'Francisco','Francisco@gmail.com','987654',2,1)

--insert into Proovedor(documento,razonSocial,correo,telefono,estado) values ('202503','Vender','SaulSajun@gmail.com','2939 3942',1)


INSERT INTO Usuario(documento, nombreCompleto, correo, clave, idRol, estado) VALUES
('750394', 'Neton Vega', 'neton_vega@gmail.com', '123456', 1, 1),
('802134', 'Karen Martínez', 'karen.mtz@gmail.com', 'abc123', 2, 1),
('912345', 'Carlos Gómez', 'carlosgomez@hotmail.com', 'clave789', 2, 0),
('631209', 'Ana Torres', 'ana.torres@yahoo.com', 'password1', 1, 1),
('583910', 'Luis Pérez', 'luisp@gmail.com', 'qwerty', 2, 1),
('768230', 'Diana López', 'diana.lopez@gmail.com', 'admin2024', 1, 0),
('704523', 'Andrés Morales', 'andresmorales@gmail.com', 'morales123', 2, 1),
('867412', 'Sofía Rivera', 'sofia_riv@gmail.com', 'sofpass', 2, 1),
('390172', 'Jorge Castillo', 'jorge.castillo@gmail.com', 'castillo88', 1, 1),
('541203', 'Daniela Cruz', 'daniela.cruz@gmail.com', 'cruz001', 2, 0),
('600412', 'Marco Díaz', 'marco.dz@gmail.com', 'marco2025', 2, 1);


--insert into Permiso(idRol,nombreMenu) values
--(1,'menuUsuario'),
--(1,'menuMantenedor'),
--(1,'menuVentas'),
--(1,'menuCompras'),
--(1,'menuClientes'),
--(1,'menuProveedores'),
--(1,'menuReportes'),
--(1,'menuAcercaDe')

--insert into Permiso(idRol,nombreMenu) values
--(2,'menuVentas'),
--(2,'menuCompras'),
--(2,'menuClientes'),
--(2,'menuProveedores'),
--(2,'menuAcercaDe')

--INSERT INTO Cliente(documento, nombreCompleto, correo, telefono, estado)
--VALUES
--('C001', 'Ana López', 'ana.lopez@gmail.com', '71450001', 1),
--('C002', 'Carlos Méndez', 'carlos.mendez@gmail.com', '71450002', 1),
--('C003', 'Lucía Rivera', 'lucia.rivera@hotmail.com', '71450003', 1),
--('C004', 'Daniela Cruz', 'daniela.cruz@gmail.com', '71450004', 1),
--('C005', 'José Martínez', 'jose.martinez@gmail.com', '71450005', 1),
--('C006', 'Luis Peña', 'luis.pena@hotmail.com', '71450006', 1),
--('C007', 'Sandra Ávila', 'sandra.avila@gmail.com', '71450007', 1),
--('C008', 'Gloria Salinas', 'gloria.salinas@gmail.com', '71450008', 1),
--('C009', 'María Hernández', 'maria.hernandez@hotmail.com', '71450009', 1),
--('C010', 'Roberto Torres', 'roberto.torres@gmail.com', '71450010', 1),
--('C011', 'Julieta Ramos', 'julieta.ramos@hotmail.com', '71450011', 1),
--('C012', 'Cecilia Vega', 'cecilia.vega@gmail.com', '71450012', 1),
--('C013', 'Edwin Escobar', 'edwin.escobar@gmail.com', '71450013', 1),
--('C014', 'Valeria Domínguez', 'valeria.dom@gmail.com', '71450014', 1),
--('C015', 'Patricia Guzmán', 'patricia.guzman@hotmail.com', '71450015', 1);


--INSERT INTO Categoria(descripcion, estado)
--VALUES
--('Ramos Grandes', 1),
--('Ramos Pequeños', 1),
--('Flores Individuales', 1),
--('Bolsas Decorativas', 1),
--('Centros de Mesa', 1),
--('Arreglos Temáticos', 1),
--('Coronas Florales', 1),
--('Flores Aromáticas', 1),
--('Floreros Decorativos', 1),
--('Cajas de Regalo con Flores', 1),
--('Rosas Eternas', 1),
--('Mini Arreglos', 1),
--('Orquídeas Artificiales', 1),
--('Tulipanes Artificiales', 1),
--('Suculentas Decorativas', 1);

--INSERT INTO Producto(codigo, nombre, descripcion, idCategoria, stock, precioCompra, precioVenta, estado, idProovedor)
--VALUES
--('P001', 'Ramo de Rosas Grandes', 'Rosas rojas con envoltorio elegante', 1, 25, 7.50, 14.99, 1, 1),
--('P002', 'Ramo Pequeño de Margaritas', 'Margaritas blancas y amarillas', 2, 20, 5.00, 9.99, 1, 2),
--('P003', 'Rosa Artificial Individual', 'Rosa de tela roja', 3, 100, 1.00, 2.50, 1, 1),
--('P004', 'Bolsa Decorativa Mediana', 'Diseño floral, ideal para regalo', 4, 50, 1.20, 2.99, 1, 3),
--('P005', 'Centro de Mesa Clásico', 'Flores surtidas en base de vidrio', 5, 15, 10.00, 19.99, 1, 2)
--('P006', 'Arreglo Día de la Madre', 'Especial temático con rosas', 6, 10, 12.00, 24.99, 1, 1),
--('P007', 'Corona Fúnebre Blanca', 'Con flores artificiales y base circular', 7, 5, 15.00, 29.99, 1, 3),
--('P008', 'Flor Aromática de Lavanda', 'Flor sintética perfumada', 8, 40, 2.00, 4.99, 1, 2),
--('P009', 'Florero Decorativo de Cristal', 'Florero para arreglos medianos', 9, 30, 3.50, 7.50, 1, 1),
--('P010', 'Caja con Flores Artificiales', 'Caja elegante con rosas eternas', 10, 12, 8.00, 16.50, 1, 2),
--('P011', 'Rosa Eterna Azul', 'Rosa sintética encapsulada', 11, 10, 5.00, 12.99, 1, 1),
--('P012', 'Mini Arreglo Floral', 'Perfecto para escritorio', 12, 25, 2.50, 5.99, 1, 3),
--('P013', 'Orquídea Artificial Blanca', 'Orquídea de gran detalle', 13, 18, 6.00, 11.99, 1, 2),
--('P014', 'Tulipán Amarillo Artificial', 'Tulipán de tela de alta calidad', 14, 50, 2.00, 4.49, 1, 1),
--('P015', 'Suculenta Decorativa Pequeña', 'Maceta con suculenta sintética', 15, 60, 1.50, 3.99, 1, 2);

INSERT INTO Producto (codigo, nombre, descripcion, idCategoria, stock, precioCompra, precioVenta, estado)
VALUES
('ARTF001', 'Rosa Artificial Roja', 'Rosa de tela roja con tallo flexible', 7, 100, 0.80, 2.50, 1),
('ARTF002', 'Orquídea Artificial Blanca', 'Orquídea de plástico premium con maceta pequeña', 5, 60, 3.50, 7.00, 1),
('ARRA001', 'Ramo Decorativo de Tulipanes', 'Ramo de 10 tulipanes artificiales en tonos variados', 6, 25, 6.00, 12.00, 1),
('ARRA002', 'Centro de Mesa con Flores Artificiales', 'Arreglo en base de cerámica con margaritas y follaje artificial', 7, 15, 8.00, 18.00, 1),
('DECO001', 'Jarrón Decorativo Moderno', 'Jarrón alto de vidrio para arreglos artificiales', 8, 30, 4.00, 9.00, 1)

select*from Producto
select idCategoria from Categoria
select idCategoria,descripcion from Categoria