CREATE PROCEDURE [dbo].[spWorkoutProgramSwapOrder]
	@WorkoutOneId int = 0,
	@WorkoutOneOrder int = 0,
	@WorkoutTwoId int = 0,
	@WorkoutTwoOrder int = 0
AS
BEGIN
	SET NOCOUNT ON;

	UPDATE [dbo].[WorkoutPrograms]
	SET [WorkoutProgramOrder] = @WorkoutTwoOrder
	WHERE [Id] = @WorkoutOneId;

	UPDATE [dbo].[WorkoutPrograms]
	SET [WorkoutProgramOrder] = @WorkoutOneOrder
	WHERE [Id] = @WorkoutTwoId;
END;
