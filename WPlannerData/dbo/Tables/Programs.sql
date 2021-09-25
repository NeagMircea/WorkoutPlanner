CREATE TABLE [dbo].[Programs]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NVARCHAR(100) NOT NULL DEFAULT 'My New Program', 
    [ProgramOrder] INT NULL

	CONSTRAINT Unique_ProgramName UNIQUE([Name])
)
