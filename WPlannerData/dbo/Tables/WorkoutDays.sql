CREATE TABLE [dbo].[WorkoutDays]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [fk_WorkoutId] INT NOT NULL, 
    [fk_DayId] INT NOT NULL, 

    CONSTRAINT [FK_WorkoutDays_ToWorkouts] FOREIGN KEY ([fk_WorkoutId]) REFERENCES [Workouts]([Id])
	ON UPDATE NO ACTION ON DELETE CASCADE, 

    CONSTRAINT [FK_WorkoutDays_ToDays] FOREIGN KEY ([fk_DayId]) REFERENCES [Days]([Id])
	ON UPDATE NO ACTION ON DELETE CASCADE,

	CONSTRAINT [Unique_Workout_Days] UNIQUE (fk_WorkoutId, fk_DayId)
)
