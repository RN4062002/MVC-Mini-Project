CREATE PROCEDURE BooksDelete
@BookID INT
/*
***********************************************************************************************
	Date   			Modified By   	   Purpose of Modification
1	4-April-2024    Rohan Nagargoje	   Delete Books
***********************************************************************************************
BooksDelete 1
*/	
AS
BEGIN
	UPDATE Books
	SET IsActive = 0 
	WHERE
	BookID = @BookID
END
GO