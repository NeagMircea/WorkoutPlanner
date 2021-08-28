CREATE PROCEDURE [dbo].[spWorkoutGetAll]
AS
BEGIN
	SET NOCOUNT ON;

	SELECT *
	FROM [dbo].[Workouts]
	ORDER BY [WorkoutOrder];
END;
