use TrainingDB_RohanNagargoje

CREATE OR ALTER PROCEDURE add_values 
 @a int,
 @b int,
 @c int output
AS 
BEGIN
	BEGIN TRY
		SET @c = @a / @b
		PRINT @c
	END TRY

	BEGIN CATCH
		SELECT 
		ERROR_LINE(),
		ERROR_SEVERITY(),
		ERROR_NUMBER()
	END CATCH
END

declare @v int 
exec add_values 34,12,@v output