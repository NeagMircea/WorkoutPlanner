CREATE TABLE [dbo].[Categories]
(
	[CategoryId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [CategoryName] NVARCHAR(100) NOT NULL, 
    [CategoryOrder] INT NULL

	CONSTRAINT Unique_CategoryName UNIQUE([CategoryName])
)
