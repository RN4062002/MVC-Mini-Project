
CREATE OR ALTER PROCEDURE StudentGetList
/*
***********************************************************************************************
	Date   			Modified By   	   Purpose of Modification
1	22-May-2024    Rohan Nagargoje	   Create Student List
***********************************************************************************************
StudentGetList
-------------------------------------------  
*/
AS
BEGIN

	SELECT 
		ISNULL(StudentID, '') AS StudentID,
		ISNULL(StudentName, '') AS StudentName,
		ISNULL(IsActive, '') AS IsActive,
		ISNULL(CreatedBy, '') AS CreatedBy,
		ISNULL(CreatedOn, '') AS CreatedOn,
		ISNULL(LastModifiedBy, '') AS ModifiedBy,
		ISNULL(LastModifiedOn, '') AS ModifiedOn
		
	FROM Students

END
GO

