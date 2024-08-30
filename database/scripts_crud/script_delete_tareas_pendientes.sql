USE Tareas
GO



--drop procedure sp_dr_delete_tareas_pendientes;
go

/*

declare @cod_error	int = 0
declare @msg_error	varchar(100) = ''

execute sp_dr_delete_tareas_pendientes 
1,
@cod_error	output,
@msg_error	= @msg_error output

select @cod_error, @msg_error


*/

create procedure sp_dr_delete_tareas_pendientes
@id				int,
@cod_error		int output,
@msg_error		varchar(100) output
as
begin
	SET NOCOUNT ON;
	set @cod_error	= 0;
	set @msg_error	= 'OK';

	BEGIN TRY
		begin transaction
		
		DELETE FROM dbo.tareasPendientes
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




