CREATE PROCEDURE [dbo].[spWorkoutSwapOrder]
	@WorkoutOneId INT,
	@WorkoutOneOrder INT,
	@WorkoutTwoId INT,
	@WorkoutTwoOrder INT
AS
BEGIN
	SET NOCOUNT ON;

	UPDATE [dbo].[Workouts]
	SET [WorkoutOrder] = @WorkoutTwoOrder
	WHERE [WorkoutId] = @WorkoutOneId;

	UPDATE [dbo].[Workouts]
	SET [WorkoutOrder] = @WorkoutOneOrder
	WHERE [WorkoutId] = @WorkoutTwoId;
END;
