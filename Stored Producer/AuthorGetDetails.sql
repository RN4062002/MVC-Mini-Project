CREATE OR ALTER PROCEDURE AuthorGetDetails 
@AuthorID INT
/*
***********************************************************************************************
	Date   			Modified By   	   Purpose of Modification
1	15-April-2024    Rohan Nagargoje	   AuthorGetDetails 
***********************************************************************************************

AuthorGetDetails 

*/
AS
BEGIN
	SELECT
		ISNULL(AuthorID, '') AS BookID,
		ISNULL(AuthorName, '') AS BookName,
		ISNULL(AuthorPhone, '') AS BookPrice,
		ISNULL(IsActive, '') AS IsActive,
		ISNULL(CreatedBy, '') AS CreatedBy,
		ISNULL(CreatedOn, '') AS CreatedOn,
		ISNULL(LastModifiedBy, '') AS ModifiedBy,
		ISNULL(LastModifiedOn, '') AS ModifiedOn
	FROM
		Authors
	WHERE
		AuthorID = @AuthorID AND IsActive = 1 
END
GO