create database InventarioAlimento

go 

use InventarioAlimento

go

create table Producto(
id_Producto int identity(1,1) primary key,
Nombre varchar (50) not null,
Descripcion varchar (100) not null,
Cantidad int not null
)

go 
create proc SP_CrearProducto (@nombre varchar (50), @descripcion varchar (100),@cantidad int, @respuesta bit output)
as begin 
	set @respuesta = 0
	if not exists(Select * from Producto where Nombre = @nombre and Descripcion = @descripcion)
		begin
			Insert Producto values(@nombre,@descripcion,@cantidad)
			set @respuesta = 1
		end
end

go 

create proc SP_ActualizarProducto(@id_Producto int,@nombre varchar (50), @descripcion varchar (100),@cantidad int, @respuesta bit output)
as begin
	set @respuesta = 0
	if not exists(Select * from Producto where Nombre = @nombre and Descripcion = @descripcion and id_Producto != @id_Producto)
	begin
		Update Producto set Nombre = @nombre, Descripcion = @descripcion, Cantidad = @cantidad where id_Producto = @id_Producto
		set @respuesta = 1
	end
end

go 

create proc SP_ListarTodo
as begin 
Select * from Producto
end

go 

create proc SP_ObtenerProducto (@id_Producto int)
as begin 
Select * from Producto where id_Producto = @id_Producto
end

go 

create proc SP_Eliminar(@id_Producto int, @respuesta bit output)
as begin 
	set @respuesta = 0
	if exists(Select * from Producto where id_Producto = @id_Producto)
	begin
		Delete top (1) Producto where id_Producto = @id_Producto
		set @respuesta = 1
	end
end


