CREATE TABLE [dbo].[ExercisesDays]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [fk_ExerciseId] INT NOT NULL, 
    [fk_DayId] INT NOT NULL, 

    CONSTRAINT [FK_ExercisesDays_Exercises] FOREIGN KEY ([fk_ExerciseId]) REFERENCES [Exercises]([Id])
	ON UPDATE NO ACTION ON DELETE CASCADE, 

    CONSTRAINT [FK_ExercisesDays_Days] FOREIGN KEY ([fk_DayId]) REFERENCES [Days]([Id])
	ON UPDATE NO ACTION ON DELETE CASCADE
)
