CREATE OR ALTER PROCEDURE AuthorGetList
--@search VARCHAR(30) = NULL,
--@PublisherSearch INT = NULL
/*
***********************************************************************************************
	Date   			Modified By   	   Purpose of Modification
	15-April-2024    Rohan Nagargoje	   AuthorGetList
***********************************************************************************************
AuthorGetList
*/
AS
BEGIN
	SELECT 
		ISNULL(AuthorID, '') AS AuthorID,
		ISNULL(AuthorName, '') AS AuthorName,
		ISNULL(AuthorPhone, '') AS AuthorPhone,
		ISNULL(IsActive, '') AS IsActive,
		ISNULL(CreatedBy, '') AS CreatedBy,
		ISNULL(CreatedOn, '') AS CreatedOn,
		ISNULL(LastModifiedBy, '') AS ModifiedBy,
		ISNULL(LastModifiedOn, '') AS ModifiedOn
	FROM Authors WHERE IsActive = 1 
	 

END
GO
