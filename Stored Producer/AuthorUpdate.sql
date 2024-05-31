CREATE OR ALTER PROCEDURE AuthorUpdate
@AuthorID INT,
@AuthorName VARCHAR (200),
@AuthorPhone VARCHAR(200)
/*
***********************************************************************************************
	Date   			Modified By   	   Purpose of Modification
1	15-April-2024    Rohan Nagargoje	   AuthorUpdate
***********************************************************************************************
*/
AS
BEGIN
	UPDATE
		Authors
	SET
		AuthorName = @AuthorName,
		AuthorPhone = @AuthorPhone,
		LastModifiedBy = 'admin',
		LastModifiedOn = GETDATE() 
	WHERE
		AuthorID = @AuthorID
END
GO

--EXEC  BooksUpdate 3,'xyz',230,30

