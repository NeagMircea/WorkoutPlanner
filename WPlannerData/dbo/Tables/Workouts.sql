CREATE TABLE [dbo].[Workouts]
(
	[WorkoutId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [WorkoutName] VARCHAR(100) NULL DEFAULT 'My New Workout'
)
