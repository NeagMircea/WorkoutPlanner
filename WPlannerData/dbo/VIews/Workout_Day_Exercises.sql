CREATE VIEW [dbo].[Workout_Day_Exercises]
	AS 
	SELECT [WorkoutDayExercises].[Id], [Workouts].[WorkoutId], [Days].[DayId], [Exercises].*, [ExerciseOrder]
	FROM [WorkoutDayExercises]

	JOIN [Workouts] ON [WorkoutDayExercises].[fk_WorkoutId] = [Workouts].[WorkoutId]
	JOIN [Days] ON [WorkoutDayExercises].[fk_DayId] = [Days].[DayId]
	JOIN [Exercises] ON [WorkoutDayExercises].[fk_ExerciseId] = [Exercises].[ExerciseId];
