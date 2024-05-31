
CREATE PROCEDURE PublisherGetList
/*
***********************************************************************************************
	Date   			Modified By   	   Purpose of Modification
1	4-April-2024    Rohan Nagargoje	   GetList Books
***********************************************************************************************
BooksGetList
*/
AS
BEGIN
	SELECT 
		ISNULL(PublisherID, '') AS PublisherID,
		ISNULL(PublisherName, '') AS PublisherName,
		ISNULL(PublisherCountry, '') AS PublisherCountry,
		ISNULL(IsActive, '') AS IsActive,
		ISNULL(CreatedBy, '') AS CreatedBy,
		ISNULL(CreatedOn, '') AS CreatedOn,
		ISNULL(LastModifiedBy, '') AS LastModifiedBy,
		ISNULL(LastModifiedOn, '') AS LastModifiedOn
	FROM
		Publishers	
END
GO