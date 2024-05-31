use TrainingDB_RohanNagargoje

CREATE OR ALTER PROCEDURE SHOW
@a int = 3,
@b int= 0,
@c int output
    AS
    BEGIN
    BEGIN TRY
		  SET @c = @a /@b
		  print @c
    END TRY
    BEGIN CATCH
		SELECT
						   ERROR_NUMBER() as ErrorNumber,
						   ERROR_MESSAGE() as ErrorMessage,
						   ERROR_PROCEDURE() as ErrorProcedure,
						   ERROR_STATE() as ErrorState,
						   ERROR_SEVERITY() as ErrorSeverity,
						   ERROR_LINE() as ErrorLine
    END CATCH
	END

DECLARE @RESULT INT

EXEC SHOW 12,0,@RESULT OUTPUT