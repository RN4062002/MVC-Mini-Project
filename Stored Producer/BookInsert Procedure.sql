CREATE OR ALTER PROCEDURE BookInsert 
@BookID INT OUTPUT,
@BookName VARCHAR (200),
@BookPrice INT,
@BookStock INT,
@PublisherID INT,
@AuthorID INT = 21,
@CreatedBy varchar(max)=null

/*
***********************************************************************************************
	Date   			Modified By   	   Purpose of Modification
1	4-April-2024    Rohan Nagargoje	   Insert Books
***********************************************************************************************
*/
AS
BEGIN
	INSERT INTO Books
	(
		BookName,
		BookPrice,
		BookStock,
		PublisherID,
		AuthorID,
		CreatedOn,
		CreatedBy		
	)
	VALUES
	(
		@BookName,
		@BookPrice,
		@BookStock,
		@PublisherID,
		@AuthorID,
		GETDATE(),
		'admin'			
	)
	SET @BookID = @@IDENTITY;
END
GO


--DECLARE @VAR INT
--EXECUTE BookInsert @VAR,'aganipankh',200,50,1,






