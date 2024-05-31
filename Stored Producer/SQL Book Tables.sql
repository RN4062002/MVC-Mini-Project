USE TrainingDB_RohanNagargoje
--------------------------------------------------------------------------------------------------------------------------
CREATE TABLE Readers(
	ReaderID INT PRIMARY KEY NOT NULL IDENTITY(1,1),
	ReaderName VARCHAR(20) NOT NULL,
	ReaderEmail VARCHAR(20),
	ReaderPhone VARCHAR(30),
	CreatedBy VARCHAR(100) NOT NULL,
	CreatedOn DateTime DEFAULT GETDATE(),
	LastModifiedBy VARCHAR(100) NULL,
	LastModifiedOn  DateTime DEFAULT GETDATE(),
	IsActive bit DEFAULT 1);
--------------------------------------------------------------------------------------------------------------------
CREATE TABLE Authors(
	AuthorID INT PRIMARY KEY NOT NULL IDENTITY(1,1),
	AuthorName VARCHAR(20) NOT NULL,
	AuthorPhone VARCHAR(20) ,
	CreatedBy VARCHAR(100) NOT NULL,
	CreatedOn DateTime ,
	LastModifiedBy VARCHAR(100) NULL,
	LastModifiedOn  DateTime DEFAULT GETDATE(),
	IsActive bit DEFAULT 1);
--------------------------------------------------------------------------------------------------------------------
CREATE TABLE Publishers(
    PublisherID INT PRIMARY KEY  NOT NULL IDENTITY(1,1),
	PublisherName VARCHAR(max) NOT NULL,
	PublisherCountry VARCHAR(max) NOT NULL,
	CreatedBy VARCHAR(100),
	CreatedOn DateTime ,
	LastModifiedBy VARCHAR(100) ,
	LastModifiedOn  DateTime DEFAULT GETDATE(),
	IsActive bit DEFAULT 1
	)
--------------------------------------------------------------------------------------------------------------------
CREATE TABLE Books(
	BookID INT PRIMARY KEY NOT NULL IDENTITY(1,1),
	BookName VARCHAR(20) NOT NULL,
	BookPrice INT,
	BookStock INT,
	PublisherID INT  ,
	AuthorID INT,
	CreatedBy VARCHAR(100) NOT NULL,
	CreatedOn DateTime ,
	LastModifiedBy VARCHAR(100) NULL,
	LastModifiedOn  DateTime DEFAULT GETDATE(),
	IsActive bit DEFAULT 1,
	FOREIGN KEY (PublisherID) REFERENCES Publishers(PublisherID),
	FOREIGN KEY (AuthorID) REFERENCES Authors(AuthorID)
);
----------------------------------------------------------------------------------------------------------------------
CREATE TABLE BookAuthors(
	BookAuthorID INT PRIMARY KEY NOT NULL IDENTITY(1,1),
	BookID INT,
	AuthorID INT,
	CreatedBy VARCHAR(100) NOT NULL,
	CreatedOn DateTime ,
	LastModifiedBy VARCHAR(100) NULL,
	LastModifiedOn  DateTime DEFAULT GETDATE(),
	IsActive bit DEFAULT 1,
	FOREIGN KEY (BookID)REFERENCES Books(BookID)
	);
-----------------------------------------------------------------------------------------------------------------------
CREATE TABLE Employees(
	EmployeeID INT PRIMARY KEY  NOT NULL IDENTITY(1,1),
	EmployeeName VARCHAR(max) NOT NULL,
	EmployeeEmail VARCHAR(max) NOT NULL,
	EmployeePhone VARCHAR(max) ,
	CreatedBy varchar(100),
	CreatedOn DateTime ,
	LastModifiedBy VARCHAR(100) NULL ,
	LastModifiedOn  DateTime DEFAULT GETDATE(),
	IsActive bit DEFAULT 1
)
---------------------------------------------------------------------------------------------------------------------------
CREATE TABLE Genres(
	GenreID INT PRIMARY KEY NOT NULL IDENTITY(1,1),
	GenreName VARCHAR(20) NOT NULL,
	CreatedBy VARCHAR(100),
	CreatedOn DateTime ,
	LastModifiedBy VARCHAR(100) ,
	LastModifiedOn  DateTime DEFAULT GETDATE(),
	IsActive bit DEFAULT 1
	)
------------------------------------------------------------------------------------------------------------------------
CREATE TABLE BookGenres(
	BookGenreID INT PRIMARY KEY NOT NULL IDENTITY(1,1),
	BookID INT,
	GenreID INT,
	CreatedBy VARCHAR(100),
	CreatedOn DateTime ,
	LastModifiedBy VARCHAR(100) ,
	LastModifiedOn  DateTime DEFAULT GETDATE(),
	IsActive bit DEFAULT 1,
	FOREIGN KEY (BOOKID)REFERENCES Books(BookID),
	FOREIGN KEY (GenreID)REFERENCES Genres(GenreID)
	)
------------------------------------------------------------------------------------------------------------------------

------------------------------------------------------------------------------------------------------------------------
CREATE TABLE BorrowBooks(
	BorrowBookID INT PRIMARY KEY NOT NULL IDENTITY(1,1),
	BorrowBookDate DATETIME DEFAULT GETDATE(),
	BookID INT,
	ReaderID INT,
	PublisherID INT,
	CreatedBy VARCHAR(100),
	CreatedOn DateTime ,
	LastModifiedBy VARCHAR(100) ,
	LastModifiedOn  DateTime DEFAULT GETDATE(),
	IsActive bit DEFAULT 1,
	FOREIGN KEY (BookID)REFERENCES Books(BOOKID),
	FOREIGN KEY (ReaderID)REFERENCES Readers(ReaderID),
	FOREIGN KEY (PublisherID)REFERENCES Publishers(PublisherID)
	)
------------------------------------------------------------------------------------------------------------------------
CREATE TABLE Vendors(
	VendorID INT PRIMARY KEY NOT NULL IDENTITY(1,1),
	VendorName VARCHAR(20) NOT NULL,
	VendorPhone VARCHAR(30),
	CreatedBy VARCHAR(100),
	CreatedOn DateTime ,
	LastModifiedBy VARCHAR(100) ,
	LastModifiedOn  DateTime DEFAULT GETDATE(),
	IsActive bit DEFAULT 1,
)
------------------------------------------------------------------------------------------------------------------------
CREATE TABLE VendorBooks(
	VendorBookID INT PRIMARY KEY NOT NULL IDENTITY(1,1),
	VendorBookPrice INT NOT NULL,
	BookID INT,
	VendorID INT,
	CreatedBy VARCHAR(100),
	CreatedOn DateTime ,
	LastModifiedBy VARCHAR(100) ,
	LastModifiedOn  DateTime DEFAULT GETDATE(),
	IsActive bit DEFAULT 1,
	FOREIGN KEY (BookID)REFERENCES Books(BookID),
	FOREIGN KEY (VendorID)REFERENCES Vendors(VendorID)
	)
---------------------------------------------------------------------------------------------------------------------------
CREATE TABLE BookPurchesOrders(
	BookPurchesOrderID INT PRIMARY KEY NOT NULL IDENTITY(1,1),
	BookPurchesDate DATETIME DEFAULT GETDATE(),
	VendorID INT,
	BookID INT,
	BookPurchesQuentity INT,
	CreatedBy VARCHAR(100),
	CreatedOn DateTime ,
	LastModifiedBy VARCHAR(100) ,
	LastModifiedOn  DateTime DEFAULT GETDATE(),
	IsActive bit DEFAULT 1,
	FOREIGN KEY (VendorID)REFERENCES Vendors(VendorID),
	FOREIGN KEY (BookID)REFERENCES Books(BookID)
	)
---------------------------------------------------------------------------------------------------------------------------
CREATE TABLE Fines(
	FineID INT PRIMARY KEY NOT NULL IDENTITY(1,1),
	FinePerDay INT ,
	BookID INT,
	BorrowBookID INT,
	CreatedBy VARCHAR(100),
	CreatedOn DateTime ,
	LastModifiedBy VARCHAR(100) ,
	LastModifiedOn  DateTime DEFAULT GETDATE(),
	IsActive bit DEFAULT 1
	)
-----------------------------------------------------------------------------------------------------------------------------------------
CREATE TABLE IssueBooks(
IssueBookID  INT PRIMARY KEY IDENTITY(1,1),
StudentID INT ,
IssueBookDate INT,
CreatedBy VARCHAR(100) NOT NULL,
CreatedOn DateTime DEFAULT GETDATE(),
LastModifiedBy VARCHAR(100) NULL,
LastModifiedOn  DateTime DEFAULT GETDATE(),
IsActive bit DEFAULT 1,
FOREIGN KEY (StudentID) REFERENCES Students(StudentID)
);
-----------------------------------------------------------------------------------------------------------------------------------------

CREATE TABLE IssueBooksDetails(
IssueBookDetailsID  INT PRIMARY KEY IDENTITY(1,1),
IssueBookID INT,
BookID INT,
CreatedBy VARCHAR(100) NOT NULL,
CreatedOn DateTime DEFAULT GETDATE(),
LastModifiedBy VARCHAR(100) NULL,
LastModifiedOn  DateTime DEFAULT GETDATE(),
IsActive bit DEFAULT 1,
FOREIGN KEY (BookID) REFERENCES Books(BookID)
);
-------------------------------------------------------------------------------------------------------------------------------------
SELECT * FROM Readers
SELECT * FROM Authors
SELECT * FROM Publishers
SELECT * FROM Books
SELECT * FROM BookAuthors
SELECT * FROM Employees
SELECT * FROM Genres
SELECT * FROM BookGenres
SELECT * FROM BorrowBooks
SELECT * FROM Vendors
SELECT * FROM VendorBooks
SELECT * FROM BookPurchesOrders
SELECT * FROM Fines
------------------------------------------------------------------------------------------------------------------------------------
DROP TABLE  Readers
DROP TABLE  Authors
DROP TABLE  Publishers
DROP TABLE  Books
DROP TABLE  BookAuthors
DROP TABLE  Employees
DROP TABLE  Genres
DROP TABLE  BookGenres
DROP TABLE  BorrowBooks
DROP TABLE  Vendors
DROP TABLE  VendorBooks
DROP TABLE  BookPurchesOrders
DROP TABLE  Fines
DROP TABLE IssueBooksDetails
------------------------------------------------------------------------------------------------------------------------------------
