/*--------------------------------------------------------------------------------------------*/

go


create PROC SP_REGISTRARUSUARIO(
@Documento varchar(50),
@NombreCompleto varchar(100),
@Correo varchar(100),
@Clave varchar(100),
@IdRol int,
@Estado bit,
@IdUsuarioResultado int output,
@Mensaje varchar(500) output
)
as
begin
	set @IdUsuarioResultado = 0
	set @Mensaje = ''


	if not exists(select * from Usuario where documento = @Documento)
	begin
		insert into Usuario(documento,nombreCompleto,correo,clave,idRol,estado) values
		(@Documento,@NombreCompleto,@Correo,@Clave,@IdRol,@Estado)

		set @IdUsuarioResultado = SCOPE_IDENTITY()
		
	end
	else
		set @Mensaje = 'No se puede repetir el documento para más de un usuario'


end

go

ALTER PROCEDURE dbo.SP_REGISTROUSUARIO
    @Documento VARCHAR(50),
    @NombreCompleto VARCHAR(100),
    @Correo VARCHAR(100),
    @Clave VARCHAR(60),
    @IdRol INT,
    @Estado BIT,
    @IdUsuarioResultado INT OUTPUT,
    @Mensaje VARCHAR(500) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    SET @IdUsuarioResultado = 0;
    SET @Mensaje = '';

    IF NOT EXISTS(SELECT 1 FROM Usuario WHERE documento = @Documento)
    BEGIN
        INSERT INTO Usuario(documento,nombreCompleto,correo,clave,idRol,estado)
        VALUES (@Documento,@NombreCompleto,@Correo,@Clave,@IdRol,@Estado);

        SET @IdUsuarioResultado = SCOPE_IDENTITY();
    END
    ELSE
        SET @Mensaje = 'No se puede repetir el documento para más de un usuario';
END

go

create PROC SP_EDITARUSUARIO(
@IdUsuario int,
@Documento varchar(50),
@NombreCompleto varchar(100),
@Correo varchar(100),
@Clave varchar(100),
@IdRol int,
@Estado bit,
@Respuesta bit output,
@Mensaje varchar(500) output
)
as
begin
	set @Respuesta = 0
	set @Mensaje = ''


	if not exists(select * from Usuario where documento = @Documento and idUsuario != @IdUsuario)
	begin
		update  Usuario set
		documento = @Documento,
		nombreCompleto = @NombreCompleto,
		correo = @Correo,
		clave = @Clave,
		idRol = @IdRol,
		estado = @Estado
		where idUsuario = @IdUsuario

		set @Respuesta = 1
		
	end
	else
		set @Mensaje = 'No se puede repetir el documento para más de un usuario'


end
go

create PROC SP_ELIMINARUSUARIO(
@IdUsuario int,
@Respuesta bit output,
@Mensaje varchar(500) output
)
as
begin
	set @Respuesta = 0
	set @Mensaje = ''
	declare @pasoreglas bit = 1

	IF EXISTS (SELECT * FROM Compra C 
	INNER JOIN Usuario U ON U.idUsuario = C.idUsuario
	WHERE U.idUsuario = @IdUsuario
	)
	BEGIN
		set @pasoreglas = 0
		set @Respuesta = 0
		set @Mensaje = @Mensaje + 'No se puede eliminar porque el usuario se encuentra relacionado a una COMPRA\n' 
	END

	IF EXISTS (SELECT * FROM Venta V
	INNER JOIN Usuario U ON U.idUsuario = V.idUsuario
	WHERE U.idUsuario = @IdUsuario
	)
	BEGIN
		set @pasoreglas = 0
		set @Respuesta = 0
		set @Mensaje = @Mensaje + 'No se puede eliminar porque el usuario se encuentra relacionado a una VENTA\n' 
	END

	if(@pasoreglas = 1)
	begin
		delete from Usuario where IdUsuario = @IdUsuario
		set @Respuesta = 1 
	end

end

go



/* ---------- PROCEDIMIENTOS PARA CATEGORIA -----------------*/


create PROC SP_RegistrarCategoria(
@Descripcion varchar(50),
@Estado bit,
@Resultado int output,
@Mensaje varchar(500) output
)as
begin
	SET @Resultado = 0
	IF NOT EXISTS (SELECT * FROM Categoria WHERE descripcion = @Descripcion)
	begin
		insert into Categoria(descripcion,estado) values (@Descripcion,@Estado)
		set @Resultado = SCOPE_IDENTITY()
	end
	ELSE
		set @Mensaje = 'No se puede repetir la descripcion de una categoria'
	
end


go

Create procedure sp_EditarCategoria(
@IdCategoria int,
@Descripcion varchar(50),
@Estado bit,
@Resultado bit output,
@Mensaje varchar(500) output
)
as
begin
	SET @Resultado = 1
	IF NOT EXISTS (SELECT * FROM Categoria WHERE descripcion =@Descripcion and idCategoria != @IdCategoria)
		update Categoria set
		descripcion = @Descripcion,
		estado = @Estado
		where idCategoria = @IdCategoria
	ELSE
	begin
		SET @Resultado = 0
		set @Mensaje = 'No se puede repetir la descripcion de una categoria'
	end

end

go

create procedure sp_EliminarCategoria(
@IdCategoria int,
@Resultado bit output,
@Mensaje varchar(500) output
)
as
begin
	SET @Resultado = 1
	IF NOT EXISTS (
	 select *  from Categoria c
	 inner join Producto p on p.idCategoria = c.IdCategoria
	 where c.idCategoria = @IdCategoria
	)
	begin
	 delete top(1) from Categoria where idCategoria = @IdCategoria
	end
	ELSE
	begin
		SET @Resultado = 0
		set @Mensaje = 'La categoria se encuentara relacionada a un producto'
	end

end

GO

/* ---------- PROCEDIMIENTOS PARA PRODUCTO -----------------*/

create PROC sp_RegistrarProducto(
@Codigo varchar(20),
@Nombre varchar(30),
@Descripcion varchar(30),
@IdCategoria int,
@Estado bit,
@Resultado int output,
@Mensaje varchar(500) output
)as
begin
	SET @Resultado = 0
	IF NOT EXISTS (SELECT * FROM Producto WHERE codigo = @Codigo)
	begin
		insert into Producto(codigo,nombre,descripcion,idCategoria,estado) values (@Codigo,@Nombre,@Descripcion,@IdCategoria,@Estado)
		set @Resultado = SCOPE_IDENTITY()
	end
	ELSE
	 SET @Mensaje = 'Ya existe un producto con el mismo codigo' 
	
end

GO

create procedure sp_ModificarProducto(
@IdProducto int,
@Codigo varchar(20),
@Nombre varchar(30),
@Descripcion varchar(30),
@IdCategoria int,
@Estado bit,
@Resultado bit output,
@Mensaje varchar(500) output
)
as
begin
	SET @Resultado = 1
	IF NOT EXISTS (SELECT * FROM Producto WHERE codigo = @Codigo and idProducto != @IdProducto)
		
		update Producto set
		codigo = @Codigo,
		nombre = @Nombre,
		descripcion = @Descripcion,
		idCategoria = @IdCategoria,
		estado = @Estado
		where IdProducto = @IdProducto
	ELSE
	begin
		SET @Resultado = 0
		SET @Mensaje = 'Ya existe un producto con el mismo codigo' 
	end
end

go


create PROC SP_EliminarProducto(
@IdProducto int,
@Respuesta bit output,
@Mensaje varchar(500) output
)
as
begin
	set @Respuesta = 0
	set @Mensaje = ''
	declare @pasoreglas bit = 1

	IF EXISTS (SELECT * FROM Detalle_Compra dc 
	INNER JOIN Producto p ON p.idProducto = dc.idProducto
	WHERE p.idProducto = @IdProducto
	)
	BEGIN
		set @pasoreglas = 0
		set @Respuesta = 0
		set @Mensaje = @Mensaje + 'No se puede eliminar porque se encuentra relacionado a una COMPRA\n' 
	END

	IF EXISTS (SELECT * FROM Detalle_Compra dv
	INNER JOIN Producto p ON p.idProducto = dv.idProducto
	WHERE p.idProducto = @IdProducto
	)
	BEGIN
		set @pasoreglas = 0
		set @Respuesta = 0
		set @Mensaje = @Mensaje + 'No se puede eliminar porque se encuentra relacionado a una VENTA\n' 
	END

	if(@pasoreglas = 1)
	begin
		delete from Producto where idProducto = @IdProducto
		set @Respuesta = 1 
	end

end
go

/* ---------- PROCEDIMIENTOS PARA CLIENTE -----------------*/

create PROC sp_RegistrarCliente(
@Documento varchar(50),
@NombreCompleto varchar(50),
@Correo varchar(50),
@Telefono varchar(50),
@Estado bit,
@Resultado int output,
@Mensaje varchar(500) output
)as
begin
	SET @Resultado = 0
	DECLARE @IDPERSONA INT 
	IF NOT EXISTS (SELECT * FROM Cliente WHERE Documento = @Documento)
	begin
		insert into Cliente(documento,nombreCompleto,correo,telefono,estado) values (
		@Documento,@NombreCompleto,@Correo,@Telefono,@Estado)

		set @Resultado = SCOPE_IDENTITY()
	end
	else
		set @Mensaje = 'El numero de documento ya existe'
end

go

create PROC sp_ModificarCliente(
@IdCliente int,
@Documento varchar(50),
@NombreCompleto varchar(50),
@Correo varchar(50),
@Telefono varchar(50),
@Estado bit,
@Resultado bit output,
@Mensaje varchar(500) output
)as
begin
	SET @Resultado = 1
	DECLARE @IDPERSONA INT 
	IF NOT EXISTS (SELECT * FROM Cliente WHERE documento = @Documento and idCliente != @IdCliente)
	begin
		update Cliente set
		documento = @Documento,
		nombreCompleto = @NombreCompleto,
		correo = @Correo,
		telefono = @Telefono,
		estado = @Estado
		where idCliente = @IdCliente
	end
	else
	begin
		SET @Resultado = 0
		set @Mensaje = 'El numero de documento ya existe'
	end
end

GO

/* ---------- PROCEDIMIENTOS PARA PROVEEDOR -----------------*/

create PROC sp_RegistrarProveedor(
@Documento varchar(50),
@RazonSocial varchar(50),
@Correo varchar(50),
@Telefono varchar(50),
@Estado bit,
@Resultado int output,
@Mensaje varchar(500) output
)as
begin
	SET @Resultado = 0
	DECLARE @IDPERSONA INT 
	IF NOT EXISTS (SELECT * FROM Proovedor WHERE Documento = @Documento)
	begin
		insert into Proovedor(documento,razonSocial,correo,telefono,estado) values (
		@Documento,@RazonSocial,@Correo,@Telefono,@Estado)

		set @Resultado = SCOPE_IDENTITY()
	end
	else
		set @Mensaje = 'El numero de documento ya existe'
end

GO

create PROC sp_ModificarProveedor(
@IdProveedor int,
@Documento varchar(50),
@RazonSocial varchar(50),
@Correo varchar(50),
@Telefono varchar(50),
@Estado bit,
@Resultado bit output,
@Mensaje varchar(500) output
)as
begin
	SET @Resultado = 1
	DECLARE @IDPERSONA INT 
	IF NOT EXISTS (SELECT * FROM Proovedor WHERE documento = @Documento and idProovedor != @IdProveedor)
	begin
		update Proovedor set
		documento = @Documento,
		razonSocial = @RazonSocial,
		correo = @Correo,
		telefono = @Telefono,
		estado = @Estado
		where idProovedor = @IdProveedor
	end
	else
	begin
		SET @Resultado = 0
		set @Mensaje = 'El numero de documento ya existe'
	end
end


go

create procedure sp_EliminarProveedor(
@IdProveedor int,
@Resultado bit output,
@Mensaje varchar(500) output
)
as
begin
	SET @Resultado = 1
	IF NOT EXISTS (
	 select *  from Proovedor p
	 inner join Compra c on p.idProovedor = c.idProovedor
	 where p.idProovedor = @IdProveedor
	)
	begin
	 delete top(1) from Proovedor where idProovedor = @IdProveedor
	end
	ELSE
	begin
		SET @Resultado = 0
		set @Mensaje = 'El proveedor se encuentara relacionado a una compra'
	end

end

go

/* PROCESOS PARA REGISTRAR UNA COMPRA */

CREATE TYPE [dbo].[EDetalle_Compra] AS TABLE(
	[idProducto] int NULL,
	[precioCompra] decimal(18,2) NULL,
	[precioVenta] decimal(18,2) NULL,
	[cantidad] int NULL,
	[montoTotal] decimal(18,2) NULL
)


GO


CREATE PROCEDURE sp_RegistrarCompra(
@IdUsuario int,
@IdProveedor int,
@TipoDocumento varchar(500),
@NumeroDocumento varchar(500),
@MontoTotal decimal(18,2),
@DetalleCompra [EDetalle_Compra] READONLY,
@Resultado bit output,
@Mensaje varchar(500) output
)
as
begin
	
	begin try

		declare @idcompra int = 0
		set @Resultado = 1
		set @Mensaje = ''

		begin transaction registro

		insert into Compra(idUsuario,idProovedor,tipoDocumento,numeroDocumento,montoTotal)
		values(@IdUsuario,@IdProveedor,@TipoDocumento,@NumeroDocumento,@MontoTotal)

		set @idcompra = SCOPE_IDENTITY()

		insert into Detalle_Compra(idCompra,idProducto,precioCompra,precioVenta,cantidad,montoTotal)
		select @idcompra,IdProducto,PrecioCompra,PrecioVenta,Cantidad,MontoTotal from @DetalleCompra


		update p set p.stock = p.stock + dc.cantidad, 
		p.precioCompra = dc.precioCompra,
		p.precioVenta = dc.precioVenta
		from Producto p
		inner join @DetalleCompra dc on dc.idProducto= p.idProducto

		commit transaction registro


	end try
	begin catch
		set @Resultado = 0
		set @Mensaje = ERROR_MESSAGE()
		rollback transaction registro
	end catch

end


GO

/* PROCESOS PARA REGISTRAR UNA VENTA */

CREATE TYPE [dbo].[EDetalle_Venta] AS TABLE(
	[IdProducto] int NULL,
	[PrecioVenta] decimal(18,2) NULL,
	[Cantidad] int NULL,
	[SubTotal] decimal(18,2) NULL
)


GO


create procedure usp_RegistrarVenta(
@IdUsuario int,
@TipoDocumento varchar(500),
@NumeroDocumento varchar(500),
@DocumentoCliente varchar(500),
@NombreCliente varchar(500),
@MontoPago decimal(18,2),
@MontoCambio decimal(18,2),
@MontoTotal decimal(18,2),
@DetalleVenta [EDetalle_Venta] READONLY,                                      
@Resultado bit output,
@Mensaje varchar(500) output
)
as
begin
	
	begin try

		declare @idventa int = 0
		set @Resultado = 1
		set @Mensaje = ''

		begin  transaction registro

		insert into Venta(idUsuario,tipoDocumento,numeroDocumento,documentoCliente,nombreCliente,montoPago,montoCambio,montoTotal)
		values(@IdUsuario,@TipoDocumento,@NumeroDocumento,@DocumentoCliente,@NombreCliente,@MontoPago,@MontoCambio,@MontoTotal)

		set @idventa = SCOPE_IDENTITY()

		insert into Detalle_Venta(idVenta,idProducto,precioVenta,cantidad,subTotal)
		select @idventa,IdProducto,PrecioVenta,Cantidad,SubTotal from @DetalleVenta

		commit transaction registro

	end try
	begin catch
		set @Resultado = 0
		set @Mensaje = ERROR_MESSAGE()
		rollback transaction registro
	end catch

end

go


create PROC sp_ReporteCompras(
 @fechainicio varchar(10),
 @fechafin varchar(10),
 @idproveedor int
 )
  as
 begin

  SET DATEFORMAT dmy;
   select 
 convert(char(10),c.FechaRegistro,103)[FechaRegistro],c.TipoDocumento,c.NumeroDocumento,c.MontoTotal,
 u.NombreCompleto[UsuarioRegistro],
 pr.Documento[DocumentoProveedor],pr.RazonSocial,
 p.Codigo[CodigoProducto],p.Nombre[NombreProducto],ca.Descripcion[Categoria],dc.PrecioCompra,dc.PrecioVenta,dc.Cantidad,dc.MontoTotal[SubTotal]
 from COMPRA c
 inner join Usuario u on u.idUsuario = c.idUsuario
 inner join Proovedor pr on pr.idProovedor = c.idProovedor
 inner join Detalle_Compra dc on dc.idCompra = c.idCompra
 inner join Producto p on p.idProducto = dc.idProducto
 inner join Categoria ca on ca.idCategoria = p.idCategoria
 where CONVERT(date,c.FechaRegistro) between @fechainicio and @fechafin
 and pr.idProovedor = iif(@idproveedor=0,pr.IdProveedor,@idproveedor)
 end

 go

 CREATE PROC sp_ReporteVentas(
 @fechainicio varchar(10),
 @fechafin varchar(10)
 )
 as
 begin
 SET DATEFORMAT dmy;  
 select 
 convert(char(10),v.fechaRegistro,103)[FechaRegistro],v.tipoDocumento,v.numeroDocumento,v.montoTotal,
 u.nombreCompleto[UsuarioRegistro],
 v.documentoCliente,v.nombreCliente,
 p.codigo[CodigoProducto],p.nombre[NombreProducto],ca.descripcion[Categoria],dv.precioVenta,dv.cantidad,dv.subTotal
 from Venta v
 inner join Usuario u on u.idUsuario = v.idUsuario
 inner join Detalle_Venta dv on dv.idVenta = v.idVenta
 inner join Producto p on p.idProducto = dv.idProducto
 inner join Categoria ca on ca.idCategoria = p.idCategoria
 where CONVERT(date,v.fechaRegistro) between @fechainicio and @fechafin
end


