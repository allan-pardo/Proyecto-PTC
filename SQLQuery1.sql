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

