CREATE PROCEDURE [dbo].[spExercisesGetByWorkoutDayId]
	@WorkoutId INT,
	@DayId INT
AS
BEGIN
	SET NOCOUNT ON;
	
	SELECT [Id], [ExerciseId], [ExerciseName], [VideoPath], [Sets], [MinReps], [MaxReps], [Duration], [ExerciseOrder]
	FROM [Workout_Day_Exercises]
	WHERE [WorkoutId] = @WorkoutId AND [DayId] = @DayId
	ORDER BY [ExerciseOrder];
END;