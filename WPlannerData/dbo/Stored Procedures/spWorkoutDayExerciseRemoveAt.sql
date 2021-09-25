CREATE PROCEDURE [dbo].[spWorkoutDayExerciseRemoveAt]
	@WorkoutId INT,
	@DayId INT,
	@ExerciseId INT
AS
BEGIN
	SET NOCOUNT ON;

	DELETE TOP(1)
	FROM [WorkoutDayExercises]
	WHERE [fk_WorkoutId] = @WorkoutId AND [fk_DayId] = @DayId AND [fk_ExerciseId] = @ExerciseId;
END;
