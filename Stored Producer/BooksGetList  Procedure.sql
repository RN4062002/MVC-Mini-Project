
CREATE OR ALTER PROCEDURE BooksGetList
@search VARCHAR(30) = NULL,
@PublisherSearch varchar(max) = NULL,
@AuthorSearch varchar(max) = NULL,
@PageNumber INT=1, 
@PageSize   INT=5,
@TotalCount INT OUTPUT,
@TOTALROWS  INT OUTPUT
/*
***********************************************************************************************
	Date   			Modified By   	   Purpose of Modification
1	4-April-2024    Rohan Nagargoje	   Insert Books
***********************************************************************************************
BooksGetList
----------------------------------------
DECLARE @VAR INT
EXECUTE BooksGetList NULL,Null,NULL,1,10,@VAR OUT
PRINT  @VAR
-------------------------------------------  
*/
AS
BEGIN
DROP TABLE IF EXISTS #Paging

	SELECT 
		ISNULL(BookID, '') AS BookID,
		ISNULL(BookName, '') AS BookName,
		ISNULL(BookPrice, '') AS BookPrice,
		ISNULL(BookStock, '') AS BookStock,
		ISNULL(PublisherName, '') AS PublisherName,
		ISNULL(AuthorName, '') AS AuthorName,
		ISNULL(b.IsActive, '') AS IsActive,
		ISNULL(b.CreatedBy, '') AS CreatedBy,
		ISNULL(b.CreatedOn, '') AS CreatedOn,
		ISNULL(b.LastModifiedBy, '') AS ModifiedBy,
		ISNULL(b.LastModifiedOn, '') AS ModifiedOn,
		Row_number() OVER(ORDER BY BookID) AS rownum

		INTO #Paging

	FROM
		Books AS b 
		LEFT JOIN Publishers AS p  ON b.PublisherID = p.PublisherID 
		LEFT JOIN Authors	 AS a  ON b.AuthorID = a.AuthorID
	WHERE
		b.IsActive = 1  
	  	AND ((@search is null) or BookName like '%'+@search+'%')
	  	AND ((@PublisherSearch is null OR @PublisherSearch='') or p.PublisherID in (SELECT *FROM string_split(@PublisherSearch,',')))
	  	AND ((@AuthorSearch is null OR @AuthorSearch='') or a.AuthorID in  (SELECT *FROM string_split(@AuthorSearch,',')))

	SELECT * 
	FROM	#Paging 
	WHERE	rownum BETWEEN ((@PageNumber-1) * @pageSize + 1 ) AND ((((@PageNumber-1) * @PageSize + 1) + @PageSize) - 1 )


	--DECLARE @TOTALROWS INT
SELECT @TOTALROWS = COUNT(*) FROM #Paging WHERE isActive = 1
SET @TotalCount = ceiling(cast(@TOTALROWS AS decimal)/@PageSize)

END
GO




DECLARE @TotalCount INT, @TotalRow INT
EXEC BooksGetList @search=NULL, @PublisherSearch='1,2,3,4,5', @AuthorSearch=NULL, @PageNumber=1, @PageSize=1000, @TotalCount=@TotalCount OUTPUT,@TOTALROWS = @TotalRow OUTPUT
PRINT @TotalCount
PRINT @TotalRow