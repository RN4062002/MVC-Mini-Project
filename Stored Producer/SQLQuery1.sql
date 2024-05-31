use TrainingDB_RohanNagargoje
-------------------------------------------------------------------------------------------------------------
CREATE OR ALTER PROCEDURE GetBooksDetails 
@search varchar(30) = NULL,
@price varchar(30) = NULL,
@stock Varchar(20) = NULL,
@sort varchar(30) = NULL
AS
BEGIN

IF(@sort = 1)
BEGIN
--------------Ascending Order---------------------------
SELECT BookID, BookName, BookPrice, BookStock 
FROM Book 
WHERE IsActive = 1 
  AND (@search IS NULL OR BookName LIKE '%'+@search+'%') 
  AND(@price IS NULL OR BookPrice LIKE '%'+@price+'%') 
  AND(@stock IS NULL OR BookStock LIKE '%'+@stock+'%')
     ORDER BY BookName
END
-------------------Descending Order--------------------------
ELSE IF(@sort= 2)
BEGIN 
SELECT BookID, BookName, BookPrice, BookStock 
FROM Book 
WHERE IsActive = 1 
  AND (@search IS NULL OR BookName LIKE '%'+@search+'%') 
  AND(@price IS NULL OR BookPrice LIKE '%'+@price+'%') 
  AND(@stock IS NULL OR BookStock LIKE '%'+@stock+'%')
     ORDER BY BookName DESC
END
ELSE

BEGIN
SELECT BookID, BookName, BookPrice, BookStock 
FROM Book 
WHERE IsActive = 1 
  AND (@search IS NULL OR BookName LIKE '%'+@search+'%') 
  AND(@price IS NULL OR BookPrice LIKE '%'+@price+'%') 
  AND(@stock IS NULL OR BookStock LIKE '%'+@stock+'%')
     
END
END;

EXEC GetBooksDetails NULL,NULL,NULL,NULL
------------------------------------------------------------------------------------------------------------
CREATE OR ALTER PROCEDURE USPBookAdd
@BookName varchar(30),
@BookPrice varchar(30),
@BookStock varchar(30)
AS
BEGIN
    INSERT INTO Book(BookName,BookPrice,BookStock)
	VALUES(@BookName,@BookPrice,@BookStock)
END;

EXEC USPBookAdd 'agnipath','2000','20';

-------------------------------------------------------------------------------------------------------------

CREATE OR ALTER PROCEDURE USPBookUpdate
@BookID int,
@BookName varchar(30),
@BookPrice varchar(30),
@BookStock varchar(30)
AS
BEGIN
		UPDATE Book SET BookName = @BookName, BookPrice = @BookPrice,BookStock = @BookStock 
		WHERE BookID = @BookID;
END;

EXEC USPBookUpdate 1,'abc','200','23'


-----------------------------------------------------------------------------------------------------------

CREATE OR ALTER PROCEDURE USPBookDelete
@BookID int
AS
BEGIN 
		UPDATE Book SET IsActive = 0 WHERE BookID = @BookID;
END;

EXEC USPBookDelete 1

------------------------------------------------------------------------------------------------------------
ALTER TABLE Book ADD DEFAULT(1) FOR IsActive
UPDATE Book SET IsActive = 1 WHERE IsActive = 0
------------------------------------------------------------------------------------------------------------

CREATE OR ALTER PROCEDURE USPPagging
@Number int
AS
BEGIN
      SELECT * FROM Book WHERE BookID BETWEEN 1 AND @Number+9 ;
END;

EXEC USPPagging 10

------------------------------------------------------------------------------------------------------------------

