CREATE PROCEDURE [dbo].[spExercisesSwapOrder]
	@IdOne INT,
	@ExerciseOneOrder INT,
	@IdTwo INT,
	@ExerciseTwoOrder INT
AS
BEGIN
	SET NOCOUNT ON;

	UPDATE [dbo].[WorkoutDayExercises]
	SET [ExerciseOrder] = @ExerciseTwoOrder
	WHERE [Id] = @IdOne;

	UPDATE [dbo].[WorkoutDayExercises]
	SET [ExerciseOrder] = @ExerciseOneOrder
	WHERE [Id] = @IdTwo;
END;
