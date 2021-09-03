CREATE PROCEDURE [dbo].[spWorkoutLookupByProgramId]
	@ProgramId int = 0
AS
BEGIN
	SET NOCOUNT ON;
	SELECT [Id], [WorkoutId], [WorkoutName], [WorkoutProgramId], [WorkoutProgramOrder]
	FROM [dbo].[Program_Workouts]
	WHERE [ProgramId] = @ProgramId
	ORDER BY [WorkoutProgramOrder];
END;
