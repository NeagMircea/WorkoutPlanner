CREATE TABLE [dbo].[WorkoutDayExercises]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [fk_WorkoutId] INT NOT NULL, 
    [fk_DayId] INT NOT NULL, 
    [fk_ExerciseId] INT NOT NULL, 
    [ExerciseOrder] INT NULL, 

    CONSTRAINT [FK_WorkoutDayExercises_Workouts] FOREIGN KEY ([fk_WorkoutId]) REFERENCES [Workouts]([WorkoutId])
	ON UPDATE NO ACTION ON DELETE CASCADE, 

    CONSTRAINT [FK_WorkoutDayExercises_Days] FOREIGN KEY ([fk_DayId]) REFERENCES [Days]([DayId])
	ON UPDATE NO ACTION ON DELETE CASCADE, 

    CONSTRAINT [FK_WorkoutDayExercises_Exercises] FOREIGN KEY ([fk_ExerciseId]) REFERENCES [Exercises]([ExerciseId])
	ON UPDATE NO ACTION ON DELETE CASCADE
)
