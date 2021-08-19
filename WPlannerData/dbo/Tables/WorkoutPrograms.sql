CREATE TABLE [dbo].[WorkoutPrograms]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [fk_ProgramId] INT NOT NULL, 
    [fk_WorkoutId] INT NOT NULL, 

    CONSTRAINT [FK_WorkoutPrograms_ToWorkouts] FOREIGN KEY ([fk_WorkoutId]) REFERENCES [Workouts]([Id])
	ON UPDATE NO ACTION ON DELETE CASCADE, 

    CONSTRAINT [FK_WorkoutPrograms_ToPrograms] FOREIGN KEY ([fk_ProgramId]) REFERENCES [Programs]([Id])
	ON UPDATE NO ACTION ON DELETE CASCADE

)
