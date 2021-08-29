CREATE TABLE [dbo].[WorkoutExercises]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [fk_WorkoutId] INT NOT NULL, 
    [fk_ExerciseId] INT NOT NULL, 
    [ExerciseOrder] INT NULL, 

    CONSTRAINT [FK_WorkoutExercises_ToWorkouts] FOREIGN KEY ([fk_WorkoutId]) REFERENCES [Workouts]([WorkoutId]), 
    CONSTRAINT [FK_WorkoutExercises_ToExercises] FOREIGN KEY ([fk_ExerciseId]) REFERENCES [Exercises]([Id])
)
