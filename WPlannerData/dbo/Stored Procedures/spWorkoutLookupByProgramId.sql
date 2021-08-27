CREATE PROCEDURE [dbo].[spWorkoutLookupByProgramId]
	@ProgramId int = 0
AS
BEGIN
	SET NOCOUNT ON;
	SELECT [WorkoutId], [WorkoutName], [WorkoutProgramId], [WorkoutOrder]
	FROM [dbo].[Program_Workouts]
	WHERE [ProgramId] = @ProgramId
	ORDER BY [WorkoutOrder];
END;
