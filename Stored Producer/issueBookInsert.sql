CREATE OR ALTER PROCEDURE issueBookInsert 
    @studentID INT,
    @issueBookDate DATE,
    @BookQuentityList XML
  
AS
/*
EXECUTE issueBookInsert 1,'2024-02-01','<?xml version="1.0"?>
<ArrayOfBookQuentityList xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <BookQuentityList>
    <BookID>22</BookID>
    <Quentity>1</Quentity>
  </BookQuentityList>
  <BookQuentityList>
    <BookID>23</BookID>
    <Quentity>1</Quentity>
  </BookQuentityList>
</ArrayOfBookQuentityList>'
*/

BEGIN
    -- Insert into IssueBooks table
    INSERT INTO IssueBooks
    (
        StudentID,
        IssueBookDate,
        CreatedBy,
        CreatedOn
    )
    VALUES
    (
        @studentID,
        @issueBookDate,
        'admin',
        GETDATE()
    )

    -- Retrieve the new IssueBookID
	declare @IssueBookID int
    SET @IssueBookID = ident_current('IssueBooks')

    -- Insert into IssueBooksDetails table
    INSERT INTO IssueBooksDetails
    (
        IssueBookID,
        BookID,
        Quentity,
        CreatedOn,
        CreatedBy
    )
    SELECT 
		@IssueBookID,
       BookQuentity.value('(BookID)[1]', 'INT'),
       BookQuentity.value('(Quentity)[1]', 'INT'),
        GETDATE(),
		'admin'
    FROM @BookQuentityList.nodes('/ArrayOfBookQuentityList/BookQuentityList') AS T(BookQuentity)
END
GO


select * from IssueBooks 
select * from IssueBooksDetails