CREATE PROCEDURE [dbo].[spWorkoutDayExercisesInsert]
	@WorkoutId INT,
	@DayId INT,
	@ExerciseId INT
AS
BEGIN
	SET NOCOUNT ON;
	INSERT INTO [WorkoutDayExercises](fk_WorkoutId, fk_DayId, fk_ExerciseId)
	VALUES (@WorkoutId, @DayId, @ExerciseId);

	UPDATE [WorkoutDayExercises]
	SET [ExerciseOrder] = SCOPE_IDENTITY()
	WHERE Id = SCOPE_IDENTITY();
END;
