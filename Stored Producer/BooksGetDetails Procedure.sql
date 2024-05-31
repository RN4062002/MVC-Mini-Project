CREATE OR ALTER PROCEDURE BooksGetDetails 
@BookID INT
/*
***********************************************************************************************
	Date   			Modified By   	   Purpose of Modification
1	4-April-2024    Rohan Nagargoje	   GetDetails Books
***********************************************************************************************
BooksGetDetails 2
*/
AS
BEGIN
	SELECT
		ISNULL(b.BookID, '') AS BookID,
		ISNULL(b.BookName, '') AS BookName,
		ISNULL(b.BookPrice, '') AS BookPrice,
		ISNULL(b.BookStock, '') AS BookStock,
		ISNULL(p.PublisherName,'')AS PublisherName,
		ISNULL(AuthorName, '') AS AuthorName,
		ISNULL(b.AuthorID, '') AS AuthorID,
		ISNULL(b.PublisherID, '') AS PublisherID,
		ISNULL(b.IsActive, '') AS IsActive,
		ISNULL(b.CreatedBy, '') AS CreatedBy,
		ISNULL(b.CreatedOn, '') AS CreatedOn,
		ISNULL(b.LastModifiedBy, '') AS ModifiedBy,
		ISNULL(b.LastModifiedOn, '') AS ModifiedOn
	FROM
		Books AS b
		LEFT JOIN Publishers AS p ON b.PublisherID = p.PublisherID
		LEFT JOIN Authors    AS a ON b.AuthorID = a.AuthorID
	WHERE
		BookID = @BookID AND b.IsActive = 1 
END
GO