CREATE OR ALTER PROCEDURE BooksUpdate
@BookID INT,
@BookName VARCHAR (200),
@BookPrice INT,
@BookStock INT,
@AuthorID INT, 
@PublisherID INT
/*
***********************************************************************************************
	Date   			Modified By   	   Purpose of Modification
1	4-April-2024    Rohan Nagargoje	   Insert Books
***********************************************************************************************
*/
AS
BEGIN
	UPDATE
		Books
	SET
		BookName = @BookName,
		BookPrice = @BookPrice,
		BookStock = @BookStock,
		PublisherID = @PublisherID,
		AuthorID = @AuthorID,
		LastModifiedBy = 'admin',
		LastModifiedOn = GETDATE() 
	WHERE
		BookID = @BookID
END
GO

--EXEC  BooksUpdate 3,'xyz',230,30

