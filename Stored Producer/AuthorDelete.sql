CREATE PROCEDURE AuthorDelete
@AuthorID INT
/*
***********************************************************************************************
	Date   			Modified By   	   Purpose of Modification
1	15-April-2024    Rohan Nagargoje	   Delete Authors
***********************************************************************************************
BooksDelete 1
*/	
AS
BEGIN

	UPDATE Authors
	SET IsActive = 0 
	WHERE
	AuthorID = @AuthorID
END
GO