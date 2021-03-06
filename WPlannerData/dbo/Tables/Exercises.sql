CREATE TABLE [dbo].[Exercises]
(
	[ExerciseId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ExerciseName] NVARCHAR(50) NOT NULL, 
    [VideoPath] NVARCHAR(200) NULL, 
    [Sets] INT NULL DEFAULT 0, 
    [MinReps] INT NULL DEFAULT 0, 
	[MaxReps] INT NULL DEFAULT 0, 
    [Duration] FLOAT NULL DEFAULT 0,

)
