use TrainingDB_RohanNagargoje

CREATE OR ALTER PROCEDURE AuthorInsert 
@AuthorID INT OUTPUT,
@AuthorName VARCHAR (200),
@AuthorPhone VARCHAR(200),
@CreatedBy varchar(max)=null

/*
***********************************************************************************************
	Date   			Modified By   	   Purpose of Modification
1	15-April-2024    Rohan Nagargoje	   Insert Authors
***********************************************************************************************
*/
AS
BEGIN
	INSERT INTO Authors
	(
		AuthorName,
		AuthorPhone,
		CreatedOn,
		CreatedBy
		
	)
	VALUES
	(
		@AuthorName,
		@AuthorPhone,
		GETDATE(),
		'admin'			
	)
	SET @AuthorID = @@IDENTITY;
END
GO


DECLARE @VAR INT
EXECUTE AuthorInsert @VAR,'Ranvir','9087898789'






