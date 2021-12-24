CREATE TABLE [dbo].[Workouts]
(
	[WorkoutId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [WorkoutName] VARCHAR(100) NULL DEFAULT 'My New Workout', 
    [WorkoutOrder] INT NULL,
	[WorkoutInfo] NVARCHAR(3000) NULL

	CONSTRAINT Unique_WorkoutName UNIQUE([WorkoutName])   
)
