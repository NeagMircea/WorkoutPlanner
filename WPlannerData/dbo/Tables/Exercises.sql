﻿CREATE TABLE [dbo].[Exercises]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NVARCHAR(50) NOT NULL, 
    [VideoPath] NVARCHAR(200) NULL, 
    [Sets] INT NULL DEFAULT 0, 
    [Reps] INT NULL DEFAULT 0, 
    [Duration] FLOAT NULL DEFAULT 0,

)
