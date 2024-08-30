USE Tareas
GO

drop procedure sp_dr_update_tareas_pendientes;
go

/*

declare @cod_error	int = 0
declare @msg_error	varchar(100) = ''

execute sp_dr_update_tareas_pendientes 
1,
'compras',
'comprar en el mercado',
'20240729',
1,
'A',
@cod_error	output,
@msg_error	= @msg_error output

select @cod_error, @msg_error


*/

create procedure sp_dr_update_tareas_pendientes
@id					int,
@Titulo 			varchar(20), 
@Descripcion 		varchar(50),
@Fecha_Creacion 	datetime, 
@Fecha_Vencimiento 	datetime, 
@Completada 		bit,
@Estado	 			varchar(1),
@cod_error			int output,
@msg_error			varchar(100) output
as
begin
	SET NOCOUNT ON;
	set @cod_error	= 0;
	set @msg_error	= 'OK';

	BEGIN TRY
		begin transaction
		UPDATE dbo.tareasPendientes
			SET Titulo = @Titulo				
				,Descripcion = @Descripcion
				,Fecha_Creacion = @Fecha_Creacion
				,Fecha_Vencimiento = @Fecha_Vencimiento
				,Completada = @Completada				
				,estado = @estado
			WHERE id = @id

		commit transaction
	END TRY
	BEGIN CATCH
		rollback transaction 
		SELECT ERROR_NUMBER() AS ErrorNumber,
			ERROR_SEVERITY() AS ErrorSeverity,
			ERROR_STATE() AS ErrorState,
			ERROR_PROCEDURE() AS ErrorProcedure,
			ERROR_LINE() AS ErrorLine,
			ERROR_MESSAGE() AS ErrorMessage;
	END CATCH
end

GO



GO


