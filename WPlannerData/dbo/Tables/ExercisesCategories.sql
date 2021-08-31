CREATE TABLE [dbo].[ExercisesCategories]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [fk_ExerciseId] INT NOT NULL, 
    [fk_CategoryId] INT NOT NULL,

    CONSTRAINT [FK_ExercisesCategories_Exercises] FOREIGN KEY ([fk_ExerciseId]) REFERENCES [Exercises]([ExerciseId])
	ON UPDATE NO ACTION ON DELETE CASCADE, 

    CONSTRAINT [FK_ExercisesCategories_Categories] FOREIGN KEY ([fk_CategoryId]) REFERENCES [Categories]([CategoryId])
	ON UPDATE NO ACTION ON DELETE CASCADE,

	CONSTRAINT EC_Bridge UNIQUE([fk_ExerciseId], [fk_CategoryId])
)
