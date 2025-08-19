go
CREATE PROC SP_REGISTROUSUARIO(
@Documento varchar(50),
@NombreCompleto varchar(190),
@Correo varchar(50),
@Clave varchar(50),
@idRol int,
@Estado int,
@idUsuarioResultado int output,
@Mensaje varchar(500) output
)

as
begin
	set @idUsuarioResultado = 0
	set @Mensaje = ''

	if not exists (select * from Usuario where Documento = @Documento)
	begin

		insert into Usuario(documento,nombreCompleto,correo,clave,idRol,estado) values
		(@Documento,@NombreCompleto,@Correo,@Clave,@idRol,@Estado)

		set @idUsuarioResultado = SCOPE_IDENTITY()
	end
	else
		set @Mensaje = 'No se puede repetir el documento para màs de un usuario'
end

-------------------------------------------------------------------------------------------------

go
CREATE PROC SP_EDITARUSUARIO(
@idUsuario int,
@Documento varchar(50),
@NombreCompleto varchar(190),
@Correo varchar(50),
@Clave varchar(50),
@idRol int,
@Estado int,
@Respuesta bit output,
@Mensaje varchar(500) output
)

as
begin
	set @Respuesta = 0
	set @Mensaje = ''

	if not exists (select * from Usuario where Documento = @Documento and idUsuario != @idUsuario)
	begin

		update Usuario set
		documento = @Documento,
		nombreCompleto = @NombreCompleto,
		correo = @Correo,
		clave = @Clave,
		idRol = @idRol,
		estado = @Estado
		where idUsuario = @idUsuario

		set @Respuesta = 1
	end
	else
		set @Mensaje = 'No se puede repetir el documento para màs de un usuario'
end

-------------------------------------------------------------------------------------------------

go

go
CREATE PROC SP_ELIMINARUSUARIO(
@idUsuario int,
@Respuesta bit output,
@Mensaje varchar(500) output
)
 
as
begin
	set @Respuesta = 0
	set @Mensaje = ''
	declare @pasoreglas bit = 1


	IF EXISTS(select * from Compra C 
	inner join Usuario U on U.idUsuario = C.idUsuario 
	where U.idUsuario = @idUsuario)

	Begin
		set @pasoreglas = 0
		set @Respuesta = 0
		set @Mensaje = 'No se puede eliminar porque el usuario se encuentra relacionado a una compra\n'
	end
		
	IF EXISTS(select * from Venta V 
	inner join Usuario U on U.idUsuario = V.idUsuario 
	where U.idUsuario = @idUsuario)

	Begin
		set @pasoreglas = 0
		set @Respuesta = 0
		set @Mensaje = 'No se puede eliminar porque el usuario se encuentra relacionado a una venta\n'
	end

	if(@pasoreglas = 1)
	begin

	delete from Usuario where idUsuario = @idUsuario
	set @Respuesta = 1

	end

end



declare @respuesta bit
declare @Mensaje varchar(500)

exec SP_EDITARUSUARIO 3,'283848','Franklin','text@gmail.com','123',2,1,@respuesta output,@Mensaje output

select @respuesta

select @Mensaje 

select * from Usuario

-----------Procedimiento de categortia-----------
CREATE TABLE SP_REGISTRARCATEGORIA(
@Descripcion varchar(50),
@Resultado int output,
@Mensaje varchar(500) output
) as
begin

	SET @Resultado = 0
	IF NOT EXISTS (Select * from Categoria where Descripcion = @Descripcion)
	begin
		insert into Categoria(Descripcion) values (@Descripcion)
		set @Resultado = SCOPE_IDENTITY
	end
	else
		set @Mensaje = 'No se puede repetir la descripcion de una categoria'
end
go

-----------Procedimiento para midificar categortia-----------
CREATE TABLE SP_EDITARCATEGORIA(
@idCategoria int,
@Descripcion varchar(50),
@Resultado int output,
@Mensaje varchar(500) output
) as
begin

	SET @Resultado = 1
	IF NOT EXISTS (Select * from Categoria where Descripcion = @Descripcion and idCategoria = @idCategoria)
	begin
		insert into Categoria(Descripcion) values (@Descripcion)
		set @Resultado = SCOPE_IDENTITY
	end
	else
		set @Mensaje = 'No se puede repetir la descripcion de una categoria'
end
go