CREATE TABLE [dbo].[Subcategories]
(
	[SubcategoryId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [SubcategoryName] NVARCHAR(100) NOT NULL,
	[SubcategoryInfo] NVARCHAR(3000) NULL

	CONSTRAINT Unique_SubcatName UNIQUE([SubcategoryName])   
)


