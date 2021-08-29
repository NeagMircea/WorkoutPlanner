CREATE PROCEDURE [dbo].[spWorkoutExercisesInsert]
	@WorkoutId INT = 0,
	@ExerciseId INT = 0
AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO [WorkoutExercises](fk_WorkoutId, fk_ExerciseId)
	VALUES (@WorkoutId, @ExerciseId);

	UPDATE [WorkoutExercises]
	SET [ExerciseOrder] = SCOPE_IDENTITY()
	WHERE [Id] = SCOPE_IDENTITY();
END;
