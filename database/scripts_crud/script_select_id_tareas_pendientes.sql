USE Tareas
GO


drop procedure sp_dr_select_id_tareas_pendientes;
go

/*

declare @cod_error	int = 0
declare @msg_error	varchar(100) = ''

execute sp_dr_select_id_tareas_pendientes 
'compras',
'A',
@cod_error	output,
@msg_error	= @msg_error output

select @cod_error, @msg_error


*/

create procedure sp_dr_select_id_tareas_pendientes
@id 			varchar(20), 
@estado			varchar(1) = null,
@cod_error		int output,
@msg_error		varchar(100) output
as
begin
	SET NOCOUNT ON;
	set @cod_error	= 0;
	set @msg_error	= 'OK';

	BEGIN TRY
		SELECT Id
		  ,Titulo
		  ,Descripcion
		  ,Fecha_Creacion
		  ,Fecha_Vencimiento
		  ,Completada
		  ,Estado
		FROM tareasPendientes
		WHERE id = @id and estado = isnull(@estado, estado)

	END TRY
	BEGIN CATCH
		SELECT ERROR_NUMBER() AS ErrorNumber,
			ERROR_SEVERITY() AS ErrorSeverity,
			ERROR_STATE() AS ErrorState,
			ERROR_PROCEDURE() AS ErrorProcedure,
			ERROR_LINE() AS ErrorLine,
			ERROR_MESSAGE() AS ErrorMessage;
	END CATCH
end

GO




